using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Service.DataAccess.Interfaces;
using Service.DataAccess.Supports;

namespace Service.DataAccess
{
    public class DataMapping
    {
        private IDbConnection conn = null;
        private IDbDataAdapter dataAdapter = null;
        private DataSet ds = null;

        #region 构造函数
        public DataMapping()
        { }

        public DataMapping(IMappingExecute executeObj)
        {
            this.ExecuteObject = executeObj;
        }

        public DataMapping(IMappingExecute executeObj, string tableName)
            : this(executeObj)
        {
            this.tableName = tableName;
            this.keyColumns = "ID";
            this.columns = "*";
        }

        public DataMapping(IMappingExecute executeObj, string tableName, string keyColumns)
            : this(executeObj, tableName)
        {
            this.keyColumns = keyColumns;
            this.columns = "*";
        }

        public DataMapping(IMappingExecute executeObj, string tableName, string keyColumns, string columns)
            : this(executeObj, tableName, keyColumns)
        {
            this.columns = columns;
        }
        #endregion

        #region 依赖接口
        /// <summary>
        /// 映射执行对象
        /// </summary>
        public IMappingExecute ExecuteObject
        {
            set
            {
                executeObj = value;
                conn = executeObj.GetConn();
                dataAdapter = executeObj.GetDataAdapter();
                ds = executeObj.GetDataSet();
            }
            get
            {
                return executeObj;
            }
        }
        private IMappingExecute executeObj = null;
        #endregion

        #region 公有方法
        /// <summary>
        /// 填充数据集
        /// 参数：查询语句
        /// 参数：内存表名
        /// </summary>
        public void Fill(string sqlText)
        {
            Fill(sqlText, string.Format("Table{0}", executeObj.GetMaxTableIndex()));
        }

        /// <summary>
        /// 填充数据集
        /// 参数：查询语句
        /// 参数：内存表名
        /// </summary>
        public void Fill(string sqlText, string tableName)
        {
            Fill(sqlText, tableName, null, CommandType.Text);
        }

        /// <summary>
        /// 填充数据集
        /// 参数：查询语句
        /// 参数：内存表名
        /// 参数：存储过程参数
        /// </summary>
        public void Fill(string sqlText, string tableName, IList<ProcParams> lisParamse)
        {
            Fill(sqlText, tableName, lisParamse, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 填充数据集
        /// 参数：查询语句
        /// 参数：内存表名
        /// 参数：命令类型，查询语句或存储过程
        /// </summary>
        public void Fill(string sqlText, string tableName, IList<ProcParams> lisParams, CommandType type)
        {
            IDbCommand cmd = executeObj.GetCommand();
            cmd.Connection = conn;
            cmd.CommandText = sqlText;
            dataAdapter.SelectCommand = cmd;
            if (type == CommandType.StoredProcedure)
            { 
                //存储过程，则设置存储过程参数                
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameter param = null;                
                foreach (ProcParams procParams in lisParams)
                {                    
                    
                    if (procParams.Type == Service.DataAccess.Supports.SqlDbType.VarChar)
                    {
                        IDataParameter[] parameter = { new System.Data.SqlClient.SqlParameter(procParams.ParamName, System.Data.SqlDbType.VarChar, procParams.TypeSize) };
                        parameter[0].Direction = procParams.ParameterDirection;
                        cmd.Parameters.Add(parameter[0]);
                    }
                    else {
                        param = executeObj.GetDataParameter(procParams.ParamName);
                        param.Value = procParams.Value;
                        param.Direction = procParams.ParameterDirection;
                        cmd.Parameters.Add(param);
                    }                    
                    
                    
                }
            }

            ((DbDataAdapter)dataAdapter).Fill(ds, tableName);
        }

        /// <summary>
        /// 设置更新命令
        /// </summary>
        public void SetCommands(DataCommandType commandType)
        {
            //设置更新事件时添加映射对象
            executeObj.AddDataMapping(this);
            if ((commandType & DataCommandType.Insert) == DataCommandType.Insert)
            {
                CreateInsertCommand(ds);
            }

            if ((commandType & DataCommandType.Update) == DataCommandType.Update)
            {
                CreateUpdateCommand(ds);
            }

            if ((commandType & DataCommandType.Delete) == DataCommandType.Delete)
            {
                CreateDeleteCommand(ds);
            }
        }

        /// <summary>
        /// 设置数据提交事务
        /// </summary>
        public void SetTransaction(IDbTransaction transaction)
        {
            if (dataAdapter.InsertCommand != null)
            {
                dataAdapter.InsertCommand.Transaction = transaction;
            }

            if (dataAdapter.UpdateCommand != null)
            {
                dataAdapter.UpdateCommand.Transaction = transaction;
            }

            if (dataAdapter.DeleteCommand != null)
            {
                dataAdapter.DeleteCommand.Transaction = transaction;
            }
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        public bool Update(DataSet ds)
        {
            return ((DbDataAdapter)dataAdapter).Update(ds, tableName) > 0;
        }

        /// <summary>
        /// 获取适配器
        /// </summary>
        public IDbDataAdapter GetDataAdapter()
        {
            return dataAdapter;
        }
        #endregion

        #region 支撑方法
        /// <summary>
        /// 生成新增命令及SQL语句
        /// </summary>
        private void CreateInsertCommand(DataSet ds)
        {
            IList<string> lisColumns = GetColumns(ds);
            StringBuilder sbCol = new StringBuilder();
            StringBuilder sbVal = new StringBuilder();
            foreach (string col in lisColumns)
            {
                sbCol.AppendFormat(", {0}", col);
                sbVal.AppendFormat(", {0}", executeObj.GetSourceColumn(col));
            }

            sbCol.Remove(0, 2);
            sbVal.Remove(0, 2);
            string sqlText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, sbCol.ToString(), sbVal.ToString());
            IDbCommand cmd = executeObj.GetCommand();
            cmd.Connection = conn;
            cmd.CommandText = sqlText;
            SetCommandParams(cmd, lisColumns);
            dataAdapter.InsertCommand = cmd;
        }

        /// <summary>
        /// 生成更新命令及SQL语句
        /// </summary>
        private void CreateUpdateCommand(DataSet ds)
        {
            IList<string> lisColumns = GetColumns(ds);
            IList<string> lisKeys = GetKeys();
            StringBuilder sbCol = new StringBuilder();
            foreach (string col in lisColumns)
            {
                if (lisKeys.Contains(col))
                {
                    continue;
                }

                sbCol.AppendFormat(", {0} = {1}", col, executeObj.GetSourceColumn(col));
            }

            StringBuilder sbWhere = new StringBuilder();
            foreach (string col in lisKeys)
            {
                sbWhere.AppendFormat(" AND {0} = {1}", col, executeObj.GetSourceColumn(col));
            }

            sbCol.Remove(0, 2);
            sbWhere.Remove(0, 5);
            string sqlText = string.Format("UPDATE {0} SET {1} WHERE {2}", tableName, sbCol.ToString(), sbWhere.ToString());
            IDbCommand cmd = executeObj.GetCommand();
            cmd.Connection = conn;
            cmd.CommandText = sqlText;
            SetCommandParams(cmd, lisColumns);
            dataAdapter.UpdateCommand = cmd;
        }

        /// <summary>
        /// 生成删除命令及SQL语句
        /// </summary>
        private void CreateDeleteCommand(DataSet ds)
        {
            IList<string> lisKeys = GetKeys();
            StringBuilder sbWhere = new StringBuilder();
            foreach (string col in lisKeys)
            {
                sbWhere.AppendFormat(" AND {0} = {1}", col, executeObj.GetSourceColumn(col));
            }

            sbWhere.Remove(0, 5);
            string sqlText = string.Format("DELETE FROM {0} WHERE {1}", tableName, sbWhere.ToString());
            IDbCommand cmd = executeObj.GetCommand();
            cmd.Connection = conn;
            cmd.CommandText = sqlText;
            SetCommandParams(cmd, lisKeys);
            dataAdapter.DeleteCommand = cmd;
        }

        /// <summary>
        /// 获取列字段集
        /// </summary>
        private IList<string> GetColumns(DataSet ds)
        {
            IList<string> lisColumns = new List<string>();
            if (columns != "*")
            {
                string[] sltCol = columns.Split(',');
                foreach (string col in sltCol)
                {
                    lisColumns.Add(col.Trim());
                }
            }
            else
            {
                DataTable dt = ds.Tables[tableName];
                foreach (DataColumn dc in dt.Columns)
                {
                    lisColumns.Add(dc.ColumnName);
                }
            }

            return lisColumns;
        }

        /// <summary>
        /// 获取主键集
        /// </summary>
        private IList<string> GetKeys()
        {
            IList<string> lisKeys = new List<string>();
            string[] sltKeys = keyColumns.Split(',');
            foreach (string key in sltKeys)
            {
                lisKeys.Add(key.Trim());
            }

            return lisKeys;
        }

        /// <summary>
        /// 设置命令参数
        /// </summary>
        private void SetCommandParams(IDbCommand cmd, IList<string> lisColumns)
        {
            IDbDataParameter param = null;
            foreach (string col in lisColumns)
            {
                param = executeObj.GetDataParameter(col);
                param.SourceColumn = col;
                cmd.Parameters.Add(param);
            }
        }
        #endregion

        #region 映射属性
        /// <summary>
        /// 更新列名
        /// </summary>
        public string Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
            }
        }
        private string columns = "";

        /// <summary>
        /// 主键名
        /// </summary>
        public string KeyColumns
        {
            get
            {
                return keyColumns;
            }
            set
            {
                keyColumns = value;
            }
        }
        private string keyColumns = "";

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }
        private string tableName = "";
        #endregion
    }
}
