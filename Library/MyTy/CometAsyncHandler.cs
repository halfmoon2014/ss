using Log4NetApply;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace Comet
{
    public class LongSataMrg
    {
        public static LongSataMrg instance = null;
        public string name;
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
            if (clienConnetList.ContainsKey(to))
            {
                if (!clienConnetList[to].Context.Response.IsClientConnected)
                {
                    clienConnetList.Remove(to);
                    return 1001;
                }
                else
                {
                    clienConnetList[to].ExtraData = data;
                    int code= clienConnetList[to].Call();
                    clienConnetList.Remove(to);
                    return code;
                }
            }
            return 1002;

        }
        public static Dictionary<string, CometResult> clienConnetList = new Dictionary<string, CometResult>();
    }



    class CometAsyncHandler : IHttpAsyncHandler
    {
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object extraData)
        {

            //通过context可以取请求附加的数据，略
            //...            
            //之后生成IAsyncResult对象，callback比较重要，调用这个回调，EndProcessRequest才被触发
            var result = new CometResult(context, callback, extraData);
            //在返回之前把刚生成的IAsyncResult对象保存起来，略
            string from = context.Request.QueryString["u"].ToString();
            LogHelper.WriteLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, new LogContent("ip", "", "BeginProcessRequest", from));
            if (LongSataMrg.clienConnetList.ContainsKey(from))
            {
                if (LongSataMrg.clienConnetList[from].Context.Response.IsClientConnected)
                    LongSataMrg.clienConnetList[from].Call();

                LongSataMrg.clienConnetList.Remove(from);
                LongSataMrg.clienConnetList[from] = result;
            }
            else
            {
                LongSataMrg.clienConnetList.Add(from, result);
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
