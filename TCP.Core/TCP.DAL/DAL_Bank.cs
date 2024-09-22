using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;

namespace TCP.DAL
{
   public class DAL_Bank
    {
        public IEnumerable<DTO_Bank> GETALL(int BankID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@BankID", BankID);
                return DapperHelper.Query<DTO_Bank>("TCP_Bank_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateBank(DTO_Bank data)
        {
            var param = new DynamicParameters();
            param.Add("@BankID", data.BankID);
            param.Add("@BankName", data.BankName);
            param.Add("@Month12", data.Month12);
            param.Add("@Month9", data.Month9);
            param.Add("@Month6", data.Month6);
            param.Add("@Month3", data.Month3);
            param.Add("@Images", data.Images);
            return DapperHelper.Execute("TCP_Bank_SAVE", param) >= 1 ? true : false;
        }
        public int BankDelete(int ID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@BankID", ID);
                return DapperHelper.Execute("TCP_Bank_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
