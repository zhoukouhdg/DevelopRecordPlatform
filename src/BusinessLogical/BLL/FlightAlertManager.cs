using DevelopRecordPlatform.BusinessLogical.DAL;
using DevelopRecordPlatform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopRecordPlatform.BusinessLogical.BLL
{
    public class FlightAlertManager
    {
        private IDataProvider _provider;
        public static readonly FlightAlertManager Instance = new FlightAlertManager();
        public FlightAlertManager()
        {
            _provider = new SqlDataProvider();
        }

        /// <summary>
        /// 获取航班告警统计
        /// </summary>
        /// <returns></returns>
        public List<FlightAlarmDetail> GetFlightAlertList()
        {
            try
            {
                return _provider.GetFlightAlertList().Where(p => p.AlarmTotal.Count > 0).ToList();
            }
            catch (Exception ex)
            {
                return new List<FlightAlarmDetail>();
            }
        }

        /// <summary>
        /// 获取航班列表
        /// </summary>
        /// <returns></returns>
        public List<FlightInfo> GetFlightInfoList()
        {
            try
            {
                return _provider.GetFlightInfoList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 新增一条告警
        /// </summary>
        /// <param name="flightInfoId"></param>
        /// <returns></returns>
        public bool AddOneFlightAlarm(int flightInfoId)
        {
            try
            {
                return _provider.AddOneFlightAlarm(flightInfoId);
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        /// <summary>
        /// 取消告警
        /// </summary>
        /// <param name="flightInfoId"></param>
        /// <returns></returns>
        public bool CancelAllFlightAlarm(int flightInfoId)
        {
            try
            {
                return _provider.CancelAllFlightAlarm(flightInfoId);
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 确认告警
        /// </summary>
        /// <param name="flightInfoId"></param>
        /// <returns></returns>
        public bool ConfirmAllFlightAlarm(int flightInfoId)
        {
            try
            {
                return _provider.ConfirmAllFlightAlarm(flightInfoId);
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}
