using System;
using System.Data;
using System.Data.SqlClient;
using Service.DataAccess.Interfaces;

namespace Service.DataAccess.MappingExcuter
{
    /// <summary>
    /// MSSql映射执行者
    /// </summary>
    public class MSSqlExecutor : DataExecutor, IMappingExecute
    {
        public MSSqlExecutor()
        {
            ds = new DataSet();
        }

        public IDbConnection GetConn()
        {
            if (conn != null)
            {
                return conn;
            }

            conn = new SqlConnection();
            conn.ConnectionString = connStr;
            return conn;
        }

        public override IMappingExecute GetInstant()
        {
            return this;
        }

        public IDbDataAdapter GetDataAdapter()
        {
            return new SqlDataAdapter();
        }

        public IDbCommand GetCommand()
        {
            return new SqlCommand();
        }

        public IDbDataParameter GetDataParameter(string col)
        {
            return new SqlParameter(col, null);
        }

        public string GetSourceColumn(string col)
        {
            return string.Format("@{0}", col);
        }
    }
}
