using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopRecordPlatform.Model
{
    /// <summary>
    /// 航班信息
    /// </summary>
    public class FlightInfo
    {
        public FlightInfo()
        {

        }
        public FlightInfo(System.Data.IDataReader dr)
        {
            this.FlightInfoID = Convert.ToInt32(dr["FlightInfoID"]);         
            this.FlightNo = Convert.ToString(dr["FlightNo"]);
        }
        /// <summary>
        /// 航班ID
        /// </summary>
        public int FlightInfoID { get; set; }

        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo { get; set; }
    }
}
