using LocalCapture.Core;

namespace LocalCapture
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.Hide();
            //ע���ȼ�Ctrl+Alt+A��Id��Ϊ100��HotKey.KeyModifiers.ShiftҲ����ֱ��ʹ������4����ʾ��
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.CtrlAlt, Keys.A);
        }
        //WinForm����TaskTab
        protected override CreateParams CreateParams
        {
            get
            {
                return HideTabTask.HideTabTaskAction(base.CreateParams);
            }
        }
        //��д����Windows��Ϣ
        protected override void WndProc(ref Message m)
        {

            //����ݼ�
            switch (m.Msg)
            {
                case HotKey.WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:  //���µ���Ctrl+Alt+A
                            //�˴���д��ݼ���Ӧ����
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
            //����ԭ����
            base.WndProc(ref m);
        }

        //�ޱ߿�����ƶ�����
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            //Ϊ��ǰӦ�ó����ͷ���겶��
            MoveWindow.ReleaseCapture();
            //������Ϣ ��ϵͳ����Ϊ�ڱ������ϰ������
            MoveWindow.SendMessage((IntPtr)this.Handle, MoveWindow.VM_NCLBUTTONDOWN, MoveWindow.HTCAPTION, 0);
        }
        //�˳�����
        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ע��Id��Ϊ100���ȼ��趨
            HotKey.UnregisterHotKey(Handle, 100);
            Application.Exit();
        }
    }
}