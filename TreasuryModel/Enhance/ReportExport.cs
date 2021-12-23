using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;

using KKB.Treasury.TreasuryCommon.Common;
using KKB.Treasury.TreasuryReport.Report;

namespace KKB.Treasury.TreasuryModel.Model
{
    public class ReportExport
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(ReportExport));
        protected ReportExport() { }
        public static bool GenerateFile(DataTable dt, ReportDocument doc, string FileType, string ExportPath, string ExportFileName)
        {
            try
            {
                Log.Info("---Generate File With Rows=" + dt.Rows.Count + ", FileType="+ FileType +", ExportPath="+ ExportPath +", ExportFileName="+ ExportFileName +" ---");

                Log.Info("---Start Check Directory---");
                if (Directory.Exists(ExportPath + FileType))
                {
                    Log.Info("---Start Clear Directory before Create Directory  ---");
                    string[] f = Directory.GetFiles(ExportPath + FileType);
                    for (int l = 0; l < f.Length; l++)
                    {
                        File.Delete(f[l]);
                    }
                    Directory.Delete(ExportPath + FileType);
                }
                Log.Info("---Start Create Diretory : " + ExportPath + "---");
                Directory.CreateDirectory(ExportPath);
                Log.Info("---Bind Data Source---");
                doc.SetDataSource(dt);
                
                Log.Info("---Start Export By FileType : " + FileType + "---");
                if (FileType.ToUpper().Equals("PDF"))
                {
                    doc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    doc.PrintOptions.PaperSize = PaperSize.PaperA3;
                    DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
                    ExportOptions rptExportOption;
                    rptFileDestOption.DiskFileName = ExportPath + "\\" + ExportFileName + ".pdf";
                    rptExportOption = doc.ExportOptions;
                    rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                    rptExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rptExportOption.ExportDestinationOptions = rptFileDestOption;

                    Log.Info("Before Export PDF");
                    doc.Export(rptExportOption);
                    Log.Info("After Export PDF");
                }
                else if (FileType.Equals("Excel"))
                {
                    DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
                    ExportOptions rptExportOption;
                    ExcelFormatOptions rptFormatOption = new ExcelFormatOptions();
                    rptFormatOption.ExcelUseConstantColumnWidth = true;
                    rptFormatOption.ExcelConstantColumnWidth = 2000;
                    rptFileDestOption.DiskFileName = ExportPath + "\\" + ExportFileName + ".xls";
                    rptExportOption = doc.ExportOptions;
                    {
                        rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                        if (FileType.Equals("Excel"))
                            rptExportOption.ExportFormatType = ExportFormatType.Excel;
                        else if (FileType.Equals("CSV"))
                            rptExportOption.ExportFormatType = ExportFormatType.ExcelRecord;
                        rptExportOption.ExportDestinationOptions = rptFileDestOption;
                        rptExportOption.ExportFormatOptions = rptFormatOption;
                    }
                    Log.Info("Before Export Excel");
                    doc.Export(rptExportOption);
                    Log.Info("After Export Excel");
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Info("ERROR : " + ex.Message);
                return false;
            }
        }

        
    }
}
