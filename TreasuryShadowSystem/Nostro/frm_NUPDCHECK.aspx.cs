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
using System.Drawing;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_NUPDCHECK : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_NUPDCHECK));
        public string[] prodcode = Config.GetParameterFile("NTFXPROD").Split(',');

        public DataTable result = new DataTable("NUPDCHK");

        //NUPD Trans
        DataTable nupdTemp = new DataTable();
        DataTable fxTemp = new DataTable();
        DataTable nupd = new DataTable("NUPD_TRANS");
        DataTable fx = new DataTable("FX_TRANS");
  
        protected void Page_Init(object sender, EventArgs e)
        {
            
            GridDetailNUPD.PageIndexChanging += new GridViewPageEventHandler(GridDetailNUPD_PageIndexChanging);           
           
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String today = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                Log.Info("Trade date = " + today);

                //For Test 
                //today = Config.GetParameterFile("TEST");

                todaydate.Text = "Trade Date : " + today.ToString();
               
                if (!IsPostBack)
                {                   
                    GetTable();

                    nupdTemp = NUPDCheck.getDataNUPD(Config.ConnectionString, today, prodcode);
                    nupd = GetSumNUPD(nupdTemp);
                    ViewState["nupdTemp"] = nupdTemp;

                    fxTemp = NUPDCheck.getDataFX(Config.ConnectionString, today, prodcode);
                    fx = GetSumFX(fxTemp);

                    /*
                    var query = from a in nupd.AsEnumerable()
                                join b in fx.AsEnumerable() on new { X1 = a.Field<string>("CURR"), X2 = a.Field<string>("ACCTNO") } equals new { X1 = b.Field<string>("CURR"), X2 = b.Field<string>("ACCTNO") }
                                where a.Field<string>("CURR") != "THB"
                                orderby a.Field<string>("CURR")
                                
                                select result.LoadDataRow(new object[]
                            {
                                a.Field<string>("CURR"),
                                a.Field<string>("ACCTNO"),
                                a.Field<int>("COUNT"),
                                a.Field<double>("AMOUNT").ToString("N", CultureInfo.InvariantCulture),
                                b.Field<double>("AMOUNT").ToString("N", CultureInfo.InvariantCulture),
                                (a.Field<double>("AMOUNT") - b.Field<double>("AMOUNT")).ToString("N", CultureInfo.InvariantCulture),                               
                            }, false); 
                     */

                    var query = from a in nupd.AsEnumerable()
                                join b in fx.AsEnumerable() on new { X1 = a.Field<string>("CURR"), X2 = a.Field<string>("ACCTNO") } equals new { X1 = b.Field<string>("CURR"), X2 = b.Field<string>("ACCTNO") } into lj        
                                from b in lj.DefaultIfEmpty()                               
                                orderby a.Field<string>("CURR")

                                select result.LoadDataRow(new object[]
                            {
                                a.Field<string>("CURR"),
                                a.Field<string>("ACCTNO"),
                                a.Field<int>("COUNT"),
                                Convert.ToDouble(a.Field<string>("AMOUNT")).ToString("N", CultureInfo.InvariantCulture),
                                b == null ? "-" : Convert.ToDouble(b.Field<string>("AMOUNT")).ToString("N", CultureInfo.InvariantCulture) ,
                                b == null ? (Convert.ToDouble(a.Field<string>("AMOUNT"))-Convert.ToDouble("0")).ToString("N", CultureInfo.InvariantCulture) : (Convert.ToDouble(a.Field<string>("AMOUNT"))-Convert.ToDouble(b.Field<string>("AMOUNT"))).ToString("N", CultureInfo.InvariantCulture),                                                             
                            }, false);


                    result = query.Any() ? query.CopyToDataTable() : nupdTemp.Clone();
                    result.Rows.Add("", "Total", result.Compute("Sum(COUNT)", ""), "", "");

                    ViewState["ShowNUPD"] = result;               
                    BindGrid();
                    
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void GetTable()
        {
            // New DataTable for show result
            result.Columns.Add("CURR", typeof(string));
            result.Columns.Add("ACCTNO", typeof(string));
            result.Columns.Add("COUNT", typeof(int));
            result.Columns.Add("NUPDAMT", typeof(string));
            result.Columns.Add("FXAMT", typeof(string));
            result.Columns.Add("DIFF", typeof(string));

        }

        private void GridDetailNUPD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridDetailNUPD.PageIndex = e.NewPageIndex;
                BindDetail();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        double totaldiff = 0;
        protected void GridDiff_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Loop thru each data row 
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totaldiff += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "AMOUNT"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Diff Total  ";
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Style.Add("padding-right", "5px");
                e.Row.Cells[5].Font.Bold = true;

                e.Row.Cells[6].Text = totaldiff.ToString("N", CultureInfo.InvariantCulture)+"  ";
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Style.Add("padding-right", "5px");
                e.Row.Cells[6].Font.Bold = true;
            }
        }

        private DataTable GetSumNUPD(DataTable record)
        {
            DataTable dt = new DataTable();
            try
            {
                
                //Group and Sum by currency,accountno
                var queryFinal = record.AsEnumerable().Select(x =>
                                 new
                                 {
                                     Curr = x["CURR"],
                                     Acctno = x["ACCTNO"],
                                     Amount = x["AMOUNT"],

                                 }
                                )
                              .GroupBy(s => new { s.Curr, s.Acctno })
                              .Select(g =>
                                 new
                                 {
                                     Curr = g.Key.Curr,
                                     Acctno = g.Key.Acctno,
                                     Count = g.Count(),
                                     Amount = g.Sum(x => Math.Round(Convert.ToDouble(x.Amount), 2)).ToString("0.##"),


                                 }
                                ).ToList();
                                
                dt.Columns.Add("CURR", typeof(string));
                dt.Columns.Add("ACCTNO", typeof(string));
                dt.Columns.Add("COUNT", typeof(int));
                dt.Columns.Add("AMOUNT", typeof(string));

                foreach (var item in queryFinal)
                {
                    dt.Rows.Add(item.Curr, item.Acctno, item.Count, item.Amount);
                }
            }
            catch(Exception ex) {
                Log.Error("----- GetDataNUPD Error-----");
                Log.Error(ex.Message);
            }

            return dt;
        }

        private DataTable GetSumFX(DataTable record) {

            DataTable dt = new DataTable();

            try
            {
                //Group and Sum by currency,accountno
                var queryFinal = record.AsEnumerable().Select(x =>
                                     new
                                     {
                                         Curr = x["CURR"],
                                         Acctno = x["ACCTNO"],
                                         Amount = x["AMOUNT"],

                                     }
                                    )
                                  .GroupBy(s => new { s.Curr, s.Acctno })
                                  .Select(g =>
                                     new
                                     {
                                         Curr = g.Key.Curr,
                                         Acctno = g.Key.Acctno,
                                         Count = g.Count(),
                                         Amount = g.Sum(x => Math.Round(Convert.ToDouble(x.Amount), 2)).ToString("0.##"),


                                     }
                                    ).ToList();


                dt.Columns.Add("CURR", typeof(string));
                dt.Columns.Add("ACCTNO", typeof(string));
                dt.Columns.Add("COUNT", typeof(int));
                dt.Columns.Add("AMOUNT", typeof(string));

                foreach (var item in queryFinal)
                {
                    dt.Rows.Add(item.Curr, item.Acctno, item.Count, item.Amount);
                }
               

            }
            catch (Exception ex)
            {
                Log.Error("----- GetDataFX Error-----");
                Log.Error(ex.Message);
            }

            return dt;
        
        }

        void BindGrid()
        {
            try
            {
                GridCheckNUPD.DataSource = (DataTable)ViewState["ShowNUPD"];
                GridCheckNUPD.DataBind();

                if (GridCheckNUPD.Rows.Count > 0)
                {
                    GridCheckNUPD.Rows[GridCheckNUPD.Rows.Count - 1].Font.Bold = true;
                    GridCheckNUPD.Rows[GridCheckNUPD.Rows.Count - 1].ForeColor = Color.DarkGreen;
                }
                                   
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
      
        void BindDetail()

                {
                    try
                    {
                        DataTable records = (DataTable)ViewState["ShowDetail"];
                        
                        GridDetailNUPD.DataSource = records;
                        GridDetailNUPD.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }

        void BindDiff()
        {
            try
            {
                DataTable records = (DataTable)ViewState["ShowDiff"];

                GridDiff.DataSource = records;
                GridDiff.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        
        protected void show_Detail(object sender, CommandEventArgs e)
        {

            try
            {

                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;

                DataTable showNUPD = (DataTable)ViewState["ShowNUPD"];
                string curr = (string)showNUPD.Rows[index][0];
                string acct = (string)showNUPD.Rows[index][1];
                Log.Info("row/curr/acct : "+index+"/"+acct+ "/" +curr);

                DataTable findRec = (DataTable)ViewState["nupdTemp"];
                
                var query = findRec.AsEnumerable();
                if(!curr.Equals("") && !acct.Equals("")){
                    query = query.Where(r => (r.Field<string>("CURR").Trim()).Equals(curr) && (r.Field<string>("ACCTNO").Trim()).Equals(acct));
                }

                query = query.OrderBy(r => r.Field<string>("DEALNO"));

                ViewState["ShowDetail"] = query.CopyToDataTable();
                BindDetail();

                showdetail.Style["display"] = "block";
                showdiff.Style["display"] = "none";
                //GridDetailNUPD.Style["display"] = "block";
                //GridDiff.Style["display"] = "none";

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        protected void show_Diff(object sender, CommandEventArgs e)
        {

            try
            {
                string thisyear = DateTime.Now.ToString("yy", CultureInfo.InvariantCulture);
                string lastyear = (Int32.Parse(thisyear) - 1).ToString();
                string conthis = thisyear + "9";
                string conlast = lastyear + "9";

                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;

                DataTable showNUPD = (DataTable)ViewState["ShowNUPD"];
                string curr = (string)showNUPD.Rows[index][0];
                string acct = (string)showNUPD.Rows[index][1];
                Log.Info("row/curr/acct : " + index + "/" + acct + "/" + curr);

                DataTable findRec = (DataTable)ViewState["nupdTemp"];

                var query = findRec.AsEnumerable()
                            .Where(x => x.Field<string>("DEALNO").StartsWith(conthis) || x.Field<string>("DEALNO").StartsWith(conlast));
                if (!curr.Equals("") && !acct.Equals(""))
                {
                    query = query.Where(r => (r.Field<string>("CURR").Trim()).Equals(curr) && (r.Field<string>("ACCTNO").Trim()).Equals(acct));
                }

                ViewState["ShowDiff"] = query.Any() ? query.CopyToDataTable() : findRec.Clone();
                BindDiff();

                showdetail.Style["display"] = "none";
                showdiff.Style["display"] = "block";
                //GridDetailNUPD.Style["display"] = "none";
                //GridDiff.Style["display"] = "block";

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        protected void ExportToExcel(object sender, EventArgs e)
        {

            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=NUPDDetail.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    GridDetailNUPD.AllowPaging = false;
                    this.BindDetail();

                    GridDetailNUPD.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    //Response.End();

                    //Response.Redirect("~/Form/frm_TR148NTDAILY.aspx", false);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {
                Response.End();
            }

        }
        
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

    }

}
