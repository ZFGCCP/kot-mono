using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gears.Cloud
{
    public static class VersionManager
    {
        private static string _GearsVGEVersion = "ALPHA";
        private static string _version;
        public static string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }
        public static string GearsVGEVersion
        {
            get { return _GearsVGEVersion; }
        }
    }
}
