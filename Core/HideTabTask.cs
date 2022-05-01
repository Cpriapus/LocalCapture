namespace LocalCapture.Core
{
    internal class HideTabTask
    {
        public static CreateParams HideTabTaskAction(CreateParams basecp)
        {
            const int WS_EX_APPWINDOW = 0x40000;
            const int WS_EX_TOOLWINDOW = 0x80;
            CreateParams cp = basecp;
            cp.ExStyle &= ~WS_EX_APPWINDOW;    // 不显示在TaskBar
            cp.ExStyle |= WS_EX_TOOLWINDOW;      // 不显示在Alt+Tab
            return cp;
        }
    }
}
