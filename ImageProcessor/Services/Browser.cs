using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Services
{
    public class Browser:IBrowser
    {
        public void openUrl(string url)
        {
            url=(url.Contains("www."))?url:"www." + url;
            System.Diagnostics.Process.Start(url);
         }
    }
}
