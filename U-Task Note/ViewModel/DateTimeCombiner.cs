using System;
using System.Globalization;
using System.Windows.Data;

namespace U_Task_Note.ViewModel
{
    public class DateTimeCombiner : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is DateTime date && values[1] is TimeSpan time)
            {
                return date.Date + time;
            }
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return new object[] { dateTime.Date, dateTime.TimeOfDay };
            }
            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }
    }
}