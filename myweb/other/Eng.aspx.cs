using System;
using System.Text;
using System.Net;
using System.IO;
public partial class other_Eng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = Request.Url.AbsoluteUri.Replace(Request.Url.Authority + Request.CurrentExecutionFilePath, "192.168.1.204:4529/ent.aspx");

        
        HttpWebResponse response = MyTy.HttpWebResponseUtility.CreatePostHttpResponse(url, Request.InputStream, null, Request.UserAgent, Request.ContentEncoding, null, Request.ContentType);
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);        

        Response.Clear();
        Response.Write(reader.ReadToEnd());
        Response.Flush();
        Response.End();
    }     
       
    
}