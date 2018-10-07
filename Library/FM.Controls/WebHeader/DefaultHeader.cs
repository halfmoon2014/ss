using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using MyTy;
namespace FM.Controls.Header
{
    public class DefaultHeader : Header, IHttpHandler
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string applicationPath = HttpContext.Current.Request.ApplicationPath.ToString().Trim();
            string absolutePath = HttpContext.Current.Request.Url.AbsolutePath.Remove(0, applicationPath.Length);
            //string url = HttpContext.Current.Request.FilePath.ToString();
            //string applicationPath = HttpContext.Current.Request.ApplicationPath.ToString().Trim();
            MyTy.MyCode myCode = new MyTy.MyCode();

            //首页,标题读取
            if (myCode.CheckPageType(absolutePath, "MenuPage"))
            {
                Business.ProcPager pr = new Business.ProcPager();
                DataTable dt = pr.GetPosConfig(MySession.SessionHandle.Get("tzid")).Tables[0];
                if (dt.Rows.Count != 0) { this.Title = dt.Rows[0]["pos_name"].ToString(); }
            }

            //登陆页,套账选择页不需要在head中加截jquery
            if(myCode.CheckPageType(absolutePath, "Login") || myCode.CheckPageType(absolutePath, "ChooseTz") || myCode.CheckPageType(absolutePath, "Other&Nope") )
            {

            }else if ( myCode.CheckPageType(absolutePath, "SysXTSZ&Ordinary") || myCode.CheckPageType(absolutePath, "Other&Ordinary")) {
                SetJqueryScript();
            }
            else if (myCode.CheckPageType(absolutePath, "SysXTSZ&JQY")|| myCode.CheckPageType(absolutePath, "Other&JQY")) {
                SetJqueryScript();
                SetJQEUI(MyCode.GetPageThemes().Themes);
            }
            else if (myCode.CheckPageType(absolutePath, "MenuPage"))
            {
                SetJqueryScript();
                SETProgressDefender();
                SetJSUtil();
                SetStyleSheet(string.Format("{0}/css/loading/loading.css", MyCode.GetPageThemes().CssCDN));
                SetStyleSheet(string.Format("{0}/css/sweetalert/sweetalert.css", MyCode.GetPageThemes().CssCDN));
                SetScript(string.Format("{0}/javascripts/sweetalert/sweetalert.min.js", MyCode.GetPageThemes().JsCDN));
                SetScript(string.Format("{0}/javascripts/sweetalert/swalProcess.js", MyCode.GetPageThemes().JsCDN));

                //手机
                if (RequestExtensions.IsMobileBrowser(HttpContext.Current.Request))
                {
                    SetStyleSheet(string.Format("{0}/css/bootstrap/3.3.7/css/bootstrap.min.css", MyCode.GetPageThemes().CssCDN));
                    SetStyleSheet(string.Format("{0}/css/bootstrap/ie10-viewport-bug-workaround.css", MyCode.GetPageThemes().CssCDN));
                    //SetScript(string.Format("{0}/javascripts/bootstrap/ie-emulation-modes-warning.js", MyCode.GetPageThemes().JsCDN));
                    SetScript(string.Format("{0}/javascripts/bootstrap/ie10-viewport-bug-workaround.js", MyCode.GetPageThemes().JsCDN));
                    SetScript(string.Format("{0}/javascripts/bootstrap/3.3.7/bootstrap.min.js", MyCode.GetPageThemes().JsCDN));
                    SetScript(string.Format("{0}/javascripts/menu_3/menu_3_mobile.js", MyCode.GetPageThemes().JsCDN));
                }
                else
                {
                    SetJQEUI(MyCode.GetPageThemes().Themes);                
                    SetStyleSheet(string.Format("{0}/css/menu_3.css", MyCode.GetPageThemes().CssCDN));
                    SetScript(string.Format("{0}/javascripts/menu_3/menu_3.js", MyCode.GetPageThemes().JsCDN));
                    SetScript(string.Format("{0}/javascripts/longPolling.js", MyCode.GetPageThemes().JsCDN));
                    SetScript(string.Format("{0}/javascripts/myjs/myweb.js", MyCode.GetPageThemes().JsCDN));
                }
            }
            //其它页             
            else
            {
                if (string.Compare(MyCode.GetPageThemes().PageThemes, "jqeui") == 0)
                {
                    SetJqueryScript();
                    SetJQEUI(MyCode.GetPageThemes().Themes);
                    SetJSUtil();
                    SetPlat();
                    SETProgressDefender();
                }
                //或者主题不是jqeui
                else if (string.Compare(MyCode.GetPageThemes().PageThemes, "jqeui") != 0)
                {
                    SetJqueryScript();
                    SetStyleSheet(string.Format("{0}/css/f1/main.css", MyCode.GetPageThemes().CssCDN));
                    SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", MyCode.GetPageThemes().CssCDN));
                    SetScript(string.Format("{0}/AjaxHandler/ProcPager.js", MyCode.GetPageThemes().JsCDN));
                    SetScript(string.Format("{0}/javascripts/f1/base.js", MyCode.GetPageThemes().JsCDN));
                    SetScript(string.Format("{0}/javascripts/utils.js", MyCode.GetPageThemes().JsCDN));
                    SetScript(string.Format("{0}/javascripts/myjs/myweb.js", MyCode.GetPageThemes().JsCDN));
                    SetScript(string.Format("{0}/javascripts/json2.js", MyCode.GetPageThemes().JsCDN));
                }
            }
        }
        /// <summary>
        /// 加载jquery easy ui
        /// </summary>
        /// <param name="Themes"></param>
        void SetJQEUI(string Themes)
        {
            SetStyleSheet(string.Format("{0}/css/jey/{1}/easyui.css", MyCode.GetPageThemes().CssCDN, Themes));
            SetStyleSheet(string.Format("{0}/css/jey/icon.css", MyCode.GetPageThemes().CssCDN));          
            SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.min.js", MyCode.GetPageThemes().JsCDN));
            //jquery easyui 日期格式化
            SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.add.js", MyCode.GetPageThemes().JsCDN));
            SetScript(string.Format("{0}/javascripts/jey/easyui-lang-zh_CN.js", MyCode.GetPageThemes().JsCDN));
            //end jquery easyui 日期格式化
        }
        /// <summary>
        /// JS工具
        /// </summary>
        void SetJSUtil()
        {            
            SetScript(string.Format("{0}/javascripts/utils.js", MyCode.GetPageThemes().JsCDN));            
            SetScript(string.Format("{0}/javascripts/json2.js", MyCode.GetPageThemes().JsCDN));            
        }
        /// <summary>
        /// 平台自定义
        /// </summary>
        void SetPlat()
        {
            SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", MyCode.GetPageThemes().CssCDN));
            SetScript(string.Format("{0}/javascripts/myjs/myweb.js", MyCode.GetPageThemes().JsCDN));
            SetScript(string.Format("{0}/AjaxHandler/ProcPager.js", MyCode.GetPageThemes().JsCDN));
        }
        /// <summary>
        /// 进程守护
        /// </summary>
        void SETProgressDefender()
        {
            SetScript(string.Format("{0}/javascripts/progressDefender.js", MyCode.GetPageThemes().JsCDN));            
        }
        /// <summary>
        /// 加载JQUERY
        /// </summary>
        void SetJqueryScript()
        {
            SetScript(string.Format("{0}/javascripts/jquery/1.12.4/jquery.min.js", MyCode.GetPageThemes().JsCDN));
        }
        bool IHttpHandler.IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
