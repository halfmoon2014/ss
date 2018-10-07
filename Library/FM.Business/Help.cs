using System.Data;
using System.Web;
using System.Web.Security;
using Service.Util;
namespace FM.Business
{
    public  class Help
    {
        public string GetMM(string value2)
        {
            string EnPswdStr = FormsAuthentication.HashPasswordForStoringInConfigFile("Z" + FormsAuthentication.HashPasswordForStoringInConfigFile(value2, "MD5") + "a", "MD5");
            return EnPswdStr;
        }
        public DataSet ExecuteDataset(string commandText)
        {               
            Service.DAL.DALInterface execObj = new Service.DAL.DALInterface();         
            ConnetString connstr = new ConnetString();
    
            return execObj.SubmitTextDataSet(commandText, connstr.GetDb(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid")));
            
        }
        public int ExecuteNonQuery(string commandText)
        {
            Service.DAL.DALInterface execObj = new Service.DAL.DALInterface();
            ConnetString connstr = new ConnetString();

            return execObj.SubmitTextInt(commandText, connstr.GetDb(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid")));

        }
        /// <summary>
        /// 获取网站虚拟路径
        /// </summary>
        public static string GetWebVirtualUrl()
        {
            string url = string.Format("http://{0}{1}/", HttpContext.Current.Request.Url.Authority, HttpRuntime.AppDomainAppVirtualPath);
            if (url.EndsWith("/"))
                url = url.Substring(0, url.Length - 1);

            return url;
        }
        /// <summary>
        ///等待框
        /// </summary>
        /// <returns></returns>
        public string GetWait()
        {
            return GetWait("none") ;
        }
        public string GetWait(string sty)
        {
            return "<div id='Loading' algin='center' style=\"display:"+sty+";position:absolute;z-index:1000;top:0px;left:0px;width:100%;height:100%;background:#DDDDDB ;text-align:center;padding-top: 5%;\"><table style='width:100%' ><tr><td>&nbsp;</td><td style='width:20px'><img src='../images/loading.gif'/></td><td style='width:58px'><font color=\"#15428B\" style='font-size: 12px;'>加载中···</font></td><td>&nbsp;</td></tr></table></div>";
        }
    }

}
