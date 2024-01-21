using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using U_Task_Note.Model;

namespace U_Task_Note.ViewModel
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var priority = (Priority)value;
            switch (priority)
            {
                case Priority.Низький:
                    return new SolidColorBrush(Colors.Green);
                case Priority.Середній:
                    return new SolidColorBrush(Colors.Yellow);
                case Priority.Високий:
                    return new SolidColorBrush(Colors.Red);
                default:
                    return new SolidColorBrush(Colors.Gray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}