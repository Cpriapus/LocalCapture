namespace LocalCapture.Core
{
    internal class Capture
    {
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        public Capture()
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        {
            ScreenHeight = Screen.PrimaryScreen.Bounds.Height;
            ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
        }
        public Bitmap FullScreen { get; set; }
        public Bitmap CaptureScreen { get; set; }
        public int ScreenHeight { get; set; }
        public int ScreenWidth { get; set; }
        public Bitmap ShowCanvans()
        {
            FullScreen = new Bitmap(ScreenWidth, ScreenHeight);
            Graphics g = Graphics.FromImage(FullScreen);
            g.CopyFromScreen(0, 0, 0, 0, FullScreen.Size);
            g.Dispose();
            //FullScreen.Save(@"\\10.12.15.215\vdi_homedir\k2115617\Desktop\待办\cap.png", System.Drawing.Imaging.ImageFormat.Png);
            return FullScreen;
        }
    }
}
