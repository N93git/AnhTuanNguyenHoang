using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
   public  class DTO_Giathamkhao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
