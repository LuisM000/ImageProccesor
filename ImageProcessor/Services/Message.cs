using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageProcessor.Services
{
    public class Message:IMessage
    {
        public void showMessage(string title, string content)
        {
            MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
          

        }
    }
}
