using MyTy;
using System;

namespace FM.Controls
{
    public class Page :System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// JS Ver
        /// </summary>
        /// <returns></returns>
        protected string GetJsVer()
        {
            return MyCode.GetAppSettings().JsVer;
        }
        protected string GetJsCDN()
        {
            string jsCDN = MyCode.GetAppSettings().JsCDN;
            //没配CDN会有问题，路径
            return jsCDN;
        }
        protected string GetCssCDN()
        {
            string jsCDN = MyCode.GetAppSettings().CssCDN;
            //没配CDN会有问题，路径
            return jsCDN ;
        }
        protected string GetRequireJs()
        {
            string jsCDN = MyCode.GetAppSettings().JsCDN;
            return jsCDN + "/lib/require.js";
        }
    }
}
