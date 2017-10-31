using System;
using System.Reflection;
using Service.DataAccess.MappingExcuter;
using Service.DataAccess.Utils;

namespace Service.DataAccess
{
    public sealed class ExecutorFactory
    {
        public static DataExecutor Create()
        {
            return Create(null);
        }

        public static DataExecutor Create(string dataBaseTypeKey)
        {
            return Create(dataBaseTypeKey, null);
        }

        public static DataExecutor Create(string dataBaseTypeKey, string connStrKey)
        {
            if (string.IsNullOrEmpty(dataBaseTypeKey))
            {
                dataBaseTypeKey = "DBExecutor";
            }

            string[] sltDataBaseType = ConfigReader.Read(dataBaseTypeKey).Split(',');
            string asselblyName = sltDataBaseType[0];
            string nameSpace = sltDataBaseType[1].Trim();
            Assembly assembly = Assembly.Load(asselblyName);
            DataExecutor execObj = assembly.CreateInstance(nameSpace) as DataExecutor;
            execObj.SetConnectionString(connStrKey);

            return execObj;
        }
    }
}
