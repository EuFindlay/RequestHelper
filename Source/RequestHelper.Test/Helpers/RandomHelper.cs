using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RequestHelper.Test.Helpers
{
    public static class RandomHelper
    {
        private static Random random = new Random();

        public static string GetRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
            return new string(Enumerable.Repeat(chars, random.Next(5, 100))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GetRandomInt()
        {
            return random.Next(Int32.MaxValue);
        }

        public static DateTime GetRandomDate()
        {
            DateTime start = new DateTime(2000, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        public static DateTime? GetRandomNullableDate()
        {
            bool isNullable = GetRandomBool();
            if (isNullable)
            {
                return null;
            }

            return GetRandomDate();
        }

        public static bool GetRandomBool()
        {
            return random.Next(0, 2) == 1;
        }
    }
}
