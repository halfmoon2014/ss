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
            return MyCode.GetPageThemes().JsVer;
        }
        protected string GetJsCDN()
        {
            string jsCDN = MyCode.GetPageThemes().JsCDN;
            //没配CDN会有问题，路径
            return jsCDN + "/javascripts";
        }
        protected string GetRequireJs()
        {
            string jsCDN = MyCode.GetPageThemes().JsCDN;
            return jsCDN + "/javascripts/lib/require.js";
        }
    }
}
