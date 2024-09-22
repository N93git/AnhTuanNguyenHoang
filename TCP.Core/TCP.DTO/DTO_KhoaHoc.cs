using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_KhoaHoc
    {
        public int ID { get; set; }
        public string ParentName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ChildName { get; set; }
        public int ProductID { get; set; }
        public string Contents { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class DTO_KhoaHocProduct
    {
        public int ID { get; set; }
        public string ParentName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ChildName { get; set; }
        public int ProductID { get; set; }
        public string Contents { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProductTitle { get; set; }
    }
}
