using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class AcceptDivertedPowerMission : IMission
    {
        public string GetName()
        {
            return "전환된 에너지 받기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var mapping = new List<(Point Point, List<Color> Color)>
            {
                (new Point(360, 405), new List<Color> { Color.FromArgb(221, 180, 64) }),
                (new Point(368, 394), new List<Color> { Color.FromArgb(209, 201, 180) }),
                (new Point(370, 445), new List<Color> { Color.FromArgb(153, 121, 26) }),
                (new Point(371, 409), new List<Color> { Color.FromArgb(151, 118, 17) }),
                (new Point(376, 436), new List<Color> { Color.FromArgb(129, 92, 1) }),
                (new Point(378, 395), new List<Color> { Color.FromArgb(175, 148, 59) }),
                (new Point(379, 424), new List<Color> { Color.FromArgb(185, 142, 12) }),
                (new Point(387, 378), new List<Color> { Color.FromArgb(161, 158, 147) }),
                (new Point(387, 424), new List<Color> { Color.FromArgb(148, 107, 0) }),
                (new Point(657, 561), new List<Color> { Color.FromArgb(105, 105, 105) }),
                (new Point(704, 395), new List<Color> { Color.FromArgb(105, 105, 105) }),
                (new Point(765, 521), new List<Color> { Color.FromArgb(234, 234, 234) }),
                (new Point(774, 287), new List<Color> { Color.FromArgb(105, 105, 105) }),
                (new Point(791, 441), new List<Color> { Color.FromArgb(224, 224, 224) }),
                (new Point(805, 359), new List<Color> { Color.FromArgb(255, 255, 255) }),
                (new Point(884, 351), new List<Color> { Color.FromArgb(168, 168, 168) }),
                (new Point(917, 548), new List<Color> { Color.FromArgb(105, 105, 105) }),
                (new Point(921, 329), new List<Color> { Color.FromArgb(105, 105, 105) }),
                (new Point(968, 288), new List<Color> { Color.FromArgb(105, 105, 105) }),
                (new Point(1010, 470), new List<Color> { Color.FromArgb(105, 105, 105) }),
                (new Point(1013, 344), new List<Color> { Color.FromArgb(105, 105, 105) }),
            };

            return mapping.All(x => x.Color.Contains(image.GetPixel(x.Point.X, x.Point.Y)));
        }

        public void StartMission(Bitmap image)
        {
            Thread.Sleep(100);
            MouseController.Click(688, 415, 50);
        }
    }
}
