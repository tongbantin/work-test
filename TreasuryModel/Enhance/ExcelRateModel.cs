using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using System.IO;
//using NPOI.HSSF.UserModel;
//using NPOI.SS.UserModel;
using log4net;
using System.Data.OleDb;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class ExcelRateModel
    {
        //Per 1 Sheet
        private static ILog Log = log4net.LogManager.GetLogger(typeof(ExcelRateModel));
        
        public string ExcelFile { get; set; }
        public Int32 SheetIndex { get; set; }
        public String SheetName { get; set; }

        public ExcelRateModel()
        {
            
        }

        public List<DataTable> ExcelDataTable(string ExcelFile, string Round, string Date, string[] sheetname)
        {
            String ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFile+ ";" + "Extended Properties=\"Excel 8.0;HDR=Yes;\";";
            OleDbConnection _oleConn = new OleDbConnection();
            try
            {
                _oleConn.ConnectionString = ConnectionString;
                if (_oleConn.State == ConnectionState.Open)
                    _oleConn.Close();
                _oleConn.Open();

                List<DataTable> l = new List<DataTable>();
                for (int i = 0; i < sheetname.Length; i++)
                {
                    string [] d = Date.Split('/');
                    string sql = @"SELECT [Currency Pair] as PAIR, [Source Code] as SOURCECODE, CheckSP as CHECKSPOT, [End Date] as ENDDATE, DAYS, [Swap Points Mid] as SWAP_POINTS_MID, [Spot Mid] as SPOT_MID, [Outright Mid] as OUTRIGHT_MID, [Index1] as INDEX_1, [Index2] as INDEX_2, '" + Round + "' as ROUND, [End Date] as EDATE, '" + d[1] + "/" + d[0] + "/" + d[2] + " 12:00:00 PM' as CREATEDATE FROM [" + sheetname[i] + "$] ";
                    OleDbCommand _oleCmdSelect = new OleDbCommand(sql, _oleConn);
                    OleDbDataAdapter oleAdapter = new OleDbDataAdapter();
                    oleAdapter.SelectCommand = _oleCmdSelect;
                    DataTable dt = new DataTable(sheetname[i]);
                    oleAdapter.FillSchema(dt, SchemaType.Source);
                    oleAdapter.Fill(dt);

                    //.Net haven't truncate decimal must => shift data to int with *1000000000 , +9 , /10 , truncate to int and shift right with 100000000
                    dt.AsEnumerable().ToList<DataRow>().ForEach(r => {
                        r["SWAP_POINTS_MID"] = RoundUp(Convert.ToDecimal(r["SWAP_POINTS_MID"]));
                        r["SPOT_MID"] = RoundUp(Convert.ToDecimal(r["SPOT_MID"]));
                        r["OUTRIGHT_MID"] = RoundUp(Convert.ToDecimal(r["OUTRIGHT_MID"])); 
                    });

                    l.Add(dt);
                }

                return l;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public decimal RoundUp(decimal val)
        {
            try
            {
                decimal result = 0;
                result = Math.Truncate(val * 1000000000)/10;
                result = Math.Round(result, MidpointRounding.AwayFromZero);
                result = result / 100000000;
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return 0;
            }
        }
    }
}
