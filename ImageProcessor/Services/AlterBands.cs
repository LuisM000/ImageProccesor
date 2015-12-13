using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public class AlterBands : IAlterBands
    {
         

        public BitmapImage getImage(BitmapImage bitmap, int[] order)
        {
            var height = bitmap.PixelHeight;
            var width = bitmap.PixelWidth;
            var stride = 4 * width;
            var pixels = new byte[height * stride];
            var pixelsAlter = new byte[height * stride];
            bitmap.CopyPixels(pixels, stride, 0);

            for (int i = 0; i < pixels.Length;i+=4)
            {
                //var color = ColorRamp(vi);
                pixelsAlter[i] = pixels[i+order[0]];
                pixelsAlter[i+1] = pixels[i + order[1]];
                pixelsAlter[i+2] = pixels[i + order[2]];
                pixelsAlter[i+3] = pixels[i + order[3]]; 
            }

           

            var bitmapAlter = BitmapSource.Create(
                width, height, 96d, 96d, bitmap.Format, null, pixelsAlter, stride);

            bitmapAlter.Freeze();
            return BitmapTransform.BitmapProcess.ConvertBitmapSourceToBitmapImage(bitmapAlter);
        }
    }
}
