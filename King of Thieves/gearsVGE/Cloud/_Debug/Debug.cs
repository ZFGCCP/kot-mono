using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gears.Cloud._Debug
{
    /// <summary>
    /// A class to handle static debug functions.
    /// </summary>
    public static class Debug
    {
        private const string _systemOut = @"Debug\DebugOut.log";

        /// <summary>
        /// Outputs a message to the debug out log file and adds a datetime stamp.
        /// </summary>
        /// <param name="msg">The output message.</param>
        public static void Out(string msg)
        {
            try
            {
                System.IO.File.AppendAllText(_systemOut, System.DateTime.Now + " " + msg + "\r\n");
            }
            catch (Exception) { }
        }
    }
}
