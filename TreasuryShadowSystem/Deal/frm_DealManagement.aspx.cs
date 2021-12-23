using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using TreasuryModel.Data;
using TreasuryShadowSystem.Configuration;
using log4net;
using TreasuryModel.Enum;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_DealManagement : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_DealManagement));
        
        protected void Page_Load(object sender, EventArgs e)
        {
            GridDeal.PageIndexChanging += new GridViewPageEventHandler(GridDeal_PageIndexChanging);
        }


        void GridDeal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            //throw new NotImplementedException();
            try
            {
                GridDeal.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void Delete(string key)
        {
            try
            {
                Deal m = new Deal();
                if (m.Delete(Config.ConnectionString, key))
                {
                    MessageBox.Text = "ลบรายการเรียบร้อย";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถลบข้อมูลได้ กรุณาตรวจสอบข้อมูล";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                BindGrid();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindGrid()
        {
            try
            {
                GridDeal.DataSource = Deal.DealList(Config.ConnectionString, InputDateSearchBox.Text.Trim());
                GridDeal.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                string key = "";
                for (int i = 0; i < GridDeal.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GridDeal.Rows[i].Cells[0].FindControl("chkDeleteDocument");
                    if (chk.Checked)
                    {
                        key += " " + GridDeal.DataKeys[i].Value.ToString();
                    }
                }

                key = key.Trim().Replace(' ', ',');
                Delete(key);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
