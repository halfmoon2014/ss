using System.Reflection;
namespace Service.DAL
{
    /// <summary>
    /// 数据库执行者工厂模式
    /// </summary>
    sealed class ExecutorFactory
    {
        public static DataExecutor Create()
        {
            return Create(null);
        }

        public static DataExecutor Create(string dataBaseTypeKey)
        {
            return Create(dataBaseTypeKey, null);
        }

        public static DataExecutor Create(string dataBaseTypeKey, string connString)
        {
            if (string.IsNullOrEmpty(dataBaseTypeKey))            
                dataBaseTypeKey = "DExecutor";
            
            if (string.IsNullOrEmpty(connString))            
                connString =MyTy.ConfigReader.Read("DBCon");
            
            string[] sltDataBaseType = MyTy.ConfigReader.Read(dataBaseTypeKey).Split(',');
            string asselblyName = sltDataBaseType[0];
            string nameSpace = sltDataBaseType[1].Trim();
            Assembly assembly = Assembly.Load(asselblyName);
            DataExecutor execObj = assembly.CreateInstance(nameSpace) as DataExecutor;
            execObj.GetInstant().SetConnectionString(connString);

            return execObj;
        }
    }
}
