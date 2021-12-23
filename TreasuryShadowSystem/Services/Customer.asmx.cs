using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Data;
using TreasuryModel.Data;

namespace TreasuryShadowSystem.Services
{
    /// <summary>
    /// Summary description for Customer
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Customer : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CustomerJson(string cust)
        {
            DataTable dt = TreasuryModel.Data.Customer.getCustomerForServices(Configuration.Config.ConnectionString, cust);
            JsonData[] e = new JsonData[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                e[i] = new JsonData();
                e[i].value = dt.Rows[i]["CustomerID"].ToString().Trim();
                e[i].label = dt.Rows[i]["NameEn"].ToString().Trim();
                e[i].desc = dt.Rows[i]["NameEn"].ToString().Trim();
                e[i].icon = dt.Rows[i]["AssetCompany"].ToString().Trim();
                e[i].incvat = dt.Rows[i]["IncVat"].ToString().Trim();
                e[i].Bank = dt.Rows[i]["TargetBank"].ToString();
                e[i].Account = dt.Rows[i]["TargetAcct"].ToString();
            }
            string a = new JavaScriptSerializer().Serialize(e);
            //string b = new JavaScriptSerializer().Serialize(new { Result="OK", Records=e, TotalRecordCount=e.Length});
            return a;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string IssuerJson(string cust, string custtype)
        {
            DataTable dt = TreasuryModel.Data.Customer.getIssuerForServices(Configuration.Config.ConnectionString, cust, custtype);
            List<Issuer> param = new List<Issuer>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Issuer s = new Issuer();
                s.CustomerID = int.Parse(dt.Rows[i]["CustomerID"].ToString());
                s.Name = dt.Rows[i]["NameEn"].ToString();
                s.Bank = dt.Rows[i]["TargetBank"].ToString();
                s.Account = dt.Rows[i]["TargetAcct"].ToString();
                param.Add(s);
            }
            string a = new JavaScriptSerializer().Serialize(param);
            return a;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SellerJson(string cust, string custtype)
        {
            DataTable dt = TreasuryModel.Data.Customer.getSellerForServices(Configuration.Config.ConnectionString, cust, custtype);
            List<Issuer> param = new List<Issuer>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Issuer s = new Issuer();
                s.CustomerID = int.Parse(dt.Rows[i]["CustomerID"].ToString());
                s.Name = dt.Rows[i]["NameEn"].ToString();
                s.Bank = dt.Rows[i]["TargetBank"].ToString();
                s.Account = dt.Rows[i]["TargetAcct"].ToString();
                param.Add(s);
            }
            string a = new JavaScriptSerializer().Serialize(param);
            return a;
        }
    }


    public class JsonData
    {
        public string value { get; set; }
        public string label { get; set; }
        public string desc { get; set; }
        public string icon { get; set; }
        public string incvat { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }

    public class Issuer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }
}
