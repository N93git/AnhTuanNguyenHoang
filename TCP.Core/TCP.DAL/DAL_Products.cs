using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public  class DAL_Products
    {
        public bool UpdateProductsCounts(int ProductId)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", ProductId);
            return DapperHelper.Execute("TCP_Products_Counts", param) >= 1 ? true : false;
        }
        public bool UpdateProductshow(int ProductId, int IsShow)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", ProductId);
            param.Add("@IsShow", IsShow);
            return DapperHelper.Execute("TCP_Product_Show", param) >= 1 ? true : false;
        }
        public bool UpdateProductAnTuong(int ProductId, int IsShow)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", ProductId);
            param.Add("@IsShow", IsShow);
            return DapperHelper.Execute("TCP_Product_Antuong", param) >= 1 ? true : false;
        }
        public bool UpdateProductBanchay(int ProductId, int IsShow)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", ProductId);
            param.Add("@IsShow", IsShow);
            return DapperHelper.Execute("TCP_Product_Banchay", param) >= 1 ? true : false;
        }
        public bool UpdateProductMoinhat(int ProductId, int IsShow)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", ProductId);
            param.Add("@IsShow", IsShow);
            return DapperHelper.Execute("TCP_Product_Moinhat", param) >= 1 ? true : false;
        }
        public bool UpdateProductOrder(int ProductId, int DisplayOrder)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", ProductId);
            param.Add("@DisplayOrder", DisplayOrder);
            return DapperHelper.Execute("TCP_Product_Sort", param) >= 1 ? true : false;
        }
        public IEnumerable<DTO_Products> GETALL(string strKey,int ProductId, int isShow,int page,int pageSize)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@strKey", strKey);
                param.Add("@isShow", isShow);
                param.Add("@ProductId", ProductId);
                param.Add("@page", page);
                param.Add("@pageSize", pageSize);
                return DapperHelper.Query<DTO_Products>("TCP_Products_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateProducts(DTO_Products data)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", data.ProductId);
            param.Add("@ProductTitle", data.ProductTitle);
            param.Add("@ProductTitle1", data.ProductTitle1);
            param.Add("@ProductTitle2", data.ProductTitle2);
            param.Add("@ProductUrl", data.ProductUrl);
            param.Add("@ProductImage", data.ProductImage);
            param.Add("@ProductPrice", data.ProductPrice);
            param.Add("@ProductPriceDrop", data.ProductPriceDrop);
            param.Add("@Age", data.Age);
            param.Add("@Weeks", data.Weeks);
            param.Add("@ProductDesc", data.ProductDesc);
            param.Add("@ProductContents", data.ProductContents);
            param.Add("@ProductContents1", data.ProductContents1);
            param.Add("@ProductContents2", data.ProductContents2);
            param.Add("@Quydinhdoihang", data.Quydinhdoihang);
            param.Add("@Quydinhdoihang1", data.Quydinhdoihang1);
            param.Add("@Quydinhdoihang2", data.Quydinhdoihang2);
            param.Add("@Phivanchuyen", data.Phivanchuyen);
            param.Add("@Phivanchuyen1", data.Phivanchuyen1);
            param.Add("@Phivanchuyen2", data.Phivanchuyen2);
            param.Add("@DisplayOrder", data.DisplayOrder);
            param.Add("@IsShow", data.IsShow);
            param.Add("@ParentID", data.ParentID);
            param.Add("@CreatedBy", data.CreatedBy);

            param.Add("@Title", data.Title);
            param.Add("@Url", data.Url);
            param.Add("@MetaDescription", data.MetaDescription);
            param.Add("@MetaKeywords", data.MetaKeywords);
            return DapperHelper.Execute("TCP_Products_SAVE", param) >= 1 ? true : false;
        }

        //

        public bool UpdateProductImg(int ProductId, int ProductId1,string Images)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", ProductId);
            param.Add("@ProductId1", ProductId1);
            param.Add("@Images", Images);
            return DapperHelper.Execute("TCP_Img_SAVE", param) >= 1 ? true : false;
        }

        //

        public IEnumerable<DTO_Images> GETImages(int ProductId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductId", ProductId);
                return DapperHelper.Query<DTO_Images>("TCP_Img_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool DeleteProductImage(int ProductId,string Images)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", ProductId);
            param.Add("@Images", Images);
            var result= DapperHelper.Execute("TCP_Delete_Img", param);

            return true;

        }
        public int ProductsDelete(int ProductID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ProductId", ProductID);
                return DapperHelper.Execute("TCP_Products_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public bool TCP_ProductChangePrice(int ProductId, decimal ProductPrice, decimal ProductPriceDrop)
        {
            var param = new DynamicParameters();
            param.Add("@ProductId", ProductId);
            param.Add("@ProductPrice", ProductPrice);
            param.Add("@ProductPriceDrop", ProductPriceDrop);
            return DapperHelper.Execute("TCP_ProductChangePrice", param) >= 1 ? true : false;
        }
    }
}
