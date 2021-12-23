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
using System.Text.RegularExpressions;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR143PCONF : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR143PCONF));
        private DataTable brokTable = new DataTable();
        private List<string> broker = new List<string>();

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                string message = Config.GetParameterFile("PconfBrok");
                broker = message.Split(',').Select(p => p.Trim()).ToList<string>();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindBroker();

                GridPConf.PageIndexChanging += new GridViewPageEventHandler(GridPConf_PageIndexChanging);
                GridPConf.RowDataBound += new GridViewRowEventHandler(GridPConf_RowDataBound);                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindDetail(GridView gd, string SeriesID)
        {
            try
            {
                //gd.DataSource = BEAllotment.BEAllotmentList(Config.ConnectionString, int.Parse(SeriesID));
                //gd.DataBind();
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
                GridPConf.DataSource = PreConfirm.PreConfList(Config.ConnectionString, SearchDateBox.Text.Trim());
                GridPConf.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindBroker()
        {
            try
            {
                BrokerListBox.DataSource = broker;
                BrokerListBox.DataBind();

                BrokerBox.DataSource = broker;
                BrokerBox.DataBind();
            }
            catch (Exception ex)
            {

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

   
        void GridPConf_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string PConfID = GridPConf.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gd = e.Row.FindControl("GridPConf") as GridView;
                    BindDetail(gd, PConfID);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridPConf_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridPConf.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        
    
        /*Web Method*/
        [WebMethod(EnableSession = true)]
        public static object GetPreconf(int PCON_ID)
        {
            return PreConfirm.getPreConf(Config.ConnectionString, PCON_ID);
        }

        [WebMethod(EnableSession = true)]
        public static object AddPreconf(PreConfirm record)
        {
            Log.Info("Log add proconfirmation");
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            record.CREATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return PreConfirm.Create(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdPreconf(PreConfirm record)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            record.UPDATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return PreConfirm.Update(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeletePreconf(int PCON_ID)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            return PreConfirm.Delete(Config.ConnectionString, PCON_ID);
        }
    /*
        [WebMethod(EnableSession = true)]
        public object ClickFillin(int PCON_ID)
        {
            string pconfDetail = this.DeatilBox.Text;
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            return PreConfirm.Filldata(pconfDetail);

        } 
     */


        [WebMethod(EnableSession = true)]
        public static object ClickFillin(string DETAIL, string SELBROK, string SELTYPE, string SELNF)
        {
            string allCurr = Config.GetParameterFile("143Currency");

            return PreConfirm.Filldata(Config.ConnectionString, DETAIL, allCurr, SELBROK, SELTYPE, SELNF);
        }
    }
}
