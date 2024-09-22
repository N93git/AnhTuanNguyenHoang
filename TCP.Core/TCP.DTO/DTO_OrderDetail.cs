using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public float Prices { get; set; }
        public string Colors { get; set; }
        public string Size { get; set; }
    }
}
