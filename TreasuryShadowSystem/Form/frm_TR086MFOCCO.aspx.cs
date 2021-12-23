using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR086MFOCCO : System.Web.UI.Page
    {
        public string TheUrl = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Outstanding FX";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
            //    Response.Redirect("~/Login.aspx");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/View/ReportViewer2.aspx?report=TR049DBOFX&b=" + txtBranch.Text + "&e=" + txtExecuteDate.Value);
            TheUrl = "../View/ReportViewer2.aspx?report=TR086MFOCCO";
            TheUrl += "&b=" + txtBranch.Text + "&e=" + txtExecuteDate.Value + "&p=" + ddCost.SelectedValue;
            Page.DataBind();
        }
    }
}
