using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace ImageProcessor.Services
{
    public class Colordialog:IColordialog
    {

        Color? IColordialog.showDialog(Color initialColor)
        {
            ColorDialog cd = new ColorDialog();
            cd.SolidColorOnly = true;
            cd.Color = System.Drawing.Color.FromArgb(initialColor.R, initialColor.G, initialColor.B);
            if (cd.ShowDialog() == DialogResult.OK)
            {
                return Color.FromRgb(cd.Color.R, cd.Color.G, cd.Color.B);
            }
            else
            {
                return null;
            }
        }
    }
}
