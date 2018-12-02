using System;
using System.Text;
using EI.Web;
using EI.Web.Modal;
using DTO;
using MyTy;
using System.Data;

public partial class lss : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region
        int intWid = 0;
        if (Request["wid"] != null)
            intWid = int.Parse(Request["wid"].ToString());

        if (intWid == 0)
        {
            //lss.aspx一定需要wid
            Response.Clear();
            Response.Write("缺少必要参数");
            Response.Flush();
            Response.End();
        }
        else
        {
            sysHead.WebID = intWid;
            if (Request["title"] != null)
                sysHead.Title = Request["title"].ToString().Trim();

            StringBuilder innerHtml = new StringBuilder();
            StringBuilder cache = new StringBuilder();
            int tzid = int.Parse(MySession.SessionHandle.Get("tzid").ToString().Trim());
            int userid = int.Parse(MySession.SessionHandle.Get("userid").ToString().Trim());
            string menupage = MySession.SessionHandle.Get("menupage").ToString();
            FM.Business.Login lg = new FM.Business.Login();
            DataRow userDR = lg.GetUser(userid.ToString()).Tables[0].Rows[0];
            string username = userDR["name"].ToString();
            string usr =  userDR["usr"].ToString();
            cache = (StringBuilder)CacheTools.WidGet(intWid.ToString(), userid.ToString());
            //if (1 == 1/*cache==null*/)
            //{
            WebEdit webEdit = new WebEdit(tzid.ToString(), userid.ToString(), username);
           
            HtmlParameter htmlParameter = new HtmlParameter();
            htmlParameter.QueryString = Request.QueryString;
            htmlParameter.Form = Request.Form;            
            Html layout = webEdit.WebLayOut(intWid, htmlParameter,MyTy.RequestExtensions.IsMobileBrowser(Request) );
            innerHtml.Append(layout.HtmlMark);
            innerHtml.Append("<input type=\"hidden\"  id=\"wid\" IsEasyLayout=\"" + layout.IsEasyLayout.ToString() + "\"  value=\"" + intWid.ToString() + "\" />");
            
            innerHtml.Append("<input type=\"hidden\"  id=\"username\" a=\"" + menupage + "\" b=\"" + tzid.ToString() + "\"  usr=\""+usr+"\" value=\"" + username + "\" longpollingurl=\"" + MyCode.GetAppSettings().LongPollingUrl + "\" />");
            innerHtml.Append("<dialog id=\"platDialog\" style=\"border: 3px;padding:16px;\"><iframe style=\"width: 800px; height: 600px\" id=\"platIframe\" frameborder=\"0\" ></iframe></dialog>");
            CacheTools.WidInsert(intWid.ToString(), userid.ToString(), innerHtml);
            //}
            //else
            //{
            //    innerHtml = cache;
            //}
            platformbody.InnerHtml = innerHtml.ToString();
        }                    ;

        #endregion
    }


}