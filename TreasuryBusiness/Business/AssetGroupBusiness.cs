using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TreasuryModel.Data;
using TreasuryModel.Repository;
namespace TreasuryBusiness.Business
{
    public class AssetGroupBusiness
    {
        public static List<AssetGroup> AssetGroupList(string ConnectionString)
        {
            try
            {
                List<AssetGroup> AssetGroupList = AssetGroupRepository.AssetGroupList(ConnectionString);
                return AssetGroupList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static object AssetGroupList(string ConnectionString, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                List<AssetGroup> AssetGroupList = AssetGroupRepository.AssetGroupList(ConnectionString, jtStartIndex, jtPageSize, jtSorting);
                return new { Result = "OK", Records = AssetGroupList, TotalRecordCount = AssetGroupList.Count };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public static object CreateAssetGroup(string ConnectionString, AssetGroup record)
        {
            try
            {
                AssetGroup assgroup = AssetGroupRepository.AssetGroupByID(ConnectionString, record.PARENT);
                record.TLEVEL = assgroup.TLEVEL + 1;
                record.CREATEBY = "System";
                record.CREATEDATE = System.DateTime.Now;

                int obj = AssetGroupRepository.CreateAssetGroup(ConnectionString, record);
                if (obj > 0)
                    return new { Result = "OK", Record = record };
                else
                    return new { Result = "ERROR", Message = "Insert Data failed" };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public static object UpdateAssetGroup(string ConnectionString, AssetGroup record)
        {
            try
            {
                AssetGroup assgroup = AssetGroupRepository.AssetGroupByID(ConnectionString, record.PARENT);
                record.TLEVEL = assgroup.TLEVEL + 1;
                record.UPDATEBY = "System";
                record.UPDATEDATE = System.DateTime.Now;

                int obj = AssetGroupRepository.UpdateAssetGroup(ConnectionString, record);
                if (obj > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Update Data failed" };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public static object DeleteAssetGroup(string ConnectionString, int AssetGroupID)
        {
            try
            {
                int obj = AssetGroupRepository.DeleteAssetGroup(ConnectionString, AssetGroupID);
                if (obj > 0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Delete Data failed" };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public static object GetAssetGroupOption(string ConnectionString, int TLEVEL)
        {
            List<AssetGroup> AssetGroupList = AssetGroupRepository.AssetGroupList(ConnectionString, TLEVEL);
            var assetgroup = AssetGroupList
                .Select(c => new { DisplayText = (c.TLEVEL == 2 ? "---" + c.NO : c.TLEVEL == 3 ? "-----" + c.NO : c.NO) + (c.SCRIPLESS=="y" ? "(SCRIPLESS)" : c.SCRIPLESS=="n" ? "(SCRIP)" : "") + "  " + c.DESCRIPTION.Substring(0,50) , Value = c.ASSETGROUPID })
                .OrderBy(p=>p.DisplayText);

            return new { Result = "OK", Options = assetgroup };
        }
    }
}
