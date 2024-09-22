using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_Sizes
    {
        public IEnumerable<DTO_Sizes> GETALL(int ID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                return DapperHelper.Query<DTO_Sizes>("TCP_Sizes_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateColors(DTO_Sizes data)
        {
            var param = new DynamicParameters();
            param.Add("@ID", data.ID);
            param.Add("@Name", data.Name);
            param.Add("@Notes", data.Notes);
            return DapperHelper.Execute("TCP_Sizes_SAVE", param) >= 1 ? true : false;
        }
        public int SizesDelete(int ID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                return DapperHelper.Execute("TCP_Sizes_Delete", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
