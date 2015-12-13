using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public interface INavigationImage
    {
        void navigationTo(BitmapImage image);
        
    }
}
