using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TreasuryShadowSystem
{
    public partial class TreePermission : System.Web.UI.Page
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
