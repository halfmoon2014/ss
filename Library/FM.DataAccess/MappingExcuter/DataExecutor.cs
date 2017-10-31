using System;
using System.Collections.Generic;
using System.Data;
using Service.DataAccess.Interfaces;

namespace Service.DataAccess.MappingExcuter
{
    public abstract class DataExecutor
    {
        protected IDbConnection conn = null;
        protected string connStr = "";
        protected DataSet ds = null;
        private IList<DataMapping> lisDataMappings = new List<DataMapping>();
        private static int tableIndex = 0;  //DataSet中表序号

        /// <summary>
        /// 设置数据连接字串
        /// </summary>
        public void SetConnectionString()
        {
            SetConnectionString(null);
        }

        /// <summary>
        /// 设置数据连接字串
        /// </summary>
        public void SetConnectionString(string connStrKey)
        {       
            //    
            //TODO:数据库连接字串未实现
            //
            //FM.ConnetString.ConnetString cs = new ConnetString.ConnetString();
            //this.connStr = cs.GetConnString("tzid");
            this.connStr="";            
        }

        public DataSet GetDataSet()
        {
            return ds;
        }

        /// <summary>
        /// 获取执行对象实例
        /// </summary>
        public virtual IMappingExecute GetInstant()
        {
            throw new Exception("未找到实例化执行对象!");
        }

        /// <summary>
        /// 添加数据映射对象
        /// </summary>
        public void AddDataMapping(DataMapping map)
        {
            lisDataMappings.Add(map);
        }

        /// <summary>
        /// 获取DataSet中最大表序号
        /// </summary>
        public int GetMaxTableIndex()
        {
            int retVal = tableIndex;
            tableIndex++;

            return retVal;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public bool Update()
        {
            using (conn)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                IDbTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                foreach (DataMapping map in lisDataMappings)
                {
                    map.SetTransaction(transaction);
                }
                
                try
                {
                    foreach (DataMapping map in lisDataMappings)
                    {
                        map.Update(ds);
                    }

                    transaction.Commit();
                }
                catch (System.Exception ex)
                {
                    transaction.Rollback();
                    throw new System.Exception(ex.Message);
                }
            }

            return true;
        }
    }
}
