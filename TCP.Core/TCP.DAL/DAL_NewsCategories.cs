using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_NewsCategories
    {
        public bool UpdateNewsshow(int NewsCategoryId, int IsShow)
        {
            var param = new DynamicParameters();
            param.Add("@NewsCategoryId", NewsCategoryId);
            param.Add("@IsShow", IsShow);
            return DapperHelper.Execute("TCP_NewsCategories_Show", param) >= 1 ? true : false;
        }
        public bool UpdateProductCategoriesOrder(int NewsCategoryId, int DisplayOrder)
        {
            var param = new DynamicParameters();
            param.Add("@NewsCategoryId", NewsCategoryId);
            param.Add("@DisplayOrder", DisplayOrder);
            return DapperHelper.Execute("TCP_NewsCategories_Sort", param) >= 1 ? true : false;
        }
        public IEnumerable<DTO_NewsCategories> GETALL(string strKey, int intParentId, int NewsCategoryId, int isShow)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@strKey", strKey);
                param.Add("@parentId", intParentId);
                param.Add("@NewsCategoryId", NewsCategoryId);
                param.Add("@isShow", isShow);
                return DapperHelper.Query<DTO_NewsCategories>("TCP_NewsCategories_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateNewsCategories(DTO_NewsCategories data)
        {
            var param = new DynamicParameters();
            param.Add("@NewsCategoryId", data.NewsCategoryId);
            param.Add("@NewsCategoryTitle", data.NewsCategoryTitle);
            param.Add("@NewsCategoryTitle1", data.NewsCategoryTitle1);
            param.Add("@NewsCategoryTitle2", data.NewsCategoryTitle2);
            param.Add("@NewsCategoryUrl", data.NewsCategoryUrl);
            param.Add("@NewsCategoryImage", data.NewsCategoryImage);
            param.Add("@NewsCategoryDesc", data.NewsCategoryDesc);
            param.Add("@NewsCategoryDesc1", data.NewsCategoryDesc1);
            param.Add("@NewsCategoryDesc2", data.NewsCategoryDesc2);
            param.Add("@NewsCategoryContents", data.NewsCategoryContents);
            param.Add("@DisplayOrder", data.DisplayOrder);
            param.Add("@IsShow", data.IsShow);
            param.Add("@ParentID", data.ParentID);
            param.Add("@CreatedBy", data.CreatedBy);

            param.Add("@Title", data.Title);
            param.Add("@Url", data.Url);
            param.Add("@MetaDescription", data.MetaDescription);
            param.Add("@MetaKeywords", data.MetaKeywords);
            return DapperHelper.Execute("TCP_NewsCategories_SAVE", param) >= 1 ? true : false;
        }
        public int NewsCategoryDelete(int NewsCategoryId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@NewsCategoryId ", NewsCategoryId);
                return DapperHelper.Execute("TCP_NewsCategories_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
