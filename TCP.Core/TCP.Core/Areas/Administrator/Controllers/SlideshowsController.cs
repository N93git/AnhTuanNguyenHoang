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
    public class SlideshowsController : Controller
    {
        DAL_Slideshow objSlideshow = new DAL_Slideshow();

        [HttpPost]
        public bool slideshowSort(int id, int idorder)
        {
            var result = objSlideshow.UpdateSlideshowOrder(id, idorder);
            return result;
        }
        // GET: Administrator/Slideshows
        public ActionResult SlideshowGetList()
        {
            return View();
        }
        public ActionResult SlideshowInsertList(int? SlideshowId)
        {
            if (SlideshowId == null)
                SlideshowId = -1;

            var model = objSlideshow.GETALL(int.Parse(SlideshowId.ToString()));
          
            return View(model);
        }
        [ValidateInput(false)]
        public bool SlideshowCategories(string data)
        {
            DTO_Slideshows model = JsonConvert.DeserializeObject<DTO_Slideshows>(data);
           
            string newsImage = model.Images;
            //
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/Slideshow/" + newsImage)))
            {
                if (newsImage != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/Slideshow/" + newsImage)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/Slideshow"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + newsImage), Server.MapPath("~/Upload/Slideshow/" + newsImage));
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