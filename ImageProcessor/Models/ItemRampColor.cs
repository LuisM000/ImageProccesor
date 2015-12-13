using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ImageProcessor.Models
{
    public class ItemRampColor
    {
        public Func<double, byte[]> FunctionRamp { get; set; }
        public string Name { get; set; }
        public string ImageRamp { get; set; }
    }

    public class RampsNDVI
    {
        public ObservableCollection<ItemRampColor> Ramps { get; private set; }

        public RampsNDVI()
        {
            this.Ramps = new ObservableCollection<ItemRampColor>()
            {
                new ItemRampColor(){FunctionRamp=Classic,ImageRamp="Classic.png",Name="Clásico"},
                new ItemRampColor(){FunctionRamp=BlackAndWhite,ImageRamp="BlackAndWhite.png",Name="Blanco y negro"},
                new ItemRampColor(){FunctionRamp=RedYellowGreen,ImageRamp="RedYellowGreen.png",Name="Rojo-amarillo-verde"},
                new ItemRampColor(){FunctionRamp=RedYellowGreenPurple,ImageRamp="RedYellowGreenPurple.png",Name="Rojo-amarillo-verde-morado"},
                new ItemRampColor(){FunctionRamp=Terrain,ImageRamp="Terrain.png",Name="Terreno"}
            };

       
        }

        private byte[] BlackAndWhite(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = Math.Abs((-1 - ratio)) / 2;
            colorReturn[3] = 255;

            Color a = Color.FromArgb(255, 0, 0, 0);
            Color b = Color.FromArgb(255, 255, 255, 255);

            colorReturn[2] = (byte)((double)a.R + ((double)b.R - (double)a.R) * ratio);
            colorReturn[1] = (byte)((double)a.G + ((double)b.G - (double)a.G) * ratio);
            colorReturn[0] = (byte)((double)a.B + ((double)b.B - (double)a.B) * ratio);
            return colorReturn;


        }
        private byte[] RedYellowGreenPurple(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = Math.Abs((-1 - ratio)) / 2;
            colorReturn[3] = 255;
            Color a, b;

            if (ratio <= 0.5)
            {
                a = Color.FromArgb(255, 255, 0, 0);
                b = Color.FromArgb(255, 255, 233, 0);
                ratio = scaleAdjustment(ratio, 0, 0.5, 0, 1);

            }
            else if (ratio > 0.5 && ratio <= 0.75)
            {
                a = Color.FromArgb(255, 255, 233, 0);
                b = Color.FromArgb(255, 0, 120, 0);
                ratio = scaleAdjustment(ratio, 0.5, 0.75, 0, 1);

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
        private byte[] RedYellowGreen(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = Math.Abs((-1 - ratio)) / 2;
            colorReturn[3] = 255;
            Color a, b;

            if (ratio <= 0.5)
            {
                a = Color.FromArgb(255, 255, 0, 0);
                b = Color.FromArgb(255, 255, 233, 0);
                ratio = scaleAdjustment(ratio, 0, 0.5, 0, 1);

            }
            else
            {
                a = Color.FromArgb(255, 255, 233, 0);
                b = Color.FromArgb(255, 0, 120, 0);
                ratio = scaleAdjustment(ratio, 0.5,1, 0, 1);
            }


            colorReturn[2] = (byte)((double)a.R + ((double)b.R - (double)a.R) * ratio);
            colorReturn[1] = (byte)((double)a.G + ((double)b.G - (double)a.G) * ratio);
            colorReturn[0] = (byte)((double)a.B + ((double)b.B - (double)a.B) * ratio);
            return colorReturn;


        }
        private byte[] Terrain(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = Math.Abs((-1 - ratio)) / 2;
            colorReturn[3] = 255;
            Color a, b;

            if (ratio <= 0.5)
            {
                a = Color.FromArgb(255, 166, 29, 20);
                b = Color.FromArgb(255, 253, 249, 188);
                ratio = scaleAdjustment(ratio, 0, 0.5, 0, 1);

            }
            else
            {
                a = Color.FromArgb(255, 253, 249, 188);
                b = Color.FromArgb(255, 61,145,36);
                ratio = scaleAdjustment(ratio, 0.5, 1, 0, 1);
            }


            colorReturn[2] = (byte)((double)a.R + ((double)b.R - (double)a.R) * ratio);
            colorReturn[1] = (byte)((double)a.G + ((double)b.G - (double)a.G) * ratio);
            colorReturn[0] = (byte)((double)a.B + ((double)b.B - (double)a.B) * ratio);
            return colorReturn;


        }
        private byte[] Classic(double ratio)
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
                ratio=scaleAdjustment(ratio,0.55,0.8,0,1);
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

        private double scaleAdjustment(double valueIn, double baseMin, double baseMax, double limitMin, double limitMax)
        {
            return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
        }
    }


    public class RampsVI
    {
        public ObservableCollection<ItemRampColor> Ramps { get; private set; }
        public int MaxValueScale { get; set; }

        public RampsVI()
        {
            this.MaxValueScale = 5;
            this.Ramps = new ObservableCollection<ItemRampColor>()
            {
                new ItemRampColor(){FunctionRamp=Classic,ImageRamp="Classic.png",Name="Clásico"},
                new ItemRampColor(){FunctionRamp=BlackAndWhite,ImageRamp="BlackAndWhite.png",Name="Blanco y negro"},
                new ItemRampColor(){FunctionRamp=RedYellowGreen,ImageRamp="RedYellowGreen.png",Name="Rojo-amarillo-verde"},
                new ItemRampColor(){FunctionRamp=RedYellowGreenPurple,ImageRamp="RedYellowGreenPurple.png",Name="Rojo-amarillo-verde-morado"},
                new ItemRampColor(){FunctionRamp=Terrain,ImageRamp="Terrain.png",Name="Terreno"}
            };


        }

        private byte[] BlackAndWhite(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = scaleAdjustment(ratio, 0, this.MaxValueScale, 0, 1);
            colorReturn[3] = 255;

            Color a = Color.FromArgb(255, 0, 0, 0);
            Color b = Color.FromArgb(255, 255, 255, 255);

            colorReturn[2] = (byte)((double)a.R + ((double)b.R - (double)a.R) * ratio);
            colorReturn[1] = (byte)((double)a.G + ((double)b.G - (double)a.G) * ratio);
            colorReturn[0] = (byte)((double)a.B + ((double)b.B - (double)a.B) * ratio);
            return colorReturn;


        }
        private byte[] RedYellowGreenPurple(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = scaleAdjustment(ratio, 0, this.MaxValueScale, 0, 1);
            colorReturn[3] = 255;
            Color a, b;

            if (ratio <= 0.5)
            {
                a = Color.FromArgb(255, 255, 0, 0);
                b = Color.FromArgb(255, 255, 233, 0);
                ratio = scaleAdjustment(ratio, 0, 0.5, 0, 1);

            }
            else if (ratio > 0.5 && ratio <= 0.75)
            {
                a = Color.FromArgb(255, 255, 233, 0);
                b = Color.FromArgb(255, 0, 120, 0);
                ratio = scaleAdjustment(ratio, 0.5, 0.75, 0, 1);

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
        private byte[] RedYellowGreen(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = scaleAdjustment(ratio, 0, this.MaxValueScale, 0, 1);
            colorReturn[3] = 255;
            Color a, b;

            if (ratio <= 0.5)
            {
                a = Color.FromArgb(255, 255, 0, 0);
                b = Color.FromArgb(255, 255, 233, 0);
                ratio = scaleAdjustment(ratio, 0, 0.5, 0, 1);

            }
            else
            {
                a = Color.FromArgb(255, 255, 233, 0);
                b = Color.FromArgb(255, 0, 120, 0);
                ratio = scaleAdjustment(ratio, 0.5, 1, 0, 1);
            }


            colorReturn[2] = (byte)((double)a.R + ((double)b.R - (double)a.R) * ratio);
            colorReturn[1] = (byte)((double)a.G + ((double)b.G - (double)a.G) * ratio);
            colorReturn[0] = (byte)((double)a.B + ((double)b.B - (double)a.B) * ratio);
            return colorReturn;


        }
        private byte[] Terrain(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = scaleAdjustment(ratio, 0, this.MaxValueScale, 0, 1);
            colorReturn[3] = 255;
            Color a, b;

            if (ratio <= 0.5)
            {
                a = Color.FromArgb(255, 166, 29, 20);
                b = Color.FromArgb(255, 253, 249, 188);
                ratio = scaleAdjustment(ratio, 0, 0.5, 0, 1);

            }
            else
            {
                a = Color.FromArgb(255, 253, 249, 188);
                b = Color.FromArgb(255, 61, 145, 36);
                ratio = scaleAdjustment(ratio, 0.5, 1, 0, 1);
            }


            colorReturn[2] = (byte)((double)a.R + ((double)b.R - (double)a.R) * ratio);
            colorReturn[1] = (byte)((double)a.G + ((double)b.G - (double)a.G) * ratio);
            colorReturn[0] = (byte)((double)a.B + ((double)b.B - (double)a.B) * ratio);
            return colorReturn;


        }
        private byte[] Classic(double ratio)
        {
            byte[] colorReturn = new byte[4];
            ratio = scaleAdjustment(ratio, 0, this.MaxValueScale, 0, 1);
            colorReturn[3] = 255;
            Color a, b;

            if (ratio <= 0.1)
            {
                a = Color.FromArgb(255, 255, 0, 0);
                b = Color.FromArgb(255, 255, 0, 0);
                ratio = scaleAdjustment(ratio, 0, 0.1, 0, 1);
            }
            else if (ratio > 0.1 && ratio <= 0.3)
            {
                a = Color.FromArgb(255, 255, 0, 0);
                b = Color.FromArgb(255, 230, 95, 0);
                ratio = scaleAdjustment(ratio, 0.2, 0.3, 0, 1);
            }
            else if (ratio > 0.3 && ratio <= 0.75)
            {
                a = Color.FromArgb(255, 0, 255, 0);
                b = Color.FromArgb(255, 0, 120, 0);
                ratio = scaleAdjustment(ratio, 0.3, 0.75, 0, 1);
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

        private double scaleAdjustment(double valueIn, double baseMin, double baseMax, double limitMin, double limitMax)
        {
            valueIn = (valueIn > baseMax) ? baseMax : valueIn;
            return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
        }
    }
}
