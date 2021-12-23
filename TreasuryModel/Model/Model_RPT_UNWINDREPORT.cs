using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;
using log4net;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_UNWINDREPORT
    {
         private static string Owner = Configuration.Config.Owner;
         private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_UNWINDREPORT));

         public Model_RPT_UNWINDREPORT()
        {

        }

         public static List<ReportClass> getReport(string ConnectionString, string symbol, string producttype , string stnid)
         {
             Log.Info("-----Model_RPT_UNWINDREPORT Start-----");
             Log.Info("Store Procedure : " + Model_RPT_UNWINDREPORT.Owner + ".KKB_SP_TSS_STNCONF");

             List<ReportClass> list = new List<ReportClass>();
             ReportClass report = null;
             Oracle o = Oracle.getInstance(ConnectionString);
             try
             {
                 DataSet s = null;
                 o.openConnection();
                 o.createCommand(Model_RPT_UNWINDREPORT.Owner + ".KKB_SP_TSS_STNCONF", CommandType.StoredProcedure);
                 o.setInputParameter("P_action", "UNWIND");
                 o.setInputParameter("P_product", producttype);
                 o.setInputParameter("P_symbol", symbol);
                 o.setInputParameter("P_stnid", stnid);
                 s = o.executeReader("ref_rpt_cur", "TB_STNCONF");

                 if (s != null)
                 {
                     int num = s.Tables[0].Rows.Count;

                     if (num > 0)
                     {



                         for (int n = 0; n < num; n++)
                         {

                             DataSet ds = new DataSet();
                             DataTable dt = new DataTable();
                             dt = s.Tables[0].Clone();

                             var row = dt.NewRow();
                             row = s.Tables[0].Rows[n];


                             dt.Rows.Add(row.ItemArray);

                             ds.Tables.Add(dt);



                             switch (producttype)
                             {

                                 case "SFIN":
                                     report = new RPT_STNSHFN_UNWIND();
                                     report.SetDataSource(ds);
                                    // Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                                     Log.Info(s.GetXml());

                                     break;
                                 case "DUAL":
                                     report = new RPT_STNDUCN_UNWIND();
                                     report.SetDataSource(ds);
                                     //Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                                     Log.Info(s.GetXml());

                                     break;
                                 case "DRAN":

                                     report = new RPT_STNDRAN_UNWIND();
                                     report.SetDataSource(ds);
                                    // Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                                     Log.Info(s.GetXml());
                                     break;
                                 case "BODN":
                                     report = new RPT_STNBODN_UNWIND();
                                     report.SetDataSource(ds);
                                    // Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                                     Log.Info(s.GetXml());

                                     break;
                             }

                             list.Add(report);

                         }

                     }
                 }
                 //else
                 //    report = null;
             }
             catch (Exception ex)
             {
                 Log.Error("-----Model_RPT_UNWINDREPORT Error-----");
                 Log.Error(ex.Message);
             }
             finally
             {
                 Log.Info("-----Model_RPT_UNWINDREPORT Finish-----");
                 o.closeConnection();
             }
             return list;
         }

         
    }
}
