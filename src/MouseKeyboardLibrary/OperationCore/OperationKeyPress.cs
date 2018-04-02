using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OperationCore
{
    /// <summary>
    /// 操作模型：键盘录入内容
    /// </summary>
    public class OperationKeyPress : OperationBase
    {
        /// <summary>
        /// 待录入的内容
        /// </summary>
        public string KeyPressContent { get; set; }

        public OperationKeyPress()
            : base()
        {

        }
        public OperationKeyPress(string content)
        {
            this.KeyPressContent = content;
        }

        public override void ExecuteOperate()
        {
            base.ExecuteOperate();

            //http://www.cnblogs.com/sydeveloper/archive/2013/02/25/2932571.html
            //异步模拟按键(不阻塞UI)
            //SendKeys.Send(this.txtSimulateContentBefore.Text);
            // 同步模拟按键(会阻塞UI直到对方处理完消息后返回)

            SendKeys.SendWait(this.KeyPressContent);
            if (this.IsStay) Thread.Sleep(this.StayInterval);

        }

        public override string ToString()
        {
            return string.Format("操作类型【键盘录入】\t内容【{0}】", this.KeyPressContent);
        }
    }
}
