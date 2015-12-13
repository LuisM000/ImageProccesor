using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Services.UI
{
    public class ProgressWindow:IProgressWindow
    {
        Progress pw;

        public void showProgress(string text, string title)
        {
            pw = new Progress(text, title);
            pw.ShowDialog();
        }

        public void closeProgress()
        {
            pw.Dispatcher.Invoke((Action)(() =>
            {
                pw.Close();
            }));
        }
    }
}
