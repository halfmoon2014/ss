using System;
using System.Web.UI;
using System.Collections.Generic;
using FM.Components;
using System.Data;
namespace FM.Controls
{
    public class Header : Control
    {
        private IList<string> listReferences = new List<string>();        
        private const string keyWorkFmt = "<meta name=\"keywords\" content=\"{0}\" />";
        private const string descriptionFmt = "<meta name=\"description\" content=\"{0}\" />";
        private const string contentTyptFmt = "<META CHARSET={0}\" />";
        private const string scriptFmt = "<script language=\"javascript\" src=\"{0}\"></script>";
        private const string styleFmt = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);         
        }

        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
            Page.Header.Title = Title;
            output.WriteLine(contentTyptFmt, Charset);
            output.WriteLine("<META HTTP-EQUIV=\"X-UA-Compatible\" CONTENT=\"IE=edge\" />");
            output.WriteLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");            
            output.WriteLine(keyWorkFmt, KeyWorks);
            output.WriteLine(descriptionFmt, Description);
            foreach (string references in listReferences)
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
            if (this.WebID != 0)
            {
                Service.Util.Business ei = new Service.Util.Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
                DataTable dt = ei.GetJavaScript(this.WebID.ToString()).Tables[0];

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
            listReferences.Add(string.Format(styleFmt, file));
        }

        /// <summary>
        /// 添加JS引用
        /// </summary>
        public void SetScript(string file)
        {
            listReferences.Add(string.Format(scriptFmt, file));
        }

        public string Title
        {
            get;
            set;
        }

        public int WebID
        {
            get;
            set;
        }

        public string PageType
        {
            get;
            set;
        }

        public string KeyWorks
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Charset
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
