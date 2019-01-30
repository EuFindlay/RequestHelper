using Microsoft.AspNetCore.Mvc.Rendering;
using RequestHelperSample.Data.Models;
using RequestHelperSample.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestHelperSample.ViewModels
{
    public class StudentSearchModel
    {
        public StudentsSearchRequest SearchParameters { get; set; } 
        public List<Student> SearchResult { get; set; }
        public List<SelectListItem> AvailableGrades { get; set; }
    }
}
