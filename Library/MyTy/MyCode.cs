using System.Collections.Generic;
using System.Web;
namespace MyTy
{
    /// <summary>
    /// 通用类
    /// </summary>
    public class MyCode
    {
        ///在外->内1.SQL语句,外面使用mySysDate(encodeURIComponent),接收的方法使用MySysDate(str)
        ///限制条件是不能有'号,如果需要,必须在外面构告''
        /// /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>             
        public static string MySysDate(string str)
        {
            return Microsoft.JScript.GlobalObject.decodeURIComponent(str);
        }
 
              
        /// <summary>
        /// 处理用户输入数据,使用存储过程处理数据
        /// </summary>
        /// <param name="str">面页传送到后台数据</param>
        /// <returns></returns>
        public static string DoUrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }
        /// <summary>
        /// 判断当前请求页面是否是特定类型
        /// </summary>
        /// <param name="FileName">当前请求页面,相对于根目录路径</param>
        /// <param name="checkType">需要判断的类型</param>
        /// <returns></returns>
        public bool CheckPageType(string FileName,string checkType)
        {
            string xml = HttpContext.Current.Server.MapPath("~/config.xml");
            string path="";
            if (string.Compare(checkType, "MenuPage", true) == 0)
            {//菜单页
                path = "/Root/WebFile/MenuPage/FileName";
            }
            else if (string.Compare(checkType, "PrintPage", true) == 0)
            {//打印页
                path = "/Root/WebFile/PrintPage/FileName";
            }
            else if (string.Compare(checkType, "ExcelPage", true) == 0)
            {//导EXCEL页面
                path = "/Root/WebFile/ExcelPage/FileName";
            }
            else if (string.Compare(checkType, "Login", true) == 0)
            {//登陆页
                path = "/Root/WebFile/Login/FileName";
            }
            else if (string.Compare(checkType, "ChooseTz", true) == 0)
            {//套账选择页
                path = "/Root/WebFile/ChooseTz/FileName";
            }
            else if (string.Compare(checkType, "SysModulePage", true) == 0)
            {//模块页
                path = "/Root/WebFile/SysModulePage/FileName";
            }
            else if (string.Compare(checkType, "SysXTSZ&JQY", true) == 0)
            {//平台设计页,需要jqy
                path = "/Root/WebFile/SysXTSZ/JQY/FileName";
            }
            else if (string.Compare(checkType, "SysXTSZ&Ordinary", true) == 0)
            {//平台设计页
                path = "/Root/WebFile/SysXTSZ/Ordinary/FileName";
            }
            else if (string.Compare(checkType, "NoLimitUrl", true) == 0)
            {
                path = "/Root/NoLimitUrl/Site";
            }
            return ConfigReader.CheckInnerText(xml, path, FileName);
        }
        /// <summary>
        /// 返回页面样式配置
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetPageThemes()
        {
            System.Collections.Generic.Dictionary<string,string> dic =  new Dictionary<string,string>();            
            string xml = HttpContext.Current.Server.MapPath("~/config.xml");
            dic.Add("PageThemes",MyTy.ConfigReader.Read(xml, "/Root/appSettings/PageThemes", ""));
            dic.Add("Themes", MyTy.ConfigReader.Read(xml, "/Root/appSettings/Themes", ""));
            return dic;    
            
        }
 

    }
}
