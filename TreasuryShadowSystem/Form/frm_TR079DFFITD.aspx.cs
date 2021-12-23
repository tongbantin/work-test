﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR079DFFITD : System.Web.UI.Page
    {
        public string TheUrl = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Purchase Interest";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
                Response.Redirect("~/Login.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/View/ReportViewer2.aspx?report=TR063DBOFI&b=" + txtBranch.Text + "&e=" + txtExecuteDate.Value);
            TheUrl = "../View/ReportViewer2.aspx?report=TR079DFFITD";
            TheUrl += "&b=" + txtBranch.Text + "&s=" + txtFromDate.Value + "&f=" + txtToDate.Value;
            Page.DataBind();
        }
    }
}
