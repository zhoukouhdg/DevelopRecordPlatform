using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Reflection;

using System.Configuration;
namespace CarolLib.Core
{
    public static class SqlHelper
    {
        #region Execute

        private static int commandTimeOut = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["TimeOut"]) ? 60 : int.Parse(System.Configuration.ConfigurationManager.AppSettings["TimeOut"]);


        public static SqlDataReader ExecuteReader(SqlConnection conn, String name, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(name, conn))
            {
                command.CommandTimeout = commandTimeOut;
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                conn.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
        }

        public static SqlDataReader ExecuteReader(SqlConnection conn, String name, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(name, conn))
            {
                command.CommandTimeout = commandTimeOut;
                command.CommandType = type;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                conn.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Parameters.Clear();
                return reader;
            }
        }

        public static object ExecuteScalar(SqlConnection conn, String name, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(name, conn))
            {

                command.CommandTimeout = commandTimeOut;
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                conn.Open();
                object returnValue = command.ExecuteScalar();
                conn.Close();
                return returnValue;
            }
        }

        public static object ExecuteScalar(SqlConnection conn, string sql, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.CommandTimeout = commandTimeOut;
                command.CommandType = type;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                conn.Open();
                object returnValue = command.ExecuteScalar();
                conn.Close();
                return returnValue;
            }
        }

        public static long ExecuteNonQuery(SqlConnection conn, String name, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(name, conn))
            {

                command.CommandType = type;
                command.CommandTimeout = commandTimeOut;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                conn.Open();
                long returnValue = command.ExecuteNonQuery();
                conn.Close();
                return returnValue;
            }
        }

        public static long ExecuteNonQueryWithTransaction(SqlConnection conn, String name, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(name, conn))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    command.CommandType = type;
                    command.CommandTimeout = commandTimeOut;
                    command.Transaction = tran;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    long returnValue = command.ExecuteNonQuery();
                    tran.Commit();
                    return returnValue;
                }
                catch (SqlException sqlEx)
                {
                    tran.Rollback();
                    throw sqlEx;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static long ExecuteNonQuery(SqlConnection conn, String name, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(name, conn))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.CommandTimeout = commandTimeOut;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    long returnValue = command.ExecuteNonQuery();
                    conn.Close();
                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                ex.Data["name"] = name;
                int i = 0;
                foreach (var para in parameters)
                {
                    ex.Data["para" + i] = para.Value;
                    i++;
                }

                throw;
            }
        }


        public static T ExecuteWithReturn<T>(SqlConnection conn, String name, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(name, conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandTimeout = commandTimeOut;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
                conn.Open();
                command.ExecuteNonQuery();
                T returnValue = (T)command.Parameters["ReturnValue"].Value;
                conn.Close();
                return returnValue;
            }
        }

        public static DataSet ExecuteDataSet(SqlConnection conn, String name, params SqlParameter[] parameters)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(name, conn))
            {
                using (SqlCommand command = new SqlCommand(name, conn))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    DataSet ds = new DataSet();

                    da.SelectCommand = command;
                    da.Fill(ds);

                    return ds;
                }
            }
        }

        public static DataSet ExecuteDataSet(SqlConnection conn, String name,CommandType tpye, params SqlParameter[] parameters)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(name, conn))
            {
                using (SqlCommand command = new SqlCommand(name, conn))
                {
                    command.CommandType = tpye;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    DataSet ds = new DataSet();

                    da.SelectCommand = command;
                    da.Fill(ds);

                    return ds;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="name"></param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSetTimeOut(SqlConnection conn, String name,int? timeOut, params SqlParameter[] parameters)
        {
            using (SqlDataAdapter da = new SqlDataAdapter(name, conn))
            {
                using (SqlCommand command = new SqlCommand(name, conn))
                {
                    if (timeOut!=null)
                    {
                        command.CommandTimeout = Convert.ToInt32(timeOut);
                    }
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    DataSet ds = new DataSet();

                    da.SelectCommand = command;
                    da.Fill(ds);

                    return ds;
                }
            }
        }

        //返回SqlCommand
        public static SqlCommand CreateCmd(string proName, SqlParameter[] prams, SqlConnection Conn)
        {
            SqlConnection SqlConn = Conn;
            if (SqlConn.State.Equals(ConnectionState.Closed))
            {
                SqlConn.Open();                               //关闭
            }
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Connection = SqlConn;
            Cmd.CommandText = proName;
            if (prams != null)
            {
                foreach (SqlParameter Parameter in prams)
                {
                    if (Parameter != null)
                    {
                        Cmd.Parameters.Add(Parameter);
                    }
                }
            }
            return Cmd;
        }
        //重载返回SqlCommand
        public static SqlCommand CreateCmd(string sqlStr, SqlParameter[] prams, SqlConnection Conn, CommandType Ctype)
        {
            SqlConnection SqlConn = Conn;
            if (SqlConn.State.Equals(ConnectionState.Closed))
            {
                SqlConn.Open();
            }
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = Ctype;
            Cmd.Connection = SqlConn;
            Cmd.CommandText = sqlStr;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    if (parameter != null)
                    {
                        Cmd.Parameters.Add(parameter);
                    }
                }
            }
            return Cmd;
        }

        //返回DataTable
        public static DataTable RunProGetTable(string proName, SqlParameter[] prams, SqlConnection Conn)
        {
            try
            {
                SqlCommand Cmd = CreateCmd(proName, prams, Conn);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataSet Ds = new DataSet();
                Da.SelectCommand = Cmd;
                Da.Fill(Ds);
                DataTable Dt = Ds.Tables[0];
                return Dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
        }

        public static DataTable RunProGetTable(string sqlStr, SqlParameter[] prams, SqlConnection Conn, CommandType Ctype)
        {
            try
            {
                SqlCommand Cmd = CreateCmd(sqlStr, prams, Conn, Ctype);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataSet Ds = new DataSet();
                Da.SelectCommand = Cmd;
                Da.Fill(Ds);
                DataTable Dt = Ds.Tables[0];
                return Dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();

            }


        }

        #endregion

        #region Create Parameter

        public static SqlParameter CreateNTextInParameter(String name, String s)
        {
            return CreateInParameter(name, SqlDbType.NText,
                s != null ? s.Length : 16, s);
        }

        public static SqlParameter CreateImageInParameter(String name, Byte[] bytes)
        {
            return CreateInParameter(name, SqlDbType.Image,
                bytes != null ? bytes.Length : 16, bytes);
        }

        public static SqlParameter CreateInParameter(String name, SqlDbType datatype, int size, Object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Input;
            parameter.SqlDbType = datatype;
            parameter.Size = size;
            parameter.Value = value;
            return parameter;
        }

        public static SqlParameter CreateOutParameter(String name, SqlDbType datatype, int size)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;
            parameter.SqlDbType = datatype;
            parameter.Size = size;
            return parameter;
        }

        #endregion

        #region Ids

        public static object IDFromString(String id)
        {
            return id != null && id.Length > 0 ? (object)Int64.Parse(id) : DBNull.Value;
        }

        public static string CleanSearchString(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return null;

            // Do wild card replacements
            searchString = searchString.Replace("*", "%");

            // Strip any markup characters
            //searchString = Transforms.StripHtmlXmlTags(searchString);

            // Remove known bad SQL characters
            searchString = Regex.Replace(searchString, "--|;|'|\"", " ", RegexOptions.Compiled | RegexOptions.Multiline);

            // Finally remove any extra spaces from the string
            searchString = Regex.Replace(searchString, " {1,}", " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

            return searchString;
        }

        public static List<int> PopulateReadersToIds(IDataReader dr, string key)
        {
            return PopulateReadersToIds<int>(dr, key);
        }

        public static List<T> PopulateReadersToIds<T>(IDataReader dr, string key)
        {
            List<T> ids = new List<T>();
            //Dictionary<T, bool> existsIds = new Dictionary<T, bool>();
            while (dr.Read())
            {
                T id = (T)dr[key];
                //if (!existsIds.ContainsKey(id))
                //{
                //existsIds.Add(id, true);
                ids.Add(id);
                //}
            }
            return ids;
        }

        /// <summary>
        /// convert datareader to dictionary
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, DateTime> PopulateReadersToDic(IDataReader dr, string key, string value)
        {
            return PopulateReadersToDic<string, DateTime>(dr, key, value);
        }

        public static Dictionary<T1, T2> PopulateReadersToDic<T1, T2>(IDataReader dr, string key, string value)
        {
            Dictionary<T1, T2> result = new Dictionary<T1, T2>();

            while (dr.Read())
            {
                T1 name = (T1)dr[key];
                T2 date = (T2)dr[value];

                result.Add(name, date);

            }
            return result;

        }

        public static object ConvertIdsToXML<T>(string itemName, T[] ids)
        {
            string rootName = itemName + "s";
            string idName = "i";
            return ConvertIdsToXML<T>(rootName, itemName, idName, ids);
        }

        public static object ConvertModelListToXML<T>(string itemName, IEnumerable<T> modelList)
        {
            if (modelList == null)
            {
                return DBNull.Value;
            }

            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            writer.WriteStartElement(itemName + "s");

            var type = typeof(T);
            if (type.IsValueType)
            {//添加对值类型的支持
                foreach (T model in modelList)
                {
                    writer.WriteStartElement(itemName);
                    writer.WriteAttributeString(type.Name, type.IsEnum ? Convert.ToInt32(model).ToString() : model.ToString());
                    writer.WriteEndElement();
                }
            }
            else
            {
                foreach (T model in modelList)
                {
                    writer.WriteStartElement(itemName);
                    foreach (PropertyInfo p in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        try
                        {
                            if (p.PropertyType.IsGenericType)
                            {
                                var ut = Nullable.GetUnderlyingType(p.PropertyType);
                                if (ut != null && ut.IsEnum)
                                {
                                    var v = p.GetValue(model, null);
                                    if (v == null)
                                    {
                                        writer.WriteAttributeString(p.Name, string.Empty);
                                    }
                                    else
                                    {
                                        writer.WriteAttributeString(p.Name, Convert.ToString((int)v));
                                    }
                                    //writer.WriteAttributeString(p.Name, Convert.ToString((int?)p.GetValue(model, null)));
                                }
                                else
                                {
                                    writer.WriteAttributeString(p.Name, Convert.ToString(p.GetValue(model, null)));
                                }
                            }
                            else if (p.PropertyType.IsEnum)
                                writer.WriteAttributeString(p.Name, Convert.ToString((int)p.GetValue(model, null)));
                            else
                                writer.WriteAttributeString(p.Name, Convert.ToString(p.GetValue(model, null)));
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
            writer.Close();
            return sw.ToString();
        }

        public static object ConvertIdsToXML<T>(string rootName, string itemName, string idName, T[] ids)
        {
            if (ids == null)
                return DBNull.Value;

            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            writer.WriteStartElement(rootName);
            foreach (T id in ids)
            {
                writer.WriteStartElement(itemName);
                writer.WriteAttributeString(idName, typeof(T).IsEnum ? Convert.ToInt32(id).ToString() : id.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Close();
            return sw.ToString();
        }

        #endregion

        #region SQL TypeSafe

        public static object GetSafeSqlDateTime(DateTime? date)
        {
            if (date == null)
                return DBNull.Value;
            return GetSafeSqlDateTime(date.Value);
        }

        public static DateTime GetSafeSqlDateTime(DateTime date)
        {
            if (date < SqlDateTime.MinValue)
            {
                return (DateTime)SqlDateTime.MinValue;
            }
            if (date > SqlDateTime.MaxValue)
            {
                return (DateTime)SqlDateTime.MaxValue;
            }
            return date;
        }

        public static string GetSafeSqlDateTimeFormat(DateTime date)
        {
            return date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.SortableDateTimePattern);
        }

        public static int GetSafeSqlInt(int i)
        {
            if (i <= ((int)SqlInt32.MinValue))
            {
                return (((int)SqlInt32.MinValue) + 1);
            }
            if (i >= ((int)SqlInt32.MaxValue))
            {
                return (((int)SqlInt32.MaxValue) - 1);
            }
            return i;
        }

        public static object StringOrNull(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return DBNull.Value;
            }
            return text;
        }

        #endregion

        #region SqlBulkCopy 数据批量导入...
        /// <summary>
        /// 批量复制操作（SqlBulkCopy） 数据批量导入sqlserver
        /// </summary>
        /// <param name="connectionString">数据库连接地址</param>
        /// <param name="dt">DataTable</param>
        /// <param name="TableName">表名可空，为空使用DataTable名称作为表名 </param>
        /// <param name="batchSize">导入多少条数据</param>
        /// <param name="bulkCopyTimeout">每次导入的时间限制</param>
        public static bool SqlBulkCopyByDatatable(SqlConnection conn, DataTable dt, int batchSize = 500, int bulkCopyTimeout = 9999, string TableName = null)
        {
           try{
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    bool bl = executeSqlBulkCopy(conn, transaction, dt, TableName == null ? dt.TableName : TableName, batchSize, bulkCopyTimeout);
                    if (bl)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
           catch(Exception ex)
           {
               throw;
           }
           finally
           {
               conn.Close();
           }
        
        }

        /// <summary>
        /// 数据批量导入sqlserver 
        /// </summary>
        /// <param name="connectionString">数据库连接地址</param>
        /// <param name="ds">DataSet</param>
        /// <param name="batchSize">导入多少条数据</param>
        /// <param name="bulkCopyTimeout">每次导入的时间限制</param>
        /// <param name="TableName">表名数组可以有多个参数可空，为空使用DataTable名称作为表名</param>
        public static bool SqlBulkCopyByDataSet(SqlConnection conn, DataSet ds, int batchSize = 500, int bulkCopyTimeout = 9999, string[] TableName = null)
        {
            try{
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    int exectint = 0;
                    int count = ds.Tables.Count;
                    bool tableName = true;
                    string tName = string.Empty;
                    if (TableName == null)
                        tableName = false;

                    foreach (DataTable dt in ds.Tables)
                    {
                        tName = tableName ? TableName[exectint] : dt.TableName;
                        bool bl = executeSqlBulkCopy(conn, transaction, dt, tName, batchSize, bulkCopyTimeout);
                        if (bl)
                            exectint++;
                        else
                            break;
                    }
                    if (exectint == count)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
         }

        /// <summary>
        /// 执行SqlBulkCopy操作
        /// </summary>
        /// <param name="conn">SqlConnection 对象</param>
        /// <param name="transaction">SqlTransaction 对象</param>
        /// <param name="dt">DataTable 执行的表</param>
        /// <param name="TableName">表名</param>
        /// <param name="batchSize">导入多少条数据</param>
        /// <param name="bulkCopyTimeout">每次导入的时间限制</param>
        /// <returns></returns>
        private static bool executeSqlBulkCopy(SqlConnection conn, SqlTransaction transaction, DataTable dt, string TableName, int batchSize, int bulkCopyTimeout)
        {
            using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
            {
                sqlbulkcopy.BatchSize = batchSize;
                sqlbulkcopy.BulkCopyTimeout = bulkCopyTimeout;//每次导入的时间限制
                sqlbulkcopy.DestinationTableName = TableName;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                }
                try
                {
                    sqlbulkcopy.WriteToServer(dt);
                    return true;
                }
                catch (System.Exception ex)
                {
                    //  记录错误信息 throw ex;
                    return false;
                }
                finally
                {
                    sqlbulkcopy.Close();
                }
            }
        }
       
        #endregion
    }
}
