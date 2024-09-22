using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
   public class DAL_News
    {
        public bool UpdateNewsCounts(int NewsId, int Counts)
        { 
            var param = new DynamicParameters();
            param.Add("@NewsId", NewsId);
            param.Add("@Counts", Counts);
            return DapperHelper.Execute("TCP_News_Counts", param) >= 1 ? true : false;
        }
        public bool UpdateProductCategoriesOrder(int NewsId, int DisplayOrder)
        {
            var param = new DynamicParameters();
            param.Add("@NewsId", NewsId);
            param.Add("@DisplayOrder", DisplayOrder);
            return DapperHelper.Execute("TCP_News_Sort", param) >= 1 ? true : false;
        }
        public IEnumerable<DTO_News> GETALL(string strKey, int NewsId, int isShow, int page, int pageSize)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@strKey", strKey);
                param.Add("@isShow", isShow);
                param.Add("@NewsId", NewsId);
                param.Add("@page", page);
                param.Add("@pageSize", pageSize);
                return DapperHelper.Query<DTO_News>("TCP_News_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateNews(DTO_News data)
        {
            var param = new DynamicParameters();
            param.Add("@NewsId", data.NewsId);
            param.Add("@NewsTitle", data.NewsTitle);
            param.Add("@NewsTitle1", data.NewsTitle1);
            param.Add("@NewsTitle2", data.NewsTitle2);
            param.Add("@NewsUrl", data.NewsUrl);
            param.Add("@NewsImage", data.NewsImage);
            param.Add("@NewsDesc", data.NewsDesc);
            param.Add("@NewsDesc1", data.NewsDesc1);
            param.Add("@NewsDesc2", data.NewsDesc2);
            param.Add("@NewsContents", data.NewsContents);
            param.Add("@NewsContents1", data.NewsContents1);
            param.Add("@NewsContents2", data.NewsContents2);
            param.Add("@DisplayOrder", data.DisplayOrder);
            param.Add("@IsShow", data.IsShow);
            param.Add("@ParentID", data.ParentID);
            param.Add("@CreatedBy", data.CreatedBy);

            param.Add("@Title", data.Title);
            param.Add("@Url", data.Url);
            param.Add("@MetaDescription", data.MetaDescription);
            param.Add("@MetaKeywords", data.MetaKeywords);
            return DapperHelper.Execute("TCP_News_SAVE", param) >= 1 ? true : false;
        }

        //

        public int NewsDelete(int NewsID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@NewsId", NewsID);
                return DapperHelper.Execute("TCP_News_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }

    }
}
