using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

using System.Data;

namespace TreasuryShadowSystem.Services
{
    /// <summary>
    /// Summary description for Opics
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Opicinf : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DebentureCustomer(string name)
        {
            DataTable dt = TreasuryModel.Data.Model_MS_Debenture.getDebentureCustomer(Configuration.Config.ConnectionString, name);
            AjaxJson[] e = new AjaxJson[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                e[i] = new AjaxJson();
                e[i].value = dt.Rows[i]["CustID"].ToString();
                e[i].label = dt.Rows[i]["NameThai"].ToString();
                e[i].desc = dt.Rows[i]["CustID"].ToString();
                e[i].icon = dt.Rows[i]["CustID"].ToString();
                e[i].total = dt.Rows[i]["CustID"].ToString();
            }

            return new JavaScriptSerializer().Serialize(e);
        }
    }

}
