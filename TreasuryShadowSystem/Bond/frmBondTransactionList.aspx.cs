using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryModel.Master;
using System.Data;
using System.Text;
using System.IO;
using TreasuryBusiness.ThirdParty;
//using TreasuryModel.Master;
using System.Web.UI.HtmlControls;
using log4net;

namespace TreasuryShadowSystem.Bond
{
    public partial class frmBondTransactionList : System.Web.UI.Page
    {
        public const string REPORT_USERCODE = "TRPTIBONTH";
        public string TheUrl = "";
        string ExportType = "";
        string PathDownload = "";
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frmBondTransactionList));

        DataSet PTIResultSession
        {
            get
            {
                ViewState["PTIResultSession"] = Model_MS_PTITRANS.get(Configuration.Config.ConnectionString, txtDealNo.Text, txtTransDate.Value);
                return (DataSet)ViewState["PTIResultSession"];
            }
            set
            {
                ViewState["PTIResultSession"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                GridData.RowCommand += new GridViewCommandEventHandler(GridData_RowCommand);
                GridData.PageIndexChanging += new GridViewPageEventHandler(GridData_PageIndexChanging);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridData.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                int row = Int32.Parse(e.CommandArgument.ToString());
                string key = GridData.DataKeys[row].Value.ToString();
                if (e.CommandName == "XML")
                {
                    ExportType = "XML";
                    GenerateXML(key);
                }
                else if (e.CommandName == "Print")
                {
                    ExportType = "PRINT";
                    GeneratePDF(key);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //BindGrid();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid(); 
                //Page.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void BindGrid()
        {
            try
            {
                GridData.DataSource = PTIResultSession;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void GenerateXML(string key)
        {
            try
            {
                DataTable dt = Model_MS_PTITRANS.getTransLogByID(Configuration.Config.ConnectionString, key);
                Model_MS_PTITRANS md = new Model_MS_PTITRANS();
                md.TransLogNo = dt.Rows[0]["TransLogNo"].ToString();
                md.TransLogDate = dt.Rows[0]["TransLogDate"].ToString();
                md.SecID = dt.Rows[0]["SecID"].ToString();
                md.PartialID = dt.Rows[0]["PartialID"].ToString();
                md.MsgType = dt.Rows[0]["MsgType"].ToString();
                md.Priority = dt.Rows[0]["Priority"].ToString();
                md.SenderRef = dt.Rows[0]["SenderRef"].ToString();
                md.RelateRef = dt.Rows[0]["RelateRef"].ToString();
                md.PlaceCode = dt.Rows[0]["PlaceCode"].ToString();
                md.SettDate = dt.Rows[0]["SettDate"].ToString();
                md.TradeDate = dt.Rows[0]["TradeDate"].ToString();
                md.PercentType = dt.Rows[0]["PercentType"].ToString();
                md.DealPrice = dt.Rows[0]["DealPrice"].ToString();
                md.ISIN = dt.Rows[0]["ISIN"].ToString();
                md.FaceAmt = dt.Rows[0]["FaceAmt"].ToString();
                md.AcctNo = dt.Rows[0]["AcctNo"].ToString();
                md.CashAcct = dt.Rows[0]["CashAcct"].ToString();
                md.BusinessType = dt.Rows[0]["BusinessType"].ToString();
                md.SettType = dt.Rows[0]["SettType"].ToString();
                md.SettPlace = dt.Rows[0]["SettPlace"].ToString();
                md.AgentBIC = dt.Rows[0]["AgentBIC"].ToString();
                md.AgentAcctNo = dt.Rows[0]["AgentAcctNo"].ToString();
                md.SettAmt = dt.Rows[0]["SettAmt"].ToString();

                PathDownload = Server.MapPath("~/Temporary/PTIDownload/PTI" + md.MsgType + md.SecID + md.SenderRef + ".xml");
                FileStream f = File.Create(PathDownload);
                Byte[] info = new UTF8Encoding(true).GetBytes(PTI.ExcelToXML(md));
                f.Write(info, 0, info.Length);
                f.Close();
                ExportFile("~/Temporary/PTIDownload/PTI" + md.MsgType + md.SecID + md.SenderRef + ".xml");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void GeneratePDF(string key)
        {
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenConfirmation", "window.open(\"../View/ReportViewer2.aspx?report=" + REPORT_USERCODE + "&p=" + key + "\");", true);
                //Response.Redirect("~/View/ReportViewer2.aspx?report=" + REPORT_USERCODE + "&p=" + key, false );
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void ExportFile(string str)
        {
            try
            {
                string result = Path.GetFileName(str);
                Response.ContentType = "application/xml";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + result);
                Response.TransmitFile(Server.MapPath(str));
                Response.End();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
