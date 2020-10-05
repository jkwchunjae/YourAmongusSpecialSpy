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

            return points.All(x =>
                   x.R >= 169 && x.R <= 175
                && x.G >= 169 && x.G <= 175
                && x.B >= 169 && x.B <= 175
            );
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
                    MouseController.Move(point.X + 10, point.Y + 10);
                    Thread.Sleep(TimeSpan.FromMilliseconds(50));
                    MouseController.Click(point.X + 10, point.Y + 10);
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
0,53,0,0,0,49,0,0,0,46,4,0,7,89,52,5 
0,61,46,0,0,0,57,0,0,36,31,0,6,82,54,5 
0,40,45,0,0,26,78,0,0,0,30,21,7,61,60,1 
12,31,25,0,3,85,80,1,8,22,58,3,0,0,34,0 
0,74,48,0,3,82,45,0,11,5,31,20,2,57,52,0 
0,44,14,0,11,76,28,0,35,39,41,18,3,58,58,4 
7,54,76,6,0,0,58,0,0,26,26,0,0,26,6,0 
0,63,51,0,0,80,63,0,20,44,57,11,7,56,56,5 
0,57,66,0,0,75,95,0,0,0,49,0,0,0,31,0 
0,59,74,3,0,58,68,33,0,52,60,40,15,79,66,15 
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

        private static List<int> GetImageNumberCount(Bitmap image)
        {
            var numberCount = new List<int>();
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    var count = 0;
                    var beginPoint = new Point(column * 14, row * 14);
                    for (var dx = 0; dx < 14; dx++)
                    {
                        for (var dy = 0; dy < 14; dy++)
                        {
                            var x = beginPoint.X + dx;
                            var y = beginPoint.Y + dy;
                            var pixel = image.GetPixel(x, y);
                            if (IsNumberColor(pixel))
                            {
                                count++;
                            }
                        }
                    }

                    numberCount.Add(count);
                }
            }

            return numberCount;
        }

        private static bool IsNumberColor(Color color)
        {
            if (color.B > 60 && color.R < 100 && color.G < 100)
                return true;

            return false;
        }

        private int CheckSimilarity(List<int> blackCounts)
        {
            var pair = BlackCount
                .Select((x, i) => new { Model = x, Image = blackCounts[i] })
                .ToList();

            var same = pair
                .Select(x => new
                {
                    Model = x.Model == 0 ? 0 : x.Model < 20 ? 1 : 2,
                    Image = x.Image == 0 ? 0 : x.Image < 20 ? 1 : 2,
                })
                .Count(x => x.Model == x.Image);

            return same;
        }

        public int CheckSimilarity(Bitmap image)
        {
            return CheckSimilarity(GetImageNumberCount(image));
        }
    }
}
