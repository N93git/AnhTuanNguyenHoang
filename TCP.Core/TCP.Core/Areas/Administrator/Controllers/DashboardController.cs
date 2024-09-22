using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCP.DAL;
using Tool;
using TCP.DTO;
using Newtonsoft.Json;
namespace TCP.Core.Areas.Administrator.Controllers
{
    public class DashboardController : Controller
    {
        DAL_Login objlogin = new DAL_Login();
        // GET: Administrator/Dashboard
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public bool Login(string data)
        {
            string pass = Tool.Utils.DecryptMD5("admin123@456");
            bool isLogin = false;
            DTO_Login models = JsonConvert.DeserializeObject<DTO_Login>(data);
            models.Password = Tool.Utils.DecryptMD5(models.Password);
            IEnumerable<DTO_Login> tmp = objlogin.TCP_Logincheck(models.UserName, models.Password);
            if (tmp != null && tmp.Count() > 0)
            {
                isLogin = true;
                foreach (var it in tmp)
                {

                    HttpCookie userinfo1 = new HttpCookie("userinfoadmin2s");
                    userinfo1["username"] = it.UserName;
                    userinfo1["id"] = it.Id.ToString();
                    userinfo1.Expires = DateTime.Now.AddDays(365);
                    Response.Cookies.Add(userinfo1);
                }
            }
            return isLogin;
        }
        public void Logout()
        {
            var cokie = Request.Cookies["userinfoadmin2s"];
            cokie.Expires = DateTime.Now.AddDays(-366);
            cokie.Value = null;
            Response.Cookies.Add(cokie);
        }
    }
}