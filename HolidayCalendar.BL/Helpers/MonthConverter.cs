using System;
using HolidayCalendar.DataObject;

namespace HolidayCalendar.BL
{
    public static class MonthConverter
    {
        public static MonthsOfTheYear ConvertMonthToEnum (string month) 
            => (MonthsOfTheYear)Enum.Parse(typeof(MonthsOfTheYear), month);
    }
}
