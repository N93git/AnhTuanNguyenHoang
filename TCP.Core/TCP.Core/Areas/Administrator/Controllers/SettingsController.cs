using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCP.DAL;
using TCP.DTO;
using Tool;

namespace TCP.Core.Areas.Administrator.Controllers
{
    public class SettingsController : Controller
    {
        DAL_Settings objDal_Productcate = new DAL_Settings();
        // GET: Administrator/Settings
        public ActionResult Index()
        {
            var model = objDal_Productcate.GETALL(0);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public bool TCP_Setting_SAVE(string data)
        {
            DTO_Settings model = JsonConvert.DeserializeObject<DTO_Settings>(data);

            string logo = model.Logo;
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/Settings/" + logo)))
            {
                if (logo != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/Settings/" + logo)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/Settings"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + logo), Server.MapPath("~/Upload/Settings/" + logo));
                        }
                    }
                    catch (Exception) { }
                }
            }
            logo = model.Favicon;
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/Settings/" + logo)))
            {
                if (logo != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/Settings/" + logo)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/Settings"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + logo), Server.MapPath("~/Upload/Settings/" + logo));
                        }
                    }
                    catch (Exception) { }
                }
            }
            //
            logo = model.Banner1;
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/Settings/" + logo)))
            {
                if (logo != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/Settings/" + logo)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/Settings"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + logo), Server.MapPath("~/Upload/Settings/" + logo));
                        }
                    }
                    catch (Exception) { }
                }
            }
            logo = model.Banner2;
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/Settings/" + logo)))
            {
                if (logo != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/Settings/" + logo)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/Settings"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + logo), Server.MapPath("~/Upload/Settings/" + logo));
                        }
                    }
                    catch (Exception) { }
                }
            }
            bool result = objDal_Productcate.UpdateSettings(model);
            return result;
        }

    }
}