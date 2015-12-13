using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageProcessor.Services
{
    public class MouseActions:IMouseActions
    {
        public void mouseEnter()
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        public void mouseLeave()
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
