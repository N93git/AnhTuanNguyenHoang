using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public  class DTO_NewsComment
    {
        public int CommtentId { get; set; }
        public int CustomerId { get; set; }
        public int Type { get; set; }
        public int Duyet { get; set; }
        public int Reply { get; set; }
        public string Contents { get; set; }
        public int ParentID { get; set; }
        public int NewsID { get; set; }
        public int Displayorder { get; set; }
        public DateTime CreateDate { get; set; }
        public string Replyfor { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDT { get; set; }
    }
}
