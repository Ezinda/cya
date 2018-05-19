using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.Helpers
{
    public static class MethodsHelpers
    {
        public static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}