using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using TreasuryModel.Data;
using log4net;
using System.IO;
using System.Web.Services;
using System.Xml.Serialization;
using TreasuryModel.Enhance;
using TreasuryShadowSystem.Configuration;
using System.Net.Mail;

namespace TreasuryShadowSystem.Sanction
{
    public partial class frm_MessageMoneyTransfer : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_MessageMoneyTransfer));

        protected void Page_Init(object sender, EventArgs e)
        {
            GridSanction.RowDataBound += new GridViewRowEventHandler(GridSanction_RowDataBound);
        }


        void GridSanction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[2].Text.Trim().Equals("CWL-I-1000"))
                        e.Row.Cells[3].Text = "In KK Sanction List. Please Review!!";

                    PARTYMsgChk rowdata = e.Row.DataItem as PARTYMsgChk;
                    List<PARTYMsgChk> msg = PARTYMsgChk.getSanctionDetailByMessageID(Configuration.Config.ConnectionString, rowdata.CHECKINGID.ToString());

                    GridView gd = e.Row.FindControl("GridSanctionDetail") as GridView;
                    gd.RowDataBound += new GridViewRowEventHandler(gd_RowDataBound);
                    BindDetail(gd, msg);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

        }

        void gd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TreasuryModel.SanctionService.RecordType searchrecord = (TreasuryModel.SanctionService.RecordType)e.Row.DataItem;
                    e.Row.Cells[2].Text = String.Join(", ", searchrecord.sanctionGroup);
                    e.Row.Cells[3].Text = String.Join(", ", searchrecord.types);
                    e.Row.Cells[4].Text = String.Join(", ", searchrecord.keyword);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void BindDetail(GridView gd, List<PARTYMsgChk> msg)
        {
            try
            {
                List<TreasuryModel.SanctionService.RecordType> record = new List<TreasuryModel.SanctionService.RecordType>();
                for (int i = 0; i < msg.Count; i++)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TreasuryModel.SanctionService.RecordType));
                    string[] rd = msg[i].DETAIL.Split(new string[] { "<Record />" }, StringSplitOptions.None);
                    for (int j = 0; j < rd.Length; j++)
                    {
                        if (!rd[j].Trim().Equals(""))
                        {
                            StringReader rdr = new StringReader(rd[j]);
                            TreasuryModel.SanctionService.RecordType searchrecord = (TreasuryModel.SanctionService.RecordType)serializer.Deserialize(rdr);
                            record.Add(searchrecord);
                        }
                    }
                }

                gd.DataSource = record;
                gd.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetMessageByID();
                BindGrid(); 
            }
        }
        private void BindGrid()
        {
            try
            {
                GridSanction.DataSource = PARTYMsgChk.getCheckSanctionByMessageID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                GridSanction.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        private void GetMessageByID()
        {
            try
            {
                PartyMsg msgobj = PartyMsg.getMessageByID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                string swiftMessage = msgobj.MSG;

                //View Or Edit
                if ((!msgobj.STATUS.Equals(MessageStatus.WAITING_TO_REVIEW.ToString())) && (!msgobj.STATUS.Equals(MessageStatus.FAIL_TO_SEND)))
                {
                    RejectButton.Visible = false;
                    //RecheckButton.Visible = false;
                    AcceptButton.Visible = false;
                    RemarkBox.ReadOnly = false;
                }
                else
                {

                    RejectButton.Visible = true;
                    //RecheckButton.Visible = true;
                    AcceptButton.Visible = true;
                    RemarkBox.ReadOnly = true;
                }
                
                OrderNoBox.Text = msgobj.ORDER_NO;
                OrderDateBox.Text = msgobj.ORDER_DATE;
                CorrespondentBranchBox.Text = msgobj.CORRESPONDENT_BRANCH;
                CustomerNameBox.Text = msgobj.CUST_FIRSTNAME + msgobj.CUST_LASTNAME1 + msgobj.CUST_LASTNAME2;
                CustomerCountryBox.Text = msgobj.CUST_COUNTRY;
                BeneficiaryNameBox.Text = msgobj.BENEF_FIRSTNAME + msgobj.BENEF_LASTNAME1 + msgobj.BENEF_LASTNAME2;
                MessageBeneficiaryBox.Text = msgobj.MESSAGE_FOR_BENEF;
                AccountNumberBox.Text = msgobj.BANK_ACCOUNT_NO;
                AmountBox.Text = msgobj.AMT_PAY;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }        
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PartyMsg.AcceptTransaction(Configuration.Config.ConnectionString, Request.QueryString["Msg"], RemarkBox.Text.Trim()))
                {
                    PartyMsg txt = PartyMsg.getMessageByID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                    string Msg = "The message Order ID : " + txt.ORDER_ID + " is APPROVE.";
                    SendMail.Send(
                        Config.GetParameterFile("147MAILRESULTFROM")
                        , Config.GetParameterFile("147MAILRESULTTO")
                        , Config.GetParameterFile("147MAILRESULTCC")
                        , Config.GetParameterFile("147MAILRESULTSUBJECT")
                        , Msg, new List<Attachment>(), Config.GetParameterFile("089SMTP")
                        , int.Parse(Config.GetParameterFile("089SMTPPort")));
                    MessageBox.Text = "The message Order ID : " + txt.ORDER_ID + " is approve.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: { close : function(){ $(this).dialog('close'); window.location='frm_MoneyTransferList.aspx' }}});", true);
                }
                else
                {
                    MessageBox.Text = "Change status Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: { close : function(){ $(this).dialog('close');}}});", true);
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void RejectButton_Click(object sender, EventArgs e)
        {
            try
            {

                if (PartyMsg.RejectTransaction(Configuration.Config.ConnectionString, Request.QueryString["Msg"], RemarkBox.Text.Trim()))
                {
                    PartyMsg txt = PartyMsg.getMessageByID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                    string Msg = "The message Order ID : " + txt.ORDER_ID + " is REJECT!!.";
                    SendMail.Send(
                        Config.GetParameterFile("147MAILRESULTFROM")
                        , Config.GetParameterFile("147MAILRESULTTO")
                        , Config.GetParameterFile("147MAILRESULTCC")
                        , Config.GetParameterFile("147MAILRESULTSUBJECT")
                        , Msg, new List<Attachment>(), Config.GetParameterFile("089SMTP")
                        , int.Parse(Config.GetParameterFile("089SMTPPort")));
                    MessageBox.Text = "The message is reject.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: { close : function(){ $(this).dialog('close'); window.location='frm_MoneyTransferList.aspx' }}});", true);
                }
                else
                {
                    MessageBox.Text = "Change status Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: {close: function() { $(this).dialog('close'); }});", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        //protected void RecheckButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (PartyMsg.RecheckTransaction(Configuration.Config.ConnectionString, Request.QueryString["Msg"], RemarkBox.Text.Trim()))
        //        {
        //            MessageBox.Text = "The message will recheck.";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: { close : function(){ $(this).dialog('close'); window.location='frm_MoneyTransferList.aspx' }}});", true);
        //        }
        //        else
        //        {
        //            MessageBox.Text = "Change status Failed";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: { close: function() { $(this).dialog('close'); }}});", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        

        [WebMethod(EnableSession = true)]
        public static object GetSanctionDetail(int uid)
        {
            return PartyMsg.GetUIDDetail(uid);
        }
    }
}
