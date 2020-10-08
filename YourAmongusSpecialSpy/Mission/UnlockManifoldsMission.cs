using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class UnlockManifoldsMission : IMission
    {
        private List<Point> _leftTop = new List<Point>
        {
            new Point(422, 312),
            new Point(531, 312),
            new Point(640, 312),
            new Point(749, 312),
            new Point(858, 312),
            new Point(422, 422),
            new Point(531, 422),
            new Point(640, 422),
            new Point(749, 422),
            new Point(858, 422),
        };

        public string GetName()
        {
            return "매니폴드 열기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var points = new List<Color>
            {
                image.GetPixel(400, 288),
                image.GetPixel(666, 288),
                image.GetPixel(977, 288),
            };

            var mapping = new List<(Point Point, Color Color)>
            {
                (new Point(444, 333), Color.FromArgb(142, 161, 208)),
                (new Point(666, 333), Color.FromArgb(142, 161, 208)),
                (new Point(877, 333), Color.FromArgb(142, 161, 208)),
            };

            return points.All(x =>
                   x.R >= 169 && x.R <= 175
                && x.G >= 169 && x.G <= 175
                && x.B >= 169 && x.B <= 175
            )
                && mapping.All(x => image.GetPixel(x.Point.X, x.Point.Y) == x.Color);
        }

        public void StartMission(Bitmap image)
        {
            var digitList = SplitDigit(image);

            var digitNumberData = digitList
                .Select((digitImage, i) => new { Index = i, Number = ImageRecognizer.Recognize(digitImage) })
                .OrderBy(x => x.Number)
                .ToList();

            digitNumberData
                .ForEach(data =>
                {
                    var point = _leftTop[data.Index];
                    MouseController.Move(point.X + 50, point.Y + 50);
                    Thread.Sleep(TimeSpan.FromMilliseconds(50));
                    MouseController.Click();
                    Thread.Sleep(TimeSpan.FromMilliseconds(50));
                });

            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        private List<Bitmap> SplitDigit(Image image)
        {
            return _leftTop
                .Select(p1 => image.Cut(p1.X + 20, p1.Y + 20, 96 - 40, 96 - 40))
                .ToList();
        }
    }

    public static class ImageRecognizer
    {
        public static int Recognize(Bitmap image)
        {
            var models = NumberModel.MakeNumberModelList();
            var list = models.Select(x => new { Model = x, Similarity = x.CheckSimilarity(image) })
                .OrderByDescending(x => x.Similarity)
                .ToList();

            return list
                .First()
                .Model.Number;
        }
    }

    public class NumberModel
    {
        public int Number { get; set; }
        public List<int> BlackCount { get; set; }

        private static readonly string NumberModelText = @"
0,0,2,0,0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,1,2,0,0,0,0,2,2,2,1,0 
0,1,2,2,0,0,0,1,0,2,0,0,0,0,0,2,0,0,0,0,1,1,0,0,0,1,2,1,1,0,0,2,2,2,1,0 
0,1,2,2,0,0,0,0,0,2,0,0,0,0,2,2,1,0,0,0,0,0,2,0,0,2,0,1,2,0,0,1,2,2,1,0 
0,1,1,1,0,0,0,2,1,1,0,0,0,2,2,2,1,0,0,1,0,1,1,0,0,0,0,1,0,0,0,0,0,1,0,0 
0,2,2,2,1,0,0,2,0,0,0,0,0,2,2,2,1,0,0,1,0,0,2,0,0,2,1,1,2,0,0,1,2,2,0,0 
0,0,2,1,0,0,0,2,1,0,0,0,0,2,2,2,1,0,0,2,0,1,2,0,0,2,1,1,2,0,0,1,2,2,1,0 
0,2,2,2,2,0,0,0,1,2,1,0,0,0,1,2,0,0,0,0,1,1,0,0,0,0,2,0,0,0,0,0,1,0,0,0 
0,1,2,2,0,0,0,2,1,2,0,0,0,1,2,2,0,0,0,2,1,2,2,0,0,2,0,1,2,0,0,1,2,2,1,0 
0,1,2,2,1,0,0,2,0,1,2,0,0,1,2,2,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0 
0,1,1,2,1,0,0,2,2,2,2,0,0,1,2,1,2,0,0,1,2,1,2,0,0,2,1,2,2,0,0,2,2,1,1,0 
";

        public static List<NumberModel> MakeNumberModelList()
        {
            return NumberModelText.Trim().Replace("\r", "").Split('\n')
                .Select((x, i) => new NumberModel
                {
                    Number = i + 1,
                    BlackCount = x.Trim().Split(',').Select(e => int.Parse(e.Trim())).ToList(),
                })
                .ToList();
        }

        public static List<int> CalcModel(Bitmap image, int splitCount)
        {
            var result = new List<int>();

            var widthSplit = SplitNumber(image.Size.Width, splitCount);
            var heightSplit = SplitNumber(image.Size.Height, splitCount);

            for (int row = 0; row < splitCount; row++)
            {
                for (var column = 0; column < splitCount; column++)
                {
                    var count = 0;
                    var beginPoint = new Point(widthSplit.Take(column).Sum(), heightSplit.Take(row).Sum());
                    for (var dx = 0; dx < widthSplit[column]; dx++)
                    {
                        for (var dy = 0; dy < heightSplit[row]; dy++)
                        {
                            var x = beginPoint.X + dx;
                            var y = beginPoint.Y + dy;
                            var pixel = image.GetPixel(x, y);
                            if (IsNumberColor(pixel))
                            {
                                count++;
                            }
                            else
                            {
                            }
                        }
                    }

                    result.Add(count);
                }
            }

            return result.Select(x => x == 0 ? 0 : x < 20 ? 1 : 2).ToList();
        }

        static List<int> SplitNumber(int number, int splitCount)
        {
            return Enumerable.Range(0, splitCount)
                .Select(x =>
                {
                    if (x < (number % splitCount))
                        return number / splitCount + 1;
                    else
                        return number / splitCount;
                })
                .ToList();
        }
        private static bool IsNumberColor(Color color)
        {
            if (color.B > 60 && color.R < 100 && color.G < 100)
                return true;

            return false;
        }

        private int CheckSimilarity(List<int> blackCounts)
        {
            return BlackCount
                .Select((x, i) => new { Model = x, Image = blackCounts[i] })
                .Count(x => x.Model == x.Image);
        }

        public int CheckSimilarity(Bitmap image)
        {
            return CheckSimilarity(CalcModel(image, 6));
        }
    }
}
