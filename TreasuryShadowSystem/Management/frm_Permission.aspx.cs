using System;
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
    public partial class frm_Permission : System.Web.UI.Page
    {

        DataSet UserGroupSession
        {
            get
            {
                ViewState["UserGroupSession"] = Model.Model_Tb_UserGroup.getUserGroups();
                return (DataSet)ViewState["UserGroupSession"];
            }
            set
            {
                ViewState["UserGroupSession"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadUserGroup();
            }
        }

        private void LoadUserGroup()
        {
            ddUserGroup.DataSource = UserGroupSession;
            ddUserGroup.DataTextField = "USERGROUP_NAME";
            ddUserGroup.DataValueField = "USERGROUP_ID";
            ddUserGroup.DataBind();
            ddUserGroup.Items.Insert(0, new ListItem(""));

        }
        
    }
}
