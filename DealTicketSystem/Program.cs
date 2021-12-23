using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;
using System.Data.OracleClient;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using log4net;
using KKB.DealTicketSystem.Configuration;
using KKB.DealTicketModel.Model;
using KKB.Treasury.ThirdParty;

namespace KKB.DealTicketSystem
{
    class Program
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            //OracleConnection o = new OracleConnection(Config.ConnectionString);
            IDatabase ora = new SystemOracleClient();
            OracleConnection o = new OracleConnection();                
            try
            {
                Program.Log.Info("-----Start DealTicket System-----");
                Console.WriteLine("-----Start DealTicket System-----");

                o = (OracleConnection)ora.getConnection();
                //if (o.State == ConnectionState.Open)
                //    o.Close();
                //o.Open();
                OracleCommand command = new OracleCommand("OPICINF.KKB_DEALS_TICKET_PRINT", o);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("p_branch", "01");
                OracleParameter outparam = new OracleParameter("ref_rpt_cur", OracleType.Cursor);
                command.Parameters.Add(outparam).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                OracleDataReader reader = (OracleDataReader)command.Parameters["ref_rpt_cur"].Value;

                while (reader.Read())
                {
                    bool printresult = false;
                    DataSet s = new DataSet();

                    String Branch = reader["BR"].ToString();
                    String Product = reader["PRODUCT"].ToString();
                    String ProductType = reader["PRODTYPE"].ToString();
                    String DealNo = reader["DEALNO"].ToString();
                    String FromType = reader["FORMTYPE"].ToString();
                    
                    Program.Log.Info("Start Print Deal No : " + DealNo);
                    Console.WriteLine("Start Print Deal No : " + DealNo);

                    Program.Log.Info("FormType : " + reader["FORMTYPE"].ToString());

                    //string[] ArrPrint;
                    //if (Config.PrintToPrinter != null)
                    //    ArrPrint = Config.PrintToPrinter.Split(',');
                    //else 
                    //{
                    //    ArrPrint = new string[1];
                    //    ArrPrint[0] = "";
                    //}
                    //string[] ArrExcel;
                    //if (Config.PrintToExcel != null)
                    //    ArrExcel = Config.PrintToExcel.Split(',');
                    //else {
                    //    ArrExcel = new string[1];
                    //    ArrExcel[0] = "";
                    //}

                    string[] ArrPrint = Config.PrintToPrinter.Split(',');
                    string[] ArrExcel = Config.PrintToExcel.Split(',');

                    Boolean Is2Print = false;
                    Boolean Is2Excel = false;
                    switch (reader["FORMTYPE"].ToString())
                    { 
                        case "DL01" :
                            if (ArrPrint.Contains("DL01"))
                                Is2Print = true;
                            if (ArrExcel.Contains("DL01"))
                                Is2Excel = true;
                            s = DealTicket.getReport(DL.StoreProcedure, DL.TableDataSet, Branch, Product, ProductType, DealNo);
                            printresult = Program.GetDealTicket(DL.ReportFileName, DL.ReportName, DL.TableDataSet, s, Is2Print, Is2Excel, Product + ProductType + DealNo);
                            break;
                        case "FI01":
                            if (ArrPrint.Contains("FI01"))
                                Is2Print = true;
                            if (ArrExcel.Contains("FI01"))
                                Is2Excel = true;
                            s = DealTicket.getReport(FI.StoreProcedure, FI.TableDataSet, Branch, Product, ProductType, DealNo);
                            printresult = Program.GetDealTicket(FI.ReportFileName, FI.ReportName, FI.TableDataSet, s, Is2Print, Is2Excel, Product + ProductType + DealNo);
                            break;
                        case "FX01":
                            if (ArrPrint.Contains("FX01"))
                                Is2Print = true;
                            if (ArrExcel.Contains("FX01"))
                                Is2Excel = true;
                            s = DealTicket.getReport(FX.StoreProcedure, FX.TableDataSet, Branch, Product, ProductType, DealNo);
                            printresult = Program.GetDealTicket(FX.ReportFileName, FX.ReportName, FX.TableDataSet, s, Is2Print, Is2Excel, Product + ProductType + DealNo);
                            break;
                        case "RP01":
                            if (ArrPrint.Contains("RP01"))
                                Is2Print = true;
                            if (ArrExcel.Contains("RP01"))
                                Is2Excel = true;
                            s = DealTicket.getReport(RP.StoreProcedure, RP.TableDataSet, Branch, Product, ProductType, DealNo);
                            printresult = Program.GetDealTicket(RP.ReportFileName, RP.ReportName, RP.TableDataSet, s, Is2Print, Is2Excel, Product + ProductType + DealNo);
                            break;
                        case "SW01":
                            if (ArrPrint.Contains("SW01"))
                                Is2Print = true;
                            if (ArrExcel.Contains("SW01"))
                                Is2Excel = true;
                            s = DealTicket.getReport(SW.StoreProcedure, SW.TableDataSet, Branch, Product, ProductType, DealNo);
                            printresult = Program.GetDealTicket(SW.ReportFileName, SW.ReportName, SW.TableDataSet, s, Is2Print, Is2Excel, Product + ProductType + DealNo);
                            break;
                        case "FX02":
                            if (ArrPrint.Contains("FX02"))
                                Is2Print = true;
                            if (ArrExcel.Contains("FX02"))
                                Is2Excel = true;
                            s = DealTicket.getReport(FXSW.StoreProcedure, FXSW.TableDataSet, Branch, Product, ProductType, DealNo);
                            printresult = Program.GetDealTicket(FXSW.ReportFileName, FXSW.ReportName, FXSW.TableDataSet, s, Is2Print, Is2Excel, Product + ProductType + DealNo);
                            break;
                    }
                    if (printresult)
                    {
                        if (!DealTicket.UpdateStatus(Branch, Product, ProductType, DealNo, "P"))
                        {
                            Program.Log.Info("-----Update Status Error-----");
                            Program.Log.Error("-----Update Status Error-----");
                            Console.WriteLine("-----Update Status Error-----");
                            Console.WriteLine("-----Update Status Error-----");
                        }
                    }
                }
            } 
            catch(Exception ex)
            {
                Program.Log.Info("-----Export Error-----");
                Program.Log.Error(ex.Message);
                Console.WriteLine("-----Export Error-----");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Program.Log.Info("-----DealTicket System  Finish-----");
                Console.WriteLine("-----DealTicket System Finish-----");
                o.Close();
            }
        }

        public static bool GetDealTicket(String ReportFileName, String ReportName, String TbName, DataSet ds, Boolean Is2Print, Boolean Is2Excel, String EPName )
        {
            try
            {
                ReportDocument doc = new ReportDocument();

                Console.WriteLine(Config.ReportPath + ReportFileName);
                doc.Load(Config.ReportPath + ReportFileName);


                if (ds != null)
                {
                    
                    string ExportFileName = Config.ExportPath + EPName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

                    Program.Log.Info("DealTicket : " + ds.GetXml());
                    doc.SetDataSource(ds);
                    try
                    {
                        if (Is2Print)
                        {
                            doc.PrintOptions.PaperSize = PaperSize.PaperA4;
                            doc.PrintOptions.PrinterName = Config.PrinterName;
                            doc.PrintToPrinter(1, true, 1, 1);
                            Program.Log.Info("Report Path : " + Config.ReportPath + ReportFileName);
                            Program.Log.Info("-----Print Complete-----");
                            Console.WriteLine("Report Path : " + Config.ReportPath + ReportFileName);
                            Console.WriteLine("-----Print Complete-----");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Print Error : " + ex.Message);
                        Program.Log.Error("Print Error : " + ex.Message);
                    }
                    /*Version 1.2 : 27/01/2014*/
                    try
                    {
                        if (Is2Excel)
                        {
                            ExportOptions rptExportOption;
                            DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
                            ExcelFormatOptions rptFormatOption = new ExcelFormatOptions();
                            rptFormatOption.ExcelUseConstantColumnWidth = true;
                            rptFormatOption.ExcelConstantColumnWidth = 2000;
                            rptFileDestOption.DiskFileName = ExportFileName;
                            rptExportOption = doc.ExportOptions;
                            {
                                rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                                rptExportOption.ExportFormatType = ExportFormatType.Excel;
                                rptExportOption.ExportDestinationOptions = rptFileDestOption;
                                rptExportOption.ExportFormatOptions = rptFormatOption;
                            }
                            doc.Export();
                        }                    
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Export Error : " + ex.Message);
                        Program.Log.Error("Export Error : " + ex.Message);
                    }
                    return true;
                }
                else
                {
                    Program.Log.Info("----Data Not Found-----");
                    Console.WriteLine("-----Data Not Found-----");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Program.Log.Info("----Error : " + ex.Message + "-----");
                Console.WriteLine("-----Error : " + ex.Message + "-----");
                return false;
            }
        }
    }
}
