using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageProcessor
{
    public partial class ImageWindow : Window
    {
        private Point start;
        private Point origin;
        private Point current;


        public ImageWindow()
        {
            InitializeComponent();
             
         }
        public ImageWindow(BitmapImage image)
        {
            InitializeComponent();
            this.img.Source = image;
          }


        #region zoomAndMove
        private void sliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var tt = (TranslateTransform)((TransformGroup)this.img.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
            tt.X = current.X * this.sliderZoom.Value;
            tt.Y = current.Y * this.sliderZoom.Value;
        }
        private void img_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                this.sliderZoom.Value += 1;
            else
                this.sliderZoom.Value -= 1;
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            img.CaptureMouse();
            Mouse.OverrideCursor = Cursors.Hand;
            var tt = (TranslateTransform)((TransformGroup)this.img.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
            start = e.GetPosition(this.gridMain);
            origin = new Point(tt.X, tt.Y);
        }
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.img.IsMouseCaptured)
            {
                var tt = (TranslateTransform)((TransformGroup)img.RenderTransform)
                    .Children.First(tr => tr is TranslateTransform);
                Vector v = start - e.GetPosition(gridMain);
                tt.X = origin.X - v.X;
                tt.Y = origin.Y - v.Y;
            }
        }
        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.img.IsMouseCaptured)
            {
                this.img.ReleaseMouseCapture();
                Mouse.OverrideCursor = Cursors.Arrow;
                Point tt = new Point();
                Vector v = start - e.GetPosition(this.gridMain);
                tt.X = origin.X - v.X;
                tt.Y = origin.Y - v.Y;
                current = new Point(tt.X / this.sliderZoom.Value, tt.Y / this.sliderZoom.Value);
            }
        }
        #endregion

       
       
    }
}
