using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;

namespace Comet
{
    class CometAsyncHandler : IHttpAsyncHandler
    {
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object extraData)
        {
            //通过context可以取请求附加的数据，略
            //...

            //之后生成IAsyncResult对象，callback比较重要，调用这个回调，EndProcessRequest才被触发
            var result = new CometResult(context, callback, extraData);
            //在返回之前把刚生成的IAsyncResult对象保存起来，略
            //...
            return result;
        }

        public void EndProcessRequest(IAsyncResult asyncResult)
        {
            //得到对应的IAsyncResult对象
            var result = asyncResult as CometResult;
            //后续处理，如输出内容等，略
            //...
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
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
        public object ExtraData { get; private set; }
        public CometResult(HttpContext context, AsyncCallback callback, object extraData)
        {
            this.Context = context;
            this.Callback = callback;
            this.ExtraData = extraData;
        }
        public void Call()
        {
            if (this.Callback != null)
                this.Callback(this);
        }
    }
}
