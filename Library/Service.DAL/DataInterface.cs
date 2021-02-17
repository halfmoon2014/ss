using System.Data;
namespace Service.DAL
{
    public class DALInterface
    {
        readonly Service.DAL.DataExecutor execObj = null;
        public DALInterface()
        {
           execObj = Service.DAL.ExecutorFactory.Create(null);
        }
        public DALInterface(string dataBaseTypeKey)
        {
            execObj = Service.DAL.ExecutorFactory.Create(dataBaseTypeKey);
        }
        public DALInterface(string dataBaseTypeKey, string connString)
        {
            execObj = Service.DAL.ExecutorFactory.Create(dataBaseTypeKey, connString);
        }
        public void SetConnectionString(string connectionString){
            execObj.GetInstant().SetConnectionString(connectionString);
        }
        public void GetConnectionString()
        {
            execObj.GetInstant().GetConnectionString();
        }
        /// <summary>
        /// 执行SQL文本命令,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet SubmitTextDataSet(string commandText)
        {
            return execObj.GetInstant().SubmitTextDataSet(commandText);
        }
        /// <summary>
        /// 执行SQL文本命令,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DataSet SubmitTextDataSet(string commandText, string connectionString)
        {
            return execObj.GetInstant().SubmitTextDataSet(commandText, connectionString);
        }
        /// <summary>
        /// 执行SQL文本命令,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int SubmitTextInt(string commandText)
        {
            return execObj.GetInstant().SubmitTextInt(commandText);
        }
        /// <summary>
        /// 执行SQL文本命令,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public int SubmitTextInt(string commandText, string connectionString)
        {
            return execObj.GetInstant().SubmitTextInt(commandText, connectionString);
        }


        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet SubmitStoredProcedureDataSet(string commandText)
        {
            return execObj.GetInstant().SubmitStoredProcedureDataSet(commandText);
        }
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DataSet SubmitStoredProcedureDataSet(string commandText, string connectionString)
        {
            return execObj.GetInstant().SubmitStoredProcedureDataSet(commandText, connectionString);
        }
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet SubmitStoredProcedureDataSet(string commandText, string connectionString, System.Data.SqlClient.SqlParameter[] commandParameters)
        {            
            return execObj.GetInstant().SubmitStoredProcedureDataSet(commandText, connectionString, commandParameters);
        }

        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet SubmitStoredProcedureDataSet(string commandText,  System.Data.SqlClient.SqlParameter[] commandParameters)
        {
            return execObj.GetInstant().SubmitStoredProcedureDataSet(commandText,  commandParameters);
        }

        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int SubmitStoredProcedureInt(string commandText)
        {
            return execObj.GetInstant().SubmitStoredProcedureInt(commandText);
        }
        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public int SubmitStoredProcedureInt(string commandText, string connectionString)
        {
            return execObj.GetInstant().SubmitStoredProcedureInt(commandText, connectionString);
        }
        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public int SubmitStoredProcedureInt(string commandText, string connectionString, System.Data.SqlClient.SqlParameter[] commandParameters)
        {
            return execObj.GetInstant().SubmitStoredProcedureInt(commandText, connectionString, commandParameters);
        }
        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public int SubmitStoredProcedureInt(string commandText,  System.Data.SqlClient.SqlParameter[] commandParameters)
        {
            return execObj.GetInstant().SubmitStoredProcedureInt(commandText,  commandParameters);
        }
        /// <summary>
        /// 执行SQL文本命令,并返回结果集中第一行的第一列
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object SubmitTextObject(string commandText)
        {
            return execObj.GetInstant().SubmitTextObject(commandText);
        }
        /// <summary>
        /// 执行SQL文本命令,并返回结果集中第一行的第一列
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public object SubmitTextObject(string commandText, string connectionString)
        {
            return execObj.GetInstant().SubmitTextObject(commandText, connectionString);
        }
    }
}
