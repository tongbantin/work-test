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

namespace TreasuryShadowSystem
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                DataList1.ItemDataBound += new DataListItemEventHandler(DataList1_ItemDataBound);
                DataList2.ItemDataBound += new DataListItemEventHandler(DataList2_ItemDataBound);
                DataList3.ItemDataBound += new DataListItemEventHandler(DataList3_ItemDataBound);
            }
            catch (Exception ex)
            { 
            
            }
        }

        void DataList3_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    DataSet ds = (DataSet)Session["UserMenu"];
                    DataRowView r = (DataRowView)e.Item.DataItem;

                    DataRow[] dr = ds.Tables[0].Select("DATA_TYPE='USER' AND PARENTID='" + r.Row["ID"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        decimal parentid = Convert.ToDecimal(r.Row["ID"].ToString());
                        if (dr.Length < 10)
                            ((DataList)e.Item.FindControl("DataListUserSubMenu")).RepeatColumns = 1;
                        else
                            ((DataList)e.Item.FindControl("DataListUserSubMenu")).RepeatColumns = 5;
                        ((DataList)e.Item.FindControl("DataListUserSubMenu")).DataSource = ds.Tables[0].AsEnumerable().Where(t => t.Field<string>("DATA_TYPE") == "USER" && t.Field<decimal>("PARENTID") == parentid).CopyToDataTable();
                        ((DataList)e.Item.FindControl("DataListUserSubMenu")).DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        void DataList2_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    DataSet ds = (DataSet)Session["UserMenu"];
                    DataRowView r = (DataRowView)e.Item.DataItem;

                    DataRow[] dr = ds.Tables[0].Select("DATA_TYPE='REPORT' AND PARENTID='" + r.Row["ID"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        decimal parentid = Convert.ToDecimal(r.Row["ID"].ToString());
                        if (dr.Length < 10)
                            ((DataList)e.Item.FindControl("DataListReportSubMenu")).RepeatColumns = 1;
                        else
                            ((DataList)e.Item.FindControl("DataListReportSubMenu")).RepeatColumns = 5;
                        ((DataList)e.Item.FindControl("DataListReportSubMenu")).DataSource = ds.Tables[0].AsEnumerable().Where(t => t.Field<string>("DATA_TYPE") == "REPORT" && t.Field<decimal>("PARENTID") == parentid).CopyToDataTable();
                        ((DataList)e.Item.FindControl("DataListReportSubMenu")).DataBind();
                    }
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    DataSet ds = (DataSet)Session["UserMenu"];
                    DataRowView r = (DataRowView)e.Item.DataItem;

                    DataRow[] dr = ds.Tables[0].Select("DATA_TYPE='MAINTAIN' AND PARENTID='" + r.Row["ID"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        decimal parentid = Convert.ToDecimal(r.Row["ID"].ToString());
                        if (dr.Length < 10)
                            ((DataList)e.Item.FindControl("DataListMaintainSubMenu")).RepeatColumns = 1;
                        else
                            ((DataList)e.Item.FindControl("DataListMaintainSubMenu")).RepeatColumns = 5;
                        ((DataList)e.Item.FindControl("DataListMaintainSubMenu")).DataSource = ds.Tables[0].AsEnumerable().Where(t => t.Field<string>("DATA_TYPE") == "MAINTAIN" && t.Field<decimal>("PARENTID") == parentid).CopyToDataTable();
                        ((DataList)e.Item.FindControl("DataListMaintainSubMenu")).DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FullName"] != null)
            {
                DataSet ds = (DataSet)Session["UserMenu"];

                DataRow [] dr  = ds.Tables[0].Select("DATA_TYPE='MAINTAIN' AND PARENTID='1'");
                if (dr.Length > 0)
                {
                    if (dr.Length < 10)
                        DataList1.RepeatColumns = 1;
                    else
                        DataList1.RepeatColumns = 5;
                    DataList1.DataSource = ds.Tables[0].AsEnumerable().Where(t => t.Field<string>("DATA_TYPE") == "MAINTAIN" && t.Field<decimal>("PARENTID") == 1).CopyToDataTable();
                    DataList1.DataBind();
                }
                else {
                    MainTainMenu.Visible = false;
                }

                dr = ds.Tables[0].Select("DATA_TYPE='REPORT' AND PARENTID='1'");
                if (dr.Length > 0)
                {
                    if (dr.Length < 10)
                        DataList2.RepeatColumns = 1;
                    else
                        DataList2.RepeatColumns = 5;
                    DataList2.DataSource = ds.Tables[0].AsEnumerable().Where(t => t.Field<string>("DATA_TYPE") == "REPORT" && t.Field<decimal>("PARENTID") == 1).CopyToDataTable();
                    DataList2.DataBind();
                }
                else {
                    ReportMenu.Visible = false;
                }

                dr = ds.Tables[0].Select("DATA_TYPE='USER' AND PARENTID='1'");
                if (dr.Length > 0)
                {
                    if (dr.Length < 10)
                        DataList3.RepeatColumns = 1;
                    else
                        DataList3.RepeatColumns = 5;
                    DataList3.DataSource = ds.Tables[0].AsEnumerable().Where(t => t.Field<string>("DATA_TYPE") == "USER" && t.Field<decimal>("PARENTID") == 1).CopyToDataTable();
                    DataList3.DataBind();
                }
                else {
                    UserMenu.Visible = false;
                }
            }
            else
            {
                Session.Clear();
                Response.Redirect("~/Login.aspx");
            }
        }

    }
}
