using ImageProcessor.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class ProcessWindow : Window
    {
        public ProcessWindow(BitmapImage bmpOriginal,BitmapImage bmpIRC)
        {
            InitializeComponent();
            
            this.imgOriginal.Source = bmpOriginal;
            this.imgIRC.Source = bmpIRC;
          }

        private void WebBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string script = "document.body.style.overflow ='hidden'";
            WebBrowser wb = (WebBrowser)sender;
            wb.InvokeScript("execScript", new Object[] { script, "JavaScript" });
        }
 

        


    }
}
