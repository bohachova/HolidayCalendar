using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using HolidayCalendar.DataObject;

namespace HolidayCalendar.BL
{
    internal static class APISettingsUtility
    {
        const string PATH = @"apiSettings.json";
        internal static APISettings Settings { get; set; }

        internal static void ReadSettings ()
        {
            if (File.Exists(PATH))
            {
                try
                {
                    var data = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), PATH));
                    Settings = JsonConvert.DeserializeObject<APISettings>(data);
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
