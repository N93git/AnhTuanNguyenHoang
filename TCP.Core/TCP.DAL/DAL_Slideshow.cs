using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;
namespace TCP.DAL
{
    public class DAL_Slideshow
    {
        public bool UpdateSlideshowOrder(int NewsId, int DisplayOrder)
        {
            var param = new DynamicParameters();
            param.Add("@SlideshowId", NewsId);
            param.Add("@DisplayOrder", DisplayOrder);
            return DapperHelper.Execute("TCP_News_Slideshow", param) >= 1 ? true : false;
        }
        public IEnumerable<DTO_Slideshows> GETALL(int idslideshow)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@SlideshowId", idslideshow);
                return DapperHelper.Query<DTO_Slideshows>("TCP_Slideshow_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateNewsCategories(DTO_Slideshows data)
        {
            var param = new DynamicParameters();
            param.Add("@SlideshowId", data.SlideshowId);
            param.Add("@SlideshowName", data.SlideshowName);
            param.Add("@Images", data.Images);
            return DapperHelper.Execute("TCP_Slideshow_SAVE", param) >= 1 ? true : false;
        }
        public int NewsCategoryDelete(int NewsCategoryId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@SlideshowId ", NewsCategoryId);
                return DapperHelper.Execute("TCP_Slideshow_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
