﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Services
{
    public interface INotepad
    {
        bool openFile(string fileName);
    }
}
