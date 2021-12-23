using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR130BOTANNUAL : System.Web.UI.Page
    {
        public string TheUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
            //    Response.Redirect("~/Login.aspx");
            BindYear();
        }

        void BindYear()
        {
            for (int i = DateTime.Now.Year; i >= 2012; i--)
            {
                YearBox.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Choose Report
            string Report = "TR130BOTQUARTER";
            if (RangeTimeBox.SelectedValue.Equals("SummaryYear"))
                Report = "TR130BOTANNUAL";
            
            if (TypeBox.SelectedValue.Equals("Transaction"))
                Report = "TR130BOTANNUALD";

            //Choose Range
            string StartDate = "01/01/" + YearBox.SelectedValue;
            string EndDate = "31/12/" + YearBox.SelectedValue;
            if (RangeTimeBox.SelectedValue.Equals("Quarter1"))
            {
                StartDate = "01/01/" + YearBox.SelectedValue;
                EndDate = "31/03/" + YearBox.SelectedValue;
            }
            else if (RangeTimeBox.SelectedValue.Equals("Quarter2"))
            {
                StartDate = "01/04/" + YearBox.SelectedValue;
                EndDate = "30/06/" + YearBox.SelectedValue;
            }
            else if (RangeTimeBox.SelectedValue.Equals("Quarter3"))
            {
                StartDate = "01/07/" + YearBox.SelectedValue;
                EndDate = "30/09/" + YearBox.SelectedValue;
            }
            else if (RangeTimeBox.SelectedValue.Equals("Quarter4"))
            {
                StartDate = "01/10/" + YearBox.SelectedValue;
                EndDate = "31/12/" + YearBox.SelectedValue;
            }
            
            TheUrl = "../View/ReportViewer2.aspx?report=" + Report;
            TheUrl += "&s=" + StartDate + "&e=" + EndDate;
            Page.DataBind();
        }
    }
}
