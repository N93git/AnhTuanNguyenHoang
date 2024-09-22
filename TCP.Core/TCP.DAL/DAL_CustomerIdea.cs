using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
   public  class DAL_CustomerIdea
    {
        public IEnumerable<DTO_Customer> GETALLCustomer(int ID)
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<DTO_Customer>("TCP_Getlastcustomer", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public IEnumerable<DTO_CustomerIdea> GETALL(int ID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                return DapperHelper.Query<DTO_CustomerIdea>("TCP_Customer_Idea_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateCustomerIdea(DTO_CustomerIdea data)
        {
            var param = new DynamicParameters();
            param.Add("@ID", data.ID);
            param.Add("@Name", data.Name);
            param.Add("@Name1", data.Name1);
            param.Add("@Name2", data.Name2);
            param.Add("@Contents", data.Contents);
            param.Add("@Contents1", data.Contents1);
            param.Add("@Contents2", data.Contents2);
            param.Add("@Images", data.Images);
            return DapperHelper.Execute("TCP_Customer_Idea_SAVE", param) >= 1 ? true : false;
        }
        public int CustomerIdeaDelete(int ID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID ", ID);
                return DapperHelper.Execute("TCP_Customer_Idea_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
