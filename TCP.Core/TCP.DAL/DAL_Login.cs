using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;

namespace TCP.DAL
{
    public class DAL_Login
    {
        public IEnumerable<DTO_Login> TCP_Logincheck(string UserName, string Password)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", UserName);
                param.Add("@Password", Password);
                return DapperHelper.Query<DTO_Login>("TCP_Logincheck", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
