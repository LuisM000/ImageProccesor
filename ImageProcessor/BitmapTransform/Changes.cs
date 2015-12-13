using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageProcessor.BitmapTransform
{
    public class Changes
    {
        public static BitmapImage croppedToBitmap(CroppedBitmap cp)
        {
            MemoryStream mStream = new MemoryStream();
            TiffBitmapEncoder jEncoder = new TiffBitmapEncoder();

            jEncoder.Frames.Add(BitmapFrame.Create(cp));  //the croppedBitmap is a CroppedBitmap object 
            jEncoder.Save(mStream);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = mStream;
            image.EndInit();

            return image;
        }

    }
}
