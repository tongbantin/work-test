using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryModel.Model;
using System.Data;
using log4net;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR089IMPRATE : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR089IMPRATE));
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            try
            {
                if (!IsPostBack)
                    DateBox.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                if (FileRate.PostedFile.ContentLength > 1)
                {
                    string Filename = Server.MapPath("~/Temporary/") + DateTime.Now.ToString("yyyyMMddHHmmss") + FileRate.PostedFile.FileName;
                    Log.Info("Import Rate WIth Filename : " + Filename);
                    FileRate.PostedFile.SaveAs(Filename);
                    if (ImportRate(Filename))
                    {
                        MessageBox.Text = "ระบบนำเข้าข้อมูลเรียบร้อยแล้ว";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                    else
                    {
                        MessageBox.Text = "ระบบไม่สามารถนำเข้าข้อมูลได้ กรุณาทำรายการใหม่";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                }
                else {
                    MessageBox.Text = "กรุณาเลือกไฟล์ที่ต้องการนำเข้า";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = "เกิดข้อผิดพลาด " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }

        protected bool ImportRate(string Filename)
        {
            try
            {
                ImportRateModel rate = new ImportRateModel();
                rate.ClearRate(Config.ConnectionString);

                string ExcelFile = Filename;
                string[] SheetName = Config.GetParameterFile("089SheetName").Split(',');

                ExcelRateModel excel = new ExcelRateModel();
                List<DataTable> l = excel.ExcelDataTable(ExcelFile, RoundBox.SelectedValue, DateBox.Text, SheetName);

                Log.Debug("Start Merge Table Oracle & Excel");
                
                if (rate.AddRate(Config.ConnectionString, RoundBox.SelectedValue, l))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }
    }
}
