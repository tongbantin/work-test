using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using TreasuryModel.Data;
using TreasuryShadowSystem.Configuration;
using log4net;
using TreasuryModel.Enum;
using System.Web.Services;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_BESeries : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_BESeries));
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GridBESeries.PageIndexChanging += new GridViewPageEventHandler(GridBESeries_PageIndexChanging);
                GridBESeries.RowDataBound += new GridViewRowEventHandler(GridBESeries_RowDataBound);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        
        void GridBESeries_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string SeriesID = GridBESeries.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gd = e.Row.FindControl("GridBEAllotment") as GridView;
                    BindDetail(gd, SeriesID);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindDetail(GridView gd, string SeriesID) {
            try
            {
                gd.DataSource = BEAllotment.BEAllotmentList(Config.ConnectionString, int.Parse(SeriesID));
                gd.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridBESeries_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridBESeries.PageIndex = e.NewPageIndex;
                BindGrid();
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

        private void BindGrid()
        {
            try
            {
                GridBESeries.DataSource = BESeries.BESeriesList(Config.ConnectionString, BESeriesSearchBox.Text.Trim(), MatFromDateBox.Text.Trim(), MatToDateBox.Text.Trim(), IssueFormDateBox.Text.Trim(), IssueToDateBox.Text.Trim());
                GridBESeries.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        /*Web Method*/
        [WebMethod(EnableSession = true)]
        public static object GetSeries(int BESeriesID)
        {
            return BESeries.getBESeries(Config.ConnectionString, BESeriesID);
        }

        [WebMethod(EnableSession = true)]
        public static object AddSeries(BESeries record)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            record.CREATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return  BESeries.Create(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object AddAvalSeries(BESeries record)
        {
            if (HttpContext.Current.Session["UserName"] == null)
            {
                return new { Result = "ERROR", Message = "Login Session Expired" };
            }
            record.CREATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return BESeries.Create(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateSeries(BESeries record)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            record.UPDATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return BESeries.Update(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteSeries(int BESeriesID)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            return BESeries.Delete(Config.ConnectionString, BESeriesID);
        }

        [WebMethod(EnableSession = true)]
        public static object GetAllotment(int BEAllotmentID)
        {
            return BEAllotment.getBEAllotment(Config.ConnectionString, BEAllotmentID);
        }

        [WebMethod(EnableSession = true)]
        public static object AddAllotment(BEAllotment record)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            record.CREATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return BEAllotment.Create(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateAllotment(BEAllotment record)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            record.UPDATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return BEAllotment.Update(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteAllotment(int BEAllotmentID)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            return BEAllotment.Delete(Config.ConnectionString, BEAllotmentID);
        }

        [WebMethod(EnableSession = true)]
        public static object RunningRefNo(int BESeriesID)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            //update selected ticket template before run refno.
            if (BEAllotment.UpdTemplateType(Config.ConnectionString, BESeriesID, "1") > 0)
            {
                return BEAllotment.RunningRefNo(Config.ConnectionString, BESeriesID);
            }
            else
            {
                return new { Result = "ERROR", Message = "Could not uppdate template type" };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object RunningRefNo2(int BESeriesID)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };

            //update selected ticket template before run refno.
            if (BEAllotment.UpdTemplateType(Config.ConnectionString, BESeriesID, "2") > 0)
            {
                return BEAllotment.RunningRefNo2(Config.ConnectionString, BESeriesID);
            }
            else
            {
                return new { Result = "ERROR", Message = "Could not uppdate template type" };
            }
        }


        // Validation
        [WebMethod(EnableSession = true)]
        public static object duplicateBESeries(string beseriesno, int beseriesid)
        {
            return BESeries.duplicateBESeries(Config.ConnectionString, beseriesno, beseriesid);
        }

        [WebMethod(EnableSession = true)]
        public static object duplicateBENo(string beno, int allotid)
        {
            return BESeries.duplicateBENo(Config.ConnectionString, beno, allotid);
        }

        [WebMethod(EnableSession = true)]
        public static object validateHoliday(DateTime date)
        {
            return BESeries.validateHoliday(Config.ConnectionString, date);
        }
    }
}
