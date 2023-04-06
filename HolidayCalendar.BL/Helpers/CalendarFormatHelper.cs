using System;
using System.Collections.Generic;

namespace HolidayCalendar.BL
{
    public class CalendarFormatHelper
    {
        int selectedYear;
        int selectedMonth;
        DateTime firstDayOfMonth;
        DateTime lastDayOfMonth;
        int previousMonthDaysCount;
        int nextMonthDayCount;


        public CalendarFormatHelper(int year, int month)
        {
            selectedMonth = month;
            selectedYear = year;
            GetAssistanceData();
        }

        public List<List<int>> GetFullCalendarDates()
        {
            List<List<int>> calendarDates = new List<List<int>>();
            calendarDates.Add(GetPreviousMonthDates());
            calendarDates.Add(GetSelectedMonthDates());
            calendarDates.Add(GetNextMonthDates());

            return calendarDates;
        }

        private List<int> GetNextMonthDates()
        {
            List<int> nextMonthDates = new List<int>();
            DateTime firstDayOfNextMonth = lastDayOfMonth.AddDays(1);
            int date = firstDayOfNextMonth.Day;
            nextMonthDates.Add(date);
            if (nextMonthDayCount != 7)
            {
                for (int i = 1; i < nextMonthDayCount; i++)
                {
                    date++;
                    nextMonthDates.Add(date);
                }

                return nextMonthDates;
            }
            return null;
        }

        private List<int> GetPreviousMonthDates()
        {
            List<int> previousMonthDates = new List<int>();
            DateTime lastDayOfPreviousMonth = firstDayOfMonth.AddDays(-1);
            int date = lastDayOfPreviousMonth.Day;
            previousMonthDates.Add(date);
            if (previousMonthDaysCount != 0)
            {
                for (int i = 1; i < previousMonthDaysCount; i++)
                {
                    date--;
                    previousMonthDates.Add(date);
                }
                previousMonthDates.Reverse();
                return previousMonthDates;
            }
            return null;
        }

        private List<int> GetSelectedMonthDates()
        {
            List<int> selectedMonthDates = new List<int>();
            for (int i = firstDayOfMonth.Day; i <= lastDayOfMonth.Day; i++)
                selectedMonthDates.Add(i);
           
            return selectedMonthDates;
        }

        private void GetAssistanceData ()
        {
            firstDayOfMonth = new DateTime(selectedYear, selectedMonth, 1);
            lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            DayOfWeek firstDayOfWeek = firstDayOfMonth.DayOfWeek;
            DayOfWeek lastDayOfWeek = lastDayOfMonth.DayOfWeek;

            previousMonthDaysCount = (int)firstDayOfWeek - 1;
            nextMonthDayCount = 7 - (int)lastDayOfWeek;
        }
    }
}
