using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_NewsCategories
    {
        public int NewsCategoryId { get; set; }
        public string NewsCategoryTitle { get; set; }
        public string NewsCategoryTitle1 { get; set; }
        public string NewsCategoryTitle2 { get; set; }
        public string NewsCategoryUrl { get; set; }
        public string NewsCategoryImage { get; set; }
        public string NewsCategoryDesc { get; set; }
        public string NewsCategoryDesc1 { get; set; }
        public string NewsCategoryDesc2 { get; set; }
        public string NewsCategoryContents { get; set; }
        public int DisplayOrder { get; set; }
        public int IsShow { get; set; }
        public int ParentID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


        public string Title { get; set; }
        public string Url { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
    }
}
