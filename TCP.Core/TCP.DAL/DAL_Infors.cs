using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_Infors
    {
        public IEnumerable<DTO_Infors> GETALL()
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<DTO_Infors>("TCP_Infors_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateInfors(DTO_Infors data)
        {
            var param = new DynamicParameters();
            param.Add("@Name", data.Name);
            param.Add("@Address", data.Address);
            param.Add("@Hotline", data.Hotline);

            param.Add("@Email", data.Email);
            param.Add("@OpenTimes", data.OpenTimes);
            param.Add("@Facebook", data.Facebook);
            param.Add("@Youtube", data.Youtube);
            param.Add("@Description", data.Description);
            param.Add("@Contents", data.Contents);
            return DapperHelper.Execute("TCP_Infors_SAVE", param) >= 1 ? true : false;
        }
    }
}
