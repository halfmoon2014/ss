using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

public partial class webpage_ProcPager_SysExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FM.Controls.FMPager fm = new FM.Controls.FMPager();
        fm = new FM.Controls.Pager.ProcPager();
        fm.GetDate();
        printForm.InnerHtml = "<div><table class=\"title_prt\"><tr><td id=\"excelTitle\">" + Request.Params["title"] + "</td></tr></table></div>" +
            "<div id=\"divPager\"  >" + fm.Html() + "</div>";

        Response.Clear();
        Response.Buffer = true;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.
        Now.ToString("yyyyMMdd") + ".xls");
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        this.EnableViewState = false;
    }

    #region 向Url发送post请求,返回网站响应内容

    /// <summary>
    /// 向Url发送post请求,返回网站响应内容
    /// </summary>
    /// <param name="postData">发送数据</param>
    /// <param name="uriStr">接受数据的Url</param>
    /// <param name="action">更新操作</param>
    /// <returns>返回网站响应内容</returns>
    public static string RequestPost(string uriStr, HttpRequest Request)
    {
        //HttpWebRequest requestScore = (HttpWebRequest)WebRequest.Create(uriStr);

        //        string root = HttpContext.Request;

        uriStr = "http://" + HttpContext.Current.Request.UserHostAddress + ":" + HttpContext.Current.Request.Url.Port + "/myweb" + uriStr;
        HttpWebRequest requestScore = (HttpWebRequest)WebRequest.Create(uriStr);
        StringBuilder postContent = new StringBuilder();
        Encoding myEncoding = Encoding.GetEncoding("utf-8");
        // + parm+ "filterRow:\"" + filterRow + "\",prtFlag:\"sysprt\",orderBy:\"" + orderBy + "\",pageSize:\"" + pageSize

        postContent.Append(HttpUtility.UrlEncode("message", myEncoding));
        postContent.Append("=");
        postContent.Append(HttpUtility.UrlEncode("post", myEncoding));

        //
        postContent.Append("&");
        postContent.Append(HttpUtility.UrlEncode("filterRow", myEncoding));
        postContent.Append("=");
        postContent.Append(HttpUtility.UrlEncode((Request.QueryString["sysFindSortRow"] == "" ? "" : " and " + Request.QueryString["sysFindSortRow"]), myEncoding));
        postContent.Append("&");
        postContent.Append(HttpUtility.UrlEncode("prtFlag", myEncoding));
        postContent.Append("=");
        postContent.Append(HttpUtility.UrlEncode("sysPrint", myEncoding));
        postContent.Append("&");
        postContent.Append(HttpUtility.UrlEncode("orderBy", myEncoding));
        postContent.Append("=");
        postContent.Append(HttpUtility.UrlEncode(Request.QueryString["orderBy"], myEncoding));
        postContent.Append("&");
        postContent.Append(HttpUtility.UrlEncode("pageSize", myEncoding));
        postContent.Append("=");
        postContent.Append(HttpUtility.UrlEncode(Request.QueryString["pageSize"], myEncoding));
        for (int i = 0; i < Request.QueryString["parm"].Split(',').Length; i++)
        {
            postContent.Append("&");
            postContent.Append(HttpUtility.UrlEncode(Request.QueryString["parm"].Split(',')[i].Split(':')[0].Replace("\"", ""), myEncoding));
            postContent.Append("=");
            postContent.Append(HttpUtility.UrlEncode(HttpUtility.UrlEncode(Request.QueryString["parm"].Split(',')[i].Split(':')[1], myEncoding)));
        }

        byte[] data = Encoding.ASCII.GetBytes(postContent.ToString());
        requestScore.Method = "Post";

        requestScore.Headers.Add("Cookie", Request.Headers["Cookie"]);

        requestScore.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
        requestScore.ContentLength = data.Length;
        requestScore.KeepAlive = false;
        Stream stream = requestScore.GetRequestStream();
        stream.Write(data, 0, data.Length);
        stream.Close();
        HttpWebResponse responseSorce;
        string content = "";
        try
        {
            responseSorce = (HttpWebResponse)requestScore.GetResponse();
            StreamReader reader = new StreamReader(responseSorce.GetResponseStream(), Encoding.UTF8);
            content = reader.ReadToEnd();
            reader.Dispose();
            responseSorce.Close();
        }
        catch (WebException ex)
        {
            throw ex;
            //responseSorce = (HttpWebResponse)ex.Response;//得到请求网站的详细错误提示
        }

        requestScore.Abort();
        stream.Dispose();
        return content;
    }

    #endregion 向Url发送post请求,返回网站响应内容
}