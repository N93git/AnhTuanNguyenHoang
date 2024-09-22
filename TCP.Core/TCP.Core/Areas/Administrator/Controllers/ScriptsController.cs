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
    public class ScriptsController : Controller
    {
        DAL_Scripts objDal_Productcate = new DAL_Scripts();
        // GET: Administrator/ProductCategoty
        // GET: Administrator/Scripts
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ScriptsGetData()
        {
            var model = objDal_Productcate.GETALL(0);
            return PartialView(model);
        }
        public ActionResult ScriptsInsertList(int? ScriptID)
        {
            if (ScriptID == null)
                ScriptID = -1;

            var model = objDal_Productcate.GETALL(int.Parse(ScriptID.ToString()));
           
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public bool TCP_Scipts_SAVE(string data)
        {
            DTO_Scripts model = JsonConvert.DeserializeObject<DTO_Scripts>(data);
            
            //
            bool result = objDal_Productcate.UpdateNewsCategories(model);
            return result;
        }
        public int ScriptsDelete(int ScriptsId)
        {
            var model = objDal_Productcate.ScriptDelete(ScriptsId);
            return model;
        }

    }
}