using RequestHelperSample.Data.Context;
using RequestHelperSample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestHelperSample.Data.DbInitializer
{
    public static class DbInitializer
    {
        private static Random _random = new Random();

        public static void AddTestData(this DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Grades.Count() > 0 && context.Students.Count() > 0)
            {
                return;
            }

            var grades = GetTestGrades();

            context.Grades.AddRange(
                GetTestGrades());
            context.SaveChanges();

            context.Students.AddRange(
                GetTestStudents(context.Grades.ToList()));
            context.SaveChanges();
        }

        private static List<Student> GetTestStudents(List<Grade> grades)
        {
            List<Student> testStudents = new List<Student>();

            List<string> names = new List<string>()
            {
                "Angelique Mailloux",
                "Aundrea Simonds",
                "Youlanda Krell",
                "Claudette Stalder",
                "Valeri Gaillard",
                "Cassaundra Dear",
                "Melda Mcgruder",
                "Louisa Shingleton",
                "Williemae Marrinan",
                "Cecile Thelen",
                "Ellyn Royals",
                "Coleman Port",
                "Jefferey Epperson"
            };

            foreach (var name in names)
            {
                testStudents.Add(new Student()
                {
                    StudentName = name,
                    Height = 120,
                    Weight = 40,
                    PhotoImageName = null,
                    GradeId = grades[_random.Next(0, grades.Count)].Id
                });
            }

            return testStudents;
        }

        private static List<Grade> GetTestGrades()
        {
            List<Grade> testGrades = new List<Grade>();

            List<string> names = new List<string>()
            {
                "A1", "B2", "C3", "D4", "G5", "E6"
            };

            foreach (var name in names)
            {
                testGrades.Add(new Grade()
                {
                    GradeName = name,
                    Section = "None",
                });
            }

            return testGrades;
        }
    }
}
