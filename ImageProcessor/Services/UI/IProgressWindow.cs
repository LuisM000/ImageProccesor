using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Services.UI
{
    public interface IProgressWindow
    {
        void showProgress(string text,string title="Procesando imagen...");
        void closeProgress();
    }
}
