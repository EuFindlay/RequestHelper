using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RequestHelper.Test.Helpers
{
    public static class RandomHelper
    {
        private static Random random = new Random();

        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
            return new string(Enumerable.Repeat(chars, random.Next(5, 100))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomInt()
        {
            return random.Next(Int32.MaxValue);
        }
    }
}
