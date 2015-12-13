using Accord.MachineLearning;
using Accord.Math;
using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public class VI:IVI
    {
        public List<ItemProportions> Results { get; set; }
        public Func<double, byte[]> ColorRamp { get; set; }
        public bool RampAdjustment { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public BitmapImage getVI(BitmapImage imageOriginal, BitmapImage imageNIR)
        {
            var height = imageOriginal.PixelHeight;
            var width = imageOriginal.PixelWidth;
            var stride = 4 * width;

            var pixels = new byte[height * stride];
            var pixelsOrig = new byte[height * stride];
            var pixelsNIR = new byte[height * stride];
            imageOriginal.CopyPixels(pixelsOrig, stride, 0);
            imageNIR.CopyPixels(pixelsNIR, stride, 0);


            restarMinMax();
            checkColorRamp();
            for (int i = 0; i < pixelsOrig.Length; )
            {
                var red = (double)pixelsOrig[i + 2] == 0 ? 1 : (double)pixelsOrig[i + 2];
                var vi = (double)pixelsNIR[i + 2] / (double)red;

                checkMinMax(vi);
                var color = ColorRamp(vi);
                pixels[i++] = color[0];
                pixels[i++] = color[1];
                pixels[i++] = color[2];
                pixels[i++] = color[3];
            }

            if (this.RampAdjustment && (this.MaxValue != 1 || this.MinValue != -1))
            {
                double min, max;
                min = 255; max = 0;
                for (int i = 0; i < pixelsOrig.Length; )
                {
                    var red = (double)pixelsOrig[i + 2] == 0 ? 1 : (double)pixelsOrig[i + 2];
                    var vi = (double)pixelsNIR[i + 2] / (double)red;

                    vi = scaleAdjustment(vi, this.MinValue, this.MaxValue, 0,255);
                    var color = ColorRamp(vi);
                    checkMinMaxAux(vi, ref min, ref max);
                    pixels[i++] = color[0];
                    pixels[i++] = color[1];
                    pixels[i++] = color[2];
                    pixels[i++] = color[3];
                }
                this.MinValue = min; this.MaxValue = max;
            }


            var bitmapVI = BitmapSource.Create(
                width, height, 96d, 96d, imageNIR.Format, null, pixels, stride);

            bitmapVI.Freeze();
            return BitmapTransform.BitmapProcess.ConvertBitmapSourceToBitmapImage(bitmapVI);
        }
        public BitmapImage getVI(BitmapImage imageOriginal, BitmapImage imageNIR, Segmentation segmentation)
        {
            var height = imageOriginal.PixelHeight;
            var width = imageOriginal.PixelWidth;
            var stride = 4 * width;
            var pixels = new byte[height * stride];
            var pixelsNIR = new byte[height * stride];
            imageOriginal.CopyPixels(pixels, stride, 0);
            imageNIR.CopyPixels(pixelsNIR, stride, 0);

            double[][] pixelsTwo = new double[pixels.Length / 4][];
            int valueCurrent = 0;

            restarMinMax();
            checkColorRamp();
            for (int i = 0; i < pixels.Length; )
            {
                var red = (double)pixels[i + 2] == 0 ? 1 : (double)pixels[i + 2];
                var vi = (double)pixelsNIR[i + 2] / (double)red;
                checkMinMax(vi);
                 pixelsTwo[valueCurrent] = new double[] { vi };
                i += 4;
                valueCurrent = i / 4;
            }


            valueCurrent = 0;
            if (this.RampAdjustment && (this.MaxValue != 1 || this.MinValue != -1))
            {
                double min, max;
                min = 255; max = 0;
                for (int i = 0; i < pixels.Length; )
                {
                    var red = (double)pixels[i + 2] == 0 ? 1 : (double)pixels[i + 2];
                    var vi = (double)pixelsNIR[i + 2] / (double)red;
                    vi = scaleAdjustment(vi, this.MinValue, this.MaxValue, 0, 255);
                    checkMinMaxAux(vi, ref min, ref max);
                    pixelsTwo[valueCurrent] = new double[] { vi };
                    i += 4;
                    valueCurrent = i / 4;
                }
                this.MinValue = min; this.MaxValue = max;
            }



            var newPixels = getSegmentation(pixelsTwo, segmentation);
            valueCurrent = 0;
            for (int i = 0; i < pixels.Length; )
            {
                var color = ColorRamp(newPixels[valueCurrent][0]);
                pixels[i++] = color[0];
                pixels[i++] = color[1];
                pixels[i++] = color[2];
                pixels[i++] = color[3];
                valueCurrent = i / 4;
            }
            var bitmapVI = BitmapSource.Create(
                width, height, 96d, 96d, imageNIR.Format, null, pixels, stride);

            bitmapVI.Freeze();
            return BitmapTransform.BitmapProcess.ConvertBitmapSourceToBitmapImage(bitmapVI);
        }
        public BitmapImage getVI(BitmapImage imageNIR)
        {
            //Debug.Assert(imageNIR.Format.BitsPerPixel == 32);
            var height = imageNIR.PixelHeight;
            var width = imageNIR.PixelWidth;
            var stride = 4 * width;
            var pixels = new byte[height * stride];
            var pixelsNIR = new byte[height * stride];
            imageNIR.CopyPixels(pixelsNIR, stride, 0);

            restarMinMax();
            checkColorRamp();
            for (int i = 0; i < pixels.Length; )
            {
                var red = (double)pixelsNIR[i + 1] == 0 ? 1 : (double)pixelsNIR[i + 1];
                var vi = (double)pixelsNIR[i + 2] / red;
                checkMinMax(vi);
                var color = ColorRamp(vi);
                pixels[i++] = color[0];
                pixels[i++] = color[1];
                pixels[i++] = color[2];
                pixels[i++] = color[3];
            }

            if (this.RampAdjustment && (this.MaxValue != 1 || this.MinValue != -1))
            {
                double min, max;
                min = 255; max = 0;
                for (int i = 0; i < pixels.Length; )
                {
                    var red = (double)pixelsNIR[i + 1] == 0 ? 1 : (double)pixelsNIR[i + 1];
                    var vi = (double)pixelsNIR[i + 2] / red;
                    vi = scaleAdjustment(vi, this.MinValue, this.MaxValue, 0,255);
                    checkMinMaxAux(vi, ref min, ref max);
                   
                    var color = ColorRamp(vi);
                    pixels[i++] = color[0];
                    pixels[i++] = color[1];
                    pixels[i++] = color[2];
                    pixels[i++] = color[3];
                }
                this.MinValue = min; this.MaxValue = max;
            }

            var bitmapVI = BitmapSource.Create(
                width, height, 96d, 96d, imageNIR.Format, null, pixels, stride);

            bitmapVI.Freeze();
            return BitmapTransform.BitmapProcess.ConvertBitmapSourceToBitmapImage(bitmapVI);
        }
        public BitmapImage getVI(BitmapImage imageNIR,Segmentation segmentation)
        {
             var height = imageNIR.PixelHeight;
            var width = imageNIR.PixelWidth;
            var stride = 4 * width;
            var pixels = new byte[height * stride];
            imageNIR.CopyPixels(pixels, stride, 0);

            double[][] pixelsTwo = new double[pixels.Length / 4][];
            int valueCurrent = 0;

            restarMinMax();
            checkColorRamp();
            for (int i = 0; i < pixels.Length; )
            {
                var red = (double)pixels[i + 1] == 0 ? 1 : (double)pixels[i + 1];
                var vi = (double)pixels[i + 2] / red;
                checkMinMax(vi);
                pixelsTwo[valueCurrent] = new double[] { vi};
                i += 4;
                valueCurrent = i / 4;
            }


            valueCurrent = 0;
            if (this.RampAdjustment && (this.MaxValue != 1 || this.MinValue != -1))
            {
                double min, max;
                min = 255; max = 0;
                for (int i = 0; i < pixels.Length; )
                {
                    var red = (double)pixels[i + 1] == 0 ? 1 : (double)pixels[i + 1];
                    var vi = (double)pixels[i + 2] / red;
                    vi = scaleAdjustment(vi, this.MinValue, this.MaxValue, 0,255);
                    checkMinMaxAux(vi, ref min, ref max);
                    pixelsTwo[valueCurrent] = new double[] { vi };
                    i += 4;
                    valueCurrent = i / 4;
                }
                this.MinValue = min; this.MaxValue = max;
            }



            var newPixels = getSegmentation(pixelsTwo,segmentation);
            valueCurrent = 0;
            for (int i = 0; i < pixels.Length; )
            {
                var color = ColorRamp(newPixels[valueCurrent][0]);
                pixels[i++] = color[0];
                pixels[i++] = color[1];
                pixels[i++] = color[2];
                pixels[i++] = color[3];
                valueCurrent = i / 4;
            }
            
            var bitmapVI = BitmapSource.Create(
                width, height, 96d, 96d, imageNIR.Format, null, pixels, stride);

            bitmapVI.Freeze();
            return BitmapTransform.BitmapProcess.ConvertBitmapSourceToBitmapImage(bitmapVI);
        } 




        public byte[] getColor(double ratio)
        {
            byte[] colorReturn = new byte[4];

            if (ratio < 1)
            {
                colorReturn[0] = 0;
                colorReturn[1] = 0;
                colorReturn[2] = 255;
                colorReturn[3] = 255;
            }
            else if (ratio >= 1 && ratio < 1.2)
            {
                colorReturn[0] = 0;
                colorReturn[1] = Convert.ToByte(128 + (byte)((1.2 - ratio) * 105 / 1.2));
                colorReturn[2] = 255;
                colorReturn[3] = 255;
            }
            else if (ratio >= 1.2 && ratio < 1.4)
            {
                colorReturn[0] = 0;
                colorReturn[1] = Convert.ToByte(233 + (byte)((1.4 - ratio) * 22 / 1.2)); ;
                colorReturn[2] = Convert.ToByte((byte)((1.4 - ratio) * 255 / 1.2));
                colorReturn[3] = 255;
            }
            else if (ratio >= 1.4 && ratio<5)
            {
                colorReturn[0] = 0;
                colorReturn[1] = Convert.ToByte(54 + (byte)((5 - ratio) * 200 / 3.6));
                colorReturn[2] = 0;
                colorReturn[3] = 255;
            }
            else
            {
                colorReturn[0] = 100;
                colorReturn[1] = 35;
                colorReturn[2] =74;
                colorReturn[3] = 255;
            }
           
            return colorReturn;

        }
        public double[][] getSegmentation(double[][] pixelsTwo,Segmentation segmentation)
        {
            if (segmentation.TypeSegmentation==Segmentation.Segmentations.Kmeans)
            {
                KMeans kmeans = new KMeans(segmentation.Clusters, getDistance(segmentation.TypeDistance));
                kmeans.Tolerance = segmentation.Tolerance;
                int[] idx = kmeans.Compute(pixelsTwo);
                pixelsTwo.ApplyInPlace((x, i) => kmeans.Clusters.Centroids[idx[i]]);
                int index = 0;
                Results = new List<ItemProportions>(segmentation.Clusters);
                foreach (var item in kmeans.Clusters)
                {
                    ItemProportions ik = new ItemProportions();
                    var color = ColorRamp(item.Mean[0]).ToList().ConvertAll(x => (double)x);
                    ik.Colors = color;
                    ik.Proportion = item.Proportion;
                    ik.index = index;
                    Results.Add(ik);
                    index++;
                }
                Results = Results.OrderByDescending(C => C.Proportion).ToList();

            }
            if (segmentation.TypeSegmentation == Segmentation.Segmentations.Kmodes)
            {
                 KModes kmodes = new KModes(segmentation.Clusters);
                 var pixelsTwoInt = convertToInt(pixelsTwo);
                 int[] idx = kmodes.Compute(pixelsTwoInt, segmentation.Tolerance);
                 pixelsTwoInt.ApplyInPlace((x, i) => kmodes.Clusters.Centroids[idx[i]]);
                 pixelsTwo = convertToDouble(pixelsTwoInt);

                 int index = 0;
                 Results = new List<ItemProportions>(segmentation.Clusters);
                 foreach (var item in kmodes.Clusters)
                 {
                     ItemProportions ik = new ItemProportions();
                     var color = ColorRamp((double)item.Mode[0] / 100).ToList().ConvertAll(x => (double)x);
                     ik.Colors = color;
                     ik.Proportion = item.Proportion;
                     ik.index = index;
                     Results.Add(ik);
                     index++;
                 }
                 Results = Results.OrderByDescending(C => C.Proportion).ToList();
            }
            return pixelsTwo;

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
        private int[][] convertToInt(double[][] value)
        {
            int[][] valueReturn = new int[value.Length][];
            for (int i = 0; i < value.Length; i++)
            {
                valueReturn[i] = new int[] { (int)(value[i][0]*100) };
            }

            return valueReturn;
        }
        private double[][] convertToDouble(int[][] value)
        {
            double[][] valueReturn = new double[value.Length][];
            for (int i = 0; i < value.Length; i++)
            {
                valueReturn[i] = new double[] { (double)((double)value[i][0]/100) };
            }

            return valueReturn;
        }
        private void checkMinMax(double actualValue)
        {
            if (actualValue > this.MaxValue) { this.MaxValue = actualValue; }
            if (actualValue < this.MinValue) { this.MinValue = actualValue; }
        }
        private void checkMinMaxAux(double actualValue, ref double min, ref double max)
        {
            if (actualValue > max) { max = actualValue; }
            if (actualValue < min) { min = actualValue; }
        }
        private void checkColorRamp()
        {
            if (this.ColorRamp == null) { this.ColorRamp = getColor; }
        }
        private void restarMinMax()
        {
            this.MinValue = 255;
            this.MaxValue = 0;
        }
        private double scaleAdjustment(double valueIn, double baseMin, double baseMax, double limitMin, double limitMax)
        {
            return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
        }

       
    }
}
