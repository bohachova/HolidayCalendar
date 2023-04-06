using System.Collections.Generic;
using Newtonsoft.Json;

namespace HolidayCalendar.DataObject
{
    public class HolidaysResponse
    {
        [JsonProperty("holidays")]
        public List<Holiday> Holidays { get; set; }
    }
}
