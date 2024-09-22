using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCP.DTO;
using TCP.DAL;
using Newtonsoft.Json;

namespace TCP.Core.Areas.Administrator.Controllers
{
    public class CommnentsController : Controller
    {
        // GET: Administrator/Commnents
        DAL_CustomerComment objcomment = new DAL_CustomerComment();
        DAL_Products objproduct = new DAL_Products();
        DAL_CustomerComment objcus = new DAL_CustomerComment();
        public ActionResult Getlistcomment()
        {
            var model = (from cm in objcomment.GETALL2(0).ToList()
                         join p in objproduct.GETALL("", 0, 1, 0, 100).ToList()
                         on cm.ProductId equals p.ProductId 
                         join cus in objcus.GETALL(0).ToList()
                         on cm.CustomerId equals cus.CustomerId
                         where cm.ParentID==0
                         select new CustomerComments()
                         {
                             CommtentId=cm.CommtentId,
                             Contents=cm.Contents,
                             CustomerId=cm.CustomerId,
                             CustomerName=cus.Name,
                             Duyet=cm.Duyet,
                             Reply=cm.Reply,
                             ProductName=p.ProductTitle,
                             CreateDate=cm.CreateDate,
                             ProductUrl=objproduct.GETALL("", p.ProductId,1, 1,100).FirstOrDefault().ProductUrl,
                             HoTen=cus.Name,
                             Email=cus.Email,
                             SoDT=cus.Phone
                         }
                        ).ToList();
            return View(model);
        }
        public ActionResult CommnentsDetail(int CommtentId)
        {
            var model = (from cm in objcomment.GETALL2(0).ToList()
                         join p in objproduct.GETALL("", 0, 1, 0, 100).ToList()
                         on cm.ProductId equals p.ProductId
                         join cus in objcus.GETALL(0).ToList()
                         on cm.CustomerId equals cus.CustomerId
                         where cm.CommtentId==CommtentId
                         && cm.ParentID==0
                         select new CustomerComments()
                         {
                             CommtentId = cm.CommtentId,
                             Contents = cm.Contents,
                             CustomerName = cus.Name,
                             Duyet = cm.Duyet,
                             Reply = cm.Reply,
                             ProductName = p.ProductTitle,
                             CreateDate = cm.CreateDate,
                             ProductId=cm.ProductId
                         }
                       ).ToList();
            return View(model);
        }
        [ValidateInput(false)]
        public void SendComments(string data)
        {
            DTO_Comment model = JsonConvert.DeserializeObject<DTO_Comment>(data);
            if (model.CommtentId != 0)
            {
                DTO_Comment now = objcomment.GETALL2(model.CommtentId).FirstOrDefault();
                if (now != null && now.CommtentId != 0)
                {
                    model.CustomerId = now.CustomerId;
                    model.ProductId = now.ProductId;
                    model.CreateDate = now.CreateDate;
                    model.Type = now.Type;
                    model.ParentID = now.ParentID;
                }
            }
          
            model.Displayorder = 0;
            model.Duyet = 1;
            objcus.UpdateComments(model);

            //Cập nhật đã xem Parent
            objcomment.Comments_Reply(model.ParentID);
        }
        public void Duyet(int CommtentId,int Duyet)
        {
            if (Duyet == 0)
                Duyet = 1;
            else
                Duyet = 0;
            objcus.Comments_Duyet(CommtentId, Duyet);
        }
        public ActionResult CommmentChild(int CommtentId)
        {
            var models = (from cm in objcus.GETALL2(0).ToList()
                          join p in objproduct.GETALL("", 0, 1, 0, 100).ToList()
                          on cm.ProductId equals p.ProductId
                          join cus in objcus.GETALL(0).ToList()
                          on cm.CustomerId equals cus.CustomerId into lc
                          from lcs in lc.DefaultIfEmpty()
                          where cm.ParentID == CommtentId
                          select new CustomerComments()
                          {
                              CommtentId = cm.CommtentId,
                              Contents = cm.Contents,
                              CustomerName = lcs != null ? lcs.Name : "",
                              Duyet = cm.Duyet,
                              Reply = cm.Reply,
                              ProductName = p.ProductTitle,
                              CreateDate = cm.CreateDate,
                              ProductId = cm.ProductId,
                              Type = cm.Type,
                              ParentID = cm.ParentID
                          }
                     ).ToList();
            return PartialView(models);
        }
        public ActionResult GetCustomerInfo(int CustomerId)
        {
            var model = objcus.GETALL(CustomerId).ToList();
            return PartialView(model);
        }
    }
}