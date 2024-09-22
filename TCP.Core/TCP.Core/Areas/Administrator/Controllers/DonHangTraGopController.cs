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
    public class DonHangTraGopController : Controller
    {
        DAL_Contacts objContact = new DAL_Contacts();
        // GET: Administrator/DonHangTraGop
        public ActionResult DonHangTraGopById(int ID)
        {
            var model = objContact.GETALLDonHangTraGop(ID).ToList();
            return View(model);
        }
        public ActionResult DonHangTraGop()
        {
            var model = objContact.GETALLDonHangTraGop(0).ToList();
            return View(model);
        }
        public void UpdateDonHangTraGop(int ID)
        {
            objContact.DonHangTraGop_Duyet(ID);
        }
    }
}