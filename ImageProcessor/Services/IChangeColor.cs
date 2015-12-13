using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public interface IChangeColor
    {
        List<ItemProportions> Results { get; set; }

        BitmapImage changeColor(BitmapImage bitmap, Color originalColor, Color returnColor, List<ItemProportions> results);
    }
}
