using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCP.DTO;
using TCP.DAL;

namespace TCP.Core.Areas.Administrator.Controllers
{
    public class OrdersController : Controller
    {
        DAL_Orders objOrder = new DAL_Orders();
        DAL_UserLogin user = new DAL_UserLogin();
        DAL_CustomerIdea cusidea = new DAL_CustomerIdea();
        // GET: Administrator/Orders
        public ActionResult Orders()
        {
            var orderlist = objOrder.TCP_Order_SEL().ToList();
            var cus1 = cusidea.GETALLCustomer(0).ToList();
            var cus2 = user.GETALL(0).ToList();
            List<DTO_CusOrders> model = new List<DTO_CusOrders>();
            foreach(var item in orderlist)
            {
                
                var cus = cus1.Where(m => m.CustomerId == item.CustomerId && item.Type==2).FirstOrDefault();
                if (cus != null)
                {
                    DTO_CusOrders cusorder = new DTO_CusOrders();
                    cusorder.Adress = cus.Adress;
                    cusorder.Name = cus.Name;
                    cusorder.OrderID = item.OrderID;
                    cusorder.Phone = cus.Phone;
                    cusorder.UserID = cus.CustomerId;
                    cusorder.Totals = item.Totals;
                    cusorder.Desciptions = item.Desciptions;
                    cusorder.DateCreated = item.DateCreated.ToString();
                    model.Add(cusorder);
                }
                var cuss = cus2.Where(m => m.UserID == item.CustomerId && item.Type==1).FirstOrDefault();
                if (cuss != null)
                {
                    DTO_CusOrders cusorder = new DTO_CusOrders();
                    cusorder.Adress = cuss.Adress;
                    cusorder.Name = cuss.Name;
                    cusorder.OrderID = item.OrderID;
                    cusorder.Phone = cuss.Phone;
                    cusorder.UserID = cuss.UserID;
                    cusorder.Totals = item.Totals;
                    cusorder.Desciptions = item.Desciptions;
                    cusorder.DateCreated = item.DateCreated.ToString();
                    model.Add(cusorder);
                }
            }
            return View(model);
        }
        public ActionResult OrdersDetail(int OrderID)
        {
            var orderlist = objOrder.TCP_Order_SEL().ToList().Where(m=>m.OrderID==OrderID).ToList();
            var cus1 = cusidea.GETALLCustomer(0).ToList();
            var cus2 = user.GETALL(0).ToList();
            List<DTO_CusOrders> model = new List<DTO_CusOrders>();
            foreach (var item in orderlist)
            {

                var cus = cus1.Where(m => m.CustomerId == item.CustomerId && item.Type == 2).FirstOrDefault();
                if (cus != null)
                {
                    DTO_CusOrders cusorder = new DTO_CusOrders();
                    cusorder.Adress = cus.Adress;
                    cusorder.Name = cus.Name;
                    cusorder.OrderID = item.OrderID;
                    cusorder.Phone = cus.Phone;
                    cusorder.UserID = cus.CustomerId;
                    cusorder.Totals = item.Totals;
                    cusorder.Desciptions = item.Desciptions;
                    cusorder.DateCreated = item.DateCreated.ToString();
                    model.Add(cusorder);
                }
                var cuss = cus2.Where(m => m.UserID == item.CustomerId && item.Type == 1).FirstOrDefault();
                if (cuss != null)
                {
                    DTO_CusOrders cusorder = new DTO_CusOrders();
                    cusorder.Adress = cuss.Adress;
                    cusorder.Name = cuss.Name;
                    cusorder.OrderID = item.OrderID;
                    cusorder.Phone = cuss.Phone;
                    cusorder.UserID = cuss.UserID;
                    cusorder.Totals = item.Totals;
                    cusorder.Desciptions = item.Desciptions;
                    cusorder.DateCreated = item.DateCreated.ToString();
                    model.Add(cusorder);
                }
            }
            ViewBag.OrderDetails = objOrder.TCP_Orderdetail_SEL(OrderID);
            ViewBag.Total = objOrder.TCP_Orderdetail_SEL(OrderID).Sum(m => m.Total);
            return View(model);
        }
    }
}