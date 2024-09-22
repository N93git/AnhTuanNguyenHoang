using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;

namespace TCP.DAL
{
    public class DAL_Colors
    {
        public IEnumerable<DTO_Colors> GETALL( int ID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                return DapperHelper.Query<DTO_Colors>("TCP_Colors_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateColors(DTO_Colors data)
        {
            var param = new DynamicParameters();
            param.Add("@ID", data.ID);
            param.Add("@Name", data.Name);
            param.Add("@Hex", data.Hex);
            return DapperHelper.Execute("TCP_Colors_SAVE", param) >= 1 ? true : false;
        }
        public int ColorsDelete(int ID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                return DapperHelper.Execute("CP_Colors_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
