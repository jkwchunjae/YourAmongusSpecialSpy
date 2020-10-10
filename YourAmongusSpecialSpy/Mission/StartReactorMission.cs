using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class StartReactorMission : IMission
    {
        public string GetName()
        {
            return "원자로 가동하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var mapping = new List<(Point Point, Color[] Color)>
            {
                (new Point(350, 260), new [] { Color.FromArgb(0, 192, 0), Color.FromArgb(106, 106, 106) }),
                (new Point(400, 260), new [] { Color.FromArgb(104, 103, 104) }),
                (new Point(470, 260), new [] { Color.FromArgb(105, 105, 105) }),
                (new Point(530, 260), new [] { Color.FromArgb(106, 106, 106) }),
            };

            return mapping.All(x => x.Color.Contains(image.GetPixel(x.Point.X, x.Point.Y)));
        }

        public void StartMission(Bitmap image)
        {
            MouseController.Click(230, 200, 100);
            MouseController.Click(1300, 700, 100);
            Thread.Sleep(1000);
            for (var buttonCount = 1; buttonCount <= 5; buttonCount++)
            {
                var images = GetButtonImages(buttonCount);

                foreach (var position in images.Select(x => FindPosition(x)))
                {
                    var point = _positions[position];
                    MouseController.Click(point.X + _keypadDiff, point.Y, 100);
                }
                Thread.Sleep(1300);
            }
        }

        private readonly List<Point> _positions = new List<Point>
        {
            new Point(390, 377),
            new Point(470, 377),
            new Point(550, 377),
            new Point(390, 460),
            new Point(470, 460),
            new Point(550, 460),
            new Point(390, 540),
            new Point(470, 540),
            new Point(550, 540),
        };

        private readonly int _keypadDiff = 420;

        private List<Bitmap> GetButtonImages(int count)
        {
            var result = new List<Bitmap>();
            for (var i = 0; i < count; i++)
            {
                result.Add(Amongus.GetImage());
                Thread.Sleep(320);
            }

            return result;
        }

        private int FindPosition(Bitmap image)
        {
            return _positions.Select((x, i) => new { Index = i, Color = image.GetPixel(x.X, x.Y) })
                .First(x => x.Color.B > 200)
                .Index;
        }
    }
}
