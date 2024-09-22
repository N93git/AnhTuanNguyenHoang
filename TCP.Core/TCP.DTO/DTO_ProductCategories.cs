using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_ProductCategories
    {
       public int ProductCategoryId { get; set; }
        public string ProductCategoryTitle { get; set; }
        public string ProductCategoryTitle1 { get; set; }
        public string ProductCategoryTitle2 { get; set; }
        public string ProductCategoryUrl { get; set; }
        public string ProductCategoryImage { get; set; }
        public string ProductCategoryDesc { get; set; }
        public string ProductCategoryContents { get; set; }
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
