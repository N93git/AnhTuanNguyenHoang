using System;
using System.IO;
using System.Web;
using Tool;
namespace vitahr.Tool
{
    /// <summary>
    /// Summary description for UploadCKEditor
    /// </summary>
    public class UploadCKEditor : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile uploads = context.Request.Files["upload"];
            string curUrl = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/";
            string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
            string guidName = Guid.NewGuid().ToString().Substring(0, 4);
            string file = Utils.ConvertToUnSign(System.IO.Path.GetFileNameWithoutExtension(uploads.FileName)) + "-" + guidName + System.IO.Path.GetExtension(uploads.FileName);
            if (!Directory.Exists(context.Server.MapPath("~/Upload/Editor/" + curUrl)))
            {
                Directory.CreateDirectory(context.Server.MapPath("~/Upload/Editor/" + curUrl));
            }
            uploads.SaveAs(context.Server.MapPath("~/Upload/Editor/" + curUrl) + file);
            //provide direct URL here
            string url = "/Upload/Editor/" + curUrl + file;

            context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
            context.Response.End();
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