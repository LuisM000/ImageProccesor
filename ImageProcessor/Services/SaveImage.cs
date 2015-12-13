using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public class SaveImage:ISaveImage
    {
        public void saveImage(BitmapImage imageSave, string name = "")
        {
            SaveFileDialog sfd = new SaveFileDialog();
             sfd.Title = "Guardar imagen";
            sfd.DefaultExt = "*.tif";
            sfd.FileName = name;
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            sfd.Filter = string.Format("{0}",
                string.Join("|", codecs.Select(codec =>
                string.Format("{0} ({1})|{1}", codec.CodecName, codec.FilenameExtension)).ToArray()),
                string.Join(";", codecs.Select(codec => codec.FilenameExtension).ToArray()));
            Nullable<bool> result = sfd.ShowDialog();
            if (result == true)
            {
                var encoder = new TiffBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageSave));
                using (var stream = sfd.OpenFile())
                {
                    encoder.Save(stream);
                }
            }
        }
    }
}
