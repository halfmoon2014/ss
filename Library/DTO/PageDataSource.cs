using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PageDataSource
    {       
        private string name;
        private string sql;
        private string fwsql;
        private int mrcx;
        private int pagesize;
        private string orderby;
        private int myadd;
        private string mxgl;
        private string mxsql;
        private string mxhgl;
        private string mxhord;
        private string mxhsql;
        private string mxly;
        private string sql_2;
        private int id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// 详情SQL
        /// </summary>
        public string Sql { get => sql; set => sql = value; }
        /// <summary>
        /// 单据SQL
        /// </summary>
        public string Fwsql { get => fwsql; set => fwsql = value; }
        public int Mrcx { get => mrcx; set => mrcx = value; }
        public int Pagesize { get => pagesize; set => pagesize = value; }
        public string Orderby { get => orderby; set => orderby = value; }
        public int Myadd { get => myadd; set => myadd = value; }
        /// <summary>
        /// 明细SQL-尺码关联
        /// </summary>
        public string Mxgl { get => mxgl; set => mxgl = value; }
        /// <summary>
        /// 尺码
        /// </summary>
        public string Mxsql { get => mxsql; set => mxsql = value; }
        /// <summary>
        /// 明细SQL-尺码标题关联
        /// </summary>
        public string Mxhord { get => mxhord; set => mxhord = value; }
        /// <summary>
        /// 尺码标题
        /// </summary>
        public string Mxhsql { get => mxhsql; set => mxhsql = value; }
        /// <summary>
        /// 尺码来源
        /// </summary>
        public string Mxly { get => mxly; set => mxly = value; }
        public string Sql_2 { get => sql_2; set => sql_2 = value; }
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// 尺码数据源-尺码标题关联
        /// </summary>
        public string Mxhgl { get => mxhgl; set => mxhgl = value; }
    }
}
