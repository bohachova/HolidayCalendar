using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HolidayCalendar.DataObject;
using HolidayCalendar.BL;

namespace HolidayCalendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int gridRowIndex = 0;
        int gridColumnIndex = -1;
        public MainWindow()
        {
            InitializeComponent();
            selectedYearLabel.Content = DateTime.Now.ToString("yyyy");
            selectedMonthLabel.Content = StringFormatHelper.GetFormattedString(DateTime.Now);
            ShowCalendar();
        }

        private void MonthsPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            monthsTooltip.IsOpen = false;
        }
        private void MonthsPopup_MouseEnter(object sender, MouseEventArgs e)
        {
            monthsTooltip.IsOpen = true;
        }

        private void MonthsLink_Click (object sender, RoutedEventArgs e)
        {
            MonthsWindow monthsWindow = new MonthsWindow();
            if (monthsWindow.ShowDialog() == true)
                selectedMonthLabel.Content = monthsWindow.month;
        }

        private void yearDecrementButton_Click(object sender, RoutedEventArgs e)
        { 
            int selectedYear = GetSelectedYear() - 1;
            if (selectedYear < 2000)
                MessageBox.Show("Can not be less than 2000", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                selectedYearLabel.Content = selectedYear.ToString();
        }

        private void yearIncrementButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedYear = GetSelectedYear() + 1;
            if (selectedYear >2040)
                MessageBox.Show("Can not be more than 2040", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                selectedYearLabel.Content = selectedYear.ToString();
        }

        private int GetSelectedYear () => int.Parse(selectedYearLabel.Content.ToString());

        private void monthDecrementButton_Click(object sender, RoutedEventArgs e)
        {
            MonthsOfTheYear month = MonthConverter.ConvertMonthToEnum(selectedMonthLabel.Content.ToString());
            if (month == MonthsOfTheYear.January)
                selectedMonthLabel.Content = MonthsOfTheYear.December;
            else
                selectedMonthLabel.Content = (MonthsOfTheYear)((int)month - 1);

            ShowCalendar();
        }

        private void monthIncrementButton_Click(object sender, RoutedEventArgs e)
        {
            MonthsOfTheYear month = MonthConverter.ConvertMonthToEnum(selectedMonthLabel.Content.ToString());
            if (month == MonthsOfTheYear.December)
                selectedMonthLabel.Content = MonthsOfTheYear.January;
            else
                selectedMonthLabel.Content = (MonthsOfTheYear)((int)month + 1);

            ShowCalendar();
        }

        private void ShowCalendar ()
        {
            calendarGrid.Children.Clear();
            CalendarFormatHelper calendarHelper = new CalendarFormatHelper(int.Parse(selectedYearLabel.Content.ToString()), GetSelectedMonthNumber());

            List<List<int>> calendarDates = calendarHelper.GetFullCalendarDates();

            CreateCalendarLabels(calendarDates[0], false);
            CreateCalendarLabels(calendarDates[1], true);
            CreateCalendarLabels(calendarDates[2], false);

            gridColumnIndex = -1;
            gridRowIndex = 0;
        }

        private void CreateCalendarLabels(List<int> dates, bool isCurrentMonth)
        {
            if (dates != null)
            {
                foreach (var date in dates)
                {
                    TextBlock tb = new TextBlock();
                    tb.Text = date.ToString();
                    tb.FontFamily = new FontFamily("Comic Sans MS");
                    tb.FontSize = 30;
                    tb.HorizontalAlignment = HorizontalAlignment.Center;
                    tb.VerticalAlignment = VerticalAlignment.Center;

                    if (isCurrentMonth)
                    {
                        tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF935A20"));
                        tb.Cursor = Cursors.Hand;
                        tb.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown;
                    }  
                    else
                        tb.Foreground = Brushes.DarkGray;

                    if (gridColumnIndex == 6)
                    {
                        gridColumnIndex = -1;
                        gridRowIndex++;
                    }

                    gridColumnIndex++;

                    Grid.SetRow(tb, gridRowIndex);
                    Grid.SetColumn(tb, gridColumnIndex);

                    calendarGrid.Children.Add(tb);
                }
            }
        }

        private async void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            loadingGrid.Visibility = Visibility.Visible;
            calendarGrid.IsEnabled = false;
            int year = GetSelectedYear();
            int day = int.Parse((sender as TextBlock).Text);
            MainApiResponse response = await ShowHolidaysList(year, GetSelectedMonthNumber(), day);
            ShowHolidaysWindow(response, year, selectedMonthLabel.Content.ToString(), day );
            calendarGrid.IsEnabled = true;
            loadingGrid.Visibility = Visibility.Hidden;
        }

        private Task<MainApiResponse> ShowHolidaysList (int year, int month, int day)
        {
            HolidaysApiClient client = new HolidaysApiClient();
            return client.GetHolidays(year, month, day);
        }

        private int GetSelectedMonthNumber () => (int)MonthConverter.ConvertMonthToEnum(selectedMonthLabel.Content.ToString());

        private void ShowHolidaysWindow (MainApiResponse response, int year, string month, int day)
        {
            HolidaysWindow holidaysWindow = new HolidaysWindow(response, year, month, day);
            holidaysWindow.ShowDialog();
        }

        
       
    }
}
