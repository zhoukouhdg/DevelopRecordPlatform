namespace DevelopRecordPlatform.Client.Common.UserControls
{
    partial class ucFlightAlarm
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbFlightFullNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbFuelAlert = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbFlightFullNo
            // 
            this.lbFlightFullNo.BackColor = System.Drawing.Color.Transparent;
            this.lbFlightFullNo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFlightFullNo.Location = new System.Drawing.Point(58, 13);
            this.lbFlightFullNo.Name = "lbFlightFullNo";
            this.lbFlightFullNo.Size = new System.Drawing.Size(70, 19);
            this.lbFlightFullNo.TabIndex = 70;
            this.lbFlightFullNo.Text = "MU9584";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 69;
            this.label1.Text = "航班：";
            // 
            // lbFuelAlert
            // 
            this.lbFuelAlert.BackColor = System.Drawing.Color.Green;
            this.lbFuelAlert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbFuelAlert.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFuelAlert.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbFuelAlert.Location = new System.Drawing.Point(323, 13);
            this.lbFuelAlert.Name = "lbFuelAlert";
            this.lbFuelAlert.Size = new System.Drawing.Size(41, 22);
            this.lbFuelAlert.TabIndex = 72;
            this.lbFuelAlert.Text = "0/0";
            this.lbFuelAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(266, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 71;
            this.label5.Text = "告警：";
            // 
            // ucFlightAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbFuelAlert);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbFlightFullNo);
            this.Controls.Add(this.label1);
            this.Name = "ucFlightAlarm";
            this.Size = new System.Drawing.Size(380, 47);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbFlightFullNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbFuelAlert;
        private System.Windows.Forms.Label label5;
    }
}
