using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopRecordPlatform.Model
{
    /// <summary>
    /// 航班告警详情
    /// </summary>
    public class FlightAlarmDetail
    {
        public FlightAlarmDetail()
        {

        }
        public FlightAlarmDetail(System.Data.IDataReader dr)
        {
            this.FlightInfoID = Convert.ToInt32(dr["FlightInfoID"]);
            this.FlightNo = Convert.ToString(dr["FlightNo"]);
            var confirm = Convert.ToInt32(dr["ConfirmTotal"]);
            var unconfirm = Convert.ToInt32(dr["UnconfirmTotal"]);
            this.AlarmTotal = new FlightAlarmTypeTotal
            {
                Confirm = confirm,
                Unconfirmed = unconfirm,
                Count = unconfirm + confirm
            };

        }
        /// <summary>
        /// 航班ID
        /// </summary>
        public int FlightInfoID { get; set; }

        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo { get; set; }

        public FlightAlarmTypeTotal AlarmTotal { get; set; }

        /// <summary>
        /// 当前航班是否有告警
        /// </summary>
        public bool IsAlarm
        {
            get
            {
                if (this.AlarmTotal != null) return this.AlarmTotal.Unconfirmed > 0;
                return false;
            }
        }
    }

    /// <summary>
    /// 航班类别告警数量
    /// </summary>
    public class FlightAlarmTypeTotal
    {
        /// <summary>
        /// 确认告警数量
        /// </summary>
        public int Confirm { get; set; }

        /// <summary>
        /// 未确认告警数量
        /// </summary>

        public int Unconfirmed { get; set; }

        /// <summary>
        /// 所有告警数量
        /// </summary>
        public int Count { get; set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}", this.Unconfirmed, this.Count);
        }
    }
}
