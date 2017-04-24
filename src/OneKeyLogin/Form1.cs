using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OneKeyLogin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSetCursorPos_Click(object sender, EventArgs e)
        {
            int x = int.Parse(this.txtCursorPosX.Text.Trim());
            int y = int.Parse(this.txtCursorPosY.Text.Trim());

            MouseHandler.SetCursorPos(x, y);
            MouseHandler.MouseClick();
        }

        private void btnSimulateKeyBord_Click(object sender, EventArgs e)
        {
            this.txtSimulateContentEnd.Focus();

            //异步模拟按键(不阻塞UI)
            //SendKeys.Send(this.txtSimulateContentBefore.Text);
            // 同步模拟按键(会阻塞UI直到对方处理完消息后返回)
            SendKeys.SendWait(this.txtSimulateContentBefore.Text);

            MessageBox.Show(this.txtSimulateContentEnd.Text);
            // var keys = this.txtSimulateContentBefore.Text.ToCharArray();         

        }
    }
}
