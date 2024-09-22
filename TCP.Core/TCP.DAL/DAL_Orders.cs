using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;

namespace TCP.DAL
{
    public class DAL_Orders
    {
        public bool UpdateProducts(DTO_Customer data)
        {
            var param = new DynamicParameters();
            param.Add("@Name", data.Name);
            param.Add("@Adress", data.Adress);
            param.Add("@Phone", data.Phone);
            param.Add("@Desciptions", data.Desciptions);
            return DapperHelper.Execute("TCP_Customer_SAVE", param) >= 1 ? true : false;
        }

        public bool UpdateOrders(string Desciptions,float totals,int Type,int Customerid)
        {
            var param = new DynamicParameters();
            param.Add("@Desciptions", Desciptions);
            param.Add("@CustomerId", Customerid);
            param.Add("@Totals", totals);
            param.Add("@Type", Type);
            return DapperHelper.Execute("TCP_Orders_SAVE", param) >= 1 ? true : false;
        }
        public bool UpdateOrdersDetail(DTO_OrderDetail data)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", data.ProductId);
            param.Add("@Amount", data.Amount);
            param.Add("@Prices", data.Prices);
            param.Add("@Colors", data.Colors);
            param.Add("@Size", data.Size);
            return DapperHelper.Execute("TCP_Orderdetail_SAVE", param) >= 1 ? true : false;
        }
        public IEnumerable<DTO_CusOrders> TCP_CusOrder_SEL()
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<DTO_CusOrders>("TCP_CusOrder_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public IEnumerable<DTO_Orders> TCP_Order_SEL()
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<DTO_Orders>("TCP_Order_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public IEnumerable<DTO_ProductOrders> TCP_Orderdetail_SEL(int OrderID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@OrderID", OrderID);    
                return DapperHelper.Query<DTO_ProductOrders>("TCP_Orderdetail_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
