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
    public class ProductsColorsController : Controller
    {
        DAL_ProductColors objprodColor = new DAL_ProductColors();
        DAL_Colors objColor = new DAL_Colors();
        DAL_Sizes objSize = new DAL_Sizes();
        DAL_Products obj = new DAL_Products();
        // GET: Administrator/ProductsColors

        public ActionResult Getlistimage(int id)
        {
            var model = obj.GETImages(id).ToList();
            return PartialView(model);
        }
        public string getSizeName(string values)
        {
            var str = "";
            var lst = values.Split(',');
            var lstSize = objSize.GETALL(0);
            foreach(var item in lst)
            {
                if (item != "")
                {
                    var size = lstSize.ToList().Where(m => m.ID == int.Parse(item)).FirstOrDefault();
                    str += size.Name + ",";
                }
               
            }
           
            return str;
        }
        public ActionResult ProductColorList(int ProductId)
        {
            ViewBag.IDProduct = ProductId;
            var model = objprodColor.GETALL(0, ProductId).ToList();
            var nmodel = (from pcl in model
                          join cl in objColor.GETALL(0)
                          on pcl.IDColor equals cl.ID
                          select new DTO_ProductColors()
                          {
                              ID=pcl.ID,
                              ColorName=cl.Name,
                              ColorHex=cl.Hex,
                              SizeName=getSizeName(pcl.IDSize)
                          }
                        ).ToList();
            ViewBag.Listcolor = objColor.GETALL(0).ToList();
            ViewBag.Listsize = objSize.GETALL(0).ToList();
            return View(nmodel);
        }

        //public ActionResult SizesInsertList(int? ID)
        //{
        //    if (ID == null)
        //        ID = -1;

        //    var model = objprodColor.GETALL(int.Parse(ID.ToString())).ToList();
        //    return View(model);
        //}
        public int UpdateSize(string data)
        {
            DTO_ProductColors model = JsonConvert.DeserializeObject<DTO_ProductColors>(data);
            objprodColor.UpdateProductColors(model);
            var idnew = objprodColor.GETALL(0, model.IDProduct).OrderByDescending(m => m.ID).FirstOrDefault().ID;
            var lstimg = model.multiImage.Split(',');
            foreach(var item in lstimg)
            {
                if (item != "")
                {
                    if (obj.GETImages(idnew).Where(m => m.Images == item).Count() == 0)
                    {
                        obj.UpdateProductImg(idnew,0, item);
                    }
                    
                }
            }

            string[] lstStr = model.multiImage.Split(',');
            foreach (var item in lstStr.ToList())
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (!System.IO.File.Exists(Server.MapPath("~/Upload/ProductsImg/" + item)))
                    {
                        if (item != "")
                        {
                            try
                            {
                                if (!System.IO.File.Exists(Server.MapPath("~/Upload/ProductsImg/" + item)))
                                {
                                    Directory.CreateDirectory(Server.MapPath("~/Upload/ProductsImg"));
                                    System.IO.File.Move(Server.MapPath("~/Upload/Temp/" + item), Server.MapPath("~/Upload/ProductsImg/" + item));
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                }

            }
            return 1;
        }

        public JsonResult Getitem(int id,int idproduct)
        {
            var model = objprodColor.GETALL(id, idproduct).FirstOrDefault();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public int DeleteProductsColors(int ID)
        {
            objprodColor.ProductsColorsDelete(ID);
            return 1;
        }
    }
}