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
    public class DoitacController : Controller
    {
        DAL_Doitac objSlideshow = new DAL_Doitac();
        // GET: Administrator/Slideshows
        public ActionResult DoitacGetList()
        {
            return View();
        }
        public ActionResult DoitacInsertList(int? DoitacID)
        {
            if (DoitacID == null)
                DoitacID = -1;

            var model = objSlideshow.GETALL(int.Parse(DoitacID.ToString()));

            return View(model);
        }
        [ValidateInput(false)]
        public bool SlideshowCategories(string data)
        {
            DTO_Doitac model = JsonConvert.DeserializeObject<DTO_Doitac>(data);

            string newsImage = model.Images;
            //
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/Doitac/" + newsImage)))
            {
                if (newsImage != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/Doitac/" + newsImage)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/Doitac"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + newsImage), Server.MapPath("~/Upload/Doitac/" + newsImage));
                        }
                    }
                    catch (Exception) { }
                }
            }
            //
            bool result = objSlideshow.UpdateNewsCategories(model);
            return result;
        }
        [HttpPost]
        public ActionResult SlideshowGetData()
        {
            var model = objSlideshow.GETALL(0);
            return PartialView(model);
        }
        [HttpPost]
        public bool SlideshowDelete(int SlideshowId)
        {
            var model = objSlideshow.NewsCategoryDelete(SlideshowId);
            return true;
        }
    }
}