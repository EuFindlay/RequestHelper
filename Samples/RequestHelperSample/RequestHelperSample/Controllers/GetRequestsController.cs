using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RequestHelperSample.Controllers
{
    public class GetRequestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
