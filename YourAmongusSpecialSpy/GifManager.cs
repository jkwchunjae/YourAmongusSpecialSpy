using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace YourAmongusSpecialSpy
{
    public class GifManager
    {
        List<RecordData> _imageData;
        int _beginIndex;

        public void Init(List<RecordData> imageData, int beginIndex)
        {
            _imageData = imageData;
            _beginIndex = beginIndex;
        }

        public void Complete(int endIndex)
        {
            CreateGif(_imageData, _beginIndex, endIndex);
        }

        private void CreateGif(List<RecordData> imageData, int beginIndex, int endIndex)
        {
            var gEnc = new GifBitmapEncoder();

            imageData
                .Select((x, i) => new { Data = x, Index = i })
                .Where(x => x.Index >= beginIndex && x.Index <= endIndex)
                .ToList()
                .ForEach(x =>
                {
                    var bmp = x.Data.Image.GetHbitmap();
                    var src = Imaging.CreateBitmapSourceFromHBitmap(
                        bmp,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                    gEnc.Frames.Add(BitmapFrame.Create(src));
                    // DeleteObject(bmp);
                });

            var dir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads\\amongus-captures"));
            var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".gif";
            if (!dir.Exists)
                Directory.CreateDirectory(dir.FullName);

            using (FileStream fs = new FileStream(Path.Combine(dir.FullName, fileName), FileMode.Create))
            {
                gEnc.Save(fs);
            }
        }
    }
}
