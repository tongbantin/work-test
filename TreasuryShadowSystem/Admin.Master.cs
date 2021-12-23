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
using System.Text;

namespace TreasuryShadowSystem
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        public string TheProfileMenu = "";
        public string TheProfile = "";

        public string GetAppPath()
        {

            return (System.Web.HttpRuntime.AppDomainAppVirtualPath == "/") ? "../../" : "../";

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FullName"] != null)
            {

                TheProfile = String.Format("{0} ({1})", (string)Session["FullName"], (string)Session["Branch"]);

                CreateMenu();
                Page.DataBind();

            }
            else
            {
                Session.Clear();
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void CreateMenu()
        {
            try
            {
                StringBuilder sp = new StringBuilder();
                DataSet menu = (DataSet)Session["UserMenu"];
                if (menu.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in menu.Tables[0].Select("PARENTID IS NULL"))   //Home
                    {

                        DataRow[] dr1 = menu.Tables[0].Select("PARENTID=" + dr["ID"].ToString() + " AND DATA_CODE='Management'"); //Management
                        if (dr1.Count() > 0)
                        {

                            foreach (DataRow r1 in dr1)
                            {
                                DataRow[] dr2 = menu.Tables[0].Select("PARENTID=" + r1["ID"].ToString());
                                if (dr2.Count() > 0)
                                {
                                    sp.Append("<h1>");
                                    sp.Append(r1["DATA_NAME_ENG"].ToString());
                                    sp.Append("</h1><div>");

                                    foreach (DataRow r2 in dr2)
                                    {
                                        if (r2["PATH_URL"].ToString() != "")
                                        {

                                            sp.Append("<p>");
                                            sp.Append("<a href=\"#\" class=\"leftmenu\" onclick=\"return loadIframe('ifMain', '" + GetAppPath() + r2["PATH_URL"].ToString() + "')\">" + r2["DATA_NAME_ENG"].ToString() + "</a>");
                                            sp.Append("</p>");

                                        }
                                    }
                                    sp.Append("</div>");
                                }
                            }
                        }
                    }

                    TheProfileMenu = sp.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
