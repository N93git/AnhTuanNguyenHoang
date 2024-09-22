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
    public class InformsController : Controller
    {
        DAL_Infors objInfor = new DAL_Infors();
        // GET: Administrator/Informs
        public ActionResult GetInfor()
        {
            var model = objInfor.GETALL();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public bool inforUpdates(string data)
        {
            DTO_Infors model = JsonConvert.DeserializeObject<DTO_Infors>(data);
            var result= objInfor.UpdateInfors(model);
            return result;
        }
    }
}