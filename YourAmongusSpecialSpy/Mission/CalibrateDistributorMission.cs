using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class CalibrateDistributorMission : IMission
    {
        public string GetName()
        {
            return "배선기 조정하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            int Radius = 71;
            Point center1 = new Point(509, 220);
            Point center2 = new Point(509, 408);
            Point center3 = new Point(509, 601);
            var step = 10;

            var circle1 = center1.Circle(Radius, step).Select(p => image.GetPixel(p.X, p.Y)).ToList();
            if (circle1.Count(x => x.R == 255) < circle1.Count * 0.8)
                return false;

            var circle2 = center2.Circle(Radius, step).Select(p => image.GetPixel(p.X, p.Y)).ToList();
            if (circle2.Count(x => x.G != 255 && x.B == 255) < circle2.Count * 0.8)
                return false;

            var circle3 = center3.Circle(Radius, step).Select(p => image.GetPixel(p.X, p.Y)).ToList();
            if (circle3.Count(x => x.G == 255 && x.B == 255) < circle3.Count * 0.8)
                return false;

            return true;
        }

        public void StartMission(Bitmap image1)
        {
            var position = new List<(Point Button, Point Status)>
            {
                (new Point(900, 250), new Point(900, 200)),
                (new Point(900, 444), new Point(900, 388)),
                (new Point(900, 630), new Point(900, 580)),
            };

            foreach (var p in position)
            {
                MouseController.Move(p.Button.X, p.Button.Y);
                while (true)
                {
                    var image = Amongus.GetImage();
                    if (image.GetPixel(p.Status.X, p.Status.Y) != Color.FromArgb(0, 0, 0))
                    {
                        MouseController.Click();
                        Thread.Sleep(100);
                        break;
                    }
                    Thread.Sleep(20);
                }
            }
        }
    }

    public static class Asdf
    {
        public static List<Point> Circle(this Point center, int radius, int step)
        {
            var result = new List<Point>();
            for (var a = 0; a < 360; a += step)
            {
                var p = center.CirclePoint(radius, a);
                result.Add(p);
            }
            return result;
        }

        public static Point CirclePoint(this Point center, int radius, int angle360)
        {
            return new Point(
                (int)(center.X + radius * Math.Cos(angle360 / 180.0 * Math.PI)),
                (int)(center.Y + radius * Math.Sin(angle360 / 180.0 * Math.PI))
            );
        }
    }
}
