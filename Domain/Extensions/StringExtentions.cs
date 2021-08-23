using System;

namespace Domain.Extensions
{
    public static class StringExtentions
    {
        public static void Guard(this string str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
