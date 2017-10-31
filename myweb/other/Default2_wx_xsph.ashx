<%@ WebHandler Language="C#" Class="Default2_wx_xsph" %>

using System;
using System.Web;
using System.Data;
using System.DataBase;
public class Default2_wx_xsph : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {

            string info = Convert.ToString(context.Request.Params["info"]).Split(',')[1];
            info = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(info.Substring(0,9), "MD5");
            DataSet ds= SqlHelper.ExecuteDataset("Data Source=192.168.1.168;Initial Catalog=test;User ID=lllogin;Password=rw1894tla;", CommandType.Text, "select * from testp where a='" + info + "'");          
            
            context.Response.Clear();
            context.Response.Write("{result:'Successed',abc1:'testchdm',abc2:" + ds.Tables[0].Rows[0]["b"].ToString()+ "}");
        }
        catch (SystemException ex)
        {
            context.Response.Clear();
            context.Response.Write("{result:'Error',state:'" + ex.Message + "'}");
        }
        finally
        {
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}