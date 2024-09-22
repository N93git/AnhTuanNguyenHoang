using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCP.DAL;
using TCP.DTO;
using Tool;
namespace TCP.Core.Areas.Administrator.Controllers
{
    public class GiathamkhaoController : Controller
    {
        DAL_Giathamkhao objgtk = new DAL_Giathamkhao();
        // GET: Administrator/Giathamkhao
        public ActionResult Getlistthamkhao(int ProductId)
        {
            var model = objgtk.GETALL(0, ProductId).ToList();
            ViewBag.ProductId = ProductId;
            return View(model);
        }
        public ActionResult GiathamkhaoInsertList(int? Id,int ProductId)
        {
            if (Id == 0)
                Id = -1;
            ViewBag.ProductId = ProductId;
            var model = objgtk.GETALL(int.Parse(Id.ToString()), ProductId).ToList();
            if (model.Count()>0)
            {
                int thang = model.FirstOrDefault().CreateDate.Month;
                string thangs = "";
                if (thang < 10)
                    thangs = "0" + model.FirstOrDefault().CreateDate.Month.ToString();
                else
                    thangs = model.FirstOrDefault().CreateDate.Month.ToString();
                ViewBag.Date = model.FirstOrDefault().CreateDate.Year + "-" + thangs + "-" + model.FirstOrDefault().CreateDate.Day;
            }
            return View(model);
        }
        public bool GiathamkhaoUpdate(string data)
        {
            DTO_Giathamkhao model = JsonConvert.DeserializeObject<DTO_Giathamkhao>(data);

            //
            bool result = objgtk.UpdateNewsCategories(model);
            return result;
        }
        [HttpPost]
        public bool GiathamkhaoDelete(int Id)
        {
            var model = objgtk.GiathamkhaoDelete(Id);
            return true;
        }
    }
}