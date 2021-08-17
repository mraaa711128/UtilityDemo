using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToChineseDate(this DateTime value)
        {
            return $"{(value.Year - 1911).ToString("000")}{value.Month.ToString("00")}{value.Day.ToString("00")}";
        }

        public static string ToYmTime(this DateTime value, int length = 6)
        {
            if (length != 4 && length != 6) { throw new ArgumentOutOfRangeException(); }
            if (length == 6)
            {
                return $"{value.Hour.ToString("00")}{value.Minute.ToString("00")}{value.Second.ToString("00")}";
            } else
            {
                return $"{value.Hour.ToString("00")}{value.Minute.ToString("00")}";
            }
            
        }
    }
}
