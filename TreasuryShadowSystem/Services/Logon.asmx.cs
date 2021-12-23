using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Collections;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.DirectoryServices;
using log4net;
using KKB.Treasury.TreasuryCommon.Common;
using TreasuryShadowSystem.Configuration;

namespace TreasuryShadowSystem.Services
{
    /// <summary>
    /// Summary description for Logon
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Logon : System.Web.Services.WebService
    {
        [WebMethod(EnableSession=true)]
        public string AuthenticateUser(string username, string password)
        {
            string response = "Invalid Username or Password";


            ILog Log = LogManager.GetLogger(typeof(Logon));

            DirectoryEntry de = new DirectoryEntry(Configuration.Config.LdapConnection, username, password, AuthenticationTypes.Secure);
            try
            {
                DirectorySearcher ds = new DirectorySearcher(de);
                ds.Filter = "(&(objectClass=user)(sAMAccountName=" + username + "))";
                SearchResultCollection sr = ds.FindAll();
                string[] a = { "cn", "dc", "name", "distinguishedName", "sn", "givenname", "displayName", "sAMAccountName" };
                if (sr.Count > 0)
                {
                    DataSet ts = Model.Model_Tb_UserGroup.getUserGroups();
                    string[] dept = new string[ts.Tables[0].Rows.Count]; // { "OU=FDOper", "OU=Treasury", "OU=ITProject", "OU=Treasury_Oper", "OU=Account_Data" };
                    ////string[] dept = { "OU=FDOper", "OU=Treasury", "OU=ITMIS", "OU=Treasury", "OU=Accounting" };
                    
                    for (int i = 0; i < ts.Tables[0].Rows.Count; i++)
                    {
                        dept[i] = "OU=" + ts.Tables[0].Rows[i]["USERGROUP_NAME"].ToString();
                    }

                    foreach (SearchResult result in sr)
                    {
                        string[] str = result.GetDirectoryEntry().Properties["distinguishedName"].Value.ToString().Split(',');

                    //    //if ((str.Contains("OU=Treasury")) || (str.Contains("OU=ITProject")) || (str.Contains("OU=Treasury_Oper")) || (str.Contains("OU=Account_Data")) || (str.Contains("OU=FDOper")))
                        if (str.Intersect(dept).Any())
                        {
                            Session.Timeout = 90;
                            Session["UserName"] = result.GetDirectoryEntry().Properties["cn"].Value;
                            Session["FullName"] = result.GetDirectoryEntry().Properties["displayName"].Value;
                            Session["Branch"] = str[2].Substring(3); //get branch from distinguishedName
                            Session["LoginDate"] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                            Session["UserMenu"] = Model.Model_Tb_Menu_Auth.GetAuthenticationMenu(str[1].Substring(3));
                            
                            //Session["UserName"] = "YUWANEE.YOS";
                            //Session["FullName"] = "YUWANEE.YOS";
                            //Session["Branch"] = "HQ"; //get branch from distinguishedName
                            //Session["LoginDate"] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                            //Session["UserMenu"] = Model.Model_Tb_Menu_Auth.GetAuthenticationMenu("FDOper");
                            //Response.Redirect("Home.aspx", true);
                            response = "successful";

                            Log.Info(response);
                        }
                        else
                        {
                            //lblError.Text = "You could not access";
                            for (int i = 0; i < a.Count() - 1; i++)
                            {
                                Log.Info("PropertyName=" + a[i] + ", Value=" + result.GetDirectoryEntry().Properties[a[i]].Value);
                            }
                            response = "You are not permission to access the system, please contact administrator.";
                            Log.Error(response);
                        }
                    }
                }
                else
                {
                    //lblError.Text = "User not found";
                    response = "User not found";
                    Log.Error("User not found");
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = "Username or Password Invalid";
                response = ex.Message;
                Log.Error(ex.Message);
            }
            finally
            {

            }

            return response;

        }

        [WebMethod(EnableSession = true)]
        public string OracleIsValid()
        {
            try
            {
                Oracle orcl = Oracle.getInstance(Config.ConnectionString);
                bool _bool = orcl.openConnection();
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                
            }
        }
    }
}
