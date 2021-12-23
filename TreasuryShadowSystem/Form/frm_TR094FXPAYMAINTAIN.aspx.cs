using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using log4net;
using KKB.Treasury.TreasuryModel.Model;
using System.Data;
using System.Web.Services;
using TreasuryShadowSystem.Configuration;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR094FXPAYMAINTAIN : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR094FXPAYMAINTAIN));
        public string TheUrl = "";
        string reportname = "TR094FXPAYMAINTAIN";
        ReportClass rpt = new ReportClass();
        string LongFXText = "";
        List<string> DealNet = new List<string>();
        protected void Page_Init(object sender, EventArgs e)
        {

            GridFX.PageIndexChanging += new GridViewPageEventHandler(GridFX_PageIndexChanging);
            GridFX.RowCommand += new GridViewCommandEventHandler(GridFX_RowCommand);
            GridFX.RowDataBound += new GridViewRowEventHandler(GridFX_RowDataBound);
        }

        void GridFX_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    string prodtext = e.Row.Cells[4].Text.Trim() + "=" + e.Row.Cells[1].Text.Trim();
                    bool IsNetting = DealNet.Contains(e.Row.Cells[0].Text.Trim()); 
                    if (!LongFXText.Contains(prodtext) || IsNetting)
                        e.Row.Cells[11].Controls.RemoveAt(0);
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        void GridFX_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void GridFX_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridFX.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindGrid()
        {
            try
            {
                string FromDate = FromDateBox.Text;
                string ToDate = ToDateBox.Text;
                if (ToDateBox.Text.Trim().Equals(""))
                    ToDate = FromDateBox.Text;
                DealNet = Model_RPT_TR094OBOFXSW.getDealNet(Config.ConnectionString);
                DataSet ds = Model_RPT_TR094OBOFXSW.getFXDeal(Config.ConnectionString, CMNEBox.Text.Trim().ToUpper(), FromDate, ToDate, Config.GetParameterFile("139FXCCSPROD"));
                GridFX.DataSource = ds.Tables[0];
                GridFX.DataBind();
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
                LongFXText = Config.GetParameterFile("139FXCCSPROD");
            }
            catch (Exception ex)
            { 
            
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateGross(string DealNo, string Gross)
        {
            return Model_RPT_TR094OBOFXSW.UpdateGross(Config.ConnectionString, DealNo, Gross, Model_RPT_TR094OBOFXSW.IsExisting(Config.ConnectionString, DealNo));
        }

        [WebMethod(EnableSession = true)]
        public static object GetNettingDeal(string DealNo, string Cmne, string FromDate, string ToDate)
        {
            return Model_RPT_TR094OBOFXSW.getFXNettingDeal(Config.ConnectionString, DealNo, Cmne, FromDate, ToDate);
        }

        [WebMethod(EnableSession = true)]
        public static object NettingDeal(string NettingDeal, string CMNE, string DealNo)
        {
            return Model_RPT_TR094OBOFXSW.UpdatePostNet(Config.ConnectionString, NettingDeal, CMNE, DealNo, Model_RPT_TR094OBOFXSW.IsExisting(Config.ConnectionString, NettingDeal));
        }
    }
}
