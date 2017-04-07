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
                 if (alarmList == null) return;

                 if (alarmList.Count > 0)
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

                         foreach (var item in alarmList)
                         {
                             //新增
                             if (addList.Contains(item.FlightInfoID))
                             {
                                 var c = new ucFlightAlarm(item);
                                 c.OrderAlarmPluginEvent += OrderAlarmPluginEvent;
                                 this.flowLayoutPanel1.Controls.Add(c);
                                 this.currentAlarmControl.Add(c);
                             }
                             else //更新
                             {
                                 var _id = item.FlightInfoID;
                                 var c = currentAlarmControl.Where(p => p.AlarmDetail.FlightInfoID == item.FlightInfoID).FirstOrDefault();
                                 if (c != null) c.UpdateAlarm(item);
                             }
                         }
                         this.OrderAlarmPluginContainer();
                     }));
                 }
                 else
                 {
                     //清空容器和组件缓存
                     if (this.currentAlarmControl.Count > 0) this.currentAlarmControl.Clear();
                     if (this.flowLayoutPanel1.Controls.Count > 0) this.Invoke(new Action(() => this.flowLayoutPanel1.Controls.Clear()));
                 }

             }), null);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            GetFlightAlertListAsync();
        }


        #region “航班告警”组件排序
        /// <summary>
        /// 容器中的组件进行排序
        /// </summary>

        private void OrderAlarmPluginContainer()
        {
            /*
               “航班告警监控”组件排序移动规则：
                    1、以“是否有报警”（有未确认的告警）分组，按照“航班号”排序
                    2、当有新增航班且有未确认告警时，该航班组件实现上移操作，此时需满足规则1。
                    3、当现有航班告警“未确认告警”由 有->无 变化时，该航班组件实现下移操作，此时需满足规则1。
            */
            var preOrderIndex = GetPreOrderIndex();
            var newOrderIndex = GetNewOrderIndex();

            //新的排序信息与旧的如果不相等增进行排序
            if (!EqualsOrderInfoList(preOrderIndex, newOrderIndex))
            {
                foreach (var o in newOrderIndex)
                {
                    var c = this.currentAlarmControl.Where(p => p.AlarmDetail.FlightInfoID == o.FlightInfoId).FirstOrDefault();
                    if (c != null && c.OrderAccord.Index != o.Index)
                    {
                        this.flowLayoutPanel1.Controls.SetChildIndex(c, o.Index);
                    }
                }
            }

        }

        /// <summary>
        /// 事件-“航班告警”组件触发排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderAlarmPluginEvent(object sender, EventArgs e)
        {
            OrderAlarmPluginContainer();
        }

        /// <summary>
        /// 判断两个排序列表是否相等
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <param name="isDistinct"></param>
        /// <returns></returns>
        private bool EqualsOrderInfoList(IEnumerable<FlightAlarmPluginOrderInfo> list1, IEnumerable<FlightAlarmPluginOrderInfo> list2, bool isDistinct = false)
        {
            if (list1 == null || list2 == null) return false;
            if (list1.Count() != list2.Count()) return false;

            var _list1 = isDistinct ? list1.Distinct(new FlightAlarmPluginOrderInfoCompare()) : list1;
            var _list2 = isDistinct ? list2.Distinct(new FlightAlarmPluginOrderInfoCompare()) : list2;
            var lst1 = _list1.Except(_list2, new FlightAlarmPluginOrderInfoCompare());
            var lst2 = _list2.Except(_list1, new FlightAlarmPluginOrderInfoCompare());

            return lst1.Count() <= 0 && lst2.Count() <= 0;
        }

        /// <summary>
        /// 获取新数据更新前列表的排序索引
        /// </summary>
        /// <returns></returns>
        private List<FlightAlarmPluginOrderInfo> GetPreOrderIndex()
        {
            foreach (var c in this.currentAlarmControl)
            {
                if (c.OrderAccord == null)
                {
                    c.OrderAccord = new FlightAlarmPluginOrderInfo
                    {
                        FlightInfoId = c.AlarmDetail.FlightInfoID,
                        FlightNo = c.AlarmDetail.FlightNo,
                        HasAlarm = c.AlarmDetail.IsAlarm,
                        Index = this.flowLayoutPanel1.Controls.GetChildIndex(c, false)
                    };
                }
                else {
                    c.OrderAccord.Index = this.flowLayoutPanel1.Controls.GetChildIndex(c, false);
                    c.OrderAccord.HasAlarm = c.AlarmDetail.IsAlarm;
                }
            }
            return this.currentAlarmControl.Select(p => p.OrderAccord).ToList();
        }

        /// <summary>
        /// 获取新数据列表的排序索引信息
        /// </summary>
        /// <returns></returns>
        private List<FlightAlarmPluginOrderInfo> GetNewOrderIndex()
        {
            try
            {
                var alarmList = this.currentAlarmControl.Select(p => p.AlarmDetail);

                var l1 = alarmList.Where(p => p.IsAlarm).OrderBy(p => p.FlightNo);
                var l2 = alarmList.Where(p => !p.IsAlarm).OrderBy(p => p.FlightNo);
                var lst = l1.Union(l2).ToList();

                return lst.Select(p => new FlightAlarmPluginOrderInfo
                {
                    FlightInfoId = p.FlightInfoID,
                    FlightNo = p.FlightNo,
                    HasAlarm = p.IsAlarm,
                    Index = lst.IndexOf(p)
                }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
