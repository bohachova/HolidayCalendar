using System.Collections.Generic;
using Newtonsoft.Json;

namespace HolidayCalendar.DataObject
{
    public class Holiday
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
