using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;

namespace TCP.DAL
{
    public class DAL_ProductColors
    {
        public IEnumerable<DTO_ProductColors> GETALL(int ID,int IDProduct)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@IDProduct", IDProduct);
                return DapperHelper.Query<DTO_ProductColors>("TCP_ProductColorSize_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateProductColors(DTO_ProductColors data)
        {
            var param = new DynamicParameters();
            param.Add("@ID", data.ID);
            param.Add("@IDProduct", data.IDProduct);
            param.Add("@IDColor", data.IDColor);
            param.Add("@IDSize", data.IDSize);
            return DapperHelper.Execute("TCP_ProductColorSize_SAVE", param) >= 1 ? true : false;
        }
        public int ProductsColorsDelete(int ID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                return DapperHelper.Execute("TCP_ProductColorSize_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
