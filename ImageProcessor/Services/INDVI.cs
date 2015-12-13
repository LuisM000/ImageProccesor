using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public interface INDVI
    {
        List<ItemProportions> Results { get; set; }
        Func<double, byte[]> ColorRamp { get; set; }
        bool RampAdjustment { get; set; }
        double MinValue { get; set; }
        double MaxValue { get; set; }

        BitmapImage getNDVI(BitmapImage imageOriginal, BitmapImage imageNIR);
        BitmapImage getNDVI(BitmapImage imageOriginal, BitmapImage imageNIR, Segmentation segmentation);
        BitmapImage getNDVI(BitmapImage imageNIR);
        BitmapImage getNDVI(BitmapImage imageNIR, Segmentation segmentation);


    }
}
