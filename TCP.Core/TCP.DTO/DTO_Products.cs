using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.DTO
{
    public class DTO_Products
    {
        public int ProductId { get; set; }
        public string ParentID { get; set; }
        public string ProductTitle { get; set; }
        public string ProductTitle1 { get; set; }
        public string ProductTitle2 { get; set; }
        public string ProductUrl { get; set; }
        public string ProductImage { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductPriceEN { get; set; }
        public decimal ProductPriceFR { get; set; }
        public decimal LastPrice { get; set; }
        public decimal LastPriceEN { get; set; }
        public decimal LastPriceFR { get; set; }
        public decimal ProductPriceDrop { get; set; }
        public decimal ProductPriceDropEN { get; set; }
        public decimal ProductPriceDropFR { get; set; }
        public string Age { get; set; }
        public string Weeks { get; set; }
        public string ProductDesc { get; set; }
        public string ProductContents { get; set; }
        public string ProductContents1 { get; set; }
        public string ProductContents2 { get; set; }
        //
        public string Quydinhdoihang { get; set; }
        public string Quydinhdoihang1 { get; set; }
        public string Quydinhdoihang2 { get; set; }
        public string Phivanchuyen { get; set; }
        public string Phivanchuyen1 { get; set; }
        public string Phivanchuyen2 { get; set; }

        public int DisplayOrder { get; set; }
        public int IsShow { get; set; }
        public int Antuong { get; set; }
        public int Banchay { get; set; }
        public int Moinhat { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


        public string Title { get; set; }
        public string Url { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string multiImage { get; set; }
    }
}
