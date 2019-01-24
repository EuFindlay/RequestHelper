using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using UrlHelper;
using UrlHelper.Attributes;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            FormFile file = null;
            var stream = File.OpenRead(@"C:\Users\Eugene\Pictures\Capture.png");
            
                file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/png"
                };

            
            var formModel = new FormModel()
            {
                Image = file,
                Test = "aaaa",
                List = new List<string>() { "a", "b", "c" }
            };

            var result = MultipartFormDataBuilder.Convert(formModel);

        }
    }

    public class FormModel
    {
        [FileParameter]
        public IFormFile Image { get; set; }

        public string Test { get; set; }

        public List<string> List { get; set; }
    }
}
