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
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TreasuryShadowSystem.Collateral
{
    using TreasuryModel.Data.Collateral;
    
    public partial class CtrReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            try
            {
                string json = string.Empty;
                string Operator = Request.QueryString["Operator"];
                if (Request.Form.Count > 0) json = Request.Form[0];

                switch (Operator.ToUpper())
                {
                    case "GETALL":
                        break;
                    case "ADD":
                        break;
                    case "UPDATE":
                        break;
                    case "GETBYID":
                        break;
                    case "REMOVE":
                        break;

                }
            }
            catch (Exception ex)
            {
                //NotFound(ex);
            }

        }

    }
}
