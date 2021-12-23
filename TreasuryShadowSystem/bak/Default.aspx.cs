using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using TreasuryShadowSystem.Configuration;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;

namespace TreasuryShadowSystem
{
    public partial class _Default : System.Web.UI.Page
    {
        private ILog Log = log4net.LogManager.GetLogger(typeof(_Default));

        protected void Page_Load(object sender, EventArgs e)
        {
            //OracleConnection conn = new OracleConnection(Config.ConnectionString);
            //if (conn.State != ConnectionState.Open)
            //    conn.Open();
            //OracleCommand command = new OracleCommand("SYSTEM.kkb_sp_tss_tc021obobe", conn);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.Add("p_branch",OracleDbType.Char,2, "01");
            //command.Parameters.Add("p_trade_date_from",OracleDbType.Varchar2,10, "01/04/2012");
            //command.Parameters.Add("p_trade_date_to", OracleDbType.Varchar2, 10, "01/08/2012");
            //command.Parameters.Add("p_deal_no_from", OracleDbType.Varchar2, 10, "01/04/2012");
            //command.Parameters.Add("p_deal_no_to", OracleDbType.Varchar2, 10, "01/08/2012");
            //OracleParameter paramReturn = new OracleParameter("ref_rpt_cur", OracleDbType.RefCursor, ParameterDirection.Output);
            //command.Parameters.Add(paramReturn);

            //int result = command.ExecuteNonQuery();
            //OracleRefCursor refCur = (OracleRefCursor)paramReturn.Value;
            //OracleDataReader dr = refCur.GetDataReader();
            //OracleDataAdapter adapter = new OracleDataAdapter();
            

            //while (dr.Read())
            //{
            //    string str = "";
            //    for (int i = 0; i < dr.FieldCount; i++ )
            //    {
            //        str += dr.GetName(i) + "=" + dr[i];
            //        if (i!=dr.FieldCount-1)
            //            str += ",";
            //    }
            //    Log.Info("Record: " + str);
            //}
                
            

            Response.Redirect("Login.aspx");
        }
    }
}
