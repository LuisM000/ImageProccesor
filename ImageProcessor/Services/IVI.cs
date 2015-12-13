using Accord.Math;
using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public interface IVI
    {
        List<ItemProportions> Results { get; set; }
        Func<double, byte[]> ColorRamp { get; set; }
        bool RampAdjustment { get; set; }
        double MinValue { get; set; }
        double MaxValue { get; set; }


        BitmapImage getVI(BitmapImage imageOriginal, BitmapImage imageNIR);
        BitmapImage getVI(BitmapImage imageNIR);
        BitmapImage getVI(BitmapImage imageOriginal, BitmapImage imageNIR, Segmentation segmentation);
        BitmapImage getVI(BitmapImage imageNIR, Segmentation segmentation);
    }
}
