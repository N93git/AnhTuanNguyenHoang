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
    public class ColorsController : Controller
    {
        DAL_Colors objColor = new DAL_Colors();
        // GET: Administrator/Colors
        public ActionResult ColorGetList()
        {
            var model = objColor.GETALL(0).ToList();
            return View(model);
        }
        public ActionResult ColorsInsertList(int? ID)
        {
            if (ID == null)
                ID = -1;

            var model = objColor.GETALL(int.Parse(ID.ToString())).ToList();
            return View(model);
        }
        public int UpdateColors(string data)
        {
            DTO_Colors model = JsonConvert.DeserializeObject<DTO_Colors>(data);
            objColor.UpdateColors(model);
            return 1;
        }
        public int DeleteColors(int ID)
        {
            objColor.ColorsDelete(ID);
            return 1;
        }
    }
}