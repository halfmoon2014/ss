using System;
using System.Data;
using Service.DataAccess;

namespace Service.DataAccess.Interfaces
{
    /// <summary>
    /// 映射执行接口
    /// </summary>
    public interface IMappingExecute
    {
        /// <summary>
        /// 获取连接对象
        /// </summary>
        IDbConnection GetConn();

        /// <summary>
        /// 获取DataSet对象
        /// </summary>
        /// <returns></returns>
        DataSet GetDataSet();

        /// <summary>
        /// 获取数据适配器
        /// </summary>
        IDbDataAdapter GetDataAdapter();

        /// <summary>
        /// 获取命令对象
        /// </summary>
        IDbCommand GetCommand();

        /// <summary>
        /// 获取命令参数
        /// </summary>
        IDbDataParameter GetDataParameter(string col);

        /// <summary>
        /// 获取命令参数
        /// </summary>
        string GetSourceColumn(string col);

        /// <summary>
        /// 添加数据映射对象
        /// </summary>
        void AddDataMapping(DataMapping map);

        /// <summary>
        /// 获取DataSet中最大表序号
        /// </summary>
        int GetMaxTableIndex();
    }
}
