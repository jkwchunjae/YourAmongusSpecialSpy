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
            => Process.GetProcessesByName("Among Us")
                .FirstOrDefault(x => x.MainWindowTitle == "Among Us");

        private static RecordData _lastImage = new RecordData
        {
            Image = null,
            Time = DateTime.MinValue,
        };

        public static Bitmap GetImage(bool force = true)
        {
            try
            {
                if (force || DateTime.Now.Subtract(_lastImage.Time) > TimeSpan.FromMilliseconds(300))
                {
                    var image = GetImageImpl();
                    return image;
                }
                return _lastImage.Image;
            }
            catch
            {
                return GetImageImpl();
            }
        }

        private static Bitmap GetImageImpl()
        {
            var image = GetProcess()?.CaptureWindow();
            _lastImage = new RecordData
            {
                Image = image,
                Time = DateTime.Now,
            };
            return image;
        }
    }
}
