using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using MyTy;
namespace FM.Controls.Header
{
    public class DefaultHeader : Header, IHttpHandler
    {
        private string JsCDN = MyCode.GetPageThemes().JsCDN;
        private string CssCDN = MyCode.GetPageThemes().CssCDN;

        private string JsVer = (MyCode.GetPageThemes().JsVer.Length == 0 ? "regular" : MyCode.GetPageThemes().JsVer);
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
            if (myCode.CheckPageType(absolutePath, "Login"))
            {

            }
            else if (myCode.CheckPageType(absolutePath, "ChooseTz"))
            {

            }
            else if (myCode.CheckPageType(absolutePath, "Other&Nope"))
            {
            }
            else if (myCode.CheckPageType(absolutePath, "SysXTSZ&Ordinary") || myCode.CheckPageType(absolutePath, "Other&Ordinary"))
            {
                SetJqueryScript();
            }
            else if (myCode.CheckPageType(absolutePath, "SysXTSZ&JQY") || myCode.CheckPageType(absolutePath, "Other&JQY"))
            {
                SetJqueryScript();
                SetJQEUI(MyCode.GetPageThemes().Themes);
            }
            else if (myCode.CheckPageType(absolutePath, "MenuPage"))
            {
                SetJqueryScript();
                SETProgressDefender();
                SetJSUtil();
                SetStyleSheet(string.Format("{0}/loading/loading.css?ver={1}", CssCDN, JsVer));
                SetStyleSheet(string.Format("{0}/sweetalert/sweetalert.css?ver={1}", CssCDN, JsVer));
                SetScript(string.Format("{0}/sweetalert/sweetalert.min.js?ver={1}", JsCDN, JsVer));
                SetScript(string.Format("{0}/sweetalert/swalProcess.js?ver={1}", JsCDN, JsVer));

                //手机
                if (RequestExtensions.IsMobileBrowser(HttpContext.Current.Request))
                {
                    SetStyleSheet(string.Format("{0}/bootstrap/3.3.7/css/bootstrap.min.css?ver={1}", CssCDN, JsVer));
                    SetStyleSheet(string.Format("{0}/bootstrap/ie10-viewport-bug-workaround.css?ver={1}", CssCDN, JsVer));
                    //SetScript(string.Format("{0}/javascripts/bootstrap/ie-emulation-modes-warning.js?ver={1}", JsCDN,JsVer));
                    SetScript(string.Format("{0}/bootstrap/ie10-viewport-bug-workaround.js?ver={1}", JsCDN, JsVer));
                    SetScript(string.Format("{0}/bootstrap/3.3.7/bootstrap.min.js?ver={1}", JsCDN, JsVer));
                    SetScript(string.Format("{0}/menu_3/menu_3_mobile.js?ver={1}", JsCDN, JsVer));
                }
                else
                {
                    SetJQEUI(MyCode.GetPageThemes().Themes);
                    SetStyleSheet(string.Format("{0}/menu_3.css?ver={1}", CssCDN, JsVer));
                    SetScript(string.Format("{0}/menu_3/menu_3.js?ver={1}", JsCDN, JsVer));
                    SetScript(string.Format("{0}/myjs/longPolling.js?ver={1}", JsCDN, JsVer));
                    SetScript(string.Format("{0}/myjs/myweb.js?ver={1}", JsCDN, JsVer));
                }
            }
            else if (myCode.CheckPageType(absolutePath, "SysModulePage"))
            {
                //模块页
                SetStyleSheet(string.Format("{0}/sweetalert/sweetalert.css?ver={1}", CssCDN, JsVer));
                SetJqueryScript();
                SetJQEUI(MyCode.GetPageThemes().Themes);
                SetJSUtil();
                SetPlat();
                SETProgressDefender();

                SetScript(string.Format("{0}/sweetalert/sweetalert.min.js?ver={1}", JsCDN, JsVer));
                SetScript(string.Format("{0}/sweetalert/swalProcess.js?ver={1}", JsCDN, JsVer));
                SetScript(string.Format("{0}/lss/lss.js?ver={1}", JsCDN, JsVer));

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
                    SetStyleSheet(string.Format("{0}/f1/main.css?ver={1}", CssCDN, JsVer));
                    SetStyleSheet(string.Format("{0}/mycss/myweb.css?ver={1}", CssCDN, JsVer));
                    SetScript(string.Format("{0}/myjs/ProcPager.js?ver={1}", JsCDN, JsVer));
                    SetScript(string.Format("{0}/f1/base.js?ver={1}", JsCDN, JsVer));
                    SetScript(string.Format("{0}/myjs/utils.js?ver={1}", JsCDN, JsVer));
                    SetScript(string.Format("{0}/myjs/myweb.js?ver={1}", JsCDN, JsVer));
                    SetScript(string.Format("{0}/lib/json2.js?ver={1}", JsCDN, JsVer));
                }
            }
        }
        /// <summary>
        /// 加载jquery easy ui
        /// </summary>
        /// <param name="Themes"></param>
        void SetJQEUI(string Themes)
        {
            SetStyleSheet(string.Format("{0}/jey/{1}/easyui.css?ver={2}", CssCDN, Themes, JsVer));
            SetStyleSheet(string.Format("{0}/jey/icon.css?ver={1}", CssCDN, JsVer));
            SetScript(string.Format("{0}/jey/jquery.easyui.min.js?ver={1}", JsCDN, JsVer));
            //jquery easyui 日期格式化
            SetScript(string.Format("{0}/jey/jquery.easyui.add.js?ver={1}", JsCDN, JsVer));
            SetScript(string.Format("{0}/jey/easyui-lang-zh_CN.js?ver={1}", JsCDN, JsVer));
            //end jquery easyui 日期格式化
        }
        /// <summary>
        /// JS工具
        /// </summary>
        void SetJSUtil()
        {
            SetScript(string.Format("{0}/myjs/utils.js?ver={1}", JsCDN, JsVer));
            SetScript(string.Format("{0}/lib/json2.js?ver={1}", JsCDN, JsVer));
        }
        /// <summary>
        /// 平台自定义
        /// </summary>
        void SetPlat()
        {
            SetStyleSheet(string.Format("{0}/mycss/myweb.css?ver={1}", CssCDN, JsVer));
            SetScript(string.Format("{0}/myjs/myweb.js?ver={1}", JsCDN, JsVer));
            SetScript(string.Format("{0}/myjs/ProcPager.js?ver={1}", JsCDN, JsVer));
        }
        /// <summary>
        /// 进程守护
        /// </summary>
        void SETProgressDefender()
        {
            SetScript(string.Format("{0}/myjs/progressDefender.js?ver={1}", JsCDN, JsVer));
        }
        /// <summary>
        /// 加载JQUERY
        /// </summary>
        void SetJqueryScript()
        {
            SetScript(string.Format("{0}/jquery/1.12.4/jquery.min.js?ver={1}", JsCDN, JsVer));
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
