using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using RequestHelper.Attributes;
using RequestHelper.Enums;
using RequestHelper.Extensions;
using RequestHelper.Models;

namespace RequestHelper
{
    public class RequestParameters
    {
        private ParametersDictionary _urlParameters = new ParametersDictionary();

        internal ParametersDictionary UrlParameters
        {
            get { return _urlParameters; }
        }

        public RequestParameters() { }

        public RequestParameters(ParametersDictionary parameters)
        {
            _urlParameters = parameters;
        }

        public void Add(string key, object val)
        {
            TypeDefinition propertyType = val.GetUrlType();

            switch (propertyType)
            {
                case TypeDefinition.Simple:
                    AddSimpleValue(ref _urlParameters, key, val);
                    break;
                case TypeDefinition.Array:
                    AddArrayValue(ref _urlParameters, key, val);
                    break;
                case TypeDefinition.Model:
                    AddModelValue(ref _urlParameters, key, val);
                    break;
                default: break;
            };
        }

        public static RequestParameters CreateFromModel(object model)
        {
            var parameters = GetParametersListFromModel(model);
            return new RequestParameters(parameters);
        }

        private static ParametersDictionary GetParametersListFromModel(object model, bool basedOnModel = false, string modelName = null)
        {
            IList<PropertyInfo> propertiesForConverting = GetPropertiesForConverting(model);

            ParametersDictionary parameters = new ParametersDictionary();

            foreach (PropertyInfo property in propertiesForConverting)
            {
                object propertyValue = property.GetValue(model, null);
                string propertyName = property.GetCustomAttribute<UrlConverter>()?.Name ?? property.Name;

                if (propertyValue == null || String.IsNullOrEmpty(propertyName))
                {
                    continue;
                }

                TypeDefinition propertyType = property.PropertyType.GetUrlType();

                switch (propertyType)
                {
                    case TypeDefinition.Simple:
                        AddSimpleValue(ref parameters, propertyName, propertyValue, basedOnModel, modelName);
                        break;
                    case TypeDefinition.Array:
                        AddArrayValue(ref parameters, propertyName, propertyValue, basedOnModel, modelName);
                        break;
                    case TypeDefinition.Model:
                        AddModelValue(ref parameters, propertyName, propertyValue, basedOnModel, modelName);
                        break;
                    default: break;
                }
            }

            return parameters;
        }

        private static List<PropertyInfo> GetPropertiesForConverting(Object model)
        {
            Type objectType = model.GetType();
            return new List<PropertyInfo>(
                objectType.GetProperties().Where(x => x.GetCustomAttribute<UrlIgnore>() == null && x.GetCustomAttribute<FileParameter>() == null));
        }

        public override string ToString()
        {
            var encodedUrlBuilder = new StringBuilder();
            foreach (var elementPair in _urlParameters)
            {
                if (encodedUrlBuilder.Length > 0)
                {
                    encodedUrlBuilder.Append("&");
                }

                encodedUrlBuilder.AppendFormat("{0}={1}",
                    WebUtility.UrlEncode(elementPair.Key),
                    WebUtility.UrlEncode(elementPair.Value));
            }

            return encodedUrlBuilder.ToString();
        }

        public void AddParametersToUrl(ref string url)
        {
            if (_urlParameters != null)
            {
                url += $"?{this.ToString()}";
            }
        }

        #region Add property value methods 

        private static void AddArrayValue(ref ParametersDictionary parameters, string propertyName, object propertyValue, bool basedOnModel = false, string modelName = null)
        {
            var propertyValues = ((IEnumerable)propertyValue).Cast<object>().ToList();

            if (basedOnModel)
            {
                propertyName = $"{modelName}.{propertyName}";
            }

            for (int index = 0; index < propertyValues.Count; index++)
            {
                var value = propertyValues[index];

                switch (value.GetUrlType())
                {
                    case TypeDefinition.Simple:
                        AddSimpleValue(ref parameters, $"{propertyName}[{index}]", value, false, null);
                        break;
                    case TypeDefinition.Array:
                        AddArrayValue(ref parameters, $"{propertyName}[{index}]", value, false, null);
                        break;
                    case TypeDefinition.Model:
                        AddModelValue(ref parameters, $"{propertyName}[{index}]", value, false, null);
                        break;
                }
            }
        }

        private static void AddSimpleValue(ref ParametersDictionary parameters, string propertyName, object propertyValue, bool basedOnModel = false, string modelName = null)
        {
            string convertedValue = propertyValue.ToString();

            if (basedOnModel && !String.IsNullOrEmpty(modelName))
            {
                parameters.Add($"{modelName}.{propertyName}", convertedValue);
            }
            else
            {
                parameters.Add(propertyName, convertedValue);
            }
        }

        private static void AddModelValue(ref ParametersDictionary parameters, string propertyName, object propertyValue, bool basedOnModel = false, string modelName = null)
        {
            if (basedOnModel && !String.IsNullOrEmpty(modelName))
            {
                propertyName = $"{modelName}.{propertyName}";
            }

            parameters.AddRange(GetParametersListFromModel(propertyValue, true, propertyName));
        }

        #endregion
    }
}

