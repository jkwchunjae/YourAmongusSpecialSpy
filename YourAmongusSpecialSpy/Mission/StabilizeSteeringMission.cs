using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class StabilizeSteeringMission : IMission
    {
        public string GetName()
        {
            return "항로 조정하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var mapping = new List<(Point Point, List<Color> Color)>
            {
                (new Point(452, 390), new List<Color> { Color.FromArgb(41, 117, 169) }),
                (new Point(459, 348), new List<Color> { Color.FromArgb(41, 117, 168) }),
                (new Point(462, 286), new List<Color> { Color.FromArgb(38, 109, 157) }),
                (new Point(498, 386), new List<Color> { Color.FromArgb(47, 135, 194) }),
                (new Point(501, 344), new List<Color> { Color.FromArgb(46, 131, 189) }),
                (new Point(513, 286), new List<Color> { Color.FromArgb(44, 125, 180) }),
                (new Point(518, 238), new List<Color> { Color.FromArgb(40, 115, 165) }),
                (new Point(521, 196), new List<Color> { Color.FromArgb(35, 103, 149) }),
                (new Point(543, 241), new List<Color> { Color.FromArgb(42, 122, 175) }),
                (new Point(556, 393), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(564, 337), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(568, 289), new List<Color> { Color.FromArgb(50, 142, 204) }),
                (new Point(614, 288), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(616, 344), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(620, 387), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(657, 386), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(659, 176), new List<Color> { Color.FromArgb(40, 116, 168) }),
                (new Point(666, 337), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(670, 245), new List<Color> { Color.FromArgb(50, 143, 205) }),
                (new Point(670, 290), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(686, 416), new List<Color> { Color.FromArgb(255, 255, 255) }),
                (new Point(687, 380), new List<Color> { Color.FromArgb(255, 255, 255) }),
                (new Point(705, 384), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(707, 244), new List<Color> { Color.FromArgb(50, 142, 204) }),
                (new Point(713, 179), new List<Color> { Color.FromArgb(41, 117, 169) }),
                (new Point(724, 291), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(730, 339), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(748, 245), new List<Color> { Color.FromArgb(49, 139, 200) }),
                (new Point(758, 339), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(770, 384), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(778, 187), new List<Color> { Color.FromArgb(39, 114, 165) }),
                (new Point(779, 291), new List<Color> { Color.FromArgb(52, 149, 214) }),
                (new Point(811, 389), new List<Color> { Color.FromArgb(54, 152, 218) }),
                (new Point(815, 335), new List<Color> { Color.FromArgb(53, 150, 216) }),
                (new Point(817, 291), new List<Color> { Color.FromArgb(49, 139, 200) }),
                (new Point(818, 240), new List<Color> { Color.FromArgb(44, 125, 180) }),
                (new Point(825, 190), new List<Color> { Color.FromArgb(37, 108, 155) }),
                (new Point(864, 343), new List<Color> { Color.FromArgb(47, 135, 194) }),
                (new Point(865, 289), new List<Color> { Color.FromArgb(44, 125, 180) }),
                (new Point(867, 388), new List<Color> { Color.FromArgb(49, 138, 199) }),
                (new Point(870, 240), new List<Color> { Color.FromArgb(39, 112, 161) }),
                (new Point(914, 325), new List<Color> { Color.FromArgb(40, 115, 166) }),
                (new Point(920, 294), new List<Color> { Color.FromArgb(37, 108, 156) }),
                (new Point(925, 383), new List<Color> { Color.FromArgb(40, 116, 168) }),
            };

            return mapping.Count(x => x.Color.Contains(image.GetPixel(x.Point.X, x.Point.Y))) > mapping.Count * 0.7;
        }

        public void StartMission(Bitmap image)
        {
            var target = GetCenter(image);
            var center = new Point(688, 415);

            if (target.IsEmpty)
                return;

            MouseController.LeftDown(target, 50);
            MouseController.LeftUp(center, 50);
        }


        public Point GetCenter(Bitmap image)
        {
            var center = new Point(688, 415);
            var radius = 278;
            var colorWhite = Color.FromArgb(255, 255, 255);

            var whites = center.Circle(radius, 1)
                .Where(x => image.GetPixel(x.X, x.Y) == colorWhite)
                .ToList();

            if (whites.Any())
            {
                var minX = whites.Min(x => x.X);
                var maxX = whites.Max(x => x.X);
                var avgX = whites.Where(x => x.X > minX + 10 && x.X < maxX - 10).Average(x => x.X);

                var minY = whites.Min(x => x.Y);
                var maxY = whites.Max(x => x.Y);
                var avgY = whites.Where(x => x.Y > minY + 10 && x.Y < maxY - 10).Average(x => x.Y);

                return new Point((int)avgX, (int)avgY);
            }
            else
            {
                return Point.Empty;
            }
        }
    }
}
