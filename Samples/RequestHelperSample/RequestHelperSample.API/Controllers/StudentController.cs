using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestHelperSample.Data.Helpers;
using RequestHelperSample.Data.Models;
using RequestHelperSample.Data.Repositories;
using RequestHelperSample.Data.ViewModels;

namespace RequestHelperSample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("GetAll")]
        public StudentsSearchResponse GetAll()
        {
            var searchResponse = new StudentsSearchResponse();
            searchResponse.Students = _studentRepository.GetAll();

            return searchResponse;
        }


        [HttpGet("Search")]
        public StudentsSearchResponse SearchStudents([FromQuery]StudentsSearchRequest request, [FromQuery]bool test)
        {
            var students = _studentRepository.GetAllQ();

            if (request.HeightFrom != null)
            {
                students = students.Where(x => x.Height > request.HeightFrom);
            }
            if (request.HeightTo != null)
            {
                students = students.Where(x => x.Height < request.HeightTo);
            }

            if (request.WeightFrom != null)
            {
                students = students.Where(x => x.Weight > request.WeightFrom);
            }
            if (request.WeightTo != null)
            {
                students = students.Where(x => x.Weight < request.WeightTo);
            }

            if (!String.IsNullOrEmpty(request.NameSearchText))
            {
                students = students.Where(x => x.StudentName.Contains(request.NameSearchText) || request.NameSearchText.Contains(x.StudentName));
            }

            if (request.SelectedGradeIds != null && request.SelectedGradeIds.Count > 0)
            {
                students = students.Where(x => request.SelectedGradeIds.Contains(x.GradeId));
            }

            var searchResponse = new StudentsSearchResponse();
            searchResponse.Students = students.ToList();

            return searchResponse;
        }

        [HttpPost("UpdatePhoto")]
        public async Task UpdatePhoto([FromForm]LoadPhotoRequest request)
        {
            string pictureFileName = FileHelper.SaveFile(request.Photo);
            var student = await _studentRepository.GetByIdAsync(request.StudentId);

            student.PhotoImageName = pictureFileName;
            _studentRepository.Update(student);
            await _studentRepository.CommitAsync();
        }
    }
}