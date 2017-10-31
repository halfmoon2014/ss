using System;
using System.Text;
using EI.Web;
using EI.Web.Modal;
using System.Collections.Generic;
using System.Collections.Specialized;
public partial class lss : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region
        int intWid = 0;
        if (Request["wid"] != null)
        {
            intWid = int.Parse(Request["wid"].ToString());
        }
        if (intWid == 0)
        {//lss.aspx一定需要wid
            Response.Clear();
            Response.Write("缺少必要参数");
            Response.Flush();
            Response.End();
        }
        else
        {
            sysHead.WEBID = intWid;
            if (Request["title"] != null)
            {
                sysHead.TITLE = Request["title"].ToString().Trim();
            }
            StringBuilder innerHtml = new StringBuilder();
            StringBuilder cache = new StringBuilder();
            int tzid = int.Parse(MySession.SessionHandle.Get("tzid").ToString().Trim());
            int userid =int.Parse(MySession.SessionHandle.Get("userid").ToString().Trim());
            string menupage = MySession.SessionHandle.Get("menupage").ToString();
            FM.Business.Login lg = new FM.Business.Login();
            string username = lg.GetUser(userid.ToString()).Tables[0].Rows[0]["name"].ToString();
            cache = (StringBuilder)CacheTools.Get(intWid.ToString(), userid.ToString());
            if (1 == 1/*cache==null*/)
            {
                WebEdit webEdit = new WebEdit(tzid.ToString(), userid.ToString(), username);
                Dictionary<string, NameValueCollection> requestParameter = new Dictionary<string, NameValueCollection>();
                //得到URL参数        
                requestParameter.Add("QueryString", Request.QueryString);
                requestParameter.Add("Form", Request.Form);

                Html layout = webEdit.WebLayOut(intWid, requestParameter);                
                innerHtml.Append(layout.HtmlMark);
                innerHtml.Append("<input type=\"hidden\"  id=\"wid\" IsEasyLayout=\"" + layout.IsEasyLayout.ToString() + "\"  value=\"" + intWid.ToString() + "\" />");
                innerHtml.Append("<input type=\"hidden\"  id=\"username\" a=\"" + menupage + "\" b=\"" + tzid.ToString() + "\" value=\"" + username + "\" />");
                CacheTools.Insert(intWid.ToString(), userid.ToString(), innerHtml);
            }
            else
            {
                innerHtml = cache;
            }
            platformbody.InnerHtml = innerHtml.ToString();
        }
                     ;

        #endregion
    }


}