using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_Orders
    {
        public int OrderID { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Desciptions { get; set; }
        public decimal Totals { get; set; }
        public int Status { get; set; }
        public bool IsDelete { get; set; }
        public int Type { get; set; }
    }
}
