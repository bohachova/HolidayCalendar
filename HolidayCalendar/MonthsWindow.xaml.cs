using System.Windows;
using System.Windows.Controls;
using HolidayCalendar.DataObject;
using HolidayCalendar.BL;

namespace HolidayCalendar
{
    /// <summary>
    /// Interaction logic for MonthsWindow.xaml
    /// </summary>
    public partial class MonthsWindow : Window
    {
        public MonthsOfTheYear month;
       
        public MonthsWindow()
        {
            InitializeComponent();
        }

        private void MonthButton_Click(object sender, RoutedEventArgs e)
        {
            month = MonthConverter.ConvertMonthToEnum((sender as Button).Content.ToString());
            DialogResult = true;
            Close();
        }
    }
}
