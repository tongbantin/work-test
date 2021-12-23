using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryShadowSystem.Configuration;
using System.IO;
using System.IO.Compression;
using log4net;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using CrystalDecisions.Shared;
using System.Data;
using log4net;
using System.IO.Packaging;
using System.Net.Mime;
using TreasuryModel.Enhance;
using System.Net.Mail;
using System.Globalization;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR136BOBESERIES : System.Web.UI.Page
    {
        #region Fields

        public string TheUrl = "";
        string reportname = "TR136BOBESERIES";
        ReportClass rpt = new ReportClass();
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR136BOBESERIES));
        private static Dictionary<string, string> BEReportDict = new Dictionary<string, string>()
        {
            { "-", "-- Please Select --" },
            { "Series-Confirmation", "Buy Confirmation" },
            { "Series-SummaryDoc", "Buy Cover" },
            { "Series-AllotmentConfirm", "Buy Allotment Confirm" },
            { "Series-Report", "Summary Report" },
            { "Series-ReportAll", "Summary Report All" },
            { "Allotment-Confirmation", "Sell Confirmation" },
            { "Series-BE", "Bill of Exchange" },
            { "Maturity-BE", "BE Maturity Deal" },
            { "Series-BalanceReport", "Balance Stock" },
            { "Series-BuyPreConfirmationAval", "Buy Pre-Confirmation" },
            { "Series-SellPreConfirmationAval", "Sell Pre-Confirmation" }
        };
        private static Dictionary<string, string> BECopyDict = new Dictionary<string, string>()
        {
            { "-", "-- Please Select --" },
            { string.Empty, "Blank" },
            { "1", "Bank's Copy" },
            { "2", "Customer's Copy" }
        };
        private static Dictionary<string, string> BECategoryDict = new Dictionary<string, string>()
        {
            { "-", "-- Please Select --" },
            { string.Empty, "Other" },
            { "KKPB", "KKPB" },
            { "PHATRA", "PHATRA" },
            { "AVAL", "AVAL" },
            { "FUND", "FUND" }
        };

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindControl();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string Category = BECategoryDDL.SelectedValue;
                string Report = BEReportDDL.SelectedValue;
                string Series = BESeriestxt.Text.Trim();
                string MDate = string.IsNullOrEmpty(BEDatetxt.Text.Trim()) ? DateTime.Now.ToString("dd/MM/yyyy") : BEDatetxt.Text.Trim();
                rpt = Model_RPT_TR136BOBE.getReport(Config.ConnectionString, Category, Report, Series, MDate);
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                else
                {
                    if ((rpt is RPT_TR136BBOBEDOC) || (rpt is RPT_TR136SBOBEDOC))
                    {
                        rpt.SetParameterValue("parameterCopy", BECopyDDL.SelectedValue == "-" ? string.Empty : BECopyDDL.SelectedValue);
                    }
                    else if(rpt is RPT_TR136DBOBERPBL)
                    {   
                        DateTime matDate;
                        DateTime.TryParseExact(MDate, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out matDate);
                        rpt.SetParameterValue("ParamReportDate", string.Format("As of {0:MMM y}", matDate));
                        rpt.SetParameterValue("ParamNextReportDate", string.Format("{0:dd MMM y}", matDate));
                    }
                }

                Session["ReportSource"] = rpt;
                TheUrl = "../View/Viewer.aspx";
                Page.DataBind();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void SendConfirmation_Click(object sender, EventArgs e)
        {
        
            string report = BEReportDDL.SelectedValue;

            try
            {
                if (report.Equals("Maturity-BE"))
                {
                    GenMaturityReport();
                }
                else
                {
                    ExportReport();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void ViewPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string Category = BECategoryDDL.SelectedValue;
                string Reportname = BEReportDDL.SelectedValue;
                string Series = BESeriestxt.Text.Trim();
                string MDate = string.IsNullOrEmpty(BEDatetxt.Text.Trim()) ? DateTime.Now.ToString("dd/MM/yyyy") : BEDatetxt.Text.Trim();

                DataSet ds = Model_RPT_TR136BOBE.getData(Config.ConnectionString, Reportname, Category, Series, MDate);

                string[] prefixEncrypt;
                string[] code;

                if (ds != null)
                {
                    prefixEncrypt = new string[ds.Tables[0].Rows.Count];
                    code = new string[ds.Tables[0].Rows.Count];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        prefixEncrypt[i] = ds.Tables[0].Rows[i]["ENCRYPT"].ToString();
                        code[i] = ds.Tables[0].Rows[i]["CODE"].ToString();
                    }

                    string[] uniqueItems = prefixEncrypt.Select(x => x.ToUpper()).Distinct<string>().ToArray();
                    int j = 0; string message = "";
                    foreach (string item in uniqueItems)
                    {
                        message += item + Config.GetParameterFile("136ENCRYPT_BE")+"<br>";
                        j++;
                    }

                    MessageBox.Text = message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                }
                else {
                    MessageBox.Text = "ไม่พบ Password";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);
                
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
     
  
        protected void BEReportDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string BEReportDDLSelectedValue = BEReportDDL.SelectedValue;
            
            // Copy DDL
            List<string> copyAllow = new List<string>() { "Series-Confirmation", "Allotment-Confirmation" };
            BECopyDDL.DataSource = getBECopyByReport(BEReportDDLSelectedValue);
            BECopyDDL.DataValueField = "Key";
            BECopyDDL.DataTextField = "Value";
            BECopyDDL.Enabled = copyAllow.Contains(BEReportDDLSelectedValue) ? true : false;
            BECopyDDL.SelectedIndex = 0;
            BECopyDDL.DataBind();

            // Category DDL
            List<string> categoryAllow = new List<string>() { "Series-Confirmation", "Series-BalanceReport", "Series-BuyPreConfirmationAval", "Series-SellPreConfirmationAval" };
            BECategoryDDL.DataSource = getBECategoryByReport(BEReportDDLSelectedValue);
            BECategoryDDL.DataValueField = "Key";
            BECategoryDDL.DataTextField = "Value";
            BECategoryDDL.Enabled = categoryAllow.Contains(BEReportDDLSelectedValue) ? true : false;
            BECategoryDDL.SelectedIndex = 0;
            BECategoryDDL.DataBind();

            enableBESeriesByReport(BEReportDDLSelectedValue);
            enableBEDateByReport(BEReportDDLSelectedValue);

            TheUrl = "abount:blank";
            Page.DataBind();
        }
        private void bindControl()
        {
            // BE Report DDL
            BEReportDDL.DataSource = BEReportDict;
            BEReportDDL.DataValueField = "Key";
            BEReportDDL.DataTextField = "Value";
            BEReportDDL.Enabled = true;
            BEReportDDL.SelectedIndex = 0;
            BEReportDDL.DataBind();

            // BE Category DDL
            BECategoryDDL.DataSource = getBECategoryByReport(BEReportDDL.SelectedValue);
            BECategoryDDL.DataValueField = "Key";
            BECategoryDDL.DataTextField = "Value";
            BECategoryDDL.Enabled = true;
            BECategoryDDL.SelectedIndex = 0;
            BECategoryDDL.DataBind();

            // BE Copy DDL
            BECopyDDL.DataSource = getBECopyByReport(BEReportDDL.SelectedValue);
            BECopyDDL.DataValueField = "Key";
            BECopyDDL.DataTextField = "Value";
            BECopyDDL.Enabled = true;
            BECopyDDL.SelectedIndex = 0;
            BECopyDDL.DataBind();

            enableBESeriesByReport(BEReportDDL.SelectedValue);
            enableBEDateByReport(BEReportDDL.SelectedValue);
        }

        private Dictionary<string, string> getBECopyByReport(string report)
        {
            Dictionary<string, string> result = BECopyDict.Where(x => x.Key == "-").ToDictionary(x => x.Key, x => x.Value);

            if (report == "Series-Confirmation" || report == "Allotment-Confirmation")
            {
                result = BECopyDict;
            }

            return result;
        }

        private Dictionary<string, string> getBECategoryByReport(string report)
        {
            Dictionary<string, string> result = BECategoryDict.Where(x => x.Key == "-").ToDictionary(x => x.Key, x => x.Value);

            if (report == "Series-Confirmation")
            {
                List<string> buyConfirmCategory = new List<string>() { string.Empty, "AVAL" };
                result = BECategoryDict.Where(x => buyConfirmCategory.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
            }
            else if (report == "Series-BalanceReport")
            {
                List<string> balanceStockCategory = new List<string>() { "-", "AVAL", "KKPB", "PHATRA" };
                result = BECategoryDict.Where(x => balanceStockCategory.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
            }
            else if (report == "Series-BuyPreConfirmationAval" || report == "Series-SellPreConfirmationAval")
            {
                List<string> preConfirmCategory = new List<string>() { "AVAL" };
                result = BECategoryDict.Where(x => preConfirmCategory.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
            }

            return result;
        }

        private void enableBESeriesByReport(string report)
        {
            if (report == "Maturity-BE" || report == "Series-BalanceReport")
            {
                BESeriestxt.Text = string.Empty;
                BESeriestxt.Enabled = false;
            }
            else
            {
                BESeriestxt.Enabled = true;
            }
        }

        private void enableBEDateByReport(string report)
        {
            if (report == "Series-ReportAll" || report == "Maturity-BE" || report == "-")
            {
                BEDatelbl.Text = "Maturity Date";
                BEDatetxt.Text = string.Empty;
                BEDatetxt.Enabled = true;
            }
            else if (report == "Series-BalanceReport")
            {
                BEDatelbl.Text = "Withdraw Date";
                BEDatetxt.Text = string.Empty;
                BEDatetxt.Enabled = true;
            }
            else
            {
                BEDatelbl.Text = "Maturity Date";
                BEDatetxt.Text = string.Empty;
                BEDatetxt.Enabled = false;
            }
        }

        
        #endregion

        
        #region Methods

        private void ExportReport()
        {
            try
            {
                string Category = BECategoryDDL.SelectedValue;
                string Reportname = BEReportDDL.SelectedValue;
                string Series = BESeriestxt.Text.Trim();
                string MDate = string.IsNullOrEmpty(BEDatetxt.Text.Trim()) ? DateTime.Now.ToString("dd/MM/yyyy") : BEDatetxt.Text.Trim();

                DataSet ds = Model_RPT_TR136BOBE.getData(Config.ConnectionString, Reportname, Category, Series, MDate);    
                
                //SET OBJECT TABLE AND REPORT
                ReportClass report = null;
                string tableName = "" , subTableName = "" , subReport = "";
                string filename = "";
                string zipcon = "";
                string[] prefixEncrypt;
                string[] uniqueItems;
                string[] code;

                if (Reportname == "Series-Confirmation")
                {
                    if (Category == "AVAL")
                    {
                        tableName = "TB_TR136BBOBEDOCAVAL";                       
                        report = new RPT_TR136BBOBEDOCAVAL();
                    }
                    else
                    {
                        tableName = "TB_TR136BBOBEDOC";
                        report = new RPT_TR136BBOBEDOC();
                    }

                    filename = "BuyConfirmation";
                    zipcon = "encypt";
                }

                else if (Reportname == "Series-Report")
                {
                    tableName = "TB_TR136DBOBERP";
                    report = new RPT_TR136DBOBERP();
                    zipcon = "compress";
                    filename = "SummaryReport";
                }
                else if (Reportname == "Series-SummaryDoc")
                {
                    tableName = "TB_TR136OBOBE";
                    report = new RPT_TR136OBOBE();
                    zipcon = "compress";
                    filename = "BuyCover";
                }
                 else if (Reportname == "Series-BE")
                {
                    tableName = "TB_TR136BOBE";
                    report = new RPT_TR136BOBE();
                    zipcon = "compress";
                    filename = "BillOfExchange";
                }
                else if (Reportname == "Series-ReportAll")
                {
                    tableName = "TB_TR136DBOBERPAL";
                    report = new RPT_TR136DBOBERPAL();
                    zipcon = "compress";
                    filename = "SummaryReportAll";
                }
 
                else if (Reportname == "Series-BalanceReport")
                {
                    tableName = "TB_TR136DBOBERPBL";
                    report = new RPT_TR136DBOBERPBL();
                    zipcon = "compress";
                    filename = "BalanceStock";
                }
                
                else if (Reportname == "Allotment-Confirmation")
                {
                    tableName = "TB_TR136SBOBEDOC";
                    report = new RPT_TR136SBOBEDOC();
                    zipcon = "encypt";
                    filename = "SellConfirmation";
                }
               
                else if (Reportname == "Series-AllotmentConfirm")
                {
                    tableName = "TB_TR136BBOBEALLOTCONF";
                    report = new RPT_TR136BBOBEALLOTCONF();
                    zipcon = "encypt";
                    filename = "BuyAllotment";
                }
                
                else if (Reportname == "Series-BuyPreConfirmationAval")
                {
                    tableName = "TB_TR136BPCAVAL";
                    report = new RPT_TR136BPCAVAL();
                    subTableName = "ALLOTMENT";
                    subReport = "ALLOT_SUB_RPT";

                    zipcon = "encypt";
                    filename = "BuyPre-Confirmation";
                }
                else if (Reportname == "Series-SellPreConfirmationAval")
                {
                    tableName = "TB_TR136SPCAVAL";
                    report = new RPT_TR136SPCAVAL();
                    subTableName = "ALLOTMENT";
                    subReport = "ALLOT_SELL_SUB_RPT";

                    zipcon = "encypt";
                    filename = "SellPre-Confirmation";
                }
                
                if(ds != null)
                {                  
/*
                    //FileManage.ClearDirectory(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + DateTime.Now.ToString("yyyyMMdd")), true);
                    prefixEncrypt = new string[ds.Tables[0].Rows.Count];
                    code = new string[ds.Tables[0].Rows.Count];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //Create folders
                        prefixEncrypt[i] = ds.Tables[0].Rows[i]["ENCRYPT"].ToString();
                        code[i] = ds.Tables[0].Rows[i]["CODE"].ToString();

                        FileManage.ManageDirectory(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + prefixEncrypt[i]));
                        
                        DataTable tb = ds.Tables[0].Clone();                        
                        DataRow row = ds.Tables[0].Rows[i];                        
                        tb.TableName = tableName;
                        tb.ImportRow(row);

                        //Save files to folder
                        ReportClass doc = report;
                        doc.SetDataSource(tb);
                        doc.SetParameterValue("parameterCopy", BECopyDDL.SelectedValue == "-" ? string.Empty : BECopyDDL.SelectedValue);
                        doc.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + prefixEncrypt[i]) + "/" + code[i] + "_" + filename + ".pdf");
                    }

                //Distinct folder to encrypt
                string[] uniqueItems = prefixEncrypt.Select(x => x.ToUpper()).Distinct<string>().ToArray();

                if (zipcon != null && zipcon.Equals("encypt")){
                    Encryption(filename, uniqueItems);   
                }else{
                    Compress(DateTime.Now.ToString("yyyyMMdd"));
                }
*/
                // ============== test =============

                prefixEncrypt = new string[ds.Tables[0].Rows.Count];
                code = new string[ds.Tables[0].Rows.Count];

                List<Attachment> attach = new List<Attachment>();

                if (zipcon != null && zipcon.Equals("encypt"))
                {
                    // Buy - Sell Confirmation
                    if (filename.Equals("BuyConfirmation") || filename.Equals("SellConfirmation"))
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            //Create folders
                            prefixEncrypt[i] = ds.Tables[0].Rows[i]["ENCRYPT"].ToString();
                            code[i] = ds.Tables[0].Rows[i]["CODE"].ToString();

                            FileManage.ManageDirectory(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + prefixEncrypt[i]));

                            DataTable tb = ds.Tables[0].Clone();
                            DataRow row = ds.Tables[0].Rows[i];
                            tb.TableName = tableName;
                            tb.ImportRow(row);

                            //Save files to folder
                            ReportClass doc = report;
                            doc.SetDataSource(tb);
                            doc.SetParameterValue("parameterCopy", BECopyDDL.SelectedValue == "-" ? string.Empty : BECopyDDL.SelectedValue);
                            doc.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + prefixEncrypt[i]) + "/" + code[i] + "_" + filename + ".pdf");

                        }

                        uniqueItems = prefixEncrypt.Select(x => x.ToUpper()).Distinct<string>().ToArray();
                        Encryption(filename, uniqueItems);

                        // Attach file to mail
                        foreach (string item in uniqueItems)
                        {
                            attach.Add(new Attachment(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/") + item + ".zip"));
                        }

                    }
                    // Other Confiramtion
                    else {

                        ReportClass doc = report;
                        DataTable tb = ds.Tables[0].Clone();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            prefixEncrypt[i] = ds.Tables[0].Rows[i]["ENCRYPT"].ToString();
                            code[i] = ds.Tables[0].Rows[i]["CODE"].ToString();

                            //Create folders                       
                            FileManage.ManageDirectory(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + prefixEncrypt[i]));

                            DataRow row = ds.Tables[0].Rows[i];
                            tb.TableName = tableName;
                            tb.ImportRow(row);
                            doc.SetDataSource(tb);                          
                        
                        }

                        if (Category.Equals("AVAL"))
                        {
                            doc = Model_RPT_TR136BOBE.SetSubReportData(Config.ConnectionString, ds, doc, Category, Reportname, Series, MDate);
                        }

                        uniqueItems = prefixEncrypt.Select(x => x.ToUpper()).Distinct<string>().ToArray();


                        //Save files to folder
                        foreach (string item in uniqueItems)
                        {
                            doc.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + item) + "/" + filename + ".pdf");
                        }
                        
                        Encryption(filename, uniqueItems);

                        // Attach file to mail
                        foreach (string item in uniqueItems)
                        {
                            attach.Add(new Attachment(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/") + item + ".zip"));
                        }
                    }

                        /* 
  
                          DataSet subds = Model_RPT_TR136BOBE.getData(Config.ConnectionString, subReport, Category, Series, MDate);
                                                       for (int j = 0; j < subds.Tables[0].Rows.Count; j++)
                                                       {
                                                           DataRow subrow = subds.Tables[0].Rows[j];
                                                           DataTable subtb = subds.Tables[0].Clone();
                                                           subtb.TableName = subTableName;
                                                           subtb.ImportRow(subrow);

                                                           ds.Tables.Add(subtb);
                                                           doc.SetDataSource(ds);
                                                           doc.Subreports[subReport].SetDataSource(ds);
  
                         */
                    
              
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FileManage.ManageDirectory(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + DateTime.Now.ToString("yyyyMMdd")));
                        DataTable tb = ds.Tables[0].Clone();
                        DataRow row = ds.Tables[0].Rows[i];
                        tb.TableName = tableName;
                        tb.ImportRow(row);

                        //Save files to folder
                        ReportClass doc = report;
                        doc.SetDataSource(tb);
                        //doc.SetParameterValue("parameterCopy", BECopyDDL.SelectedValue == "-" ? string.Empty : BECopyDDL.SelectedValue);
                        doc.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + DateTime.Now.ToString("yyyyMMdd") + "/" + filename + ".pdf"));
                    
                    }                    
                    
                    Compress(DateTime.Now.ToString("yyyyMMdd"));
                    // Attach file to mail
                    attach.Add(new Attachment(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/") + DateTime.Now.ToString("yyyyMMdd") + ".zip"));
                }
                


                // =================================
                    
                //Send mail to opertion
                //List<Attachment> attach = new List<Attachment>();
                //foreach (string item in uniqueItems) {                    
                //    attach.Add(new Attachment(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/") + item + ".zip"));
                //}
                //SendMail(prefixEncrypt, Config.GetParameterFile("136To"), Config.GetParameterFile("136Cc"), filename + DateTime.Now.ToString("yyyyMMdd"), attach);
                SendMail(Config.GetParameterFile("136To"), Config.GetParameterFile("136Cc"), filename + " Report " + DateTime.Now.ToString("ddMMyyyy"), attach);
                MessageBox.Text = "ระบบส่งไฟล์เรียบร้อยแล้ว";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Text = "ไม่สามารถส่งไฟล์ได้ กรุณาติดต่อผู้ดูแลระบบ";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคินภัทร จำกัด(มหาชน)',modal: true});", true);

                Log.Error(ex.Message);
            }
        }

        public void GenMaturityReport()
        {
            Log.Info("-----Go To GenMaturityReport ---- ");
            try
            {
                string Report = string.Empty; // ReportBox.SelectedValue.ToString();
                string Series = string.Empty; // SeriesBox.Text.Trim();
                string MDate = string.Empty; // MatFromDateBox.Text.Trim();
                string Category = string.Empty;
                DataSet ds = Model_RPT_TR136BOBE.getData(Config.ConnectionString, Report, Category, Series, MDate); 

                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    DataTable dcust = dt.DefaultView.ToTable(true, "CUSTODIAN");
                    string[] d = MDate.Split('/');
                    //DateTime DataDate = Convert.ToDateTime(MDate);
                    DateTime DataDate = DateTime.ParseExact(MDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    for (int j = 0; j < dcust.Rows.Count; j++)
                    {
                        string FileName = dcust.Rows[j]["CUSTODIAN"].ToString() + "_" + DataDate.ToString("yyyyMMdd");
                        DataTable dnew = new DataTable();
                        dnew.Columns.Add("BE_NO", typeof(string));
                        dnew.Columns.Add("BE_SERIES_CODE", typeof(string));
                        dnew.Columns.Add("ISSUE_DATE", typeof(string));
                        dnew.Columns.Add("MAT_DATE", typeof(string));
                        dnew.Columns.Add("BUYER", typeof(string));
                        dnew.Columns.Add("CUSTODIAN", typeof(string));
                        dnew.Columns.Add("AMOUNT", typeof(int));
                        dnew.Columns.Add("S_BESERIES", typeof(string));

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["CUSTODIAN"].Equals(dcust.Rows[j]["CUSTODIAN"]))
                            {
                                dnew.Rows.Add(dt.Rows[i]["BE_NO"], dt.Rows[i]["BE_SERIES_CODE"], dt.Rows[i]["ISSUE_DATE"], dt.Rows[i]["MAT_DATE"], dt.Rows[i]["BUYER"], dt.Rows[i]["CUSTODIAN"], dt.Rows[i]["AMOUNT"]);
                            }
                        }

                        //----------
                        DataTable s_beseries = dnew.DefaultView.ToTable(true, "BE_SERIES_CODE");
                        string show_beseries = "";
                        for (int test = 0; test < s_beseries.Rows.Count; test++)
                        {
                            if (test == 0 || test == s_beseries.Rows.Count)
                            {
                                show_beseries = show_beseries + s_beseries.Rows[test]["BE_SERIES_CODE"];
                            }
                            else
                            {
                                show_beseries = show_beseries + " , " + s_beseries.Rows[test]["BE_SERIES_CODE"];
                            }
                        }
                        Log.Info("show_beseries = " + show_beseries);
                        dnew.Rows[0]["S_BESERIES"] = show_beseries;
                        //------------

                        ReportDocument doc = new ReportDocument();
                        doc = (ReportDocument)(new RPT_TR136BOBEMAT());
                        ReportExport.GenerateFile(dnew, doc, "Excel", Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + DateTime.Now.ToString("yyyyMMdd")), FileName);

                        Log.Info("-----End Generate File----- " + j);
                    }
                    //Compress File
                    Compress(DateTime.Now.ToString("yyyyMMdd"));

                    //Send Mail
                    List<Attachment> attach = new List<Attachment>();
                    attach.Add(new Attachment(Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/") + DateTime.Now.ToString("yyyyMMdd") + ".zip"));
                    SendMail(Config.GetParameterFile("136To"), Config.GetParameterFile("136Cc"), "BE Maturity Report " + DateTime.Now.ToString("yyyyMMdd"), attach);

                    //Successful Message
                    MessageBox.Text = "ระบบส่งไฟล์เรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);

                    Session["ReportSource"] = rpt;
                    TheUrl = "../View/Viewer.aspx";
                    Page.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = "เกิดข้อผิดพลาด (" + ex.Message + ")";
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }
                
        public void Compress(string FolderName)
        {
            try
            {
                Log.Info(" TR136BOBESERIES Compress ");

                string Path = Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/");
                if (File.Exists(Path + "/" + DateTime.Now.ToString("yyyyMMdd") + ".zip"))
                    File.Delete(Path + "/" + DateTime.Now.ToString("yyyyMMdd") + ".zip");
                using (Package Zip = Package.Open(Path + DateTime.Now.ToString("yyyyMMdd") + ".zip", FileMode.OpenOrCreate))
                {
                    foreach (string F in Directory.GetFiles(Path + FolderName + "/", "*.*", SearchOption.AllDirectories))
                    {
                        string F1 = F.Replace(' ', '_'); //ZipPackage can't handle spaces in filenames.
                        Uri URI = new Uri(F1.Substring((Path + FolderName + "/").Length), UriKind.Relative);

                        PackagePart ZipPart = Zip.CreatePart(PackUriHelper.CreatePartUri(URI), MediaTypeNames.Application.Zip, CompressionOption.Maximum);
                        byte[] B = File.ReadAllBytes(F);
                        ZipPart.GetStream().Write(B, 0, B.Length);
                    }
                }

                //Clear File After Zip
                string ExportPath = Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + DateTime.Now.ToString("yyyyMMdd"));
                if (Directory.Exists(ExportPath))
                {
                    string[] f = Directory.GetFiles(ExportPath);
                    for (int l = 0; l < f.Length; l++)
                    {
                        File.Delete(f[l]);
                    }

                    Directory.Delete(ExportPath);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public void Encryption(string filename, string[] uniqueItems)
        {
            try
            {
                Log.Info(" TR136BOBESERIES Encryption ");
                string Path = Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/");                

                //Encrypt file with password
                for (int i = 0; i < uniqueItems.Length ; i++)
                {
                    string ExportPath = Server.MapPath("~/" + Config.GetParameterFile("136ExportPath") + "/" + uniqueItems[i]);
                    
                    if (File.Exists(Path + "/" + uniqueItems[i] + ".zip"))
                        File.Delete(Path + "/" + uniqueItems[i] + ".zip");
                    
                    
                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.UseUnicodeAsNecessary = true;  //utf-8
                        zip.Password = uniqueItems[i]  + Config.GetParameterFile("136ENCRYPT_BE");
                        zip.AddDirectory(ExportPath);

                        zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                        zip.Save(ExportPath + ".zip");
                    }
                    Log.Info("encrypt pass : " + uniqueItems[i] + Config.GetParameterFile("136ENCRYPT_BE"));
                    Log.Info("encrypt path : " + ExportPath);

                    //Clear File After Zip
                    if (Directory.Exists(ExportPath))
                    {
                        string[] f = Directory.GetFiles(ExportPath);
                        for (int l = 0; l < f.Length; l++)
                        {
                            File.Delete(f[l]);
                        }
                            Directory.Delete(ExportPath);
                    }
                }             
            }           
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void SendMail(string To, string Cc, string Subject, List<Attachment> Attach)
        {
            try
            {
                //Parameter
                string to = To;
                bool isHtml = true;
                string message = "Please kindly recheck and feedback." + "<br>";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(Config.GetParameterFile("136From"));
                    string[] tto = to.Split(',');
                    for (int i = 0; i < tto.Count(); i++)
                    {
                        mail.To.Add(new MailAddress(tto[i].Replace("\n", "")));
                    }
                    if (!Cc.Trim().Equals(""))
                    {
                        string[] ccto = Cc.Split(',');
                        for (int j = 0; j < ccto.Count(); j++)
                        {
                            mail.CC.Add(new MailAddress(ccto[j].Replace("\n", "")));
                        }
                    }
                    // Define the message
                    mail.Subject = Subject;
                    mail.IsBodyHtml = isHtml;
                    mail.Body = message;
                    foreach (Attachment a in Attach)
                    {
                        mail.Attachments.Add(a);
                    }

                    var mailclient = new SmtpClient();
                    mailclient.Host = Config.GetParameterFile("SMTP");
                    mailclient.Port = int.Parse(Config.GetParameterFile("SMTPPort"));
                    mailclient.EnableSsl = false;
                    mailclient.UseDefaultCredentials = true;

                    mailclient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw ex;
            }

        }
        

        #endregion
    }
}
