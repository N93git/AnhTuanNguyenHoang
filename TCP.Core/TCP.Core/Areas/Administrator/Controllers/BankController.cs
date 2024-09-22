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
    public class BankController : Controller
    {
        DAL_Bank objBank = new DAL_Bank();
        // GET: Administrator/Bank
        public ActionResult BankGetList()
        {
            var model = objBank.GETALL(0).ToList();
            return View(model);
        }
        public ActionResult BankInsertList(int? ID)
        {
            if (ID == null)
                ID = -1;

            var model = objBank.GETALL(int.Parse(ID.ToString())).ToList();
            return View(model);
        }
        public int UpdateColors(string data)
        {
            DTO_Bank model = JsonConvert.DeserializeObject<DTO_Bank>(data);
            objBank.UpdateBank(model);
            //    string newsImage = model.NewsCategoryImage;
            //
            string newsImage = model.Images;
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/Bank/" + newsImage)))
            {
                if (newsImage != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/Bank/" + newsImage)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/Bank"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + newsImage), Server.MapPath("~/Upload/Bank/" + newsImage));
                        }
                    }
                    catch (Exception) { }
                }
            }
            //
            return 1;
        }
        public int DeleteColors(int ID)
        {
            objBank.BankDelete(ID);
            return 1;
        }
    }
}