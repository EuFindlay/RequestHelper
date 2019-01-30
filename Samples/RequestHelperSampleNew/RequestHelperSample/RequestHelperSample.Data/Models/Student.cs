using System;
using System.Collections.Generic;
using System.Text;

namespace RequestHelperSample.Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string PhotoImageName { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
