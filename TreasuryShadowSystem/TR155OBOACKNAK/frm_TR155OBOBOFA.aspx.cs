using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasuryModel.Data;
using System.IO;
using log4net;
using System.Collections;

namespace TreasuryShadowSystem.TR155OBOACKNAK
{
    public partial class frm_TR155OBOBOFA : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR155OBOBOFA));
        private ArrayList error;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            String filename = DateTime.Now.ToString("yyyyMMddHHmm") + MyFile.FileName;
            String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
            List<CashPro> l = new List<CashPro>();
            try
            {
                MyFile.SaveAs(pathFile);
                string[] lines = File.ReadAllLines(pathFile);
                foreach (string data in lines)
                {
                    string[] cpro = data.Split(',');
                    try
                    {
                        CashPro cp = new CashPro();
                        cp.ASOF = DateTime.Parse(cpro[0].Replace("\"", ""));
                        cp.CCY = cpro[1].Replace("\"", "");
                        cp.BANK_ID_TYPE = cpro[2].Replace("\"", "");
                        cp.BANK_ID = cpro[3].Replace("\"", "");
                        cp.ACCOUNT = cpro[4].Replace("\"", "");
                        cp.DATA_TYPE = cpro[5].Replace("\"", "");
                        cp.BAI_CODE = cpro[6].Replace("\"", "");
                        cp.DESCRIPTION = cpro[7].Replace("\"", "");
                        cp.AMOUNT = decimal.Parse(cpro[8].Replace("\"", ""));
                        cp.VALUE_DATE = DateTime.Parse(cpro[9].Replace("\"", ""));
                        cp.CUST_REFERENCE = cpro[10].Replace("\"", "");
                        cp.IMMEDIATE_AVAILABILITY = cpro[11].Replace("\"", "");
                        cp.DAY1_FLOAT = cpro[12].Replace("\"", "");
                        cp.DAY2_FLOAT = cpro[13].Replace("\"", "");
                        cp.BANK_REFERENCE = cpro[14].Replace("\"", "");
                        cp.OF_ITEMS = cpro[15].Replace("\"", "");
                        cp.TEXT_DESCRIPTION = cpro[16].Replace("\"", "");

                        l.Add(cp);
                    }
                    catch (Exception ex)
                    { 
                    
                    }
                }
                Session["CashPro"] = l;
                BindGrid();
                btnSave.Visible = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<CashPro> l = (List<CashPro>)Session["CashPro"];

                CashPro cp = new CashPro();
                error = cp.save(Configuration.Config.ConnectionString, l);
                if (error.Count <= 0)
                {
                    BindGrid(); 
                    MessageBox.Text = "บันทึกข้อมูลเรียบร้อยแล้ว";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                    MessageBox.Text = "ไม่สามารถบันทึกบางรายการได้";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {
                if (error.Count <= 0)
                {
                    btnSave.Visible = false;
                    Session["SoftLoan"] = null;

                    Session.Remove("SoftLoan");
                }
            }
        }

        void BindGrid()
        {
            try
            {
                List<CashPro> l = (List<CashPro>)Session["CashPro"];
                GridData.DataSource = l;
                GridData.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }
    }
}
