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
    public class ProductsController : Controller
    {
        private int phanTrangSP=15;
        DAL_ProductCategories objDal_Productcate = new DAL_ProductCategories();
        DAL_Products objDal_Product = new DAL_Products();
        DAL_Colors objcolor = new DAL_Colors();
        DAL_Sizes objSize = new DAL_Sizes();
        DAL_News objnew = new DAL_News();
        // GET: Administrator/Products
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public bool ProductAnTuong(int id, int idorder)
        {
            var result = objDal_Product.UpdateProductAnTuong(id, idorder);
            return result;
        }
        //updateBanchay
        public bool ProductBanChay(int id, int idorder)
        {
            var result = objDal_Product.UpdateProductBanchay(id, idorder);
            return result;
        }
        public bool ProductMoiNhat(int id, int idorder)
        {
            var result = objDal_Product.UpdateProductMoinhat(id, idorder);
            return result;
        }
        [HttpPost]
        public bool ProductSort(int id, int idorder)
        {
            var result = objDal_Product.UpdateProductOrder(id, idorder);
            return result;
        }
        public ActionResult ProductsGetList(int ? idcate,int ? page, string key)
        {
            if (key == null)
                key = "";
            int pages=0;
            if (idcate == null)
                idcate = 0;
            if (page == null)
                pages = 1;
            else
                pages = int.Parse(page.ToString());
            ViewBag.Listcate = objDal_Productcate.GETALL("",0, 0, -1).ToList();
            var model = objDal_Product.GETALL(key, 0, 0, 1,1000).ToList().Where(m => Tool.Utils.CheckSplit(m.ParentID, int.Parse(idcate.ToString())) || idcate == 0).OrderByDescending(m=>m.DisplayOrder).ToList();
            //Phân trang
            int totalSP = model.Count();
            int tongSoTrang = totalSP / phanTrangSP;
            int sodu = totalSP - (tongSoTrang * phanTrangSP);
            if (sodu > 0)
                tongSoTrang += 1;
            ViewBag.idcate = idcate;
            ViewBag.pages = pages;
            ViewBag.tongSoTrang = tongSoTrang;
            ViewBag.key = key;
            //Phân trang model
            model = model.Skip((pages - 1) * phanTrangSP).Take(phanTrangSP).ToList();
            return View(model);
        }
        public ActionResult ProductsGetListChild(int ParentID)
        {
            ViewBag.Listcate = objDal_Productcate.GETALL("", ParentID, 0, -1).ToList();
            return View();
        }
        [HttpPost]
        public string GetNameImages(int ProductID)
        {
            var nod = objDal_Product.GETImages(int.Parse(ProductID.ToString()));
            string img = "";
            foreach(var item in nod)
            {
                img += item.Images + ",";

            }
            return img;
        }
        [HttpPost]
        public ActionResult ProductsGetData(int idcate)
        {
            var model = objDal_Product.GETALL("",0, 0, 1, 20).Where(m =>Tool.Utils.CheckSplit(m.ParentID,idcate) || idcate==0).ToList();
          
            return PartialView(model);
        }
        public ActionResult ProductsInsertList(int? ProductId)
        {
            if (ProductId == null)
                ProductId = -1;

            var model = objDal_Product.GETALL("", int.Parse(ProductId.ToString()), - 1,1,20);
            if (model != null && model.Count() > 0)
            {
                var lsts = model.FirstOrDefault().ParentID.Split(',');
                ViewBag.Selected = lsts[lsts.Length - 1];
            }
            else
            {
                ViewBag.Selected = 0;
            }
            ViewBag.ListProCate = objDal_Productcate.GETALL("", 0, 0, -1).ToList();
            //
            var nod = objDal_Product.GETImages(int.Parse(ProductId.ToString()));
           
            ViewBag.ListImage = nod;
            //
            ViewBag.Listcolor = objcolor.GETALL(0).ToList();
            ViewBag.Listsize = objSize.GETALL(0).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public bool UpdateProductCategories(string data)
        {
            DTO_Products model = JsonConvert.DeserializeObject<DTO_Products>(data);
            DTO_Products dtprod = objDal_Product.GETALL("", 0, 0, 1, 1).ToList().OrderByDescending(m => m.ProductId).FirstOrDefault();
            var get = dtprod != null ? dtprod.DisplayOrder + 1 : 1;
            model.DisplayOrder = get;
            model.CreatedBy = 1;
            if (model.Title == "")
                model.Title = model.ProductTitle;
            if (model.Url == "")
            {
                model.Url = Utils.ConvertToUnSign(model.Title);

            }
            model.ProductUrl = model.Url;

            string newsImage = model.ProductImage;
            //
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/Products/" + newsImage)))
            {
                if (newsImage != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/Products/" + newsImage)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/Products"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + newsImage), Server.MapPath("~/Upload/Products/" + newsImage));
                        }
                    }
                    catch (Exception) { }
                }
            }
            //
            bool result = objDal_Product.UpdateProducts(model);
            //Multi images
            int idProductsnews = 0;
            if (model.ProductId == 0)
            {
                 idProductsnews = objDal_Product.GETALL("", 0, 0, 1, 20).OrderByDescending(m => m.ProductId).FirstOrDefault().ProductId;
            }
            else
            {
                idProductsnews = model.ProductId;
            }

            string[] lstStr = model.multiImage.Split(',');
            foreach(var item in lstStr.ToList())
            {
                if (!string.IsNullOrEmpty(item))
                {
                   var rs= objDal_Product.UpdateProductImg(0,idProductsnews, item);
                    if (!System.IO.File.Exists(Server.MapPath("~/Upload/Products/" + item)))
                    {
                        if (item != "")
                        {
                            try
                            {
                                if (!System.IO.File.Exists(Server.MapPath("~/Upload/Products/" + item)))
                                {
                                    Directory.CreateDirectory(Server.MapPath("~/Upload/Products"));
                                    System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + item), Server.MapPath("~/Upload/Products/" + item));
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                }
               
            }

            return result;
        }
        public ActionResult ProductCategoryselect(int parentId)
        {
            var model = objDal_Productcate.GETALL("", parentId, 0, -1).ToList();
            return PartialView(model);
        }
        public ActionResult ProductCategoryselect2(int parentId)
        {
            var model = objDal_Productcate.GETALL("", parentId, 0, -1).ToList();
            return PartialView(model);
        }
        [HttpPost]
        public bool DeleteProductsImage(int ProductId,string Images)
        {
            var result = objDal_Product.DeleteProductImage(ProductId, Images);
            return true;
        }
        [HttpPost]
        public int ProductsDelete(int ProductID)
        {
            var model = objDal_Product.ProductsDelete(ProductID);
            return 1;
        }
        public void UpTopUpTop(string data)
        {
            var spl = data.Split(',');
            DTO_Products dtprod = objDal_Product.GETALL("", 0, 0, 1, 1).ToList().OrderByDescending(m => m.DisplayOrder).FirstOrDefault();
            var get = dtprod.DisplayOrder;
            for (int i= (spl.Length - 1); i > 0; i--)
            {
                get += 1;
                objDal_Product.UpdateProductOrder(int.Parse(spl[i-1]), get);
            }
        }
        public void UpTopUpTop2(string data)
        {
            var spl = data.Split(',');
            DTO_News dtprod = objnew.GETALL("", 0, 0, 1, 1).ToList().OrderByDescending(m => m.DisplayOrder).FirstOrDefault();

            var get = dtprod.DisplayOrder;
            for (int i = (spl.Length - 1); i > 0; i--)
            {
                get += 1;
                objnew.UpdateProductCategoriesOrder(int.Parse(spl[i - 1]), get);
            }
        }
        public void Capnhatgiaca(int idproduct,decimal ProductPrice,decimal ProductPriceDrop)
        {
            var getmodel = objDal_Product.GETALL("", idproduct, 0, 1, 100).FirstOrDefault();
            getmodel.ProductPrice = ProductPrice;
            getmodel.ProductPriceDrop = ProductPriceDrop;
            objDal_Product.TCP_ProductChangePrice(getmodel.ProductId, getmodel.ProductPrice, getmodel.ProductPriceDrop);
        }
    }
}