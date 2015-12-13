using Accord.MachineLearning;
using Accord.Math;
using ImageProcessor.BitmapTransform;
using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public class NDVI : INDVI
    {
        public List<ItemProportions> Results { get; set; }
        public Func<double, byte[]> ColorRamp { get; set; }
        public bool RampAdjustment { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public BitmapImage getNDVI(BitmapImage imageOriginal, BitmapImage imageNIR)
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
                var value = (pixelsNIR[i + 2] + pixelsOrig[i + 2]) == 0 ? 1 : (pixelsNIR[i + 2] + pixelsOrig[i + 2]);
                var ndvi = ((double)pixelsNIR[i + 2] - (double)pixelsOrig[i + 2]) /
                               (double)value;
                checkMinMax(ndvi);
                var color = ColorRamp(ndvi);
                pixels[i++] = color[0];
                pixels[i++] = color[1];
                pixels[i++] = color[2];
                pixels[i++] = color[3];
            }

             if (this.RampAdjustment && (this.MaxValue != 1 || this.MinValue != -1))
            {
                double min, max;
                min = 1; max = -1;
                for (int i = 0; i < pixelsOrig.Length; )
                {
                    var value = (pixelsNIR[i + 2] + pixelsOrig[i + 2]) == 0 ? 1 : (pixelsNIR[i + 2] + pixelsOrig[i + 2]);
                    var ndvi = ((double)pixelsNIR[i + 2] - (double)pixelsOrig[i + 2]) /
                                   (double)value;
                    ndvi = scaleAdjustment(ndvi, this.MinValue, this.MaxValue, -1, 1);
                    var color = ColorRamp(ndvi);
                    checkMinMaxAux(ndvi, ref min, ref max);
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
        public BitmapImage getNDVI(BitmapImage imageOriginal, BitmapImage imageNIR, Segmentation segmentation)
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
                var value = (pixelsNIR[i + 2] + pixels[i + 2]) == 0 ? 1 : (pixelsNIR[i + 2] + pixels[i + 2]);
                var ndvi = ((double)pixelsNIR[i + 2] - (double)pixels[i + 2]) /
                               (double)value;
                checkMinMax(ndvi);
                pixelsTwo[valueCurrent] = new double[] { ndvi };
                i += 4;
                valueCurrent = i / 4;
            }

            valueCurrent = 0;
 
            if (this.RampAdjustment && (this.MaxValue != 1 || this.MinValue != -1))
            {
                double min, max;
                min = 1; max = -1;
                for (int i = 0; i < pixels.Length; )
                {
                    var value = (pixelsNIR[i + 2] + pixels[i + 2]) == 0 ? 1 : (pixelsNIR[i + 2] + pixels[i + 2]);
                    var ndvi = ((double)pixelsNIR[i + 2] - (double)pixels[i + 2]) /
                                   (double)value;
                    ndvi = scaleAdjustment(ndvi, this.MinValue, this.MaxValue, -1, 1);
                    checkMinMaxAux(ndvi, ref min, ref max);
                    pixelsTwo[valueCurrent] = new double[] { ndvi };
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
        public BitmapImage getNDVI(BitmapImage imageNIR)
        {
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
                var value = (pixelsNIR[i + 2] + pixelsNIR[i + 1]) == 0 ? 1 : (pixelsNIR[i + 2] + pixelsNIR[i + 1]);
                var value1 = (pixelsNIR[i + 2] - pixelsNIR[i + 1]);
                var ndvi = (double)value1 / (double)value;
                checkMinMax(ndvi);
                var color = ColorRamp(ndvi);
                pixels[i++] = color[0];
                pixels[i++] = color[1];
                pixels[i++] = color[2];
                pixels[i++] = color[3];
            }
 
            if (this.RampAdjustment && (this.MaxValue != 1 || this.MinValue != -1))
            {
                double min, max;
                min = 1; max = -1;
                for (int i = 0; i < pixels.Length; )
                {
                    var value = (pixelsNIR[i + 2] + pixelsNIR[i + 1]) == 0 ? 1 : (pixelsNIR[i + 2] + pixelsNIR[i + 1]);
                    var value1 = (pixelsNIR[i + 2] - pixelsNIR[i + 1]);
                    var ndvi = (double)value1 / (double)value;
                    ndvi = scaleAdjustment(ndvi, this.MinValue, this.MaxValue, -1, 1);
                    checkMinMaxAux(ndvi, ref min, ref max);
                    var color = ColorRamp(ndvi);
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
        public BitmapImage getNDVI(BitmapImage imageNIR, Segmentation segmentation)
        {
            var height = imageNIR.PixelHeight;
            var width = imageNIR.PixelWidth;
            var stride = 4 * width;
            var pixels = new byte[height * stride];
            var pixelsNIR = new byte[height * stride];
            imageNIR.CopyPixels(pixelsNIR, stride, 0);

            double[][] pixelsTwo = new double[pixels.Length / 4][];
            int valueCurrent = 0;

            restarMinMax();
            checkColorRamp();
            for (int i = 0; i < pixels.Length; )
            {
                var value = (pixelsNIR[i + 2] + pixelsNIR[i + 1]) == 0 ? 1 : (pixelsNIR[i + 2] + pixelsNIR[i + 1]);
                var value1 = (pixelsNIR[i + 2] - pixelsNIR[i + 1]);
                var ndvi = (double)value1 /
                               (double)value;
                checkMinMax(ndvi);
                pixelsTwo[valueCurrent] = new double[] { ndvi };
                i += 4;
                valueCurrent = i / 4;
            }

            valueCurrent = 0;
            if (this.RampAdjustment && (this.MaxValue != 1 || this.MinValue != -1))
            {
                double min, max;
                min = 1; max = -1;
                for (int i = 0; i < pixels.Length; )
                {
                    var value = (pixelsNIR[i + 2] + pixelsNIR[i + 1]) == 0 ? 1 : (pixelsNIR[i + 2] + pixelsNIR[i + 1]);
                    var value1 = (pixelsNIR[i + 2] - pixelsNIR[i + 1]);
                    var ndvi = (double)value1 /
                                   (double)value;
                    ndvi = scaleAdjustment(ndvi, this.MinValue, this.MaxValue, -1, 1);
                    checkMinMaxAux(ndvi, ref min,ref max);
                    pixelsTwo[valueCurrent] = new double[] { ndvi };
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

        private byte[] getColor(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = Math.Abs((-1 - ratio)) / 2;
            colorReturn[3] = 255;
            Color a, b;

            if (ratio <= 0.45)
            {
                a = Color.FromArgb(255, 255, 0, 0);
                b = Color.FromArgb(255, 255, 0, 0);
                ratio = scaleAdjustment(ratio, 0, 0.45, 0, 1);
            }
            else if (ratio > 0.45 && ratio <= 0.55)
            {
                a = Color.FromArgb(255, 255, 0, 0);
                b = Color.FromArgb(255, 230, 95, 0);
                ratio = scaleAdjustment(ratio, 0.45, 0.55, 0, 1);
            }
            else if (ratio > 0.55 && ratio <= 0.8)
            {
                a = Color.FromArgb(255, 0, 255, 0);
                b = Color.FromArgb(255, 0, 120, 0);
                ratio = scaleAdjustment(ratio, 0.55, 0.8, 0, 1);
            }
            else
            {
                a = Color.FromArgb(255, 75, 35, 100);
                b = Color.FromArgb(255, 75, 35, 100);
            }


            colorReturn[2] = (byte)((double)a.R + ((double)b.R - (double)a.R) * ratio);
            colorReturn[1] = (byte)((double)a.G + ((double)b.G - (double)a.G) * ratio);
            colorReturn[0] = (byte)((double)a.B + ((double)b.B - (double)a.B) * ratio);
            return colorReturn;
        }
        public double[][] getSegmentation(double[][] pixelsTwo, Segmentation segmentation)
        {
            if (segmentation.TypeSegmentation == Segmentation.Segmentations.Kmeans)
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
                valueReturn[i] = new int[] { (int)(value[i][0] * 100) };
            }

            return valueReturn;
        }
        private double[][] convertToDouble(int[][] value)
        {
            double[][] valueReturn = new double[value.Length][];
            for (int i = 0; i < value.Length; i++)
            {
                valueReturn[i] = new double[] { (double)((double)value[i][0] / 100) };
            }

            return valueReturn;
        }
        private void checkMinMax(double actualValue)
        {
            if (actualValue > this.MaxValue) { this.MaxValue = actualValue; }
            if (actualValue < this.MinValue) { this.MinValue = actualValue; }
        }
        private void checkMinMaxAux(double actualValue, ref double min,ref double max)
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
            this.MinValue = 1;
            this.MaxValue = -1;
        }
        private double scaleAdjustment(double valueIn, double baseMin, double baseMax, double limitMin, double limitMax)
        {
            return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
        }
    }
}
