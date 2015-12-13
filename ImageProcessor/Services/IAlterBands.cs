using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public interface IAlterBands
    {
        BitmapImage getImage(BitmapImage bitmap, int[] order);
    }
}
