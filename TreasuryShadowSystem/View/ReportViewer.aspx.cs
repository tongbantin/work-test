using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using log4net;
using CrystalDecisions.CrystalReports.Engine;
using KKB.Treasury.TreasuryModel.Model;
using TreasuryShadowSystem.Configuration;

namespace TreasuryShadowSystem.View
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        private ILog Log = log4net.LogManager.GetLogger(typeof(ReportViewer));

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {                    
                    ReportClass rpt = new ReportClass();
                    
                    switch (Request.QueryString["report"])
                    {
                        case "TR007MBOBE":
                            rpt = Model_RPT_TR007MBOBE.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["ex"]);
                            break;
                        case "TR008DBOBN":
                            rpt = Model_RPT_TR008DBOBN.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["ex"], Request.QueryString["t"]);
                            break;
                        case "TF020OBOPN":
                            rpt = Model_RPT_TF020OBOPN.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["d"], Request.QueryString["r"]);
                            break;
                        case "TC021OBOBE":
                            rpt = Model_RPT_TC021OBOBE.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["tf"], Request.QueryString["tt"], Request.QueryString["df"], Request.QueryString["dt"]);
                            break;
                        case "TC022OBOBE":
                            rpt = Model_RPT_TC022OBOBE.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["cp"], Request.QueryString["mf"]);
                            break;
                        case "TR023OBOBE":
                            rpt = Model_RPT_TR023OBOBE.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["ex"]);
                            break;
                        case "TR024MBOFI":
                            rpt = Model_RPT_TR024MBOFI.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["ex"], Request.QueryString["c"]);
                            break;
                        case "TR025OITAL":
                            rpt = Model_RPT_TR025OITAL.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["tf"], Request.QueryString["tt"], Request.QueryString["cs"], Request.QueryString["ss"], Request.QueryString["tps"]);
                            break;
                        case "TF026OBOTX":
                            rpt = Model_RPT_TF026OBOTX.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["d"]);
                            break;
                        case "TR027DFOEQ":
                            rpt = Model_RPT_TR027DFOEQ.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["ex"]);
                            break;
                        case "TR033DBOAL":
                            rpt = Model_RPT_TR033DBOAL.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["ex"]);
                            break;
                        case "TR034MFOEQ":
                            rpt = Model_RPT_TR034MFOEQ.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["ex"]);
                            break;
                        case "TR043OBOBN":
                            rpt = Model_RPT_TR043OBOBN.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR046OBOBN":
                            rpt = Model_RPT_TR046OBOBN.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR045OBORP":
                            rpt = Model_RPT_TR045OBORP.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TF048OBOBE":
                            rpt = Model_RPT_TF048OBOBE.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["d"]);
                            break;
                        case "TR049DBOFX":
                            rpt = Model_RPT_TR049DBOFX.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"]);
                            break;
                        case "TR050OBOBN":
                            rpt = Model_RPT_TR050OBOBN.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR051BONTH":
                            rpt = Model_RPT_TR051BONTH.getReport(Config.ConnectionString, Request.QueryString["d"], Request.QueryString["s"]);
                            break;
                        case "TR052OBORP":
                            rpt = Model_RPT_TR052OBORP.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR053OBOBN":
                            rpt = Model_RPT_TR053OBOBN.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR054OBOSW":
                            rpt = Model_RPT_TR054OBOSW.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR055OBOFX":
                            rpt = Model_RPT_TR055OBOFX.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR056OBODL":
                            rpt = Model_RPT_TR056OBODL.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR057DBOSL":
                            rpt = Model_RPT_TR057DBOSL.getReport(Config.ConnectionString, Request.QueryString["e"]);
                            break;
                        case "TR058DBOSL":
                            rpt = Model_RPT_TR058DBOSL.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR059MBOFI":
                            rpt = Model_RPT_TR059MBOFI.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"]);
                            break;
                        case "TR063DBOFI":
                            rpt = Model_RPT_TR063MBOAL.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"]);
                            break;
                    }

                    if (rpt != null)
                    {
                        CrystalViewer.ReportSource = rpt;                       
                        
                        Session["ReportSource"] = rpt;
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('Data Not Found');window.history.go(-1);</script>");
                    }        
                }
                else {
                    if (Session["ReportSource"] != null)
                    {
                        CrystalViewer.ReportSource = (ReportDocument)Session["ReportSource"];                         
                        CrystalViewer.DataBind();                        
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + " : " + ex.Data);
            }
            finally { 

            }
        }

    }
}
