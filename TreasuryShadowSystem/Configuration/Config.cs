using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using KKB.Treasury.TreasuryCommon.Common;
using log4net;

namespace TreasuryShadowSystem.Configuration
{
    public class Config
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(Config));

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ToString();
        public static string PageTitle = ConfigurationManager.AppSettings["PageTitle"].ToString();
        public static string LdapConnection = ConfigurationManager.AppSettings["LdapConnection"].ToString();
        public static string Owner = ConfigurationManager.AppSettings["Owner"].ToString();
        public static string Website = ConfigurationManager.AppSettings["Website"].ToString();
        public static string Rpt089Folder = ConfigurationManager.AppSettings["Rpt089Folder"].ToString(); 
        public static string ConstantParameter = ConfigurationManager.AppSettings["ConstantParameter"].ToString();
        public static string MTMExportPath = ConfigurationManager.AppSettings["MTMExportPath"].ToString();
        public static string TDSConnectionString = ConfigurationManager.ConnectionStrings["TDSConnectionString"].ToString();

        public static string GetParameterFile(string Param)
        {
                DataSet s = null;
                Oracle o = Oracle.getInstance(ConnectionString);
                try
                {
                    string sql = "Select Value From OPICINF.KKB_TB_WEBPARAM WHERE NAME='" + Param + "'";
                    o.openConnection();
                    o.createCommand(sql, CommandType.Text);
                    return o.executeScalar().ToString(); 
                    //Log.Info(s.GetXml());
                }
                catch (Exception ex)
                {
                    Log.Error("-----Model_MS_Customer Error-----");
                    Log.Error(ex.Message);
                    return null;
                }
                finally
                {
                    Log.Info("-----Model_MS_Customer Finish-----");
                    o.closeConnection();
                }

                /*
                using (StreamReader reader = new StreamReader(Config.ConstantParameter))
                {
                    string line;
                    while((line = reader.ReadLine())!=null)
                    {
                        string[] part = line.Split('=');
                        if (part[0].Equals(Param))
                            return part[1];
                    }
                }
                return "";
                */
        }

        public static bool SetParameterFile(string Param, string Value)
        {
            try
            {

                DataSet s = null;
                Oracle o = Oracle.getInstance(ConnectionString);
                try
                {
                    string sql = "Update OPICINF.KKB_TB_WEBPARAM Set VALUE='" + Value + "' WHERE NAME='" + Param + "'";
                    o.openConnection();
                    o.createCommand(sql, CommandType.Text);
                    if (o.executeNonQuery() > 0)
                        return true;
                    else
                        return false;
                    //Log.Info(s.GetXml());
                }
                catch (Exception ex)
                {
                    Log.Error("-----Model_MS_WEBPARAM Error-----");
                    Log.Error(ex.Message);
                    return false;
                }
                finally
                {
                    Log.Info("-----Model_MS_WEBPARAM Finish-----");
                    o.closeConnection();
                }
                /*
                File.Copy(Config.ConstantParameter, Config.ConstantParameter + DateTime.Now.ToString("yyyyMMddHHmmss"), true);
                string[] str = File.ReadAllLines(Config.ConstantParameter);                
                using (StreamWriter writer = new StreamWriter(Config.ConstantParameter, false))
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        string[] part = str[i].Split('=');
                        if (part[0].Equals(Param))
                            writer.WriteLine(part[0] + "=" + Value);
                        else
                            writer.WriteLine(part[0] + "=" + part[1]);
                    }
                }*/
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
