using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TreasuryModel.Data;
using TreasuryModel.Repository;
namespace TreasuryBusiness.Business
{
    public class SecAssetBusiness
    {
        public static object SecAssetList(string ConnectionString, string secid, string assetgroupid, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                List<SecAsset> secassetList = SecAssetRepository.SecAssetList(ConnectionString, secid, assetgroupid, jtStartIndex, jtPageSize, jtSorting);
                return new { Result = "OK", Records = secassetList, TotalRecordCount = secassetList.Count };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public static object CreateSecAsset(string ConnectionString, SecAsset record)
        {
            try
            {
                record.CREATEBY = "System";
                record.CREATEDATE = DateTime.Now;

                int obj = SecAssetRepository.CreateSecAsset(ConnectionString, record);
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

        public static object UpdateSecAsset(string ConnectionString, SecAsset record)
        {
            try
            {
                record.UPDATEBY = "System";
                record.UPDATEDATE = DateTime.Now;

                int obj = SecAssetRepository.UpdateSecAsset(ConnectionString, record);
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

        public static object DeleteSecAsset(string ConnectionString, int SecAssetID)
        {
            try
            {
                int obj = SecAssetRepository.DeleteSecAsset(ConnectionString, SecAssetID);
                if (obj>0)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Delete Data failed" };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

        public static object LoadSecAsset(string ConnectionString)
        {
            try
            {
                int obj = SecAssetRepository.LoadSecAsset(ConnectionString);
                if (obj != -99)
                    return new { Result = "OK" };
                else
                    return new { Result = "ERROR", Message = "Load Data failed" };
            }
            catch (Exception ex)
            {
                return new { Result = "ERROR", Message = ex.Message };
            }
        }

    }
}
