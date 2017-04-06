using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevelopRecordPlatform.Model;
namespace DevelopRecordPlatform.BusinessLogical.DAL
{
    public interface IDataProvider
    {
        List<FlightAlarmDetail> GetFlightAlertList();

        List<FlightInfo> GetFlightInfoList();

        bool AddOneFlightAlarm(int flightInfoId);

        bool CancelAllFlightAlarm(int flightInfoId);
    }
}
