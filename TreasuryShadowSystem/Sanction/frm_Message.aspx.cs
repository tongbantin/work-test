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
using System.Xml.Serialization;
using System.IO;
using TreasuryModel.Enhance;
using System.Net.Mail;
using TreasuryShadowSystem.Configuration;

namespace TreasuryShadowSystem.Sanction
{
    public partial class frm_Message : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_Message));

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
                    SWIFTMsgChk rowdata = e.Row.DataItem as SWIFTMsgChk;
                    List<SWIFTMsgChk> msg = SWIFTMsgChk.getSanctionDetailByMessageID(Configuration.Config.ConnectionString, rowdata.CHECKINGID.ToString());

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

        void BindDetail(GridView gd, List<SWIFTMsgChk> msg)
        {
            try
            {
                List<TreasuryModel.SanctionService.RecordType> record = new List<TreasuryModel.SanctionService.RecordType>();
                for (int i = 0; i < msg.Count; i++)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TreasuryModel.SanctionService.RecordType));
                    string[] rd = msg[i].DETAIL.Split(new string[] { "</RecordType>" }, StringSplitOptions.None);
                    for (int j = 0; j < rd.Length; j++)
                    {
                        if (!rd[j].Trim().Equals(""))
                        {
                            StringReader rdr = new StringReader(rd[j] + "</RecordType>");
                            TreasuryModel.SanctionService.RecordType searchrecord = (TreasuryModel.SanctionService.RecordType)serializer.Deserialize(rdr);
                            record.Add(searchrecord);
                        }
                    }
                    
                    //StringReader rdr = new StringReader(msg[i].DETAIL);
                    //TreasuryModel.SanctionService.RecordType searchrecord = (TreasuryModel.SanctionService.RecordType)serializer.Deserialize(rdr);
                    //record.Add(searchrecord);
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
                BindGrid();
                RenderForm();
            }
        }

        private void BindGrid() {
            try
            {
                GridSanction.DataSource = SWIFTMsgChk.getCheckSanctionByMessageID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                GridSanction.DataBind();
            }
            catch (Exception ex)
            { 
                
            }
        }

        private void RenderForm()
        {
            try
            {
                SWIFTMsg msgobj = SWIFTMsg.getMessageByID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                string swiftMessage = msgobj.MSG;

                //View Or Edit
                if ((!msgobj.STATUS.Equals(MessageStatus.WAITING_TO_REVIEW.ToString())) && (!msgobj.STATUS.Equals(MessageStatus.FAIL_TO_SEND)))
                {
                    RejectButton.Visible = false;
                    RecheckButton.Visible = false;
                    AcceptButton.Visible = false;
                    RemarkBox.ReadOnly = false;
                }
                else {

                    RejectButton.Visible = true;
                    RecheckButton.Visible = true;
                    AcceptButton.Visible = true;
                    RemarkBox.ReadOnly = true;
                }

                //Split Message
                ArrayList dictmsg = new ArrayList();
                String[] series = swiftMessage.Replace("{", "").Split('}');  //Delete { and split with } 
                foreach (var msgline in series)
                {
                    String[] msg = msgline.Split(':'); //Split each row 
                    
                    Label lb = new Label() { ID = "Label" + msg[0], Text = msg[0] };
                    TextBox txt = new TextBox() { ID = "Text" + msg[0], Text = msg[1], Width = 400, ReadOnly=true };

                    TableCell cellLabel = new TableCell() { HorizontalAlign = HorizontalAlign.Right };
                    cellLabel.Controls.Add(lb);

                    TableCell cellValue = new TableCell();
                    cellValue.Controls.Add(txt);

                    TableRow row = new TableRow();
                    row.Cells.Add(cellLabel);
                    row.Cells.Add(cellValue);

                    SWIFTDetailTable.Rows.AddAt(SWIFTDetailTable.Rows.Count - 3, row);

                    if (msg.Length > 2)
                    {
                        int k = 2;
                        while(k<=msg.Count())
                        {
                            Label lba = new Label() { ID = "Label" + msg[k], Text = msg[k] };
                            TextBox txta = new TextBox() { ID = "Text" + msg[k], Text = msg[k+1], Width = 400, ReadOnly=true };

                            TableCell cellLabela = new TableCell() { HorizontalAlign = HorizontalAlign.Right };
                            cellLabela.Controls.Add(lba);

                            TableCell cellValuea = new TableCell();
                            cellValuea.Controls.Add(txta);

                            TableRow rowa = new TableRow();
                            rowa.Cells.Add(cellLabela);
                            rowa.Cells.Add(cellValuea);

                            SWIFTDetailTable.Rows.AddAt(SWIFTDetailTable.Rows.Count - 3, rowa);
                            k += 2;
                        }
                    }

                }
                 

            }
            catch (Exception ex)
            { 
                
            }
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SWIFTMsg.AcceptTransaction(Configuration.Config.ConnectionString, Request.QueryString["Msg"], RemarkBox.Text.Trim()))
                {
                    SWIFTMsg txt = SWIFTMsg.getMessageByID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                    string Msg = "The message REF NO : " + txt.REFNO + " is APPROVE.";
                    SendMail.Send(
                        Config.GetParameterFile("147MAILRESULTFROM")
                        , Config.GetParameterFile("147MAILRESULTTO")
                        , Config.GetParameterFile("147MAILRESULTCC")
                        , Config.GetParameterFile("147MAILRESULTSUBJECT")
                        , Msg, new List<Attachment>(), Config.GetParameterFile("089SMTP")
                        , int.Parse(Config.GetParameterFile("089SMTPPort")));
                    MessageBox.Text = "The message Order ID : " + txt.REFNO + " is APPROVE.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: { close : function(){ $(this).dialog('close'); window.location='frm_SanctionList.aspx' }}});", true);
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

                if (SWIFTMsg.RejectTransaction(Configuration.Config.ConnectionString, Request.QueryString["Msg"], RemarkBox.Text.Trim()))
                {

                    SWIFTMsg txt = SWIFTMsg.getMessageByID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                    string Msg = "The message REF NO : " + txt.REFNO + " is REJECT.";
                    SendMail.Send(
                        Config.GetParameterFile("147MAILRESULTFROM")
                        , Config.GetParameterFile("147MAILRESULTTO")
                        , Config.GetParameterFile("147MAILRESULTCC")
                        , Config.GetParameterFile("147MAILRESULTSUBJECT")
                        , Msg, new List<Attachment>(), Config.GetParameterFile("089SMTP")
                        , int.Parse(Config.GetParameterFile("089SMTPPort")));
                    MessageBox.Text = "The message REF NO : " + txt.REFNO + " is REJECT!!.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: { close : function(){ $(this).dialog('close'); window.location='frm_SanctionList.aspx' }}});", true);
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

        protected void RecheckButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SWIFTMsg.RecheckTransaction(Configuration.Config.ConnectionString, Request.QueryString["Msg"], RemarkBox.Text.Trim()))
                {

                    SWIFTMsg txt = SWIFTMsg.getMessageByID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);
                    string Msg = "The message REF NO : " + txt.REFNO + " is RECHECK.";
                    SendMail.Send(
                        Config.GetParameterFile("147MAILRESULTFROM")
                        , Config.GetParameterFile("147MAILRESULTTO")
                        , Config.GetParameterFile("147MAILRESULTCC")
                        , Config.GetParameterFile("147MAILRESULTSUBJECT")
                        , Msg, new List<Attachment>(), Config.GetParameterFile("089SMTP")
                        , int.Parse(Config.GetParameterFile("089SMTPPort")));
                    MessageBox.Text = "The message REF NO : " + txt.REFNO + " is RECHECK.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: { close : function(){ $(this).dialog('close'); window.location='frm_SanctionList.aspx' }}});", true);
                }
                else
                {
                    MessageBox.Text = "Change status Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Kiatnakin Bank", "$('#dialogmessage').dialog({title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)',modal: true, buttons: { close: function() { $(this).dialog('close'); }}});", true);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
