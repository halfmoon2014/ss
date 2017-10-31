using System;
using System.Data;

namespace Service.DataAccess.Supports
{
    /// <summary>
    /// 存储过程参数结构体
    /// </summary>
    public struct ProcParams
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName
        {
            get;
            set;
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// 输出参数类型
        /// </summary>
        public ParameterDirection ParameterDirection
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义返回类型
        /// </summary>
        public Service.DataAccess.Supports.SqlDbType Type
        {
            get;
            set;
        }
        public int TypeSize
        {
            get;
            set;
        }

    }


    public enum SqlDbType
        {
            Object = 0,
            VarChar = 1     
        }
    

}
