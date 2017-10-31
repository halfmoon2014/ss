using System.Data;
namespace Service.DAL
{
    interface IExecute
    {
        void SetConnectionString(string connectionString);
        string GetConnectionString();

        /// <summary>
        /// 执行SQL文本命令,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet SubmitTextDataSet(string commandText);
        /// <summary>
        /// 执行SQL文本命令,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet SubmitTextDataSet(string commandText, string connectionString);

        /// <summary>
        /// 执行SQL文本命令,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        int SubmitTextInt(string commandText);
        /// <summary>
        /// 执行SQL文本命令,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        int SubmitTextInt(string commandText, string connectionString);

        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet SubmitStoredProcedureDataSet(string commandText);
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet SubmitStoredProcedureDataSet(string commandText, string connectionString);
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet SubmitStoredProcedureDataSet(string commandText, string connectionString, params System.Data.SqlClient.SqlParameter[] commandParameters);
        /// <summary>
        /// 执行存储过程,返回DataSet
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet SubmitStoredProcedureDataSet(string commandText,  params System.Data.SqlClient.SqlParameter[] commandParameters);

        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        int SubmitStoredProcedureInt(string commandText);
        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        int SubmitStoredProcedureInt(string commandText, string connectionString);
        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        int SubmitStoredProcedureInt(string commandText, string connectionString, params System.Data.SqlClient.SqlParameter[] commandParameters);
        /// <summary>
        /// 执行存储过程,返回Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        int SubmitStoredProcedureInt(string commandText, params System.Data.SqlClient.SqlParameter[] commandParameters);

        /// <summary>
        /// 执行SQL文本命令,并返回结果集中第一行的第一列
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        object SubmitTextObject(string commandText);
        /// <summary>
        /// 执行SQL文本命令,并返回结果集中第一行的第一列
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        object SubmitTextObject(string commandText, string connectionString);        
    }
}
