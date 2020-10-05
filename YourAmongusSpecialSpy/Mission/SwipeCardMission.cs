using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public class SwipeCardMission : IMission
    {
        public string GetName()
        {
            return "카드 긋기";
        }

        public bool IsMyMission(Bitmap image)
        {
            var mapping = new List<(Point Point, Color Color)>
            {
                (new Point(900, 150), Color.FromArgb(22, 74, 57)),
                (new Point(913, 264), Color.FromArgb(99, 0, 0)),
                (new Point(960, 264), Color.FromArgb(0, 99, 71)),
            };

            return mapping.All(x => image.GetPixel(x.Point.X, x.Point.Y) == x.Color);
        }

        public void StartMission(Bitmap image)
        {
            MouseController.Click(600, 620);
            Thread.Sleep(1000);
            MouseController.Move(380, 330);
            Thread.Sleep(50);
            MouseController.LeftDown(new Point(380, 330));
            Thread.Sleep(50);
            for (var x = 400; x <= 1030; x+= 30)
            {
                MouseController.Move(x, 330);
                Thread.Sleep(50);
            }
            Thread.Sleep(50);
            MouseController.LeftUp(new Point(1020, 330));
            Thread.Sleep(2000);
        }
    }
}
