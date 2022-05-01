using LocalCapture.Core;
using System.Runtime.InteropServices;

namespace LocalCapture
{
    public partial class PinForm : Form
    {
        int dHeight = 0;
        int dWidth = 0;
        bool Zoom = false;
        Point Position;
        public PinForm(Point Position, Bitmap ShowCapture)
        {
            InitializeComponent();
            dWidth = ShowCapture.Width;
            dHeight = ShowCapture.Height;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.Position = Position;
            this.Size = new Size(dWidth, dHeight);
            this.BackgroundImage = ShowCapture;
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            this.Show();
        }
        private void SelectForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.Close();
            }
        }

        private void PinForm_MouseDown(object sender, MouseEventArgs e)
        {
            //为当前应用程序释放鼠标捕获
            MoveWindow.ReleaseCapture();
            //发送消息 让系统误以为在标题栏上按下鼠标
            MoveWindow.SendMessage((IntPtr)this.Handle, MoveWindow.VM_NCLBUTTONDOWN, MoveWindow.HTCAPTION, 0);
        }

        private void PinForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                Console.WriteLine("MouseButtons.Middle");
                if (Zoom)
                {
                    this.Size = new Size(dWidth, dHeight);
                    Zoom = false;
                }
                else
                {
                    this.Size = new Size(50, 50);
                    Zoom = true;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
            }
        }
        //WinForm隐藏TaskTab
        protected override CreateParams CreateParams
        {
            get
            {
                return HideTabTask.HideTabTaskAction(base.CreateParams);
            }
        }
        //窗体阴影
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
    }
}
