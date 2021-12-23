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
using TreasuryModel.Data;
using System.Text;
using System.IO;

namespace TreasuryShadowSystem.Sanction
{
    public partial class frm_MoneyTransferDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DownloadFile();
        }

        protected void DownloadFile()
        {
            try
            {
                PartyMsg msg = PartyMsg.getMessageByID(Configuration.Config.ConnectionString, Request.QueryString["Msg"]);

                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
                string FullFilename = Server.MapPath("~/Temporary/MoneyTransfer/" + fname);
                File.WriteAllText(FullFilename, msg.MSG, Encoding.UTF8);

                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fname);
                Response.TransmitFile(FullFilename);
                Response.End();    
                    
            }
            catch (Exception ex)
            { 
                
            }
        }
    }
}
