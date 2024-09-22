using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.DAL;
using TCP.DTO;
namespace TCP.BAL
{
    public class BAL_ProductCategoriesBAL
    {
        DAL_ProductCategories objprodcateDAL = new DAL_ProductCategories();
        public IEnumerable<DTO_ProductCategories> GetCMS_ProductCategories(string strKey, int intParentId, int productCategoryId, int isShow)
        {
            try
            {
                return objprodcateDAL.GETALL(strKey, intParentId,  productCategoryId,  isShow);
            }
            catch (Exception ex)
            { return null; }
        }
    }
}
