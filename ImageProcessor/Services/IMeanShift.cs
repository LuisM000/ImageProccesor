using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public interface IMeanShift
    {
        List<ItemProportions> Results { get; set; }

        BitmapImage getImage(BitmapImage bitmap, double constKernel,string typeKernel,
            double sigma,double tolerance, int maxIterations);
    }
}
