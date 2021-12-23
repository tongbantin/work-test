using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using KKB.DealTicketModel.Model;
using KKB.DealTicketReport.Report;
using KKB.Treasury.TreasuryReport.Report;
using System.Collections;
using System.ComponentModel;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class Model_RPT_TR136BOBE
    {
        private static string Owner = Configuration.Config.Owner;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Model_RPT_TR136BOBE));
        
        public static ReportClass getReport(string ConnectionString, string Category, string ReportName, string Series , string MDate)
        {
            Log.Info("-----Model_RPT_TR136BOBE getReport Start-----");
            Log.Info("Store Procedure : " + Model_RPT_TR136BOBE.Owner + ".KKB_SP_TSS_TR136BOBESERIES");
            Log.Info("param[ Category: " + Category + " ReportName: " + ReportName + " Series: " + Series + " MDate: " + MDate);

            ReportClass report = null;
            string TableName="";

            if (ReportName == "Series-Confirmation")
            {
                if (Category == "AVAL")
                {
                    TableName = "TB_TR136BBOBEDOCAVAL";
                    report = new RPT_TR136BBOBEDOCAVAL();

                    #region Mock data for test
                    //// mock data for test
                    //DataSet ds = new DataSet();
                    //DataTable dt = new DataTable("TB_TR136BBOBEDOCAVAL");
                    //DataRow dr;
                    //dt.Columns.Add("BE_SELLER_NAME");
                    //dt.Columns.Add("BE_BUYER_NAME");
                    //dt.Columns.Add("BE_ISSUE_DATE");
                    //dt.Columns.Add("BE_MAT_DATE");
                    //dt.Columns.Add("TENOR");
                    //dt.Columns.Add("BE_DIS_RATE");
                    //dt.Columns.Add("BE_SERIES_CODE");
                    //dt.Columns.Add("BE_ISSUER_NAME");
                    //dt.Columns.Add("BE_TOTAL_AMT");
                    //dt.Columns.Add("GROSS_SETTLE");
                    //dt.Columns.Add("DISCOUNT_AMT");
                    //dt.Columns.Add("TARGETBANK");
                    //dt.Columns.Add("TARGETACCT");

                    //dr = dt.NewRow();
                    //dr["BE_SELLER_NAME"] = "นางสาวบรรเจิดศักดิ์ สัณหภักดี";
                    //dr["BE_BUYER_NAME"] = "Kiatnakin Bank PLC.";
                    //dr["BE_ISSUE_DATE"] = DateTime.Now.ToString("dd/MMM/yyyy");
                    //dr["BE_MAT_DATE"] = DateTime.Now.AddMonths(2).ToString("dd/MMM/yyyy");
                    //dr["TENOR"] = (DateTime.Now.Date - DateTime.Now.AddMonths(2).Date).TotalDays.ToString();
                    //dr["BE_DIS_RATE"] = "1.4";
                    //dr["BE_SERIES_CODE"] = "BETEST22052019";
                    //dr["BE_ISSUER_NAME"] = "Pruasa Real Estate";
                    //dr["BE_TOTAL_AMT"] = "1400000";
                    //dr["GROSS_SETTLE"] = "1000000";
                    //dr["DISCOUNT_AMT"] = "400000";
                    //dr["TARGETBANK"] = "KBANK";
                    //dr["TARGETACCT"] = "123456789";
                    //dt.Rows.Add(dr);

                    //dr = dt.NewRow();
                    //dr["BE_SELLER_NAME"] = "Mr.Narit Trachuentong";
                    //dr["BE_BUYER_NAME"] = "Kiatnakin Bank PLC.";
                    //dr["BE_ISSUE_DATE"] = DateTime.Now.ToString("dd/MMM/yyyy");
                    //dr["BE_MAT_DATE"] = DateTime.Now.AddMonths(2).ToString("dd/MMM/yyyy");
                    //dr["TENOR"] = (DateTime.Now.Date - DateTime.Now.AddMonths(2).Date).TotalDays.ToString();
                    //dr["BE_DIS_RATE"] = "2.78";
                    //dr["BE_SERIES_CODE"] = "BETEST22052019";
                    //dr["BE_ISSUER_NAME"] = "Pruasa Real Estate";
                    //dr["BE_TOTAL_AMT"] = "2000000";
                    //dr["GROSS_SETTLE"] = "450000";
                    //dr["DISCOUNT_AMT"] = "1550000";
                    //dr["TARGETBANK"] = "SCB";
                    //dr["TARGETACCT"] = "987654321";
                    //dt.Rows.Add(dr);

                    //// allotment
                    //DataTable dtc = new DataTable("ALLOTMENT");
                    //dtc.Columns.Add("AL_SELLER_NAME");
                    //dtc.Columns.Add("AL_ALLOT_NO");
                    //dtc.Columns.Add("AL_ISSUE_NAME");
                    //dtc.Columns.Add("AL_TOTAL_AMT", typeof(int));
                    //dtc.Columns.Add("AL_GROSS_SETTLE");
                    //dtc.Columns.Add("AL_DISCOUNT_AMT");

                    //dr = dtc.NewRow();
                    //dr["AL_SELLER_NAME"] = "นางสาวบรรเจิดศักดิ์ สัณหภักดี";
                    //dr["AL_ALLOT_NO"] = "ABC1234";
                    //dr["AL_ISSUE_NAME"] = "Pruasa Real Estate";
                    //dr["AL_TOTAL_AMT"] = 1111111;
                    //dr["AL_GROSS_SETTLE"] = "1111111";
                    //dr["AL_DISCOUNT_AMT"] = "1111111";
                    //dtc.Rows.Add(dr);

                    //dr = dtc.NewRow();
                    //dr["AL_SELLER_NAME"] = "Mr.Narit Trachuentong";
                    //dr["AL_ALLOT_NO"] = "FFF1234";
                    //dr["AL_ISSUE_NAME"] = "PTT";
                    //dr["AL_TOTAL_AMT"] = 3333333;
                    //dr["AL_GROSS_SETTLE"] = "3333333";
                    //dr["AL_DISCOUNT_AMT"] = "3333333";
                    //dtc.Rows.Add(dr);

                    //dr = dtc.NewRow();
                    //dr["AL_SELLER_NAME"] = "นางสาวบรรเจิดศักดิ์ สัณหภักดี";
                    //dr["AL_ALLOT_NO"] = "ABC5678";
                    //dr["AL_ISSUE_NAME"] = "AIS";
                    //dr["AL_TOTAL_AMT"] = 2222222;
                    //dr["AL_GROSS_SETTLE"] = "2222222";
                    //dr["AL_DISCOUNT_AMT"] = "2222222";
                    //dtc.Rows.Add(dr);

                    //dr = dtc.NewRow();
                    //dr["AL_SELLER_NAME"] = "Mr.Narit Trachuentong";
                    //dr["AL_ALLOT_NO"] = "FFF5678";
                    //dr["AL_ISSUE_NAME"] = "AI";
                    //dr["AL_TOTAL_AMT"] = 4444444;
                    //dr["AL_GROSS_SETTLE"] = "4444444";
                    //dr["AL_DISCOUNT_AMT"] = "4444444";
                    //dtc.Rows.Add(dr);

                    //ds.Tables.Add(dt);
                    //ds.Tables.Add(dtc);

                    //report.SetDataSource(ds);
                    //report.Subreports["ALLOT_SUB_RPT"].SetDataSource(ds);
                    #endregion
                }
                else
                {
                    TableName = "TB_TR136BBOBEDOC";
                    report = new RPT_TR136BBOBEDOC();
                }
            }
            else if (ReportName == "Series-Report")
            {
                TableName = "TB_TR136DBOBERP";
                report = new RPT_TR136DBOBERP();
            }
            else if (ReportName == "Series-SummaryDoc")
            {
                TableName = "TB_TR136OBOBE";
                report = new RPT_TR136OBOBE();
            }
            else if (ReportName == "Allotment-Confirmation")
            {
                TableName = "TB_TR136SBOBEDOC";
                report = new RPT_TR136SBOBEDOC();
            }
            else if (ReportName == "Series-BE")
            {
                TableName = "TB_TR136BOBE";
                report = new RPT_TR136BOBE();
            }
            else if (ReportName == "Series-ReportAll")
            {
                TableName = "TB_TR136DBOBERPAL";
                report = new RPT_TR136DBOBERPAL();
            }
            else if (ReportName == "Series-AllotmentConfirm")
            {
                TableName = "TB_TR136BBOBEALLOTCONF";
                report = new RPT_TR136BBOBEALLOTCONF();
            }
            else if (ReportName == "Maturity-BE")
            {
                TableName = "TB_TR136BOBEMAT";
                report = new RPT_TR136BOBEMAT();
            }
            else if (ReportName == "Series-BalanceReport")
            {
                TableName = "TB_TR136DBOBERPBL";
                report = new RPT_TR136DBOBERPBL();
            }
            else if(ReportName == "Series-BuyPreConfirmationAval")
            {
                TableName = "TB_TR136BBOBEDOCAVAL";
                report = new RPT_TR136BPCAVAL();
            }
            else if (ReportName == "Series-SellPreConfirmationAval")
            {
                TableName = "TB_TR136BBOBEDOCAVAL";
                report = new RPT_TR136SPCAVAL();
            }

            Oracle o = Oracle.getInstance(ConnectionString);
            DataSet s = null;
            try
            {
                o.openConnection();
                o.createCommand(Model_RPT_TR136BOBE.Owner + ".KKB_SP_TSS_TR136BOBESERIES", CommandType.StoredProcedure);
                o.setInputParameter("p_category", Category);
                o.setInputParameter("p_series", Series);
                o.setInputParameter("p_report", ReportName);
                o.setInputParameter("p_mdate", MDate);
                s = o.executeReader("ref_rpt_cur", TableName);

                if (s != null)
                {
                    if (s.Tables[0].Rows.Count > 0)
                    {
                        if (Category == "AVAL")
                        {
                            report = SetSubReportData(ConnectionString, s, report, Category, ReportName, Series, MDate);
                        }
                        else
                        {
                            report.SetDataSource(s);
                        }
                        Log.Info("Total Records : " + s.Tables[0].Rows.Count + " Record(s)");
                    }
                    else
                    {
                        report = null;
                    }
                }
                else
                    report = null;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----Model_RPT_TR136BOBE Finish-----");
                o.closeConnection();
            }

            return report;
        }

        public static DataSet getData(string ConnectionString, string ReportName, string Category, string Series, string MDate)
        {
            Log.Info("----- Model_RPT_TR136BOBE getData -----");
            Log.Info("Store Procedure : " + Model_RPT_TR136BOBE.Owner + ".KKB_SP_TSS_TR136BOBESERIES");
            Log.Info("ReportName:" + ReportName + "Category:" + Category + " Series :" + Series + " MDate:" + MDate);

            string TableName = "";
            Oracle o = Oracle.getInstance(ConnectionString);
            try
            {
                DataSet s = null;
                o.openConnection();
                o.createCommand(Model_RPT_TR136BOBE.Owner + ".KKB_SP_TSS_TR136BOBESERIES", CommandType.StoredProcedure);
                o.setInputParameter("p_category", Category);
                o.setInputParameter("p_series", Series);               
                o.setInputParameter("p_report", ReportName);
                o.setInputParameter("p_mdate", MDate);
                s = o.executeReader("ref_rpt_cur", "");

                return s;
            }
            catch (Exception ex)
            {
                Log.Error("-----Model Error-----");
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                Log.Info("-----Model_RPT_TR136BOBE Finish-----");
                o.closeConnection();
            }
        }

        public static DataSet ConvertToDataTable<T>(IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                DataSet ds = new DataSet("DS_TR136DBOBERPBL");
                DataTable dt = ds.Tables.Add("TB_TR136DBOBERPBL");
                foreach (PropertyDescriptor prop in properties)
                {
                    dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                foreach (T item in data)
                {
                    DataRow row = dt.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    dt.Rows.Add(row);
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ReportClass SetSubReportData(string ConnectionString, DataSet dataSet, ReportClass report, string Category, string ReportName, string Series, string MDate)
        {
            if (report != null && (report.Subreports != null && report.Subreports.Count > 0))
            {
                string TableName = string.Empty;
                string subReportName = string.Empty;

                if (ReportName == "Series-Confirmation")
                {
                    TableName = "ALLOTMENT";
                    subReportName = "ALLOT_SUB_RPT";
                }
                else if (ReportName == "Series-BuyPreConfirmationAval")
                {
                    TableName = "ALLOTMENT";
                    subReportName = "ALLOT_SUB_RPT";
                }
                else if (ReportName == "Series-SellPreConfirmationAval")
                {
                    TableName = "ALLOTMENT";
                    subReportName = "ALLOT_SELL_SUB_RPT";
                }

                Oracle o = Oracle.getInstance(ConnectionString);
                try
                {
                    DataTable copyDataTable = new DataTable();
                    DataSet s = null;
                    o.openConnection();
                    o.createCommand(Model_RPT_TR136BOBE.Owner + ".KKB_SP_TSS_TR136BOBESERIES", CommandType.StoredProcedure);
                    o.setInputParameter("p_category", Category);
                    o.setInputParameter("p_series", Series);
                    o.setInputParameter("p_report", subReportName);
                    o.setInputParameter("p_mdate", MDate);
                    s = o.executeReader("ref_rpt_cur", TableName);

                    if (s != null)
                    {
                        if (s.Tables[0].Rows.Count > 0)
                        {
                            copyDataTable = s.Tables[0].Copy();
                            dataSet.Tables.Add(copyDataTable);
                            report.SetDataSource(dataSet);
                            report.Subreports[subReportName].SetDataSource(dataSet);
                        }
                        
                    }
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    o.closeConnection();
                }
            }
            else
            {
                report.SetDataSource(dataSet);
            }

            return report;
        }
    }

    public class TR136DBOBERPBLModel
    {
        public string BE_ALLOT_SKSNO { get; set; }
        public string BE_ALLOT_PATRAREGNO { get; set; }
        public string BE_ALLOT_CLIENTNAME { get; set; }
        public string BE_SETTLE_DATE { get; set; }
        public string BE_MAT_DATE { get; set; }
        public string BE_SERIES_CODE { get; set; }
        public string BE_ALLOT_NO { get; set; }
        public string BE_ALLOT_AMT { get; set; }
        public string BE_ALLOT_DEPOSITDATE { get; set; }
        public string BE_ALLOT_WITHDRAWDATE { get; set; }
        public string BE_ALLOT_COMMENT { get; set; }
    }

}
