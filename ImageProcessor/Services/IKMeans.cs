using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
   
    public interface IKMeans
    {
        List<ItemProportions> Results { get; set; }
        BitmapImage getImage(BitmapImage bitmap, int clusters, double tolerance, string typeDistance);
    }
}
