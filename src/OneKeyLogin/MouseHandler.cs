using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace OneKeyLogin
{
    public static class MouseHandler
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public const int MOUSEEVENTF_LEFTDOWN = 0x2;
        public const int MOUSEEVENTF_LEFTUP = 0x4;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetCursorPos(int x, int y);

        /// <summary>
        /// 移动鼠标到指定位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void Move(int x, int y)
        {

        }

        public static void  MouseClick()
        {
            mouse_event(MouseHandler.MOUSEEVENTF_LEFTDOWN | MouseHandler.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }
}
