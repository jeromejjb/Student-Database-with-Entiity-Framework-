using System;
using System.Collections.Generic;

#nullable disable

namespace StudentDbwithEntity.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Hometown { get; set; }
        public string Ffood { get; set; }
    }
}
