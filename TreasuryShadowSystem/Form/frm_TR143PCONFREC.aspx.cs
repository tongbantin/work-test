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

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR143PCONFREC : System.Web.UI.Page
    {
        public string TheUrl = "";
        ReportClass rpt = new ReportClass();
        string reportname = "TR143PCONFREC";
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR143PCONFREC));
        
        protected void Page_Load(object sender, EventArgs e)
        {         
        }       

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string[] d = DateBox.Text.Split('/');
                String DataDate = d[0] + "/" + d[1] + "/" + d[2];
                //String Swifttype = SwiftType.SelectedValue;

                rpt = Model_RPT_TR143PCONFREC.getReport(Config.ConnectionString, DataDate);
                if (rpt == null)
                {
                    DataSet ds = Model.Model_Tb_Master.getReportDataNotFound(reportname);
                    rpt = new RPT_MESSAGE();
                    rpt.SetDataSource(ds);
                }
                Session["ReportSource"] = rpt;

                TheUrl = "../View/Viewer.aspx";
                Page.DataBind();

            }
            catch (Exception ex)
            {

            }
        }   
      
    }
}
