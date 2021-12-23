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
    using TreasuryShadowSystem.Collateral.Filters;
    
    public partial class CtrMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            try
            {
                string json = string.Empty;
                int Id = 0;
                CashFlowFilter filter = new CashFlowFilter();
                CASH_FLOW model = new CASH_FLOW();
                string Operator = Request.QueryString["Operator"];
                if (Request.Form.Count > 0) json = Request.Form[0];
                if (Request.QueryString["Id"] != null) Id = Convert.ToInt32(Request.QueryString["Id"]);

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
