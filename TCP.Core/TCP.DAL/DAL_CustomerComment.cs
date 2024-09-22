using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_CustomerComment
    {
        public IEnumerable<DTO_CustomerComment> GETALL(int CustomerId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CustomerId", CustomerId);
                return DapperHelper.Query<DTO_CustomerComment>("TCP_Customer_Comment_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public IEnumerable<DTO_Comment> GETALL2(int CommtentId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CommtentId", CommtentId);
                return DapperHelper.Query<DTO_Comment>("TCP_Comments_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateNewsCategories(DTO_CustomerComment data)
        {
            var param = new DynamicParameters();
            param.Add("@CustomerId", data.CustomerId);
            param.Add("@Name", data.Name);
            param.Add("@Phone", data.Phone);
            param.Add("@Email", data.Email);
            return DapperHelper.Execute("TCP_Customer_Comment_SAVE", param) >= 1 ? true : false;
        }
        public bool UpdateComments(DTO_Comment data)
        {
            var param = new DynamicParameters();
            param.Add("@CommtentId", data.CommtentId);
            param.Add("@CustomerId", data.CustomerId);
            param.Add("@Type", data.Type);
            param.Add("@Duyet", data.Duyet);
            param.Add("@Contents", data.Contents);
            param.Add("@ParentID", data.ParentID);
            param.Add("@ProductId", data.ProductId);
            param.Add("@Displayorder", data.Displayorder);
            param.Add("@Replyfor", data.Replyfor);
            param.Add("@HoTen", data.HoTen);
            param.Add("@Email", data.Email);
            param.Add("@SoDT", data.SoDT);
            return DapperHelper.Execute("TCP_Comments_SAVE", param) >= 1 ? true : false;
        }
        public bool UpdateNewComments(DTO_NewsComment data)
        {
            var param = new DynamicParameters();
            param.Add("@CommtentId", data.CommtentId);
            param.Add("@CustomerId", data.CustomerId);
            param.Add("@Type", data.Type);
            param.Add("@Duyet", data.Duyet);
            param.Add("@Contents", data.Contents);
            param.Add("@ParentID", data.ParentID);
            param.Add("@NewsID", data.NewsID);
            param.Add("@Displayorder", data.Displayorder);
            param.Add("@Replyfor", data.Replyfor);
            param.Add("@HoTen", data.HoTen);
            param.Add("@Email", data.Email);
            param.Add("@SoDT", data.SoDT);
            return DapperHelper.Execute("TCP_New_Comments_SAVE", param) >= 1 ? true : false;
        }
        public bool Comments_Duyet(int CommtentId, int Duyet)
        {
            var param = new DynamicParameters();
            param.Add("@CommtentId", CommtentId);
            param.Add("@Duyet", Duyet);
            return DapperHelper.Execute("TCP_CommentsNews_Duyet", param) >= 1 ? true : false;
        }
        public bool Comments_Reply(int CommtentId)
        {
            var param = new DynamicParameters();
            param.Add("@CommtentId", CommtentId);
            return DapperHelper.Execute("TCP_CommentsNew_Reply", param) >= 1 ? true : false;
        }
    }
}
