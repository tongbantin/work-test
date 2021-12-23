using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;

using System.Configuration;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Data;
using KKB.Treasury.TreasuryModel.Model;
using System.Data;
using log4net;
using System.Data.OleDb;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TreasuryShadowSystem.STN
{
    public partial class UploadSTN : System.Web.UI.Page
    {

        private static ILog Log = log4net.LogManager.GetLogger(typeof(UploadSTN));


        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            try
            {
                if (!IsPostBack) { } 
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
                if (FileUpload1.PostedFile.ContentLength > 1)
                {
                    string pathname = Server.MapPath("~/Temporary/STN/");
                    string Filename = pathname + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.PostedFile.FileName;
                    Log.Info("Import Filename : " + Filename);


                    if (!Directory.Exists(pathname))
                    {
                        Directory.CreateDirectory(pathname);
                    }
                    //save file
                    FileUpload1.PostedFile.SaveAs(Filename);
                     
                    //get filetype                    
                    string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).Substring(1);
                    string msg = string.Empty;
                    List<KKBSTN> List = new List<KKBSTN>();
                    DataTable dt = new DataTable();

                    if (fileExt.ToUpper().Equals("CSV"))
                    {
                        dt = CSVDataTable(Filename);
                    }else{
                        dt = ExcelDataTable(Filename);
                    }
                        

                    if (dt.Rows.Count > 0)
                    {                        
                        List = ConvertDataTableToListTrans2(dt);

                        if (List.Count > 0)
                        {

                                if (validateFile(List))
                                {

                                    #region validate
                                    var result = List.Select(x => 
                                             new
                                                {
                                                    kksymbol = x.KKSYMBOL,
                                                    amount = x.AMOUNT
                                                }                               
                                            )
                                          .GroupBy(s => new { s.kksymbol})
                                          .Select (g => 
                                             new {
                                                 kksymbol = g.Key.kksymbol, 
                                                 Amount = g.Sum(x => Math.Round(Convert.ToDecimal(x.amount), 2)),
                                                 Record = g.Count(),
                                                }
                                            ).ToList();


                                            bool status = true;
                                            string textmsg = string.Empty;

                                            for (int r = 0; r < result.Count; r++ )
                                            {

                                                string kksym = result[r].kksymbol;
                                                decimal sum = result[r].Amount;
                                                int record = result[r].Record;

                                                var response = KKBSTN.reconcile(kksym, sum, record);

                                                Type t = response.GetType();
                                                PropertyInfo res = t.GetProperty("result");
                                                PropertyInfo m = t.GetProperty("Message");

                                                status = bool.Parse(res.GetValue(response, null).ToString());
                                                textmsg = m.GetValue(response, null).ToString();

                                                if (!status)
                                                {

                                                    break;

                                                }
                                                 

                                            }


                                    #endregion


                                        if (status)
                                        {

                                            bool status2 = true;

                                            for (int r = 0; r < result.Count; r++)
                                            {

                                                string kksym = result[r].kksymbol;
                                                decimal sum = result[r].Amount;
                                                int record = result[r].Record;

                                                if (havekkSymbol(kksym))
                                                {

                                                    status2 = removeRecordBykkSymbol(kksym);

                                                    if (!status2)
                                                    {
                                                        textmsg = "'Cannot remove data of Symbol (" + kksym + ")'";
                                                        break;

                                                    }

                                                }

                                            }

                                            if (status2)
                                            {
                                                msg = ImportFile(List);
                                            }
                                            else {

                                                msg += "NOT PASS because " + textmsg;
                                            }
                                        }
                                        else
                                        {

                                            msg += "NOT PASS because " + textmsg;
                                        }

                                }
                                else
                                {

                                    msg += "Please check column SYMBOL,TITLE NAME_TH,FIRST NAME_TH,LAST NAME_TH,UNDERLYING1,MATURITY DATE is require field!!";

                                }

                        }
                        else
                        {

                            msg += "Cannot convert to file template.";

                        }
  
                    }
                    else {

                        msg += "Data in file is empty!!";
                    }



                    if (string.IsNullOrEmpty(msg))
                    { 
                        MessageBox.Text = "ระบบนำเข้าข้อมูลเรียบร้อยแล้ว";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                    else
                    {
                        MessageBox.Text = msg;//"ระบบไม่สามารถนำเข้าข้อมูลได้ กรุณาทำรายการใหม่";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
                    }
                }
                else
                {
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



        protected string ImportFile(List<KKBSTN> List)
        {
             
            string msg = string.Empty;
            bool issuccess = false;
            try
            {
                 
                    if (List.Count > 0)
                    {
                         
                                issuccess = KKBSTN.AddSTN(List);

                                 
                                if (!issuccess){


                                    msg += " Cannot add STN!!";

                                }
                             
                    }
                    else
                    {

                        msg += "Data in file is empty";
                    }

                 
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

            }

            return msg;
        }

        public DataTable ExcelDataTable(string ExcelFile)
        {
            //String ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFile + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
            String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFile + ";" + "Extended Properties=\"Excel 8.0;HDR=Yes;\";";

            OleDbConnection _oleConn = new OleDbConnection();
            DataTable dt = new DataTable();
            try
            {
                _oleConn.ConnectionString = ConnectionString;
                if (_oleConn.State == ConnectionState.Open)
                    _oleConn.Close();
                _oleConn.Open();
                OleDbCommand _oleCmdSelect = new OleDbCommand(@"SELECT * FROM [Sheet1$] ", _oleConn);
                OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                oleAdapter.SelectCommand = _oleCmdSelect; 

                oleAdapter.FillSchema(dt, SchemaType.Source);
                oleAdapter.Fill(dt);
            
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                _oleConn.Close();
            }

            return dt;
        }


        public static DataTable CSVDataTable(string ExcelFile)
        {
            StreamReader srtest = new StreamReader(ExcelFile);

            StreamReader sr = new StreamReader(ExcelFile, System.Text.Encoding.GetEncoding(874));
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        } 



        private List<KKBSTN> ConvertDataTableToListTrans2(DataTable dt)
        {
            
            List<KKBSTN> result = new List<KKBSTN>();
            KKBSTN STN = new KKBSTN();
            try { 

                int colcount = dt.Columns.Count;



                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string action = dt.Rows[i][0].ToString();

                        if(!string.IsNullOrEmpty(action)){

                                    STN = new KKBSTN();

                                    STN.ACTION = string.IsNullOrEmpty(dt.Rows[i][0].ToString()) ? "" : dt.Rows[i][0].ToString();
                                    STN.CURR_DATE = string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? "" : DateTime.Parse(dt.Rows[i][1].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.ORDER_NO = string.IsNullOrEmpty(dt.Rows[i][2].ToString()) ? "" : dt.Rows[i][2].ToString();
                                    STN.ACCT_NO = string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? "" : dt.Rows[i][3].ToString();
                                    STN.SYMBOL = string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? "" : dt.Rows[i][4].ToString();
                                    STN.KKSYMBOL = string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? "" : dt.Rows[i][5].ToString();
                                    STN.UNIT = string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][6].ToString());
                                    STN.AMOUNT = string.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][7].ToString());
                                    STN.CUST_TYPE = string.IsNullOrEmpty(dt.Rows[i][8].ToString()) ? "" : dt.Rows[i][8].ToString();
                                    STN.TITLE_NAME_TH = string.IsNullOrEmpty(dt.Rows[i][9].ToString()) ? "" : dt.Rows[i][9].ToString();
                                    STN.FIRST_NAME_TH = string.IsNullOrEmpty(dt.Rows[i][10].ToString()) ? "" : dt.Rows[i][10].ToString();
                                    STN.LAST_NAME_TH = string.IsNullOrEmpty(dt.Rows[i][11].ToString()) ? "" : dt.Rows[i][11].ToString();
                                    STN.TITLE_NAME_EN = string.IsNullOrEmpty(dt.Rows[i][12].ToString()) ? "" : dt.Rows[i][12].ToString();
                                    STN.FIRST_NAME_EN = string.IsNullOrEmpty(dt.Rows[i][13].ToString()) ? "" : dt.Rows[i][13].ToString();
                                    STN.LAST_NAME_EN = string.IsNullOrEmpty(dt.Rows[i][14].ToString()) ? "" : dt.Rows[i][14].ToString();
                                    STN.TAXID = string.IsNullOrEmpty(dt.Rows[i][15].ToString()) ? "" : dt.Rows[i][15].ToString();
                                    STN.CUST_BANK_CODE = string.IsNullOrEmpty(dt.Rows[i][16].ToString()) ? "" : dt.Rows[i][16].ToString();
                                    STN.CUST_BANK_ACCT = string.IsNullOrEmpty(dt.Rows[i][17].ToString()) ? "" : dt.Rows[i][17].ToString();
                                    // replace semicolon with colon in tel and email 28.07.2021
                                    STN.TEL = string.IsNullOrEmpty(dt.Rows[i][18].ToString()) ? "" : dt.Rows[i][18].ToString().Replace(";",",");
                                    STN.EMAIL = string.IsNullOrEmpty(dt.Rows[i][19].ToString()) ? "" : dt.Rows[i][19].ToString().Replace(";", ",");
                                    STN.FC_NAME = string.IsNullOrEmpty(dt.Rows[i][20].ToString()) ? "" : dt.Rows[i][20].ToString();
                                    STN.FC_EMAIL = string.IsNullOrEmpty(dt.Rows[i][21].ToString()) ? "" : dt.Rows[i][21].ToString().Replace(";", ",");
                                    STN.STN_TYPE = string.IsNullOrEmpty(dt.Rows[i][22].ToString()) ? "" : dt.Rows[i][22].ToString();
                                    STN.NOTE_SERIES = string.IsNullOrEmpty(dt.Rows[i][23].ToString()) ? "" : dt.Rows[i][23].ToString();
                                    STN.UNDERLYING1 = string.IsNullOrEmpty(dt.Rows[i][24].ToString()) ? "" : dt.Rows[i][24].ToString();
                                    STN.UNDERLYING2 = string.IsNullOrEmpty(dt.Rows[i][25].ToString()) ? "" : dt.Rows[i][25].ToString();
                                    STN.UNDERLYING3 = string.IsNullOrEmpty(dt.Rows[i][26].ToString()) ? "" : dt.Rows[i][26].ToString();
                                    STN.UNDERLYING4 = string.IsNullOrEmpty(dt.Rows[i][27].ToString()) ? "" : dt.Rows[i][27].ToString();
                                    STN.UNDERLYING5 = string.IsNullOrEmpty(dt.Rows[i][28].ToString()) ? "" : dt.Rows[i][28].ToString();
                                    STN.UNDERLYING6 = string.IsNullOrEmpty(dt.Rows[i][29].ToString()) ? "" : dt.Rows[i][29].ToString();
                                    STN.STRIKE_PRICE1 = string.IsNullOrEmpty(dt.Rows[i][30].ToString()) ? "0" : dt.Rows[i][30].ToString();
                                    STN.STRIKE_PRICE2 = string.IsNullOrEmpty(dt.Rows[i][31].ToString()) ? "0" : dt.Rows[i][31].ToString();
                                    STN.STRIKE_PRICE3 = string.IsNullOrEmpty(dt.Rows[i][32].ToString()) ? "0" : dt.Rows[i][32].ToString();
                                    STN.STRIKE_PRICE4 = string.IsNullOrEmpty(dt.Rows[i][33].ToString()) ? "0" : dt.Rows[i][33].ToString();
                                    STN.STRIKE_PRICE5 = string.IsNullOrEmpty(dt.Rows[i][34].ToString()) ? "0" : dt.Rows[i][34].ToString();
                                    STN.STRIKE_PRICE6 = string.IsNullOrEmpty(dt.Rows[i][35].ToString()) ? "0" : dt.Rows[i][35].ToString();
                                    STN.BARRIER_PRICE1 = string.IsNullOrEmpty(dt.Rows[i][36].ToString()) ? "0" : dt.Rows[i][36].ToString();
                                    STN.BARRIER_PRICE2 = string.IsNullOrEmpty(dt.Rows[i][37].ToString()) ? "0" : dt.Rows[i][37].ToString();
                                    STN.BARRIER_PRICE3 = string.IsNullOrEmpty(dt.Rows[i][38].ToString()) ? "0" : dt.Rows[i][38].ToString();
                                    STN.BARRIER_PRICE4 = string.IsNullOrEmpty(dt.Rows[i][39].ToString()) ? "0" : dt.Rows[i][39].ToString();
                                    STN.BARRIER_PRICE5 = string.IsNullOrEmpty(dt.Rows[i][40].ToString()) ? "0" : dt.Rows[i][40].ToString();
                                    STN.BARRIER_PRICE6 = string.IsNullOrEmpty(dt.Rows[i][41].ToString()) ? "0" : dt.Rows[i][41].ToString();
                                    //STN.KI_PRICE = 
                                    STN.PROTECT_LEVELP = string.IsNullOrEmpty(dt.Rows[i][42].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][42].ToString().Replace("%",""));
                                    STN.TTM = string.IsNullOrEmpty(dt.Rows[i][43].ToString()) ? 0 : int.Parse(dt.Rows[i][43].ToString());
                                    STN.TRADING_DAY = string.IsNullOrEmpty(dt.Rows[i][44].ToString()) ? 0 : int.Parse(dt.Rows[i][44].ToString());
                                    STN.EXERCISE_TYPE = string.IsNullOrEmpty(dt.Rows[i][45].ToString()) ? "" : dt.Rows[i][45].ToString();
                                    STN.PAR_VALUE = string.IsNullOrEmpty(dt.Rows[i][46].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][46].ToString());
                                    STN.OFFER_PRICEP = string.IsNullOrEmpty(dt.Rows[i][47].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][47].ToString().Replace("%", ""));
                                    STN.OFFER_PRICE_AMT = string.IsNullOrEmpty(dt.Rows[i][48].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][48].ToString());
                                    STN.INITIAL_PRICEP = string.IsNullOrEmpty(dt.Rows[i][49].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][49].ToString().Replace("%", ""));
                                    STN.INITIAL_PRICE_AMT = string.IsNullOrEmpty(dt.Rows[i][50].ToString()) ? "0" : dt.Rows[i][50].ToString();
                                    STN.STRIKE_PRICE1P = string.IsNullOrEmpty(dt.Rows[i][51].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][51].ToString().Replace("%", ""));
                                    STN.STRIKE_PRICE2P = string.IsNullOrEmpty(dt.Rows[i][52].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][52].ToString().Replace("%", ""));
                                    STN.STRIKE_PRICE3P = string.IsNullOrEmpty(dt.Rows[i][53].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][53].ToString().Replace("%", ""));
                                    STN.STRIKE_PRICE4P = string.IsNullOrEmpty(dt.Rows[i][54].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][54].ToString().Replace("%", ""));
                                    STN.STRIKE_PRICE5P = string.IsNullOrEmpty(dt.Rows[i][55].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][55].ToString().Replace("%", ""));
                                    STN.STRIKE_PRICE6P = string.IsNullOrEmpty(dt.Rows[i][56].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][56].ToString().Replace("%", ""));
                                    STN.BARRIER_PRICE1P = string.IsNullOrEmpty(dt.Rows[i][57].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][57].ToString().Replace("%", ""));
                                    STN.BARRIER_PRICE2P = string.IsNullOrEmpty(dt.Rows[i][58].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][58].ToString().Replace("%", ""));
                                    STN.BARRIER_PRICE3P = string.IsNullOrEmpty(dt.Rows[i][59].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][59].ToString().Replace("%", ""));
                                    STN.BARRIER_PRICE4P = string.IsNullOrEmpty(dt.Rows[i][60].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][60].ToString().Replace("%", ""));
                                    STN.BARRIER_PRICE5P = string.IsNullOrEmpty(dt.Rows[i][61].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][61].ToString().Replace("%", ""));
                                    STN.BARRIER_PRICE6P = string.IsNullOrEmpty(dt.Rows[i][62].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][62].ToString().Replace("%", ""));
                                    //STN.KK_PRICEP 
                                    STN.PR1 = string.IsNullOrEmpty(dt.Rows[i][63].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][63].ToString().Replace("%", ""));
                                    STN.PR2 = string.IsNullOrEmpty(dt.Rows[i][64].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][64].ToString().Replace("%", ""));
                                    STN.MIN_YIELD1P = string.IsNullOrEmpty(dt.Rows[i][65].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][65].ToString().Replace("%", ""));
                                    STN.MIN_YIELD2P = string.IsNullOrEmpty(dt.Rows[i][66].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][66].ToString().Replace("%", ""));
                                    STN.MAX_YIELD1P = string.IsNullOrEmpty(dt.Rows[i][67].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][67].ToString().Replace("%", ""));
                                    STN.MAX_YIELD2P = string.IsNullOrEmpty(dt.Rows[i][68].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][68].ToString().Replace("%", ""));
                                    STN.REBATE_AMT = string.IsNullOrEmpty(dt.Rows[i][69].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][69].ToString());
                                    STN.REBATEP = string.IsNullOrEmpty(dt.Rows[i][70].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][70].ToString().Replace("%", ""));
                                    STN.NOTE_CURR = string.IsNullOrEmpty(dt.Rows[i][71].ToString()) ? "" : dt.Rows[i][71].ToString();
                                    STN.UNDERLYING_CURR = string.IsNullOrEmpty(dt.Rows[i][72].ToString()) ? "" : dt.Rows[i][72].ToString();
                                    STN.FX_DELIVERY_AMT = string.IsNullOrEmpty(dt.Rows[i][73].ToString()) ? "0" : dt.Rows[i][73].ToString();
                                    STN.SURPLUS_CASH = string.IsNullOrEmpty(dt.Rows[i][74].ToString()) ? "0" : dt.Rows[i][74].ToString();
                                    STN.SUB_BEGIN_DATE = string.IsNullOrEmpty(dt.Rows[i][75].ToString()) ? "" : DateTime.Parse(dt.Rows[i][75].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.SUB_END_DATE = string.IsNullOrEmpty(dt.Rows[i][76].ToString()) ? "" : DateTime.Parse(dt.Rows[i][76].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.TRADE_DATE = string.IsNullOrEmpty(dt.Rows[i][77].ToString()) ? "" : DateTime.Parse(dt.Rows[i][77].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.EFFECTIVE_DATE = string.IsNullOrEmpty(dt.Rows[i][78].ToString()) ? "" : DateTime.Parse(dt.Rows[i][78].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.SETTLE_DATE = string.IsNullOrEmpty(dt.Rows[i][79].ToString()) ? "" : DateTime.Parse(dt.Rows[i][79].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.ISSUE_DATE = string.IsNullOrEmpty(dt.Rows[i][80].ToString()) ? "" : DateTime.Parse(dt.Rows[i][80].ToString()).ToString("dd/MM/yyyy");
                                    STN.FIXING_DATE = string.IsNullOrEmpty(dt.Rows[i][81].ToString()) ? "" : DateTime.Parse(dt.Rows[i][81].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.MATURITY_DATE = string.IsNullOrEmpty(dt.Rows[i][82].ToString()) ? "" : DateTime.Parse(dt.Rows[i][82].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE1 = string.IsNullOrEmpty(dt.Rows[i][83].ToString()) ? "" : DateTime.Parse(dt.Rows[i][83].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE2 = string.IsNullOrEmpty(dt.Rows[i][84].ToString()) ? "" : DateTime.Parse(dt.Rows[i][84].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE3 = string.IsNullOrEmpty(dt.Rows[i][85].ToString()) ? "" : DateTime.Parse(dt.Rows[i][85].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE4 = string.IsNullOrEmpty(dt.Rows[i][86].ToString()) ? "" : DateTime.Parse(dt.Rows[i][86].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE5 = string.IsNullOrEmpty(dt.Rows[i][87].ToString()) ? "" : DateTime.Parse(dt.Rows[i][87].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE6 = string.IsNullOrEmpty(dt.Rows[i][88].ToString()) ? "" : DateTime.Parse(dt.Rows[i][88].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE7 = string.IsNullOrEmpty(dt.Rows[i][89].ToString()) ? "" : DateTime.Parse(dt.Rows[i][89].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE8 = string.IsNullOrEmpty(dt.Rows[i][90].ToString()) ? "" : DateTime.Parse(dt.Rows[i][90].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE9 = string.IsNullOrEmpty(dt.Rows[i][91].ToString()) ? "" : DateTime.Parse(dt.Rows[i][91].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE10 = string.IsNullOrEmpty(dt.Rows[i][92].ToString()) ? "" : DateTime.Parse(dt.Rows[i][92].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE11 = string.IsNullOrEmpty(dt.Rows[i][93].ToString()) ? "" : DateTime.Parse(dt.Rows[i][93].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE12 = string.IsNullOrEmpty(dt.Rows[i][94].ToString()) ? "" : DateTime.Parse(dt.Rows[i][94].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE13 = string.IsNullOrEmpty(dt.Rows[i][95].ToString()) ? "" : DateTime.Parse(dt.Rows[i][95].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE14 = string.IsNullOrEmpty(dt.Rows[i][96].ToString()) ? "" : DateTime.Parse(dt.Rows[i][96].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE15 = string.IsNullOrEmpty(dt.Rows[i][97].ToString()) ? "" : DateTime.Parse(dt.Rows[i][97].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE16 = string.IsNullOrEmpty(dt.Rows[i][98].ToString()) ? "" : DateTime.Parse(dt.Rows[i][98].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE17 = string.IsNullOrEmpty(dt.Rows[i][99].ToString()) ? "" : DateTime.Parse(dt.Rows[i][99].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE18 = string.IsNullOrEmpty(dt.Rows[i][100].ToString()) ? "" : DateTime.Parse(dt.Rows[i][100].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE19 = string.IsNullOrEmpty(dt.Rows[i][101].ToString()) ? "" : DateTime.Parse(dt.Rows[i][101].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.OBS_DATE20 = string.IsNullOrEmpty(dt.Rows[i][102].ToString()) ? "" : DateTime.Parse(dt.Rows[i][102].ToString(), new CultureInfo("en-CA")).ToString("dd/MM/yyyy");
                                    STN.SETTLE_PERIOD = string.IsNullOrEmpty(dt.Rows[i][103].ToString()) ? 0 : int.Parse(dt.Rows[i][103].ToString());
                                    STN.PAY_TYPE = string.IsNullOrEmpty(dt.Rows[i][104].ToString()) ? "" : dt.Rows[i][104].ToString();
                                    STN.EXCRATE_TRADEDATE = string.IsNullOrEmpty(dt.Rows[i][105].ToString()) ? 0 : Decimal.Parse(dt.Rows[i][105].ToString());
                                    STN.EXCHANGE1 = string.IsNullOrEmpty(dt.Rows[i][106].ToString()) ? "" : dt.Rows[i][106].ToString();
                                    STN.EXCHANGE2 = string.IsNullOrEmpty(dt.Rows[i][107].ToString()) ? "" : dt.Rows[i][107].ToString();
                                    STN.EXCHANGE3 = string.IsNullOrEmpty(dt.Rows[i][108].ToString()) ? "" : dt.Rows[i][108].ToString();                                
                                    
                                    result.Add(STN);
                        }
                    }
            }
            catch (Exception ex) {
                Log.Error(ex.Message);
            }

            return result;

        }

        private bool validateFile(List<KKBSTN> List)
        {

            bool ispass = true;

            try
            {

                foreach(KKBSTN d in List)
                {
                     
                    if (string.IsNullOrEmpty(d.SYMBOL) ||  string.IsNullOrEmpty(d.UNDERLYING1) || string.IsNullOrEmpty(d.MATURITY_DATE) )
                    {
                        ispass = false;
                    }

                }
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }

            return ispass;

        }


        private bool havekkSymbol(string kksymbol){
        
             bool ishave = false;

            try
            {

                ishave = KKBSTN.havekkSymbol(kksymbol);

            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }

            return ishave;

        
        }

        

        private bool removeRecordBykkSymbol(string kksymbol)
        {

            bool issuccess = false;

            try
            {

                issuccess = KKBSTN.removeRecordBykkSymbol(kksymbol);

            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
            }

            return issuccess;


        }

    }
}
