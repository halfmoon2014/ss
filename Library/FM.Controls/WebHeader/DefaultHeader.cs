using System;
using System.DataBase;
using System.Data;
using System.Web;
using FM.Components;
using System.Collections.Generic;
using FM.Controls;

namespace FM.Controls
{
    public class DefaultHeader : Header, System.Web.IHttpHandler
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string url = HttpContext.Current.Request.Path.ToString();
            string ApplicationPath = HttpContext.Current.Request.ApplicationPath.ToString().Trim();            
            MyTy.MyCode myCode = new MyTy.MyCode();
            Dictionary<string, string> pageThemes = myCode.GetPageThemes();
            
            if (myCode.CheckMenuPage(url.Replace(ApplicationPath + "/", ""), "MenuPage"))
            {
                //首页,标题读取
                FM.Business.ProcPager pr = new FM.Business.ProcPager();
                DataTable dt = pr.GetPosConfig(MySession.SessionHandle.Get("tzid")).Tables[0];
                if (dt.Rows.Count != 0) { this.TITLE = dt.Rows[0]["pos_name"].ToString(); }
            }

            if (myCode.CheckMenuPage(url.Replace(ApplicationPath + "/", ""), "PrintPage") || myCode.CheckMenuPage(url.Replace(ApplicationPath + "/", ""), "ExcelPage"))
            {
                //打印页,导EXCEL页
                // this.SetStyleSheet(string.Format("{0}/css/f1/main.css", HTMLHelper.GetWebVirtualUrl()));
                //this.SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", HTMLHelper.GetWebVirtualUrl()));
                this.SetScript(string.Format("{0}/AjaxHandler/ProcPager.js", HTMLHelper.GetWebVirtualUrl()));
                //this.SetScript(string.Format("{0}/javascripts/f1/base.js", HTMLHelper.GetWebVirtualUrl()));
                //
                this.SetStyleSheet(string.Format("{0}/css/jey/{1}/easyui.css", HTMLHelper.GetWebVirtualUrl(), pageThemes["Themes"]));
                this.SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", HTMLHelper.GetWebVirtualUrl()));
                this.SetStyleSheet(string.Format("{0}/css/jey/icon.css", HTMLHelper.GetWebVirtualUrl()));
                this.SetStyleSheet(string.Format("{0}/css/jey/mycss.css", HTMLHelper.GetWebVirtualUrl()));
                //jquery easyui 日期格式化
                this.SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.add.js", HTMLHelper.GetWebVirtualUrl()));
                this.SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.min.js", HTMLHelper.GetWebVirtualUrl()));
                this.SetScript(string.Format("{0}/javascripts/jey/easyui-lang-zh_CN.js", HTMLHelper.GetWebVirtualUrl()));
            }
            //发现,右键页标签没有处理,暂时先直接加载全部jquery easyui
            //else if (myCode.CheckMenuPage(url.Replace(ApplicationPath + "/", ""), "MenuPage"))
            //{//菜单页


            //    if (string.Compare(pageThemes["PageThemes"], "jqeui") != 0)
            //    {
            //        //或者主题不是jqeui
            //        this.SetStyleSheet(string.Format("{0}/css/f1/main.css", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/AjaxHandler/ProcPager.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/f1/base.js", HTMLHelper.GetWebVirtualUrl()));
            //    }
            //    else if (string.Compare(pageThemes["PageThemes"], "jqeui") == 0)
            //    {
            //        this.SetStyleSheet(string.Format("{0}/css/jey/{1}/easyui.css", HTMLHelper.GetWebVirtualUrl(), pageThemes["Themes"]));
            //        this.SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetStyleSheet(string.Format("{0}/css/jey/icon.css", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetStyleSheet(string.Format("{0}/css/jey/mycss.css", HTMLHelper.GetWebVirtualUrl()));
            //        //jquery easyui 日期格式化
            //        this.SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.add.js", HTMLHelper.GetWebVirtualUrl()));
            //        //插件很大277.4kb
            //        //this.SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.min.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.parser.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.resizable.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.panel.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.layout.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.accordion.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.tabs.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.menu.js", HTMLHelper.GetWebVirtualUrl()));                    
            //        //end插件很大277.4kb

            //        this.SetScript(string.Format("{0}/javascripts/jey/easyui-lang-zh_CN.js", HTMLHelper.GetWebVirtualUrl()));
            //    }
            //}
            else if (myCode.CheckMenuPage(url.Replace(ApplicationPath + "/", ""), "Login") || myCode.CheckMenuPage(url.Replace(ApplicationPath + "/", ""), "ChooseTz"))
            {//登陆页,套账选择页

            }
            //else if (myCode.CheckMenuPage(url.Replace(ApplicationPath + "/", ""), "SysModulePage"))
            //{//登陆页,套账选择页
            //    if (string.Compare(pageThemes["PageThemes"], "jqeui") != 0)
            //    {
            //        //或者主题不是jqeui
            //        this.SetStyleSheet(string.Format("{0}/css/f1/main.css", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/AjaxHandler/ProcPager.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/f1/base.js", HTMLHelper.GetWebVirtualUrl()));
            //    }
            //    else if (string.Compare(pageThemes["PageThemes"], "jqeui") == 0)
            //    {
            //        this.SetStyleSheet(string.Format("{0}/css/jey/{1}/easyui.css", HTMLHelper.GetWebVirtualUrl(), pageThemes["Themes"]));
            //        this.SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetStyleSheet(string.Format("{0}/css/jey/icon.css", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetStyleSheet(string.Format("{0}/css/jey/mycss.css", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetStyleSheet(string.Format("{0}/css/jey/pepper-grinder/textbox.css", HTMLHelper.GetWebVirtualUrl()));
            //        //jquery esay ui 日期控件要引用的JS,顺序不要打乱
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.parser.js", HTMLHelper.GetWebVirtualUrl()));                    
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.validatebox.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.linkbutton.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.textbox.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.tooltip.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.panel.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.calendar.js", HTMLHelper.GetWebVirtualUrl()));

            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.window.js", HTMLHelper.GetWebVirtualUrl()));

            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.combo.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/plugins/jquery.datebox.js", HTMLHelper.GetWebVirtualUrl()));
            //        // end jquery esay ui 日期控件要引用的JS,顺序不要打乱


            //        //jquery easyui 日期格式化
            //        this.SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.add.js", HTMLHelper.GetWebVirtualUrl()));
            //        this.SetScript(string.Format("{0}/javascripts/jey/easyui-lang-zh_CN.js", HTMLHelper.GetWebVirtualUrl()));
            //        //end jquery easyui 日期格式化

            //    }
            //}               
            else
            {//其它页
                if (string.Compare(pageThemes["PageThemes"], "jqeui") != 0)
                {
                    //或者主题不是jqeui
                    this.SetStyleSheet(string.Format("{0}/css/f1/main.css", HTMLHelper.GetWebVirtualUrl()));
                    this.SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", HTMLHelper.GetWebVirtualUrl()));
                    this.SetScript(string.Format("{0}/AjaxHandler/ProcPager.js", HTMLHelper.GetWebVirtualUrl()));
                    this.SetScript(string.Format("{0}/javascripts/f1/base.js", HTMLHelper.GetWebVirtualUrl()));
                }
                else if (string.Compare(pageThemes["PageThemes"], "jqeui") == 0)
                {
                    this.SetStyleSheet(string.Format("{0}/css/jey/{1}/easyui.css", HTMLHelper.GetWebVirtualUrl(), pageThemes["Themes"]));
                    this.SetStyleSheet(string.Format("{0}/css/mycss/myweb.css", HTMLHelper.GetWebVirtualUrl()));
                    this.SetStyleSheet(string.Format("{0}/css/jey/icon.css", HTMLHelper.GetWebVirtualUrl()));
                    this.SetStyleSheet(string.Format("{0}/css/jey/mycss.css", HTMLHelper.GetWebVirtualUrl()));
                    
                    this.SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.min.js", HTMLHelper.GetWebVirtualUrl()));
                    //jquery easyui 日期格式化
                    this.SetScript(string.Format("{0}/javascripts/jey/jquery.easyui.add.js", HTMLHelper.GetWebVirtualUrl()));                    
                    this.SetScript(string.Format("{0}/javascripts/jey/easyui-lang-zh_CN.js", HTMLHelper.GetWebVirtualUrl()));
                    //end jquery easyui 日期格式化

                }
            }
            
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
