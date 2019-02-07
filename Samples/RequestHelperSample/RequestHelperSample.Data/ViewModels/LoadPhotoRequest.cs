using Microsoft.AspNetCore.Http;
using RequestHelper.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestHelperSample.Data.ViewModels
{
    public class LoadPhotoRequest
    {
        public int StudentId { get; set; }
        [FileParameter]
        public IFormFile Photo { get; set; }
    }
}
