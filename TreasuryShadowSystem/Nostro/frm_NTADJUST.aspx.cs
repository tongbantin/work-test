using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryReport.Report;
using KKB.Treasury.TreasuryModel.Master;
using TreasuryShadowSystem.Configuration;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Net.Mime;
using System.Net;
using System.Net.Mail;
using System.Web.Services;
using System.Globalization;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_NTADJUST : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_NTADJUST));

        public string[] prodadj = Config.GetParameterFile("NTADJPROD").Split(',');
        public DataTable acctCurr = new DataTable();
       

        protected void Page_Init(object sender, EventArgs e)
        {
            

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                    GridAdjTrans.PageIndexChanging += new GridViewPageEventHandler(GridAdjTrans_PageIndexChanging);
                    BindCurrency();
                    BindAdjProd();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridAdjTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                GridAdjTrans.PageIndex = e.NewPageIndex;
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
                GridAdjTrans.DataSource = NostroDaily.getAdjustList(Config.ConnectionString, SearchVDate.Value);
                GridAdjTrans.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindCurrency()
        {
            try
            {
                acctCurr = NostroDaily.getNostCurrency(Config.ConnectionString);

                DataView view = new DataView(acctCurr);
                CurrBox.DataSource = view.ToTable(true, "CCY"); ;
                CurrBox.DataTextField = "CCY";
                CurrBox.DataValueField = "CCY";
                CurrBox.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindAdjProd()
        {
            try
            {
                ProdTypeBox.DataSource = prodadj;
                ProdTypeBox.DataBind();
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
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        public static object SecAcctList(string CURRVAL)
        {
            
            List<KeyValuePair<string, string>> acctlist = NostroDaily.getNostAccount(Config.ConnectionString, CURRVAL);
            return new { records = acctlist };
        }

        [WebMethod(EnableSession = true)]
        public static object GetAdjustRow(int ADJ_ID)
        {
            return NostroDaily.getAdjustRow(Config.ConnectionString, ADJ_ID);
        }

        [WebMethod(EnableSession = true)]
        public static object AddNostroAdj(NostroDaily record)
        {
            Log.Info("Log add adjust nostro");
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            record.CREATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return NostroDaily.Create(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateNostroAdj(NostroDaily record)
        {
            Log.Info("Log update adjust nostro");
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            record.UPDATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return NostroDaily.Update(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteNostroAdj(int ADJ_ID)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            return NostroDaily.Delete(Config.ConnectionString, ADJ_ID);
        }

    }

}
