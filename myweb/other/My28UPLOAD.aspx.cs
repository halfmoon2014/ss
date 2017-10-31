using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class upload : System.Web.UI.Page
{
    string picPath = "";
    string picServer = "UpFile";
    protected string itemID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != null)
        {
            itemID = Request.QueryString["id"];
        }

        if (IsPostBack)
        {
            picPath = Server.MapPath("UpFile");
            doUpload();
        }
    }

    protected void doUpload()
    {
        try
        {
            HttpPostedFile file = file1.PostedFile;
            string strNewPath = GetSaveFilePath() + GetExtension(file.FileName);
            file.SaveAs(picPath + strNewPath);
            string urlPath = picServer + strNewPath;
            urlPath = urlPath.Replace("\\", "/");
            WriteJs("parent.uploadsuccess('" + urlPath + "','" + itemID + "'); ");

        }
        catch (Exception ex)
        {
            WriteJs("parent.uploaderror();console.log('" + ex.Message+"');");
        }
    }

    private string GetExtension(string fileName)
    {
        try
        {
            int startPos = fileName.LastIndexOf(".");
            string ext = fileName.Substring(startPos, fileName.Length - startPos);
            return ext;
        }
        catch (Exception ex)
        {
            WriteJs("parent.uploaderror('" + itemID + "');console.log('" + ex.Message + "')");
            return string.Empty;
        }
    }

    private string GetSaveFilePath()
    {
        try
        {
            DateTime dateTime = DateTime.Now;
            string yearStr = dateTime.Year.ToString(); ;
            string monthStr = dateTime.Month.ToString();
            string dayStr = dateTime.Day.ToString();
            string hourStr = dateTime.Hour.ToString();
            string minuteStr = dateTime.Minute.ToString();
            string dir = dateTime.ToString(@"\\yyyyMMdd");
            if (!Directory.Exists(picPath + dir))
            {
                Directory.CreateDirectory(picPath + dir);
            }
            return dir + dateTime.ToString("\\\\yyyyMMddhhmmssffff");
        }
        catch (Exception ex)
        {
            WriteJs("parent.uploaderror();console.log('" + ex.Message + "')");
            return string.Empty;
        }
    }

    protected void WriteJs(string jsContent)
    {
        Page.ClientScript.RegisterStartupScript(typeof(string),"writejs","<script type='text/javascript'>" + jsContent + "</script>");
        
    }
}