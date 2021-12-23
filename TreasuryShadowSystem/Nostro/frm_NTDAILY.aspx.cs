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
using System.Text;


namespace TreasuryShadowSystem.Form
{
    public partial class frm_NTDAILY : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_NTDAILY));

        // for daily data
        public DataTable result = new DataTable("NOSTDAILY");
        public DataTable emaildaily = new DataTable("EMAILDAILY");

        public DataTable prvtemp = new DataTable();
        public DataTable tdtemp = new DataTable();
        public DataTable tmrtemp = new DataTable();
        public DataTable next2temp = new DataTable();

        public string prevbrprcdate, brprcdate, nextbrprcdate, next2date, next3date = "";
        public string showprevdate, showbrprcdate, shownextdate, shownext2date, shownext3date ="";
        public Boolean nextthaihly, next2thaihly = false;
        public string[] prodcode = Config.GetParameterFile("NTFXPROD").Split(',');        

        protected void Page_Init(object sender, EventArgs e)
        {
            GridDailyTrans.PageIndexChanging += new GridViewPageEventHandler(GridDailyTrans_PageIndexChanging);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            try
            {
                Log.Info("product code =" + prodcode);
                if (!IsPostBack)
                {
                    ViewState["Holiday"] = NostroDaily.getHolidayData(Config.ConnectionString);

                    DataTable dt = NostroDaily.getProcessDate(Config.ConnectionString);
                    prevbrprcdate = dt.Rows[0]["PREVBRANPRCDATE"].ToString();
                    brprcdate = dt.Rows[0]["BRANPRCDATE"].ToString();
                    nextbrprcdate = dt.Rows[0]["NEXTBRANPRCDATE"].ToString();

                    Log.Info("Nostro prevbrprcdate " + prevbrprcdate);
                    Log.Info("Nostro brprcdate " + brprcdate);
                    Log.Info("Nostro nextbrprcdate " + nextbrprcdate);

                    GetTable();


// Find Previous Day ---------------------------------
                        int i = 0;  // Check Weekend date
                        do
                        {
                            if (!VerifyWeekEnd(prevbrprcdate)){
                                break;
                            }else {
                                prevbrprcdate = DateTime.ParseExact(prevbrprcdate, "dd/MM/yyyy", CultureInfo.CurrentUICulture).AddDays(-1).ToString("dd/MM/yyyy",CultureInfo.CurrentUICulture);
                            }
                            i++;
                        } while (i < 5);


                        //DataTable prevhldy = verifyHoliday(prevbrprcdate);  // Check Holiday

// Find Today ------------------------------------------

                        ViewState["todayhldy"] = VerifyHoliday(brprcdate); // Check Holiday

// Find Tomorrow Day -----------------------------------
                    
                        int j = 0; // Check Weekend date
                        do
                        {
                            if (!VerifyWeekEnd(nextbrprcdate))
                            {
                                break;
                            }
                            else
                            {
                                nextbrprcdate = DateTime.ParseExact(nextbrprcdate, "dd/MM/yyyy", CultureInfo.CurrentUICulture).AddDays(1).ToString("dd/MM/yyyy", CultureInfo.CurrentUICulture);
                            }
                            j++;
                        } while (j < 5);

                        ViewState["nexthldy"] = VerifyHoliday(nextbrprcdate);  //Data Holiday
                        DataTable nexthldy = (DataTable)ViewState["nexthldy"]; 
                        nextthaihly = nexthldy.Select().ToList().Exists(row => row["CCY"].ToString().Trim().ToUpper() == "THB"); //Check THB Holiday

// Find Next Two Day -----------------------------------

                        next2date = DateTime.ParseExact(nextbrprcdate, "dd/MM/yyyy", CultureInfo.CurrentUICulture).AddDays(1).ToString("dd/MM/yyyy", CultureInfo.CurrentUICulture);
                      
                        int k = 0; // Check Weekend date
                        do
                        {
                            if (!VerifyWeekEnd(next2date))
                            {
                                break;
                            }
                            else
                            {
                                next2date = DateTime.ParseExact(next2date, "dd/MM/yyyy", CultureInfo.CurrentUICulture).AddDays(1).ToString("dd/MM/yyyy", CultureInfo.CurrentUICulture);
                            }
                            k++;
                        } while (j < 5);

                        ViewState["next2hldy"] = VerifyHoliday(next2date);  // Check Holiday


                    // Find Transactions of each date
                    DataTable prvdt = GetDataPrevDate(Config.ConnectionString , prevbrprcdate);
                    prvtemp = NostroDaily.getSumTrans(Config.ConnectionString, prevbrprcdate, prodcode); 
                    BankingPort(prvtemp, prevbrprcdate);

                    tdtemp = NostroDaily.getSumTrans(Config.ConnectionString, brprcdate, prodcode);
                    BankingPort(tdtemp, brprcdate);
                    DataTable tdtadj = NostroDaily.getAdjustList(Config.ConnectionString, brprcdate);  // adjust trans
                    DataTable tdtotal = SumTotalTrans(tdtemp, tdtadj);  // union fxdh and adjust trans 
                    DataTable tdgroup = GroupByCurrency(tdtotal);
                    DataTable tddt = new DataTable();
                    tddt = GetTransByDate(prvdt, tdgroup);
                    
                    tmrtemp = NostroDaily.getSumTrans(Config.ConnectionString, nextbrprcdate, prodcode);
                    BankingPort(tmrtemp, nextbrprcdate);
                    DataTable tmrtadj = NostroDaily.getAdjustList(Config.ConnectionString, nextbrprcdate);  // adjust trans
                    DataTable tmrtotal = SumTotalTrans(tmrtemp, tmrtadj);  // union fxdh and adjust trans 
                    DataTable tmrtgroup = GroupByCurrency(tmrtotal);
                    DataTable tmrdt = new DataTable();
                    tmrdt = GetTransByDate(tddt, tmrtgroup);

                    next2temp = NostroDaily.getSumTrans(Config.ConnectionString, next2date, prodcode);
                    BankingPort(next2temp, next2date);
                    DataTable next2adj = NostroDaily.getAdjustList(Config.ConnectionString, next2date);  // adjust trans
                    DataTable next2total = SumTotalTrans(next2temp, next2adj);  // union fxdh and adjust trans 
                    DataTable next2group = GroupByCurrency(next2total);
                    DataTable next2dt = new DataTable();
                    next2dt = GetTransByDate(tmrdt, next2group);

                    // Merge Transactions
                    var query = from aa in prvdt.AsEnumerable()
                                join bb in tddt.AsEnumerable() on new { M1 = aa.Field<string>("CURR"), M2 = aa.Field<string>("ACCTNO") } equals new { M1 = bb.Field<string>("CURR"), M2 = bb.Field<string>("ACCTNO") }
                                join cc in tmrdt.AsEnumerable() on new { N1 = bb.Field<string>("CURR"), N2 = bb.Field<string>("ACCTNO") } equals new { N1 = cc.Field<string>("CURR"), N2 = cc.Field<string>("ACCTNO") }
                                join dd in next2dt.AsEnumerable() on new { O1 = cc.Field<string>("CURR"), O2 = cc.Field<string>("ACCTNO") } equals new { O1 = dd.Field<string>("CURR"), O2 = dd.Field<string>("ACCTNO") }                                

                                select result.LoadDataRow(new object[]
                            {
                                aa.Field<string>("CURR"),
                                aa.Field<string>("BANK_STR"),
                                aa.Field<string>("ACCTNO"),
                                aa.Field<decimal>("BALANCE"),
                                bb.Field<decimal>("BALANCE"),
                                cc.Field<decimal>("BALANCE"),
                                dd.Field<decimal>("BALANCE"),
                                //ee.Field<decimal>("BALANCE"),

                            }, false);

                    result = query.CopyToDataTable();
                    ViewState["Sumnostro"] = result;
                 
                    DateTime dttemp = DateTime.ParseExact(prevbrprcdate, "dd/MM/yyyy", CultureInfo.CurrentUICulture);
                    showprevdate = dttemp.ToString("dd-MMM-yy", CultureInfo.CurrentUICulture);

                    GridViewNostro.Columns[4].HeaderText = showprevdate;
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
            result.Columns.Add("CURR", typeof(string));
            result.Columns.Add("BANKSTR", typeof(string));
            result.Columns.Add("ACCTNO", typeof(string));
            result.Columns.Add("BALD1", typeof(decimal));
            result.Columns.Add("BALD2", typeof(decimal));
            result.Columns.Add("BALD3", typeof(decimal));
            result.Columns.Add("BALD4", typeof(decimal));

            emaildaily.Columns.Add("CURR", typeof(string));
            emaildaily.Columns.Add("BANKSTR", typeof(string));
            //emaildaily.Columns.Add("ACCTNO", typeof(string));
            emaildaily.Columns.Add("BALD2", typeof(decimal));
            emaildaily.Columns.Add("BALD3", typeof(decimal));
            emaildaily.Columns.Add("BALD4", typeof(decimal));
        }

        private void GridDailyTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridDailyTrans.PageIndex = e.NewPageIndex;
                BindTrans();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void GridViewNostro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try {
             
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Get Nostro by costcenter
                    string currency = e.Row.Cells[1].Text;
                    string nosacct = e.Row.Cells[3].Text;

                    GridView gd = e.Row.FindControl("GridNostroByCost") as GridView;
                    gd.ShowHeader = false;
                    BindByCost(gd, currency, nosacct);

                    //Check THB holiday to show column in port
                    if (nextthaihly)
                    {
                        gd.Columns[4].Visible = true;
                    }
                    else {
                        gd.Columns[4].Visible = false;                    }
                    
                    //Set red color for minus data
                    string bal1 =  e.Row.Cells[4].Text;
                    string bal2 = (e.Row.Cells[5].Controls[0] as DataBoundLiteralControl).Text.Trim();
                    string bal3 = (e.Row.Cells[6].Controls[0] as DataBoundLiteralControl).Text.Trim();
                    string bal4 = (e.Row.Cells[7].Controls[0] as DataBoundLiteralControl).Text.Trim();

                    if (!bal1.Equals("") && Convert.ToDecimal(bal1) < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                    if (!bal2.Equals("") && Convert.ToDecimal(bal2) < 0)
                    {
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    }
                    if (!bal3.Equals("") && Convert.ToDecimal(bal3) < 0)
                    {
                        e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    }
                    if (!bal4.Equals("") && Convert.ToDecimal(bal4) < 0)
                    {
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                    }

                    //Set purple highlight for other currency holiday data
                    DataTable todayhldy = (DataTable)ViewState["todayhldy"];
                    if (todayhldy.Rows.Count > 0)
                    {
                        for (int i = 0; i < todayhldy.Rows.Count; i++)
                        {
                            if (todayhldy.Rows[i]["CCY"].ToString().Trim().Equals(e.Row.Cells[1].Text))
                            {
                                e.Row.Cells[5].BackColor = System.Drawing.Color.MediumPurple;
                            }
                        }
                    }

                    DataTable nexthldy = (DataTable)ViewState["nexthldy"];
                    if (nexthldy.Rows.Count > 0)
                    {
                        for (int i = 0; i < nexthldy.Rows.Count; i++)
                        {
                            if (nexthldy.Rows[i]["CCY"].ToString().Trim().Equals(e.Row.Cells[1].Text))
                            {
                                e.Row.Cells[6].BackColor = System.Drawing.Color.MediumPurple;
                            }
                        }
                    }
                    
                    DataTable next2hldy = (DataTable)ViewState["next2hldy"];
                    if (next2hldy.Rows.Count > 0)
                    {
                        for (int i = 0; i < next2hldy.Rows.Count; i++)
                        {
                            if (next2hldy.Rows[i]["CCY"].ToString().Trim().Equals(e.Row.Cells[1].Text))
                            {
                                e.Row.Cells[7].BackColor = System.Drawing.Color.MediumPurple;
                            }
                        }
                    }
                   

                    // SET TABLE TO EMAIL ====================
                    DataTable emailtemp = (DataTable)ViewState["Sumnostro"];
                    DataTable bycosttemp = (DataTable)ViewState["Bycost"];
                    int rowIndex = emailtemp.Rows.IndexOf(emailtemp.Select("CURR = '" + currency + "' AND ACCTNO = '" + nosacct + "'")[0]);
                    string emailBank = Convert.ToString(emailtemp.Rows[rowIndex]["BANKSTR"]);
                    decimal emailbal2 = Convert.ToDecimal(emailtemp.Rows[rowIndex]["BALD2"]);
                    decimal emailbal3 = Convert.ToDecimal(emailtemp.Rows[rowIndex]["BALD3"]);
                    decimal emailbal4 = Convert.ToDecimal(emailtemp.Rows[rowIndex]["BALD4"]);

                    emaildaily.Rows.Add(currency, emailBank, emailbal2, emailbal3, emailbal4);
                    emaildaily.Rows.Add("", bycosttemp.Rows[0].Field<string>("PORT"),  bycosttemp.Rows[0].Field<decimal>("T2"),
                               bycosttemp.Rows[0].Field<decimal>("T3"), bycosttemp.Rows[0].Field<decimal>("T4"));
                    
                    emaildaily.Rows.Add("", bycosttemp.Rows[1].Field<string>("PORT"), bycosttemp.Rows[1].Field<decimal>("T2"),
                               bycosttemp.Rows[1].Field<decimal>("T3"), bycosttemp.Rows[1].Field<decimal>("T4"));

                    // ======================================= 
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }            
        }

        protected void GridNostroByCost_RowDataBound(object sender, GridViewRowEventArgs e) {

            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Set red color for minus data
                    string t1 = e.Row.Cells[1].Text;
                    string t2 = e.Row.Cells[2].Text;
                    string t3 = e.Row.Cells[3].Text;
                    string t4 = e.Row.Cells[4].Text;

                    if (!t1.Equals("") && Convert.ToDecimal(t1) < 0)
                    {
                        e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    }
                    if (!t2.Equals("") && Convert.ToDecimal(t2) < 0)
                    {
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    }
                    if (!t3.Equals("") && Convert.ToDecimal(t3) < 0)
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    }
                    if (!t4.Equals("") && Convert.ToDecimal(t4) < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        
        }


        protected void GridViewNostroMail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    GridView gd = e.Row.FindControl("GridViewNostroMail") as GridView;

                    //Set purple highlight for ther currency holiday data  
                    DataTable todayhldy = (DataTable)ViewState["todayhldy"];
                    if (todayhldy.Rows.Count > 0)
                    {
                        for (int i = 0; i < todayhldy.Rows.Count; i++)
                        {
                            if (todayhldy.Rows[i]["CCY"].ToString().Trim().Equals(e.Row.Cells[0].Text))
                            {
                                e.Row.Cells[2].BackColor = System.Drawing.Color.MediumPurple;
                            }
                        }
                    }

                    DataTable nexthldy = (DataTable)ViewState["nexthldy"];
                    if (nexthldy.Rows.Count > 0)
                    {
                        for (int i = 0; i < nexthldy.Rows.Count; i++)
                        {
                            if (nexthldy.Rows[i]["CCY"].ToString().Trim().Equals(e.Row.Cells[0].Text))
                            {
                                e.Row.Cells[3].BackColor = System.Drawing.Color.MediumPurple;
                            }
                        }
                    }

                    DataTable next2hldy = (DataTable)ViewState["next2hldy"];
                    if (next2hldy.Rows.Count > 0)
                    {
                        for (int i = 0; i < next2hldy.Rows.Count; i++)
                        {
                            if (next2hldy.Rows[i]["CCY"].ToString().Trim().Equals(e.Row.Cells[0].Text))
                            {
                                e.Row.Cells[4].BackColor = System.Drawing.Color.MediumPurple;
                            }
                        }
                    }

                    //Set Trading/Banking row backgroud color
                    string port = e.Row.Cells[1].Text;
                    string lightgray = "#009ac7", midgray = "#332a86";
                    if(!port.Equals("") && port.Equals("Banking")){

                        e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml(lightgray);
                        e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml(lightgray);
                        e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml(lightgray);
                        e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml(lightgray);

                    }
                    else if (!port.Equals("") && port.Equals("Trading"))
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml(midgray);
                        e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml(midgray);
                        e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml(midgray);
                        e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml(midgray);
                    }

                    //Set Trading/Banking control row font color
                    int index = e.Row.RowIndex;
                    int chkrow = index % 3;
                    if (chkrow == 0){
                        e.Row.Cells[0].Font.Bold = true;
                        e.Row.Cells[1].Font.Bold = true;
                        e.Row.Cells[2].Font.Bold = true;
                        e.Row.Cells[3].Font.Bold = true;
                        e.Row.Cells[4].Font.Bold = true;

                    }
                    else { 
                        string white = "#FFFFFF";
                        e.Row.Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml(white);
                        e.Row.Cells[2].ForeColor = System.Drawing.ColorTranslator.FromHtml(white);
                        e.Row.Cells[3].ForeColor = System.Drawing.ColorTranslator.FromHtml(white);
                        e.Row.Cells[4].ForeColor = System.Drawing.ColorTranslator.FromHtml(white);
                        
                    }
                        

                    //Set red color for minus data
                    string bal2 = e.Row.Cells[2].Text;
                    string bal3 = e.Row.Cells[3].Text;
                    string bal4 = e.Row.Cells[4].Text;

                    if (!bal2.Equals("") && Convert.ToDecimal(bal2) < 0)
                    {
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                    }
                    if (!bal3.Equals("") && Convert.ToDecimal(bal3) < 0)
                    {
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    }
                    if (!bal4.Equals("") && Convert.ToDecimal(bal4) < 0)
                    {
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                    }


                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }        

        private DataTable GetDataPrevDate(string constring, string prevbrprcdate)
        {
            // call procedure KKB_SP_TSS_TR148NTDAILY
            DataTable prvdt = NostroDaily.getDataPrevDate(constring, prevbrprcdate);
            return prvdt;

        }

        private DataTable GetTransByDate(DataTable table1, DataTable table2)
        {
            DataTable resultdt = new DataTable();
            resultdt.Columns.Add("CURR", typeof(string));
            resultdt.Columns.Add("COST", typeof(string));
            resultdt.Columns.Add("ACCTNO", typeof(string));
            resultdt.Columns.Add("BALANCE", typeof(decimal));

            try {
                for (int i = 0; i < table1.Rows.Count; i++)
                {
                    decimal sumOfItems = 0;
                    for (int j = 0; j < table2.Rows.Count; j++)
                    {
                        if (table1.Rows[i]["CURR"].ToString().Trim().Equals(table2.Rows[j]["CURR"].ToString().Trim()) &&
                              table1.Rows[i]["ACCTNO"].ToString().Trim().Equals(table2.Rows[j]["ACCTNO"].ToString().Trim()))
                        {
                            sumOfItems = Convert.ToDecimal(table1.Rows[i]["BALANCE"]) + Convert.ToDecimal(table2.Rows[j]["BALANCE"]);
                        }
                    }

                    if (sumOfItems == 0)
                    {
                        DataRow dr = resultdt.NewRow();
                        dr["CURR"] = table1.Rows[i]["CURR"];
                        dr["COST"] = table1.Rows[i]["COST"];
                        dr["ACCTNO"] = table1.Rows[i]["ACCTNO"];
                        dr["BALANCE"] = table1.Rows[i]["BALANCE"];
                        resultdt.Rows.Add(dr);
                       
                    }
                    else
                    {
                        resultdt.Rows.Add(table1.Rows[i]["CURR"], table1.Rows[i]["COST"], table1.Rows[i]["ACCTNO"], sumOfItems);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return resultdt;
        }

        void BindGrid()
        {
            try
            {
               
                GridViewNostro.DataSource = (DataTable)ViewState["Sumnostro"]; 
                GridViewNostro.DataBind();

                //SET TABLE FOR EMAIL
                GridViewNostroMail.DataSource = emaildaily;
                GridViewNostroMail.DataBind();
                //GridViewNostroMail.Columns[3].HeaderText = showprevdate; 
 
                showbrprcdate = DateTime.ParseExact(brprcdate, "dd/MM/yyyy", CultureInfo.CurrentUICulture).ToString("dd-MMM-yy", CultureInfo.CurrentUICulture);
                LinkButton bal2 = (LinkButton)GridViewNostro.HeaderRow.Cells[5].FindControl("bal2");
                bal2.Text = showbrprcdate;
                bal2.CommandArgument = brprcdate;
                GridViewNostroMail.Columns[2].HeaderText = showbrprcdate; //for email


                shownextdate = DateTime.ParseExact(nextbrprcdate, "dd/MM/yyyy", CultureInfo.CurrentUICulture).ToString("dd-MMM-yy", CultureInfo.CurrentUICulture);
                LinkButton bal3 = (LinkButton)GridViewNostro.HeaderRow.Cells[6].FindControl("bal3");
                
                //Show THB Holiday
                if (nextthaihly)
                {
                    shownextdate = shownextdate + "<br>" + " (THB Holiday) ";
                    bal3.Text = shownextdate;
                    bal3.BackColor = System.Drawing.Color.MediumPurple;
                    bal3.CommandArgument = nextbrprcdate;

                    GridViewNostroMail.Columns[3].HeaderStyle.BackColor = System.Drawing.Color.MediumPurple; // for email
                }
                else {
                    bal3.Text = shownextdate;
                    bal3.CommandArgument = nextbrprcdate;
                    //nextday is not thai holiday hide next2day
                    this.GridViewNostro.Columns[7].Visible = false;

                    GridViewNostroMail.Columns[3].HeaderText = shownextdate.Replace("<br>", ""); // for email
                    //nextday is not thai holiday hide next2day
                    this.GridViewNostroMail.Columns[4].Visible = false; // for email
                
                }

                GridViewNostroMail.Columns[3].HeaderText = shownextdate.Replace("<br>",""); // for email

                shownext2date = DateTime.ParseExact(next2date, "dd/MM/yyyy", CultureInfo.CurrentUICulture).ToString("dd-MMM-yy", CultureInfo.CurrentUICulture);
                LinkButton bal4 = (LinkButton)GridViewNostro.HeaderRow.Cells[7].FindControl("bal4");
                DataTable next2hldy = (DataTable)ViewState["next2hldy"];
                if (next2hldy.Rows.Count > 0)
                {
                    for (int j = 0; j < next2hldy.Rows.Count; j++)
                    {
                        if (next2hldy.Rows[j]["CCY"].ToString().Trim().Equals("THB"))
                        {
                            shownext2date = shownext2date + "<br>" + "(THB Holiday) ";
                            bal4.Text = shownext2date;
                            bal4.BackColor = System.Drawing.Color.MediumPurple;
                            bal4.CommandArgument = next2date;
                            GridViewNostroMail.Columns[4].HeaderStyle.BackColor = System.Drawing.Color.MediumPurple; // for email
                            break;
                        }
                        else
                        {
                            bal4.Text = shownext2date;
                            bal4.CommandArgument = next2date;
                        }
                    }
                }
                else
                {
                    bal4.Text = shownext2date;
                    bal4.CommandArgument = next2date;
                }
                GridViewNostroMail.Columns[4].HeaderText = shownext2date.Replace("<br>", ""); ; // for email

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindTrans()
        {
            try
            {
                string TransDate = (string)ViewState["TransDate"];
                DataTable records = NostroDaily.getDailyTrans(Config.ConnectionString, TransDate, prodcode);
                //ViewState["count"] =
                count.Text = "Records >>> " + records.Rows.Count.ToString();

                GridDailyTrans.DataSource = records;
                GridDailyTrans.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindByCost(GridView gd, string currency, string nostacct)
        {
            DataTable bycost = new DataTable();
            bycost.Columns.Add("PORT", typeof(string));
            bycost.Columns.Add("T1", typeof(decimal));
            bycost.Columns.Add("T2", typeof(decimal));
            bycost.Columns.Add("T3", typeof(decimal));
            bycost.Columns.Add("T4", typeof(decimal));

            try
            {
                DataTable bankport = NostroDaily.getSumbyPort(Config.ConnectionString, prevbrprcdate, brprcdate, nextbrprcdate, next2date, next3date, currency, nostacct);
                
                //Check banking port 340 in Datatable
                Decimal prvbanking = 0 , tdtbanking = 0 , tmrbanking = 0 , next2banking = 0;

                /*
                //Previous 
                DataTable prvbankport = (DataTable)ViewState["Prvbankport"];
                if (prvbankport.Rows.Count > 0)
                {
                    for (int i = 0; i < prvbankport.Rows.Count; i++)
                    {
                        if (prvbankport.Rows[i].Field<string>("CURR").Equals(currency))
                            prvbanking = prvbankport.Rows[i].Field<decimal>("BALANCE");
                    }
                }
                
                //Today
                DataTable tdtbankport = (DataTable)ViewState["Tdtbankport"]; 
                if (tdtbankport.Rows.Count > 0) {
                    for (int i = 0; i < tdtbankport.Rows.Count; i++)
                    {
                        if (tdtbankport.Rows[i].Field<string>("CURR").Equals(currency))
                            tdtbanking = tdtbankport.Rows[i].Field<decimal>("BALANCE");
                    }
                }
                //Tomorrow
                DataTable tmrbankport = (DataTable)ViewState["Tmrbankport"];
                if (tmrbankport.Rows.Count > 0){
                    for (int i = 0; i < tmrbankport.Rows.Count; i++)
                    {
                        if (tmrbankport.Rows[i].Field<string>("CURR").Equals(currency))
                            tmrbanking = tmrbankport.Rows[i].Field<decimal>("BALANCE");
                    }
                }
                //Next2Days
                DataTable n2dbankport = (DataTable)ViewState["N2dbankport"];
                if (n2dbankport.Rows.Count > 0)
                {
                    for (int i = 0; i < n2dbankport.Rows.Count; i++)
                    {
                        if (n2dbankport.Rows[i].Field<string>("CURR").Equals(currency))
                            next2banking = n2dbankport.Rows[i].Field<decimal>("BALANCE");
                    }
                }  
                */
                
                //Show banking value
                Decimal bankt1 = bankport.Rows[0].Field<decimal>("SUMM") + prvbanking;
                Decimal bankt2 = bankport.Rows[1].Field<decimal>("SUMM") + prvbanking + tdtbanking;
                Decimal bankt3 = bankport.Rows[2].Field<decimal>("SUMM") + prvbanking + tdtbanking + tmrbanking;
                Decimal bankt4 = bankport.Rows[3].Field<decimal>("SUMM") + prvbanking + tdtbanking + tmrbanking + next2banking;

                bycost.Rows.Add("Banking", bankt1, bankt2, bankt3, bankt4);

                Log.Info("show port Banking");
                Log.Info("prvbanking = " + prvbanking); //previous
                Log.Info("tdtbanking = " + tdtbanking); //today
                Log.Info("tmrbanking = " + tmrbanking); //tomorrow 
                Log.Info("next2banking = " + next2banking); //next2day

                Log.Info(bankt1); //previous date
                Log.Info(bankt2); //today 
                Log.Info(bankt3); //tomorrow
                Log.Info(bankt4); //next2date
 
                //Show trading value
                DataTable total = (DataTable)ViewState["Sumnostro"]; 
                var query = from a in total.AsEnumerable()
                            where a.Field<string>("CURR") == currency
                            where a.Field<string>("ACCTNO") == nostacct
                            select a;

                DataTable totalbycurr = query.Any() ? query.CopyToDataTable() : total.Clone();

                if (totalbycurr.Rows.Count > 0)
                {
                    Decimal tradt1 = totalbycurr.Rows[0].Field<decimal>("BALD1") - bankt1;
                    Decimal tradt2 = totalbycurr.Rows[0].Field<decimal>("BALD2") - bankt2;
                    Decimal tradt3 = totalbycurr.Rows[0].Field<decimal>("BALD3") - bankt3;
                    Decimal tradt4 = totalbycurr.Rows[0].Field<decimal>("BALD4") - bankt4;

                    bycost.Rows.Add("Trading", tradt1, tradt2, tradt3, tradt4);

                }

                ViewState["Bycost"] = bycost;

                gd.DataSource = bycost;
                gd.DataBind();

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void show_Click(object sender, EventArgs e)
        {

            try
            {
                LinkButton btn = (LinkButton)(sender);
                string TransDate = btn.CommandArgument;
                ViewState["TransDate"] = TransDate;

                BindTrans();

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
                Response.AddHeader("content-disposition", "attachment;filename=DailyDetails.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    GridDailyTrans.AllowPaging = false;
                    this.BindTrans();

                    GridDailyTrans.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
    
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

        protected void GridToExcel(object sender, EventArgs e)
        {

            try
            {
              
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=DailyTrans.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    GridViewNostroMail.AllowPaging = false;
                    GridViewNostroMail.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();

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

        protected void SendMail(object sender, EventArgs e)
        {

            try {

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        GridViewNostroMail.RenderControl(hw);
                        //StringReader sr = new StringReader(sw.ToString());

                        string message  = @"<style>
                                            .GridViewMailStyle
                                            {
                                                font-family: Arial, Sans-Serif;
                                                font-size: 12px;                                                
                  
                                                                                          
                                            }
                                             .HeaderMailStyle th
                                            {
     
                                                color: #ffffff;
                                                font-weight: bold;
                                                background-color:#507CD1;
                                                text-align: center;
                                               
                                            }                                    
                                           
                                            </style><div style='color:Red;font-size:11px;'>* Purple Highlight is Currency Holiday Date</div>"
                                            + sw.ToString();

                        //Write excle file to path 
                        string Exportpath = Server.MapPath("~/" + Config.GetParameterFile("132ExportPath")) + "\\" + "dailynostro.xls";
                        System.IO.File.WriteAllText(Exportpath, message);                       

                        //Get file for sending mail
                        string FullPathFileName = Path.Combine(Server.MapPath("~/" + Config.GetParameterFile("132ExportPath")), "dailynostro.xls");
                        List<Attachment> attach = new List<Attachment>();
                        attach.Add(new Attachment(FullPathFileName));

                        MailMessage mail = new MailMessage();

                        mail.From = new MailAddress(Config.GetParameterFile("DailyNostroFrom"));
                        string to = Config.GetParameterFile("DailyNostroTo");
                        string cc = Config.GetParameterFile("DailyNostroCc");

                        string[] tto = to.Split(',');
                        for (int i = 0; i < tto.Count(); i++)
                        {
                            mail.To.Add(new MailAddress(tto[i].Replace("\n", "")));
                        }
                        if (!cc.Trim().Equals(""))
                        {
                            string[] ccto = cc.Split(',');
                            for (int j = 0; j < ccto.Count(); j++)
                            {
                                mail.CC.Add(new MailAddress(ccto[j].Replace("\n", "")));
                            }
                        }
                        // Define the message
                        mail.Subject = "Nostro&S.W.I.F.T_as_of_" + DateTime.Now.ToString("ddMMMyyyy hh:mm:ss tt");
                        mail.IsBodyHtml = true;
                        mail.Body = message;
                        
                        foreach (Attachment a in attach)
                        {
                            mail.Attachments.Add(a);
                        }

                        var mailclient = new SmtpClient();
                        mailclient.Host = Config.GetParameterFile("SMTP");
                        mailclient.Port = int.Parse(Config.GetParameterFile("SMTPPort"));
                        mailclient.EnableSsl = false;
                        mailclient.UseDefaultCredentials = true;

                        mailclient.Send(mail);

                        MessageBox.Text = "ระบบส่งไฟล์เรียบร้อยแล้ว";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        private bool VerifyWeekEnd(string verdate){
            bool weekend = false;

            try
            {
                DateTime dt = DateTime.ParseExact(verdate, "dd/MM/yyyy", CultureInfo.CurrentUICulture); //Convert.ToDateTime(verdate);
                DayOfWeek today = dt.DayOfWeek;

                if (today == DayOfWeek.Sunday || today == DayOfWeek.Saturday) {
                    weekend = true;
                }
            }
            catch (Exception ex){
                Log.Error(ex.Message);
            }
            return weekend;
        }

        private DataTable VerifyHoliday(string verdate)
        {

            DataTable data = (DataTable)ViewState["Holiday"];
            var query = from a in data.AsEnumerable()
                        where a.Field<string>("HOLIDAY") == verdate
                        select a;

            DataTable res = query.Any() ? query.CopyToDataTable() : data.Clone();           

            return res;
        }

        private DataTable SumTotalTrans(DataTable t1 , DataTable t2) {

            DataTable dt = new DataTable();

            try {                    
                    var query = t1.AsEnumerable()
                                .Union(t2.AsEnumerable())
                                .Select(x =>
                                     new
                                     {
                                         Curr = x["CURR"],
                                         Cost = x["COST"],
                                         Acctno = x["ACCTNO"],
                                         Amount = x["BALANCE"],
                                     }
                                 )
                                //.GroupBy(s => new { s.Curr, s.Acctno })
                                .GroupBy(s => new { s.Curr, s.Cost, s.Acctno })
                                .Select(g =>
                                        new
                                        {
                                            Curr = g.Key.Curr,
                                            Cost = g.Key.Cost,
                                            Acctno = g.Key.Acctno,
                                            Amount = g.Sum(x => Math.Round(Convert.ToDecimal(x.Amount), 2)),
                                        }
                                ).ToList();

                dt.Columns.Add("CURR", typeof(string));
                dt.Columns.Add("COST", typeof(string));
                dt.Columns.Add("ACCTNO", typeof(string));
                dt.Columns.Add("BALANCE", typeof(decimal));

                foreach (var item in query)
                {
                    //dt.Rows.Add(item.Curr, item.Acctno, item.Amount);
                    dt.Rows.Add(item.Curr, item.Cost, item.Acctno, item.Amount);
                }

            }catch (Exception ex){
                Log.Error(ex.Message);
            }
                return dt;  
            }

        private DataTable GroupByCurrency(DataTable total)
        {

            DataTable dt = new DataTable();

            try
            {
                var query = total.AsEnumerable()
                            .Select(x =>
                                 new
                                 {
                                     Curr = x["CURR"],
                                     Acctno = x["ACCTNO"],
                                     Amount = x["BALANCE"],
                                 }
                             )
                    //.GroupBy(s => new { s.Curr, s.Acctno })
                            .GroupBy(s => new { s.Curr, s.Acctno })
                            .Select(g =>
                                    new
                                    {
                                        Curr = g.Key.Curr,
                                        Acctno = g.Key.Acctno,
                                        Amount = g.Sum(x => Math.Round(Convert.ToDecimal(x.Amount), 2)),
                                    }
                            ).ToList();

                dt.Columns.Add("CURR", typeof(string));
                dt.Columns.Add("COST", typeof(string));
                dt.Columns.Add("ACCTNO", typeof(string));
                dt.Columns.Add("BALANCE", typeof(decimal));

                foreach (var item in query)
                {
                    //dt.Rows.Add(item.Curr, item.Acctno, item.Amount);
                    dt.Rows.Add(item.Curr, "" , item.Acctno, item.Amount);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return dt;
        }

        private void BankingPort(DataTable trans, string portdate)
        {
            DataTable res = new DataTable();
            try
            {
                //Filter with banking cost (340)
                var query = from a in trans.AsEnumerable()
                            where a.Field<string>("COST").Replace(" ", "") == "340" && a.Field<string>("CURR").Trim() != "THB"
                            select a;

                res = query.Any() ? query.CopyToDataTable() : trans.Clone();

                //insert to KKB_TB_NOSTROPORT
                if (res.Rows.Count > 0)
                {
                    for (int i = 0; i < res.Rows.Count; i++)
                    {
                        string curr = res.Rows[i].Field<string>("CURR");
                        string cost = res.Rows[i].Field<string>("COST").Trim();
                        string acctno = res.Rows[i].Field<string>("ACCTNO");
                        decimal amount = res.Rows[i].Field<decimal>("BALANCE");

                        Boolean result = NostroDaily.checkCurrPort(Config.ConnectionString, curr, cost, acctno, portdate);
                        if (result)
                        {
                            // Update KKB_TB_NOSTROPORT
                            NostroDaily.updCurrPort(Config.ConnectionString, curr, cost, acctno, portdate, amount);
                        
                        }else{
                            // Insert KKB_TB_NOSTROPORT
                            NostroDaily.insertCurrPort(Config.ConnectionString, curr, cost, acctno, portdate, amount);
                        }                      
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            //return res;
        }
    }


}
