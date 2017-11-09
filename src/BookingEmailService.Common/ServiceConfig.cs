using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using log4net;

namespace BookingEmailService.Common
{
    public sealed class ServiceConfig
    {
        private static readonly ILog _Log = LogManager.GetLogger("log");

        public static int MaxDegreeOfParallelism
        {
            get
            {
                int result = 1;
                string val = GetConfigValByKey("MaxDegreeOfParallelism");
                if (!string.IsNullOrEmpty(val))
                {
                    int.TryParse(val, out result);
                }
                return result;
            }
        }

        private static string GetConfigValByKey(string key)
        {
            string val = string.Empty;
            try
            {
                val = ConfigurationManager.AppSettings[key] == null ? "" :
                    ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                _Log.Error(ex.Message);
            }
            return val;
        }
    }
}
