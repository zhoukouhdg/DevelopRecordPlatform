namespace OneKeyLogin
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtCursorPosX = new System.Windows.Forms.TextBox();
            this.txtCursorPosY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetCursorPos = new System.Windows.Forms.Button();
            this.btnSimulateKeyBord = new System.Windows.Forms.Button();
            this.txtSimulateContentBefore = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSimulateContentEnd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCursorPosX
            // 
            this.txtCursorPosX.Location = new System.Drawing.Point(51, 21);
            this.txtCursorPosX.Name = "txtCursorPosX";
            this.txtCursorPosX.Size = new System.Drawing.Size(34, 21);
            this.txtCursorPosX.TabIndex = 0;
            this.txtCursorPosX.Text = "200";
            // 
            // txtCursorPosY
            // 
            this.txtCursorPosY.Location = new System.Drawing.Point(109, 21);
            this.txtCursorPosY.Name = "txtCursorPosY";
            this.txtCursorPosY.Size = new System.Drawing.Size(34, 21);
            this.txtCursorPosY.TabIndex = 1;
            this.txtCursorPosY.Text = "160";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y:";
            // 
            // btnSetCursorPos
            // 
            this.btnSetCursorPos.Location = new System.Drawing.Point(167, 21);
            this.btnSetCursorPos.Name = "btnSetCursorPos";
            this.btnSetCursorPos.Size = new System.Drawing.Size(140, 23);
            this.btnSetCursorPos.TabIndex = 4;
            this.btnSetCursorPos.Text = "移动鼠标并左键单击";
            this.btnSetCursorPos.UseVisualStyleBackColor = true;
            this.btnSetCursorPos.Click += new System.EventHandler(this.btnSetCursorPos_Click);
            // 
            // btnSimulateKeyBord
            // 
            this.btnSimulateKeyBord.Location = new System.Drawing.Point(262, 72);
            this.btnSimulateKeyBord.Name = "btnSimulateKeyBord";
            this.btnSimulateKeyBord.Size = new System.Drawing.Size(135, 54);
            this.btnSimulateKeyBord.TabIndex = 5;
            this.btnSimulateKeyBord.Text = "模拟键盘输入\r\n\r\n并按下回车键弹出内容";
            this.btnSimulateKeyBord.UseVisualStyleBackColor = true;
            this.btnSimulateKeyBord.Click += new System.EventHandler(this.btnSimulateKeyBord_Click);
            // 
            // txtSimulateContentBefore
            // 
            this.txtSimulateContentBefore.Location = new System.Drawing.Point(133, 69);
            this.txtSimulateContentBefore.Name = "txtSimulateContentBefore";
            this.txtSimulateContentBefore.Size = new System.Drawing.Size(112, 21);
            this.txtSimulateContentBefore.TabIndex = 6;
            this.txtSimulateContentBefore.Text = "welsd";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "待模拟输入的内容";
            // 
            // txtSimulateContentEnd
            // 
            this.txtSimulateContentEnd.Location = new System.Drawing.Point(133, 105);
            this.txtSimulateContentEnd.Name = "txtSimulateContentEnd";
            this.txtSimulateContentEnd.Size = new System.Drawing.Size(112, 21);
            this.txtSimulateContentEnd.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "模拟输入后的内容";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 480);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSimulateContentEnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSimulateContentBefore);
            this.Controls.Add(this.btnSimulateKeyBord);
            this.Controls.Add(this.btnSetCursorPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCursorPosY);
            this.Controls.Add(this.txtCursorPosX);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCursorPosX;
        private System.Windows.Forms.TextBox txtCursorPosY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetCursorPos;
        private System.Windows.Forms.Button btnSimulateKeyBord;
        private System.Windows.Forms.TextBox txtSimulateContentBefore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSimulateContentEnd;
        private System.Windows.Forms.Label label4;
    }
}

