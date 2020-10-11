using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class ClearAsteroidsMission : IMission
    {
        static readonly Point leftTop = new Point(404, 130);
        static readonly Point rightBotton = new Point(970, 650);

        readonly List<Point> _mapping = Enumerable.Range(leftTop.X, rightBotton.X - leftTop.X).Where(x => x % 10 == 0)
            .Join(Enumerable.Range(leftTop.Y, rightBotton.Y - leftTop.Y).Where(y => y % 10 == 0),
                x => 1, y => 1, (x, y) => new Point(x, y))
            .ToList();

        readonly List<Point> _8way = new List<Point>
        {
            new Point(-1, -1),
            new Point(0, -1),
            new Point(1, -1),
            new Point(-1, 0),
            //new Point(0, 0),
            new Point(1, 0),
            new Point(-1, 1),
            new Point(0, 1),
            new Point(1, 1),
        };

        public string GetName()
        {
            return "소행성 파괴하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            return IsMyMission(GetImageColor(image), 0.7);
        }

        private bool IsMyMission(List<(Point Point, Color Color)> imageColor, double ratio)
        {
            var validCount = imageColor.Count(x => IsMissionColor(x.Color));

            return validCount > _mapping.Count * ratio;
        }

        public void StartMission(Bitmap image)
        {
            var imageColor = GetImageColor(image);

            while (IsMyMission(imageColor, 0.5))
            {
                var diff = imageColor.Where(x => IsAsteroidColor(x.Color)).ToList();
                if (diff.Any())
                {
                    var find = diff.Where(x => FindAsteroid(image, x.Point)).ToList();
                    if (find.Any())
                    {
                        var first = find[find.Count / 2].Point;
                        MouseController.Click(first.X - 20, first.Y, 20);
                    }
                }

                Thread.Sleep(30);
                image = Amongus.GetImage();
                imageColor = GetImageColor(image);
            }
        }

        private bool IsAsteroidColor(Color color)
        {
            return color.R >= 54 && color.R <= 61 
                && color.G >= 109 && color.G <= 126
                && color.B >= 63 && color.B <= 72;
        }

        private bool FindAsteroid(Bitmap image, Point point)
        {
            var center = image.GetPixel(point.X, point.Y);
            return _8way.Count(x => IsAsteroidColor(image.GetPixel(point.X + x.X, point.Y + x.Y))) > 6;
        }

        private List<(Point Point, Color Color)> GetImageColor(Bitmap image)
        {
            return _mapping.Select(x => (x, image.GetPixel(x.X, x.Y))).ToList();
        }

        private bool IsMissionColor(Color color)
        {
            return color.G > 110 && color.R < 100 && color.B < 100;
        }
    }
}
