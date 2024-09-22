using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_KhoaHoc
    {
   
        public IEnumerable<DTO_KhoaHocProduct> GetAll()
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<DTO_KhoaHocProduct>("TCP_KhoaHoc_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateKhoaHoc(DTO_KhoaHoc data)
        {
            var param = new DynamicParameters();
            param.Add("@ParentName", data.ParentName);
            param.Add("@Email", data.Email);
            param.Add("@Phone", data.Phone);
            param.Add("@ChildName", data.ChildName);
            param.Add("@ProductID", data.ProductID);
            param.Add("@Contents", data.Contents);
            param.Add("@DateCreated", data.DateCreated);
            return DapperHelper.Execute("TCP_KhoaHoc_SAVE", param) >= 1 ? true : false;
        }
    }
}
