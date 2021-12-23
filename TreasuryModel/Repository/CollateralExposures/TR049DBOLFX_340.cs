using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace TreasuryModel.Repository.OPXRPT
{
    public class TR049DBOLFX_340 : BaseExcelReader<TR049DBOLFX_340>
    {
        //TROFSSP01B\OpicsPlus22\OPXRPT\20180213\TR049DBOLFX_340	LFX
        private const string sheetName = @"LFX";

        private TR049DBOLFX_340() { }

        public static double ToExposureValue()
        {
            Log.Info("-----TR049DBOLFX_340.ToExposureValue begin process -----");
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
                        result += ws.GetRow(row).GetCell(9).NumericCellValue;
                    }
                }
                Log.Info("-----TR049DBOLFX_340.ToExposureValue return with done -----");
            }
            catch (Exception ex)
            {
                Log.Info("-----TR049DBOLFX_340.ToExposureValue return with error -----");
                Log.Error(ex.Message);
            }
            finally
            {
                Log.Info("-----TR049DBOLFX_340.ToExposureValue finished a process -----");
            }

            return result;
        }
    }
}
