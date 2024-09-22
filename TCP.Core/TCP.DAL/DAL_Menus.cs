using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_Menus
    {
        public IEnumerable<DTO_Menus> GetSiteMap(string actionBy)
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<DTO_Menus>("GenerateSitemap", actionBy, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<DTO_Menus> GETALL(int intParentId,int templateid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RelateId", intParentId);
                param.Add("@templateid", templateid);
                return DapperHelper.Query<DTO_Menus>("TCP_Menu_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public IEnumerable<DTO_Menus> GETMenuTemplate(string url)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@url", url);
                return DapperHelper.Query<DTO_Menus>("TCP_Menu_Template_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
