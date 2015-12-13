using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageProcessor.Services
{
    public interface INavigationWindow
    {
        void NavigateTo(Window actualWindow, Window goToWindow);
        void NavigateTo(Window actualWindow, Window goToWindow, BitmapImage imageOriginal, BitmapImage imageNIR);
    }
}
