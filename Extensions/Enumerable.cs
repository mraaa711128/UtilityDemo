using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extensions.Enumerable
{
    public static class EnumerableExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            if (value is null) { return true; }
            if (value.Count() <= 0) { return true; }
            return false;
        }
    }
}
