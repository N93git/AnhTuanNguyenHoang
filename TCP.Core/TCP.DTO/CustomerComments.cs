using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class CustomerComments
    {
        public int CommtentId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string ProductUrl { get; set; }
        public int Type { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string Contents { get; set; }
        public int Duyet { get; set; }
        public int Reply { get; set; }
        public DateTime CreateDate { get; set; }
        public int ParentID { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDT { get; set; }

    }
}
