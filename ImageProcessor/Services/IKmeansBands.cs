using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public interface IKmeansBands
    {
        List<ItemProportions> Results { get; set; }
        int Clusters { get; set; }
        double Tolerance { get; set; }
        string TypeDistance { get; set; }

        BitmapImage getImage(BitmapImage bitmap, bool chanel1, bool chanel2, bool chanel3);
        BitmapImage getImage(BitmapImage bitmapIR, BitmapImage bitmapOriginal, bool chanel1IR, bool chanel2IR, bool chanel3IR,
                                    bool chanel1Original, bool chanel2Original, bool chanel3Original, List<int> returnBands);
    }
}
