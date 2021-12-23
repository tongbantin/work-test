using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Net.Mime;
using System.Text;
using TreasuryBusiness.ThirdParty;
using TreasuryModel.Master;
using System.Web.UI.HtmlControls;
using log4net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using TreasuryShadowSystem.Configuration;

namespace TreasuryShadowSystem.Bond
{
    public partial class frmBondUpload : System.Web.UI.Page
    {
        
        DataSet pds = new DataSet();
        String FolderName = "";
        int SeqStart = 0;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frmBondUpload));
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                gdData.RowDataBound += new GridViewRowEventHandler(gdData_RowDataBound);
                //gdData.SelectedIndexChanging += new GridViewSelectEventHandler(gdData_SelectedIndexChanging);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        


        void gdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                if (e.Row.RowIndex >= 0)
                {
                    //int row = e.Row.RowIndex + 1;
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text + SeqStart.ToString("00000");
                    //e.Row.Cells[4].Text = e.Row.Cells[3].Text;
                    SeqStart += 1;
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
                lblMessage.Text = "";
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
                lblMessage.Text = "";
                String filename = DateTime.Now.ToString("yyyyMMddHHmm") + MyFile.FileName;
                String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                String secid = MyFile.FileName.Substring(0, MyFile.FileName.IndexOf("."));
                if ((MyFile.PostedFile.ContentType.Equals("application/vnd.ms-excel")) || (MyFile.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")))
                {
                    MyFile.SaveAs(pathFile);

                    if (File.Exists(Server.MapPath("~/Temporary") + "\\" + filename))
                    {
                        //HSSFWorkbook hssfwb;
                        //using (FileStream file = new FileStream(pathFile, FileMode.Open, FileAccess.ReadWrite))
                        //{
                        //    hssfwb = new HSSFWorkbook(file);
                        //    List<Model_MS_PTITRANS> l = new List<Model_MS_PTITRANS>();
                        //    FolderName = "PTI" + DateTime.Now.ToString("yyyyMMddHHmm");
                        //    DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Temporary/" + FolderName + "/"));
                        //    ISheet sheet = hssfwb.GetSheetAt(0);


                        //    for (int i = 1; i < sheet.LastRowNum; i++)
                        //    {
                        //        Model_MS_PTITRANS md = new Model_MS_PTITRANS();
                        //        md.TransLogNo = "PTI" + sheet.GetRow(i + 1).GetCell(4).ToString() + DateTime.Now.ToString("yyyyMMdd");
                        //        md.TransLogDate = DateTime.Now.ToString("dd/MM/yyyy");
                        //        md.SecID = sheet.SheetName;

                        //        for (int col = 0; col < 25; col++)
                        //        {
                        //            sheet.GetRow(i).Cells[col].SetCellType(CellType.String);
                        //        }

                        //        md.PartialID = sheet.GetRow(i).Cells[0].StringCellValue;
                        //        md.MsgType = sheet.GetRow(i).Cells[1].StringCellValue;
                        //        md.Priority = sheet.GetRow(i).Cells[2].StringCellValue;
                        //        md.SenderRef = sheet.GetRow(i).Cells[3].StringCellValue;
                        //        md.RelateRef = sheet.GetRow(i).Cells[4].StringCellValue;
                        //        md.PlaceCode = sheet.GetRow(i).Cells[5].StringCellValue;
                        //        md.SettDate = sheet.GetRow(i).Cells[6].StringCellValue;
                        //        md.TradeDate = sheet.GetRow(i).Cells[7].StringCellValue;
                        //        md.PercentType = sheet.GetRow(i).Cells[8].StringCellValue;
                        //        md.DealPrice = sheet.GetRow(i).Cells[9].StringCellValue;
                        //        md.ISIN = sheet.GetRow(i).Cells[10].StringCellValue;
                        //        md.FaceAmt = sheet.GetRow(i).Cells[11].StringCellValue;
                        //        md.AcctNo = sheet.GetRow(i).Cells[12].StringCellValue;
                        //        md.CashAcct = sheet.GetRow(i).Cells[13].StringCellValue;
                        //        md.BusinessType = sheet.GetRow(i).Cells[14].StringCellValue;
                        //        md.SettType = sheet.GetRow(i).Cells[15].StringCellValue;
                        //        md.PS = sheet.GetRow(i).Cells[16].StringCellValue;
                        //        md.SettPlace = sheet.GetRow(i).Cells[17].StringCellValue;
                        //        md.Attn = sheet.GetRow(i).Cells[18].StringCellValue;
                        //        md.AgentBIC = sheet.GetRow(i).Cells[19].StringCellValue;
                        //        md.AgentCA = sheet.GetRow(i).Cells[20].StringCellValue;
                        //        md.AgentAcctNo = sheet.GetRow(i).Cells[21].StringCellValue;
                        //        md.PrinAmt = sheet.GetRow(i).Cells[22].StringCellValue;
                        //        md.IntAmt = sheet.GetRow(i).Cells[23].StringCellValue;
                        //        md.SettAmt = sheet.GetRow(i).Cells[24].StringCellValue;

                        //        l.Add(md);

                        //        FileStream f = File.Create(Server.MapPath("~/Temporary/" + FolderName) + "/PTI" + md.MsgType + md.SecID + md.SenderRef + ".xml");
                        //        Byte[] info = new UTF8Encoding(true).GetBytes(PTI.ExcelToXML(md));
                        //        f.Write(info, 0, info.Length);
                        //        f.Close();
                        //    }
                        //    //ViewState["dt"] = dt;

                        //    //SeqStart = Model_MS_PTITRANS.getNextSequence(ConnectionString, dt.Rows[0]["RelatedRef"].ToString());

                        //    gdData.DataSource = l;
                        //    gdData.DataBind();

                        //    Session["FolderName"] = FolderName;
                        //    Session["PTITrans"] = l;
                        //    Compress();

                        //    btnDownload.Visible = false;
                        //    btnGoTo.Visible = false;
                        //    btnSave.Visible = true;
                        //}


                        //String ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=Yes;\";";
                        String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 8.0;HDR=Yes;\";";
                        OleDbConnection _oleConn = new OleDbConnection();
                        try
                        {
                            _oleConn.ConnectionString = ConnectionString;
                            if (_oleConn.State == ConnectionState.Open)
                                _oleConn.Close();
                            _oleConn.Open();
                            OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [" + secid + "$] WHERE PartiID is not null ", _oleConn);
                            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                            oleAdapter.SelectCommand = _oleCmdSelect;
                            DataTable dt = new DataTable(secid);

                            oleAdapter.FillSchema(dt, SchemaType.Source);
                            oleAdapter.Fill(dt);

                            ViewState["dt"] = dt;

                            SeqStart = Model_MS_PTITRANS.getNextSequence(Config.ConnectionString, dt.Rows[0]["RelatedRef"].ToString());

                            gdData.DataSource = dt;
                            gdData.DataBind();

                            List<Model_MS_PTITRANS> l = new List<Model_MS_PTITRANS>();

                            FolderName = "PTI" + DateTime.Now.ToString("yyyyMMddHHmm");
                            DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Temporary/" + FolderName + "/"));

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Model_MS_PTITRANS md = new Model_MS_PTITRANS();
                                md.TransLogNo = "PTI" + dt.Rows[i]["RelatedRef"].ToString() + DateTime.Now.ToString("yyyyMMdd");
                                md.TransLogDate = DateTime.Now.ToString("dd/MM/yyyy");
                                md.SecID = secid;
                                md.PartialID = dt.Rows[i]["PartiID"].ToString();
                                md.MsgType = dt.Rows[i]["MsgType"].ToString();
                                md.Priority = dt.Rows[i]["Priority"].ToString();
                                md.SenderRef = gdData.Rows[i].Cells[3].Text;
                                md.RelateRef = gdData.Rows[i].Cells[4].Text;
                                md.PlaceCode = dt.Rows[i]["PlaceCode"].ToString();
                                md.SettDate = dt.Rows[i]["SettlementDate"].ToString();
                                md.TradeDate = dt.Rows[i]["TradeDate"].ToString();
                                md.PercentType = dt.Rows[i]["PercentageType"].ToString();
                                md.DealPrice = dt.Rows[i]["DealPrice"].ToString();
                                md.ISIN = dt.Rows[i]["ISIN"].ToString();
                                md.FaceAmt = dt.Rows[i]["FaceAmount"].ToString();
                                md.AcctNo = dt.Rows[i]["AcctNo"].ToString();
                                md.CashAcct = dt.Rows[i]["CashAcct"].ToString();
                                md.BusinessType = dt.Rows[i]["BusinessType"].ToString();
                                md.SettType = dt.Rows[i]["SettlementType"].ToString();
                                md.PS = dt.Rows[i]["PS"].ToString();
                                md.SettPlace = dt.Rows[i]["SettlementPlace"].ToString();
                                md.Attn = dt.Rows[i]["Attn"].ToString().Replace("'","''");
                                md.AgentBIC = dt.Rows[i]["AgentBIC"].ToString();
                                md.AgentCA = dt.Rows[i]["AgentCA"].ToString();
                                md.AgentAcctNo = dt.Rows[i]["AgentAcctNo"].ToString();
                                md.PrinAmt = dt.Rows[i]["PrinAmt"].ToString();
                                md.IntAmt = dt.Rows[i]["IntAmt"].ToString();
                                md.SettAmt = dt.Rows[i]["SettlementAmt"].ToString();

                                l.Add(md);

                                FileStream f = File.Create(Server.MapPath("~/Temporary/" + FolderName) + "/PTI" + md.MsgType + md.SecID + md.SenderRef + ".xml");
                                Byte[] info = new UTF8Encoding(true).GetBytes(PTI.ExcelToXML(md));
                                f.Write(info, 0, info.Length);
                                f.Close();

                            }
                            Session["FolderName"] = FolderName;
                            Session["PTITrans"] = l;
                            Compress();

                            btnDownload.Visible = false;
                            btnGoTo.Visible = false;
                            btnSave.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                        }
                        finally
                        {
                            _oleConn.Close();
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "ประเภทไฟล์ไม่ถูกต้อง";
                }

                //ViewState["PTIXML"] = PTI.ExcelToXML(l);                
                //lblXML.Text = PTI.ExcelToXML();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            { 
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                List<Model_MS_PTITRANS> l = (List<Model_MS_PTITRANS>)Session["PTITrans"];

                Model_MS_PTITRANS pt = new Model_MS_PTITRANS();
                if (pt.save(Configuration.Config.ConnectionString, l))
                {
                    string str = "~/Temporary/" + Session["FolderName"].ToString() + ".zip";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenConfirmation", "window.open(\"../Bond/DownloadFile.aspx?q=" + str + "\");", true);

                    btnSave.Visible = false;
                    btnDownload.Visible = true;
                    btnGoTo.Visible = true;

                    //string result = Path.GetFileName(str);
                    //Response.ContentType = "application/zip";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Session["FolderName"].ToString() + ".zip");
                    //Response.TransmitFile(Server.MapPath(str));
                    //Response.End();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public void Compress()
        {
            try
            {
                using (Package Zip = Package.Open(Server.MapPath("~/Temporary/") + FolderName + ".zip",FileMode.Create))
                {
                    foreach (string F in Directory.GetFiles(Server.MapPath("~/Temporary/" + FolderName + "/"),
                                                            "PTI*.xml",
                                                            SearchOption.AllDirectories))
                    {
                        string F1 = F.Replace(' ', '_'); //ZipPackage can't handle spaces in filenames.
                        Uri URI = new Uri(F1.Substring(Server.MapPath("~/Temporary/" + FolderName + "/").Length), UriKind.Relative);
                        PackagePart ZipPart = Zip.CreatePart(PackUriHelper.CreatePartUri(URI),
                                                             MediaTypeNames.Application.Zip,
                                                             CompressionOption.Maximum);
                        byte[] B = File.ReadAllBytes(F);
                        ZipPart.GetStream().Write(B, 0, B.Length);
                    }
                }
                //Directory.Delete(Server.MapPath("~/Temporary/") + FolderName);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }            
        }

        protected void btnGoTo_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Bond/frmBondTransactionList.aspx");
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
                Response.Redirect("~/Temporary/" + Session["FolderName"].ToString() + ".zip", false);                    
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        
    }
}
