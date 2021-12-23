using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasuryShadowSystem.Configuration;
using System.Web.Services;
namespace TreasuryShadowSystem.Asset
{
    public partial class frm_AssetGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static object AssetGroupList(int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return TreasuryBusiness.Business.AssetGroupBusiness.AssetGroupList(Config.ConnectionString, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object AssetGroupList()
        {
            return TreasuryBusiness.Business.AssetGroupBusiness.AssetGroupList(Config.ConnectionString, 0, 0, "");
        }

        [WebMethod(EnableSession = true)]
        public static object CreateAssetGroup(TreasuryModel.Data.AssetGroup record)
        {
            record.CREATEBY = "System";
            record.CREATEDATE = DateTime.Now;

            return TreasuryBusiness.Business.AssetGroupBusiness.CreateAssetGroup(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateAssetGroup(TreasuryModel.Data.AssetGroup record)
        {
            record.UPDATEBY = "System";
            record.UPDATEDATE = DateTime.Now;

            return TreasuryBusiness.Business.AssetGroupBusiness.UpdateAssetGroup(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteAssetGroup(int ASSETGROUPID)
        {
            return TreasuryBusiness.Business.AssetGroupBusiness.DeleteAssetGroup(Config.ConnectionString, ASSETGROUPID);
        }

        [WebMethod(EnableSession = true)]
        public static object GetAssetGroupOption()
        {
            return TreasuryBusiness.Business.AssetGroupBusiness.GetAssetGroupOption(Config.ConnectionString, 0);
        }
    }
}
