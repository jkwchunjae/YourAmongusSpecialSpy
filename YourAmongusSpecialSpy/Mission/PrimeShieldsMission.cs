using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class PrimeShieldsMission : IMission
    {
        public string GetName()
        {
            return "실드 준비하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var mapping = new List<(Point Point, List<Color> Color)>
            {
                (new Point(404, 129), new List<Color> { Color.FromArgb(101, 102, 101) }),
                (new Point(950, 150), new List<Color> { Color.FromArgb(101, 102, 101) }),
                (new Point(418, 685), new List<Color> { Color.FromArgb(101, 102, 101) }),
                (new Point(961, 685), new List<Color> { Color.FromArgb(101, 102, 101) }),
                (new Point(392, 403), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(404, 414), new List<Color> { Color.FromArgb(201, 28, 35) }),
                (new Point(430, 559), new List<Color> { Color.FromArgb(31, 73, 145) }),
                (new Point(432, 270), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(440, 554), new List<Color> { Color.FromArgb(200, 28, 34) }),
                (new Point(441, 274), new List<Color> { Color.FromArgb(201, 28, 34) }),
                (new Point(540, 673), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(541, 159), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(545, 169), new List<Color> { Color.FromArgb(201, 28, 34) }),
                (new Point(545, 662), new List<Color> { Color.FromArgb(204, 28, 34) }),
                (new Point(686, 122), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(687, 699), new List<Color> { Color.FromArgb(201, 28, 34) }),
                (new Point(688, 130), new List<Color> { Color.FromArgb(204, 28, 34) }),
                (new Point(702, 709), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(827, 673), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(832, 169), new List<Color> { Color.FromArgb(201, 28, 34) }),
                (new Point(832, 660), new List<Color> { Color.FromArgb(203, 28, 34) }),
                (new Point(837, 162), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(934, 273), new List<Color> { Color.FromArgb(205, 28, 34) }),
                (new Point(934, 557), new List<Color> { Color.FromArgb(202, 28, 34) }),
                (new Point(944, 271), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(945, 559), new List<Color> { Color.FromArgb(30, 73, 145) }),
                (new Point(972, 413), new List<Color> { Color.FromArgb(199, 29, 35) }),
                (new Point(980, 418), new List<Color> { Color.FromArgb(30, 73, 145) }),
            };

            return mapping.All(x => x.Color.Contains(image.GetPixel(x.Point.X, x.Point.Y)));
        }

        public void StartMission(Bitmap image)
        {
            var shields = new List<Point>
            {
                new Point(559, 475),
                new Point(569, 363),
                new Point(666, 540),
                new Point(696, 289),
                new Point(704, 425),
                new Point(824, 333),
                new Point(828, 451),
            };

            var color = Color.FromArgb(202, 83, 100);

            foreach (var point in shields.Where(x => image.GetPixel(x.X, x.Y) == color))
            {
                MouseController.Click(point.X, point.Y, 50);
            }
        }
    }
}
