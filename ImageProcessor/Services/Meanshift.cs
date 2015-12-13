using Accord.MachineLearning;
using Accord.Math;
using Accord.Statistics.Distributions.DensityKernels;
using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public class Meanshift:IMeanShift
    {
        public List<ItemProportions> Results { get; set; }

        public BitmapImage getImage(BitmapImage bitmap, double constKernel, string typeKernel, double sigma, double tolerance, int maxIterations)
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
                pixelsTwo[valueCurrent] = new double[] { pixels[i], pixels[i + 1], pixels[i + 2] };
                i += 4;
                valueCurrent = i / 4;
            }

            MeanShift meanS = new MeanShift(3, getKernel(typeKernel, constKernel), sigma);
            int[] idx = meanS.Compute(pixelsTwo, tolerance,maxIterations);
            pixelsTwo.ApplyInPlace((x, i) => meanS.Clusters.Modes[idx[i]]);


            int index = 0;
            Results = new List<ItemProportions>();
            foreach (var item in meanS.Clusters)
            {
                ItemProportions ik = new ItemProportions();
                ik.Colors = new List<double>(item.Mode);
                double cols = (double)pixelsTwo.Where(array => array.IsEqual(ik.Colors.ToArray<double>())).Count() / (double)pixelsTwo.Count();
                ik.Proportion = cols;
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


        private IRadiallySymmetricKernel getKernel(string typeKernel,double constKernel)
        {
            switch (typeKernel)
            {
                case "EpanechnikovKernel":
                    return new EpanechnikovKernel(constKernel);
                case "GaussianKernel":
                    return new GaussianKernel(constKernel);
                case "UniformKernel":
                    return new UniformKernel(constKernel);
                
                default:
                    return new UniformKernel();
            }
        }
    }
}
