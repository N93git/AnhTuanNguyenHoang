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
    public class ContactsController : Controller
    {
        DAL_Contacts objContact = new DAL_Contacts();
        // GET: Administrator/Contacts
        public ActionResult ContactGetList()
        {
            return View();
        }
        public ActionResult ContactGetData()
        {
            var model = objContact.GETALL();
            return PartialView(model);
        }
        public ActionResult DatLichGetList()
        {
            var model = objContact.GETALLDatLich();
            return View(model);
        }
    }
}