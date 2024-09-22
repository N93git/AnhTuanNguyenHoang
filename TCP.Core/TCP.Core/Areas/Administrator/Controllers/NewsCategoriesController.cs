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
    public class NewsCategoriesController : Controller
    {
        DAL_NewsCategories objDal_Productcate = new DAL_NewsCategories();
        // GET: Administrator/ProductCategoty

        [HttpPost]
        public bool NewsCategoriesSort(int id, int idorder)
        {
            var result = objDal_Productcate.UpdateProductCategoriesOrder(id, idorder);
            return result;
        }
        public ActionResult NewCategoriesGetList()
        {
            return View();
        }
        public ActionResult Test()
        {
            return PartialView();
        }
        public ActionResult ProductCategoryselect(int parentId, int exexcep)
        {
            var model = objDal_Productcate.GETALL("", parentId, 0, -1).Where(m => m.NewsCategoryId != exexcep).ToList();
            ViewBag.Excep = exexcep;
            return PartialView(model);
        }
        public ActionResult NewscategoryInsertList(int? ProductCategoryId)
        {
            if (ProductCategoryId == null)
                ProductCategoryId = -1;

            var model = objDal_Productcate.GETALL("", -1, int.Parse(ProductCategoryId.ToString()), -1);
            if (model != null && model.Count() > 0)
            {
                ViewBag.Selected = model.FirstOrDefault().ParentID;
            }
            else
            {
                ViewBag.Selected = 0;
            }
            ViewBag.Excep = ProductCategoryId;
            ViewBag.ListProCate = objDal_Productcate.GETALL("", 0, 0, -1).Where(m => m.NewsCategoryId != ProductCategoryId).ToList();
            return View(model);
        }
        #region Ajax
        [HttpPost]
        public ActionResult NewscategoryGetData()
        {
            var model = objDal_Productcate.GETALL("", 0, 0, -1);
            return PartialView(model);
        }
        public ActionResult NewscategoryGetDataChild(int parentId)
        {
            var model = objDal_Productcate.GETALL("", parentId, 0, -1);
            return PartialView(model);
        }
        //UpdateProductCategories
        [HttpPost]
        [ValidateInput(false)]
        public bool UpdateNewsCategories(string data)
        {
            DTO_NewsCategories model = JsonConvert.DeserializeObject<DTO_NewsCategories>(data);
            model.CreatedBy = 1;
            if (model.Title == "")
                model.Title = model.NewsCategoryTitle;
            if (model.Url == "")
            {
                model.Url = Utils.ConvertToUnSign(model.Title);

            }
            model.NewsCategoryUrl = model.Url;

            string newsImage = model.NewsCategoryImage;
            //
            if (!System.IO.File.Exists(Server.MapPath("~/Upload/NewsCategories/" + newsImage)))
            {
                if (newsImage != "")
                {
                    try
                    {
                        if (!System.IO.File.Exists(Server.MapPath("~/Upload/NewsCategories/" + newsImage)))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Upload/NewsCategories"));
                            System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + newsImage), Server.MapPath("~/Upload/NewsCategories/" + newsImage));
                        }
                    }
                    catch (Exception) { }
                }
            }
            //
            bool result = objDal_Productcate.UpdateNewsCategories(model);
            return result;
        }

        [HttpPost]
        public int NewscategoryDelete(int NewsCategoryId)
        {
            var result = objDal_Productcate.NewsCategoryDelete(NewsCategoryId);
            return result;
        }
        #endregion


        [HttpPost]
        public bool NewscategoryShow(int id, int idorder)
        {
            var result = objDal_Productcate.UpdateNewsshow(id, idorder);
            return result;
        }
    }
}