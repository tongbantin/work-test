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

namespace TreasuryShadowSystem.TR154TRSSFTLN
{
    public partial class frm_TR154LSTDT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static object SoftLoanList(string txt_valuedate,string txt_matdate, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            TreasuryModel.Opics.SoftLoan.UpdateDealno(Config.ConnectionString);
            return TreasuryModel.Opics.SoftLoan.SoftLoanList(txt_valuedate,txt_matdate,Config.ConnectionString, jtStartIndex, jtPageSize, jtSorting);
        }

        //[WebMethod(EnableSession = true)]
        //public static object Create(TreasuryModel.Opics.SoftLoan record)
        //{
        //    return TreasuryModel.Opics.SoftLoan.Create(Config.ConnectionString, record);
        //}

        //[WebMethod(EnableSession = true)]
        //public static object Update(TreasuryModel.Opics.SoftLoan record)
        //{
        //    return TreasuryModel.Opics.SoftLoan.Update(Config.ConnectionString, record);
        //}

        [WebMethod(EnableSession = true)]
        public static object Delete(int RECID)
        {
            return TreasuryModel.Opics.SoftLoan.Delete(Config.ConnectionString, RECID);
        }


        [WebMethod(EnableSession = true)]
        public static object GenerateFile(object LISTID)
        {
            return new { Result = "OK" };
        }
    }
}
