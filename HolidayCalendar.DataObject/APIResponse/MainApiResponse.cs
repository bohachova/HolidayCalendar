using Newtonsoft.Json;

namespace HolidayCalendar.DataObject
{
    public class MainApiResponse
    {
        [JsonProperty("response")]
        public HolidaysResponse Response { get; set; }
    }
}
