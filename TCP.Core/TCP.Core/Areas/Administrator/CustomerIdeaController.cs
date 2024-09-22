using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCP.DAL;
using TCP.DTO;
namespace TCP.Core.Areas.Administrator
{
    public class CustomerIdeaController : Controller
    {
        DAL_CustomerIdea objCustomeridea = new DAL_CustomerIdea();
        // GET: Administrator/CustomerIdea
        public ActionResult CustomerIdeaGetList()
        {
            var model = objCustomeridea.GETALL(0).ToList();
            return View(model);
        }
        public ActionResult CustomerIdeacInsertList(int? ID)
        {
            if (ID == null)
                ID = -1;

            var model = objCustomeridea.GETALL(int.Parse(ID.ToString()));

            return View(model);
        }
        [ValidateInput(false)]
        public bool UpdateCustomerIdea(string data)
        {
            DTO_CustomerIdea model = JsonConvert.DeserializeObject<DTO_CustomerIdea>(data);

            string newsImage = model.Images;
            //
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/CustomerIdea/" + newsImage)))
            {
                if (newsImage != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/CustomerIdea/" + newsImage)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/CustomerIdea"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + newsImage), Server.MapPath("~/Upload/CustomerIdea/" + newsImage));
                        }
                    }
                    catch (Exception) { }
                }
            }
            //
            bool result = objCustomeridea.UpdateCustomerIdea(model);
            return result;
        }

        [HttpPost]
        public bool CustomerDelete(int ID)
        {
            var model = objCustomeridea.CustomerIdeaDelete(ID);
            return true;
        }
    }
}