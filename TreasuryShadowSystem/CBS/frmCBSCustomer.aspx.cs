using System;
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
using System.IO;
using System.Text;

namespace TreasuryShadowSystem.CBS
{
    public partial class frmCBSCustomer : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frmCBSCustomer));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GridData.PageIndexChanging += new GridViewPageEventHandler(GridData_PageIndexChanging);
                GridData.RowDataBound += new GridViewRowEventHandler(GridData_RowDataBound);
                if (!IsPostBack)
                {
                    BindGrid(); 
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                { 
                    if (e.Row.Cells[7].Text == "0")
                        e.Row.Cells[7].Text = "Inactive";
                    else if (e.Row.Cells[7].Text == "1")
                        e.Row.Cells[7].Text = "Active";
                }
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
                GridData.DataSource = CBSCustomer.GetList(Config.ConnectionString, CIFSearchBox.Text.Trim(), NameSearchBox.Text.Trim(), OpicsSearchBox.Checked, TDSSearchBox.Checked);
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
         }

        [WebMethod(EnableSession = true)]
        public static object SyncCustomer(string CIFID)
        {
            //if (HttpContext.Current.Session["UserName"] == null)
            //    return new { Result = "ERROR", Message = "Login Session Expired" };
            string ServiceHosting = Config.GetParameterFile("CBSServiceHosting");
            return CBSCustomer.SyncCustomer(Config.ConnectionString, ServiceHosting, CIFID);
        }

        [WebMethod(EnableSession = true)]
        public static object GetCustomer(int CBSID)
        {
            //if (HttpContext.Current.Session["UserName"] == null)
            //    return new { Result = "ERROR", Message = "Login Session Expired" };
            return CBSCustomer.GetCustomer(Config.ConnectionString, CBSID);
        }

        [WebMethod(EnableSession = true)]
        public static object AddCustomer(CBSCustomer record)
        {
            //if (HttpContext.Current.Session["UserName"] == null)
            //    return new { Result = "ERROR", Message = "Login Session Expired" };
            return CBSCustomer.Save(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateCustomer(CBSCustomer record)
        {
            //if (HttpContext.Current.Session["UserName"] == null)
            //    return new { Result = "ERROR", Message = "Login Session Expired" };
            return CBSCustomer.Update(Config.ConnectionString, record);
        }

        protected void SendOpicsBox_Click(object sender, EventArgs e)
        {
            try
            {
                string key = GetKeyCheckBox();
                if (key.Trim().Equals(""))
                {
                    MessageBox.Text = "กรุณาเลือกรายการ เพื่อส่งไปยังระบบ OPICS";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    bool result = false;
                    CBSCustomer.GenerateCNO(Config.ConnectionString, key);
                    result = CreateFileOpics(key);
                    MessageBox.Text = "สร้างไฟล์สำหรับ OPICS เรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
        
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected bool CreateFileOpics(string key)
        {
            try
            {

                FileStream f = File.Create(Config.GetParameterFile("CBSOpicsFilePath"));
                StringBuilder str = new StringBuilder();

                DataTable dt = CBSCustomer.GetDataForOpics(Config.ConnectionString, key);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string rowstr = dt.Rows[i]["CNO"].ToString().Trim();    //CNO
                    rowstr += "," + dt.Rows[i]["PRIMARYNAME01"].ToString().Trim().Substring(0, dt.Rows[i]["PRIMARYNAME01"].ToString().Trim().Length < 10 ? dt.Rows[i]["PRIMARYNAME01"].ToString().Trim().Length : 10);    //CMNE
                    rowstr += "," + dt.Rows[i]["PRIMARYNAME02"].ToString().Trim().Substring(0, dt.Rows[i]["PRIMARYNAME02"].ToString().Trim().Length < 40 ? dt.Rows[i]["PRIMARYNAME02"].ToString().Trim().Length : 40); //SN
                    string name = dt.Rows[i]["ALTERNATEFORMATTEDNAME"].ToString();
                    //NAME - CFN1
                    rowstr += "," + name.Substring(0, name.Length < 35 ? name.Length : 35);
                    //NAME - CFN2
                    if (name.Length > 35)
                        rowstr += "," + name.Substring(35, name.Length < 70 ? name.Length - 35 : 35);
                    else
                        rowstr += ",";
                    
                    string address = dt.Rows[i]["ADDRESSLINE1"].ToString().Trim()
                        + " " + dt.Rows[i]["ADDRESSLINE2"].ToString().Trim()
                        + " " + dt.Rows[i]["ADDRESSLINE3"].ToString().Trim()
                        + " " + dt.Rows[i]["ADDRESSLINE4"].ToString().Trim()
                        + " " + dt.Rows[i]["ADDRESSLINE5"].ToString().Trim()
                        + " " + dt.Rows[i]["ADDRESSLINE6"].ToString().Trim()
                        + " " + dt.Rows[i]["ADDRESSLINE7"].ToString().Trim();
                    address = address.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                    string address1="", address2="", address3 = "";
                    address1 = address.Length > 35 ? address.Substring(0, 35) : address;
                    address2 = address.Length <= 35 ? "" : address.Length > 70 ? address.Substring(35, 70) : address.Substring(35);
                    address3 = address.Length > 70 ? address.Substring(70) : "";

                    rowstr += "," + address1;      //CA1
                    rowstr += "," + address2;      //CA2 //ADDRESS ENG
                    rowstr += "," + address3;      //CA3
                    rowstr += "," + dt.Rows[i]["STATECODE"].ToString().Trim().Substring(0, dt.Rows[i]["STATECODE"].ToString().Trim().Length < 35 ? dt.Rows[i]["STATECODE"].ToString().Trim().Length : 35); //CA4
                    rowstr += "," + dt.Rows[i]["IDNUMBER"].ToString().Trim().Substring(0, dt.Rows[i]["IDNUMBER"].ToString().Trim().Length < 35 ? dt.Rows[i]["IDNUMBER"].ToString().Trim().Length : 35); //CA5
                    rowstr += "," + dt.Rows[i]["POSTALCODE"].ToString(); //CPOST
                    rowstr += ",";  //BIC
                    rowstr += "," + dt.Rows[i]["PRIMARYNAME04"].ToString().Trim().Substring(0, dt.Rows[i]["PRIMARYNAME04"].ToString().Trim().Length < 10 ? dt.Rows[i]["PRIMARYNAME04"].ToString().Trim().Length : 10); //SIC
                    rowstr += "," + dt.Rows[i]["COUNTRYCODE"].ToString().Trim(); //CCODE
                    rowstr += "," + dt.Rows[i]["COUNTRYOFCITIZENSHIPINCORPO"].ToString(); //UCCODE
                    rowstr += ",DEFAULT"; //ACCTNGTYPE
                    rowstr += "," + dt.Rows[i]["CUSTOMERCATEGORY"].ToString(); //CTYPE
                    rowstr += "," + dt.Rows[i]["REGISTERDATE"].ToString(); //BIRTHDATE
                    rowstr += "," + dt.Rows[i]["TAXNUMBER"].ToString(); //TAXID
                    rowstr += ",";  //BROKNO
                    rowstr += ",";  //BROKID
                    rowstr += ",";  //SUPPLEMENTAL
                    rowstr += ",";  //AGENTIND
                    rowstr += ",";  //NOTETEXT
                    rowstr += ",SYS1";  //IOPER
                    /*
                    //Address
                    string address = dt.Rows[i]["ADDRESSLINE1"].ToString().Trim() + dt.Rows[i]["ADDRESSLINE2"].ToString().Trim() + dt.Rows[i]["ADDRESSLINE3"].ToString().Trim();
                    //Address - CA1
                    rowstr += "," + address.Substring(0, address.Length < 35 ? address.Length : 35);
                    //Address - CA2
                    if (address.Length > 35)
                        rowstr += "," + address.Substring(35, address.Length < 70 ? address.Length - 35 : 35);
                    else
                        rowstr += ",";
                    */

                    //str.Append(rowstr);
                    str.AppendLine(rowstr);
                }

                Byte[] info = new UTF8Encoding(true).GetBytes(str.ToString());
                f.Write(info, 0, info.Length);
                f.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }

        protected void SendDataSetBox_Click(object sender, EventArgs e)
        {
            try
            {
                string key = GetKeyCheckBox();
                if (key.Trim().Equals(""))
                {
                    MessageBox.Text = "กรุณาเลือกรายการ เพื่อส่งไปยัง TDS";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    string message = "";
                    if (CBSCustomer.TransferToDataSet(Config.TDSConnectionString, key, out message))
                    {
                        MessageBox.Text = message;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                    else {
                        MessageBox.Text = message;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                }
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

        protected string GetKeyCheckBox()
        {
            try
            {
                string key = "";
                for (int i = 0; i < GridData.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GridData.Rows[i].Cells[0].FindControl("chkCustomer");
                    if (chk.Checked)
                    {
                        key += " " + GridData.DataKeys[i].Value.ToString();
                    }
                }

                key = key.Trim().Replace(' ', ',');
                return key;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return "";
            }
        }
    }
}
