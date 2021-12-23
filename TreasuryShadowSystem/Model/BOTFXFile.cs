using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml.Linq;

using log4net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using KKB.Treasury.TreasuryModel.Model;
using System.Threading;
using System.Globalization;

namespace TreasuryShadowSystem.Model
{
    public class BOTFXFile
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(BOTFXFile));

        public string ConnectionString { get; set; }
        public string XMLPath { get; set; }
        public string ExportPath { get; set; }
        public string TemplateFile { get; set; }
        public string ExportFileName { get; set; }
        public string Frequency { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public Boolean GenerateFile() 
        {

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                //Mapping XML
                MappingList m = new MappingList(XMLPath);
                KiatnakinBankForeignExchange fe = m.ForeignExchange();
                List<KiatnakinBankForeignExchangeCurrency> f = m.ForeignExchangeMappingList();
                List<KiatnakinBankSheetDayFrequencySheet> s = m.SheetDayMappingList(Frequency);

                //Get Data From Oracle
                DataTable ccy = Model_RPT_TR097BOPODETAIL.GetCurrentPosition(ConnectionString, Frequency, FromDate, ToDate);
                DataTable capital = Model_RPT_TR097BOPODETAIL.GetCapitalBalance(ConnectionString, Frequency, FromDate, ToDate);
                List<DataRow> freccy = ccy.AsEnumerable().GroupBy(t => t.Field<DateTime>("ReportDate")).Select(g => g.First()).ToList<DataRow>();

                //Daily or Weekly
                
                List<WeeklyDay> wd = new List<WeeklyDay>();
                for (int i = 0; i < s.Count; i++)
                {
                    for (int j = 0; j < freccy.Count; j++)
                    {
                        DateTime d = (DateTime)freccy[j]["ReportDate"];
                        if (d.ToString("dddd").Equals(s[i].Date))
                        {
                            WeeklyDay w = new WeeklyDay();
                            w.WeekDate = d;
                            w.WeekDay = d.ToString("dddd");
                            w.Day = d.Day;
                            w.Month = d.Month;
                            w.Year = d.Year + 543;
                            w.ColumnDay = s[i].Day;
                            w.ColumnMonth = s[i].Month;
                            w.ColumnYear = s[i].Year;
                            w.Sheet = Int32.Parse(s[i].Value);
                            DataRow cap = capital.AsEnumerable().FirstOrDefault(t => t.Field<DateTime>("EFFDATE") == d);
                            if (cap != null)
                                w.Capital = Decimal.Parse(cap["Amount"].ToString());
                            else
                                w.Capital = 0;
                            wd.Add(w);
                        }
                    }
                }

                
                //Write Data From Oracle To Excel
                FileInfo oldFile = new FileInfo(TemplateFile);
                string newFileName = ExportFileName;
                oldFile.CopyTo(newFileName, true);

                HSSFWorkbook hssfwb;
                using (FileStream file = new FileStream(newFileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    hssfwb = new HSSFWorkbook(file);
                }

                for (int k = 0; k < wd.Count; k++)
                {
                    WeeklyDay wdd = wd[k];
                    ISheet sheet = hssfwb.GetSheetAt(wdd.Sheet);
                    //Set Date Field
                    ISheet firstsheet = hssfwb.GetSheetAt(7); //Fix ws sheet
                    int[] cday = Utilities.GetRowColumn(wdd.ColumnDay);
                    int[] cmonth = Utilities.GetRowColumn(wdd.ColumnMonth);
                    int[] cyear = Utilities.GetRowColumn(wdd.ColumnYear);
                    firstsheet.GetRow(cday[0]).GetCell(cday[1]).SetCellValue(wdd.Day);
                    firstsheet.GetRow(cmonth[0]).GetCell(cmonth[1]).SetCellValue(wdd.Month);
                    firstsheet.GetRow(cyear[0]).GetCell(cyear[1]).SetCellValue(wdd.Year);

                    if (wd.Count > 1)
                    {
                        if (wdd.WeekDate.ToString("dddd").ToUpper().Equals("THURSDAY"))
                        {
                            int[] eday = Utilities.GetRowColumn("H10");
                            int[] emonth = Utilities.GetRowColumn("B5");
                            int[] eyear = Utilities.GetRowColumn("B19");
                            firstsheet.GetRow(eday[0]).GetCell(eday[1]).SetCellValue(wdd.Day);
                            firstsheet.GetRow(emonth[0]).GetCell(emonth[1]).SetCellValue(wdd.Month);
                            firstsheet.GetRow(eyear[0]).GetCell(eyear[1]).SetCellValue(wdd.Year);
                        }
                    }

                    //Write Data To Excel
                    int[] crc = Utilities.GetRowColumn(fe.Capital);
                    sheet.GetRow(crc[0]).GetCell(crc[1]).SetCellValue(Double.Parse(wdd.Capital.ToString()));
                    for (int i = 0; i < f.Count; i++)
                    {
                        KiatnakinBankForeignExchangeCurrency c = (KiatnakinBankForeignExchangeCurrency)f[i];
                        List<DataRow> dr = ccy.Select().Where(t => t.Field<string>("CCY").Equals(c.Name) && t.Field<DateTime>("REPORTDATE").Equals(wdd.WeekDate)).ToList<DataRow>();
                        if (dr.Count > 0)
                        {
                            for (int j = 0; j < c.Mapping.Count(); j++)
                            {
                                KiatnakinBankForeignExchangeCurrencyMapping mapping = (KiatnakinBankForeignExchangeCurrencyMapping)c.Mapping[j];
                                int[] rc = Utilities.GetRowColumn(mapping.Value);
                                sheet.GetRow(rc[0]).GetCell(rc[1]).SetCellValue(Double.Parse(dr[0].Field<decimal>(mapping.Column).ToString()));
                            }
                        }
                    }
                }
                FileStream fs = new FileStream(newFileName, FileMode.Open, FileAccess.ReadWrite);
                hssfwb.Write(fs);
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }
    }
}
