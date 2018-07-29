using System;
using System.Data;
using System.Web;
using FM.Components;
using System.Collections.Generic;

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
            Dictionary<string, string> pageThemes = myCode.GetPageThemes();

            //首页,标题读取
            if (myCode.CheckPageType(absolutePath, "MenuPage"))
            {
                Business.ProcPager pr = new Business.ProcPager();
                DataTable dt = pr.GetPosConfig(MySession.SessionHandle.Get("tzid")).Tables[0];
                if (dt.Rows.Count != 0) { this.Title = dt.Rows[0]["pos_name"].ToString(); }
            }

            //登陆页,套账选择页
            if (myCode.CheckPageType(absolutePath, "Login") || myCode.CheckPageType(absolutePath, "ChooseTz")
                || myCode.CheckPageType(absolutePath, "SysXTSZ&Ordinary") || myCode.CheckPageType(absolutePath, "Other&Ordinary")) {
                SetJqueryScript();
            }
            else if (myCode.CheckPageType(absolutePath, "SysXTSZ&JQY")|| myCode.CheckPageType(absolutePath, "Other&JQY")) {
                SetJqueryScript();
                SetJQEUI(pageThemes["Themes"]);
            }
            else if (myCode.CheckPageType(absolutePath, "MenuPage"))
            {
                SetJqueryScript();
                SetJQEUI(pageThemes["Themes"]);
                SetJSUtil();
                SETProgressDefender();
            }
            //其它页             
            else
            {
                if (string.Compare(pageThemes["PageThemes"], "jqeui") == 0)
                {
                    SetJqueryScript();
                    SetJQEUI(pageThemes["Themes"]);
                    SetJSUtil();
                    SetPlat();
                    SETProgressDefender();
                }
                //或者主题不是jqeui
                else if (string.Compare(pageThemes["PageThemes"], "jqeui") != 0)
                {
                    SetJqueryScript();
                    SetStyleSheet(string.Format("{0}/css/f1/main.css", ""));
                    SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", ""));
                    SetScript(string.Format("{0}/AjaxHandler/ProcPager.js", ""));
                    SetScript(string.Format("{0}/javascripts/f1/base.js", ""));
                    SetScript(string.Format("{0}/javascripts/utils.js", ""));
                    SetScript(string.Format("{0}/javascripts/myjs/myweb.js", ""));
                    SetScript(string.Format("{0}/javascripts/json2.js", ""));
                }
            }
        }
        /// <summary>
        /// 加载jquery easy ui
        /// </summary>
        /// <param name="Themes"></param>
        void SetJQEUI(string Themes)
        {
            SetStyleSheet(string.Format("{0}/css/jey/{1}/easyui.css", "", Themes));
            SetStyleSheet(string.Format("{0}/css/jey/icon.css", ""));          
            SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.min.js", ""));
            //jquery easyui 日期格式化
            SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.add.js", ""));
            SetScript(string.Format("{0}/javascripts/jey/easyui-lang-zh_CN.js", ""));
            //end jquery easyui 日期格式化
        }
        /// <summary>
        /// JS工具
        /// </summary>
        void SetJSUtil()
        {            
            SetScript(string.Format("{0}/javascripts/utils.js", ""));            
            SetScript(string.Format("{0}/javascripts/json2.js", ""));            
        }
        /// <summary>
        /// 平台自定义
        /// </summary>
        void SetPlat()
        {
            SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", ""));
            SetScript(string.Format("{0}/javascripts/myjs/myweb.js", ""));
            SetScript(string.Format("{0}/AjaxHandler/ProcPager.js", ""));
        }
        /// <summary>
        /// 进程守护
        /// </summary>
        void SETProgressDefender()
        {
            SetScript(string.Format("{0}/javascripts/progressDefender.js", ""));            
        }
        /// <summary>
        /// 加载JQUERY
        /// </summary>
        void SetJqueryScript()
        {
            SetScript(string.Format("{0}/javascripts/jquery/1.12.4/jquery.min.js", ""));
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
