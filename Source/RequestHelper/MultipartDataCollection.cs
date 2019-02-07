using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using RequestHelper.Attributes;
using RequestHelper.Models;

namespace RequestHelper
{
    public class MultipartFormDataBuilder
    {
        public static MultipartFormDataContent ConvertModelToFormData(object model)
        {
            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            List<PropertyInfo> fileProperties = GetPropertiesForConverting(model);
            AddNonFilePropertiesToCollection(model, ref multiContent);

            AddFilePropertiesToCollection(fileProperties, model, ref multiContent);

            return multiContent;
        }

        private static void AddFilePropertiesToCollection(List<PropertyInfo> fileProperties, object model, ref MultipartFormDataContent formContent)
        {
            byte[] data;

            foreach (var fileProperty in fileProperties)
            {
                IFormFile file = fileProperty.GetValue(model, null) as IFormFile;

                using (var reader = new BinaryReader(file.OpenReadStream()))
                {
                    data = reader.ReadBytes((int)file.OpenReadStream().Length);
                }

                ByteArrayContent bytes = new ByteArrayContent(data);
                var name = fileProperty.GetCustomAttribute<UrlConverter>()?.Name;
                name = String.IsNullOrEmpty(name) ? fileProperty.Name : name;

                formContent.Add(bytes, name, file.FileName);
            }
        }

        private static void AddNonFilePropertiesToCollection(object model, ref MultipartFormDataContent formContent)
        {
            ParametersDictionary generatedParameters = RequestParameters.CreateFromModel(model).UrlParameters;

            foreach (var parameter in generatedParameters)
            {
                formContent.Add(new StringContent(parameter.Value), $"\"{parameter.Key}\"");
            }
        }

        private static List<PropertyInfo> GetPropertiesForConverting(Object model)
        {
            Type objectType = model.GetType();
            return new List<PropertyInfo>(
                objectType.GetProperties().Where(x => x.GetCustomAttribute<FileParameter>() != null));
        }
    }
}