namespace DevelopRecordPlatform.Client.Common
{
    partial class frmSetFlightAlarmTool
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstFlightInfoList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetFlightInfoList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstFlightInfoList
            // 
            this.lstFlightInfoList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFlightInfoList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstFlightInfoList.FormattingEnabled = true;
            this.lstFlightInfoList.ItemHeight = 16;
            this.lstFlightInfoList.Location = new System.Drawing.Point(12, 31);
            this.lstFlightInfoList.Name = "lstFlightInfoList";
            this.lstFlightInfoList.Size = new System.Drawing.Size(132, 388);
            this.lstFlightInfoList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "当前航班列表：";
            // 
            // btnGetFlightInfoList
            // 
            this.btnGetFlightInfoList.Location = new System.Drawing.Point(164, 40);
            this.btnGetFlightInfoList.Name = "btnGetFlightInfoList";
            this.btnGetFlightInfoList.Size = new System.Drawing.Size(115, 23);
            this.btnGetFlightInfoList.TabIndex = 2;
            this.btnGetFlightInfoList.Text = "刷新航班列表";
            this.btnGetFlightInfoList.UseVisualStyleBackColor = true;
            this.btnGetFlightInfoList.Click += new System.EventHandler(this.btnGetFlightInfoList_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(164, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "取消所有告警";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(164, 169);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "新增一个告警";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmSetFlightAlarmTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 436);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnGetFlightInfoList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstFlightInfoList);
            this.Name = "frmSetFlightAlarmTool";
            this.Text = "工具-设置航班告警";
            this.Load += new System.EventHandler(this.frmSetFlightAlarmTool_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstFlightInfoList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetFlightInfoList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}