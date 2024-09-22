using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TCP.DAL;
using TCP.DTO;
namespace TCP.Core.Controllers
{
    public class PatialController : Controller
    {
        DAL_Menus objMenuBLL = new DAL_Menus();
        DAL_Scripts objscript = new DAL_Scripts();
        DAL_NewsCategories objnewsCategory = new DAL_NewsCategories();
        DAL_Products objproduct = new DAL_Products();
        DAL_CustomerComment objcuscomment = new DAL_CustomerComment();
        DAL_CustomerComment objcus = new DAL_CustomerComment();
        DAL_CustomerCommentNews objnewcom = new DAL_CustomerCommentNews();
        public string GetSitemapDocument(IEnumerable<DTO_Menus> sitemapNodes)
        {
          
            XNamespace xmlns = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");
            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            XNamespace schemaLocation = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");
            XElement root = new XElement(xmlns + "urlset",
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "schemaLocation", schemaLocation)
            );
            foreach (DTO_Menus sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(xmlns + "url",
                new XElement(xmlns + "loc", (Request.Url.GetLeftPart(UriPartial.Authority)) + (sitemapNode.Url == "/" ? "" : "/") + Uri.EscapeUriString(sitemapNode.Url)),
                sitemapNode.CreatedOn == null ? null : new XElement(
                xmlns + "lastmod",
                sitemapNode.CreatedOn.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                sitemapNode.Priority == null ? null : new XElement(
                xmlns + "priority",
                sitemapNode.Priority.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }
            XDocument document = new XDocument(root);
            return document.ToString();
        }

        [Route("sitemap.xml", Name = "GetSitemapText"), OutputCache(Duration = 3600)]
        public ActionResult SitemapXml()
        {
            IEnumerable<DTO_Menus> sitemapNodes = objMenuBLL.GetSiteMap("administrator");
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "text/xml", Encoding.UTF8);
        }
        [Route("robots.txt", Name = "GetRobotsText"), OutputCache(Duration = 86400)]
        public ContentResult RobotsText()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("User-Agent: *");
            stringBuilder.AppendLine("disallow: /admin/userlogin");
            stringBuilder.AppendLine("Allow: /");
            stringBuilder.Append("Sitemap: " + Request.Url.GetLeftPart(UriPartial.Authority) + "/sitemap.xml");

            return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }
        // GET: Patial
        DAL_News objNews= new DAL_News();
        public ActionResult GetNewsDetail(int ParentID)
        {
            var model = objNews.GETALL("", 0,0, 1, 100).ToList().Where(m=>m.ParentID.Contains(ParentID.ToString()));

            return PartialView(model);
        }
        public ActionResult GetMenuChild(int ParentID)
        {
            var model = objNews.GETALL("", 0, 0, 1, 100).ToList().Where(m => m.ParentID.Contains(ParentID.ToString()));

            return PartialView(model);
        }
        //GetMenuChildMobile
        public ActionResult GetMenuChildMobile(int ParentID)
        {
            var model = objNews.GETALL("", 0, 0, 1, 100).ToList().Where(m => m.ParentID.Contains(ParentID.ToString()));

            return PartialView(model);
        }
        public ActionResult Getscript(int type)
        {
            var model = objscript.GETALL(0).Where(m=>m.Position== type).Where(m=>!m.Name.ToLower().Contains("trang chủ")).ToList();
            return PartialView(model);
        }
        public int KTTrungma(string id, DataTable dtb,string color,string size)
        {
            int indexofrow = -1;
            for (int datarow = 0; datarow < dtb.Rows.Count; datarow++)
            {
                if ((dtb.Rows[datarow]["ProductId"].ToString() == id && dtb.Rows[datarow]["color"].ToString() == color && dtb.Rows[datarow]["size"].ToString() == size))
                {
                    indexofrow = datarow;
                    break;
                }
            }
            return indexofrow;
        }
        [HttpPost]
        public void AddDatabaleOrder(int ProductId, string ProductTitle, float ProductPrice, string ProductImage,int Amount,string color,string size)
        {
            DataTable dtb;
            if (Session["giohang"] == null)
            {
                dtb = new DataTable();
                dtb.Columns.Add("ProductId");
                dtb.Columns.Add("ProductTitle");
                dtb.Columns.Add("ProductPrice", typeof(double));
                dtb.Columns.Add("Amount", typeof(int));
                dtb.Columns.Add("ProductImage");
                dtb.Columns.Add("color");
                dtb.Columns.Add("size");
                dtb.Columns.Add("Total", typeof(decimal), "Amount*ProductPrice");
            }
            else
            {
                dtb = (DataTable)Session["giohang"];
            }
            int indexofcol = KTTrungma(ProductId.ToString(), dtb,color,size);
            if (indexofcol != -1)
            {
                dtb.Rows[indexofcol]["Amount"] = Convert.ToInt32(dtb.Rows[indexofcol]["Amount"]) + Amount;
            }
            else
            {
                DataRow dr = dtb.NewRow();
                dr["ProductId"] = ProductId;
                dr["ProductTitle"] = ProductTitle;
                dr["ProductPrice"] = ProductPrice;
                dr["Amount"] = Amount;
                dr["ProductImage"] = ProductImage;
                dr["color"] = color;
                dr["size"] = size;
                dtb.Rows.Add(dr);
            }
            Session["giohang"] = dtb;
        }
        [HttpPost]
        public int CountShopCart()
        {
            int totals = 0;
            DataTable dtb = (DataTable)Session["giohang"];
            foreach (DataRow item in dtb.Rows)
            {
                totals += int.Parse(item["Amount"].ToString());
            }
            return totals;
        }
        [HttpPost]
        public decimal UpdateShopCart(int num, int val)
        {
            string s = "";
            decimal totals = 0;
            try
            {
                int numstt = num;
                DataTable dtb = (DataTable)Session["giohang"];
                dtb.Rows[numstt]["Amount"] = val;
                s = "1";
                foreach (DataRow item in dtb.Rows)
                {
                    totals += decimal.Parse(item["Total"].ToString());
                }
            }
            catch (Exception)
            {
                s = "0";
            }
        
           
            return totals;
        }
        [HttpPost]
        public float DeleteShopCart(int _id)
        {
            float totals = 0;
            DataTable dt = (DataTable)Session["giohang"];
            dt.Rows.RemoveAt(_id);
            foreach (DataRow item in dt.Rows)
            {
                totals += float.Parse(item["Total"].ToString());
            }
            return totals;
        }


        [HttpPost]
        public ActionResult LoadGioHang()
        {
            float totals = 0;
            DataTable dtb = (DataTable)Session["giohang"];
            ViewBag.dt = dtb;
           
            return PartialView();
        }

        public ActionResult Dichvucon(int ParentID)
        {
            var model = objNews.GETALL("", 0, 1, 1, 100).Where(m => Tool.Utils.CheckSplit(m.ParentID, ParentID)).ToList();

            return PartialView(model);
        }
        public ActionResult BoxItem(int ParentID)
        {
            var model = objproduct.GETALL("", 0, 0, 1, 1).Where(m => m.IsShow == 1).Where(m => Tool.Utils.CheckSplit(m.ParentID, ParentID) || ParentID == 0).OrderByDescending(m => m.DisplayOrder).ToList();
            return PartialView(model);
        }
        public int updatenumcart()
        {
            int numm = 0;
            DataTable dt = (DataTable)Session["giohang"];
            if (dt != null)
            {   
                foreach (DataRow item in dt.Rows)
                {
                    numm += int.Parse(item["Amount"].ToString());
                }
            }
            return numm;
        }
        public int CustomerComtentSave(int CustomerId,string Name,string Email,string Phone)
        {
            DTO_CustomerComment cusc = new DTO_CustomerComment();
            cusc.CustomerId = CustomerId;
            cusc.Name = Name;
            cusc.Email = Email;
            cusc.Phone = Phone;
            objcuscomment.UpdateNewsCategories(cusc);
            var cusid = objcuscomment.GETALL(0).LastOrDefault().CustomerId;
            //

            HttpCookie userinfo1 = new HttpCookie("commentlogin");
            userinfo1["Name"] = Name;
            userinfo1["CustomerId"] = cusid.ToString();
            userinfo1.Expires = DateTime.Now.AddDays(365);
            Response.Cookies.Add(userinfo1);
            //Lấy Id customer comment mới nhất
            var lastcustomerid = objcuscomment.GETALL(0).LastOrDefault().CustomerId;
            //Gửi Email
            return lastcustomerid;
        }
        public ActionResult GetTraloi(int CommtentId)
        {
            //var model = objcuscomment.GETALL2(0).Where(m => m.ParentID == CommtentId).ToList();
            var models = (from cm in objcuscomment.GETALL2(0).ToList()
                          join p in objproduct.GETALL("", 0, 1, 0, 100).ToList()
                          on cm.ProductId equals p.ProductId
                          join cus in objcus.GETALL(0).ToList() 
                          on cm.CustomerId equals cus.CustomerId into lc
                          from lcs in lc.DefaultIfEmpty()
                          where cm.ParentID == CommtentId
                          && cm.Duyet==1
                          select new CustomerComments()
                          {
                              CommtentId = cm.CommtentId,
                              Contents = cm.Contents,
                              CustomerName = lcs!=null? lcs.Name:"",
                              Duyet = cm.Duyet,
                              Reply = cm.Reply,
                              ProductName = p.ProductTitle,
                              CreateDate = cm.CreateDate,
                              ProductId = cm.ProductId,
                              Type=cm.Type,
                              ParentID=cm.ParentID
                          }
                     ).ToList();
            return PartialView(models);
        }
        public ActionResult GetTraloiNews(int CommtentId)
        {
            //var model = objcuscomment.GETALL2(0).Where(m => m.ParentID == CommtentId).ToList();
            var models = (from cm in objnewcom.GETALL(0).ToList()
                          join p in objNews.GETALL("", 0, 1, 0, 100).ToList()
                          on cm.NewsID equals p.NewsId
                          join cus in objcus.GETALL(0).ToList()
                          on cm.CustomerId equals cus.CustomerId into lc
                          from lcs in lc.DefaultIfEmpty()
                          where cm.ParentID == CommtentId
                          && cm.Duyet == 1
                          select new CustomerComments()
                          {
                              CommtentId = cm.CommtentId,
                              Contents = cm.Contents,
                              CustomerName =cm.HoTen,
                              Duyet = cm.Duyet,
                              Reply = cm.Reply,
                              ProductName = p.NewsTitle,
                              CreateDate = cm.CreateDate,
                              ProductId = cm.NewsID,
                              Type = cm.Type,
                              ParentID = cm.ParentID
                          }
                     ).ToList();
            return PartialView(models);
        }
    }
}