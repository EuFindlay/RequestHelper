using System;
using System.Collections.Generic;
using System.Text;

namespace RequestHelper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UrlConverter : Attribute
    {
        public readonly string Name;

        public UrlConverter(string name)
        {
            Name = name;
        }
    }
}
