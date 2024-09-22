using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_Doitac
    {
        public IEnumerable<DTO_Doitac> GETALL(int idslideshow)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@DoitacID", idslideshow);
                return DapperHelper.Query<DTO_Doitac>("TCP_Doitac_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateNewsCategories(DTO_Doitac data)
        {
            var param = new DynamicParameters();
            param.Add("@DoitacID", data.DoitacID);
            param.Add("@Name", data.Name);
            param.Add("@Title", data.Title);
            param.Add("@Contents", data.Contents);
            param.Add("@Images", data.Images);
            return DapperHelper.Execute("TCP_Doitac_SAVE", param) >= 1 ? true : false;
        }
        public int NewsCategoryDelete(int NewsCategoryId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@DoitacID ", NewsCategoryId);
                return DapperHelper.Execute("TCP_Doitac_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
