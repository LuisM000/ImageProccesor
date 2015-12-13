using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageProcessor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      

        public MainWindow()
        {
            InitializeComponent();
        }
         
        public MainWindow(BitmapImage bmpOriginal, BitmapImage bmpIRC)
        {
            InitializeComponent();
            if (bmpOriginal!=null && bmpIRC!=null)
            {
                this.img.Source = bmpOriginal;
                sizeImageOriginal.Width = bmpOriginal.Width; sizeImageOriginal.Height = bmpOriginal.Height;
                this.imgIR.Source = null; this.brdIR.Visibility = Visibility.Collapsed; this.btnNext.IsEnabled = true;
                this.img.Source = bmpOriginal;
                adjustImage();
                toogleCut.IsEnabled = true;
                btnIR.IsEnabled = true;

                this.imgAux.Source = bmpIRC;
                if (bmpIRC.Width == sizeImageOriginal.Width && bmpIRC.Height == sizeImageOriginal.Height)
                {
                    this.imgIR.Source = bmpIRC;
                    this.brdIR.Visibility = Visibility.Visible;
                }
            }
            else
            {
                this.img.Source = bmpIRC;
                sizeImageOriginal.Width = bmpIRC.Width; sizeImageOriginal.Height = bmpIRC.Height;
                this.imgIR.Source = null; this.brdIR.Visibility = Visibility.Collapsed; this.btnNext.IsEnabled = true;
                this.img.Source = bmpIRC;
                adjustImage();
                toogleCut.IsEnabled = true;
                btnIR.IsEnabled = true;
            }
           
        }


        private Point start;
        private Point origin;
        private Point current;
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


        private bool _isDragging = false;
        private Point _anchorPoint = new Point();
        private Point _rectangleStart = new Point();
        #region createRectangle
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _anchorPoint = e.GetPosition(this.gridMain);
            _rectangleStart = e.GetPosition(this.gridMain);
            _isDragging = true;
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                double x = e.GetPosition(gridMain).X;
                double y = e.GetPosition(gridMain).Y;

                this.rectangleCut.SetValue(Canvas.LeftProperty, Math.Min(x, _anchorPoint.X));
                this.rectangleCut.SetValue(Canvas.TopProperty, Math.Min(y, _anchorPoint.Y));

                this.rectangleCut.Width = Math.Abs(x - _anchorPoint.X);
                this.rectangleCut.Height = Math.Abs(y - _anchorPoint.Y);

                if (!this.rectangleCut.IsVisible)
                    this.rectangleCut.Visibility = Visibility.Visible;
            }
        }
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            _rectangleStart.X = (_rectangleStart.X > e.GetPosition(this.gridMain).X) ?
                e.GetPosition(this.gridMain).X : _rectangleStart.X;
            _rectangleStart.Y = (_rectangleStart.Y > e.GetPosition(this.gridMain).Y) ?
                e.GetPosition(this.gridMain).Y : _rectangleStart.Y;
            messageCut.Visibility = Visibility.Visible;
        }
        
        #endregion

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            adjustImage();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            messageCut.Visibility = Visibility.Collapsed;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            cutImage();
            messageCut.Visibility = Visibility.Collapsed;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            openImage();
        }
        private void btnIR_Click(object sender, RoutedEventArgs e)
        {
            openImageIR();
        }
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            sizeImageIR();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            nextWindow();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            changeImages();
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.gridNext.Visibility = Visibility.Collapsed;
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ProcessWindow win2 = new ProcessWindow(this.imgAux.Source.GetType().Name == "CroppedBitmap" ? 
                BitmapTransform.Changes.croppedToBitmap((CroppedBitmap)imgAux.Source) : (BitmapImage)imgAux.Source,
                this.imgIRAux.Source.GetType().Name == "CroppedBitmap" ? 
                BitmapTransform.Changes.croppedToBitmap((CroppedBitmap)imgIRAux.Source) : (BitmapImage)imgIRAux.Source);
            win2.Show();
            this.Close();
        }

        private void cutImage()
        {
            var width = (this.img.ActualWidth / this.sliderZoom.Value);
            var height = (this.img.ActualHeight / this.sliderZoom.Value);
            var ratio = this.gridMain.ActualWidth / this.gridMain.ActualHeight;
            if (gridMain.ActualWidth >= gridMain.ActualHeight)
            {
                width = height * ratio;
            }
            else
            {
                height = width / ratio;
            }

            var strideX = ((img.ActualWidth - width) / 2);
            var strideY = ((img.ActualHeight - height) / 2);

            var tt = (TranslateTransform)((TransformGroup)img.RenderTransform)
                  .Children.First(tr => tr is TranslateTransform);

            Rect myRectangle = new Rect(new Point(strideX + _rectangleStart.X / sliderZoom.Value - current.X, strideY + _rectangleStart.Y / sliderZoom.Value - current.Y),
                new Size((width * this.rectangleCut.Width) / gridMain.ActualWidth, (height * this.rectangleCut.Height) / gridMain.ActualHeight));

            var realWidthRatio = img.Source.Width / img.ActualWidth;
            var realHeightRatio = img.Source.Height / img.ActualHeight;

            ImageSource ims = img.Source;
            BitmapImage bitmapImage;

            var ss = ims.GetType();
            if (ims.GetType().Name == "CroppedBitmap")
            {
                bitmapImage = BitmapTransform.Changes.croppedToBitmap((CroppedBitmap)ims);
            }
            else
            {
                bitmapImage = (BitmapImage)ims;
            }

            try
            {
                Int32Rect newRectangle = new Int32Rect((int)(myRectangle.X * realWidthRatio), (int)(myRectangle.Y * realHeightRatio),
            (int)(myRectangle.Width * realWidthRatio), (int)(myRectangle.Height * realHeightRatio));

                CroppedBitmap cb = new CroppedBitmap(bitmapImage, newRectangle);
                sizeImageOriginal.Width = newRectangle.Width; sizeImageOriginal.Height = newRectangle.Height;
                this.img.Source = cb;
                this.sliderZoom.Value = 1;
                if (this.imgIR.Source!=null)
                {
                    cutIRImage(newRectangle);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Fuera de los índices del rectángulo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.rectangleCut.Width = 0; this.rectangleCut.Height = 0; this.toogleCut.IsChecked = false;
            adjustImage();
        }
        private void cutIRImage(Int32Rect rectangle)
        {
            ImageSource ims = imgIR.Source;
            BitmapImage bitmapImage;
            var ss = ims.GetType();
            if (ims.GetType().Name == "CroppedBitmap")
            {
                bitmapImage = BitmapTransform.Changes.croppedToBitmap((CroppedBitmap)ims);
            }
            else
            {
                bitmapImage = (BitmapImage)ims;
            }

            try
            {
                CroppedBitmap cb = new CroppedBitmap(bitmapImage, rectangle);       //select region rect
                this.imgIR.Source = cb;
             }
            catch (Exception)
            {
                MessageBox.Show("Fuera de los índices del rectángulo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void adjustImage()
        {
            var tt = (TranslateTransform)((TransformGroup)img.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
            tt.X = 0;
            tt.Y = 0;
            sliderZoom.Value = 1;
            current = new Point(tt.X / this.sliderZoom.Value, tt.Y / this.sliderZoom.Value);
        }

        private Size sizeImageOriginal;
        private void openImage()
        {
            Microsoft.Win32.OpenFileDialog openfile = new Microsoft.Win32.OpenFileDialog();
            openfile.Title = "Abrir imagen original";
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            openfile.Filter = string.Format("All image files ({1})|{1} | {0} |All files|*",
                string.Join("|", codecs.Select(codec =>
                string.Format("{0} ({1})|{1}", codec.CodecName, codec.FilenameExtension)).ToArray()),
                string.Join(";", codecs.Select(codec => codec.FilenameExtension).ToArray()));

            Nullable<bool> result = openfile.ShowDialog();
            if (result == true)
            {
                var image=new BitmapImage(new Uri(openfile.FileName, UriKind.Absolute));
                sizeImageOriginal.Width=image.Width;sizeImageOriginal.Height=image.Height;
                this.imgIR.Source = null; this.brdIR.Visibility = Visibility.Collapsed; this.btnNext.IsEnabled = true;
                this.img.Source = image; 
                adjustImage();
                toogleCut.IsEnabled = true;
                btnIR.IsEnabled = true;
            }
              
        }
        private void openImageIR()
        {
            Microsoft.Win32.OpenFileDialog openfile = new Microsoft.Win32.OpenFileDialog();
            openfile.Title = "Abrir imagen IR";
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            openfile.Filter = string.Format("All image files ({1})|{1} | {0} |All files|*",
                string.Join("|", codecs.Select(codec =>
                string.Format("{0} ({1})|{1}", codec.CodecName, codec.FilenameExtension)).ToArray()),
                string.Join(";", codecs.Select(codec => codec.FilenameExtension).ToArray()));
            Nullable<bool> result = openfile.ShowDialog();
            if (result == true)
            {
                var imageIR = new BitmapImage(new Uri(openfile.FileName, UriKind.Absolute));
                if(imageIR.Width==sizeImageOriginal.Width && imageIR.Height==sizeImageOriginal.Height)
                {
                    this.imgIR.Source = imageIR;
                    this.brdIR.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Las dimensiones de la imagen no coinciden", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void sizeImageIR()
        {
            if (this.brdIR.Width > 230)
            {
                this.brdIR.Width = 230;
                this.brdIR.Height = 180;
                this.imgIR.ToolTip = "Clic para ampliar";
            }
            else
            {
                this.brdIR.Width = this.gridMain.ActualWidth;
                this.brdIR.Height = this.gridMain.ActualHeight;
                this.imgIR.ToolTip = "Clic para encoger";

            }
        }
        
        private void nextWindow()
        {
            if(this.img.Source!=null 
                && this.imgIR.Source!=null)
            {
                this.gridNext.Visibility = Visibility.Visible;
                this.imgAux.Source = this.img.Source;
                this.imgIRAux.Source = this.imgIR.Source;
            }
            else
            {
                ProcessWindow win2 = new ProcessWindow(null,this.img.Source.GetType().Name == "CroppedBitmap" ?
                BitmapTransform.Changes.croppedToBitmap((CroppedBitmap)img.Source) : (BitmapImage)img.Source);
                win2.Show();
                this.Close();
            }
        }
        private void changeImages()
        {
            if (this.img.Source != null
               && this.imgIR.Source != null)
            {
                this.imgAux.Source = (this.imgAux.Source==this.img.Source)?this.imgIR.Source:this.img.Source;
                this.imgIRAux.Source = (this.imgIRAux.Source == this.imgIR.Source) ? this.img.Source : this.imgIR.Source;
            }
        }

        private void brdIR_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Opacity = 1;
        }
        private void brdIR_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Opacity = 0.7;
        }
        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            this.brdIR.Visibility = Visibility.Collapsed;
            this.imgIR.Source = null;
            GC.Collect();
        }

       

   

       

   
       

     
    }
}
