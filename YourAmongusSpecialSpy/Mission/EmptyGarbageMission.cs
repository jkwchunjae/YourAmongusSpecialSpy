using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class EmptyGarbageMission : IMission
    {
        public string GetName()
        {
            return "쓰레기 비우기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var mapping = new List<(Point Point, List<Color> Color)>
            {
                (new Point(403, 119), new List<Color> { Color.FromArgb(81, 93, 109) }),
                (new Point(432, 117), new List<Color> { Color.FromArgb(81, 93, 109) }),
                (new Point(467, 121), new List<Color> { Color.FromArgb(81, 93, 109) }),
                (new Point(506, 117), new List<Color> { Color.FromArgb(86, 99, 116) }),
                (new Point(523, 120), new List<Color> { Color.FromArgb(91, 106, 123) }),
                (new Point(534, 122), new List<Color> { Color.FromArgb(107, 124, 145) }),
                (new Point(549, 119), new List<Color> { Color.FromArgb(129, 143, 156) }),
                (new Point(560, 120), new List<Color> { Color.FromArgb(144, 154, 164) }),
                (new Point(567, 270), new List<Color> { Color.FromArgb(84, 141, 203) }),
                (new Point(585, 117), new List<Color> { Color.FromArgb(151, 160, 168) }),
                (new Point(605, 269), new List<Color> { Color.FromArgb(84, 141, 203) }),
                (new Point(614, 120), new List<Color> { Color.FromArgb(144, 154, 164) }),
                (new Point(634, 120), new List<Color> { Color.FromArgb(129, 143, 156) }),
                (new Point(654, 120), new List<Color> { Color.FromArgb(107, 124, 145) }),
                (new Point(666, 119), new List<Color> { Color.FromArgb(91, 106, 123) }),
                (new Point(692, 120), new List<Color> { Color.FromArgb(86, 99, 116) }),
                (new Point(748, 124), new List<Color> { Color.FromArgb(81, 93, 109) }),
                (new Point(778, 124), new List<Color> { Color.FromArgb(81, 93, 109) }),
                (new Point(889, 304), new List<Color> { Color.FromArgb(121, 130, 154) }),
                (new Point(890, 321), new List<Color> { Color.FromArgb(20, 27, 26) }),
                (new Point(891, 291), new List<Color> { Color.FromArgb(121, 131, 156) }),
                (new Point(895, 325), new List<Color> { Color.FromArgb(140, 170, 181) }),
                (new Point(899, 328), new List<Color> { Color.FromArgb(126, 160, 170) }),
                (new Point(902, 331), new List<Color> { Color.FromArgb(90, 125, 140) }),
                (new Point(907, 401), new List<Color> { Color.FromArgb(145, 175, 187) }),
                (new Point(908, 364), new List<Color> { Color.FromArgb(115, 127, 151) }),
                (new Point(909, 364), new List<Color> { Color.FromArgb(115, 127, 151) }),
            };

            return mapping.All(x => x.Color.Contains(image.GetPixel(x.Point.X, x.Point.Y)));
        }

        public void StartMission(Bitmap image)
        {
            MouseController.LeftDown(new Point(900, 330), 50);
            Thread.Sleep(200);
            MouseController.Move(900, 550);
            Thread.Sleep(3000);
            MouseController.LeftUp(new Point(900, 550));
        }
    }
}
