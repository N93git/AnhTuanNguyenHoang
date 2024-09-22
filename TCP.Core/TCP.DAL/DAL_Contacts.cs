using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_Contacts
    {
        public IEnumerable<DTO_Contacts> GETALL()
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<DTO_Contacts>("TCP_Contact_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public IEnumerable<DTO_Contacts> GETALLDatLich()
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<DTO_Contacts>("TCP_Datlich_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateInfors(DTO_Contacts data)
        {
            var param = new DynamicParameters();
            param.Add("@ContactName", data.ContactName);
            param.Add("@ContactPhone", data.ContactPhone);
            param.Add("@ContactAddress", data.ContactAddress);
            param.Add("@ContactEmail", data.ContactEmail);
            param.Add("@DanhMuc", data.DanhMuc);
            param.Add("@ContactContent", data.ContactContent);
            param.Add("@ContactTime", data.ContactTime);
            return DapperHelper.Execute("TCP_Contacts_SAVE", param) >= 1 ? true : false;

        }
        public bool UpdateDonHangTraGop(DTO_DonHangTraGop data)
        {
            var param = new DynamicParameters();
            param.Add("@Name", data.Name);
            param.Add("@Email", data.Email);
            param.Add("@Phone", data.Phone);
            param.Add("@Address", data.Address);
            param.Add("@ProductTitle", data.ProductTitle);
            param.Add("@ProductUrl", data.ProductUrl);
            param.Add("@ThoiHan", data.ThoiHan);
            param.Add("@Tratruoc", data.Tratruoc);
            param.Add("@Gopmoithang", data.Gopmoithang);
            param.Add("@Tienphaitra", data.Tienphaitra);
            param.Add("@Loai", data.Loai);
            param.Add("@BankName", data.BankName);
            return DapperHelper.Execute("TCP_DonHangTraGop_SAVE", param) >= 1 ? true : false;
        }
        public IEnumerable<DTO_DonHangTraGop> GETALLDonHangTraGop(int ID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                return DapperHelper.Query<DTO_DonHangTraGop>("TCP_DonHangTraGop_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool DonHangTraGop_Duyet(int ID)
        {
            var param = new DynamicParameters();
            param.Add("@ID", ID);
            return DapperHelper.Execute("TCP_DonHangTraGop_Duyet", param) >= 1 ? true : false;
        }
    }
}
