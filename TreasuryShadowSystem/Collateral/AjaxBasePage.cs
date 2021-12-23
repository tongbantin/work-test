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
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TreasuryShadowSystem
{
    public abstract class AjaxBasePage<T> : System.Web.UI.Page
    {
        protected virtual void Ok()
        {
            Response.Clear();
            Response.ContentType = "application/json";
            
            Message ok = new Message() { StatusCode = 200, Text = "Ok" };
            Response.Write(JsonConvert.SerializeObject(ok));
        }
        protected virtual void Oks(IList<T> models)
        {
            Response.Clear();
            Response.ContentType = "application/json";
            Response.Write(JsonConvert.SerializeObject(models));
        }
        protected virtual void Ok<V>(V model)
        {
            Response.Clear();
            Response.ContentType = "application/json";
            Response.Write(JsonConvert.SerializeObject(model));
        }
        protected virtual void NotFound(Exception ex)
        {
            Response.Clear();
            Response.ContentType = "application/json";
            Message notFound = new Message() { StatusCode = ex.GetHashCode(), Text = ex.Message };
            Response.Write(JsonConvert.SerializeObject(notFound));
        }
        protected virtual void OkStream(byte[] streams, string fileNameWithExtension)
        {
            string fileName = string.Format("attachment;filename={0}", fileNameWithExtension);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", fileName);
            Response.BinaryWrite(streams);
            Response.End();
        }
        protected virtual void OkPdf(byte[] streams, string fileNameWithExtension)
        {
            string fileName = string.Format("attachment;filename={0}", fileNameWithExtension);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", fileName);
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(streams);
            Response.End();
            Response.Close();
        }
    }

    public class Message
    {
        public int? StatusCode { get; set; }
        public string Text { get; set; }
        public object data { get; set; }

    }

}
