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
using System.Web.Services;

namespace TreasuryShadowSystem.ForeignExchange
{
    public partial class frm_PositionDetail : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_PositionDetail));
        protected void Page_Init(object sender, EventArgs e) 
        {
            try
            {
                GridDealPosition.PageIndexChanging += new GridViewPageEventHandler(GridDealPosition_PageIndexChanging);
            }
            catch (Exception ex)
            { 
            
            }
        }

        void GridDealPosition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            try
            {
                GridDealPosition.PageIndex = e.NewPageIndex;
                BindGrid();
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
                if (!IsPostBack)
                    BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void BindGrid()
        {
            try
            {
                GridDealPosition.DataSource = PositionDetail.PositionDetailList(Config.ConnectionString, DealNoSearchBox.Text, ValueDateSearchBox.Text);
                GridDealPosition.DataBind();
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

        protected void ReImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean result = PositionDetail.ReImportPositionDetail(Config.ConnectionString, ExecuteDateBox.Value);
                if (result)
                {
                    MessageBox.Text = "ระบบ Rerun ข้อมูลเรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถ Rerun ได้ กรุณาตรวจสอบข้อมูล และทำรายการใหม่";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetPositionDetail(int DETAILID)
        {
            return PositionDetail.getPositionDetail(Config.ConnectionString, DETAILID);
        }

        [WebMethod(EnableSession = true)]
        public static object AddPositionDetail(PositionDetail record)
        {
            record.CREATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return PositionDetail.Create(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdatePositionDetail(PositionDetail record)
        {
            record.UPDATE_USER = HttpContext.Current.Session["UserName"].ToString();
            return PositionDetail.Update(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeletePositionDetail(int DETAILID)
        {
            return PositionDetail.Delete(Config.ConnectionString, DETAILID);
        }

    }
}
