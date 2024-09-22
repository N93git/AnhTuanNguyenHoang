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
    public class SizesController : Controller
    {
        DAL_Sizes objSize = new DAL_Sizes();
        // GET: Administrator/Sizes
        public ActionResult SzieGetList()
        {
            var model = objSize.GETALL(0).ToList();
            return View(model);
        }
        public ActionResult SizesInsertList(int? ID)
        {
            if (ID == null)
                ID = -1;

            var model = objSize.GETALL(int.Parse(ID.ToString())).ToList();
            return View(model);
        }
        public int UpdateSize(string data)
        {
            DTO_Sizes model = JsonConvert.DeserializeObject<DTO_Sizes>(data);
            objSize.UpdateColors(model);
            return 1;
        }
        public int DeleteSizes(int ID)
        {
            objSize.SizesDelete(ID);
            return 1;
        }
    }
}