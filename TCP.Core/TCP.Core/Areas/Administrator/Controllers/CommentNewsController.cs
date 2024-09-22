using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCP.DAL;
using TCP.DTO;

namespace TCP.Core.Areas.Administrator.Controllers
{
    public class CommentNewsController : Controller
    {
        // GET: Administrator/CommentNews
        // GET: Administrator/Commnents
        DAL_CustomerCommentNews objcomment = new DAL_CustomerCommentNews();
        DAL_News objproduct = new DAL_News();
        DAL_CustomerComment objcus = new DAL_CustomerComment();
        public ActionResult GetlistNewcomment()
        {
            var model = (from cm in objcomment.GETALL(0).ToList()
                         join p in objproduct.GETALL("", 0, 1, 0, 100).ToList()
                         on cm.NewsID equals p.NewsId 
                         where cm.ParentID == 0
                         select new CustomerComments()
                         {
                             CommtentId = cm.CommtentId,
                             Contents = cm.Contents,
                             CustomerId = cm.CustomerId, 
                             Duyet = cm.Duyet,
                             Reply = cm.Reply,
                             ProductName = p.NewsTitle,
                             CreateDate = cm.CreateDate,
                             ProductUrl = objproduct.GETALL("", p.NewsId, 1, 1, 100).FirstOrDefault().NewsUrl,
                             HoTen = cm.HoTen,
                             Email = cm.Email,
                             SoDT = cm.SoDT
                         }
                        ).ToList();
            return View(model);
        }
        public ActionResult CommnentsDetail(int CommtentId)
        {
            var model = (from cm in objcomment.GETALL(0).ToList()
                         join p in objproduct.GETALL("", 0, 1, 0, 100).ToList()
                         on cm.NewsID equals p.NewsId
                         where cm.CommtentId == CommtentId
                         && cm.ParentID == 0
                         select new CustomerComments()
                         {
                             CommtentId = cm.CommtentId,
                             Contents = cm.Contents, 
                             Duyet = cm.Duyet,
                             Reply = cm.Reply,
                             ProductName = p.NewsTitle,
                             CreateDate = cm.CreateDate,
                             ProductId = cm.NewsID,
                             HoTen = cm.HoTen,
                             Email = cm.Email,
                             SoDT = cm.SoDT
                         }
                       ).ToList();
            return View(model);
        }
        public ActionResult CommmentChild(int CommtentId)
        {
            var models = (from cm in objcomment.GETALL(0).ToList()
                          join p in objproduct.GETALL("", 0, 1, 0, 100).ToList()
                          on cm.NewsID equals p.NewsId 
                          where cm.ParentID == CommtentId
                          select new CustomerComments()
                          {
                              CommtentId = cm.CommtentId,
                              Contents = cm.Contents, 
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
        [ValidateInput(false)]
        public void SendComments(string data)
        {
            DTO_NewsComment model = JsonConvert.DeserializeObject<DTO_NewsComment>(data);
            if (model.CommtentId != 0)
            {
                DTO_NewsComment now = objcomment.GETALL(model.CommtentId).FirstOrDefault();
                if (now != null && now.CommtentId != 0)
                {
                    model.CustomerId = now.CustomerId;
                    model.NewsID = now.NewsID;
                    model.CreateDate = now.CreateDate;
                    model.Type = now.Type;
                    model.ParentID = now.ParentID;
                }
            }

            model.Displayorder = 0;
            model.Duyet = 1;
            objcomment.UpdateComments(model);

            //Cập nhật đã xem Parent
            objcomment.Comments_Reply(model.ParentID);
        }
        public void Duyet(int CommtentId, int Duyet)
        {
            if (Duyet == 0)
                Duyet = 1;
            else
                Duyet = 0;
            objcomment.Comments_Duyet(CommtentId, Duyet);
        }
        //public ActionResult CommmentChild(int CommtentId)
        //{
        //    var models = (from cm in objcus.GETALL2(0).ToList()
        //                  join p in objproduct.GETALL("", 0, 1, 0, 100).ToList()
        //                  on cm.ProductId equals p.NewsId
        //                  join cus in objcus.GETALL(0).ToList()
        //                  on cm.CustomerId equals cus.CustomerId into lc
        //                  from lcs in lc.DefaultIfEmpty()
        //                  where cm.ParentID == CommtentId
        //                  select new CustomerComments()
        //                  {
        //                      CommtentId = cm.CommtentId,
        //                      Contents = cm.Contents,
        //                      CustomerName = lcs != null ? lcs.Name : "",
        //                      Duyet = cm.Duyet,
        //                      Reply = cm.Reply,
        //                      ProductName = p.NewsTitle,
        //                      CreateDate = cm.CreateDate,
        //                      ProductId = cm.ProductId,
        //                      Type = cm.Type,
        //                      ParentID = cm.ParentID
        //                  }
        //             ).ToList();
        //    return PartialView(models);
        //}
        public ActionResult GetCustomerInfo(int CustomerId)
        {
            var model = objcus.GETALL(CustomerId).ToList();
            return PartialView(model);
        }
    }
}