using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ImageProcessor.Converters
{
    public class DoubleArrayToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var actualValue = (List<double>)value;
            Color color = new Color();
            color.R = (byte)actualValue[2];
            color.G = (byte)actualValue[1];
            color.B = (byte)actualValue[0];
            return hexConverter(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Color.FromRgb(200, 200, 200);
        }

        private string hexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        //private string RGBConverter(System.Drawing.Color c)
        //{
        //    return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        //}
    }
}
