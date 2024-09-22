using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Configuration;
using Tool;
namespace vitahr.Tool
{
    /// <summary>
    /// Summary description for Upload
    /// </summary>
    public class Upload : IHttpHandler
    {

        public class FilesStatus
        {
            public string uploadPath { get; set; }
            public string fileName { get; set; }
            public string guidName { get; set; }
            public int size { get; set; }
            public string error { get; set; }
            public string progress { get; set; }
        }

        public string KeyGui { get; set; }
        private string strFileNameGuide = "";
        string AttachDir = string.Empty;
        private readonly JavaScriptSerializer js = new JavaScriptSerializer();
        private List<string> lstExtAllow = ConfigHelper.GetImageExtenionAllow();
        private int intResizeWidth
        {
            get
            {
                object objW = ConfigurationManager.AppSettings["ResizeDimension_Width"];
                if (objW == null) return 1140;
                else
                    return Convert.ToInt32(objW);
            }
        }
        private int intResizeHeight
        {
            get
            {
                object objH = ConfigurationManager.AppSettings["ResizeDimension_Height"];
                if (objH == null) return 600;
                else
                    return Convert.ToInt32(objH);
            }
        }

        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request["fileName"];

            var r = context.Response;
            r.AddHeader("Pragma", "no-cache");
            r.AddHeader("Cache-Control", "private, no-cache");

            AttachDir = ConfigHelper.GetTempFolder();
            AttachDir = context.Server.MapPath(AttachDir);
            // nếu chưa có folder, tạo folder
            if (!Directory.Exists(AttachDir))
            {
                Directory.CreateDirectory(AttachDir);
            }
            UploadFile(context);
        }

        private void UploadFile(HttpContext context)
        {
            KeyGui = Guid.NewGuid().ToString();
            var statuses = new List<FilesStatus>();
            var headers = context.Request.Headers;
            UploadWholeFile(context, statuses);
            WriteJsonIframeSafe(context, statuses);
        }
        private string GetfileToSave(string filename,int countFile)
        {
            string strExtension = Path.GetExtension(filename).ToLower();
            string strFTail = "";
            //if(countFile>1)
            //{
                strFTail = "-" + countFile.ToString();
           // }
            File.Move(AttachDir + "\\" + filename, AttachDir + "\\" + Utils.ConvertToUnSign(Path.GetFileNameWithoutExtension(filename)) + strFTail + strExtension);
            return Utils.ConvertToUnSign(Path.GetFileNameWithoutExtension(filename))+ strFTail + strExtension;

        }
        private void UploadWholeFile(HttpContext context, List<FilesStatus> statuses)
        {
            int countFile = 1;
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                var file = context.Request.Files[i];
                string filename = Path.GetFileName(file.FileName);
                string filenamenotex = Utils.ConvertToUnSign(Path.GetFileNameWithoutExtension(file.FileName));
                string fileExt = Path.GetExtension(file.FileName);
                // đếm số file có tên bắt đầu giống với tên file.
                var lstFileInFolder = Directory.GetFiles(AttachDir);
                if(lstFileInFolder != null)
                {
                    var lstFileExist = lstFileInFolder.Where(m => m.StartsWith(AttachDir + "\\" + filenamenotex));
                    if(lstFileExist != null)
                        countFile = lstFileExist.Count() + 1;
                }
                // save vào folder temp
                string strTempPath = AttachDir + "\\" + filenamenotex + "-" + countFile + fileExt;
                //string strTempPath = AttachDir + "\\" + filenamenotex +  fileExt;
                file.SaveAs(strTempPath);
                strFileNameGuide = filenamenotex + "-" + countFile + fileExt;
                //strFileNameGuide = filenamenotex  + fileExt;
                statuses.Add(new FilesStatus
                {
                    uploadPath = ConfigHelper.GetTempFolder() + "/" + strFileNameGuide,
                    fileName = filename + "-" + countFile + fileExt,
                    size = (int)(new FileInfo(AttachDir + "\\" + strFileNameGuide)).Length,
                    guidName = strFileNameGuide,
                    progress = "1.0"
                });
            }
        }

        private void WriteJsonIframeSafe(HttpContext context, List<FilesStatus> statuses)
        {
            context.Response.AddHeader("Vary", "Accept");
            try
            {
                if (context.Request["HTTP_ACCEPT"].Contains("application/json"))
                {
                    context.Response.ContentType = "application/json";
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                }
            }
            catch
            {
                context.Response.ContentType = "text/plain";
            }
            var jsonObj = js.Serialize(statuses.ToArray());
            context.Response.Write(jsonObj);
        }

        public class ConfigHelper
        {
            public const string SERVICE_DISPLAY_CONTENTS = "Content";
            public const string SERVICE_DISPLAY_CUSTOMERS = "Customer";
            public const string SERVICE_DISPLAY_TEMPLATES = "Template";

            public const string SITEMAP_CACHE_KEY = "POPCORN_SITEMAP_DATA";

            public static string GetEditorFolder()
            {
                return "/Upload/Editor/";
            }
            public static string GetNewsFolder()
            {
                return "/Upload/News/";
            }
            public static string GetCustomerFolder()
            {
                return "/Upload/Customer/";
            }

            public static string GetTemplateFolder()
            {
                return "/Upload/Template/";
            }

            public static string GetTempFolder()
            {
                return "/Upload/Temp";
            }
            public static string GetUserImage()
            {
                return "/Upload/UserImage/";
            }
            public static List<string> GetImageExtenionAllow()
            {
                string temp = ".jpg;.png;.bmp;.gif";
                return temp.Split(';').ToList();
            }
            //public static ImageCodecInfo GetEncoderInfo(ImageFormat format)
            //{
            //    return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
            //}
            public static int GetPageSize()
            {
                return 20;
            }
        }
    }
}