using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using log4net;
namespace KKB.Treasury.TreasuryModel.Model
{
    public class FileManage
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(FileManage));
        protected FileManage() { }
        public static DataTable GetListFile(string DirectoryPath, string Path, string Filter)
        { 
            try
            {
                DirectoryInfo dir = new DirectoryInfo(DirectoryPath);
                DirectoryInfo[] subdir = dir.GetDirectories(); 
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Filename"));
                dt.Columns.Add(new DataColumn("Path"));

                if (subdir.Length > 0)
                {
                    for (int j = 0; j < subdir.Length; j++)
                    {
                        FileInfo[] file = subdir[j].GetFiles();
                        List<FileInfo> lfile = new List<FileInfo>();
                        if (!Filter.Equals(""))
                        {
                            lfile = file.Where(t => t.Name.Contains(Filter) == true).ToList();
                        }
                        else
                        {
                            lfile = file.ToList<FileInfo>();
                        }

                        for (int i = 0; i < lfile.Count; i++)
                        {
                            FileInfo f = lfile[i];
                            DataRow dr = dt.NewRow();
                            dr["Filename"] = f.Name;
                            dr["Path"] = Path + "/" + subdir[j].Name;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else {
                    FileInfo[] file = dir.GetFiles();
                    List<FileInfo> lfile = new List<FileInfo>();
                    if (!Filter.Equals(""))
                        lfile = file.Where(t => t.Name.Contains(Filter) == true).ToList();
                    else
                        lfile = file.ToList<FileInfo>();
                    for (int i = 0; i < lfile.Count; i++)
                    {
                        FileInfo f = lfile[i];
                        DataRow dr = dt.NewRow();
                        dr["Filename"] = f.Name;
                        dr["Path"] = Path;
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
        public static DataTable GetListFileRoot(string DirectoryPath, string Path, string Filter)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(DirectoryPath);
                DirectoryInfo[] subdir = dir.GetDirectories();
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Filename"));
                dt.Columns.Add(new DataColumn("Path"));

                FileInfo[] file = dir.GetFiles();
                List<FileInfo> lfile = new List<FileInfo>();
                if (!Filter.Equals(""))
                    lfile = file.Where(t => t.Name.Contains(Filter) == true).ToList();
                else
                    lfile = file.ToList<FileInfo>();
                for (int i = 0; i < lfile.Count; i++)
                {
                    FileInfo f = lfile[i];
                    DataRow dr = dt.NewRow();
                    dr["Filename"] = f.Name;
                    dr["Path"] = Path;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }
        public static bool ClearDirectory(string Path, bool Create) {
            try
            {
                string ExportPath = Path + "/";
                if (Directory.Exists(ExportPath))
                {
                    string[] f = Directory.GetFiles(ExportPath);
                    for (int l = 0; l < f.Length; l++)
                    {
                        File.Delete(f[l]);
                    }
                    Directory.Delete(ExportPath);
                }
                if (Create)
                    Directory.CreateDirectory(ExportPath);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return false;
            }
        }

        public static bool ManageDirectory(string Path)
        {
            try
            {
                string ExportPath = Path + "/";
                if (!Directory.Exists(ExportPath))
                {
                    Directory.CreateDirectory(ExportPath);
                }
               
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.InnerException);
                return false;
            }
        }
    }
}
