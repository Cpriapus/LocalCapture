using System.Runtime.InteropServices;

namespace LocalCapture.Core
{
    internal class MoveWindow
    {
        //无边框程序移动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int VM_NCLBUTTONDOWN = 0XA1;//定义鼠标左键按下
        public const int HTCAPTION = 2;

        public static int ScreenHight;
        public static int ScreenWidth;
        public static int Self_ScreenHight;
        public static int Self_ScreenWidth;
        public const int WM_EXITSIZEMOVE = 0x0232;
        public static void ReLocationWindow(Point Location, Form from)
        {
            if (Location.X < 0)
            {
                from.SetDesktopLocation(0, Location.Y);
            }
            if (Location.X + Self_ScreenWidth > ScreenWidth)
            {
                from.SetDesktopLocation(ScreenWidth - Self_ScreenWidth, Location.Y);
            }
            if (ScreenHight - Location.Y < Self_ScreenHight)
            {
                from.SetDesktopLocation(Location.X, ScreenHight - Self_ScreenHight);
            }
        }
        public static void LoadMoveWindow(Form form)
        {
            //获取显示器屏幕的大小,不包括任务栏、停靠窗口
            ScreenHight = Screen.PrimaryScreen.WorkingArea.Height;
            ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            //获取当前活动窗口高度跟宽度
            Self_ScreenHight = form.Size.Height;
            Self_ScreenWidth = form.Size.Width;
            form.SetDesktopLocation(ScreenWidth / 2 - Self_ScreenWidth / 2, 0);
        }
    }
}
