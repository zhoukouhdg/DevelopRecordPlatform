using MouseKeyboardLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OperationCore;
using OneKeyLogin.Configuration;

namespace OneKeyLogin
{
    public partial class frmLearnOperation : Form
    {
        private bool IsWork = false;
        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyboardHook = new KeyboardHook();
        int ddx = 213, ddy = 159;

        /// <summary>
        /// 操作轨迹队列
        /// </summary>
        Queue<OperationBase> operationQueue = new Queue<OperationBase>();

        public frmLearnOperation()
        {
            InitializeComponent();
        }

        private void frmLearnOperation_Load(object sender, EventArgs e)
        {
            var accounts = GameAccountConfiguration.GetGameAccountList();
            var operations = GameAccountConfiguration.GetOperationByConfig();
            foreach (var op in operations)
            {
                this.listBox2.Items.Add(op.ToString());
            }


            keyboardHook.KeyPress += new KeyPressEventHandler(keyboardHook_KeyPress);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);
            mouseHook.Click += new EventHandler(mouseHook_MouseClick);

        }
        #region events
        void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {

            RecordLearnLog($"KeyPress（{e.KeyChar}）");
            operationQueue.Enqueue(new OperationKeyPress(KeybordHandler.ConvertKeyChar(e.KeyChar)));

        }
        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {
            //RecordLearnLog($"MouseUp（X:{e.Location.X}Y:{e.Location.Y}）");
        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {
            //RecordLearnLog($"MouseDown（X:{e.Location.X}Y:{e.Location.Y}）");
        }
        void mouseHook_MouseClick(object sender, EventArgs e)
        {
            RecordLearnLog($"MouseClick（X:{MouseSimulator.X} Y:{MouseSimulator.Y}）");
            operationQueue.Enqueue(new OperationMouseMoveClick(MouseSimulator.X, MouseSimulator.Y));
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsWork)
            {
                mouseHook.Start();
                keyboardHook.Start();
                ddx = MouseSimulator.X;
                ddy = MouseSimulator.Y;
                operationQueue.Clear();
                this.listBox1.Items.Clear();
            }
            else
            {
                mouseHook.Stop();
                keyboardHook.Stop();
            }
            this.button1.Text = IsWork ? "start learn" : "stop learn";
            IsWork = !IsWork;
        }

        private void RecordLearnLog(string msg)
        {
            this.listBox1.Items.Add(msg);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                MessageBox.Show(string.Format("用户名：{0}\t密码：{1}", this.textBox1.Text, this.textBox2.Text));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            int c = 0;
            foreach (var op in operationQueue)
            {
                if (c != operationQueue.Count - 1)
                {
                    this.listBox2.Items.Add(op.ToString());
                    op.ExecuteOperate();
                }
                c++;
            }
            //operationQueue.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("用户名：{0}\t密码：{1}", this.textBox1.Text, this.textBox2.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

            //MouseHandler.SetCursorPos(ddx, ddy);
            //MouseHandler.MouseLeftDown(ddx, ddy);
            //MouseHandler.MouseLeftUp(ddx, ddy);

            foreach (var op in operationQueue)
            {
                this.listBox2.Items.Add(op.ToString());
            }
        }
    }
}
