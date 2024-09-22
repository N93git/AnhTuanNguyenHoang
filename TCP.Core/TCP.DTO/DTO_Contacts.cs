using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_Contacts
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactAddress { get; set; }

        public string ContactEmail { get; set; }
        public string DanhMuc { get; set; }
        public string ContactContent { get; set; }
        public string ContactTime { get; set; }
    }
}
