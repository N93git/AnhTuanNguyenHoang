using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_UserLogin
    {
        public IEnumerable<DTO_UserLogin> GETALL(int UserID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserID", UserID);
                return DapperHelper.Query<DTO_UserLogin>("TCP_Userlogin_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateInfors(DTO_UserLogin data)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", data.UserID);
            param.Add("@Name", data.Name);
            param.Add("@Email", data.Email);
            param.Add("@Password", data.Password);
            param.Add("@Phone", data.Phone);
            param.Add("@Adress", data.Adress);
            return DapperHelper.Execute("TCP_UserLogin_SAVE", param) >= 1 ? true : false;
        }
    }
}
