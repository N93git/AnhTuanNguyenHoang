using System;
using System.IO;
using System.Web;

namespace vitahr.Tool
{
    /// <summary>
    /// Summary description for DeleteFile
    /// </summary>
    public class DeleteFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string name= context.Request.QueryString["name"];
            string folder = context.Request.QueryString["folder"].ToString();
            //string folder = context.Request.QueryString["folder"];
            //string filename = context.Request.QueryString["filename"];
            //string kq = "0";
            try
            {
                if (File.Exists(context.Server.MapPath("~/Upload/" + folder + "/" + name)))
                {
                    File.Delete(context.Server.MapPath("~/Upload/" + folder + "/" + name));
                }
            }
            catch (Exception) { }
            //context.Response.ContentType = "text/plain";
            //context.Response.Write(kq);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}