using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class CleanO2FilterMission : IMission
    {
        public string GetName()
        {
            return "산소 필터 청소하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var validCount = _mapping
                .Count(x => image.GetPixel(x.X, x.Y) == x.Color);

            return validCount > _mapping.Count() * 0.7;
        }

        public void StartMission(Bitmap image)
        {
            var goal = new Point(460, 413);

            while (IsMyMission(image))
            {
                var diff = _mapping.Where(x => image.GetPixel(x.X, x.Y) != x.Color).ToList();
                if (diff.Any())
                {
                    var first = diff[diff.Count / 2];
                    MouseController.LeftDown(new Point(first.X, first.Y), 50);
                    MouseController.LeftUp(new Point(goal.X, goal.Y), 50);
                }

                Thread.Sleep(100);
                image = Amongus.GetImage();
            }
        }

        readonly List<(int X, Color Color)> _xMapping = new List<(int X, Color Color)>
            {
                (510, Color.FromArgb(123, 151, 209)),
                (520, Color.FromArgb(123, 151, 209)),
                (530, Color.FromArgb(126, 154, 209)),
                (540, Color.FromArgb(129, 158, 211)),
                (550, Color.FromArgb(132, 159, 211)),
                (560, Color.FromArgb(134, 162, 214)),
                (570, Color.FromArgb(140, 166, 214)),
                (580, Color.FromArgb(140, 167, 214)),
                (590, Color.FromArgb(148, 170, 217)),
                (600, Color.FromArgb(148, 172, 220)),
                (610, Color.FromArgb(151, 175, 222)),
                (620, Color.FromArgb(156, 178, 222)),
                (630, Color.FromArgb(158, 181, 224)),
                (640, Color.FromArgb(163, 183, 224)),
                (650, Color.FromArgb(165, 186, 228)),
                (660, Color.FromArgb(170, 189, 231)),
                (670, Color.FromArgb(170, 192, 228)),
                (680, Color.FromArgb(176, 195, 231)),
                (690, Color.FromArgb(181, 197, 231)),
                (700, Color.FromArgb(181, 200, 231)),
                (710, Color.FromArgb(187, 203, 236)),
                (720, Color.FromArgb(189, 206, 239)),
                (730, Color.FromArgb(192, 208, 239)),
                (740, Color.FromArgb(195, 211, 239)),
                (750, Color.FromArgb(198, 215, 242)),
                (760, Color.FromArgb(200, 216, 247)),
                (770, Color.FromArgb(206, 219, 244)),
                (780, Color.FromArgb(209, 223, 247)),
                (790, Color.FromArgb(209, 223, 247)),
                (800, Color.FromArgb(206, 219, 244)),
                (810, Color.FromArgb(202, 218, 247)),
                (820, Color.FromArgb(198, 215, 242)),
                (830, Color.FromArgb(195, 211, 239)),
                (840, Color.FromArgb(192, 208, 239)),
                (850, Color.FromArgb(189, 207, 237)),
                (860, Color.FromArgb(187, 204, 236)),
                (870, Color.FromArgb(181, 200, 233)),
                (880, Color.FromArgb(181, 199, 233)),
                (890, Color.FromArgb(176, 195, 231)),
                (900, Color.FromArgb(173, 193, 231)),
                (910, Color.FromArgb(169, 190, 231)),
                (920, Color.FromArgb(165, 187, 228)),
                (930, Color.FromArgb(165, 185, 222)),
                (940, Color.FromArgb(159, 181, 225)),
                (950, Color.FromArgb(156, 179, 222)),
                (960, Color.FromArgb(154, 177, 222)),
                (970, Color.FromArgb(148, 172, 220)),
                (980, Color.FromArgb(148, 170, 217)),
            };

        readonly List<int> _yList = Enumerable.Range(110, 710 - 110).Where(x => x % 10 == 0).ToList();

        List<(int X, int Y, Color Color)> _mapping
            => _xMapping.Join(_yList, a => 1, b => 1, (m, y) => (m.X, y, m.Color)).ToList();
    }
}
