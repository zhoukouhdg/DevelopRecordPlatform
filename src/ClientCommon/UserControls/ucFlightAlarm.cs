using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevelopRecordPlatform.Model;

namespace DevelopRecordPlatform.Client.Common.UserControls
{
    public partial class ucFlightAlarm : UserControl
    {
        #region members
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        /// <summary>
        /// 闪烁间隔时间(默认为500毫秒)
        /// </summary>
        public int Interval = 500;
        /// <summary>
        /// 闪烁的颜色(默认为Red)
        /// </summary>
        private System.Drawing.Color AlertColorRed = System.Drawing.Color.Red;
        /// <summary>
        /// 当前是否有闪烁告警标记，如果没有则停止Timer
        /// </summary>
        private bool isHaveAlarmBlink = false;

        /// <summary>
        /// 当前航班告警标签相关信息缓存
        /// </summary>
        private AlertLabelTag AlarmTagCache = null;

        /// <summary>
        /// 当前航班告警详情
        /// </summary>
        public FlightAlarmDetail AlarmDetail { get; private set; }

        #endregion

        #region ctor
        public ucFlightAlarm(FlightAlarmDetail alarmDetail)
        {
            InitializeComponent();

            //初始化闪烁定时器
            _timer.Interval = this.Interval;
            this._timer.Tick += new EventHandler(_timer_Tick);

            //AlarmTagCache = new AlertLabelTag();

            this.BackColor = System.Drawing.Color.FromArgb(224, 230, 241);
            this.lbFlightFullNo.Text = alarmDetail.FlightNo;
            //this.lbFuelAlert.Text = alarmDetail.AlarmTotal.ToString();
            //this.AlarmDetail = alarmDetail;

            UpdateAlarm(alarmDetail);
        }
        #endregion

        #region 告警闪烁

        /// <summary>
        /// 开始
        /// </summary>
        void StartAlert()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(StartAlert));
                return;
            }
            _timer.Interval = this.Interval;
            if (!this._timer.Enabled)
            {
                _timer.Start();
            }
        }
        void _timer_Tick(object sender, EventArgs e)
        {
            if (AlarmTagCache != null && AlarmTagCache.IsAlarm)
            {
                if (AlarmTagCache.BlinkState)
                {
                    this.lbFuelAlert.BackColor = AlertColorRed;
                    this.lbFuelAlert.ForeColor = Color.Yellow;
                    AlarmTagCache.BlinkState = !AlarmTagCache.BlinkState;
                }
                else
                {
                    this.lbFuelAlert.ForeColor = AlertColorRed;
                    this.lbFuelAlert.BackColor = Color.Yellow;
                    AlarmTagCache.BlinkState = !AlarmTagCache.BlinkState;
                }
                isHaveAlarmBlink = true;
            }
            if (!isHaveAlarmBlink)
                StopAlert();
        }
        /// <summary>
        /// 停止
        /// </summary>
        void StopAlert()
        {
            _timer.Stop();
        }
        #endregion

        #region 设置告警标签
        /// <summary>
        /// 设置告警标签内容
        /// </summary>
        /// <param name="total"></param>
        private void SetAlarmLabelValue(FlightAlarmTypeTotal total)
        {
            if (this.lbFuelAlert.InvokeRequired)
            {
                this.lbFuelAlert.Invoke(new Action<FlightAlarmTypeTotal>(SetAlarmLabelValue), total);
            }
            else
            {
                this.lbFuelAlert.Text = total.ToString();
                var _isAlarm = total.Count > 0 && total.Unconfirmed > 0;

                //缓存告警信息              
                if (AlarmTagCache == null)
                {
                    AlarmTagCache = new AlertLabelTag
                    {
                        BlinkState = false,
                        IsAlarm = _isAlarm,
                        AlarmTotal = total
                    };
                }
                else
                {
                    AlarmTagCache.AlarmTotal = total;
                    AlarmTagCache.IsAlarm = _isAlarm;
                }
            }
        }
        /// <summary>
        /// 设置告警标签样式
        /// </summary>
        /// <param name="isAlarm">是否是告警状态</param>
        private void SetAlarmLabelStyle(bool isAlarm)
        {
            if (this.lbFuelAlert.InvokeRequired)
            {
                this.lbFuelAlert.Invoke(new Action<bool>(SetAlarmLabelStyle), isAlarm);
            }
            else
            {
                //有告警：  红色背景、黄色字体、背景闪烁
                //无告警：  绿色背景、白色字体、不闪烁
                if (isAlarm)
                {
                    this.lbFuelAlert.ForeColor = Color.Yellow;
                    this.lbFuelAlert.BackColor = Color.Red;
                }
                else
                {
                    this.lbFuelAlert.ForeColor = Color.WhiteSmoke;
                    this.lbFuelAlert.BackColor = Color.Green;
                }
            }
        }
        #endregion

        /// <summary>
        /// 更新当前航班信息
        /// </summary>
        /// <param name="alarmDetail"></param>
        public void UpdateAlarm(FlightAlarmDetail alarmDetail)
        {
            if (alarmDetail != null)
            {
                SetAlarmLabelValue(alarmDetail.AlarmTotal);
                StartAlert();
            }
            this.AlarmDetail = alarmDetail;
        }
    }

    class AlertLabelTag
    {
        /// <summary>
        /// 是否闪烁
        /// </summary>
        public bool BlinkState { get; set; }

        /// <summary>
        /// 是否处于告警状态
        /// </summary>
        public bool IsAlarm { get; set; }

        public FlightAlarmTypeTotal AlarmTotal { get; set; }
    }
}
