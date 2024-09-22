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
    public class ProductCategoryController : Controller
    {
        DAL_ProductCategories objDal_Productcate = new DAL_ProductCategories();
        DAL_Products objproduct = new DAL_Products();
        // GET: Administrator/ProductCategoty
        public ActionResult GetList()
        {
            return View();
        }
        public ActionResult Test()
        {
            return PartialView();
        }
        public ActionResult ProductCategoryselect(int parentId,int exexcep)
        {
            var model = objDal_Productcate.GETALL("", parentId, 0, -1).Where(m=>m.ProductCategoryId!= exexcep).ToList();
            ViewBag.Excep = exexcep;
            return PartialView(model);
        }
        public ActionResult InsertList(int ? ProductCategoryId)
        {
            if (ProductCategoryId == null)
                ProductCategoryId = -1;
            
            var model = objDal_Productcate.GETALL("", -1, int.Parse(ProductCategoryId.ToString()), -1);
            if (model != null && model.Count()>0)
            {
                ViewBag.Selected = model.FirstOrDefault().ParentID;
            }
            else
            {
                ViewBag.Selected = 0;
            }
            ViewBag.Excep = ProductCategoryId;
            ViewBag.ListProCate= objDal_Productcate.GETALL("",0, 0, -1).Where(m=>m.ProductCategoryId!= ProductCategoryId).ToList();
            return View(model);
        }
        #region Ajax
        [HttpPost]
        public bool ProductcategorySort(int id,int idorder)
        {
            var result = objDal_Productcate.UpdateProductCategoriesOrder(id,idorder);
            return result;
        }
        [HttpPost]
        public bool ProductcategoryDisplayorder(int id, int IsShow)
        {
            var result = objDal_Productcate.UpdateProductCateDisplay(id, IsShow);
            return result;
        }
        [HttpPost]
        public ActionResult ProductcategoryGetData()
        {
            var model = objDal_Productcate.GETALL("", 0, 0, -1);
            return PartialView(model);
        }
        public ActionResult ProductcategoryGetDataChild(int parentId)
        {
            var model = objDal_Productcate.GETALL("", parentId, 0, -1);
            return PartialView(model);
        }
        public ActionResult ProductcategoryGetDataChild2(int parentId)
        {
            var model = objDal_Productcate.GETALL("", parentId, 0, -1);
            return PartialView(model);
        }
        //UpdateProductCategories
        [HttpPost]
        [ValidateInput(false)]
        public bool UpdateProductCategories(string data)
        {
            DTO_ProductCategories model = JsonConvert.DeserializeObject<DTO_ProductCategories>(data);
            model.CreatedBy = 1;
            if (model.Title == "")
                model.Title = model.ProductCategoryTitle;
            if (model.Url == "")
            {
                model.Url = Utils.ConvertToUnSign(model.Title);
                
            }
            model.ProductCategoryUrl = model.Url;

            string newsImage = model.ProductCategoryImage;
            //
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/ProductCategories/" + newsImage)))
            {
                if (newsImage != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/ProductCategories/"+ newsImage)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/ProductCategories"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + newsImage), Server.MapPath("~/Upload/ProductCategories/"+ newsImage));
                        }
                    }
                    catch (Exception) { }
                }
            }
            //
            bool result= objDal_Productcate.UpdateProductCategories(model);
            return result;
        }

        [HttpPost]
        public int ProductcategoryDelete(int productCategoryId)
        {
            var countSP = objproduct.GETALL("", 0, 0, 1, 100).Where(m => Tool.Utils.CheckSplit(m.ParentID, productCategoryId) || productCategoryId == 0).ToList();

            int result = 0;
            if (countSP.Count() > 0)
                result = -1;
            else
                result = objDal_Productcate.ProductcategoryDelete(productCategoryId);
            return result;
        }
        #endregion

    }
}