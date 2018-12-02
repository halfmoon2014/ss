using Log4NetApply;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace Comet
{
    /// <summary>
    /// 长连接对象管理类
    /// </summary>
    public class LongSataMrg
    {
        public static LongSataMrg instance = null;

        public static LongSataMrg getInstance()
        {
            if (instance == null)
            {
                instance = new LongSataMrg();
            }
            return instance;
        }
        public static int Send(string to, object data)
        {
            int code = 2002;//没有连接

            for (int i = clienConnetList.Count - 1; i >= 0; i--)
            {
                Complex complex = clienConnetList[i];

                //按用户名发送
                if (string.Equals(complex.Name, to, StringComparison.OrdinalIgnoreCase))
                {
                    //连接失效了
                    if (!complex.CometResult.Context.Response.IsClientConnected)
                    {
                        clienConnetList.Remove(complex);
                        code = Math.Min(code, 2001);//有可能存在多个相同用户名的连接
                    }
                    else
                    {
                        complex.CometResult.ExtraData = data;
                        code = complex.CometResult.Call();
                        clienConnetList.Remove(complex);
                    }
                }
            }
            return code;
        }
        public static List<Complex> clienConnetList = new List<Complex>();
    }
    /// <summary>
    /// 长连接实体
    /// </summary>
    public class Complex
    {
        private string name;
        private string guid;
        private DateTime createTime;
        private CometResult cometResult;
        private string ip;
        private string title;
        private int connID;

        public Complex(string name,string title,int connID, string guid, CometResult cometResult, DateTime createTime,string ip)
        {
            Name = name;
            Guid = guid;
            CometResult = cometResult;
            CreateTime = createTime;
            Ip = ip;
            Title = title;
            ConnID = connID;
        }
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }
        public int ConnID
        {
            get
            {
                return connID;
            }

            set
            {
                connID = value;
            }
        }
        public string Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        public string Guid
        {
            get
            {
                return guid;
            }

            set
            {
                guid = value;
            }
        }
        public CometResult CometResult
        {
            get
            {
                return cometResult;
            }

            set
            {
                cometResult = value;
            }
        }
        public DateTime CreateTime
        {
            get
            {
                return createTime;
            }

            set
            {
                createTime = value;
            }
        }
          
    }

    /// <summary>
    /// 异步
    /// </summary>
    class CometAsyncHandler : IHttpAsyncHandler
    {
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object extraData)
        {

            //通过context可以取请求附加的数据，略
            //...            
            //之后生成IAsyncResult对象，callback比较重要，调用这个回调，EndProcessRequest才被触发
            var result = new CometResult(context, callback, extraData);
            //在返回之前把刚生成的IAsyncResult对象保存起来，略
            string n = context.Request.QueryString["n"].ToString();
            string g = context.Request.QueryString["g"].ToString();
            string title = Microsoft.JScript.GlobalObject.decodeURI(context.Request.QueryString["title"].ToString());
            int connID =int.Parse(context.Request.QueryString["b"].ToString());
            LogHelper.WriteLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, new LogContent("ip", n, "BeginProcessRequest", g));
            string ip= "Can not get";
            if (context.Request.ServerVariables["HTTP_VIA"] != null) // using proxy
                ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
            else// not using proxy or can't get the Client IP
                ip = context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.

            LongSataMrg.clienConnetList.Add(new Complex(n,title,connID, g, result, DateTime.Now,ip));
            for (int i = LongSataMrg.clienConnetList.Count - 1; i >= 0; i--)
            {
                Complex complex = LongSataMrg.clienConnetList[i];
                //连接失效了
                if (!complex.CometResult.Context.Response.IsClientConnected)
                {
                    complex.CometResult.Call();
                    LongSataMrg.clienConnetList.Remove(complex);
                }
            }
            return result;
        }

        public void EndProcessRequest(IAsyncResult asyncResult)
        {
            //得到对应的IAsyncResult对象
            //var result = asyncResult as CometResult;
            //后续处理，如输出内容等，略
            //...

        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
    public class CometResult : IAsyncResult
    {
        #region 实现IAsyncResult接口
        public object AsyncState { get; private set; }
        public WaitHandle AsyncWaitHandle { get; private set; }
        public bool CompletedSynchronously { get; private set; }
        public bool IsCompleted { get; private set; }
        #endregion
        public AsyncCallback Callback { get; private set; }
        public HttpContext Context { get; private set; }
        public object ExtraData { get; set; }

        public CometResult(HttpContext context, AsyncCallback callback, object extraData)
        {
            Context = context;
            Callback = callback;
            ExtraData = extraData;
            IsCompleted = false;
        }
        public int Call()
        {
            Context.Response.Write(ExtraData);
            IsCompleted = true;
            if (this.Callback != null)
                this.Callback(this);
            return 0;
        }
    }
}
