using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_Menus
    {
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int RelateId { get; set; }
        public int TemplateId { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string ControllerName { get; set; }
        public string ViewName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Priority { get; set; }
    }
}
