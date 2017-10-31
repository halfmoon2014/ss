using System;
using System.Web;

namespace FM.Components
{
    /// <summary>
    /// HTML元素工具类
    /// </summary>
    public class HTMLHelper
    {
        /// <summary>
        /// 获取网站虚拟路径
        /// </summary>
        public static string GetWebVirtualUrl()
        {
            string url = string.Format("{0}://{1}{2}/", (HttpContext.Current.Request.IsSecureConnection?"https":"http"), HttpContext.Current.Request.Url.Authority, HttpRuntime.AppDomainAppVirtualPath);
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        public static string Form(string key)
        {
            string retVal = HttpContext.Current.Request.Form[key];            
            if (retVal == null)
            {
                return "";
            }

            return retVal;
         
        }
        /// <summary>
        /// 取得AJAX中的POST中的数据!//用于替换SQL中的
        /// </summary>
        /// <returns></returns>
        public static System.Collections.Specialized.NameValueCollection GetFormParameters()
        {            
            return HttpContext.Current.Request.Form;
        }

       
        public static System.Collections.Specialized.NameValueCollection GetParameters()
        {
            return HttpContext.Current.Request.Params;
        }

        /// <summary>
        /// 获取URL数据
        /// </summary>
        public static string QueryString(string key)
        {
            string retVal = HttpContext.Current.Request.QueryString[key];
            if (retVal == null)
            {
                return "";
            }

            return retVal;
        }

        /// <summary>
        /// 获取URL数据
        /// </summary>
        public static int QueryStringInt(string key)
        {            
            string retVal = HttpContext.Current.Request.QueryString[key];            

            if (retVal == null || !JudgeHelper.IsInt(retVal))
            {
                return 0;
            }

            return int.Parse(retVal);
        }
        /// <summary>
        /// 获取POST数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RequestFString(string key)
        {
            string retVal = HttpContext.Current.Request.Form[key];
            if (retVal == null)
            {
                return "";
            }

            return retVal;
        }
        /// <summary>
        /// 获取POST数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int RequestFInt(string key)
        {
            string retVal = HttpContext.Current.Request.Form[key];
            if (retVal == null || !JudgeHelper.IsInt(retVal))
            {
                return 0;
            }

            return int.Parse(retVal);
        }

    }
}
