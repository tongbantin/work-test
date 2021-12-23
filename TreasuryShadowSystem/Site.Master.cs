using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using log4net;
using System.Reflection;
namespace TreasuryShadowSystem
{
    public partial class Site : System.Web.UI.MasterPage
    {
        ILog Log = LogManager.GetLogger(typeof(Site));
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                DataList1.ItemDataBound += new RepeaterItemEventHandler(DataList1_ItemDataBound);
                DataList2.ItemDataBound += new RepeaterItemEventHandler(DataList2_ItemDataBound);
                DataList3.ItemDataBound += new RepeaterItemEventHandler(DataList3_ItemDataBound);
                DataList4.ItemDataBound += new RepeaterItemEventHandler(DataList4_ItemDataBound);

                Version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void DataList2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    DataSet ds = (DataSet)Session["UserMenu"];
                    DataRowView r = (DataRowView)e.Item.DataItem;

                    DataRow[] dr = ds.Tables[0].Select("DATA_TYPE='REPORT' AND GROUPID='" + r.Row["ID"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        decimal parentid = Convert.ToDecimal(r.Row["ID"].ToString());
                        //if (dr.Length < 10)
                        //    ((DataList)e.Item.FindControl("DataListReportSubMenu")).RepeatColumns = 1;
                        //else
                        //    ((DataList)e.Item.FindControl("DataListReportSubMenu")).RepeatColumns = 5;
                        DataRow[] drReport = dr.Where(t => t.Field<string>("DATA_TYPE") == "REPORT" && t.Field<decimal>("GROUPID") == parentid).ToArray<DataRow>();
                        if (drReport.Length > 0)
                        {
                            DataTable sortTable = drReport.CopyToDataTable();
                            sortTable.DefaultView.Sort = "SEQ,DATA_NAME_ENG";
                            ((Repeater)e.Item.FindControl("DataListReportSubMenu")).DataSource = sortTable;
                            ((Repeater)e.Item.FindControl("DataListReportSubMenu")).DataBind();
                        } else 
                        {
                            ((Repeater)e.Item.FindControl("DataListReportSubMenu")).DataSource = null;
                            ((Repeater)e.Item.FindControl("DataListReportSubMenu")).DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message); 
            }
        }

        void DataList3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    DataSet ds = (DataSet)Session["UserMenu"];
                    DataRowView r = (DataRowView)e.Item.DataItem;

                    DataRow[] dr = ds.Tables[0].Select("DATA_TYPE='USER' AND GROUPID='" + r.Row["ID"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        decimal parentid = Convert.ToDecimal(r.Row["ID"].ToString());
                        //if (dr.Length < 10)
                        //    ((DataList)e.Item.FindControl("DataListUserSubMenu")).RepeatColumns = 1;
                        //else
                        //    ((DataList)e.Item.FindControl("DataListUserSubMenu")).RepeatColumns = 5;
                        DataRow[] drUser = dr.Where(t => t.Field<string>("DATA_TYPE") == "USER" && t.Field<decimal>("GROUPID") == parentid).ToArray<DataRow>(); 
                        if (drUser.Length >0)
                        {
                            DataTable sortTable = drUser.CopyToDataTable();
                            sortTable.DefaultView.Sort = "SEQ,DATA_NAME_ENG";
                            ((Repeater)e.Item.FindControl("DataListUserSubMenu")).DataSource = sortTable;
                            ((Repeater)e.Item.FindControl("DataListUserSubMenu")).DataBind();
                        } else 
                        {
                            ((Repeater)e.Item.FindControl("DataListUserSubMenu")).DataSource = null;
                            ((Repeater)e.Item.FindControl("DataListUserSubMenu")).DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


        void DataList1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    DataSet ds = (DataSet)Session["UserMenu"];
                    DataRowView r = (DataRowView)e.Item.DataItem;

                    DataRow[] dr = ds.Tables[0].Select("DATA_TYPE='MAINTAIN' AND GROUPID='" + r.Row["ID"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        decimal parentid = Convert.ToDecimal(r.Row["ID"].ToString());
                        //if (dr.Length < 10)
                        //    ((DataList)e.Item.FindControl("DataListMaintainSubMenu")).RepeatColumns = 1;
                        //else
                        //    ((DataList)e.Item.FindControl("DataListMaintainSubMenu")).RepeatColumns = 5;
                        DataRow[] drMaintain = dr.Where(t => t.Field<string>("DATA_TYPE") == "MAINTAIN" && t.Field<decimal>("GROUPID") == parentid).ToArray<DataRow>();
                        if (drMaintain.Length > 0)
                        {
                            DataTable sortTable = drMaintain.CopyToDataTable();
                            sortTable.DefaultView.Sort = "SEQ,DATA_NAME_ENG";
                            ((Repeater)e.Item.FindControl("DataListMaintainSubMenu")).DataSource = sortTable;
                            ((Repeater)e.Item.FindControl("DataListMaintainSubMenu")).DataBind();
                        }
                        else
                        {
                            ((Repeater)e.Item.FindControl("DataListMaintainSubMenu")).DataSource = null;
                            ((Repeater)e.Item.FindControl("DataListMaintainSubMenu")).DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void DataList4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    DataSet ds = (DataSet)Session["UserMenu"];
                    DataRowView r = (DataRowView)e.Item.DataItem;

                    DataRow[] dr = ds.Tables[0].Select("DATA_TYPE='SANCTION' AND GROUPID='" + r.Row["ID"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        decimal parentid = Convert.ToDecimal(r.Row["ID"].ToString());
                        //if (dr.Length < 10)
                        //    ((DataList)e.Item.FindControl("DataListMaintainSubMenu")).RepeatColumns = 1;
                        //else
                        //    ((DataList)e.Item.FindControl("DataListMaintainSubMenu")).RepeatColumns = 5;
                        DataRow[] drMaintain = dr.Where(t => t.Field<string>("DATA_TYPE") == "SANCTION" && t.Field<decimal>("GROUPID") == parentid).ToArray<DataRow>();
                        if (drMaintain.Length > 0)
                        {
                            DataTable sortTable = drMaintain.CopyToDataTable();
                            sortTable.DefaultView.Sort = "SEQ,DATA_NAME_ENG";
                            ((Repeater)e.Item.FindControl("DataListSanctionSubMenu")).DataSource = sortTable;
                            ((Repeater)e.Item.FindControl("DataListSanctionSubMenu")).DataBind();
                        }
                        else
                        {
                            ((Repeater)e.Item.FindControl("DataListMaintainSubMenu")).DataSource = null;
                            ((Repeater)e.Item.FindControl("DataListMaintainSubMenu")).DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            
                if (Session["FullName"] != null)
                {
                    DataSet ds = (DataSet)Session["UserMenu"];
                    try
                    {
                        Username.Text = Session["Username"].ToString();
                        BranchProcessingDate.Text = ((DateTime)Session["BranchDate"]).ToString("dd/MM/yyyy");
                    }
                    catch (Exception ex) {
                        Log.Error(ex.Message);
                    }

                    DataRow [] dr  = ds.Tables[0].Select("DATA_TYPE='MAINTAIN' AND GROUPID IS NOT NULL");
                    if (dr.Length > 0)
                    {
                        //if (dr.Length < 10)
                        //    DataList1.RepeatColumns = 1;
                        //else
                        //    DataList1.RepeatColumns = 5;
                        DataRow[] drMaintain = dr.Where(t => t.Field<string>("DATA_TYPE") == "MAINTAIN" && t.Field<decimal>("GROUPID") == 1).ToArray<DataRow>();
                        if (drMaintain.Length > 0)
                        {
                            DataTable sortTable = drMaintain.CopyToDataTable();
                            sortTable.DefaultView.Sort = "SEQ,DATA_NAME_ENG";
                            DataList1.DataSource = sortTable;
                            DataList1.DataBind();
                        }
                        else
                        {
                            DataList1.DataSource = null;
                            DataList1.DataBind();
                        }
                    }
                    else {
                        MainTainMenu.Visible = false;
                    }

                    dr = ds.Tables[0].Select("DATA_TYPE='REPORT' AND GROUPID IS NOT NULL");
                    if (dr.Length > 0)
                    {
                        //if (dr.Length < 10)
                        //    DataList2.RepeatColumns = 1;
                        //else
                        //    DataList2.RepeatColumns = 5;
                        DataRow[] drReport = dr.Where(t => t.Field<string>("DATA_TYPE") == "REPORT" && t.Field<decimal>("GROUPID") == 1).ToArray<DataRow>();
                        if (drReport.Length > 0)
                        {
                            DataTable sortTable = drReport.CopyToDataTable();
                            sortTable.DefaultView.Sort = "SEQ,DATA_NAME_ENG";
                            DataList2.DataSource = sortTable;                        
                            DataList2.DataBind();
                        }
                        else
                        {
                            DataList2.DataSource = null;
                            DataList2.DataBind();
                        }
                    }
                    else {
                        ReportMenu.Visible = false;
                    }

                    dr = ds.Tables[0].Select("DATA_TYPE='USER' AND GROUPID IS NOT NULL");
                    if (dr.Length > 0)
                    {
                        //if (dr.Length < 10)
                        //    DataList3.RepeatColumns = 1;
                        //else
                        //    DataList3.RepeatColumns = 5;
                        DataRow[] drUser = dr.Where(t => t.Field<string>("DATA_TYPE") == "USER" && t.Field<decimal>("GROUPID") == 1).ToArray<DataRow>();
                        if (drUser.Length > 0)
                        {
                            DataTable sortTable = drUser.CopyToDataTable();
                            sortTable.DefaultView.Sort = "SEQ,DATA_NAME_ENG";
                            DataList3.DataSource = sortTable;
                            DataList3.DataBind();
                        }
                        else
                        {
                            DataList3.DataSource = null;
                            DataList3.DataBind();
                        }
                    }
                    else {
                        UserMenu.Visible = false;
                    }


                    dr = ds.Tables[0].Select("DATA_TYPE='SANCTION'");
                    if (dr.Length > 0)
                    {
                        //if (dr.Length < 10)
                        //    DataList1.RepeatColumns = 1;
                        //else
                        //    DataList1.RepeatColumns = 5;
                        DataRow[] drMaintain = dr.Where(t => t.Field<string>("DATA_TYPE") == "SANCTION" && t.Field<decimal>("GROUPID") == 1).ToArray<DataRow>();
                        if (drMaintain.Length > 0)
                        {
                            DataTable sortTable = drMaintain.CopyToDataTable();
                            sortTable.DefaultView.Sort = "SEQ,DATA_NAME_ENG";
                            DataList4.DataSource = sortTable;
                            DataList4.DataBind();
                        }
                        else
                        {
                            DataList4.DataSource = null;
                            DataList4.DataBind();
                        }
                    }
                    else
                    {
                        SanctionMenu.Visible = false;
                    }
                }
                else
                {
                    Session.Clear();
                    Response.Redirect("~/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

    }
}
