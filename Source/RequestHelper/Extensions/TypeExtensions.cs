﻿using System;
using System.Collections.Generic;
using RequestHelper.Enums;
using Microsoft.AspNetCore.Http;

namespace RequestHelper.Extensions
{
    public static class TypeExtensions
    {
        public static TypeDefinition GetUrlType(this Type objectType)
        {
            if (objectType.IsSimple())
            {
                return TypeDefinition.Simple;
            }

            if (objectType.IsArray())
            {
                return TypeDefinition.Array;
            }

            return TypeDefinition.Model;
        }

        private static bool IsSimple(this Type objectType)
        {
            return objectType.IsPrimitive
              || objectType.IsNullableSimple()
              || objectType.IsEnum
              || objectType.Equals(typeof(String))
              || objectType.Equals(typeof(DateTime))
              || objectType.Equals(typeof(Decimal));
        }

        private static bool IsArray(this Type objectType)
        {
            return objectType.IsArray
                || (objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(List<>)));
        }

        private static bool IsFile(this Type objectType)
        {
            return objectType.Equals(typeof(IFormFile));
        }

        private static bool IsNullableSimple(this Type objectType)
        {
            var underlyingType = Nullable.GetUnderlyingType(objectType);

            if (underlyingType == null)
            {
                return false;
            }
            
            return underlyingType.IsPrimitive
              || underlyingType.Equals(typeof(String))
              || underlyingType.Equals(typeof(DateTime))
              || underlyingType.Equals(typeof(Decimal));
        }

    }
}
