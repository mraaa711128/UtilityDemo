using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Extensions;

namespace Utility.Extensions
{
    public static class StringExtension
    {
        public static bool IsNumeric(this string value)
        {
            if (value.IsNullOrEmpty()) { return false; }
            decimal result;
            return decimal.TryParse(value, out result); ;
        }

        public static bool IsNullOrEmpty(this string value, bool includeSpace = true)
        {
            if (value is null) { return true; }
            if (includeSpace && value.Trim() == "") { return true; }
            return false;
        }

        public static string NullableTrim(this string value)
        {
            if (value is null) { return ""; }
            return value.Trim();
        }

        public static DateTime ToAcDate(this string value)
        {
            if (value.IsNullOrEmpty()) { throw new ArgumentNullException(); }
            if (value.Length < 7 || value.Length > 8) { throw new ArgumentOutOfRangeException(); }
            if (value.Length == 7) {
                var strYear = value.Substring(0, 3);
                var strMonthDay = $"{value.Substring(3, 2)}/{value.Substring(5, 2)}";
                var strAcDate = $"{strYear.ToAcYear()}/{strMonthDay}";
                return DateTime.Parse(strAcDate); ;
            }
            if (value.Length == 8) {
                var strYear = value.Substring(0, 4);
                var strMonthDay = $"{value.Substring(4, 2)}/{value.Substring(6, 2)}";
                var strAcDate = $"{strYear}/{strMonthDay}";
                return DateTime.Parse(strAcDate);
            }
            return DateTime.MaxValue;
        }

        public static int ToAcYear(this string value)
        {
            if (value.IsNullOrEmpty()) { throw new ArgumentNullException(); }
            var strYear = value.PadLeft(3, '0');
            if (!strYear.IsNumeric()) { throw new ArgumentOutOfRangeException(); }
            var intYear = int.Parse(strYear);
            return intYear + 1911;
        }
        public static DateTime ToTime(this string value)
        {
            if (value.IsNullOrEmpty()) { throw new ArgumentNullException(); }
            if (value.Length != 4 && value.Length != 6) { throw new ArgumentOutOfRangeException(); }
            var strValue = value.PadRight(6, '0');
            if (!value.IsNumeric()) { throw new ArgumentOutOfRangeException(); }
            var strDate = DateTime.Today.ToString("yyyy/MM/dd");
            var strTime = $"{strValue.Substring(0, 2)}:{strValue.Substring(2, 2)}:{strValue.Substring(4, 2)}";
            return DateTime.Parse($"{strDate} {strTime}");
        }

        public static DateTime ToAcDateTime(this string[] value)
        {
            if (value.IsNullOrEmpty()) { throw new ArgumentNullException(); }
            if (value.Length != 2) { throw new ArgumentOutOfRangeException(); }
            var acDate = value[0].ToAcDate();
            var acTime = value[1].ToTime();
            return new DateTime(acDate.Year, acDate.Month, acDate.Day, acTime.Hour, acTime.Minute, acTime.Second);
        }

        public static string ToComplement(this string value)
        {
            if (value.IsNullOrEmpty()) { throw new ArgumentNullException(); }
            if (!value.IsNumeric()) { throw new ArgumentOutOfRangeException(); }
            var max = "9".PadLeft(value.Length, '9');
            var complement = int.Parse(max) - int.Parse(value);
            return complement.ToString().PadLeft(value.Length, '0');
        }

        public static string Repeat(this string value, int Count = 0)
        {
            if (value.IsNullOrEmpty(false)) { throw new ArgumentNullException(); }
            for (int i = 0; i < Count - 1; i++)
            {
                value += value;
            }
            return value;
        }

        public static string FromHexToUnicode(this string value) {
            if (value.IsNullOrEmpty()) { throw new ArgumentNullException(); }
            if (value.Length % 2 > 0) { throw new ArgumentOutOfRangeException(); }
            var textBytes = new byte[value.Length / 2];
            for (int i = 0; i < value.Length / 2; i++) {
                textBytes[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
            }
            return Encoding.Unicode.GetString(textBytes);
        }

        public static string FromUnicodeToHex(this string value) {
            if (value.IsNullOrEmpty()) { throw new ArgumentNullException(); }
            var textBytes = Encoding.Unicode.GetBytes(value);
            return Convert.ToHexString(textBytes);
        }

        public static DateTime FromBirthDateToAcDate(this string value)
        {
            if (value.IsNullOrEmpty()) { throw new ArgumentNullException(); }
            if ("+,-".Contains(value.Substring(0, 1)))
            {
                if (value.Length != 8) { throw new ArgumentOutOfRangeException(); }
                if (!value.Substring(1,3).IsNumeric()) { throw new ArgumentOutOfRangeException(); }
                bool isNegative = (value.Substring(0, 1) == "-");
                var chiYear = int.Parse(value.Substring(1, 3));
                var acYear = (isNegative ? 1911 - chiYear : 1911 + chiYear);
                if (!value.Substring(4, 4).IsNumeric()) { throw new ArgumentOutOfRangeException(); }
                var acMonth = int.Parse(value.Substring(4, 2));
                var acDay = int.Parse(value.Substring(6, 2));
                return new DateTime(acYear, acMonth, acDay);
            } else
            {
                return value.ToAcDate();
            }
        }
    }
}
