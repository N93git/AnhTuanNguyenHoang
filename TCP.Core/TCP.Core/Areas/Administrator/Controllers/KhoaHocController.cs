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
    public class KhoaHocController : Controller
    {
        DAL_KhoaHoc objkhoahoc = new DAL_KhoaHoc();
        // GET: Administrator/KhoaHoc
        public ActionResult LoadListKhoaHoc()
        {
            var model = objkhoahoc.GetAll();
            return View(model);
        }
    }
}