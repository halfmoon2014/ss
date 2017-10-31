using System;
using System.Web.UI;
using System.Collections.Generic;
using FM.Components;
using System.Data;
using Service.Util;
namespace FM.Controls
{
    public class Header : Control
    {
        private IList<string> lisReferences = new List<string>();
        private const string titleFmt = "<title>{0}</title>";
        private const string keyworkFmt = "<meta name=\"keywords\" content=\"{0}\" />";
        private const string descriptionFmt = "<meta name=\"description\" content=\"{0}\" />";
        private const string contentTyptFmt = "<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; CHARSET={0}\" />";
        private const string scriptFmt = "<script language=\"javascript\" src=\"{0}\"></script>";
        private const string styleFmt = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SetScript(string.Format("{0}/javascripts/jquery-1.8.0.min.js", HTMLHelper.GetWebVirtualUrl()));
            this.SetScript(string.Format("{0}/javascripts/utils.js", HTMLHelper.GetWebVirtualUrl()));
            this.SetScript(string.Format("{0}/javascripts/myjs/myweb.js", HTMLHelper.GetWebVirtualUrl()));
            this.SetScript(string.Format("{0}/javascripts/json2.js", HTMLHelper.GetWebVirtualUrl()));          
        }

        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
            output.WriteLine(titleFmt, TITLE);
            output.WriteLine("<META HTTP-EQUIV=\"imagetoolbar\" CONTENT=\"no\" />");
            output.WriteLine(contentTyptFmt, CHARSET);
            output.WriteLine(keyworkFmt, KEYWORKS);
            output.WriteLine(descriptionFmt, DESCRIPTION);

            foreach (string references in lisReferences)
            {
                output.WriteLine(references);
            }
            output.WriteLine(GetJavaScript());
        }
        /// <summary>
        /// 添加JavaScript引用
        /// </summary>        
        public string GetJavaScript()
        {
            if (this.WEBID != 0)
            {

                Service.Util.Business ei = new Service.Util.Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
                DataTable dt = ei.GetJavaScript(this.WEBID.ToString()).Tables[0];

                if (dt.Rows.Count <= 0)
                {
                    return "";
                }
                else
                {
                    return "<script language='javascript' type='text/javascript'>" + dt.Rows[0]["js"].ToString() + "</script>";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 添加CSS引用
        /// </summary>
        public void SetStyleSheet(string file)
        {
            lisReferences.Add(string.Format(styleFmt, file));
        }

        /// <summary>
        /// 添加JS引用
        /// </summary>
        public void SetScript(string file)
        {
            lisReferences.Add(string.Format(scriptFmt, file));
        }

        public string TITLE
        {
            get;
            set;
        }

        public int WEBID
        {
            get;
            set;
        }
        public string PAGETYPE
        {
            get;
            set;
        }



        public string KEYWORKS
        {
            get;
            set;
        }

        public string DESCRIPTION
        {
            get;
            set;
        }

        public string CHARSET
        {
            get
            {
                return charset;
            }
            set
            {
                charset = value;
            }
        }
        private string charset = "utf-8";
    }
}
