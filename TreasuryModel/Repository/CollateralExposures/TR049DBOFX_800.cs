using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace TreasuryModel.Repository.OPXRPT
{
    public class TR049DBOFX_800 : BaseExcelReader<TR049DBOFX_800>
    {
        //TROFSSP01B\OpicsPlus22\OPXRPT\20180213\TR049DBOFX_800	    FX Trading
        private const string sheetName = @"FX Trading";
        private TR049DBOFX_800() { }

        public static double ToExposureValue()
        {
            Log.Info("-----TR049DBOFX_800.ToExposureValue begin process -----");
            double result = 0d;
            try
            {
                IWorkbook wb;
                using (FileStream file = new FileStream(excelPath + excelFile, FileMode.Open, FileAccess.Read))
                    wb = new XSSFWorkbook(file);
                ISheet ws = wb.GetSheet(sheetName);
                for (int row = 0; row <= ws.LastRowNum; row++)
                {
                    if (ws.GetRow(row) != null) //null is when the row only contains empty cells
                    {
                        //13 are exposure column
                        //Console.WriteLine(string.Format("{0} : {1}"
                        //    , ws.GetRow(row).GetCell(1).StringCellValue
                        //    , ws.GetRow(row).GetCell(13).NumericCellValue
                        //));
                        result += ws.GetRow(row).GetCell(13).NumericCellValue;
                    }
                }
                Log.Info("-----TR049DBOFX_800.ToExposureValue return with done -----");
            }
            catch (Exception ex)
            {
                Log.Info("-----TR049DBOFX_800.ToExposureValue return with error -----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----TR049DBOFX_800.ToExposureValue finished a process -----");
            }

            return result;
        }
    }
}
