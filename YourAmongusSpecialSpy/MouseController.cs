using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy
{
    public static class MouseController
    {
        public static void Move(int x, int y)
        {
            var p = Amongus.GetProcess();
            var hWnd = p.MainWindowHandle;
            var rect = new User32.RECT();
            User32.GetWindowRect(hWnd, ref rect);

            var windowX = rect.left;
            var windowY = rect.top;

            SetCursorPos(windowX + x, windowY + y);
        }

        public static void Move(Point point)
        {
            Move(point.X, point.Y);
        }

        public static void Click(int x, int y, int sleepMilliseconds = 0)
        {
            Move(x, y);

            if (sleepMilliseconds > 0)
                Thread.Sleep(sleepMilliseconds);

            mouse_event((uint)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((uint)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);

            if (sleepMilliseconds > 0)
                Thread.Sleep(sleepMilliseconds);
        }

        public static void Click()
        {
            mouse_event((uint)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((uint)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        public static void LeftDown(Point point, int sleepMilliseconds = 0)
        {
            Move(point);

            if (sleepMilliseconds > 0)
                Thread.Sleep(sleepMilliseconds);

            mouse_event((uint)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);

            if (sleepMilliseconds > 0)
                Thread.Sleep(sleepMilliseconds);
        }

        public static void LeftUp(Point point, int sleepMilliseconds = 0)
        {
            Move(point);

            if (sleepMilliseconds > 0)
                Thread.Sleep(sleepMilliseconds);

            mouse_event((uint)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);

            if (sleepMilliseconds > 0)
                Thread.Sleep(sleepMilliseconds);
        }

        private enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("User32.Dll")]
        private static extern long SetCursorPos(int x, int y);

    }
}
