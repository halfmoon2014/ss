using System.Data;
using System.DataBase;
namespace Service.DAL
{
     class MSSqlExecutor : DataExecutor, IExecute
    {
        public override IExecute GetInstant()
        {
            return this;
        }
        public void SetConnectionString(string connectionString){
            this.connectionString = connectionString;
        }
        public string GetConnectionString()
        {
            return this.connectionString;
        }

        /// <summary>
        /// 执行SQL文本命令,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public  DataSet SubmitTextDataSet(string commandText)
        {
            return SubmitTextDataSet(commandText, this.connectionString);
        }
        /// <summary>
        /// 执行SQL文本命令,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public  DataSet SubmitTextDataSet(string commandText, string connectionString)
        {
            return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);            
        }
        
        /// <summary>
        /// 执行SQL文本命令,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public  int SubmitTextInt(string commandText)
        {
            return SubmitTextInt(commandText, this.connectionString);
        }
        /// <summary>
        /// 执行SQL文本命令,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public  int SubmitTextInt(string commandText, string connectionString)
        {
            return SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, commandText);
        }   

        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public  DataSet SubmitStoredProcedureDataSet(string commandText)
        {
            return SubmitStoredProcedureDataSet(commandText, this.connectionString); 
        }
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public  DataSet SubmitStoredProcedureDataSet(string commandText, string connectionString)
        {
            return SubmitStoredProcedureDataSet(commandText, connectionString, null);
        }
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public  DataSet SubmitStoredProcedureDataSet(string commandText, string connectionString, System.Data.SqlClient.SqlParameter[] commandParameters)
        {   
             return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, commandText, commandParameters);            
        }

        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public DataSet SubmitStoredProcedureDataSet(string commandText, System.Data.SqlClient.SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteDataset(this.connectionString, CommandType.StoredProcedure, commandText, commandParameters);
        }

        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public  int SubmitStoredProcedureInt(string commandText)
        {
            return SubmitStoredProcedureInt(commandText, this.connectionString);
        }
        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public  int SubmitStoredProcedureInt(string commandText, string connectionString)
        {
            return SubmitStoredProcedureInt(commandText, connectionString, null);
        }
        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public  int SubmitStoredProcedureInt(string commandText, string connectionString, System.Data.SqlClient.SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, commandText, commandParameters);        
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
            return SqlHelper.ExecuteNonQuery(this.connectionString, CommandType.StoredProcedure, commandText, commandParameters);
        }

        /// <summary>
        /// 执行SQL文本命令,并返回结果集中第一行的第一列
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object SubmitTextObject(string commandText)
        {
            return SubmitTextObject(commandText, this.connectionString);
        }
        /// <summary>
        /// 执行SQL文本命令,并返回结果集中第一行的第一列
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public object SubmitTextObject(string commandText, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return SqlHelper.ExecuteScalar(this.connectionString, CommandType.Text, commandText);
            }
            else
            {
                return SqlHelper.ExecuteScalar(connectionString, CommandType.Text, commandText);
            }

        }
    }
}
