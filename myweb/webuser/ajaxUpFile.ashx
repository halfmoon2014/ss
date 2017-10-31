<%@ WebHandler Language="C#" Class="ajaxUpFile" %>
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.IO;
using System.Net;
using System.Text;

public class ajaxUpFile : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string _fileNamePath = "";
        try
        {
            /*需要安装IIS7 并且IIS需要集成管理模式*/
            _fileNamePath = context.Request.Form["upfile"];
            //开始上传
            string _savedFileResult = UpLoadFile(_fileNamePath);
            context.Response.Write(_savedFileResult);

        }
        catch
        {
            context.Response.Write("0|error|上传提交出错");
        }
    }
    /// <summary>
    /// 上传文件 方法
    /// </summary>
    /// <param name="fileNamePath"></param>
    /// <returns></returns>
    public string UpLoadFile(string fileNamePath)
    {

        return UpLoadFile(fileNamePath, "../UpFile");
    }

    /// <summary>
    /// 上传文件 方法
    /// </summary>
    /// <param name="fileNamePath"></param>
    /// <param name="toFilePath"></param>
    /// <returns>返回上传处理结果   格式说明： 0|file.jpg|msg   成功状态|文件名|消息    </returns>
    public string UpLoadFile(string fileNamePath, string toFilePath)
    {
        try
        {
            //获取要保存的文件信息
            FileInfo file = new FileInfo(fileNamePath);
            //获得文件扩展名
            string fileNameExt = file.Extension;

            //验证合法的文件
            if (CheckFileExt(fileNameExt))
            {
                //生成将要保存的随机文件名
                string fileName = GetFileName() + fileNameExt;
                //检查保存的路径 是否有/结尾
                if (toFilePath.EndsWith("/") == false) toFilePath = toFilePath + "/";

                //按日期归类保存
                string datePath = DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/";
                if (true)
                {
                    toFilePath += datePath;
                }

                //获得要保存的文件路径
                string serverFileName = toFilePath + fileName;
                //物理完整路径                    
                string toFileFullPath = HttpContext.Current.Server.MapPath(toFilePath);

                //检查是否有该路径  没有就创建
                if (!Directory.Exists(toFileFullPath))
                {
                    Directory.CreateDirectory(toFileFullPath);
                }

                //将要保存的完整文件名                
                string toFile = toFileFullPath + fileName;

                ///创建WebClient实例       
                WebClient myWebClient = new WebClient();
                //设定windows网络安全认证   方法1
                myWebClient.Credentials = CredentialCache.DefaultCredentials;
                ////设定windows网络安全认证   方法2
                //NetworkCredential cred = new NetworkCredential("UserName", "UserPWD");
                //CredentialCache cache = new CredentialCache();
                //cache.Add(new Uri("UploadPath"), "Basic", cred);
                //myWebClient.Credentials = cache;

                //要上传的文件       
                FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
                //FileStream fs = OpenFile();       
                BinaryReader r = new BinaryReader(fs);
                //使用UploadFile方法可以用下面的格式       
                //myWebClient.UploadFile(toFile, "PUT",fileNamePath);       
                byte[] postArray = r.ReadBytes((int)fs.Length);
                Stream postStream = myWebClient.OpenWrite(toFile, "PUT");
                if (postStream.CanWrite)
                {
                    postStream.Write(postArray, 0, postArray.Length);
                }
                else
                {
                    return "0|" + serverFileName + "|" + "文件目前不可写";
                }
                postStream.Close();


                return "1|" + serverFileName + "|" + "文件上传成功";
            }
            else
            {
                return "0|errorfile|" + "文件格式非法";
            }
        }
        catch (Exception e)
        {
            return "0|errorfile|" + "文件上传失败,错误原因：" + e.Message;
        }
    }

    /// <summary>
    /// 检查是否为合法的上传文件
    /// </summary>
    /// <param name="_fileExt"></param>
    /// <returns></returns>
    private bool CheckFileExt(string _fileExt)
    {
        return true;
        /*string[] allowExt = new string[] { ".gif", ".jpg", ".jpeg" };
        for (int i = 0; i < allowExt.Length; i++)
        {
            if (allowExt[i] == _fileExt) { return true; }
        }
        return false;*/

    }

    public static string GetFileName()
    {
        Random rd = new Random();
        StringBuilder serial = new StringBuilder();
        serial.Append(DateTime.Now.ToString("yyyyMMddHHmmss"));
        serial.Append(rd.Next(100000, 999999).ToString());
        return serial.ToString();

    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}