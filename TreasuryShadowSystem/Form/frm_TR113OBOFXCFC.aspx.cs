using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR113OBOFXCFC : System.Web.UI.Page
    {
        public string TheUrl = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Purchase Interest";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
            //    Response.Redirect("~/Login.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TheUrl = "../View/ReportViewer2.aspx?report=TR113OBOFXCFC";
            TheUrl += "&d=" + txtDeal.Text;
            Page.DataBind();
        }
    }
}
