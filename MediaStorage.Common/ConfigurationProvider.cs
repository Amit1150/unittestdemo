using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStorage.Common
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        /// <summary>
        /// Gets application setting.
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public string GetAppSetting(string settingName)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(settingName);
        }

        /// <summary>
        /// Gets database connection string.
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public string GetDBConnectionString(string settingName)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[settingName].ToString();
        }
    }
}
