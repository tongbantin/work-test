using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Data;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;
using System.Collections;

namespace TreasuryModel.Data
{
    public class SwiftImport
    {
        public int Records { get; set; }        
        public String SWIFT_IO { get; set; }
        public String SWIFT_CORS { get; set; }
        public String SWIFT_TYPE { get; set; }
        public String SWIFT_REF { get; set; }
        public String SWIFT_MUR { get; set; }
        public String SWIFT_TRANS_REF { get; set; }
        public String SWIFT_SUFFIX { get; set; }
        public DateTime? SWIFT_VDATE { get; set; }
        public String SWIFT_CURR { get; set; }
        public Decimal? SWIFT_AMT { get; set; }
        public String SWIFT_STATUS { get; set; }
        public String SWIFT_NETW_STATUS { get; set; }
        public DateTime? SWIFT_NETW_DATE { get; set; }
        public String SWIFT_ORIG_INST { get; set; }
        public DateTime? SWIFT_CREATE_DATE { get; set; }
        public String SWIFT_SERVICE_NAME { get; set; }

        private static ILog Log = log4net.LogManager.GetLogger(typeof(SwiftImport));

        public ArrayList save(string ConnectionString, List<SwiftImport> swift, List<string> items, String swiftType, ArrayList error)
        {
            Oracle o = Oracle.getInstance(ConnectionString);
            
            try
            {
                o.openConnection();
                o.beginTransaction();

                int i = 0 , j =0 ;
                string del_item = "";

// Delete duplicate date data before insert new.
                Log.Debug("all delete length = "+items.Count);
                foreach (string item in items)
                {
                    //set delete date data                                      
                    if(j==0 || j==items.Count){
                        del_item = del_item + "'" + item + "'";
                    }else{
                        del_item = del_item + ",'" + item + "'";
                    }
                    j++;
                }
                try
                {
                    string del = "Delete From OPICINF.KKB_TB_SWIFT WHERE TO_CHAR(SWIFT_VDATE,'DD/MM/YY') in (" + del_item + ") and SWIFT_TYPE = '" + swiftType + "'";
                    Log.Info("Execute Del: " + del);
                    o.createCommand(del, CommandType.Text);
                    o.executeNonQuery();
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }    

// Insert swift data
                foreach (SwiftImport r in swift)
                {
                    try
                    {      
                        string sql = @"Insert Into OPICINF.KKB_TB_SWIFT(SWIFT_IO, SWIFT_CORS, SWIFT_TYPE, SWIFT_REF, SWIFT_MUR, SWIFT_TRANS_REF, SWIFT_SUFFIX, SWIFT_VDATE, SWIFT_CURR, SWIFT_AMT, SWIFT_STATUS, SWIFT_NETW_STATUS, SWIFT_NETW_DATE, SWIFT_ORIG_INST, SWIFT_CREATE_DATE, SWIFT_SERVICE_NAME) ";
                        sql += " Values( :SWIFT_IO, :SWIFT_CORS, :SWIFT_TYPE, :SWIFT_REF, :SWIFT_MUR, :SWIFT_TRANS_REF, :SWIFT_SUFFIX, :SWIFT_VDATE , :SWIFT_CURR, :SWIFT_AMT, :SWIFT_STATUS, :SWIFT_NETW_STATUS, :SWIFT_NETW_DATE, :SWIFT_ORIG_INST, :SWIFT_CREATE_DATE, :SWIFT_SERVICE_NAME)";

                        o.createCommand(sql, CommandType.Text);
                        o.setVarCharInputParameter("SWIFT_IO", r.SWIFT_IO);
                        o.setVarCharInputParameter("SWIFT_CORS", r.SWIFT_CORS);
                        o.setVarCharInputParameter("SWIFT_TYPE", r.SWIFT_TYPE);
                        o.setVarCharInputParameter("SWIFT_REF", r.SWIFT_REF);
                        o.setVarCharInputParameter("SWIFT_MUR", r.SWIFT_MUR);
                        o.setVarCharInputParameter("SWIFT_TRANS_REF", r.SWIFT_TRANS_REF);
                        o.setVarCharInputParameter("SWIFT_SUFFIX", r.SWIFT_SUFFIX);
                        o.setDateInputParameter("SWIFT_VDATE", r.SWIFT_VDATE);
                        o.setVarCharInputParameter("SWIFT_CURR", r.SWIFT_CURR);
                        o.setDecimalInputParameter("SWIFT_AMT", r.SWIFT_AMT);
                        o.setVarCharInputParameter("SWIFT_STATUS", r.SWIFT_STATUS);
                        o.setVarCharInputParameter("SWIFT_NETW_STATUS", r.SWIFT_NETW_STATUS);
                        o.setDateInputParameter("SWIFT_NETW_DATE", r.SWIFT_NETW_DATE);
                        o.setVarCharInputParameter("SWIFT_CREATE_DATE", r.SWIFT_ORIG_INST);
                        o.setDateInputParameter("SWIFT_CREATE_DATE", r.SWIFT_CREATE_DATE);
                        o.setVarCharInputParameter("SWIFT_SERVICE_NAME", r.SWIFT_SERVICE_NAME);
               
                        o.executeNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        error.Add(r.Records);
                        i++;
                    }
                }


                if (error.Count <= 0)
                    o.commitTransaction();
                else
                    o.rollbackTransaction();
            }
            catch (Exception ex)
            {       
                Log.Error(ex.Message);
                error.Add(0);
            }
            finally
            {
                o.closeConnection();
            }
            return error;
        }

    }
}
