using MouseKeyboardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OperationCore
{
    /// <summary>
    /// 操作模型：鼠标移动到指定位置并执行单击事件
    /// </summary>
    public class OperationMouseMoveClick : OperationBase
    {
        public int CursorX { get; set; }

        public int CursorY { get; set; }

        public OperationMouseMoveClick()
        {

        }
        public OperationMouseMoveClick(int dx, int dy)
        {
            this.CursorX = dx;
            this.CursorY = dy;
        }

        public override void ExecuteOperate()
        {
            base.ExecuteOperate();
            MouseHandler.SetCursorPos(this.CursorX, this.CursorY);
            MouseHandler.MouseLeftDown(this.CursorX, this.CursorY);
            MouseHandler.MouseLeftUp(this.CursorX, this.CursorY);

            if (this.IsStay) Thread.Sleep(this.StayInterval);
        }

        public override string ToString()
        {
            return string.Format("操作类型【鼠标左键单击】\t位置【X:{0} Y:{1}】", this.CursorX, this.CursorY);
        }
    }
}
