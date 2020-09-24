using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using AnimatedGif;

namespace YourAmongusSpecialSpy
{
    public static class GifManager
    {
        public static void CreateGif(List<RecordData> imageData, int delay)
        {
            var dir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads\\amongus-captures"));
            var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".gif";
            if (!dir.Exists)
                Directory.CreateDirectory(dir.FullName);

            using (var gif = AnimatedGif.AnimatedGif.Create(Path.Combine(dir.FullName, fileName), delay, 0))
            {
                imageData
                    .ForEach(x =>
                    {
                        gif.AddFrame(x.Image);
                    });
            }
        }
    }
}
