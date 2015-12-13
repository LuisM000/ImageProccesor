using Accord.MachineLearning;
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
    public class Kmodes:IKModes
    {
        public List<ItemProportions> Results { get; set; }


        public BitmapImage getImage(BitmapImage bitmap, int clusters, double tolerance)
        {
            var height = bitmap.PixelHeight;
            var width = bitmap.PixelWidth;
            var stride = 4 * width;
            var pixels = new byte[height * stride];
            bitmap.CopyPixels(pixels, stride, 0);

            int[][] pixelsTwo = new int[pixels.Length / 4][];

            int valueCurrent = 0;

            for (int i = 0; i < pixels.Length; )
            {
                pixelsTwo[valueCurrent] = new int[] { pixels[i], pixels[i + 1], pixels[i + 2] };
                i += 4;
                valueCurrent = i / 4;
            }

            KModes kmodes = new KModes(clusters);
            int[] idx = kmodes.Compute(pixelsTwo,tolerance);
            pixelsTwo.ApplyInPlace((x, i) => kmodes.Clusters.Centroids[idx[i]]);

            int index = 0;
            Results = new List<ItemProportions>(clusters);
            foreach (var item in kmodes.Clusters)
            {
                ItemProportions ik = new ItemProportions();
                ik.Colors = new List<double>(item.Mode.ToDouble());
                ik.Proportion = item.Proportion;
                ik.index = index;
                Results.Add(ik);
                index++;
            }
            Results = Results.OrderByDescending(C => C.Proportion).ToList();

            int auxValue = 0;
            for (int i = 0; i < pixelsTwo.Length; i++)
            {
                pixels[auxValue] = (byte)pixelsTwo[i][0];
                pixels[auxValue + 1] = (byte)pixelsTwo[i][1];
                pixels[auxValue + 2] = (byte)pixelsTwo[i][2];
                pixels[auxValue + 3] = (byte)255;
                auxValue += 4;
            }
            var bitmapVI = BitmapSource.Create(
                      width, height, 96d, 96d, bitmap.Format, null, pixels, stride);

            bitmapVI.Freeze();
            var imageReturn = BitmapTransform.BitmapProcess.ConvertBitmapSourceToBitmapImage(bitmapVI);
            bitmapVI = null;
            pixelsTwo = null;
            pixels = null;
            return imageReturn;
        }
    }
}
