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
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;

using System.Data.OleDb;
using TreasuryModel.Data;
using System.Collections;
using System.Globalization;


namespace TreasuryShadowSystem.Swift
{
    public partial class frm_ImportSwift : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_ImportSwift));
        private ArrayList error;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList error = new ArrayList();
                String filename = DateTime.Now.ToString("yyyyMMddHHmm") + SwiftFile.FileName;
                String pathFile = Server.MapPath("~/Temporary") + "\\" + filename;
                // Check Excel file type
                List<SwiftImport> l = new List<SwiftImport>();
                if ((SwiftFile.PostedFile.ContentType.Equals("application/octet-stream")) || (SwiftFile.PostedFile.ContentType.Equals("application/vnd.ms-excel")) || (SwiftFile.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")))
                {
                    SwiftFile.SaveAs(pathFile);

                    if (File.Exists(Server.MapPath("~/Temporary") + "\\" + filename))
                    {
                        String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Temporary") + "\\" + filename + ";" + "Extended Properties=\"Excel 8.0;IMEX=1;HDR=No;\";";
                        OleDbConnection _oleConn = new OleDbConnection();
                        try
                        {
                            Log.Debug("Start Read Data From Excel");
                            _oleConn.ConnectionString = ConnectionString;
                            if (_oleConn.State == ConnectionState.Open)
                                _oleConn.Close();
                            _oleConn.Open();
                            string myTableName = _oleConn.GetSchema("Tables").Rows[1]["TABLE_NAME"].ToString();
                            OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [" + myTableName + "]", _oleConn);
                            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                            oleAdapter.SelectCommand = _oleCmdSelect;
                            DataTable dt = new DataTable();

                            oleAdapter.FillSchema(dt, SchemaType.Source);
                            oleAdapter.Fill(dt);
                            Session["SwiftImport"] = null;

                            //Check value date and swift type before insert
                            List<string> vdate = SelectDistict<string>(dt, "F9");
                            List<string> stype = SelectDistict<string>(dt, "F3");

                            if (SwiftType.SelectedValue.Equals(stype[1]))
                            {
                                CultureInfo culture = new CultureInfo("en-CA"); //CultureInfo.CreateSpecificCulture("en-NZ");
                                DateTimeStyles styles = DateTimeStyles.None;

                                Log.Debug("Start Add Data To Object");
                                for (int i = 1; i < dt.Rows.Count; i++)
                                {

                                    SwiftImport swift = new SwiftImport();
                                    DateTime _vdate, _netw_date, _create_date;
                                    Decimal _swift_amt;
                                    String[] str1;
                                    try
                                    {

                                        swift.SWIFT_IO = dt.Rows[i][0].ToString();
                                        swift.SWIFT_CORS = dt.Rows[i][1].ToString();
                                        swift.SWIFT_TYPE = dt.Rows[i][2].ToString();
                                        swift.SWIFT_REF = dt.Rows[i][3].ToString();
                                        swift.SWIFT_MUR = dt.Rows[i][4].ToString();
                                        swift.SWIFT_TRANS_REF = dt.Rows[i][5].ToString();
                                        swift.SWIFT_SUFFIX = dt.Rows[i][7].ToString();
                                        swift.SWIFT_CURR = dt.Rows[i][9].ToString().Substring(0, 3);
                                        swift.SWIFT_STATUS = dt.Rows[i][10].ToString();
                                        swift.SWIFT_NETW_STATUS = dt.Rows[i][11].ToString();
                                        swift.SWIFT_ORIG_INST = dt.Rows[i][13].ToString();
                                        swift.SWIFT_SERVICE_NAME = dt.Rows[i][15].ToString();

                                        str1 = Convert.ToString(dt.Rows[i][9]).Split(" ".ToCharArray());
                                        if (str1[1].Substring(str1[1].Length - 1).Equals("."))
                                        {
                                            str1[1] = str1[1].Substring(0, str1[1].Length - 1).Replace(",", "");
                                        }
                                        else
                                        {
                                            str1[1] = str1[1].Replace(",", "");
                                        }
                                        if (Decimal.TryParse(str1[1], out _swift_amt))
                                            swift.SWIFT_AMT = _swift_amt;
                                        if (DateTime.TryParse(dt.Rows[i][8].ToString(), culture, styles, out _vdate))
                                            swift.SWIFT_VDATE = _vdate;
                                        if (DateTime.TryParse(dt.Rows[i][12].ToString(), culture, styles, out _netw_date))
                                            swift.SWIFT_NETW_DATE = _netw_date;
                                        if (DateTime.TryParse(dt.Rows[i][14].ToString(), culture, styles, out _create_date))
                                            swift.SWIFT_CREATE_DATE = _create_date;

                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error(ex.Message);
                                    }
                                    l.Add(swift);
                                }
                                Session["SwiftImport"] = l;

                                //Save To Database
                                Log.Debug("Start Insert Swift To Database");
                                SwiftImport swiftImport = new SwiftImport();
                                error = swiftImport.save(Configuration.Config.ConnectionString, l, vdate, SwiftType.SelectedValue, error);
                            }
                            else {
                                error.Add("mismatch");                            
                            }
                            
                            if (error.Count <= 0)
                            {
                               MessageBox.Text = "ระบบบันทึกข้อมูลเรียบร้อยแล้ว";
                                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                                                           }
                            else
                            {
                                if (error[0].Equals("mismatch"))
                                    MessageBox.Text = "ประเภท Swift ที่เลือกไม่ตรงกับที่อัพโหลด  กรุณาตรวจสอบข้อมูล";
                                else
                                    MessageBox.Text = "ไม่สามารถบันทึกบางรายการได้ กรุณาตรวจสอบข้อมูล";
                                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                            MessageBox.Text = "ไม่สามารถบันทึกบางรายการได้ กรุณาตรวจสอบข้อมูล";
                            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                        }
                        finally
                        {
                            _oleConn.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Text = "ประเภทไฟล์ไม่ถูกต้อง";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }



            /*
            try
            {
                if (SwiftFile.PostedFile.ContentLength > 1)
                {
                    string Filename = Server.MapPath("~/Temporary/") + DateTime.Now.ToString("yyyyMMddHHmmss") + SwiftFile.PostedFile.FileName;
                    Log.Info("Import Swift WIth Filename : " + Filename);
                    SwiftFile.PostedFile.SaveAs(Filename);
                    if (ImportSwiftFile(Filename))
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
              
             */
        } // for submit button

/*

        protected bool ImportSwiftFile(string Filename)
        {
            try
            {
                ExcelSwiftModel excel = new ExcelSwiftModel();
                List<DataTable> l = excel.ExcelDataTable(Filename, SwiftType.SelectedValue);
                //DataView view = new DataView(l);
                //DataTable distinctValues = view.ToTable(true, "id");

                ImportSwiftModel rate = new ImportSwiftModel();
                rate.ClearRate(Config.ConnectionString); //clear specially data at same date as upload file                   
                Log.Debug("Start Merge Table Oracle & Excel");

                if (rate.AddRate(Config.ConnectionString, SwiftType.SelectedValue, l))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
   */

        List<T> SelectDistict<T>(DataTable table, string column)
        {
            DataTable temp = new DataView(table).ToTable(true, column);
            List<T> items = new List<T>();
            foreach (DataRow row in temp.Rows)
                items.Add(row.Field<T>(column));
            return items;
            
            //DataRow row = temp.Rows.;
            //for (int i = 1; i < table.Rows.Count; i++) {
            //    items.Add(row.Field<T>(column));
            //}
            //return items;
        }


    } // for class   
}
