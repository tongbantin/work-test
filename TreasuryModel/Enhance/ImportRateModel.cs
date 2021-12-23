using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class ImportRateModel
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(ImportRateModel));
        public string Column = "PAIR,SOURCECODE,CHECKSPOT,ENDDATE,DAYS,SWAP_POINTS_MID,SPOT_MID,OUTRIGHT_MID,INDEX_1,INDEX_2,ROUND,EDATE,CREATEDATE";
        public DataTable Table;

        public ImportRateModel()
        { 

        }

        public bool AddRate(string ConnectionString, string Round, List<DataTable> Data)
        {
            try
            {
                string sql = "Select " + Column + " From OPICINF.KKB_TB_BBRATE";
                return Oracle.addDataAdapter(ConnectionString, sql, Data);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }

        public void ClearRate(string ConnectionString)
        {
            try
            {
                string sql = "Delete From OPICINF.KKB_TB_BBRATE";
                Log.Info("-----Clear Rate Start-----");
                Log.Info("SQL Statement : " + sql);

                Oracle o = Oracle.getInstance(ConnectionString);
                try
                {
                    o.openConnection();
                    o.createCommand(sql, CommandType.Text);
                    o.executeNonQuery();
                }
                catch (Exception ex)
                {
                    Log.Error("-----Clear Rate Error-----");
                    Log.Error(ex.Message);
                }
                finally
                {
                    Log.Info("-----Clear Rate Finish-----");
                    o.closeConnection();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
