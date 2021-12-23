using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryReport.Report;

namespace TreasuryShadowSystem.View
{
    public partial class Viewer : System.Web.UI.Page
    {
        private ILog Log = log4net.LogManager.GetLogger(typeof(Viewer));
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                ReportDocument rpt = (ReportDocument)Session["ReportSource"];                 
                if ( rpt.SummaryInfo.ReportTitle!=null)
                    CrystalViewer.ID = rpt.SummaryInfo.ReportTitle;
                CrystalViewer.ReportSource = rpt;
                CrystalViewer.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + " : " + ex.Data);
            }
        }

    }
}
