using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
using System.IO;
using TCP.DAL;
namespace TCP.Core.Areas.Administrator.Controllers
{
    public class AjaxController : Controller
    {
        public DAL_Menus objMenu = new DAL_Menus();

       [HttpPost]
       public string ConvertToUnSign(string url)
        {
            string geturl = Utils.ConvertToUnSign(url);
            return geturl;
        }


        public ActionResult GetSeoBox(int relatedId,int templateid)
        {
            var model = objMenu.GETALL(relatedId, templateid);
            return PartialView(model);
        }
      
    }
}