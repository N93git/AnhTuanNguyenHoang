using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;

namespace TCP.DAL
{
    public class SupportBLL
    {
        public IEnumerable<ThongKeTruyCap> GetLuotTruyCap()
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<ThongKeTruyCap>("spThongKe_Edit", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
