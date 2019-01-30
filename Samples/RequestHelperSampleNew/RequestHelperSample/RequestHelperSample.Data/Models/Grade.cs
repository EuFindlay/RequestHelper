using System;
using System.Collections.Generic;
using System.Text;

namespace RequestHelperSample.Data.Models
{
    public class Grade
    {
        public Grade()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
