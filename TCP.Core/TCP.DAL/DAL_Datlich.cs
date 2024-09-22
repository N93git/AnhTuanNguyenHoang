using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCP.DTO;

namespace TCP.DAL
{
    public  class DAL_Datlich
    {
        public IEnumerable<DTO_Datlich> GETALL(int DatlichId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@DatlichId", DatlichId);
                return DapperHelper.Query<DTO_Datlich>("DAL_Datlich", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateDatlich(DTO_Datlich data)
        {
            var param = new DynamicParameters(); 
            param.Add("@TenKhachHang", data.TenKhachHang);
            param.Add("@SoDienThoai", data.SoDienThoai);
            param.Add("@DiaChi", data.DiaChi);
            param.Add("@GhiChu", data.GhiChu);
            param.Add("@NgayThucHien", data.NgayThucHien); 
            return DapperHelper.Execute("TCP_Datlich_SAVE", param) >= 1 ? true : false;
        } 
    }
}
