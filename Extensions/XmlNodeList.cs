using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utility.Extensions {
    public static class XmlNodeListExtension {
        public static bool IsNullOrEmpty(this XmlNodeList value) {
            if (value is null) { return true; }
            if (value.Count <= 0) { return true; }
            return false;
        }

    }
}
