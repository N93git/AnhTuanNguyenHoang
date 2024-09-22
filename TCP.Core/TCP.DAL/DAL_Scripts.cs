using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;

namespace TCP.DAL
{
    public class DAL_Scripts
    {
       
        public IEnumerable<DTO_Scripts> GETALL(int NewsCategoryId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ScriptID", NewsCategoryId);
                return DapperHelper.Query<DTO_Scripts>("TCP_Scipts_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateNewsCategories(DTO_Scripts data)
        {
            var param = new DynamicParameters();
            param.Add("@ScriptID", data.ScriptID);
            param.Add("@Name", data.Name);
            param.Add("@Contents", data.Contents);
            param.Add("@Position", data.Position);
            return DapperHelper.Execute("TCP_Scipts_SAVE", param) >= 1 ? true : false;
        }
        public int ScriptDelete(int ScriptID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ScriptID ", ScriptID);
                return DapperHelper.Execute("TCP_Scipts_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}