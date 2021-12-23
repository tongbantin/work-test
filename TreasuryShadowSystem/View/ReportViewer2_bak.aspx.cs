using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Collections.Generic;
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
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;

//using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace TreasuryShadowSystem.View
{
    public partial class ReportViewer2 : System.Web.UI.Page
    {
        private ILog Log = log4net.LogManager.GetLogger(typeof(ReportViewer2));

        protected void Page_Init(object sender, EventArgs e)
        {
            string reportname = string.Empty;
            try
            {
                if (!IsPostBack)
                {
                    ReportClass rpt = new ReportClass();
                    List<ReportClass> listrpt = new List<ReportClass>();
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
                            rpt = Model_RPT_TR049DBOFX.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"], Request.QueryString["p"]);
                            break;
                        case "TR049DBOPF":
                            rpt = Model_RPT_TR049DBOPF.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"], Request.QueryString["p"]);
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
                        case "TR060MBOFI":
                            rpt = Model_RPT_TR060MBOFI.getReport(Config.ConnectionString, Request.QueryString["b"]);
                            break;
                        case "TR063DBOFI":
                            rpt = Model_RPT_TR063MBOAL.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"]);
                            break;
                        case "TR064DBORP":
                            rpt = Model_RPT_TR064DBORP.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR065DBOCM":
                            reportname = "Repo_Cash_Margin_" + Request.QueryString["d"].Replace("-","");
                            rpt = Model_RPT_TR065DBOCM.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR069FMALLPM":
                            rpt = Model_RPT_TR069FMALLPM.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"]);
                            break;
                        case "TR070DFOTD":
                            rpt = Model_RPT_TR070DFOTD.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"], Request.QueryString["s"]);
                            break;
                        case "TR072OFOCF":
                            rpt = Model_RPT_TR072OFOCF.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["s"], Request.QueryString["t"], Request.QueryString["o"]);
                            break;
                        case "TR073DBOFIDF":
                            rpt = Model_RPT_TR073DBOFIDF.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["f"], Request.QueryString["e"], Request.QueryString["o"]);
                            break;
                        case "TR074OBOFX":
                            rpt = Model_RPT_TR074OBOFX.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR075OBOFXSW":
                            rpt = Model_RPT_TR075OBOFXSW.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR076OBOFXSW":
                            rpt = Model_RPT_TR076OBOFXSW.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR077OBOFXGL":
                            rpt = Model_RPT_TR077OBOFXGL.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"]);
                            break;
                        case "TR078DFFISD":
                            rpt = Model_RPT_TR078DFFISD.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["s"], Request.QueryString["f"]);
                            break;
                        case "TR079DFFITD":
                            rpt = Model_RPT_TR079DFFITD.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["s"], Request.QueryString["f"]);
                            break;
                        case "TR080ACSWGL":
                            rpt = Model_RPT_TR080ACSWGL.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"]);
                            break;
                        case "TR081ACSWPR":
                            rpt = Model_RPT_TR081ACSWPR.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"]);
                            break;
                        case "TR082DBOBE":
                            rpt = Model_RPT_TR082DBOBE.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["d"], Request.QueryString["n"], Request.QueryString["i"], Request.QueryString["r"], Session["UserName"].ToString());
                            break;
                        case "TR083DBOBECF":
                            rpt = Model_RPT_TR083DBOBECF.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["c"], Request.QueryString["e"]);
                            break;
                        case "TR084DBOBERP":
                            rpt = Model_RPT_TR084DBOBERP.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["c"], Request.QueryString["f"], Request.QueryString["t"]);
                            break;
                        case "TR085DBOIRS":
                            rpt = Model_RPT_TR085DBOIRS.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"], Request.QueryString["p"]);
                            break;
                        case "TR086MFOCCO":
                            rpt = Model_RPT_TR086MFOCCO.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"], Request.QueryString["p"]);
                            break;
                        //case "TR089OTHMTM":
                        //    rpt = Model_RPT_TR089OTHMTM.getReport(Config.ConnectionString, "TMBAMF", "" , "" , "");
                        //    break;
                        case "TR090OBOFIBC":
                            rpt = Model_RPT_TR090OBOFIBC.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"], Request.QueryString["p"]);
                            break;
                        case "TR091OBOFXCF":
                            rpt = Model_RPT_TR091OBOFXCF.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR092OBOFXCF":
                            rpt = Model_RPT_TR092OBOFXCF.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR093OBOFXCF":
                            rpt = Model_RPT_TR093OBOFXCF.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        //Unused TR094 case
                        case "TR094OBOFXSW":
                            //rpt = Model_RPT_TR094OBOFXSW.getReport(Config.ConnectionString, Request.QueryString["e"]);
                            rpt = Model_RPT_TR094OBOFXSW.getReport(Config.ConnectionString, Request.QueryString["e"], Request.QueryString["p"]);
                            //rpt = Model_RPT_TR094OBOFXSW.getReport(Config.ConnectionString, Request.QueryString["e"], Request.QueryString["p"], Request.QueryString["f"], Request.QueryString["t"]);
                            break;
                        case "TR095OBOECAI":
                            rpt = Model_RPT_TR095OBOECAI.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"], Request.QueryString["p"]);
                            break;
                        case "TRPTIBONTH":
                            rpt = Model_RPT_TRPTIBONTH.getReport(Config.ConnectionString, Request.QueryString["p"], Request.QueryString["d"], Request.QueryString["r"], Request.QueryString["s"]);
                            break;
                        case "TR096MBOFIDB":
                            rpt = Model_RPT_TR096MBOFIDB.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["ex"], Request.QueryString["c"]);
                            break;
                        case "TR098OBOTSDD":
                            rpt = Model_RPT_TR098OBOTSDD.getReport(Config.ConnectionString, Request.QueryString["d"], Request.QueryString["e"]);
                            break;
                        case "TR099OBOBRFI":
                            rpt = Model_RPT_TR099OBOBRFI.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["d"]);
                            break;
                        // Unused TR100 Case
                        case "TR100OBOBRFX":
                            rpt = Model_RPT_TR100OBOBRFX.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["d"]);
                            break;
                        case "TR108DBOEMIR":
                            rpt = Model_RPT_TR108DBOEMIR.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["e"], Request.QueryString["g"], Request.QueryString["c"]);
                            break;
                        //case "TR109DBOEQTB":
                        //    rpt = Model_RPT_TR109DBOEQTB.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["c"], Request.QueryString["e"]);
                        //    break;
                        //case "TR110DBODVTB":
                        //    rpt = Model_RPT_TR110DBODVTB.getReport(Config.ConnectionString, Request.QueryString["b"], Request.QueryString["c"], Request.QueryString["e"]);
                        //    break;
                        case "TR113OBOFXCFC":
                            rpt = Model_RPT_TR113OBOFXCFC.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        //case "TR113OBOFXCFC":
                        //    rpt = Model_RPT_TR113OBOFXCFC.getReport(Config.ConnectionString, Request.QueryString["d"]);
                        //    break;
                        /*case "TR127OBOFXBDBCF":
                            rpt = Model_RPT_TR127OBOFXBDBCF.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR128OBOFXBDSCF":
                            rpt = Model_RPT_TR128OBOFXBDSCF.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;
                        case "TR129OBOFXBDFG":
                            rpt = Model_RPT_TR129OBOFXBDFG.getReport(Config.ConnectionString, Request.QueryString["d"]);
                            break;*/
                        //PTI 14/10/2015
                        case "TR051TAX":
                            rpt = Model_RPT_TR051TAX.getReport(Config.ConnectionString, Request.QueryString["t"], Request.QueryString["r"]);
                            break;
                        case "TR051TAXSUMMARY":
                            rpt = Model_RPT_TR051TAXSUMMARY.getReport(Config.ConnectionString, Request.QueryString["f"], Request.QueryString["t"]);
                            break;
                        case "TR114TO125OBOCF":
                            rpt = Model_RPT_TR114TO125OBOCF.getReport(Config.ConnectionString, Request.QueryString["d"], Request.QueryString["c"]);
                            break;
                        //Add 19-02-2016
                        case "TR130BOTANNUAL":
                            rpt = Model_RPT_TR130BOTANNUAL.getReport(Config.ConnectionString, Request.QueryString["s"], Request.QueryString["e"]);
                              break;
                        case "TR130BOTANNUALD":
                              rpt = Model_RPT_TR130BOTANNUALD.getReport(Config.ConnectionString, Request.QueryString["s"], Request.QueryString["e"]);
                              break;
                        case "TR130BOTQUARTER":
                              rpt = Model_RPT_TR130BOTQUARTER.getReport(Config.ConnectionString, Request.QueryString["s"], Request.QueryString["e"]);
                              break;
                        case "CONFIRMREPORT":

                              string symbol = Request.QueryString["symbol"];
                              string producttype = Request.QueryString["producttype"];
                             
                              listrpt = Model_RPT_CONFIRMREPORT.getReport(Config.ConnectionString, symbol, producttype); 
                              break;   

                        case "PRICINGREPORT":
                              string symbol1 = Request.QueryString["symbol"];
                              string producttype1 = Request.QueryString["producttype"];

                              listrpt = Model_RPT_PRICINGREPORT.getReport(Config.ConnectionString, symbol1, producttype1);
                              break;
                        case "SETTLEMENTREPORT":
                              string symbol2 = Request.QueryString["symbol"];
                              string producttype2 = Request.QueryString["producttype"];

                              listrpt = Model_RPT_SETTLEMENTREPORT.getReport(Config.ConnectionString, symbol2, producttype2);
                              break;
                        case "KNOCKOUTREPORT":
                              string symbol3 = Request.QueryString["symbol"];
                              string producttype3 = Request.QueryString["producttype"];

                              listrpt = Model_RPT_KNOCKOUTREPORT.getReport(Config.ConnectionString, symbol3, producttype3);
                             
                            break;
                        case "UNWINDREPORT":
                              string stnid = Request.QueryString["stnid"];
                              string symbol4 = Request.QueryString["symbol"];
                              string producttype4 = Request.QueryString["producttype"];

                              listrpt = Model_RPT_UNWINDREPORT.getReport(Config.ConnectionString, symbol4, producttype4);
                              break;   
                    }

                    if ((rpt != null) || (listrpt.Count > 0))
                    {
                        if (Request.QueryString["export"] != null)
                        {
                            if ((Request.QueryString["reportname"] != null) && (!Request.QueryString["reportname"].Equals("")))
                            {
                                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/Temporary/" +  Request.QueryString["reportname"] + ".pdf"));
                                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true); 
                            }
                        }
                        else if (Request.QueryString["isreport"] != null)
                        {
                            if ((Request.QueryString["reportname"] != null) && (!Request.QueryString["reportname"].Equals("")))
                            {

                                string docsymbol = Request.QueryString["symbol"];
                                string pathfolder = Server.MapPath("~/Temporary/STNReport/" + docsymbol+"/");

                                if (!Directory.Exists(pathfolder))
                                {
                                    Directory.CreateDirectory(pathfolder);
                                }


                                if (listrpt.Count > 0) {

                                    int runningno = 1;
                                
                                    foreach(ReportClass r in listrpt){

                                        rpt = new ReportClass();
                                        rpt = r;

                                        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(pathfolder + Request.QueryString["reportname"] + "_" + runningno + ".pdf"));
                                        //string url = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/Temporary/Report/" + Request.QueryString["reportname"] + "_" + runningno + ".pdf";
                                        //ClientScript.RegisterStartupScript(typeof(Page), "openPage" + runningno, "window.open('" + url + "','_blank');", true);

                                        runningno++;
                                    

                                        //Merge File                                        
                                        if (Request.QueryString["merge"] != null)
                                        {
                                            
                                            PDFMerger mergefile = new PDFMerger();
                                            mergefile.AddFile(Server.MapPath(Server.MapPath(pathfolder + "ConfirmReport_" + docsymbol + "_" + runningno + ".pdf")));
                                            mergefile.AddFile(Server.MapPath(Server.MapPath(pathfolder + Request.QueryString["reportname"] + "_" + runningno + ".pdf")));
                                            mergefile.DestinationFile = Server.MapPath(Server.MapPath(pathfolder + "ConfirmDocument_" + docsymbol + "_" + runningno + ".pdf"));
                                            mergefile.Execute();
                                            
                                        }


                                    }
                                
                                }


                            }


                        }
                        else
                        {

                            CrystalViewer.ReportSource = rpt;

                            if (!String.IsNullOrEmpty(reportname))
                                CrystalViewer.ID = reportname;

                            Session["ReportSource"] = rpt;
                        }
                    }
                    else
                    {
                        
                        if (Request.QueryString["isreport"] != null)
                        {
                            Session["ReportSource"] = rpt;

                            ClientScript.RegisterStartupScript(typeof(Page), "Alert", "alert('Data Not Found');", true);
                        }
                        else {

                            DataSet ds = new DataSet("DS_MESSAGE");
                            DataTable dt = new DataTable("TB_MESSAGE");
                            DataColumn dc1 = new DataColumn("REPORT");
                            DataColumn dc2 = new DataColumn("MESSAGE");
                            dt.Columns.Add(dc1);
                            dt.Columns.Add(dc2);
                            ds.Tables.Add(dt);
                            DataRow dr = dt.NewRow();
                            dr["REPORT"] = Request.QueryString["report"];
                            dr["MESSAGE"] = "Data Not Found";
                            dt.Rows.Add(dr);
                            rpt = new RPT_MESSAGE();
                            rpt.SetDataSource(ds);

                            CrystalViewer.ReportSource = rpt;

                            Session["ReportSource"] = rpt;

                        
                        }

                        //Response.Write("<script language='javascript'>alert('Data Not Found');window.history.go(-1);</script>");
                    }
                }
                else
                {
                    
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
            finally
            {

            }
        }
    }
}
