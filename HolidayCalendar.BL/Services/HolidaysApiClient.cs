using System;
using System.Net.Http;
using System.Threading.Tasks;
using HolidayCalendar.DataObject;
using Newtonsoft.Json;

namespace HolidayCalendar.BL
{
    public class HolidaysApiClient
    {
        public async Task<MainApiResponse> GetHolidays (int year, int month, int day)
        {
            APISettingsUtility.ReadSettings();
            using HttpClient client = new HttpClient();

            client.BaseAddress = new Uri($"{APISettingsUtility.Settings.URL}/holidays?&api_key={APISettingsUtility.Settings.Key}&country={APISettingsUtility.Settings.Country}&year={year}&month={month}&day={day}");

            return await GetResultAsync (client);
        }

        async Task<MainApiResponse> GetResultAsync(HttpClient client)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MainApiResponse>(result);
            }
            catch
            {
                return new MainApiResponse();
            }
        }
    }
}