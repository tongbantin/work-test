using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using KKB.Treasury.TreasuryCommon.Common;
using TreasuryShadowSystem.Configuration;
using log4net;

namespace TreasuryShadowSystem.Model
{
    public class Model_Tb_Master
    {
        ILog Log = LogManager.GetLogger(typeof(Model_Tb_Menu));

        public Model_Tb_Master()
        {

        }
        public static DateTime getSysDate()
        {
            DataSet ds = null;
            DateTime sysdate = DateTime.Now;
            Oracle o = Oracle.getInstance(Config.ConnectionString);
            try
            {
                o.openConnection();
                o.createCommand("SELECT BRANPRCDATE FROM OPICS.BRPS WHERE BR='01'", CommandType.Text);
                ds = o.executeReader();
                if (ds != null)
                {
                    sysdate = DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                o.closeConnection();
            }
            return (DateTime)sysdate;
        }

        public static DataSet getReportDataNotFound(string reportname)
        {
            try
            {
                DataSet ds = new DataSet("DS_MESSAGE");
                DataTable dt = new DataTable("TB_MESSAGE");
                DataColumn dc1 = new DataColumn("REPORT");
                DataColumn dc2 = new DataColumn("MESSAGE");
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                ds.Tables.Add(dt);
                DataRow dr = dt.NewRow();
                dr["REPORT"] = reportname;
                dr["MESSAGE"] = "Data Not Found";
                dt.Rows.Add(dr);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
