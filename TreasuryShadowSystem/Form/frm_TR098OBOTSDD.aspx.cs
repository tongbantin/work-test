using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryModel.Master;
namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR098OBOTSDD : System.Web.UI.Page
    {
        public string TheUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
            //    Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                BindMonth();
                BindYear();
            }
        }

        private void BindMonth()
        {
            try
            {
                MontlyBox.DataSource = Model_MS_CALENDAR.GetMonths();
                MontlyBox.DataTextField = "MonthName";
                MontlyBox.DataValueField = "MonthID";
                MontlyBox.DataBind();

                MontlyBox.SelectedValue = DateTime.Now.Month.ToString() ;

            }
            catch (Exception ex)
            { 
            
            }
        }

        private void BindYear()
        {
            try
            {
                YearBox.DataSource = Model_MS_CALENDAR.GetYears();
                YearBox.DataTextField = "YearName";
                YearBox.DataValueField = "YearName";
                YearBox.DataBind();

                YearBox.SelectedValue = DateTime.Now.Year.ToString();
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TheUrl = "../View/ReportViewer2.aspx?report=TR098OBOTSDD";
            TheUrl += "&d=" + MontlyBox.SelectedValue + "/" + YearBox.SelectedValue + "&e=" + ExpenseBox.Text;
            Page.DataBind();
        }
    }
}
