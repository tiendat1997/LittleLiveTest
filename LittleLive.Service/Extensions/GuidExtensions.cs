using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Service.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid? guidValue)
        {
            return !guidValue.HasValue || guidValue.Equals(Guid.Empty);
        }
    }
}
