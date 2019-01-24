using System;
using System.Collections.Generic;
using System.Text;

namespace RequestHelper.Models
{
    public class ParametersDictionary : List<KeyValuePair<string, string>>
    {
        public void Add(string key, string value)
        {
            var element = new KeyValuePair<string, string>(key, value);
            Add(element);
        }
    }
}
