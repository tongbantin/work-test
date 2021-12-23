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
    public partial class frm_SecAsset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAssetGroup();
            }
        }

        [WebMethod(EnableSession = true)]
        public static object SecAssetList(string secid, string assetgroupid, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            return TreasuryBusiness.Business.SecAssetBusiness.SecAssetList(Config.ConnectionString, secid, assetgroupid, jtStartIndex, jtPageSize, jtSorting);
        }

        [WebMethod(EnableSession = true)]
        public static object SecAssetList(string secid, string assetgroupid)
        {
            return TreasuryBusiness.Business.SecAssetBusiness.SecAssetList(Config.ConnectionString, secid, assetgroupid, 0, 0, "");
        }

        [WebMethod(EnableSession = true)]
        public static object CreateSecAsset(TreasuryModel.Data.SecAsset record)
        {
            return TreasuryBusiness.Business.SecAssetBusiness.CreateSecAsset(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object UpdateSecAsset(TreasuryModel.Data.SecAsset record)
        {
            return TreasuryBusiness.Business.SecAssetBusiness.UpdateSecAsset(Config.ConnectionString, record);
        }

        [WebMethod(EnableSession = true)]
        public static object DeleteSecAsset(int SECASSETID)
        {
            return TreasuryBusiness.Business.SecAssetBusiness.DeleteSecAsset(Config.ConnectionString, SECASSETID);
        }


        [WebMethod(EnableSession = true)]
        public static object GetAssetGroupOption()
        {
            return TreasuryBusiness.Business.AssetGroupBusiness.GetAssetGroupOption(Config.ConnectionString, 3);
        }

        [WebMethod(EnableSession = true)]
        public static object LoadSecAsset()
        {
            return TreasuryBusiness.Business.SecAssetBusiness.LoadSecAsset(Config.ConnectionString);
        }

        private void BindAssetGroup()
        {
            try
            {
                AssetGroupBox.DataSource = TreasuryBusiness.Business.AssetGroupBusiness.AssetGroupList(Config.ConnectionString);
                AssetGroupBox.DataTextField = "DESCRIPTION";
                AssetGroupBox.DataValueField = "ASSETGROUPID";
                AssetGroupBox.DataBind();

                AssetGroupBox.Items.Insert(0, new ListItem("----ทั้งหมด----", ""));
                AssetGroupBox.Items.Insert(1, new ListItem("-ยังไม่ได้จัดกลุ่ม-", "0"));
            }
            catch (Exception ex)
            { 
            
            }
        }

        //protected void loadButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        TreasuryBusiness.Business.SecAssetBusiness.LoadSecAsset(Config.ConnectionString);
        //    }
        //    catch (Exception ex)
        //    { 
            
        //    }
        //}
    }
}
