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
    public class KmodesBands:IKmodesBands
    {

        public List<ItemProportions> Results { get; set; }
        public int Clusters { get; set; }
        public double Tolerance { get; set; }

        public BitmapImage getImage(BitmapImage bitmap, bool chanel1, bool chanel2, bool chanel3)
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
                List<int> currentValue = new List<int>();
                currentValue.Add((chanel1) ? pixels[i] : 0);
                currentValue.Add((chanel2) ? pixels[i + 1] : 0);
                currentValue.Add((chanel3) ? pixels[i + 2] : 0);
                pixelsTwo[valueCurrent] = currentValue.ToArray<int>();
                i += 4;
                valueCurrent = i / 4;
            }

            KModes kmodes = new KModes(this.Clusters);
            int[] idx = kmodes.Compute(pixelsTwo,this.Tolerance);
            pixelsTwo.ApplyInPlace((x, i) => kmodes.Clusters.Centroids[idx[i]]);

            int index = 0;
            Results = new List<ItemProportions>(this.Clusters);
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


        public BitmapImage getImage(BitmapImage bitmapIR, BitmapImage bitmapOriginal, bool chanel1IR, bool chanel2IR, bool chanel3IR,
                                   bool chanel1Original, bool chanel2Original, bool chanel3Original, List<int> returnBands)
        {
            var height = bitmapIR.PixelHeight;
            var width = bitmapIR.PixelWidth;
            var stride = 4 * width;
            var pixels = new byte[height * stride];
            var pixelsIR = new byte[height * stride];
            var pixelsOriginal = new byte[height * stride];
            bitmapIR.CopyPixels(pixelsIR, stride, 0);
            bitmapOriginal.CopyPixels(pixelsOriginal, stride, 0);


            int[][] pixelsTwo = new int[pixelsIR.Length / 4][];
            int valueCurrent = 0;

            for (int i = 0; i < pixelsIR.Length; )
            {
                List<int> currentValue = new List<int>();
                currentValue.Add((chanel1IR) ? pixelsIR[i] : 0);
                currentValue.Add((chanel2IR) ? pixelsIR[i + 1] : 0);
                currentValue.Add((chanel3IR) ? pixelsIR[i + 2] : 0);

                currentValue.Add((chanel1Original) ? pixelsOriginal[i] : 0);
                currentValue.Add((chanel2Original) ? pixelsOriginal[i + 1] : 0);
                currentValue.Add((chanel3Original) ? pixelsOriginal[i + 2] : 0);


                pixelsTwo[valueCurrent] = currentValue.ToArray<int>();
                i += 4;
                valueCurrent = i / 4;
            }

            KModes kmodes = new KModes(this.Clusters);
            int[] idx = kmodes.Compute(pixelsTwo, this.Tolerance);
            pixelsTwo.ApplyInPlace((x, i) => kmodes.Clusters.Centroids[idx[i]]);


            int index = 0;
            Results = new List<ItemProportions>(this.Clusters);
            foreach (var item in kmodes.Clusters)
            {
                ItemProportions ik = new ItemProportions();
                ik.Colors = new List<double>();
                for (int i = 0; i < returnBands.Count; i++)
                {
                    if (returnBands[i] != -1)
                    {
                        ik.Colors.Add(item.Mode[returnBands[i]]);
                    }
                    else
                    {
                        ik.Colors.Add(0);
                    }
                }
                ik.Proportion = item.Proportion;
                ik.index = index;
                Results.Add(ik);
                index++;
            }
            Results = Results.OrderByDescending(C => C.Proportion).ToList();

            int auxValue = 0;
            for (int i = 0; i < pixelsTwo.Length; i++)
            {
                if (returnBands[0] != -1) { pixels[auxValue] = (byte)pixelsTwo[i][returnBands[0]]; } else { pixels[auxValue] = 0; }
                if (returnBands[1] != -1) { pixels[auxValue + 1] = (byte)pixelsTwo[i][returnBands[1]]; } else { pixels[auxValue + 1] = 0; }
                if (returnBands[2] != -1) { pixels[auxValue + 2] = (byte)pixelsTwo[i][returnBands[2]]; } else { pixels[auxValue + 2] = 0; }
                pixels[auxValue + 3] = (byte)255;
                auxValue += 4;
            }
            var bitmapVI = BitmapSource.Create(
                      width, height, 96d, 96d, bitmapIR.Format, null, pixels, stride);

            bitmapVI.Freeze();
            var imageReturn = BitmapTransform.BitmapProcess.ConvertBitmapSourceToBitmapImage(bitmapVI);
            bitmapVI = null;
            pixelsTwo = null;
            pixelsIR = null;
            pixelsOriginal = null;
            pixels = null;
            return imageReturn;
        }

        
    }
}
