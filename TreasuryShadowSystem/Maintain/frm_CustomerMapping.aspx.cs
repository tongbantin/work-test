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

namespace TreasuryShadowSystem.Maintain
{
    public partial class frm_CustomerMapping : System.Web.UI.Page
    {
        [WebMethod(EnableSession = true)]
        public static object CustomerMappingList(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return TreasuryModel.Data.CustTypeMapping.CustTypeMappingList(Config.ConnectionString, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object CreateCustomerMapping(TreasuryModel.Data.CustTypeMapping record)
        {
            return TreasuryModel.Data.CustTypeMapping.CreateCustTypeMapping(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateCustomerMapping(TreasuryModel.Data.CustTypeMapping record)
        {
            return TreasuryModel.Data.CustTypeMapping.UpdateCustTypeMapping(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteCustomerMapping(int CUSTTYPEMAPPINGID)
        {
            return TreasuryModel.Data.CustTypeMapping.DeleteCustTypeMapping(Config.ConnectionString, CUSTTYPEMAPPINGID);
        }
    }
}
