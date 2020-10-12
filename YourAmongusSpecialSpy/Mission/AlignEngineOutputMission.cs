using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class AlignEngineOutputMission : IMission
    {
        static readonly Point leftTop = new Point(420, 150);
        static readonly Point rightBotton = new Point(800, 680);

        readonly List<Point> _mapping = Enumerable.Range(leftTop.X, rightBotton.X - leftTop.X).Where(x => x % 10 == 0)
            .Join(Enumerable.Range(leftTop.Y, rightBotton.Y - leftTop.Y).Where(y => y % 10 == 0),
                x => 1, y => 1, (x, y) => new Point(x, y))
            .ToList();

        public string GetName()
        {
            return "엔진 출력 정렬 시키기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var green = Color.FromArgb(12, 30, 12);
            var red = Color.FromArgb(255, 0, 0);

            var greenRatio = _mapping.Count(p => image.GetPixel(p.X, p.Y) == green) > _mapping.Count * 0.8;
            var redRatio = Enumerable.Range(leftTop.X, rightBotton.X - leftTop.X).Count(x => image.GetPixel(x, 415) == red) > 100;
            return greenRatio && redRatio;
        }

        public void StartMission(Bitmap image)
        {
            var target = GetTarget(image);
            var goal = new Point(920, 415);

            if (target.IsEmpty)
                return;

            MouseController.LeftDown(target, 50);
            MouseController.LeftUp(goal, 50);
        }

        private Point GetTarget(Bitmap image)
        {
            var color = Color.FromArgb(65, 65, 75);
            var x = 920;

            for (var y = 133; y < 700; y++)
            {
                if (image.GetPixel(x, y) == color)
                    return new Point(x, y);
            }

            return Point.Empty;
        }
    }
}
