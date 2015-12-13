using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ImageProcessor.Converters
{
    public class DoubleArrayToRGBConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var actualValue = (List<double>)value;

            return "(" + (byte)actualValue[2] + "," + (byte)actualValue[1] + "," + (byte)actualValue[0] + ")";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
}
}
