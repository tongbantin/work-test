﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TreasuryModel.Data;
using TreasuryShadowSystem.Configuration;
using log4net;
using TreasuryModel.Enum;
using System.Web.Services;
using System.Data;

namespace TreasuryShadowSystem.CBS
{
    public partial class frmCBSContact : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frmCBSContact));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GridData.PageIndexChanging += new GridViewPageEventHandler(GridData_PageIndexChanging);

                BindGrid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridData.PageIndex = e.NewPageIndex;
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
                GridData.DataSource = CBSContact.GetList(Config.ConnectionString, CIFSearchBox.Text.Trim(), NameSearchBox.Text.Trim());
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetContact(int ContactID)
        {
            return CBSContact.GetContact(Config.ConnectionString, ContactID);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteContact(int ContactID)
        {
            //if (HttpContext.Current.Session["UserName"] == null)
            //    return new { Result = "ERROR", Message = "Login Session Expired" };
            return CBSContact.DeleteContact(Config.ConnectionString, ContactID);
        }


        protected void SyncCBS_Click(object sender, EventArgs e)
        {
            try
            {
                string ServiceHosting = Config.GetParameterFile("CBSServiceHosting");
                CBSContact[] contact = CBSContact.TransformCBSContact(ServiceHosting, CIFBox.Text.Trim());
                bool result = CBSContact.Save(Config.ConnectionString, contact);
                if (result)
                {
                    CIFBox.Text = "";
                    MessageBox.Text = "นำข้อมูลเข้าเรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถนำข้อมูลเข้าได้";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
