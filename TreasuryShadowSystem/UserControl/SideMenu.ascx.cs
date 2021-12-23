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

namespace TreasuryShadowSystem.UserControl
{
    public partial class SideMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //PlaceHolderMenu.Controls.Add(new LiteralControl("<ul id=\"navmenu-v\">"));
                //DataSet menu = (DataSet)Session["UserMenu"];
                //if (menu.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in menu.Tables[0].Select("PARENTID IS NULL"))
                //    { 
                //        PlaceHolderMenu.Controls.Add(new LiteralControl("<li>"));                    
                //        HyperLink a = new HyperLink();
                //        a.Text = dr["DATA_NAME_ENG"].ToString();
                //        if (dr["PATH_URL"].ToString().Trim()!="" )
                //            a.NavigateUrl = "~/" + dr["PATH_URL"].ToString();
                //        PlaceHolderMenu.Controls.Add(a);
                //        DataRow[] r = menu.Tables[0].Select("PARENTID=" + dr["ID"].ToString());
                //        if(r.Count()>0)
                //            RenderMenu(dr["ID"].ToString(), ref menu);
                //        PlaceHolderMenu.Controls.Add(new LiteralControl("</li>"));
                //    }
                //}
                //PlaceHolderMenu.Controls.Add(new LiteralControl("</ul>"));
                if (!IsPostBack)
                {
                    CreateMenu();
                    TreeMenu.CollapseAll();
                }
            }
            catch (Exception ex) 
            {
 
            }
            
        }
        //protected void RenderMenu(String ParentMenu, ref DataSet menu)
        //{
        //    PlaceHolderMenu.Controls.Add(new LiteralControl("<ul>"));
        //    foreach (DataRow dr in menu.Tables[0].Select("PARENTID="+ParentMenu))
        //    {
        //        PlaceHolderMenu.Controls.Add(new LiteralControl("<li>"));
        //        HyperLink a = new HyperLink();
        //        a.Text = dr["DATA_NAME_ENG"].ToString();
        //        if (dr["PATH_URL"].ToString().Trim() != "")
        //            a.NavigateUrl = "~/" + dr["PATH_URL"].ToString();
        //        PlaceHolderMenu.Controls.Add(a);
        //        DataRow[] r = menu.Tables[0].Select("PARENTID=" + dr["ID"].ToString());
        //        if (r.Count() > 0)
        //            RenderMenu(dr["ID"].ToString(), ref menu);
        //        PlaceHolderMenu.Controls.Add(new LiteralControl("</li>"));
        //    }
        //    PlaceHolderMenu.Controls.Add(new LiteralControl("</ul>"));
        //}

        protected void CreateMenu()
        {
            try
            {
                DataSet menu = (DataSet)Session["UserMenu"];
                if (menu.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in menu.Tables[0].Select("PARENTID IS NULL"))
                    {
                        TreeNode node = new TreeNode(dr["DATA_NAME_ENG"].ToString(), dr["ID"].ToString());                        
                        DataRow[] r = menu.Tables[0].Select("PARENTID=" + dr["ID"].ToString());
                        if (r.Count() > 0)
                        {
                            //node.PopulateOnDemand = false;
                            node.SelectAction = TreeNodeSelectAction.None;
                            TreeMenu.Nodes.Add(node);
                            RenderTreeMenu(node, dr["ID"].ToString(), ref menu);
                        } else
                        {
                            //node.PopulateOnDemand = true;
                            node.SelectAction = TreeNodeSelectAction.None;
                            TreeMenu.Nodes.Add(node);
                        }
                    }
                }
            }
            catch (Exception ex)
            { 
            
            }
        }
        protected void RenderTreeMenu(TreeNode parentnode, String ParentMenu, ref DataSet menu)
        {
            foreach (DataRow dr in menu.Tables[0].Select("PARENTID=" + ParentMenu))
            {
                TreeNode node = new TreeNode(dr["DATA_NAME_ENG"].ToString(), dr["ID"].ToString(), "", "~/" + dr["PATH_URL"].ToString(), "_parent");        
                DataRow[] r = menu.Tables[0].Select("PARENTID=" + dr["ID"].ToString());
                if (r.Count() > 0)
                {
                    node.SelectAction = TreeNodeSelectAction.None;
                    parentnode.ChildNodes.Add(node);
                    RenderTreeMenu(node, dr["ID"].ToString(), ref menu);
                }
                else {
                    node.SelectAction = TreeNodeSelectAction.Expand;
                    parentnode.ChildNodes.Add(node);
                }
                
            }
        }
    }
}