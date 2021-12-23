using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using log4net;

namespace TreasuryShadowSystem.Form
{
    public partial class frm_TR089DFMTM : System.Web.UI.Page
    {
        private string folder = Configuration.Config.Rpt089Folder;
        private static ILog Log = log4net.LogManager.GetLogger(typeof(frm_TR089DFMTM));
        DataTable dt = new DataTable();

        protected void Page_Init(object sender, EventArgs e)
        {
            GridData.RowCommand += new GridViewCommandEventHandler(GridData_RowCommand);
            GridData.PageIndexChanging += new GridViewPageEventHandler(GridData_PageIndexChanging);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            { 
            
            }
        }
        private void BindGrid()
        {
            try
            {
                TableStructure();
                string pattern = "*.*";
                if (txtTransDate.Value.Trim() != "")
                    pattern = "*_" + txtTransDate.Value.Substring(6) + txtTransDate.Value.Substring(3, 2) + txtTransDate.Value.Substring(0, 2) + "*.xls";
                FileInfo[] info = GetFiles(pattern, folder);
                AddFile(info);
                BindData();
            }
            catch (Exception ex)
            {

            }
        }
        private void TableStructure()
        {
            try
            {
                DataColumn dcID = new DataColumn("RowID");
                DataColumn dcName = new DataColumn("Name");
                DataColumn dcPathFile = new DataColumn("PathFile");

                dt.Columns.Add(dcID);
                dt.Columns.Add(dcName);
                dt.Columns.Add(dcPathFile);
            }
            catch (Exception ex)
            {

            }
        }

        private FileInfo[] GetFiles(string Pattern, string DirectoryPath)
        {
            try
            {
                //DirectoryInfo d = new DirectoryInfo(Server.MapPath(DirectoryPath)); // if absolute path ~/Files/xxx/
                DirectoryInfo d = new DirectoryInfo(DirectoryPath);
                FileInfo[] fl = d.GetFiles(Pattern,SearchOption.AllDirectories);
                return fl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void AddFile(FileInfo[] f)
        {
            try
            {
                for (int i = 0; i < f.Length; i++)
                { 
                    DataRow dr = dt.NewRow();
                    dr["RowID"] = i + 1;
                    dr["Name"] = f[i].Name;
                    dr["PathFile"] = f[i].FullName;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        void GridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                int row = Int32.Parse(e.CommandArgument.ToString());
                string fname = GridData.Rows[row].Cells[2].Text;
                if (e.CommandName == "XCEL")
                {
                    //ExportFile(folder + "/" + fname);                    
                    ExportFile(fname);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        void GridData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //throw new NotImplementedException();
            GridData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void ExportFile(string str)
        {
            try
            {
                //string result = Path.GetFileName(str);
                string result = str.Replace('\\','/');
                string[] f = result.Split('/');
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + f[f.Length-1]);
                //Response.TransmitFile(Server.MapPath(str)); //for absolute path
                Response.TransmitFile(result);
                Response.End();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void BindData()
        {
            try
            {
                GridData.DataSource = dt;
                GridData.DataBind();
            }
            catch (Exception ex)
            { 
            
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}
