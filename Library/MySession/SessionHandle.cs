using System.Web;
namespace MySession
{
    public class SessionHandle
    {

        /// <summary>
        /// 添加Session，调动有效期默认为23分钟
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        public static void Add(string strSessionName, string strValue)
        {            
            Add(strSessionName, strValue, 60*24);
        }
        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <param name="strValue">Session值</param>
        /// <param name="iExpires">调动有效期（分钟）</param>
        public static void Add(string strSessionName, string strValue, int iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = iExpires;
        }

        /// <summary>
        /// 读取某个Session对象值
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        /// <returns>Session对象值</returns>
        public static string Get(string strSessionName)
        {
            if (HttpContext.Current.Session == null)
            {
                return null;
            }
            else
            {
                if (HttpContext.Current.Session[strSessionName] == null)
                {
                    return null;
                }
                else
                {
                    return HttpContext.Current.Session[strSessionName].ToString();
                }
            }
        }

        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName">Session对象名称</param>
        public static void Del(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }

        /// <summary>
        /// 清除
        /// </summary>
        public static void Abandon() {
            HttpContext.Current.Session.Abandon();
        }
    }

}
