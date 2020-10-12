using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class DivertPowerMission : IMission
    {
        public string GetName()
        {
            return "에너지 전환하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var mapping = new List<(Point Point, List<Color> Color)>
            {
                (new Point(448, 220), new List<Color> { Color.FromArgb(0, 0, 0) }),
                (new Point(450, 381), new List<Color> { Color.FromArgb(253, 255, 92) }),
                (new Point(452, 164), new List<Color> { Color.FromArgb(0, 0, 0) }),
                (new Point(470, 287), new List<Color> { Color.FromArgb(66, 48, 0) }),
                (new Point(474, 334), new List<Color> { Color.FromArgb(123, 89, 0) }),
                (new Point(483, 376), new List<Color> { Color.FromArgb(181, 148, 69) }),
                (new Point(514, 388), new List<Color> { Color.FromArgb(253, 255, 92) }),
                (new Point(585, 385), new List<Color> { Color.FromArgb(253, 255, 92) }),
                (new Point(656, 380), new List<Color> { Color.FromArgb(253, 255, 92) }),
                (new Point(723, 388), new List<Color> { Color.FromArgb(253, 255, 92) }),
                (new Point(724, 388), new List<Color> { Color.FromArgb(253, 255, 92) }),
                (new Point(795, 382), new List<Color> { Color.FromArgb(253, 255, 92) }),
                (new Point(863, 395), new List<Color> { Color.FromArgb(253, 255, 92) }),
                (new Point(928, 389), new List<Color> { Color.FromArgb(253, 255, 92) }),
            };

            return mapping.All(x => x.Color.Contains(image.GetPixel(x.Point.X, x.Point.Y)));
        }

        public void StartMission(Bitmap image)
        {
            var position = new List<Point>
            {
                new Point(448, 590),
                new Point(515, 590),
                new Point(583, 590),
                new Point(654, 590),
                new Point(722, 590),
                new Point(789, 590),
                new Point(859, 590),
                new Point(928, 590),
            }
            .Select((x, i) => new { Point = x, Index = i })
            .ToList();

            var color = new List<Color>
            {
                Color.FromArgb(168, 16, 16),
                Color.FromArgb(168, 17, 16),
                Color.FromArgb(169, 16, 16),
                Color.FromArgb(169, 17, 16),
            };
            var power = position.FirstOrDefault(x => color.Contains(image.GetPixel(x.Point.X, x.Point.Y)));

            if (power != null)
            {
                MouseController.LeftDown(power.Point, 50);
                MouseController.LeftUp(new Point(power.Point.X, power.Point.Y - 80), 50);
            }
        }
    }
}
