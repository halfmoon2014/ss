using System;
using System.Data;

namespace FM.Components
{
    /// <summary>
    /// 数据操作帮助类
    /// </summary>
    public class DataHelper
    {
        /// <summary>
        /// 获取分页DataTable--分页处理,
        /// </summary>
        public static DataTable DataTablePager(DataTable dt, int pageSize, int currentPage)
        {
            DataTable dtRet = dt.Clone();
            int records = dt.Rows.Count;
            int start = pageSize * (currentPage - 1);
            int end = pageSize * currentPage;
            if (end > records)
            {
                end = records;
            }

            DataRowCollection drc = dt.Rows;
            for (int i = start; i < end; i++)
            {
                dtRet.ImportRow(drc[i]);
            }

            return dtRet;
        }
    }
}
