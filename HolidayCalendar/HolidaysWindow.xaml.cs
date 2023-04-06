using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HolidayCalendar.DataObject;

namespace HolidayCalendar
{
    /// <summary>
    /// Interaction logic for HolidaysWindow.xaml
    /// </summary>
    public partial class HolidaysWindow : Window
    {
        MainApiResponse response;
      
        public HolidaysWindow(MainApiResponse response, int year, string month, int day)
        {
            InitializeComponent();
            this.response = response;
            infoLabel.Content = $"Holidays on {day} {month} {year}";
            ShowHolidaysList();
        }
        private void ShowHolidaysList ()
        {
            if (response.Response.Holidays != null && response.Response.Holidays.Count > 0)
            {
                foreach (var item in response.Response.Holidays)
                    CreateLabel(item.Name);
            }
            else
                CreateLabel("There are no holidays on this day");
        }

        private void CreateLabel (string text)
        {
            TextBlock tb = new TextBlock();
            tb.Text = text;
            tb.FontFamily = new FontFamily("Comic Sans MS");
            tb.FontWeight = FontWeights.Bold;
            tb.FontSize = 20;
            tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF935A20"));
            tb.TextWrapping = TextWrapping.Wrap;
            holidaysPanel.Children.Add(tb);
        }
    }
}
