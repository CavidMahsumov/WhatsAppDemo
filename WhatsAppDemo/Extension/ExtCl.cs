using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppDemo.Extension
{
    public static  class ExtCl
    {
        public static bool ValidateJSON(this string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
        }
        public static bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
