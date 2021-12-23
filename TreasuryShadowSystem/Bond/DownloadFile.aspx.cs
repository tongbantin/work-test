using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace TreasuryShadowSystem.Bond
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string str = Request.QueryString[""];
                string result = Path.GetFileName(str);
                Response.ContentType = "application/zip";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Session["FolderName"].ToString() + ".zip");
                Response.TransmitFile(Server.MapPath(str));
                Response.End();                
            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}
