using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RequestHelperSample.Data.Repositories;
using RequestHelperSample.Data.ViewModels;
using RequestHelperSample.ViewModels;
using RequestHelper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RequestHelperSample.Data.Models;
using Microsoft.AspNetCore.Hosting;

namespace RequestHelperSample.Controllers
{
    public class SamplesController : Controller
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;

        public SamplesController(IOptionsSnapshot<AppSettings> settings)
        {
            _settings = settings;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Students(StudentsSearchRequest searchRequest)
        {
            var model = new StudentSearchModel();

            if (searchRequest != null)
            {
                var searchParameters = RequestParameters.CreateFromModel(searchRequest); // Generates url parameters from searchRequest object
                searchParameters.Add("Test", true); // Also, you can specify additional parameters

                var response = await GetRequest($@"{_settings.Value.ApiUrl}/api/Student/search", searchParameters);
                var responseObject = JsonConvert.DeserializeObject<StudentsSearchResponse>(
                            await response.Content.ReadAsStringAsync());

                model.SearchResult = responseObject.Students;
            }

            model.SearchParameters = searchRequest;

            var gradeResponse = await GetRequest($@"{_settings.Value.ApiUrl}/api/Grade/get-all");
            var greadesList = JsonConvert.DeserializeObject<List<Grade>>(
                        await gradeResponse.Content.ReadAsStringAsync());

            model.AvailableGrades = greadesList.Select(x =>
                new SelectListItem(x.GradeName, x.Id.ToString(),
                    searchRequest.SelectedGradeIds == null ? false : searchRequest.SelectedGradeIds.Contains(x.Id))).ToList();

            return View(model);
        }

        public async Task<IActionResult> StudentsInfo()
        {
            var model = new StudentsInfoModel();

            var response = await GetRequest($@"{_settings.Value.ApiUrl}/api/Student/get-all");
            var responseObject = JsonConvert.DeserializeObject<StudentsSearchResponse>(
                        await response.Content.ReadAsStringAsync());

            model.Students = responseObject.Students;

            return View(model);
        }

        public async Task<IActionResult> LoadStudentPhoto(LoadPhotoRequest request)
        {
            MultipartFormDataContent dataModel = MultipartFormDataBuilder.ConvertModelToFormData(request);
            var response = await PostFileModel($@"{_settings.Value.ApiUrl}/api/Student/update-photo", dataModel);

            var data = await response.Content.ReadAsStringAsync();

            return Ok();
        }

        private async Task<HttpResponseMessage> GetRequest(string uri, RequestParameters parameters = null)
        {
            if (parameters != null)
            {
                uri += "?" + parameters;
            }

            var httpClient = new HttpClient();

            HttpResponseMessage response = new HttpResponseMessage();
            response = await httpClient.GetAsync(uri);

            return response;
        }

        private async Task<HttpResponseMessage> PostFileModel(string uri, MultipartFormDataContent dataModel)
        {
            var httpClient = new HttpClient();

            HttpResponseMessage response = new HttpResponseMessage();
            response = await httpClient.PostAsync(uri, dataModel);

            return response;
        }
    }
}