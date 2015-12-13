using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public class NavigationWindow:INavigationWindow
    {
        public void NavigateTo(Window actualWindow, Window goToWindow)
        {
            goToWindow.Show();
            actualWindow.Close();
        }

     
        void INavigationWindow.NavigateTo(Window actualWindow, Window goToWindow, BitmapImage imageOriginal,BitmapImage imageNIR)
        {
            goToWindow = new MainWindow(imageOriginal, imageNIR);
            goToWindow.Show();
            actualWindow.Close();
        }
    }
}
