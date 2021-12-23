using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Configuration;
using log4net;

namespace KKB.Treasury.TreasuryCommon.Common
{
    public class Oracle : DBFactory
    {
        private static Oracle instance;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Oracle));
        private static OracleDataAdapter Adapter;

        private Oracle(string ConnectionString)
        {
            try
            {
                base.Connection = new OracleConnection(ConnectionString);
                base.Command = new OracleCommand();
            }
            catch (Exception ex)
            {
                Log.Error("-----Oracle Connection Error----");
                Log.Error(ex.Message);
            } 
 
        }
        public static Oracle getInstance(string ConnectionString)
        {
            try { 
				if (Oracle.instance==null)
					Oracle.instance = new Oracle(ConnectionString);
				}
            catch (Exception ex)
            {
                Log.Error("-----Oracle getInstance Error----");
                Log.Error(ex.Message);
            } 
            return Oracle.instance;
        }
        public override bool openConnection() 
        {
            bool flag = false;
            try
            {
                if (base.Connection.State != ConnectionState.Closed)
                {
                    base.Connection.Close();
                }
                base.Connection.Open();
                base.Command.Connection = base.Connection;
                flag = true;
            }
            catch (Exception ex)
            {
                Log.Error("-----Oracle Open Connection Error----");
                Log.Error(ex.Message);
                flag = false;
            }
            return flag;
        }
        public override void createCommand(string Sql, CommandType Type)
        {
            try
            {
                base.Command = new OracleCommand(Sql, (OracleConnection)base.Connection);
                base.Command.CommandType = Type;
            }
            catch (Exception ex)
            {
                Log.Error("-----Oracle Create Command Error----");
                Log.Error(ex.Message);
            }
        }
        public void createTransaction(string Sql, CommandType Type)
        {
            try
            {
                base.Command = new OracleCommand();
                base.Command.CommandText = Sql;
                base.Command.Transaction = base.Transaction;
                base.Command.Connection = base.Connection;
                base.Command.CommandType = Type;
            }
            catch (Exception ex)
            {
                Log.Error("-----Oracle Create Command Error----");
                Log.Error(ex.Message);
            }
        }
        public override void beginTransaction() 
        {
            base.Transaction = base.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            base.Command = base.Connection.CreateCommand();
            base.Command.Transaction = base.Transaction;
        }
        public override void setOutputParameter(string ParamName) 
        {
            OracleParameter OutputParameter = new OracleParameter(ParamName, OracleDbType.RefCursor, ParameterDirection.ReturnValue);
            ((OracleCommand)base.Command).Parameters.Insert(0, OutputParameter);
        }
        public override void setInputParameter(string ParamName, object Value) 
        {
            ((OracleCommand)base.Command).Parameters.Add(ParamName, Value);
        }
        public override int executeNonQuery() 
        {
            return ((OracleCommand)base.Command).ExecuteNonQuery();    
        }
        public override object executeScalar() 
        {
            return ((OracleCommand)base.Command).ExecuteScalar();
        }
        public DataTable executeReaderToDT(string TableName)
        {
            try
            {
                DataTable dt = new DataTable(TableName);
                OracleDataAdapter da = new OracleDataAdapter((OracleCommand)this.Command);
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex) { return null; }
        }
        public DataSet executeReader()
        {
            OracleDataReader reader = null;
            DataSet ds = new DataSet();
            try
            {
                reader = ((OracleCommand)base.Command).ExecuteReader();
                DataTable schema = reader.GetSchemaTable();

                DataTable dt = new DataTable();
                foreach (DataRow dr in schema.Rows)
                {
                    dt.Columns.Add(new DataColumn(dr["ColumnName"].ToString(), Type.GetType(dr["DataType"].ToString())));
                }

                while (reader.Read())
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dr[i] = (Object)reader.GetValue(i);
                    }
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
               // Log.Info(ds.GetXml()); comment 25-08-2016 not get data record
            }
            catch (Exception ex)
            {
                Log.Error("-----Oracle ExecuteReader Error----");
                Log.Error(ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public override DataSet executeReader(string ParamOut, string TableSpaceName) 
        {
            OracleDataReader reader = null;
            DataSet ds = new DataSet();            
            try
            {

                OracleParameter paramReturn = new OracleParameter(ParamOut, OracleDbType.RefCursor, ParameterDirection.Output);
                ((OracleCommand)base.Command).Parameters.Add(paramReturn);
                int result = ((OracleCommand)base.Command).ExecuteNonQuery();
                OracleRefCursor refCur = (OracleRefCursor)paramReturn.Value;

                reader = refCur.GetDataReader();

                do
                {
                    DataTable schema = reader.GetSchemaTable();

                    DataTable dt = new DataTable(TableSpaceName);
                    foreach (DataRow dr in schema.Rows)
                    {
                        dt.Columns.Add(new DataColumn(dr["ColumnName"].ToString(), Type.GetType(dr["DataType"].ToString())));
                    }

                    while (reader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dr[i] = (Object)reader.GetValue(i);
                        }
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt); 

                } while (reader.NextResult());
            }
            catch (Exception ex)
            {
                Log.Error("-----Oracle ExecuteReader Error----");
                Log.Error(ex.Message);
                return null;
            }
            return ds;
        }
        public override void commitTransaction() 
        {
            base.Transaction.Commit();
        }
        public override void rollbackTransaction() 
        {
            base.Transaction.Rollback();
        }
        public override bool closeConnection() 
        {
            bool flag = false;
            try
            {
                base.Connection.Close();
                flag = true;
            }
            catch (Exception ex)
            {
                Log.Error("-----Oracle Close Connection Error----");
                Log.Error(ex.Message);
                flag = false;
            }
            return flag;
        }

        public static DataTable getDataAdapter(string Sql, string ConnectionString)
        {
            try
            {
                DataTable Table = new DataTable();
                Adapter = new OracleDataAdapter(Sql, ConnectionString);
                Adapter.Fill(Table);
                return Table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool addDataAdapter(string ConnectionString, string Sql, List<DataTable> Data)
        {
            try
            {
                using (OracleConnection connect = new OracleConnection(ConnectionString))
                {
                    OracleDataAdapter Adapter = new OracleDataAdapter();
                    Adapter.SelectCommand = new OracleCommand(Sql, connect);
                    OracleCommandBuilder command = new OracleCommandBuilder(Adapter);
                    if (connect.State == ConnectionState.Open)
                        connect.Close();
                    connect.Open();

                    DataTable Table = new DataTable();
                    Adapter.Fill(Table);
                    for (int i = 0; i < Data.Count; i++)
                    {
                        DataTable dt = (DataTable)Data[i];
                        for (int r = 0; r < dt.Rows.Count; r++)
                        {
                            DataRow dr = dt.Rows[r];
                            Table.Rows.Add(dr.ItemArray);
                        }
                    }
                    int row = Adapter.Update(Table);
                    if (row>0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }
        public int executeNonQuery(string ParamRowCount)
        {
            try
            {
                OracleParameter paramReturn1 = new OracleParameter(ParamRowCount, OracleDbType.Int32, ParameterDirection.Output);
                ((OracleCommand)base.Command).Parameters.Add(paramReturn1);
                ((OracleCommand)base.Command).ExecuteNonQuery();
                int rows = Int32.Parse(paramReturn1.Value.ToString());

                return rows;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return 0;
            }
        }

        //Test Thai Language
        /*public int updateCustomer(string ConnectionString, string NameTh)
        {
            String sql = "Update OPICINF.KKB_TB_CUSTOMER Set NameTh=:NAMETH Where CUSTOMERID='46'";
            try
            {
                using (OracleConnection o = new OracleConnection(ConnectionString))
                {
                    using (OracleCommand command = new OracleCommand(sql, o))
                    {
                        command.Parameters.Add("NAMETH", OracleDbType.NChar, NameTh , ParameterDirection.Input);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }*/

        //Update 19/10/2015

        public void setInputParameter(int ParamIndex, object Value)
        {
            ((OracleCommand)base.Command).Parameters[ParamIndex].Value = Value;
        }

        public void setCharInputParameter(string ParamName, object Value)
        {
            ((OracleCommand)base.Command).Parameters.Add(ParamName, OracleDbType.Char, Value, ParameterDirection.Input);
        }
        public void setNCharInputParameter(string ParamName, object Value)
        {
            ((OracleCommand)base.Command).Parameters.Add(ParamName, OracleDbType.NChar, Value, ParameterDirection.Input);
        }
        public void setVarCharInputParameter(string ParamName, object Value)
        {
            ((OracleCommand)base.Command).Parameters.Add(ParamName, OracleDbType.Varchar2, Value, ParameterDirection.Input);
        }
        public void setIntegerInputParameter(string ParamName, object Value)
        {
            ((OracleCommand)base.Command).Parameters.Add(ParamName, OracleDbType.Int64, Value, ParameterDirection.Input);
        }
        public void setDateInputParameter(string ParamName, object Value)
        {
            ((OracleCommand)base.Command).Parameters.Add(ParamName, OracleDbType.Date, Value, ParameterDirection.Input);
        }
        public void setDecimalInputParameter(string ParamName, object Value)
        {
            ((OracleCommand)base.Command).Parameters.Add(ParamName, OracleDbType.Decimal, Value, ParameterDirection.Input);
        }
        public void setCLOBInputParameter(string ParamName, object Value)
        {
            ((OracleCommand)base.Command).Parameters.Add(ParamName, OracleDbType.Clob, Value, ParameterDirection.Input);
        }
        public void executeWithOutputParameter(string ParamName, out object Value)
        {
            Value = null;
            ((OracleCommand)base.Command).Parameters.Add(ParamName, OracleDbType.RefCursor).Direction = ParameterDirection.Output;
            ((OracleCommand)base.Command).ExecuteNonQuery();
            if (((OracleCommand)base.Command) != null)
            {
                Value = ((OracleCommand)base.Command).Parameters[ParamName].Value;
            }
        }

        /// <summary>
        /// For Indicate to Output Type Integer (NUMBER)
        /// </summary>
        /// <param name="ParamName">Oracle.DataAccess.Client.OracleDbType.RefCursor</param>
        public void setOutputParameterInteger(string ParamName)
        {
            OracleParameter param = ((OracleCommand)base.Command).CreateParameter();
            param.ParameterName = ParamName;
            param.OracleDbType = OracleDbType.Int32;
            param.Direction = ParameterDirection.Output;
            ((OracleCommand)base.Command).Parameters.Add(param);
        }

        /// <summary>
        /// For Indicate to Output Type RefCursor
        /// </summary>
        /// <param name="ParamName">Oracle.DataAccess.Client.OracleDbType.RefCursor</param>
        public void setOutputParameterRefCursor(string ParamName)
        {
            OracleParameter param = ((OracleCommand)base.Command).CreateParameter();
            param.ParameterName = ParamName;
            param.OracleDbType = OracleDbType.RefCursor;
            param.Direction = ParameterDirection.Output;
            ((OracleCommand)base.Command).Parameters.Add(param);
        }

        /// <summary>
        /// A executeToDataTable is DataTable feed form Oracle RefCursor
        /// And able get Exception from PL/SQL
        /// </summary>
        /// <param name="TableName">this is named of DataTable</param>
        /// <returns>Type of System.Data.DataTable</returns>
        public DataTable executeToDataTable(string TableName)
        {
            try
            {
                DataTable dt = new DataTable(TableName);
                OracleDataAdapter da = new OracleDataAdapter((OracleCommand)this.Command);
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (OracleException ex)
            {
                throw ex;
            }
        }

        public void executeCheckCommand()
        {

            OracleCommand cmd = ((OracleCommand)base.Command);
            Log.Info("#######");
            Log.Info("CommandText :: " + cmd.CommandText);
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                OracleParameter param = cmd.Parameters[i];
                Log.Info(" ------------------- ");
                Log.Info("ParameterName  :: " + param.ParameterName);
                Log.Info("OracleDbType   :: " + param.OracleDbType.ToString() + " :: C# Type :: " + param.Value.GetType());
                switch (param.OracleDbType)
                {
                    case OracleDbType.BFile:
                        break;
                    case OracleDbType.Blob:
                        break;
                    case OracleDbType.Byte:
                        break;
                    case OracleDbType.Char:
                        break;
                    case OracleDbType.Clob:
                        break;
                    case OracleDbType.Date:
                        break;
                    case OracleDbType.Decimal:
                        break;
                    case OracleDbType.Double:
                        break;
                    case OracleDbType.Int16:
                        break;
                    case OracleDbType.Int32:
                        break;
                    case OracleDbType.Int64:
                        break;
                    case OracleDbType.IntervalDS:
                        break;
                    case OracleDbType.IntervalYM:
                        break;
                    case OracleDbType.Long:
                        break;
                    case OracleDbType.LongRaw:
                        break;
                    case OracleDbType.NChar:
                        break;
                    case OracleDbType.NClob:
                        break;
                    case OracleDbType.NVarchar2:
                        break;
                    case OracleDbType.Single:
                        break;
                    case OracleDbType.TimeStamp:
                        break;
                    case OracleDbType.TimeStampLTZ:
                        break;
                    case OracleDbType.TimeStampTZ:
                        break;
                    case OracleDbType.Varchar2:
                        break;
                    case OracleDbType.XmlType:
                        break;
                    default:
                        break;
                }
                Log.Info("ParameterValue :: " + param.Value);
                Log.Info(" ------------------- ");
            }
            Log.Info("#######");
        }

        public int executeWithOutputNumber(string paramOutputName)
        {
            int result = 0;
            try
            {
                OracleCommand cmd = (OracleCommand)base.Command;
                ((OracleCommand)base.Command).ExecuteNonQuery();
                object param = cmd.Parameters[paramOutputName].Value;
                if (param == null)
                    return -1;
                result = Int32.Parse(param.ToString());

            }
            catch (OracleException ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
