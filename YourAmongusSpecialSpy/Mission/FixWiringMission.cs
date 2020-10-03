using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class FixWiringMission : IMission
    {
        private readonly Color[] _wireColor = new Color[]
        {
            Color.FromArgb(0, 0, 255), // blue
            Color.FromArgb(255, 0, 0), // red
            Color.FromArgb(255, 0, 255), // magenta
            Color.FromArgb(255, 235, 4), // yellow
        };

        private readonly Point[] _left = new Point[]
        {
            new Point(400, 222),
            new Point(400, 357),
            new Point(400, 491),
            new Point(400, 621),
        };

        private readonly Point[] _right = new Point[]
        {
            new Point(970, 222),
            new Point(970, 357),
            new Point(970, 491),
            new Point(970, 621),
        };

        public string GetName()
        {
            return "전선 연결하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var leftCheck = _left
                .Select(leftPoint => image.GetPixel(leftPoint.X, leftPoint.Y))
                .All(leftColor => _wireColor.Contains(leftColor));

            var rightCheck = _right
                .Select(rightPoint => image.GetPixel(rightPoint.X, rightPoint.Y))
                .All(rightColor => _wireColor.Contains(rightColor));

            return leftCheck && rightCheck;
        }

        public void StartMission(Bitmap image)
        {
            foreach (var leftPoint in _left)
            {
                var leftColor = image.GetPixel(leftPoint.X, leftPoint.Y);

                var rightPoint = _right.FirstOrDefault(rp => image.GetPixel(rp.X, rp.Y) == leftColor);

                if (rightPoint != null)
                {
                    MouseController.LeftDown(leftPoint);
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    MouseController.Move(rightPoint);
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    MouseController.LeftUp(rightPoint);
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
                }
            }
        }
    }
}
