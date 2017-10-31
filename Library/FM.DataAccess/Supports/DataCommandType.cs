using System;

namespace Service.DataAccess.Supports
{
    /// <summary>
    /// 数据操作命令类型
    /// </summary>
    public enum DataCommandType
    {
        /// <summary>
        /// 新增
        /// </summary>
        Insert = 1,

        /// <summary>
        /// 修改
        /// </summary>
        Update = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 4
    }
}
