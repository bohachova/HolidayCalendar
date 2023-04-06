using System;
using System.Globalization;

namespace HolidayCalendar.BL
{
    public static class StringFormatHelper
    {
        public static string GetFormattedString (DateTime date)
        {
            string s = date.ToString("MMMM", new CultureInfo("en-US"));
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
