using System;
namespace Service.DAL
{
    abstract class DataExecutor
    {
        protected string connectionString = "";    
        /// <summary>
        /// 获取执行对象实例
        /// </summary>
        public virtual IExecute GetInstant()
        {
            throw new Exception("未找到实例化执行对象!");
        }
    }
}
