using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;

namespace AsfStartUp.Auxiliary.Converters
{
    public class StatusValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool status = (bool)value;
            return !status;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool status = (bool)value;
            return !status;
        }
        public StatusValueConverter()
        { }
    }
}
