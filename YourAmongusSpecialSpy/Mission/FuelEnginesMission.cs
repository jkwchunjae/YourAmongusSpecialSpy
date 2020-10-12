using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class FuelEnginesMission : IMission
    {
        public string GetName()
        {
            return "엔진 연료 공급하기";
        }

        public bool IsMyMission(Bitmap image)
        {
            return FindButton(image) && FindEmptyArea(image);
        }

        private bool FindButton(Bitmap image)
        {
            Point leftTop = new Point(1025, 635);
            Point rightBotton = new Point(1065, 680);

            List<Point> _mapping = Enumerable.Range(leftTop.X, rightBotton.X - leftTop.X)
                .Join(Enumerable.Range(leftTop.Y, rightBotton.Y - leftTop.Y),
                    x => 1, y => 1, (x, y) => new Point(x, y))
                .ToList();

            var color = Color.FromArgb(203, 203, 203);

            return _mapping.Count(p => image.GetPixel(p.X, p.Y) == color) == _mapping.Count;
        }

        private bool FindEmptyArea(Bitmap image)
        {
            Point _leftTop = new Point(636, 247);
            Point _rightBotton = new Point(717, 555);

            List<Point> _mapping = Enumerable.Range(_leftTop.X, _rightBotton.X - _leftTop.X).Where(x => x % 3 == 0)
                .Join(Enumerable.Range(_leftTop.Y, _rightBotton.Y - _leftTop.Y).Where(x => x % 3 == 0),
                    x => 1, y => 1, (x, y) => new Point(x, y))
                .ToList();

            var black = Color.FromArgb(0, 0, 0);
            return _mapping.Count(p => image.GetPixel(p.X, p.Y) == black) > _mapping.Count * 0.9;
        }

        public void StartMission(Bitmap image)
        {
            var button = new Point(1045, 658);

            MouseController.LeftDown(button, 50);
            Thread.Sleep(3500);
            MouseController.LeftUp(button);
        }
    }
}
