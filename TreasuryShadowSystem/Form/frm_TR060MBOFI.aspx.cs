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
    public partial class frm_TR060MBOFI : System.Web.UI.Page
    {
        public string TheUrl = "";
        public const string REPORT_USERCODE = "TR060MBOFI";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string branch = txtBranch.Text;

            TheUrl = "../View/ReportViewer2.aspx?report=" + REPORT_USERCODE;
            TheUrl += "&b=" + branch;
            Page.DataBind();
        }
    }
}
