using System;
using System.Globalization;
using Xamarin.Forms;

namespace DemoApp.Converters
{
    public class StatusColorConverter : IValueConverter
    {
        public StatusColorConverter()
        {
        }

        // when view asks the viewmodel object
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ?
                (Color)Application.Current.Resources["CompletedColor"] :
                (Color)Application.Current.Resources["ActiveColor"];
        }

        // when viewmodel reads the view object
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
