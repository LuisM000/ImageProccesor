using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Services
{
    public class NavigationImage:INavigationImage
    {
        public void navigationTo(System.Windows.Media.Imaging.BitmapImage image)
        {
            var window = new ImageWindow(image);
            window.Show();
        }
    }
}
