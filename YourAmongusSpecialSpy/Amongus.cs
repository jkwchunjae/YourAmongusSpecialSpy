using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy
{
    public static class Amongus
    {
        private static Process GetProcess()
            => Process.GetProcessesByName("Among Us").FirstOrDefault();

        public static Bitmap GetImage()
            => GetProcess()?.CaptureWindow();

    }
}
