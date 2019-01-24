using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelperTestApp.Models;
using UrlHelper.Helpers;
using System.Net.Http;

namespace HelperTestApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string uri = "https://localhost:44390/GetValue";


            var model = new TestUrlModel();

            model.Test = "asaa";
            model.ListValues = new List<List<TestUrlModel>>() { new List<TestUrlModel>() { new TestUrlModel() { Test = "1" }, new TestUrlModel() { Test = "2" } }, new List<TestUrlModel>() { new TestUrlModel() { Test = "11" }, new TestUrlModel() { Test = "22" } } };
            model.StringValues = new List<string>() { "aaa", "bbb", "cccc" };
            model.Value = 220;

            ParameterCollection parameters = ParameterCollection.CreateFromModel(model);


            var values = parameters.ToString();

            var httpClient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();

            response = await httpClient.GetAsync(uri + "?" + parameters.ToString());
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class TestUrlModel
    {
        public string Test { get; set; }
        public List<List<TestUrlModel>> ListValues { get; set; }
        public int Value { get; set; }
        public List<string> StringValues { get; set; }
    }
}
