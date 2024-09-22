using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TCP.DTO;

namespace TCP.DAL
{
    public class DAL_Settings
    {
        public IEnumerable<DTO_Settings> GETALL(int NewsCategoryId)
        {
            try
            {
                var param = new DynamicParameters();
                return DapperHelper.Query<DTO_Settings>("TCP_Settings_SEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
        public bool UpdateSettings(DTO_Settings data)
        {
            var param = new DynamicParameters();
            param.Add("@Logo", data.Logo);
            param.Add("@Favicon", data.Favicon);
            param.Add("@Banner1", data.Banner1);
            param.Add("@Banner2", data.Banner2);
            param.Add("@Title", data.Title);
            param.Add("@currencyUSA", data.currencyUSA);
            param.Add("@currencyFrance", data.currencyFrance);
            param.Add("@Metadecription", data.Metadecription);
            return DapperHelper.Execute("TCP_Settings_SAVE", param) >= 1 ? true : false;
        }
        public int SettingsDelete(int ScriptID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ScriptID ", ScriptID);
                return DapperHelper.Execute("TCP_Settings_DEL", param);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
