using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TCP.Core
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //lượt truy cập
            Application["HomNay"] = 0;
            Application["HomQua"] = 0;
            Application["TuanNay"] = 0;
            Application["TuanTruoc"] = 0;
            Application["ThangNay"] = 0;
            Application["ThangTruoc"] = 0;
            Application["TatCa"] = 0;
            Application["visitors_online"] = 0;
            //end
        }

        public void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["visitors_online"] = Convert.ToInt32(Application["visitors_online"]) + 1;
            Application.UnLock();

            try
            {
                var tmp = new TCP.DAL.SupportBLL().GetLuotTruyCap();
                if (tmp != null)
                {
                    foreach (var it in tmp)
                    {
                        Application["HomNay"] = int.Parse("0" + it.HomNay).ToString("#,###");
                        Application["HomQua"] = int.Parse("0" + it.HomQua).ToString("#,###");
                        Application["TuanNay"] = int.Parse("0" + it.TuanNay).ToString("#,###");
                        Application["TuanTruoc"] = int.Parse("0" + it.TuanTruoc).ToString("#,###");
                        Application["ThangNay"] = int.Parse("0" + it.ThangNay).ToString("#,###");
                        Application["ThangTruoc"] = int.Parse("0" + it.ThangTruoc).ToString("#,###");
                        Application["TatCa"] = int.Parse("0" + it.TatCa).ToString("#,###");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["visitors_online"] = Convert.ToInt32(Application["visitors_online"]) - 1;
            Application.UnLock();
        }
    }
}
