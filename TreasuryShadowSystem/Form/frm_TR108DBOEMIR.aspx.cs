using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TreasuryModel.Master;
using TreasuryShadowSystem.Configuration;
namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR108DBOEMIR : System.Web.UI.Page
    {
        public string TheUrl = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            //Label lb = (Label)Master.FindControl("lblFormTitle");
            //lb.Text = "Purchase Interest";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Model.Model_Tb_Menu_Auth.IsAccess((DataSet)Session["UserMenu"], HttpContext.Current.Request.Url.AbsolutePath))
            //    Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                BindCustomerGroup();
                BindCustomer();
            }
        }

        void BindCustomerGroup()
        {
            try
            {
                DataTable tb = Model_MS_CUSTOMER.GetCustomerGroupByEmir(Config.ConnectionString);
                CustomerGroupBox.DataSource = tb;
                CustomerGroupBox.DataTextField = "GRPID";
                CustomerGroupBox.DataValueField = "GRPID";
                CustomerGroupBox.DataBind();

                CustomerGroupBox.Items.Insert(0, new ListItem("---All---", ""));
            }
            catch (Exception ex)
            {

            }
        }

        void BindCustomer()
        {
            try
            {
                DataTable tb = Model_MS_CUSTOMER.GetCustomerByCustomerGroup(Config.ConnectionString, CustomerGroupBox.SelectedValue);
                CustomerBox.Items.Clear();
                CustomerBox.DataSource = tb;
                CustomerBox.DataTextField = "SN";
                CustomerBox.DataValueField = "CNO";
                CustomerBox.DataBind();

                CustomerBox.Items.Insert(0, new ListItem("---All---", ""));
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            TheUrl = "../View/ReportViewer2.aspx?report=TR108DBOEMIR";
            TheUrl += "&b=" + BranchBox.Text + "&e="+ ExecuteDateBox.Value + "&g=" + CustomerGroupBox.SelectedValue + "&c=" + CustomerBox.SelectedValue;
            Page.DataBind();
        }

        protected void CustomerGroupBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindCustomer();
            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}
