using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KKB.Treasury.TreasuryModel.Model;
using KKB.Treasury.TreasuryModel.Master;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using System.Web.Services;

using log4net;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR143PCONFIG : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR143PCONFIG));
        //public DataTable dt = new DataTable();
        private DataTable brokTable = new DataTable();
        private List<string> broker = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string message = Config.GetParameterFile("PconfBrok");
                broker = message.Split(',').Select(p => p.Trim()).ToList<string>();                

                GridViewPconfig.PageIndexChanging += new GridViewPageEventHandler(GridViewPconfig_PageIndexChanging);
                GridViewPconfig.RowDataBound += new GridViewRowEventHandler(GridViewPconfig_RowDataBound);

               if (!IsPostBack)
                {
                    //ViewState["Schedule"] = Model_MS_MTMSCHEDULE.getScheduleData(Config.ConnectionString);
                   BindBroker();                                
                   BindGrid();
                }
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

 //               BrokerListBox.DataSource = broker;
 //               BrokerListBox.DataBind();

                BrokerBox.DataSource = broker;
                BrokerBox.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindGrid()
        {            
            try
            {
                GridViewPconfig.DataSource = Model_MS_PCONFIG.PConfigList(Config.ConnectionString);
                GridViewPconfig.DataBind();
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }             
        }
                 
        void GridViewPconfig_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string PConfID = GridViewPconfig.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gd = e.Row.FindControl("GridViewPconfig") as GridView;
                    //BindDetail(gd, PConfID);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridViewPconfig_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
             try
             {
                 GridViewPconfig.PageIndex = e.NewPageIndex;
                 BindGrid();
             }
             catch (Exception ex)
             {
                 Log.Error(ex.Message);
             }
             
        }

        protected void SearchButton_Click(object sender, EventArgs e)
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
        public static object AddConfPrecon(Model_MS_PCONFIG record)
        {
            Log.Info("Log add config for preconfirmation");
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
          
            return Model_MS_PCONFIG.Create(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeletePreconf(int PCONFIG_ID)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
            return Model_MS_PCONFIG.Delete(Config.ConnectionString, PCONFIG_ID);
        }

        [WebMethod(EnableSession = true)]
        public static object GetPConfig(int PCONFIG_ID)
        {
            return Model_MS_PCONFIG.getPConfig(Config.ConnectionString, PCONFIG_ID);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdPreconf(Model_MS_PCONFIG record)
        {
            if (HttpContext.Current.Session["UserName"] == null)
                return new { Result = "ERROR", Message = "Login Session Expired" };
           
            return Model_MS_PCONFIG.Update(Config.ConnectionString, record);
        }
    
    }
}
