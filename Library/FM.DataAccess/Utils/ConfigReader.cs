using System;
using System.Configuration;

namespace Service.DataAccess.Utils
{
    /// <summary>
    /// 配置文件读取类
    /// </summary>
    public class ConfigReader
    {
        public static string Read(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
