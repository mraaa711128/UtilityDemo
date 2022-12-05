using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Extensions;

namespace Utility.Comparer
{
    public class ChineseDateStringComparer : StringComparer
    {
        public override int Compare(string x, string y)
        {
            var xDate = x.ToAcDate();
            var yDate = y.ToAcDate();
            if (xDate < yDate) return -1;
            if (xDate > yDate) return 1;
            return 0;
        }

        public override bool Equals(string x, string y)
        {
            var xDate = x.ToAcDate();
            var yDate = y.ToAcDate();
            return xDate == yDate;
        }

        public override int GetHashCode(string obj)
        {
            var date = obj.ToAcDate();
            return date.GetHashCode();
        }
    }

    public class ChineseDateComplementStringComparer : StringComparer
    {
        public override int Compare(string x, string y)
        {
            var rx = reverseComplement(x);
            var ry = reverseComplement(y);
            var xDate = rx.ToAcDate();
            var yDate = ry.ToAcDate();
            if (xDate < yDate) return 1;
            if (xDate > yDate) return -1;
            return 0;
        }

        public override bool Equals(string x, string y)
        {
            var rx = reverseComplement(x);
            var ry = reverseComplement(y);
            var xDate = rx.ToAcDate();
            var yDate = ry.ToAcDate();
            return xDate == yDate;
        }

        public override int GetHashCode(string obj)
        {
            var rObj = reverseComplement(obj);
            var date = rObj.ToAcDate();
            return date.GetHashCode();
        }

        private string reverseComplement(string xc)
        {
            var xcVal = int.Parse(xc);
            var xVal = 9999999 - xcVal;
            return xVal.ToString().PadLeft(7, '0');
        }
    }
}
