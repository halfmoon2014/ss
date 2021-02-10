using MySession;
using MyTy;
using System;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class uploaditem : System.Web.UI.Page
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
            int bizId = int.Parse(Request.QueryString["bizId"]);
            string bizKey = Request.QueryString["bizKey"];
            int groupId =int.Parse(Request.QueryString["groupId"]);
            picPath = Server.MapPath("UpFile");
            doUpload(groupId,bizId,bizKey);
        }
    }

    protected void doUpload(int groupId,int bizId,string bizKey)
    {
        try
        {
            HttpPostedFile file = file1.PostedFile;
            string strNewPath = GetSaveFilePath() + GetExtension(file.FileName);
            file.SaveAs(picPath + strNewPath);
            string urlPath = picServer + strNewPath;
            urlPath = urlPath.Replace("\\", "/");
            Service.Util.Business business = new Service.Util.Business(SessionHandle.Get("tzid"), SessionHandle.Get("userid"));
            Result<int> res= business.SavePic(groupId, bizId, bizKey, urlPath, int.Parse(SessionHandle.Get("userid")));
            WriteJs("parent.uploadsuccess('" + urlPath + "','" + itemID + "',"+ res.Data+ "); ");

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