using CarolLib.Core;
using DevelopRecordPlatform.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DevelopRecordPlatform.BusinessLogical.DAL
{
    public class SqlDataProvider : IDataProvider
    {
        #region Conn

        private static string connectionString;

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }

        public SqlDataProvider()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["DBSourceDefault"].ConnectionString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SqlDataProvider(string con)
        {
            connectionString = con;
        }

        #endregion

        public List<FlightAlarmDetail> GetFlightAlertList()
        {
            List<FlightAlarmDetail> result = new List<FlightAlarmDetail>();
            var sql = @"
SELECT flight.FlightInfoID,flight.FlightNo, ISNULL(confirm.Total,0) 'ConfirmTotal',ISNULL(unconfirm.Total,0) 'UnconfirmTotal' 
FROM dbo.temp_TB_FlightInfo flight
LEFT JOIN (SELECT FlightInfoID,COUNT(1) AS 'Total' FROM dbo.temp_TB_FlightAlert WHERE AlertStatus = 1 AND DeleteFlag = 0 GROUP BY FlightInfoID) confirm 
ON confirm.FlightInfoID = flight.FlightInfoID
LEFT JOIN (SELECT FlightInfoID,COUNT(1) AS 'Total' FROM dbo.temp_TB_FlightAlert WHERE AlertStatus = 0 AND DeleteFlag = 0 GROUP BY FlightInfoID) unconfirm 
ON unconfirm.FlightInfoID = flight.FlightInfoID";
            using (SqlConnection conn = GetSqlConnection())
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, sql, CommandType.Text))
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.Add(new Model.FlightAlarmDetail(dr));
                        }
                    }
                }
            }
            return result;
        }

        public List<FlightInfo> GetFlightInfoList()
        {
            List<FlightInfo> result = new List<FlightInfo>();
            var sql = @"select * from temp_TB_FlightInfo with(nolock)";
            using (SqlConnection conn = GetSqlConnection())
            {
                using (SqlDataReader dr = SqlHelper.ExecuteReader(conn, sql, CommandType.Text))
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            result.Add(new Model.FlightInfo(dr));
                        }
                    }
                }
            }
            return result;
        }


        public bool AddOneFlightAlarm(int flightInfoId)
        {
            var sql = @"
                        INSERT dbo.temp_TB_FlightAlert
                                ( FlightInfoID ,
                                  AlertSourceType ,
                                  AlertStatus ,
                                  DeleteFlag ,
                                  AlertTime
                                )
                        VALUES  ( @FlightInfoID , 
                                  3 ,
                                  0 ,
                                  0 , 
                                  GETDATE() 
                                )";
            using (SqlConnection conn = GetSqlConnection())
            {
                var l = SqlHelper.ExecuteNonQuery(conn, sql, CommandType.Text, new SqlParameter("@FlightInfoID", flightInfoId));
                return l > 0;
            }
        }

        public bool CancelAllFlightAlarm(int flightInfoId)
        {
            var sql = "UPDATE dbo.temp_TB_FlightAlert SET DeleteFlag =1 WHERE FlightInfoID = @FlightInfoID";
            using (SqlConnection conn = GetSqlConnection())
            {
                var l = SqlHelper.ExecuteNonQuery(conn, sql, CommandType.Text, new SqlParameter("@FlightInfoID", flightInfoId));
                return l > 0;
            }
        }
        public bool ConfirmAllFlightAlarm(int flightInfoId)
        {

            var sql = "UPDATE dbo.temp_TB_FlightAlert SET AlertStatus =1 WHERE FlightInfoID = @FlightInfoID";
            using (SqlConnection conn = GetSqlConnection())
            {
                var l = SqlHelper.ExecuteNonQuery(conn, sql, CommandType.Text, new SqlParameter("@FlightInfoID", flightInfoId));
                return l > 0;
            }
        }
    }
}
