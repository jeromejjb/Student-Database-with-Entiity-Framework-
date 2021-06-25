using StudentDbwithEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentDbwithEntity
{
    class Program
    {
        static void Main(string[] args)
        {

            DbStudentsContext db = new DbStudentsContext();
            MainDisplay(db);
        }

        public static void MainDisplay(DbStudentsContext db)
        {
            bool goOn = true;
            while (goOn == true)
            {
                Console.WriteLine("Welcome to our C# Class");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1) Learn more about a current student"); 
                Console.WriteLine("2) Register a new student");
                int choice = GetInteger(2);


                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Students in the class: (enter a number 1-12)");
                        PrintClassmatesById(db);
                        break;

                    case 2:
                        Console.WriteLine("Register a student");
                        AddStudent(db);
                        break;
                }

                goOn = GetContinue();
                Console.Clear();
            }
        }

        public static void PrintClassmatesById(DbStudentsContext db)
        {
            List<Student> students = ReadInStudents(db);
            foreach (Student s in students)
            {
                Console.WriteLine($"{s.Id}: {s.Fname}");
            }
            Student student = ChooseStudentById(students);
            PrintStudentInfo(student);
        }

        public static Student ChooseStudentById(List<Student> students)
        {
            //Max needs to increase to add students to the index 
            Console.WriteLine("Please enter the ID of the Classmate whose info you would like to view:");
            int max = students.Count();
            int id = GetInteger(max);
            List<Student> queryResults = (from student in students
                                          where student.Id == id
                                          select student).ToList();
            Student s = queryResults[0];
            return s;
        }

        public static List<Student> ReadInStudents(DbStudentsContext db)
        {
            List<Student> students = db.Students.ToList();
            return students;
        }

        public static void PrintStudentInfo(Student s)
        {

            Console.WriteLine($"ID: {s.Id} \t Name:  {s.Fname} \t Favorite Food: {s.Hometown} \t Hometown: {s.Ffood}");
            Console.WriteLine();
        }

        public static int GetInteger(int maxChoices)
        {
            string input = "";
            int output = 0;
            try
            {
                input = Console.ReadLine();
                output = int.Parse(input);
                if (output > maxChoices || output < 1)
                {
                    throw new Exception("That number is out of range. Try again.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("That was not a valid input.");
                output = GetInteger(maxChoices);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                output = GetInteger(maxChoices);
            }
            return output;
        }

        public static Student MakeStudent()
        {
        Student student = new Student();
        Console.WriteLine("Enter the new student's name:");
            student.Fname = Console.ReadLine();

            Console.WriteLine("Enter the new classmate's hometown:");
            student.Hometown = Console.ReadLine();

            Console.WriteLine("Enter the new classmate's favorite food:");
            student.Ffood = Console.ReadLine();

            return student; 
        }

    public static void AddStudent(DbStudentsContext db)
        {
            //This is the same as a INSERT statement in SQL 
            Student newStudent = MakeStudent();
            db.Students.Add(newStudent);
            db.SaveChanges();
            Console.WriteLine($"{newStudent.Fname} has been enrolled in the C# course");
        }

        public static bool GetContinue()
        {
            Console.WriteLine("Would you like to learn about anyone else? Y/N");
            string input = Console.ReadLine();
            if (input.Trim().ToLower() == "y")
            {
                return true;
            }
            else if (input.Trim().ToLower() == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine("I don't understand that input. Please try again.");
                return GetContinue();
            }
        }
    }
}
