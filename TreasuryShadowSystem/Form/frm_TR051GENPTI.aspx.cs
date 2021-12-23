using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Net.Mime;
using KKB.Treasury.TreasuryReport.Report;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Master;
using TreasuryModel.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using log4net;
using System.Text;
using TreasuryBusiness.ThirdParty;
using TreasuryModel.Data;
using System.Globalization;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR051GENPTI : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR051GENPTI));
        private const long BUFFER_SIZE = 4096;
        String FolderName = "", settleDate = "" ;
        


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {              

                GridViewAllotment.PageIndexChanging += new GridViewPageEventHandler(GridViewAllotment_PageIndexChanging);
                //CheckAll.Value = "";
                

                if (!IsPostBack)
                {
                   // BindAttn();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

      

        void GridViewAllotment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridViewAllotment.PageIndex = e.NewPageIndex;
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
                DateTime st = DateTime.ParseExact(SettleDateSearch.Text, "dd/MM/yyyy", CultureInfo.CurrentUICulture);
                settleDate = st.ToString("yyyyMMdd");

                string searchDeal = "";
                if (!DealnoSearch.Text.Trim().Equals("")) {
                    searchDeal = "'" + DealnoSearch.Text + "'";
                    searchDeal = searchDeal.Replace(",", "\',\'");
                }
                

                GridViewAllotment.DataSource = Model_MS_PTITRANS.getTransLogBySettleDate(Config.ConnectionString, settleDate, searchDeal);
                GridViewAllotment.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {              

                if (SettleDateSearch.Text.Equals("") || SettleDateSearch.Text == null)
                {
                
                    MessageBox.Text = "กรุณาระบุวันที่ต้องการค้นหา";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }else{
                                     
                    BindGrid();
                    btnDownload.Visible = false;
                    btnSave.Visible = false;

                    if (GridViewAllotment.Rows.Count > 0)
                    {
                        btnSave.Visible = true;
                    }                
                }
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt;              
                String partial = "", secid = "", dealno = "";
                List<Model_MS_PTITRANS> l = new List<Model_MS_PTITRANS>();
                Model_MS_PTITRANS pt = new Model_MS_PTITRANS();

                DateTime st = DateTime.ParseExact(SettleDateSearch.Text, "dd/MM/yyyy", CultureInfo.CurrentUICulture);
                settleDate = st.ToString("yyyyMMdd");

                FolderName = settleDate + "_" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(Server.MapPath("~/Temporary/" + FolderName + "/")))
                    Directory.CreateDirectory(Server.MapPath("~/Temporary/" + FolderName + "/"));

                string searchDeal = "";
                if (!DealnoSearch.Text.Trim().Equals(""))
                {
                    searchDeal = "'" + DealnoSearch.Text + "'";
                    searchDeal = searchDeal.Replace(",", "\',\'");
                }

                string key = "";
                for (int i = 0; i < GridViewAllotment.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox)GridViewAllotment.Rows[i].Cells[0].FindControl("chkSelectAllot");
                    if (chk.Checked)
                    {
                        key += " " + GridViewAllotment.DataKeys[i].Value.ToString();
                    }
                }
                key = key.Trim().Replace(' ', ',');

                if (key.Equals(""))
                {
                    MessageBox.Text = "กรุณาเลือกรายการที่ต้องการสร้าง XML ไฟล์";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
                else
                {
                   //Get data All
                    Log.Info("PTI Select All : " + CheckAll.Value);

                   if (CheckAll.Value.Equals("ALL"))
                   {
                       dt = Model_MS_PTITRANS.getTransLogBySettleDate(Config.ConnectionString, settleDate, searchDeal);
                   }else{
                   //Get data from selected
                       
                        dt = Model_MS_PTITRANS.getTransLogBySelected(Config.ConnectionString, key);
                    }

                   Log.Info("PTI records : " + dt.Rows.Count);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Model_MS_PTITRANS md = new Model_MS_PTITRANS();
                        partial = dt.Rows[i]["PartialID"].ToString().Trim();
                        secid = dt.Rows[i]["SecID"].ToString().Trim();
                        dealno = dt.Rows[i]["RelateRef"].ToString().Trim();

                        md.TransLogNo = dt.Rows[i]["TransLogNo"].ToString().Trim();
                        md.TransLogDate = dt.Rows[i]["TransLogDate"].ToString().Trim();
                        md.SecID = dt.Rows[i]["SecID"].ToString().Trim();
                        md.PartialID = dt.Rows[i]["PartialID"].ToString().Trim();
                        md.MsgType = dt.Rows[i]["MsgType"].ToString().Trim();
                        md.Priority = dt.Rows[i]["Priority"].ToString().Trim();
                        md.SenderRef = dt.Rows[i]["SenderRef"].ToString().Trim();
                        md.RelateRef = dt.Rows[i]["RelateRef"].ToString().Trim();
                        md.PlaceCode = dt.Rows[i]["PlaceCode"].ToString().Trim();
                        md.SettDate = dt.Rows[i]["SettDate"].ToString().Trim();
                        md.TradeDate = dt.Rows[i]["TradeDate"].ToString().Trim();
                        md.PercentType = dt.Rows[i]["PercentType"].ToString().Trim();
                        md.DealPrice = dt.Rows[i]["DealPrice"].ToString().Trim();
                        md.ISIN = dt.Rows[i]["ISIN"].ToString().Trim();
                        md.FaceAmt = dt.Rows[i]["FaceAmt"].ToString().Trim();
                        md.AcctNo = dt.Rows[i]["AcctNo"].ToString().Trim();
                        md.CashAcct = dt.Rows[i]["CashAcct"].ToString().Trim();
                        md.BusinessType = dt.Rows[i]["BusinessType"].ToString().Trim();
                        md.SettType = dt.Rows[i]["SETTTYPE"].ToString().Trim();
                        md.PS = dt.Rows[i]["PS"].ToString().Trim();
                        md.SettPlace = dt.Rows[i]["SETTPLACE"].ToString().Trim();
                        md.Attn = dt.Rows[i]["Attn"].ToString().Replace("'", "''").Trim();
                        md.AgentBIC = dt.Rows[i]["AgentBIC"].ToString().Trim();
                        md.AgentCA = dt.Rows[i]["AgentCA"].ToString().Trim();
                        md.AgentAcctNo = dt.Rows[i]["AgentAcctNo"].ToString().Trim();
                        md.PrinAmt = dt.Rows[i]["PrinAmt"].ToString().Trim();
                        md.IntAmt = dt.Rows[i]["IntAmt"].ToString().Trim();
                        md.SettAmt = dt.Rows[i]["SettAmt"].ToString().Trim();

                        l.Add(md);
                        //string filename = Server.MapPath("~/Temporary/" + FolderName) + "/PTI" + dt.Rows[i]["MsgType"].ToString().Trim() + dt.Rows[i]["SecID"].ToString().Trim() + dt.Rows[i]["SenderRef"].ToString().Trim() + string.Format("{0:00000}",i+1) + ".xml";
                        //FileStream f = File.Create(filename);
                        //Byte[] info = new UTF8Encoding(true).GetBytes(PTI.ExcelToXML(md));
                        //f.Write(info, 0, info.Length);
                        //f.Flush();
                        //f.Close();
                    }

                    string filename = Server.MapPath("~/Temporary/" + FolderName) + "/PTI" + partial + secid + dealno + ".xml";
                    FileStream f = File.Create(filename);
                    Byte[] info = new UTF8Encoding(true).GetBytes(PTI.ExcelToXMLList(l));
                    f.Write(info, 0, info.Length);
                    f.Flush();
                    f.Close();

                    Compress();

                    Session["FolderName"] = FolderName;
                    if (pt.setGenerate(Config.ConnectionString, settleDate))
                    {
                        btnSave.Visible = false;
                        btnDownload.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = true;
                        btnDownload.Visible = false;

                        MessageBox.Text = "ไม่สามารถสร้างไฟล์ได้ กรุณาทำรายการใหม่";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                
                }


            }   
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
      
      
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime st = DateTime.ParseExact(SettleDateSearch.Text, "dd/MM/yyyy", CultureInfo.CurrentUICulture);
                settleDate = st.ToString("yyyyMMdd");

                FolderName = settleDate + "_" + DateTime.Now.ToString("yyyyMMdd");

                //Response.Redirect("~/Temporary/" + Session["FolderName"].ToString() + ".zip", false);
                Response.Redirect("~/Temporary/" + FolderName + ".zip", false);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        #region method 

        public void Compress()
        {
            try
            {

                //Clear old zip file
                if (File.Exists(Server.MapPath("~/Temporary/") + FolderName + ".zip"))
                    File.Delete(Server.MapPath("~/Temporary/") + FolderName + ".zip");

                //Zip Folder
                using (Package Zip = Package.Open(Server.MapPath("~/Temporary/") + FolderName + ".zip", FileMode.OpenOrCreate))
                {
                    foreach (string F in Directory.GetFiles(Server.MapPath("~/Temporary/" + FolderName + "/"), "PTI*.xml", SearchOption.AllDirectories))
                    {
                        string F1 = F.Replace(' ', '_'); //ZipPackage can't handle spaces in filenames.
                        Uri URI = new Uri(F1.Substring(Server.MapPath("~/Temporary/" + FolderName + "/").Length), UriKind.Relative);

                        PackagePart ZipPart = Zip.CreatePart(PackUriHelper.CreatePartUri(URI), MediaTypeNames.Application.Zip, CompressionOption.Maximum);
                        byte[] B = File.ReadAllBytes(F);
                        ZipPart.GetStream().Write(B, 0, B.Length);
                    }
                }

                //Clear File After Zip
                if (Directory.Exists(Server.MapPath("~/Temporary/") + FolderName))
                {
                    string[] f = Directory.GetFiles(Server.MapPath("~/Temporary/") + FolderName);
                    for (int l = 0; l < f.Length; l++)
                    {
                        File.Delete(f[l]);
                    }
                    Directory.Delete(Server.MapPath("~/Temporary/") + FolderName);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        #endregion
    }
}
