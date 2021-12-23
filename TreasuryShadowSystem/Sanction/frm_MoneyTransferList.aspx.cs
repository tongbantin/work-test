using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using TreasuryShadowSystem.Configuration;
using TreasuryModel.Data;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using System.Text;
using log4net;

namespace TreasuryShadowSystem.Sanction
{
    public partial class frm_MoneyTransferList : System.Web.UI.Page
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_MoneyTransferList));
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                BindFileLoad();
        }

        public void BindFileLoad()
        {
            try
            {
                fileload.DataSource = PartyMsg.getFileLoad(Config.ConnectionString);
                fileload.DataTextField = "FILENAME";
                fileload.DataValueField = "FILENAME";
                fileload.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnDownloadPass_Click(object sender, EventArgs e)
        {
            try
            {
                string Filename = Path.GetFileName(fileload.Value);
                DataTable dtMessage = PartyMsg.GetMessagePassResult(Config.ConnectionString, Filename);
                string[] msg = dtMessage.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();               

                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
                string FullFilename = Server.MapPath("~/Temporary/MoneyTransfer/" + fname);
                File.WriteAllLines(FullFilename, msg, Encoding.UTF8);

                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fname);
                Response.TransmitFile(FullFilename);
                Response.End();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void btnDownloadSelected_Click(object sender, EventArgs e)
        {
            try
            {
                string Filename = Path.GetFileName(fileload.Value);
                DataTable dtMessage = PartyMsg.GetMessageBySelected(Config.ConnectionString, SelectedRowList.Value.Substring(0, SelectedRowList.Value.Length-1));
                string[] msg = dtMessage.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
                string FullFilename = Server.MapPath("~/Temporary/MoneyTransfer/" + fname);
                File.WriteAllLines(FullFilename, msg, Encoding.UTF8);

                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fname);
                Response.TransmitFile(FullFilename);
                Response.End();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        public static object MoneyTransferList(string fileload, string transno, string execdate, string status, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return PartyMsg.MoneyTransferList(Config.ConnectionString, fileload, transno, execdate, status, jtStartIndex, jtPageSize, jtSorting);
        }
    }
}
