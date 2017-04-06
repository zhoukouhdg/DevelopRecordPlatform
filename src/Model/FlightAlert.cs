using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopRecordPlatform.Model
{
    /// <summary>
    /// 航班告警
    /// </summary>
    public class FlightAlert
    {
        public FlightAlert()
        {

        }

        public FlightAlert(System.Data.IDataReader dr)
        {
            this.FlightInfoID = Convert.ToInt32(dr["FlightInfoID"]);
            this.AlertID = Convert.ToInt32(dr["AlertID"]);
            this.AlertSourceType = Convert.ToInt32(dr["AlertSourceType"]);
            this.Status = Convert.ToBoolean(dr["Status"]);
            this.AlertTime = Convert.ToDateTime(dr["AlertTime"]);
        }

        public int AlertID { get; set; }
        /// <summary>
        /// 航班ID
        /// </summary>
        public int FlightInfoID { get; set; }

        /// <summary>
        /// 告警类型
        /// </summary>
        public int AlertSourceType { get; set; }

        /// <summary>
        /// 告警状态，是否被确认
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 告警时间
        /// </summary>
        public DateTime AlertTime { get; set; }
    }
}
