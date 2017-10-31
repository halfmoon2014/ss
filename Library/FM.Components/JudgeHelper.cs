using System;
using System.Text.RegularExpressions;

namespace FM.Components
{
    /// <summary>
    /// 验证工具类
    /// </summary>
    public class JudgeHelper
    {
        /// <summary>
        /// 检查字符串是否为int类型
        /// </summary>
        public static bool IsInt(string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                return false;
            }

            return Regex.IsMatch(args, @"^\d+$");
        }
    }
}
