using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class ChartCourseMission : IMission
    {
        static readonly Point leftTop = new Point(340, 220);
        static readonly Point rightBotton = new Point(1030, 610);

        readonly List<Point> _mapping = Enumerable.Range(leftTop.X, rightBotton.X - leftTop.X).Where(x => x % 10 == 0)
            .Join(Enumerable.Range(leftTop.Y, rightBotton.Y - leftTop.Y).Where(y => y % 10 == 0),
                x => 1, y => 1, (x, y) => new Point(x, y))
            .ToList();

        public string GetName()
        {
            return "항로 계획하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var color = Color.FromArgb(55, 153, 220);

            return _mapping.Count(x => image.GetPixel(x.X, x.Y) == color) > _mapping.Count * 0.7;
        }

        public void StartMission(Bitmap image)
        {
            var xList = new[] { 410, 550, 690, 830, 970, };

            var points = xList.Select(x => new Point(x, GetPoint(image, x))).ToList();

            MouseController.LeftDown(points[0], 50);
            MouseController.LeftUp(points[1], 50);

            MouseController.LeftDown(points[1], 50);
            MouseController.LeftUp(points[2], 50);

            MouseController.LeftDown(points[2], 50);
            MouseController.LeftUp(points[3], 50);

            MouseController.LeftDown(points[3], 50);
            MouseController.LeftUp(points[4], 50);
        }

        int GetPoint(Bitmap image, int x)
        {
            var color = Color.FromArgb(55, 153, 220);

            var list = Enumerable.Range(leftTop.Y, rightBotton.Y - leftTop.Y)
                .Select(y => new { Y = y, Color = image.GetPixel(x, y) })
                .Where(e => e.Color != color)
                .ToList();

            return list[list.Count / 2].Y;
        }
    }
}
