using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_Bank
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
        public float Month12 { get; set; }
        public float Month9 { get; set; }
        public float Month6 { get; set; }
        public float Month3 { get; set; }
        public string Images { get; set; }
    }
}
