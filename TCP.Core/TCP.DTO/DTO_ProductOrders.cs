using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_ProductOrders
    {
        public string ProductTitle { get; set; }
        public string ProductTitle1 { get; set; }
        public string ProductTitle2 { get; set; }
        public string ProductImage { get; set; }
        public decimal Prices { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public string Colors { get; set; }
        public string Size { get; set; }
    }
}
