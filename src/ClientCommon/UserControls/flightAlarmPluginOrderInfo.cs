using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopRecordPlatform.Client.Common
{
    /// <summary>
    /// 航班告警组件排序依据
    /// </summary>
    public class FlightAlarmPluginOrderInfo
    {
        public int FlightInfoId { get; set; }

        public string FlightNo { get; set; }

        public bool HasAlarm { get; set; }

        public int Index { get; set; }

        public override string ToString()
        {
            return string.Format("航班：{0}({1})，索引：{2}，告警：{3}", this.FlightNo, this.FlightInfoId, this.Index, this.HasAlarm ? "是" : "否");
        }       
    }

    /// <summary>
    /// 航班告警组件排序依据比较器
    /// </summary>
    public class FlightAlarmPluginOrderInfoCompare : IEqualityComparer<FlightAlarmPluginOrderInfo>
    {
        public bool Equals(FlightAlarmPluginOrderInfo x, FlightAlarmPluginOrderInfo y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.FlightInfoId == y.FlightInfoId && x.FlightNo == y.FlightNo && x.Index == y.Index && x.HasAlarm == y.HasAlarm;

        }

        public int GetHashCode(FlightAlarmPluginOrderInfo obj)
        {
            if (obj == null)
                return 0;
            return obj.ToString().GetHashCode();
        }
    }
}
