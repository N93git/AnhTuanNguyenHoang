using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCP.DAL;
using TCP.DTO;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TCP.Core.Controllers
{

    public class HomeController : Controller
    {
        DAL_Scripts objscript = new DAL_Scripts();
        #region Khai báo
        private int phanTrangSP = 20;
        private int phantrangSearch =15;
        private int phanTrangTT = 12;
        DAL_NewsCategories objnewsCategory = new DAL_NewsCategories();
        DAL_Slideshow objslide = new DAL_Slideshow();
        DAL_News objnews = new DAL_News();
        DAL_Menus _objmenu = new DAL_Menus();
        DAL_Contacts objcontact = new DAL_Contacts();
        DAL_Products objproduct = new DAL_Products();
        DAL_ProductCategories objprodcate = new DAL_ProductCategories();
        DAL_Orders objorder = new DAL_Orders();
        //Infor
        DAL_Infors objInfo = new DAL_Infors();
        DAL_Orders objOrder = new DAL_Orders();
        DAL_Doitac objDoitac = new DAL_Doitac();
        DAL_KhoaHoc objKhoahoc = new DAL_KhoaHoc();
        DAL_UserLogin objUserlogin = new DAL_UserLogin();
        DAL_CustomerIdea objCustomerIdea = new DAL_CustomerIdea();
        DAL_Giathamkhao objgtk = new DAL_Giathamkhao();
        DAL_CustomerComment objcomm = new DAL_CustomerComment();
        DAL_CustomerComment objcus = new DAL_CustomerComment();
        DAL_CustomerCommentNews objnewcom=new DAL_CustomerCommentNews();
        DAL_Bank objBank = new DAL_Bank();
        #endregion
        public ActionResult ThankYou()
        {
            return View();
        }
        public ActionResult Page404()
        {
            return View();
        }
        DAL_ProductColors objProdColor = new DAL_ProductColors();
        DAL_Colors objcolor = new DAL_Colors();
        DAL_Sizes objsize = new DAL_Sizes();
        DAL_Datlich objDatlich = new DAL_Datlich();
        public ActionResult ProductcateMenuchild(int ParentID)
        {
            var model = objprodcate.GETALL("", ParentID, 0, -1).ToList();
            return PartialView(model);
        }
        public ActionResult ProductcateMenu(int ParentID)
        {
            var model = objprodcate.GETALL("", ParentID, 0, -1).ToList();
            return PartialView(model);
        }
        public ActionResult GetBreadcumsProduct(string parentid)
        {
            var getstring = parentid.Split(',');
            List<DTO_ProductCategories> listCate = new List<DTO_ProductCategories>();
            foreach(var item in getstring)
            {
                var it = objprodcate.GETALL("", -1, int.Parse(item), -1).FirstOrDefault();
                listCate.Add(it);
            }
            return PartialView(listCate);
        }
        public ActionResult GetProductImages(int idproduct)
        {
            var model = objproduct.GETImages(idproduct).ToList();
            return PartialView(model);
        }
        public class curentcy
        {
            public rates rates { get; set; } 
        }
        public class rates
        {
            public string VND { get; set; }
        }

        public ActionResult ProductsCategory(int id, string name, int? page,string url,string lang,int ? price,int ? color,int? size)
        {
           
            if (lang == null)
                lang = "vn";
            if (price == null)
                price = 0;
            if (color == null)
                color = 0;
            if (size == null)
                size = 0;
            ViewBag.lang = lang;
            ViewBag.title = name;
            ViewBag.Info = objInfo.GETALL().FirstOrDefault();
            ViewBag.productctate = objprodcate.GETALL("", 0, 0, -1).ToList();
            ViewBag.seo = objprodcate.GETALL("",-1,id,-1).FirstOrDefault().ProductCategoryDesc;
            ViewBag.SeoCate = objprodcate.GETALL("", -1, id,-1).FirstOrDefault().ProductCategoryContents;
            ViewBag.url = url;
            int pages = 0;
            if (page == null)
                pages = 1;
            else
                pages = int.Parse(page.ToString());
            var model = objproduct.GETALL("", 0, 0, 1, 1).Where(m=>m.IsShow==1).Where(m => Tool.Utils.CheckSplit(m.ParentID, id) || id == 0).OrderByDescending(m => m.DisplayOrder).ToList();
          
            var newmodel = (from m in model.ToList()
                            select new DTO_Products()
                            {
                                ProductImage = m.ProductImage,
                                ProductUrl = m.ProductUrl,
                                ProductTitle = m.ProductTitle,
                                ProductTitle1 = m.ProductTitle1,
                                ProductTitle2 = m.ProductTitle2,
                                Phivanchuyen1=m.Phivanchuyen1,
                                ProductPrice = m.ProductPrice,
                                ProductPriceDrop = m.ProductPriceDrop,
                                LastPrice = m.ProductPriceDrop != 0 ? m.ProductPriceDrop : m.ProductPrice,
                                LastPriceEN = m.ProductPriceDropEN != 0 ? m.ProductPriceDropEN : m.ProductPriceEN,
                                LastPriceFR = m.ProductPriceDropFR != 0 ? m.ProductPriceDropFR : m.ProductPriceFR,
                                ParentID=m.ParentID
                            }
                          ).ToList();
            
            if (color != 0)
            {
                newmodel = newmodel.ToList().Where(m => int.Parse(m.Quydinhdoihang) == color).ToList();
            }
            if (size != 0)
            {
                newmodel = newmodel.ToList().Where(m => Tool.Utils.CheckSplit(m.Quydinhdoihang1,int.Parse(size.ToString()))).ToList();
            }

            //
            int totalSP = newmodel.Count();
            int tongSoTrang = totalSP / phanTrangSP;
            int sodu = totalSP - (tongSoTrang * phanTrangSP);
            if (sodu > 0)
                tongSoTrang += 1;
            ViewBag.pages = pages;
            ViewBag.tongSoTrang = tongSoTrang;
            newmodel = newmodel.Skip((pages - 1) * phanTrangSP).Take(phanTrangSP).ToList();

            //
            ViewBag.Listcolor = objcolor.GETALL(0);
            ViewBag.Listsize = objsize.GETALL(0);
            ViewBag.Listparent = objprodcate.GETALL("", id, 0, -1).ToList();
            if (ViewBag.Listparent.Count == 0)
            {
                var current = objprodcate.GETALL("", -1, id, -1).FirstOrDefault().ParentID;
                ViewBag.Current = id;
                ViewBag.Listparent = objprodcate.GETALL("", current, 0, -1).ToList();
            }
            //
            return PartialView(newmodel);
        }

        public ActionResult ProductcategoryChild(int IdCate)
        {
           var model = objprodcate.GETALL("", IdCate, 0, -1).ToList();
            return PartialView(model);
        }
        //Chi tiết sản phẩm
        public ActionResult Products(int id, string name,string lang)
        {
            ViewBag.title = name;
            var model = objproduct.GETALL("", id, 1, 1, 100).ToList();
            ViewBag.ProductImage = objproduct.GETImages(id).ToList();
            var lenght = model.FirstOrDefault().ParentID.Split(',').Length-1;
            var parentid = model.FirstOrDefault().ParentID.Split(',')[lenght];
            DTO_ProductCategories dt = objprodcate.GETALL("",-1, int.Parse(parentid),-1).FirstOrDefault();
            ViewBag.Thuonghieu =dt.ProductCategoryTitle ;
            ViewBag.ListLienquan = objproduct.GETALL("", 0, 0, 1, 4).ToList().Where(m => m.ProductId != id && m.ParentID == model.FirstOrDefault().ParentID).ToList().Take(12).ToList();
            var parentids = int.Parse(model.FirstOrDefault().ParentID.Split(',')[1]);

            ViewBag.Listbreadcum = objprodcate.GETALL("", -1, parentids, -1).ToList();
            var parentids2 = int.Parse(model.FirstOrDefault().ParentID.Split(',')[2]);
            ViewBag.Listbreadcum2 = objprodcate.GETALL("", -1, parentids2, -1).ToList();
            ViewBag.Percent = (model.FirstOrDefault().ProductPrice != 0) ?(Math.Round((model.FirstOrDefault().ProductPrice - model.FirstOrDefault().ProductPriceDrop) / model.FirstOrDefault().ProductPrice * 100)) :0;
            ViewBag.Giathamkhao = objgtk.GETALL(0, id).ToList();
            HttpCookie reqCookies = Request.Cookies["commentlogin"];
            if (reqCookies != null)
            {
                ViewBag.login = "1";
               var idcus= reqCookies["CustomerId"].ToString();
                var cust = objcus.GETALL(int.Parse(idcus)).FirstOrDefault();
                if (cust != null)
                {
                    ViewBag.Cusname = cust.Name;
                    ViewBag.Cus = idcus;
                }
               
            }
            //
            var models = (from cm in objcomm.GETALL2(0).ToList()
                          join p in objproduct.GETALL("", 0, 1, 0, 100).ToList()
                          on cm.ProductId equals p.ProductId
                          join cus in objcus.GETALL(0).ToList()
                          on cm.CustomerId equals cus.CustomerId
                          where cm.ProductId == model.FirstOrDefault().ProductId
                           && cm.ParentID == 0
                           && cm.Duyet == 1
                          select new CustomerComments()
                          {
                              CommtentId = cm.CommtentId,
                              Contents = cm.Contents,
                              CustomerName = cus.Name,
                              Duyet = cm.Duyet,
                              Reply = cm.Reply,
                              ProductName = p.ProductTitle,
                              CreateDate = cm.CreateDate,
                              ProductId = cm.ProductId
                          }
                      ).ToList();
            ViewBag.Listcm = models;
            objproduct.UpdateProductsCounts(id);
            return PartialView(model);
        }
        public ActionResult MenuTop(string lang)
        {
            HttpCookie reqCookies = Request.Cookies["userlogins"];
            if (reqCookies != null)
            {
                string email = reqCookies["email"].ToString();
                var getuser = objUserlogin.GETALL(0).Where(m => m.Email == email).FirstOrDefault();
                ViewBag.getuser = getuser;
            }

            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            DataTable dtb = (DataTable)Session["giohang"];
            int totals = 0;
            if (dtb != null)
            {
                foreach (DataRow item in dtb.Rows)
                {
                    totals += int.Parse(item["Amount"].ToString());
                }
                ViewBag.totals = totals;
            }
           
            ViewBag.Info = objInfo.GETALL().FirstOrDefault();
            ViewBag.ListProductCate = objprodcate.GETALL("", 0, 0, -1).ToList();
            //
            ViewBag.aboutus = objnews.GETALL("", 0, 1, 1, 100).Where(m => Tool.Utils.CheckSplit(m.ParentID, 2003)).Take(5).ToList();
            ViewBag.Xaydung = objnewsCategory.GETALL("", 1010, 0, -1).ToList();
            ViewBag.Codienlanh = objprodcate.GETALL("", 0, 0, -1).Where(m => m.IsShow == 1).ToList();
            ViewBag.batdongsan = objnewsCategory.GETALL("", 1015, 0, -1).ToList();
            return PartialView();
        }
        public ActionResult Footer(string lang)
        {
            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            ViewBag.Info = objInfo.GETALL().FirstOrDefault();
           ViewBag.ChinhSach = objnews.GETALL("", 0, 1, 1, 100).Where(m => Tool.Utils.CheckSplit(m.ParentID, 3)).Take(5).ToList();
            ViewBag.Vechungtoi = objnews.GETALL("", 0, 1, 1, 100).Where(m => Tool.Utils.CheckSplit(m.ParentID, 4)).Take(5).ToList();
            ViewBag.ListProductCate = objprodcate.GETALL("", 0, 0, -1).ToList();

            //
            ViewBag.News = objnewsCategory.GETALL("", -1, 2, -1).FirstOrDefault();
            ViewBag.Sharing = objnewsCategory.GETALL("", -1, 1004, -1).FirstOrDefault();
            ViewBag.Service = objnewsCategory.GETALL("", -1, 1002, -1).FirstOrDefault();
            ViewBag.Recruitment = objnewsCategory.GETALL("", -1, 1003, -1).FirstOrDefault();
            //
            ViewBag.aboutus = objnews.GETALL("", 0, 1, 1, 100).Where(m => Tool.Utils.CheckSplit(m.ParentID, 2003)).Take(5).ToList();
            return PartialView();
        }
        public ActionResult Codienlanh()
        {
            var model = objprodcate.GETALL("",0, 0, -1).ToList();
            return PartialView(model);
        }
        public ActionResult Vechungtoi()
        {
            ViewBag.Sumenh = objnews.GETALL("", 2013, 1, 1,100).FirstOrDefault();
            ViewBag.Tamnhin = objnews.GETALL("", 2014, 1, 1, 100).FirstOrDefault();
            ViewBag.Giatricotloi = objnews.GETALL("", 2015, 1, 1, 100).FirstOrDefault();
            ViewBag.Gioithieuchung = objnews.GETALL("", 2022, 1, 1, 100).FirstOrDefault();
            ViewBag.Vechungtoi= objnewsCategory.GETALL("", -1, 2003, -1).FirstOrDefault();
            return PartialView();
        }
        public ActionResult Index(string name,string lang)
        {

            var pass = Tool.Utils.DecryptMD5("nguyenhoang123@456");
            //
            ViewBag.SlCount = objslide.GETALL(0).ToList().Count();
            ViewBag.lang = lang;
            ViewBag.Slideshow = objslide.GETALL(0).ToList();
            ViewBag.title = name;
            ViewBag.Data = objscript.GETALL(0).Where(m =>m.Name.ToLower().Contains("trang chủ")).ToList();
            //
            ViewBag.Info = objInfo.GETALL().FirstOrDefault();
            var newchung = objnews.GETALL("", 0, 1, 1, 100);
            ViewBag.TinTuc = newchung.Where(m => Tool.Utils.CheckSplit(m.ParentID, 2)).OrderByDescending(m=>m.DisplayOrder).Take(10).ToList();
            ViewBag.Tintuccate = objnewsCategory.GETALL("", -1,2, -1).FirstOrDefault();

            ViewBag.Dichvu = newchung.Where(m => Tool.Utils.CheckSplit(m.ParentID, 1002)).Take(10).ToList();
            ViewBag.Dichvucate = objnewsCategory.GETALL("", -1,1002, -1).FirstOrDefault();

            //
           // ViewBag.Vechungtoi = objnewsCategory.GETALL("", -1, 4, - 1).FirstOrDefault();
            ViewBag.Tamnhin = objnewsCategory.GETALL("", -1, 1007, -1).FirstOrDefault();
            ViewBag.Sumenh = objnewsCategory.GETALL("", -1, 1008, -1).FirstOrDefault();
            ViewBag.Giatricotloi = objnewsCategory.GETALL("", -1, 1009, -1).FirstOrDefault();
            //
            ViewBag.Xaydung = objnewsCategory.GETALL("",-1, 1010, -1).FirstOrDefault();
            ViewBag.DuAncate = objnewsCategory.GETALL("", -1, 2002, -1).FirstOrDefault();
            ViewBag.Xaydungchild=objnewsCategory.GETALL("", 1010, 0, -1).ToList();
            ViewBag.Xaydunglist= newchung.Where(m => Tool.Utils.CheckSplit(m.ParentID, 1010)).Take(10).ToList();
            //Bất động sản
            ViewBag.Batdongsan = objnewsCategory.GETALL("", -1, 1015, -1).FirstOrDefault();
            ViewBag.DuAn = newchung.Where(m => Tool.Utils.CheckSplit(m.ParentID, 2002)).Take(10).ToList();
            ViewBag.Batdongsanchild = objnewsCategory.GETALL("", 1015, 0, -1).ToList();
            ViewBag.Batdongsanlist = newchung.Where(m => Tool.Utils.CheckSplit(m.ParentID, 1015)).Take(10).ToList();
            //Cơ điện lạnh
            ViewBag.Codienlanh= objprodcate.GETALL("", 0, 0, -1).Where(m=>m.IsShow==1).ToList();
            ViewBag.ListcustomerIdea = objCustomerIdea.GETALL(0).ToList();
            //Đối tác
            ViewBag.Doitac = objDoitac.GETALL(0).ToList();
            //
            return PartialView();
        }
        public ActionResult News(int id,string name,string lang)
        {
            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            ViewBag.title = name;
            var model = objnews.GETALL("", id, 1, 1, 10).ToList();

            var lststring = model.FirstOrDefault().ParentID.Split(',');
            List<DTO_NewsCategories> lsts = new List<DTO_NewsCategories>();
            foreach (var item in lststring)
            {
                if (item != "0")
                {
                    DTO_NewsCategories  objs= objnewsCategory.GETALL("", -1, int.Parse(item), -1).FirstOrDefault();
                    lsts.Add(objs);
                }
            }
            ViewBag.Name = name;
            ViewBag.ParentName = lsts;
            ViewBag.Listlienquan = objnews.GETALL("", 0, 1, 1, 5).Where(m => m.NewsId != id && m.ParentID== model.FirstOrDefault().ParentID).OrderByDescending(m=>m.DisplayOrder).Take(10).ToList();
            objnews.UpdateNewsCounts(id, 0);

            //
            var models = (from cm in objnewcom.GETALL(0).ToList()
                          join p in objnews.GETALL("", 0, 1, 0, 100).ToList()
                          on cm.NewsID equals p.NewsId 
                          where cm.NewsID == model.FirstOrDefault().NewsId
                           && cm.ParentID == 0
                           && cm.Duyet == 1
                          select new CustomerComments()
                          {
                              CommtentId = cm.CommtentId,
                              Contents = cm.Contents,
                              CustomerName = cm.HoTen,
                              Duyet = cm.Duyet,
                              Reply = cm.Reply,
                              ProductName = p.NewsTitle,
                              CreateDate = cm.CreateDate,
                              ProductId = cm.NewsID
                          }
                    ).ToList();
            ViewBag.Listcm = models;
            return PartialView(model);
        }
        public ActionResult NewsCategories(int id, string name,string lang, int? page,string url)
        {
            int pages = 0;
            if (page == null)
                pages = 1;
            else
                pages = int.Parse(page.ToString());
            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            ViewBag.title = name;
            ViewBag.url = url;
            var model = objnews.GETALL("", 0, 1, 1, 50).Where(m => Tool.Utils.CheckSplit(m.ParentID, id)).OrderByDescending(m=>m.DisplayOrder).ToList();


            int totalSP = model.Count();
            int tongSoTrang = totalSP / phanTrangTT;
            int sodu = totalSP - (tongSoTrang * phanTrangTT);
            if (sodu > 0)
                tongSoTrang += 1;
            ViewBag.pages = pages;
            ViewBag.tongSoTrang = tongSoTrang;
            model = model.Skip((pages - 1) * phanTrangTT).Take(phanTrangTT).ToList();

            var child = objnewsCategory.GETALL("", -1, id, -1).FirstOrDefault();
            if (child.ParentID != 0)
            {
                ViewBag.parent = objnewsCategory.GETALL("", -1, child.ParentID, -1).ToList();

            }
            ViewBag.Title = name;
            ViewBag.Parant = child;
            ViewBag.Albumn = objnewsCategory.GETALL("", 1034, 0, -1).ToList();
            ViewBag.Listparent = objnewsCategory.GETALL("", id, 0, -1).ToList();
            //

            return PartialView(model);
        }
        public ActionResult About(string name,string lang)
        {
            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            ViewBag.title = name;
            ViewBag.Info = objInfo.GETALL().FirstOrDefault();
            return PartialView();
        }

     
        public ActionResult Handle(string url)
        {
            if (url == "")
                url = "/";
            var model = _objmenu.GETMenuTemplate(url);
            if (model.Count()>0 && model.FirstOrDefault().Title == "Trang chủ")
            {
                var md = objscript.GETALL(0).Where(m => m.Name== "Meta Description Trang chủ").FirstOrDefault();
                var kw= objscript.GETALL(0).Where(m => m.Name == "Meta Keyword Trang chủ").FirstOrDefault();
                if (md != null)
                    model.FirstOrDefault().MetaDescription = md.Contents;
                if (kw != null)
                    model.FirstOrDefault().MetaKeywords = kw.Contents;
            }
            else
            {
                if (model.Count() == 0)
                    return View("Page404");

            }
            return View(model);
        }
        public bool SendContact(string data)
        {
            DTO_Contacts model = JsonConvert.DeserializeObject<DTO_Contacts>(data);
            objcontact.UpdateInfors(model);
            return true;
        }
        public ActionResult Lienhebaogia()
        {
            return PartialView();
        }
        public ActionResult Contact(string name,string lang)
        {
            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            ViewBag.title = name;

            ViewBag.Info = objInfo.GETALL().FirstOrDefault();
            return PartialView();
        }

        public ActionResult GioHang()
        {
            float totals = 0;
            DataTable dtb = (DataTable)Session["giohang"];
            ViewBag.dt = dtb;
            ViewBag.title = "Giỏ hàng";
            if (dtb != null)
            {
                foreach (DataRow item in dtb.Rows)
                {
                    totals += float.Parse(item["Total"].ToString());
                }
            }
            
            ViewBag.total = totals;
            return PartialView();
        }
        public ActionResult ThanhToan()
        {
            float totals = 0;
            DataTable dtb = (DataTable)Session["giohang"];
            ViewBag.dt = dtb;
            ViewBag.title = "Thanh toán";
            if (dtb != null)
            {
                foreach (DataRow item in dtb.Rows)
                {
                    totals += float.Parse(item["Total"].ToString());
                }
            }
           
            ViewBag.total = totals;
            HttpCookie reqCookies = Request.Cookies["userlogins"];
            if (reqCookies != null)
            {
                string email = reqCookies["email"].ToString();
                var getuser = objUserlogin.GETALL(0).Where(m => m.Email == email).FirstOrDefault();
                ViewBag.getuser = getuser;
            }
            return PartialView();
        }
        public class ProductCartModel
        {
            public int ProductId { get; set; }
            public string ProductTitle { get; set; }
            public decimal ProductPrice { get; set; }
            public int Amount { get; set; }
            public string ProductImage { get; set; }
            public float Total { get; set; }
        }
        [HttpPost]
        public bool InsertThanhToan(string data)
        {
            var info = objInfo.GETALL().FirstOrDefault();
            HttpCookie reqCookies = Request.Cookies["userlogins"];
            int customerid = 0;
            if (reqCookies != null)
            {
                string email = reqCookies["email"].ToString();
                var getuser = objUserlogin.GETALL(0).Where(m => m.Email == email).FirstOrDefault();
                customerid = getuser.UserID;
            }
            DTO_Customer model = JsonConvert.DeserializeObject<DTO_Customer>(data);
            int type = 0;
            if (reqCookies == null)
            {
                type = 2;
                var result = objorder.UpdateProducts(model);
                var lastcus = objCustomerIdea.GETALLCustomer(0).FirstOrDefault().CustomerId;
                customerid = lastcus;
            }
            else
            {
                type = 1;
                model.CustomerId = customerid;
            }
            DataTable dtb = (DataTable)Session["giohang"];

            StringBuilder strODetai = new StringBuilder();
            strODetai.Append("<div>Thông tin khách hàng</div>");
            strODetai.Append("<table style=\"border-spacing:0; boder-collapse:collapse; font-size:16px;\" width=\"100%\"><tbody>");
            strODetai.Append("<tr>");
            strODetai.Append("<td>Họ tên : "+model.Name+"</td>");
            strODetai.Append("<td>Địa chỉ : " + model.Adress + "</td>");
            strODetai.Append("<td>Phone : " + model.Phone + "</td>");
            strODetai.Append("</tr>");
            strODetai.Append("</tbody></table></div>");

            strODetai.Append("<div style=\"1px dashed #e7ebed; padding:5px;font-size:16px;\">");
            strODetai.Append("<table style=\"border-spacing:0; border-collapse:collapse; font-size:16px;\" width=\"100%\"><tbody>");
            List<ProductCartModel> temp = new List<ProductCartModel>();
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
                ProductCartModel ProductCartModel = new ProductCartModel();
                ProductCartModel.ProductId = Convert.ToInt32(dtb.Rows[i]["ProductId"]); 
                ProductCartModel.ProductTitle = dtb.Rows[i]["ProductTitle"].ToString();
                ProductCartModel.ProductPrice = Convert.ToDecimal(dtb.Rows[i]["ProductPrice"]);
                ProductCartModel.Amount = Convert.ToInt32(dtb.Rows[i]["Amount"]);
                ProductCartModel.ProductImage = dtb.Rows[i]["ProductImage"].ToString();
                temp.Add(ProductCartModel);
            }
            float _totalPr = 0;
            foreach (var item in temp)
            {
                _totalPr += item.Total;
                if (item.ProductPrice == 0)
                {
                    _totalPr = 0; break;
                }
            }


            foreach (var it in temp)
            {
                strODetai.Append("<tr>");
                strODetai.Append("<td style='width:110px;padding-bottom:5px;border-bottom:1px solid #ddd;margin-bottom:5px;'><img src= \"" + "https://nguyenhoangcentury.vn/" + "/upload/Products/" + it.ProductImage + "\" style =\"width:100px; height:100px;\" /></td>");
                strODetai.Append("<td style='width:calc(100% - 110px);padding-bottom:5px;border-bottom:1px solid #ddd;margin-bottom:7px;'>");
                strODetai.Append("<div style='font-size:16px;color:#0A71B9;margin-bottom:8px;'>" + it.ProductTitle + "</div>");
                strODetai.Append("<div style=\"color:red;margin-bottom:8px;font-size:14px;\">Giá: " + (it.ProductPrice.ToString() == "0" ? "Liên hệ" : it.ProductPrice + "đ") + " </div>");
                strODetai.Append("<div style='margin-bottom:8px;font-size:14px;'>Số lượng: " + it.Amount + "</div>");
                strODetai.Append("</td>");
                strODetai.Append("</tr>");

            }
            strODetai.Append("</tbody></table>");
            strODetai.Append("</div>");
            strODetai.Append("<div style=\"1px dashed #e7ebed;padding:10px;\">");
            strODetai.Append("<table style =\"border-spacing:0;border-collapse:collapse;font-size:16px;\" width= \"100%\"><tbody>");
            strODetai.Append("<tr><td align= \"right\"><p style=\"margin: 5px 0;\"><span style= \"color:black;\">Tổng cộng: </span><span>" + (_totalPr == 0 ? "<b style='color:red'>Liên hệ</b>" : "<b style='color:red'>" + string.Format("{0:N0}", _totalPr).ToString() + "đ</b>") + "</span></p></td></tr>");
            strODetai.Append("</tbody></table></div>");



            Tool.Utils.SendEmail(info.Email, "Thông tin đơn hàng từ Nguyễn Hoàng", strODetai.ToString());

            float totals = 0;
            foreach (DataRow item in dtb.Rows)
            {
                totals += float.Parse(item["Total"].ToString());
            }
            
            objOrder.UpdateOrders(model.Desciptions,totals,type, customerid);


          
            foreach (DataRow item in dtb.Rows)
            {
                DTO_OrderDetail orderDetail = new DTO_OrderDetail();
                orderDetail.ProductId = int.Parse(item["ProductId"].ToString());
                orderDetail.Prices = float.Parse(item["ProductPrice"].ToString());
                orderDetail.Amount = int.Parse(item["Amount"].ToString());
                orderDetail.Colors = item["color"].ToString();
                orderDetail.Size = item["size"].ToString();
                objorder.UpdateOrdersDetail(orderDetail);
            }


            Session["giohang"] = null;

            return true;
        }

        public ActionResult LoadAlbumn(int NewsCategoryId)
        {
            ViewBag.parentid = NewsCategoryId;
            var model = objnews.GETALL("", 0, 1, 1, 100).Where(m => Tool.Utils.CheckSplit(m.ParentID, NewsCategoryId)).Take(10).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public void DangKyKhoaHoc(string data)
        {
            
            DTO_KhoaHoc model = JsonConvert.DeserializeObject<DTO_KhoaHoc>(data);
            model.DateCreated = DateTime.Now;
            objKhoahoc.UpdateKhoaHoc(model);

        }
        public ActionResult Getsizeproduct(string ntext)
        {
            var splis = ntext.Split(',');
            var modelcolor = objsize.GETALL(0);
            List<DTO_Sizes> model = new List<DTO_Sizes>();
            foreach (var item in splis)
            {
                if (item != "")
                {
                    var it = modelcolor.ToList().Where(m => m.ID == int.Parse(item)).FirstOrDefault();
                    model.Add(it);
                }
                
            }

            return PartialView(model);
        }

        public ActionResult GetProductsColors(int IDProduct,int IDColor)
        {
            var model = objProdColor.GETALL(0,IDProduct).ToList().Where(m => m.ID == IDColor).ToList();
            var model2 = objproduct.GETImages(model.FirstOrDefault().ID).ToList();
            return PartialView(model2);
        }
        public ActionResult GetProductsColors1(int IDProduct)
        {
            ViewBag.Listproduct = objproduct.GETALL("", IDProduct, 0, 0, 10).ToList();
            var model2 = objproduct.GETImages(IDProduct).ToList();
            return PartialView(model2);
        }
        public ActionResult ChiNhanh(string lang)
        {
            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            var model = objDoitac.GETALL(0).ToList();
            return PartialView(model);
        }
        public ActionResult ProductByCate(int ProductCategoryId)
        {
            var model = objproduct.GETALL("", 0, 0, 1, 1).Where(m => m.IsShow == 1).Where(m => Tool.Utils.CheckSplit(m.ParentID, ProductCategoryId) || ProductCategoryId == 0).OrderByDescending(m => m.DisplayOrder).Take(10).ToList();
            return PartialView(model);
        }

        public ActionResult CustomerIdea()
        {
            var model = objCustomerIdea.GETALL(0).ToList();
            return PartialView(model);
        }
        public ActionResult Antuong()
        {
            var model = objproduct.GETALL("", 0, 0, 1, 1).Where(m => m.Antuong == 1).OrderByDescending(m => m.DisplayOrder).ToList();
            return PartialView(model);
        }
        public ActionResult Banchay()
        {
            var model = objproduct.GETALL("", 0, 0, 1, 1).Where(m => m.Banchay == 1).OrderByDescending(m => m.DisplayOrder).ToList();
            return PartialView(model);
        }
        public ActionResult Moinhat()
        {
            var model = objproduct.GETALL("", 0, 0, 1, 1).Where(m => m.Moinhat == 1).OrderByDescending(m => m.DisplayOrder).ToList();
            return PartialView(model);
        }
        public class searchchung
        {
            public string title { get; set; }
            public string url { get; set; }
            public string image { get; set; }
            public int type { get; set; }
        }
        public ActionResult Search(string key,string lang, int? page,string url)
        {
            ViewBag.url = url;
            ViewBag.key = key;
            int pages = 0;
            if (page == null)
                pages = 1;
            else
                pages = int.Parse(page.ToString());
            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            var model= objproduct.GETALL("", 0, 0, 1, 100).Where(m => RemoveUnicode(m.ProductTitle.ToLower()).Contains(RemoveUnicode(key.ToLower()))).ToList();
            var listnew = objnews.GETALL("", 0, 1, 1, 100).Where(m => RemoveUnicode(m.NewsTitle.ToLower()).Contains(RemoveUnicode(key.ToLower()))).ToList();
            List<searchchung> lstsearch = new List<searchchung>();
            foreach (var item in model)
            {
                searchchung sc = new searchchung();
                sc.title = item.ProductTitle;
                sc.url = item.ProductUrl;
                sc.image = item.ProductImage;
                sc.type = 2;
                lstsearch.Add(sc);
            }
            foreach (var item in listnew)
            {
                searchchung sc = new searchchung();
                sc.title = item.NewsTitle;
                sc.url = item.NewsUrl;
                sc.image = item.NewsImage;
                sc.type = 1;
                lstsearch.Add(sc);
            }
            
            ViewBag.count = lstsearch.Count();

            int totalSP = lstsearch.Count();
            int tongSoTrang = totalSP / phantrangSearch;
            int sodu = totalSP - (tongSoTrang * phantrangSearch);
            if (sodu > 0)
                tongSoTrang += 1;
            ViewBag.pages = pages;
            ViewBag.tongSoTrang = tongSoTrang;
            lstsearch = lstsearch.Skip((pages - 1) * phantrangSearch).Take(phantrangSearch).ToList();
            return PartialView(lstsearch);
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public ActionResult searchajax(string key)
        {
            var model = objproduct.GETALL("", 0, 0, 1, 1).Where(m => RemoveUnicode(m.ProductTitle.ToLower()).Contains(RemoveUnicode(key.ToLower()))).ToList();
            return PartialView(model);
        }
        public void Handle_Info_From_Socialmethod(string firstname, string lastname,string email)
        {

            var listall = objUserlogin.GETALL(0).ToList();
            var check = listall.Where(m => m.Email == email).FirstOrDefault();
            if (check == null)
            {
                DTO_UserLogin dt = new DTO_UserLogin();
                dt.Name = firstname + lastname;
                dt.Email = email;
                dt.Password = "";
                objUserlogin.UpdateInfors(dt);
                HttpCookie userinfo1 = new HttpCookie("userlogins");
                userinfo1["email"] = email;
                userinfo1.Expires = DateTime.Now.AddDays(12);
                Response.Cookies.Add(userinfo1);
            }
            else
            {
                HttpCookie userinfo1 = new HttpCookie("userlogins");
                userinfo1.Expires = DateTime.Now.AddDays(-12);
                Response.Cookies.Add(userinfo1);
               
                HttpCookie userinfo2 = new HttpCookie("userlogins");
                userinfo2["email"] = email;
                userinfo2.Expires = DateTime.Now.AddDays(12);
                Response.Cookies.Add(userinfo2);
            }
        }
        public int Login(string email, string password)
        {
            var listall = objUserlogin.GETALL(0).ToList();
            var check= listall.Where(m => m.Email == email && m.Password==password).FirstOrDefault();
            if (check != null)
            {

                HttpCookie userinfo1 = new HttpCookie("userlogins");
                userinfo1["email"] = email;
                userinfo1.Expires = DateTime.Now.AddDays(12);
                Response.Cookies.Add(userinfo1);
                return 1;

            }
            return 0;
        }
        public int Register(string email, string password)
        {
            DTO_UserLogin dt = new DTO_UserLogin();
            dt.Name = email;
            dt.Email = email;
            dt.Password = password;
            objUserlogin.UpdateInfors(dt);
            HttpCookie userinfo1 = new HttpCookie("userlogins");
            userinfo1["email"] = email;
            userinfo1.Expires = DateTime.Now.AddDays(12);
            Response.Cookies.Add(userinfo1);
            return 1;
        }
        public void Logout()
        {
            HttpCookie userinfo1 = new HttpCookie("userlogins");
            userinfo1.Expires = DateTime.Now.AddDays(-12);
            Response.Cookies.Add(userinfo1);
        }
        public ActionResult Thongtintaikhoan(string lang)
        {
            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            HttpCookie reqCookies = Request.Cookies["userlogins"];
            if (reqCookies != null)
            {
                string email = reqCookies["email"].ToString();
                var getuser = objUserlogin.GETALL(0).Where(m => m.Email == email).FirstOrDefault();
                ViewBag.getuser = getuser;
            }

            return PartialView();
        }
        public void updateThongtintaikhoan(string data)
        {
            DTO_UserLogin model = JsonConvert.DeserializeObject<DTO_UserLogin>(data);
            objUserlogin.UpdateInfors(model);
        }
        public ActionResult Lishsudonhang(string lang)
        {
            if (lang == null)
                lang = "vn";
            ViewBag.lang = lang;
            HttpCookie reqCookies = Request.Cookies["userlogins"];
            int customer = 0;
            if (reqCookies != null)
            {
                string email = reqCookies["email"].ToString();
                var getuser = objUserlogin.GETALL(0).Where(m => m.Email == email).FirstOrDefault();
                ViewBag.getuser = getuser;
                customer = getuser.UserID;
            }
            var model = objorder.TCP_CusOrder_SEL().ToList().Where(m=>m.UserID== customer).ToList();
            return PartialView(model);
        }
        public ActionResult HistoryOrder(int OrderID,string lang)
        {
            ViewBag.lang = lang;
            var model=objorder.TCP_Orderdetail_SEL(OrderID);
            return PartialView(model);
        }

        public ActionResult MenuProdChild(int ProductCategoryId)
        {
            var model = objprodcate.GETALL("", ProductCategoryId, 0, -1);
            return PartialView(model);
        }
        public ActionResult Tragop(int ids)
        {
            var model = objproduct.GETALL("",ids, 0, 1, 4).ToList();
            ViewBag.Bank = objBank.GETALL(0).ToList();
            return PartialView(model);
        }

        public bool MuatraGop(string data)
        {
            DTO_DonHangTraGop model = JsonConvert.DeserializeObject<DTO_DonHangTraGop>(data);
            objcontact.UpdateDonHangTraGop(model);
            return true;
        }
        public void SendComments(string data)
        {
            DTO_Comment model = JsonConvert.DeserializeObject<DTO_Comment>(data);
            var productInfo = objproduct.GETALL("", model.ProductId, 0, 1, 1).FirstOrDefault();
            var customerInfo = objcus.GETALL(model.CustomerId).FirstOrDefault();
            model.Displayorder = 0;
            model.Duyet = 0;
            objcomm.UpdateComments(model);

            StringBuilder strODetai = new StringBuilder();
            strODetai.Append("<div>Thông tin khách hàng</div>");
            strODetai.Append("<table style=\"border-spacing:0; boder-collapse:collapse; font-size:16px;\" width=\"100%\"><tbody>");
            strODetai.Append("<tr>");
            strODetai.Append("<td>Họ tên : " + customerInfo.Name + "</td>");
            strODetai.Append("<td>Phone: " + customerInfo.Phone + "</td>");
            strODetai.Append("<td>Email : " + customerInfo.Email + "</td>");
            strODetai.Append("</tr>");
            strODetai.Append("</tbody></table></div>");

            strODetai.Append("<div>Nội dung commnet</div>");
            strODetai.Append("<table style=\"border-spacing:0; boder-collapse:collapse; font-size:16px;\" width=\"100%\"><tbody>");
            strODetai.Append("<tr>");
            strODetai.Append("<td>Sản phẩm : " + "<a href="+"https://nguyenhoangcentury.vn/"+productInfo.ProductUrl+">" + productInfo.ProductTitle +"</a>"+ "</td>");
            strODetai.Append("<td>Nội dung: " + model.Contents + "</td>");
            strODetai.Append("</tr>");
            strODetai.Append("</tbody></table></div>");


            strODetai.Append("<div style=\"1px dashed #e7ebed; padding:5px;font-size:16px;\">");
            strODetai.Append("<table style=\"border-spacing:0; border-collapse:collapse; font-size:16px;\" width=\"100%\"><tbody>");
           // Tool.Utils.SendEmail("info@nhc-group.vn", "Nội dung comment của khách", strODetai.ToString());
        }

        public void SendNewComments(string data)
        {
            DTO_NewsComment model = JsonConvert.DeserializeObject<DTO_NewsComment>(data);
            var productInfo = objnews.GETALL("", model.NewsID, 0, 1, 1).FirstOrDefault();
            var customerInfo = objcus.GETALL(model.CustomerId).FirstOrDefault();
            model.Displayorder = 0;
            model.Duyet = 0;
            objcomm.UpdateNewComments(model);

            StringBuilder strODetai = new StringBuilder();
            strODetai.Append("<div>Thông tin khách hàng</div>");
            strODetai.Append("<table style=\"border-spacing:0; boder-collapse:collapse; font-size:16px;\" width=\"100%\"><tbody>");
            strODetai.Append("<tr>");
            strODetai.Append("<td>Họ tên : " + model.HoTen + "</td>");
            strODetai.Append("<td>Phone: " + model.SoDT + "</td>");
            strODetai.Append("<td>Email : " + model.Email + "</td>");
            strODetai.Append("</tr>");
            strODetai.Append("</tbody></table></div>");

            strODetai.Append("<div>Nội dung commnet</div>");
            strODetai.Append("<table style=\"border-spacing:0; boder-collapse:collapse; font-size:16px;\" width=\"100%\"><tbody>");
            strODetai.Append("<tr>");
            strODetai.Append("<td>Sản phẩm : " + "<a href=" + "https://nguyenhoangcentury.vn/" + productInfo.NewsUrl + ">" + productInfo.NewsTitle + "</a>" + "</td>");
            strODetai.Append("<td>Nội dung: " + model.Contents + "</td>");
            strODetai.Append("</tr>");
            strODetai.Append("</tbody></table></div>");


            strODetai.Append("<div style=\"1px dashed #e7ebed; padding:5px;font-size:16px;\">");
            strODetai.Append("<table style=\"border-spacing:0; border-collapse:collapse; font-size:16px;\" width=\"100%\"><tbody>");
          //  Tool.Utils.SendEmail("info@nhc-group.vn", "Nội dung comment của khách", strODetai.ToString());
        }
        public decimal GetBankInfo(int BankID, int Month, decimal price)
        {
            decimal values = 0;
            var vl = objBank.GETALL(BankID).FirstOrDefault();
            if (vl != null)
            {
                switch (Month)
                {
                    case 12: return price + price * (decimal)vl.Month12 / 100;
                        break;
                    case 9:
                        return price + price * (decimal)vl.Month9 / 100;
                        break;
                    case 6:
                        return price + price * (decimal)vl.Month6 / 100;
                        break;
                    case 3:
                        return price + price * (decimal)vl.Month3 / 100;
                        break;
                } 
            }
            return values;
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Datlich()
        {
            return View();
        }

        public bool SendDatlich(string data)
        {
            DTO_Datlich model = JsonConvert.DeserializeObject<DTO_Datlich>(data);
           var result=   objDatlich.UpdateDatlich(model);
            return result;
        }
    }
}