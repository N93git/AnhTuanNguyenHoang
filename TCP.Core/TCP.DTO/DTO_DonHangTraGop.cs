using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_DonHangTraGop
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ProductTitle { get; set; }
        public string ProductUrl { get; set; }
        public string ThoiHan { get; set; }
        public decimal Tratruoc { get; set; }
        public decimal Gopmoithang { get; set; }
        public decimal Tienphaitra { get; set; }
        public string Loai { get; set; }
        public DateTime NgayTao { get; set; }
        public int Duyet { get; set; }
        public string BankName { get; set; }
    }
}
