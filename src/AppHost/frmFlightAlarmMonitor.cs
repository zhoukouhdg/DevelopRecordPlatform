using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevelopRecordPlatform.Model;
using DevelopRecordPlatform.BusinessLogical.BLL;
using DevelopRecordPlatform.Client.Common.UserControls;
using DevelopRecordPlatform.Client.Common;

namespace AppHost
{
    public partial class frmFlightAlarmMonitor : Form
    {
        /// <summary>
        /// 已添加的组件缓存
        /// </summary>
        private List<ucFlightAlarm> currentAlarmControl = null;

        public frmFlightAlarmMonitor()
        {
            InitializeComponent();
        }

        private void frmFlightAlarmMonitor_Load(object sender, EventArgs e)
        {
            currentAlarmControl = new List<ucFlightAlarm>();

            this.timer1.Interval = 5000;
            this.timer1.Enabled = true;
            this.timer1.Start();

            GetFlightAlertListAsync();

            //打开“设置航班告警”工具窗体
            new frmSetFlightAlarmTool().Show();
        }

        private void GetFlightAlertListAsync()
        {
            List<FlightAlarmDetail> alarmList = null;

            new Action(() => alarmList = FlightAlertManager.Instance.GetFlightAlertList()).BeginInvoke(new AsyncCallback((ar) =>
             {
                 if (alarmList != null && alarmList.Count > 0)
                 {
                    //已有航班告警
                    var preFlightIdList = currentAlarmControl.Select(p => p.AlarmDetail.FlightInfoID).ToList();
                    //新获取航班告警
                    var curList = alarmList.Select(p => p.FlightInfoID).ToList();
                    //旧->新 求差集得出“待删除”航班告警
                    var delList = preFlightIdList.Except(curList).ToList();
                    //新->旧 求差集得出“待新增”航班告警
                    var addList = curList.Except(preFlightIdList).ToList();

                     this.Invoke(new Action(() =>
                     {

                        //移除组件
                        foreach (var id in delList)
                         {
                             var c = currentAlarmControl.Where(p => p.AlarmDetail.FlightInfoID == id).FirstOrDefault();
                             currentAlarmControl.Remove(c);
                             this.flowLayoutPanel1.Controls.Remove(c);
                         }

                        //以“是否有未确认告警”分组，按航班号排序
                        var l1 = alarmList.Where(p => p.IsAlarm).OrderBy(p => p.FlightNo);
                         var l2 = alarmList.Where(p => !p.IsAlarm).OrderBy(p => p.FlightNo);

                         var rLst = l1.Union(l2);
                        //更新
                        foreach (var detail in rLst.Where(p => !addList.Contains(p.FlightInfoID)))
                         {
                             var _id = detail.FlightInfoID;
                             var c = currentAlarmControl.Where(p => p.AlarmDetail.FlightInfoID == _id).FirstOrDefault();
                             if (c != null) c.UpdateAlarm(detail);

                         }
                        //新增
                        foreach (var detail in rLst.Where(p => addList.Contains(p.FlightInfoID)))
                         {
                             var c = new ucFlightAlarm(detail);
                             this.flowLayoutPanel1.Controls.Add(c);
                             this.currentAlarmControl.Add(c);

                            //新增航班告警，此时如果有未确认，则将其上移
                            if (this.flowLayoutPanel1.Controls.Count > 0)
                             {

                             }
                         }
                     }));
                 }

             }), null);

            // actionGetFlightAlertList.EndInvoke(asyncResult);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            GetFlightAlertListAsync();
        }



        private void AlarmPluginContainer_Order()
        {
            /*
               “航班告警监控”组件排序移动规则：
                    1、以“是否有报警”（有未确认的告警）分组，按照“航班号”排序
                    2、当有新增航班且有未确认告警时，该航班组件实现上移操作，此时需满足规则1。
                    3、当现有航班告警“未确认告警”由 有->无 变化时，该航班组件实现下移操作，此时需满足规则1。
            */

            var c = this.flowLayoutPanel1.Controls[2];
            this.flowLayoutPanel1.Controls.SetChildIndex(c, 0);
        }
    }


}
