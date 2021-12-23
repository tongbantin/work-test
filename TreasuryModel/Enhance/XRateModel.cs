using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Excel = Microsoft.Office.Interop.Excel;
using log4net;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class XRateModel
    {
        //Per 1 Sheet
        private static ILog Log = log4net.LogManager.GetLogger(typeof(XRateModel));
        
        public string ExcelFile { get; set; }
        public Int32 SheetIndex { get; set; }
        public String SheetName { get; set; }
        private string[,] Data { get; set; }

        public XRateModel()
        {
            
        }

        public List<XRateModel> readExcel(String ExcelFile, List<KiatnakinBankTreasurySource> mlist)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook wb = null;
            xlApp.DisplayAlerts = false;
            List<XRateModel> l = new List<XRateModel>();
            try
            {
                if (xlApp == null)
                {
                    Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                    Log.Debug("EXCEL could not be started. Check that your office installation and project references are correct.");
                    throw new ApplicationException();
                }
                xlApp.Visible = false;
                wb = xlApp.Workbooks.Open(@"" + ExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                for (int i = 0; i < mlist.Count(); i++)
                {
                    Log.Debug("==========Section " + (i + 1) + " : Start==========");
                    KiatnakinBankTreasurySource t = (KiatnakinBankTreasurySource)mlist[i];
                    XRateModel e = new XRateModel();

                    int sheetindex = Int32.Parse(t.Sheet);
                    Log.Debug("Section " + (i + 1) + " : Sheet " + sheetindex);
                    Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[sheetindex];
                    e.SheetIndex = sheetindex;
                    e.SheetName = ws.Name;

                    Console.WriteLine("Section " + (i + 1) + " :  Sheet " + sheetindex);
                    if (ws == null)
                    {
                        Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.");
                        Log.Debug("Worksheet could not be created. Check that your office installation and project references are correct.");
                    }

                    /*Get Data*/
                    string[] cell = t.Data.Split(':');

                    //Get Row Name From Excel Range 
                    Excel.Range record = (Excel.Range)ws.get_Range(cell[0], cell[1]);
                    Object arr = record.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);
                    
                    //Declare Return Result
                    string[,] data = new string[record.Rows.Count, record.Columns.Count];
                    if ((record.Rows.Count == 1) && (record.Columns.Count == 1))
                    {
                        string[,] tmp = new string[1, 1];
                        data[0, 0] = arr.ToString();
                    }
                    else
                    {
                        Object[,] obj = (Object[,])arr;
                        for (int m = 0; m < record.Rows.Count; m++)
                        {
                            for (int n = 0; n < record.Columns.Count; n++)
                            {
                                if (obj[m + 1, n + 1] == null)
                                    data[m, n] = "";
                                else
                                    data[m, n] = obj[m + 1, n + 1].ToString();                                
                            }
                        }

                    }
                    e.Data = data;
                    l.Add(e);
                }
                return l;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!! : " + ex.Message);
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                wb.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
            }
        }

        public List<DataTable> ExcelDataTable(String ExcelFile, List<KiatnakinBankTreasurySource> mlist, string[] Column)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook wb = null;
            xlApp.DisplayAlerts = false;
            List<DataTable> l = new List<DataTable>();
            try
            {
                if (xlApp == null)
                {
                    Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                    Log.Debug("EXCEL could not be started. Check that your office installation and project references are correct.");
                    throw new ApplicationException();
                }
                xlApp.Visible = false;
                Log.Debug("Open Excel File : " + ExcelFile);
                
                wb = xlApp.Workbooks.Open(@"" + ExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                for (int i = 0; i < mlist.Count(); i++)
                {
                    Log.Debug("==========Section " + (i + 1) + " : Start==========");
                    KiatnakinBankTreasurySource t = (KiatnakinBankTreasurySource)mlist[i];

                    DataTable dt = new DataTable();
                    int sheetindex = Int32.Parse(t.Sheet);
                    Log.Debug("Section " + (i + 1) + " : Sheet " + sheetindex);
                    Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[sheetindex];

                    Console.WriteLine("Section " + (i + 1) + " :  Sheet " + sheetindex);
                    if (ws == null)
                    {
                        Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.");
                        Log.Debug("Worksheet could not be created. Check that your office installation and project references are correct.");
                    }

                    /*Get Data*/
                    string[] col = t.Column.Split(':');
                    string[] cell = t.Data.Split(':');
                    
                    //Get Row Name From Excel Range 
                    Excel.Range colrecord = (Excel.Range)ws.get_Range(col[0], col[1]);
                    Object arrcol = colrecord.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);

                    //Get Row Name From Excel Range 
                    Excel.Range record = (Excel.Range)ws.get_Range(cell[0], cell[1]);
                    Object arr = record.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);

                    /**************Get Record*****************************/
                    string[,] cols = new string[colrecord.Rows.Count, colrecord.Columns.Count];
                    string[,] data = new string[record.Rows.Count, record.Columns.Count];
                    /**************Create Structure Table******************/
                    if (Column.Length > 1)
                    {
                        for (int n = 0; n < Column.Length; n++)
                        {
                            DataColumn dc = new DataColumn(Column[n].Trim());
                            dt.Columns.Add(dc);
                        }
                    }   
                    else { 
                        if ((colrecord.Rows.Count == 1) && (colrecord.Columns.Count == 1))
                        {
                            for (int n = 0; n < record.Columns.Count; n++)
                            {
                                DataColumn dc = new DataColumn();
                                dt.Columns.Add(dc);
                            }
                        }
                        else 
                        {
                            Object[,] obj = (Object[,])arrcol;
                            for (int m = 0; m < colrecord.Rows.Count; m++ )
                            {
                                for (int n = 0; n < colrecord.Columns.Count; n++)
                                {
                                    DataColumn dc = new DataColumn(obj[m+1, n+1].ToString());
                                    dt.Columns.Add(dc);
                                }
                            }
                        }
                    }
                    

                    //Declare Return Result
                    
                    if ((record.Rows.Count == 1) && (record.Columns.Count == 1))
                    {
                        string[,] tmp = new string[1, 1];
                        data[0, 0] = arr.ToString();
                        DataRow dr = dt.NewRow();   
                        Log.Debug("Record 0 : " + data[0,0]);                     
                        dr[0] = data[0, 0];
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        Object[,] obj = (Object[,])arr;
                        for (int m = 0; m < record.Rows.Count; m++)
                        {
                            DataRow dr = dt.NewRow();
                            for (int n = 0; n < record.Columns.Count; n++)
                            {
                                if (obj[m + 1, n + 1] == null)
                                {
                                    Log.Debug("Data " + m + "," + n + " : Blank");  
                                    data[m, n] = "";
                                }
                                else
                                {
                                    Double decimalout;
                                    if (Double.TryParse(obj[m + 1, n + 1].ToString(), out decimalout))
                                    {
                                        Log.Debug("Data " + m + "," + n + " : " + Convert.ToDecimal(Math.Round(Convert.ToDouble(obj[m + 1, n + 1]), 6, MidpointRounding.AwayFromZero)).ToString());  
                                        data[m, n] = Convert.ToDecimal(Math.Round(Convert.ToDouble(obj[m + 1, n + 1]), 6, MidpointRounding.AwayFromZero)).ToString();
                                    }
                                    else
                                    {
                                        Log.Debug("Data " + m + "," + n + " : " + obj[m + 1, n + 1].ToString());  
                                        data[m, n] = obj[m + 1, n + 1].ToString();
                                    }
                                }
                                if (n == 0)
                                {
                                    if (!ws.Name.Substring(0, 6).Equals(data[m, n]))
                                    {
                                        Log.Debug("Data " + m + "," + n + " : " + ws.Name.Substring(0, 6)); 
                                        data[m, n] = ws.Name.Substring(0, 6);
                                    }
                                }
                                dr[n] = data[m, n];
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    l.Add(dt);
                }
                return l;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!! : " + ex.Message);
                Log.Error(ex.Message);
                return null;
            }
            finally
            {
                wb.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
            }
        }
    }
}
