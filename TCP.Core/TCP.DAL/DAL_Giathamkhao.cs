using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_Giathamkhao
    {
        public IEnumerable<DTO_Giathamkhao> GETALL(int Id, int ProductId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                param.Add("@ProductId", ProductId);
                return DapperHelper.Query<DTO_Giathamkhao>("TCP_Giathamkhao_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateNewsCategories(DTO_Giathamkhao data)
        {
            var param = new DynamicParameters();
            param.Add("@Id", data.Id);
            param.Add("@Name", data.Name);
            param.Add("@Url", data.Url);
            param.Add("@CreateDate", data.CreateDate);
            param.Add("@ProductId", data.ProductId);
            param.Add("@Price", data.Price);
            return DapperHelper.Execute("TCP_Giathamkhao_SAVE", param) >= 1 ? true : false;
        }
        public int GiathamkhaoDelete(int Id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id ", Id);
                return DapperHelper.Execute("TCP_Giathamkhao_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
