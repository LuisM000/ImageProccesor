using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageProcessor.BitmapTransform
{
    public class BitmapProcess
    {
        private static BitmapImage  bmImage;
        public static BitmapImage ConvertWriteableBitmapToBitmapImage(WriteableBitmap wbm)
        {
            bmImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                TiffBitmapEncoder encoder = new TiffBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(wbm));
                encoder.Save(stream);
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = stream;
                bmImage.EndInit();
                bmImage.Freeze();
            }
            wbm = null;
            return bmImage;
        }

        public static BitmapImage ConvertBitmapSourceToBitmapImage(BitmapSource bms)
        {
            bmImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                TiffBitmapEncoder encoder = new TiffBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bms));
                encoder.Save(stream);
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = stream;
                bmImage.EndInit();
                bmImage.Freeze();
            }
            bms = null;
            return bmImage;


        }
    }
}
