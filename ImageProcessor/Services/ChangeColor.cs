using ImageProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public class ChangeColor:IChangeColor
    {
        public List<ItemProportions> Results { get; set; }

        public BitmapImage changeColor(BitmapImage bitmap,Color originalColor, Color returnColor,
            List<ItemProportions> results)
        {
            var height = bitmap.PixelHeight;
            var width = bitmap.PixelWidth;
            var stride = 4 * width;
            var pixels = new byte[height * stride];
             bitmap.CopyPixels(pixels, stride, 0);
 
            for (int i = 0; i < pixels.Length; )
            {
                if (pixels[i] == originalColor.B && pixels[i + 1] == originalColor.G &&
                        pixels[i + 2] == originalColor.R)
                {
                    pixels[i++] = returnColor.B;
                    pixels[i++] = returnColor.G;
                    pixels[i++] = returnColor.R;
                    i++;
                }
                else
                {
                    i += 4;
                }
            }

            for (int i = 0; i < results.Count; i++)
			{
			 if((int)results[i].Colors[2]==originalColor.R && 
                 (int)results[i].Colors[1]==originalColor.G && (int)results[i].Colors[0]==originalColor.B)
             {
                 results[i].Colors = new List<double>() { returnColor.B, returnColor.G, returnColor.R };
             }
			}
            Results = results;
 
            var bitmapVI = BitmapSource.Create(
                width, height, 96d, 96d, bitmap.Format, null, pixels, stride);

            bitmapVI.Freeze();
            return BitmapTransform.BitmapProcess.ConvertBitmapSourceToBitmapImage(bitmapVI);
        }
    }
}
