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
    public class Opics : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DealNoJson(string deal)
        {
            DataTable dt = TreasuryModel.Master.Model_MS_BOND.getBondByDealNo(Configuration.Config.ConnectionString, deal);
            AjaxJson[] e = new AjaxJson[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                e[i] = new AjaxJson();
                e[i].value = dt.Rows[i]["FaceAmt"].ToString();
                e[i].label = dt.Rows[i]["DealNo"].ToString();
                e[i].desc = dt.Rows[i]["DealDate"].ToString();
                e[i].icon = dt.Rows[i]["SettDate"].ToString();
                e[i].total = dt.Rows[i]["Total"].ToString();
                e[i].paytype = dt.Rows[i]["PAYTYPE"].ToString();
            }

            return new JavaScriptSerializer().Serialize(e);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CustBicJson(string name)
        {
            DataTable dt = TreasuryModel.Master.Model_MS_CUSTOMER.GetCustomerBIC(Configuration.Config.ConnectionString, name);
            AjaxJson[] e = new AjaxJson[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                e[i] = new AjaxJson();
                e[i].value = dt.Rows[i]["BIC"].ToString();
                e[i].label = dt.Rows[i]["SN"].ToString();
                e[i].desc = dt.Rows[i]["CMNE"].ToString();
                e[i].icon = dt.Rows[i]["NAME"].ToString();
                e[i].total = "0";
            }

            return new JavaScriptSerializer().Serialize(e);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SecIDJson(string secid)
        {
            DataTable dt = TreasuryModel.Master.Model_MS_BOND.getSecID(Configuration.Config.ConnectionString, secid);
            AjaxJson[] e = new AjaxJson[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                e[i] = new AjaxJson();
                e[i].value = dt.Rows[i]["SECID"].ToString();
                e[i].label = dt.Rows[i]["SECID"].ToString();
            }

            return new JavaScriptSerializer().Serialize(e);
        }
    }

    public class AjaxJson
    {
        public string value { get; set; } 
        public string label { get; set; }
        public string desc { get; set; }
        public string icon { get; set; }
        public string total { get; set; }
        public string paytype { get; set; }
    }
}
