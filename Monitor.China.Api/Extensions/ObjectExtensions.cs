using System;

namespace Monitor.China.Api.Extensions
{
    public static class ObjectExtensions
    {
        public static void Guard(this object obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
