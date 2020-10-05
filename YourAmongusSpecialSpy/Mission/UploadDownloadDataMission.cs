using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class UploadDownloadDataMission : IMission
    {
        public string GetName()
        {
            return "업로드 & 다운로드 데이터";
        }

        public bool IsMyMission(Bitmap image)
        {
            var mapping = new List<(Point Point, Color Color)>
            {
                (new Point(380, 320), Color.FromArgb(241, 212, 161)),
                (new Point(500, 320), Color.FromArgb(241, 212, 161)),
                (new Point(380, 405), Color.FromArgb(241, 212, 161)),
                (new Point(530, 405), Color.FromArgb(241, 212, 161)),
            };

            return mapping.All(x => image.GetPixel(x.Point.X, x.Point.Y) == x.Color);
        }

        public void StartMission(Bitmap image)
        {
            Thread.Sleep(100);
            MouseController.Move(700, 500);
            Thread.Sleep(50);
            MouseController.Click(700, 500);
            Thread.Sleep(10000);
        }
    }
}
