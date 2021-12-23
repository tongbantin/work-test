using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KKB.DealTicketSystem.Configuration
{
    class Config
    {
        public static string ExportPath = System.Configuration.ConfigurationSettings.AppSettings["ExportPath"].ToString();
        public static string ReportPath = System.Configuration.ConfigurationSettings.AppSettings["ReportPath"].ToString();
        public static string PrinterName = System.Configuration.ConfigurationSettings.AppSettings["PrinterName"].ToString();
        public static string PrintToPrinter = System.Configuration.ConfigurationSettings.AppSettings["PrintToPrinter"].ToString();
        public static string PrintToExcel = System.Configuration.ConfigurationSettings.AppSettings["PrintToExcel"].ToString();
    }
}
