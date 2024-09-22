using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_ProductCategories
    {

        public bool UpdateProductCateDisplay(int ProductCategoryId, int IsShow)
        {
            var param = new DynamicParameters();
            param.Add("@ProductCategoryId", ProductCategoryId);
            param.Add("@IsShow ", IsShow);
            return DapperHelper.Execute("TCP_ProductCate_Show", param) >= 1 ? true : false;
        }
        public bool UpdateProductCategoriesOrder(int ProductCategoryId,int DisplayOrder)
        {
            var param = new DynamicParameters();
            param.Add("@ProductCategoryId", ProductCategoryId);
            param.Add("@DisplayOrder", DisplayOrder);
            return DapperHelper.Execute("TCP_ProductCategories_Sort", param) >= 1 ? true : false;
        }

        public IEnumerable<DTO_ProductCategories> GETALL(string strKey, int intParentId,int productCategoryId,int isShow)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@strKey", strKey);
                param.Add("@parentId", intParentId);
                param.Add("@productCategoryId", productCategoryId);
                param.Add("@isShow", isShow);
                return DapperHelper.Query<DTO_ProductCategories>("TCP_ProductCategories_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateProductCategories(DTO_ProductCategories data)
        {
            var param = new DynamicParameters();
            param.Add("@ProductCategoryId", data.ProductCategoryId);
            param.Add("@ProductCategoryTitle", data.ProductCategoryTitle);
            param.Add("@ProductCategoryTitle1", data.ProductCategoryTitle1);
            param.Add("@ProductCategoryTitle2", data.ProductCategoryTitle2);
            param.Add("@ProductCategoryUrl", data.ProductCategoryUrl);
            param.Add("@ProductCategoryImage", data.ProductCategoryImage);
            param.Add("@ProductCategoryDesc", data.ProductCategoryDesc);
            param.Add("@ProductCategoryContents", data.ProductCategoryContents);
            param.Add("@DisplayOrder", data.DisplayOrder);
            param.Add("@IsShow", data.IsShow);
            param.Add("@ParentID", data.ParentID);
            param.Add("@CreatedBy", data.CreatedBy);

            param.Add("@Title", data.Title);
            param.Add("@Url", data.Url);
            param.Add("@MetaDescription", data.MetaDescription);
            param.Add("@MetaKeywords", data.MetaKeywords);
            return DapperHelper.Execute("TCP_ProductCategories_SAVE", param) >= 1 ? true : false;
        }
        public int ProductcategoryDelete(int productCategoryId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@productCategoryId", productCategoryId);
                return DapperHelper.Execute("TCP_ProductCategories_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
