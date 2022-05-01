using LocalCapture.Core;

namespace LocalCapture
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.Hide();
            //注册热键Ctrl+Alt+A，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.CtrlAlt, Keys.A);
        }
        //WinForm隐藏TaskTab
        protected override CreateParams CreateParams
        {
            get
            {
                return HideTabTask.HideTabTaskAction(base.CreateParams);
            }
        }
        //重写接收Windows消息
        protected override void WndProc(ref Message m)
        {

            //按快捷键
            switch (m.Msg)
            {
                case HotKey.WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:  //按下的是Ctrl+Alt+A
                            //此处填写快捷键响应代码
                            if (!Program.IsCapturing)
                            {
                                Program.IsCapturing = true;
                                Console.WriteLine("0000");
                                new SelectForm(new Capture().ShowCanvans());
                            }
                            break;
                    }
                    break;
                case MoveWindow.WM_EXITSIZEMOVE:
                    MoveWindow.ReLocationWindow(this.Location, this);
                    break;
            }
            //运行原函数
            base.WndProc(ref m);
        }

        //无边框程序移动窗体
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            //为当前应用程序释放鼠标捕获
            MoveWindow.ReleaseCapture();
            //发送消息 让系统误以为在标题栏上按下鼠标
            MoveWindow.SendMessage((IntPtr)this.Handle, MoveWindow.VM_NCLBUTTONDOWN, MoveWindow.HTCAPTION, 0);
        }
        //退出窗体
        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定
            HotKey.UnregisterHotKey(Handle, 100);
            Application.Exit();
        }
    }
}