using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestHelperSample.Data.Repositories;

namespace RequestHelperSample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            var grades = await _gradeRepository.GetAllAsync();

            return Ok(grades);
        }
    }
}