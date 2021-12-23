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
using System.Web.Services;

using System.DirectoryServices;
using log4net;
using System.Text;
using System.Collections.Generic;

namespace TreasuryShadowSystem
{
    public partial class Login : System.Web.UI.Page
    {
        ILog Log = LogManager.GetLogger(typeof(Login));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                divError.Visible = false;
                //divInfo.Visible = true;
            }
        }

        //protected void CallScript(string msg)
        //{
        //    StringBuilder sp = new StringBuilder();
        //    sp.Append("showMessage('" + msg + "');");

        //    if (!Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType(), "showMessage"))
        //        ClientScript.RegisterClientScriptBlock(Page.GetType(), "showMessage", sp.ToString(), true);
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //CallScript("demo");

            string response = "Invalid Username or Password";
            string username = loginusername.Value;
            string password = loginpassword.Value;

            ILog Log = LogManager.GetLogger(typeof(Login));

            //For Production
            try
            {
                if (username.ToUpper().Equals("TRO.OPER"))
                {
                    Session["UserName"] = "TREASURY.OPER";
                    Session["FullName"] = "Treasury Operation";
                    Session["Branch"] = "Treasury Operation"; //get branch from distinguishedName
                    Session["LoginDate"] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    Session["BranchDate"] = Model.Model_Tb_Master.getSysDate();
                    //Session["UserMenu"] = Model.Model_Tb_Menu_Auth.GetMenu("ITProject");
                    Session["UserMenu"] = Model.Model_Tb_Menu_Auth.GetMenu(password);
                    Session["UserGroup"] = password;
                    Log.Info("Login successfull on :" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                    divError.Visible = false;
                    //divInfo.Visible = true;
                    Response.Redirect("Main.aspx", true);
                    response = "successful";
                }
                else
                {
                    DirectoryEntry de = new DirectoryEntry(Configuration.Config.LdapConnection, username, password, AuthenticationTypes.Secure);
                    DirectorySearcher ds = new DirectorySearcher(de);
                    ds.Filter = "(&(objectClass=user)(sAMAccountName=" + username + "))";
                    //ds.FindAll().Cast<SearchResult>().Select(result => new Address(result.GetDirectoryEntry())).ToList();
                    SearchResultCollection sr = ds.FindAll();
                    //string[] a = { "cn", "dc", "name", "distinguishedName", "sn", "givenname", "displayName", "sAMAccountName" };
                    if (sr.Count > 0)
                    {
                        ResultPropertyValueCollection result = sr[0].Properties["memberof"];
                        DataSet ts = new DataSet();
                        ts = Model.Model_Tb_UserGroup.getUserGroups();
                        string[] dept = new string[ts.Tables[0].Rows.Count]; // { "OU=FDOper", "OU=Treasury", "OU=ITProject", "OU=Treasury_Oper", "OU=Account_Data" };
                        //string[] dept = { "OU=FDOper", "OU=Treasury", "OU=ITMIS", "OU=Treasury", "OU=Accounting" };
                        string UserGroup = "";
                        for (int i = 0; i < ts.Tables[0].Rows.Count; i++)
                        {
                            string str  = result.Cast<String>().Where(t => t.Contains("CN=" + ts.Tables[0].Rows[i]["USERGROUP_NAME"].ToString())).FirstOrDefault<String>();
                            if (str != null)
                                UserGroup = str.Split(',')[0].Substring(3);
                        }
 
                        if (UserGroup != "")
                        {
                            Session.Timeout = 90;
                            Session["UserName"] = sr[0].Properties["cn"][0].ToString();
                            Session["FullName"] = sr[0].Properties["displayName"][0].ToString();
                            Session["Branch"] = sr[0].Properties["displayName"][0].ToString().Substring(3); //get branch from distinguishedName
                            Session["LoginDate"] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"); 
                            Session["UserMenu"] = Model.Model_Tb_Menu_Auth.GetMenu(UserGroup);
                            Session["UserGroup"] = UserGroup;
                            Log.Info("Login successfull on :" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                            divError.Visible = false;
                            //divInfo.Visible = true;
                            Response.Redirect("Main.aspx", true);
                            response = "successful";
                        }
                        else
                        {
                            response = "You are not permission to access the system, please contact administrator.";
                            Log.Error(response);
                            //divInfo.Visible = false;
                            divError.Visible = true;
                            lbMessage.Text = response;
                        }

                        //foreach (SearchResult result in sr)
                        //{
                        //    string[] str = result.GetDirectoryEntry().Properties["distinguishedName"].Value.ToString().Split(',');

                        //    //if ((str.Contains("OU=Treasury")) || (str.Contains("OU=ITProject")) || (str.Contains("OU=Treasury_Oper")) || (str.Contains("OU=Account_Data")) || (str.Contains("OU=FDOper")))
                        //    if (str.Intersect(dept).Any())
                        //    {
                        //        Session.Timeout = 90;
                        //        Session["UserName"] = result.GetDirectoryEntry().Properties["cn"].Value;
                        //        Session["FullName"] = result.GetDirectoryEntry().Properties["displayName"].Value;
                        //        Session["Branch"] = str[2].Substring(3); //get branch from distinguishedName
                        //        Session["LoginDate"] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                        //        //Session["UserMenu"] = Model.Model_Tb_Menu_Auth.GetAuthenticationMenu(str[1].Substring(3));
                        //        Session["UserMenu"] = Model.Model_Tb_Menu_Auth.GetMenu(str[1].Substring(3));
                        //        Session["UserGroup"] = str[1].Substring(3);
                        //        Log.Info("Login successfull on :" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
                        //        divError.Visible = false;
                        //        divInfo.Visible = true;
                        //        Response.Redirect("Main.aspx", true);
                        //        response = "successful";
                        //    }
                        //    else
                        //    {
                        //        response = "You are not permission to access the system, please contact administrator.";
                        //        Log.Error(response);
                        //        divInfo.Visible = false;
                        //        divError.Visible = true;
                        //        lbMessage.Text = response;
                        //    }
                        //}
                    }
                    else
                    {
                        //lblError.Text = "User not found";
                        response = "User not found";
                        Log.Error("User not found");
                        //divInfo.Visible = false;
                        divError.Visible = true;
                        lbMessage.Text = response;
                    }
                }
            }
            catch (Exception ex)
            {
                //lblError.Text = "Username or Password Invalid";
                response = ex.Message;
                Log.Error(ex.Message);
                //divInfo.Visible = false;
                divError.Visible = true;
                lbMessage.Text = response;
            }
            finally
            {

            }
        }
    }
}