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
    public class Kmeans:IKMeans
    {
        public List<ItemProportions> Results { get; set; }

        public BitmapImage getImage(BitmapImage bitmap, int clusters, double tolerance, string typeDistance)
        {
            var height = bitmap.PixelHeight;
            var width = bitmap.PixelWidth;
            var stride = 4 * width;
            var pixels = new byte[height * stride];
            bitmap.CopyPixels(pixels, stride, 0);

            double[][] pixelsTwo = new double[pixels.Length / 4][];

            int valueCurrent = 0;

            for (int i = 0; i < pixels.Length; )
            {
                pixelsTwo[valueCurrent] = new double[] { pixels[i], pixels[i+1], pixels[i+2] };
                i += 4;
                valueCurrent = i / 4;
            }

            KMeans kmeans = new KMeans(clusters, getDistance(typeDistance));
            kmeans.Tolerance = tolerance;
            int[] idx = kmeans.Compute(pixelsTwo);
            pixelsTwo.ApplyInPlace((x, i) => kmeans.Clusters.Centroids[idx[i]]);

            int index=0;
            Results = new List<ItemProportions>(clusters);
            foreach (var item in kmeans.Clusters)
            {
                ItemProportions ik = new ItemProportions();
                ik.Colors = new List<double>(item.Mean);
                ik.Proportion = item.Proportion;
                ik.index = index;
                Results.Add(ik);
                index++;
            }
            Results=Results.OrderByDescending(C => C.Proportion).ToList();

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

      
        private Func<double[], double[], double> getDistance(string typeDistance)
        {
            switch (typeDistance)
            {
                case "BrayCurtis":
                    return Distance.BrayCurtis;
                case "Chessboard":
                    return Distance.Chessboard;
                case "Correlation":
                    return Distance.Correlation;
                case "Cosine":
                    return Distance.Cosine;
                case "Euclidean":
                    return Distance.Euclidean;
                case "Manhattan":
                    return Distance.Manhattan;
                case "SquareEuclidean":
                    return Distance.SquareEuclidean;

                default:
                    return Distance.Euclidean;
            }
        }
    }
}
