using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

namespace TreasuryShadowSystem.ALR
{
    public partial class UploadTranALR : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(UploadTranALR));

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                GridALR.RowDataBound += new GridViewRowEventHandler(GridALR_RowDataBound);
                GridALR.PageIndexChanging += new GridViewPageEventHandler(GridALR_PageIndexChanging);
                GridALR.RowDeleting += new GridViewDeleteEventHandler(GridALR_RowDeleting);
                //GridALR.RowEditing += new GridViewEditEventHandler(GridALR_RowEditing);
                //GridALR.RowCancelingEdit += new GridViewCancelEditEventHandler(GridALR_RowCancelingEdit);
                //GridALR.RowUpdating += new GridViewUpdateEventHandler(GridALR_RowUpdating);
                
            }
            catch (Exception ex)
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            try
            {
                if (!IsPostBack) { }
                    //DateBox.Text = DateTime.Now.ToString("dd/MM/yy");
                    //Session["TransToALR"] = "";
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
                    string Filename = Server.MapPath("~/Temporary/") + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.PostedFile.FileName;
                    Log.Info("Import Filename : " + Filename);
                    FileUpload1.PostedFile.SaveAs(Filename);

                    string msg = ImportFile(Filename);

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
                
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try { 

                string dealno = txtDealNo.Text;
                string settlementdate = DateBox.Text;

                //string.IsNullOrEmpty(settlementdate) ? "" : DateTime.Parse(settlementdate.ToString()).ToString("dd/MM/yyyy");

                DataTable result = new DataTable();
                result = TransToALR.Search(dealno, settlementdate);

                List<TransToALR> list = ConvertDataTableToListTrans(result);

                //GridALR.DataSource = result;
                //GridALR.DataBind();

                Session["TransToALR"] = list;
                BindGrid();            
            
            }catch( Exception ex){

                Log.Error(ex.Message);
                MessageBox.Text = "เกิดข้อผิดพลาด " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true});", true);
            }
        }


        void GridALR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridALR.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {

            }
        }

        void GridALR_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            GridView gv = ((GridView)sender);
            GridViewRow gvr = ((GridViewRow)e.Row);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label updatedate = (Label)e.Row.Cells[17].Controls[0];

                string d = DateTime.Parse(updatedate.Text).ToString("dd/MM/yy");

                string now = DateTime.Now.ToString("dd/MM/yy");

                LinkButton lbedit = (LinkButton)e.Row.FindControl("LinkBtnEdit");
                LinkButton lbdelete = (LinkButton)e.Row.FindControl("LinkBtnDelete");


                if (d == now)
                {

                    lbedit.Visible = true;
                    lbdelete.Visible = true;

                }
                else
                {

                    lbedit.Visible = false;
                    lbdelete.Visible = false;
                }





            }

            // LinkButton editbutton = ((LinkButton)gvr.FindControl());

        }

        void GridALR_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                GridALR.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {

            }
        }

        void GridALR_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                GridALR.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {

            }
        }

        void GridALR_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string key = GridALR.DataKeys[e.RowIndex].Value.ToString();

                if (TransToALR.DeleteALR(key))
                {
                    GridALR.EditIndex = -1;
                    BindGrid();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        void GridALR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {

            }
        }

        void GridALR_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string key = GridALR.DataKeys[e.RowIndex].Value.ToString();

                TextBox TEAM = (TextBox)GridALR.Rows[e.RowIndex].Cells[1].Controls[1];
                TextBox DEALNO = (TextBox)GridALR.Rows[e.RowIndex].Cells[2].Controls[1];
                TextBox ID_NO_PART1 = (TextBox)GridALR.Rows[e.RowIndex].Cells[3].Controls[1];
                TextBox ID_NO_PART2 = (TextBox)GridALR.Rows[e.RowIndex].Cells[4].Controls[1];
                TextBox SENDER_CUST_TYPE = (TextBox)GridALR.Rows[e.RowIndex].Cells[5].Controls[1];
                TextBox SENDING_BANK_CODE = (TextBox)GridALR.Rows[e.RowIndex].Cells[6].Controls[1];
                TextBox SENDING_TYPE = (TextBox)GridALR.Rows[e.RowIndex].Cells[7].Controls[1];
                TextBox SENDING_AC_NO = (TextBox)GridALR.Rows[e.RowIndex].Cells[8].Controls[1];
                TextBox SENDING_INFO = (TextBox)GridALR.Rows[e.RowIndex].Cells[9].Controls[1];
                TextBox RECEIVER_CUST_TYPE = (TextBox)GridALR.Rows[e.RowIndex].Cells[10].Controls[1];
                TextBox RECEIVING_BANK_CODE = (TextBox)GridALR.Rows[e.RowIndex].Cells[11].Controls[1];
                TextBox RECEIVING_TYPE = (TextBox)GridALR.Rows[e.RowIndex].Cells[12].Controls[1];
                TextBox RECEIVING_AC_NO = (TextBox)GridALR.Rows[e.RowIndex].Cells[13].Controls[1];
                TextBox RECEIVING_INFO = (TextBox)GridALR.Rows[e.RowIndex].Cells[14].Controls[1];
                TextBox TRANSFER_AMOUNT = (TextBox)GridALR.Rows[e.RowIndex].Cells[15].Controls[1];
                TextBox SETTLE_DATE = (TextBox)GridALR.Rows[e.RowIndex].Cells[16].Controls[1];



                TransToALR data = new TransToALR();
                data.TRANSID = int.Parse(key);
                data.TEAM = TEAM.Text;
                data.DEALNO = DEALNO.Text;
                data.ID_NO_PART1 = ID_NO_PART1.Text;
                data.ID_NO_PART2 = ID_NO_PART2.Text;
                data.SENDER_CUST_TYPE = SENDER_CUST_TYPE.Text;
                data.SENDING_BANK_CODE = SENDING_BANK_CODE.Text;
                data.SENDING_TYPE = SENDING_TYPE.Text;
                data.SENDING_AC_NO = SENDING_AC_NO.Text;
                data.SENDING_INFO = SENDING_INFO.Text;
                data.RECEIVER_CUST_TYPE = RECEIVER_CUST_TYPE.Text;
                data.RECEIVING_BANK_CODE = RECEIVING_BANK_CODE.Text;
                data.RECEIVING_TYPE = RECEIVING_TYPE.Text;
                data.RECEIVING_AC_NO = RECEIVING_AC_NO.Text;
                data.RECEIVING_INFO = RECEIVING_INFO.Text;
                data.TRANSFER_AMOUNT = int.Parse(TRANSFER_AMOUNT.Text);
                data.SETTLE_DATE = string.IsNullOrEmpty(SETTLE_DATE.Text) ? null : SETTLE_DATE.Text;




                //TextBox Amount = (TextBox)GridALR.Rows[e.RowIndex].Cells[2].Controls[1];


                if (TransToALR.UpdateALR(data))
                {
                    GridALR.EditIndex = -1;
                    BindGrid();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

            }
        }


        private void BindGrid()
        {
            try
            {

                List<TransToALR> ALRList = (List<TransToALR>)Session["TransToALR"];

                //var transidlist = ALRList.Select(x => x.TRANSID.ToString()).ToArray();
                //string listtransid = string.Join(",", transidlist);

                //DataTable dt = TransToALR.getTransToALRByID(listtransid);
                //ALRList = ConvertDataTableToListTrans(dt);

                GridALR.DataSource = ALRList;                
                GridALR.DataBind();

                //GridALR.DataSource = Session["TransToALR"];
            }
            catch (Exception ex)
            {

            }
        }

        private List<TransToALR> ConvertDataTableToListTrans(DataTable dt) {

            List<TransToALR> result = new List<TransToALR>();
            TransToALR ALR = new TransToALR();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ALR = new TransToALR();
                ALR.TRANSID = string.IsNullOrEmpty(dt.Rows[i]["TRANSID"].ToString()) ? 0 : int.Parse(dt.Rows[i]["TRANSID"].ToString());
                ALR.TEAM = string.IsNullOrEmpty(dt.Rows[i]["TEAM"].ToString()) ? "" : dt.Rows[i]["TEAM"].ToString();
                ALR.DEALNO = string.IsNullOrEmpty(dt.Rows[i]["DEALNO"].ToString()) ? "" : dt.Rows[i]["DEALNO"].ToString();
                ALR.ID_NO_PART1 = string.IsNullOrEmpty(dt.Rows[i]["ID_NO_PART1"].ToString()) ? "" : dt.Rows[i]["ID_NO_PART1"].ToString();
                ALR.ID_NO_PART2 = string.IsNullOrEmpty(dt.Rows[i]["ID_NO_PART2"].ToString()) ? "" : dt.Rows[i]["ID_NO_PART2"].ToString();
                ALR.SENDER_CUST_TYPE = string.IsNullOrEmpty(dt.Rows[i]["SENDER_CUST_TYPE"].ToString()) ? "" : dt.Rows[i]["SENDER_CUST_TYPE"].ToString();
                ALR.SENDING_BANK_CODE = string.IsNullOrEmpty(dt.Rows[i]["SENDING_BANK_CODE"].ToString()) ? "" : dt.Rows[i]["SENDING_BANK_CODE"].ToString();
                ALR.SENDING_TYPE = string.IsNullOrEmpty(dt.Rows[i]["SENDING_TYPE"].ToString()) ? "" : dt.Rows[i]["SENDING_TYPE"].ToString();
                ALR.SENDING_AC_NO = string.IsNullOrEmpty(dt.Rows[i]["SENDING_AC_NO"].ToString()) ? "" : dt.Rows[i]["SENDING_AC_NO"].ToString();
                ALR.SENDING_INFO = string.IsNullOrEmpty(dt.Rows[i]["SENDING_INFO"].ToString()) ? "" : dt.Rows[i]["SENDING_INFO"].ToString();
                ALR.RECEIVER_CUST_TYPE = string.IsNullOrEmpty(dt.Rows[i]["RECEIVER_CUST_TYPE"].ToString()) ? "" : dt.Rows[i]["RECEIVER_CUST_TYPE"].ToString();
                ALR.RECEIVING_BANK_CODE = string.IsNullOrEmpty(dt.Rows[i]["RECEIVING_BANK_CODE"].ToString()) ? "" : dt.Rows[i]["RECEIVING_BANK_CODE"].ToString();
                ALR.RECEIVING_TYPE = string.IsNullOrEmpty(dt.Rows[i]["RECEIVING_TYPE"].ToString()) ? "" : dt.Rows[i]["RECEIVING_TYPE"].ToString();
                ALR.RECEIVING_AC_NO = string.IsNullOrEmpty(dt.Rows[i]["RECEIVING_AC_NO"].ToString()) ? "" : dt.Rows[i]["RECEIVING_AC_NO"].ToString();
                ALR.RECEIVING_INFO = string.IsNullOrEmpty(dt.Rows[i]["RECEIVING_INFO"].ToString()) ? "" : dt.Rows[i]["RECEIVING_INFO"].ToString();
                ALR.TRANSFER_AMOUNT = string.IsNullOrEmpty(dt.Rows[i]["TRANSFER_AMOUNT"].ToString()) ? 0 : Decimal.Parse(dt.Rows[i]["TRANSFER_AMOUNT"].ToString());
                ALR.SETTLE_DATE = string.IsNullOrEmpty(dt.Rows[i]["SETTLE_DATE"].ToString()) ? "" : DateTime.Parse(dt.Rows[i]["SETTLE_DATE"].ToString()).ToString("dd/MM/yyyy");
                ALR.UPLOAD_DATE = string.IsNullOrEmpty(dt.Rows[i]["UPLOAD_DATE"].ToString()) ? "" : DateTime.Parse(dt.Rows[i]["UPLOAD_DATE"].ToString()).ToString("dd/MM/yyyy");//dt.Rows[i]["UPLOAD_DATE"].ToString();

                result.Add(ALR);

            }
            //foreach (DataRow r in dt.Rows)
            //{
            //    trans = new TransToALR();
                
 


            //    trans.TRANSID = string.IsNullOrEmpty(r.ItemArray[0].ToString()) ? 0 : int.Parse(r.ItemArray[0].ToString());
            //    trans.TEAM = r.ItemArray[1].ToString();
            //    trans.DEALNO = r.ItemArray[2].ToString();
            //    trans.ID_NO_PART1 = r.ItemArray[3].ToString();
            //    trans.ID_NO_PART2 = r.ItemArray[4].ToString();
            //    trans.SENDER_CUST_TYPE = r.ItemArray[5].ToString();
            //    trans.SENDING_BANK_CODE = r.ItemArray[6].ToString();
            //    trans.SENDING_TYPE = r.ItemArray[7].ToString();
            //    trans.SENDING_AC_NO = r.ItemArray[8].ToString();
            //    trans.SENDING_INFO = r.ItemArray[9].ToString();
            //    trans.RECEIVER_CUST_TYPE = r.ItemArray[10].ToString();
            //    trans.RECEIVING_BANK_CODE = r.ItemArray[11].ToString();
            //    trans.RECEIVING_TYPE = r.ItemArray[12].ToString();
            //    trans.RECEIVING_AC_NO = r.ItemArray[13].ToString();
            //    trans.RECEIVING_INFO = r.ItemArray[14].ToString();
            //    trans.TRANSFER_AMOUNT = string.IsNullOrEmpty(r.ItemArray[15].ToString()) ? 0 : Decimal.Parse(r.ItemArray[15].ToString());
            //    trans.SETTLE_DATE = r.ItemArray[16].ToString();
            //    trans.UPLOAD_DATE = r.ItemArray[17].ToString();



            //    result.Add(trans);

            //}

            return result;

        }

        private List<TransToALR> ConvertDataTableToListTrans2(DataTable dt)
        {

            List<TransToALR> result = new List<TransToALR>();
            TransToALR ALR = new TransToALR();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ALR = new TransToALR();
                //ALR.TRANSID = string.IsNullOrEmpty(dt.Rows[i]["TRANSID"].ToString()) ? 0 : int.Parse(dt.Rows[i]["TRANSID"].ToString());
                ALR.TEAM = string.IsNullOrEmpty(dt.Rows[i]["TEAM"].ToString()) ? "" : dt.Rows[i]["TEAM"].ToString();
                ALR.DEALNO = string.IsNullOrEmpty(dt.Rows[i]["DEALNO"].ToString()) ? "" : dt.Rows[i]["DEALNO"].ToString();
                ALR.ID_NO_PART1 = string.IsNullOrEmpty(dt.Rows[i]["ID_NO_PART1"].ToString()) ? "" : dt.Rows[i]["ID_NO_PART1"].ToString();
                ALR.ID_NO_PART2 = string.IsNullOrEmpty(dt.Rows[i]["ID_NO_PART2"].ToString()) ? "" : dt.Rows[i]["ID_NO_PART2"].ToString();
                ALR.SENDER_CUST_TYPE = string.IsNullOrEmpty(dt.Rows[i]["SENDER_CUST_TYPE"].ToString()) ? "" : dt.Rows[i]["SENDER_CUST_TYPE"].ToString();
                ALR.SENDING_BANK_CODE = string.IsNullOrEmpty(dt.Rows[i]["SENDING_BANK_CODE"].ToString()) ? "" : dt.Rows[i]["SENDING_BANK_CODE"].ToString();
                ALR.SENDING_TYPE = string.IsNullOrEmpty(dt.Rows[i]["SENDING_TYPE"].ToString()) ? "" : dt.Rows[i]["SENDING_TYPE"].ToString();
                ALR.SENDING_AC_NO = string.IsNullOrEmpty(dt.Rows[i]["SENDING_AC_NO"].ToString()) ? "" : dt.Rows[i]["SENDING_AC_NO"].ToString();
                ALR.SENDING_INFO = string.IsNullOrEmpty(dt.Rows[i]["SENDING_INFO"].ToString()) ? "" : dt.Rows[i]["SENDING_INFO"].ToString();
                ALR.RECEIVER_CUST_TYPE = string.IsNullOrEmpty(dt.Rows[i]["RECEIVER_CUST_TYPE"].ToString()) ? "" : dt.Rows[i]["RECEIVER_CUST_TYPE"].ToString();
                ALR.RECEIVING_BANK_CODE = string.IsNullOrEmpty(dt.Rows[i]["RECEIVING_BANK_CODE"].ToString()) ? "" : dt.Rows[i]["RECEIVING_BANK_CODE"].ToString();
                ALR.RECEIVING_TYPE = string.IsNullOrEmpty(dt.Rows[i]["RECEIVING_TYPE"].ToString()) ? "" : dt.Rows[i]["RECEIVING_TYPE"].ToString();
                ALR.RECEIVING_AC_NO = string.IsNullOrEmpty(dt.Rows[i]["RECEIVING_AC_NO"].ToString()) ? "" : dt.Rows[i]["RECEIVING_AC_NO"].ToString();
                ALR.RECEIVING_INFO = string.IsNullOrEmpty(dt.Rows[i]["RECEIVING_INFO"].ToString()) ? "" : dt.Rows[i]["RECEIVING_INFO"].ToString();
                ALR.TRANSFER_AMOUNT = string.IsNullOrEmpty(dt.Rows[i]["TRANSFER_AMOUNT"].ToString()) ? 0 : Decimal.Parse(dt.Rows[i]["TRANSFER_AMOUNT"].ToString());
                //ALR.SETTLE_DATE = string.IsNullOrEmpty(dt.Rows[i]["SETTLE_DATE"].ToString()) ? "" : DateTime.Parse(dt.Rows[i]["SETTLE_DATE"].ToString()).ToString("MM/dd/yyyy");
                ALR.SETTLE_DATE = string.IsNullOrEmpty(dt.Rows[i]["SETTLE_DATE"].ToString()) ? "" : dt.Rows[i]["SETTLE_DATE"].ToString();                                                                               
                //ALR.UPLOAD_DATE = string.IsNullOrEmpty(dt.Rows[i]["UPLOAD_DATE"].ToString()) ? "" : dt.Rows[i]["UPLOAD_DATE"].ToString();

                result.Add(ALR);

            }
            //foreach (DataRow r in dt.Rows)
            //{
            //    trans = new TransToALR();




            //    trans.TRANSID = string.IsNullOrEmpty(r.ItemArray[0].ToString()) ? 0 : int.Parse(r.ItemArray[0].ToString());
            //    trans.TEAM = r.ItemArray[1].ToString();
            //    trans.DEALNO = r.ItemArray[2].ToString();
            //    trans.ID_NO_PART1 = r.ItemArray[3].ToString();
            //    trans.ID_NO_PART2 = r.ItemArray[4].ToString();
            //    trans.SENDER_CUST_TYPE = r.ItemArray[5].ToString();
            //    trans.SENDING_BANK_CODE = r.ItemArray[6].ToString();
            //    trans.SENDING_TYPE = r.ItemArray[7].ToString();
            //    trans.SENDING_AC_NO = r.ItemArray[8].ToString();
            //    trans.SENDING_INFO = r.ItemArray[9].ToString();
            //    trans.RECEIVER_CUST_TYPE = r.ItemArray[10].ToString();
            //    trans.RECEIVING_BANK_CODE = r.ItemArray[11].ToString();
            //    trans.RECEIVING_TYPE = r.ItemArray[12].ToString();
            //    trans.RECEIVING_AC_NO = r.ItemArray[13].ToString();
            //    trans.RECEIVING_INFO = r.ItemArray[14].ToString();
            //    trans.TRANSFER_AMOUNT = string.IsNullOrEmpty(r.ItemArray[15].ToString()) ? 0 : Decimal.Parse(r.ItemArray[15].ToString());
            //    trans.SETTLE_DATE = r.ItemArray[16].ToString();
            //    trans.UPLOAD_DATE = r.ItemArray[17].ToString();



            //    result.Add(trans);

            //}

            return result;

        }
        
       
        protected string ImportFile(string Filename)
        {
           // OleDbConnection _oleConn = new OleDbConnection();
            string msg = string.Empty;
            bool issuccess = false;
            try
            {
                DataTable dt = new DataTable();
                dt = ExcelDataTable(Filename);

                if (dt.Rows.Count > 0)
                {

                    List<TransToALR> List = new List<TransToALR>();
                    List = ConvertDataTableToListTrans2(dt);
                    if (List.Count > 0)
                    {
                        if (validateFile(List))
                        {
                             
                            issuccess = TransToALR.AddALR(List);

                            if (issuccess)
                            {

                                Session["TransToALR"] = List;
                                BindGrid();
                            }
                            else {

                                msg += " Cannot add transaction to ALR!!";
                            
                            }

                            

                        }
                        else
                        {

                            msg += "Please check column TEAM,ID_NO_PART1, \t SENDING_BANK_CODE,SENDING_AC_NO, \t RECEIVING_BANK_CODE,RECEIVING_AC_NO, \t TRANSFER_AMOUNT,SETTLE_DATE is require field!!";

                        }
                    }
                    else {

                        msg += "Cannot convert file to ALR template.";
                    }

                }
                else {

                    msg += "Data in file is empty!!";
                
                }

                
            }
            catch (Exception ex)
            {
                msg += ex;
                Log.Error(ex.Message);
                 
            }

            return msg;
        }

        private bool validateFile(List<TransToALR> List)
        {

            bool ispass = true;

            try
            {

                foreach (TransToALR d in List)
                {

                    if (string.IsNullOrEmpty(d.TEAM) || string.IsNullOrEmpty(d.ID_NO_PART1) || string.IsNullOrEmpty(d.SENDING_BANK_CODE)
                        || string.IsNullOrEmpty(d.SENDING_AC_NO) || string.IsNullOrEmpty(d.RECEIVING_BANK_CODE)
                        || string.IsNullOrEmpty(d.RECEIVING_AC_NO) || d.TRANSFER_AMOUNT == 0 || string.IsNullOrEmpty(d.SETTLE_DATE))
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

        public DataTable ExcelDataTable(string ExcelFile)
        { 
            String ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFile + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";

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

                //DataColumn dc;
                //dc = new DataColumn("ID", typeof(string));
                //dt.Columns.Add(dc);
                //dc = new DataColumn("TRANSACTION", typeof(string)); 
                //dt.Columns.Add(dc);

                oleAdapter.FillSchema(dt, SchemaType.Source);
                oleAdapter.Fill(dt);
                //dt.Rows[0]["TRANSACTIONID"]
  

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

       
    }
}
