using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public  class DTO_Slideshows
    {
        public int SlideshowId { get; set; }
        public string SlideshowName { get; set; }
        public string Images { get; set; }
        public int DisplayOrder { get; set; }
    }
}
