using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_ProductColors
    {
        public int ID { get; set; }
        public int IDProduct { get; set; }
        public int IDColor { get; set; }
        public string ColorName { get; set; }
        public string ColorHex { get; set; }
        public string SizeName { get; set; }
        public string IDSize { get; set; }
        public  string multiImage { get; set; }
    }
}
