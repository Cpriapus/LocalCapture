using LocalCapture.Core;

namespace LocalCapture
{
    public partial class SelectForm : Form
    {
        public Bitmap? OrigBitMap = null;
        public Bitmap? BlackBitMap = null;
        public Size ScreenSize;

        public SelectForm(Bitmap CaptureScreen)
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
            this.UpdateStyles();
            ScreenSize = new Size(CaptureScreen.Width, CaptureScreen.Height);
            this.Size = ScreenSize;
            //Graphics g = this.CreateGraphics();
            //g.DrawImage(CaptureScreen, 0, 0);
            BlackBitMap = PicEffect.MemoryLowPix(CaptureScreen);
            this.BackgroundImage = BlackBitMap;
            this.Cursor = Cursors.Cross;
            this.Show();
            this.OrigBitMap = CaptureScreen;
            this.TopMost = false;
        }

        //WinForm隐藏TaskTab
        protected override CreateParams CreateParams
        {
            get
            {
                return HideTabTask.HideTabTaskAction(base.CreateParams);
            }
        }

        bool CaptureStart = false;
        Point CaptureStartPoint;
        Rectangle rec;
        private void SelectForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!CaptureStart)
            {
                CaptureStart = true;
                Console.WriteLine("CaptureStart");
                CaptureStartPoint.X = e.X;
                CaptureStartPoint.Y = e.Y;
            }
        }

        private void SelectForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (CaptureStart)
            {
                int x = e.X;
                int y = e.Y;
                if (x == 0) x += 1;
                if (y == 0) y += 1;
                CaptureStart = false;
                Console.WriteLine("CaptureEnd");
                Point LeftTopPoint = new Point();
                Point RightBottomPoint = new Point();
                if (x == CaptureStartPoint.X && y == CaptureStartPoint.Y)
                {
                    return;
                }
                if (x - CaptureStartPoint.X >= 0 && y - CaptureStartPoint.Y >= 0)
                {//右下
                    LeftTopPoint = CaptureStartPoint;
                    RightBottomPoint.X = x;
                    RightBottomPoint.Y = y;
                }
                else if (x - CaptureStartPoint.X < 0 && y - CaptureStartPoint.Y >= 0)
                {//左下
                    LeftTopPoint.X = x;
                    LeftTopPoint.Y = CaptureStartPoint.Y;
                    RightBottomPoint.X = CaptureStartPoint.X;
                    RightBottomPoint.Y = y;
                }
                else if (x - CaptureStartPoint.X < 0 && y - CaptureStartPoint.Y < 0)
                {//左上
                    LeftTopPoint.X = x;
                    LeftTopPoint.Y = y;
                    RightBottomPoint.X = CaptureStartPoint.X;
                    RightBottomPoint.Y = CaptureStartPoint.Y;
                }
                else
                {//右上
                    LeftTopPoint.X = CaptureStartPoint.X;
                    LeftTopPoint.Y = y;
                    RightBottomPoint.X = x;
                    RightBottomPoint.Y = CaptureStartPoint.Y;
                }
                Rectangle rec2 = new Rectangle(LeftTopPoint, new Size(RightBottomPoint.X - LeftTopPoint.X, RightBottomPoint.Y - LeftTopPoint.Y));
                Bitmap? b2 = new Bitmap(rec2.Width, rec2.Height);

                using (Graphics g2 = Graphics.FromImage(b2))
                {
#pragma warning disable CS8604 // 引用类型参数可能为 null。
                    g2.DrawImage(OrigBitMap, 0, 0, new RectangleF(LeftTopPoint, new Size(RightBottomPoint.X - LeftTopPoint.X, RightBottomPoint.Y - LeftTopPoint.Y)), GraphicsUnit.Pixel);
#pragma warning restore CS8604 // 引用类型参数可能为 null。
                    Clipboard.SetData(DataFormats.Bitmap, b2.Clone() as Bitmap);
                    if (RightBottomPoint.X - LeftTopPoint.X - 3 > 0 && RightBottomPoint.Y - LeftTopPoint.Y - 3 > 0)
                    {
                        g2.DrawRectangle(Pens.Black, 0, 0, (RightBottomPoint.X - LeftTopPoint.X - 1), (RightBottomPoint.Y - LeftTopPoint.Y - 1));
                        g2.DrawRectangle(Pens.White, 1, 1, (RightBottomPoint.X - LeftTopPoint.X - 3), (RightBottomPoint.Y - LeftTopPoint.Y - 3));
                    }
                    Program.IsCapturing = false;
#pragma warning disable CS8604 // 引用类型参数可能为 null。
                    new PinForm(LeftTopPoint, b2.Clone() as Bitmap);
#pragma warning restore CS8604 // 引用类型参数可能为 null。
                }
                b2.Dispose();
                this.Close();
            }
        }

        private void SelectForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (CaptureStart)
            {
#pragma warning disable CS8602 // 解引用可能出现空引用。
                Bitmap? b = BlackBitMap.Clone() as Bitmap;
#pragma warning restore CS8602 // 解引用可能出现空引用。

#pragma warning disable CS8604 // 引用类型参数可能为 null。
                using (Graphics g = Graphics.FromImage(b))
                {
                    Pen p = new Pen(Color.White, 2);
                    rec = new Rectangle(CaptureStartPoint, new Size(e.X - CaptureStartPoint.X, e.Y - CaptureStartPoint.Y));
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawImage(BlackBitMap, 0, 0);
                    g.DrawRectangle(p, rec);
#pragma warning disable CS8604 // 引用类型参数可能为 null。
                    g.DrawImage(OrigBitMap, rec, rec, GraphicsUnit.Pixel);
#pragma warning restore CS8604 // 引用类型参数可能为 null。
                }
#pragma warning restore CS8604 // 引用类型参数可能为 null。
                using (Graphics g1 = this.CreateGraphics())
                {
                    g1.DrawImage(b, 0, 0);
                }
            }
        }

        private void SelectForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
            }
        }
        private void SelectForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.Close();
            }
        }
    }
}
