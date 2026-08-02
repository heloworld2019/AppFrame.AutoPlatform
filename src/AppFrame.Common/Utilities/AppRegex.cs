using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppFrame.Common.Utilities
{
    public static class AppRegex
    {
        public static bool MatchIpAddress(string ip)
        {
            string RegexText = "@\"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$\"";
            return Regex.Match(ip.ToString(), RegexText).Success;
        }
    }
}
