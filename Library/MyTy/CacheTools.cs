using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
/// <summary>
/// CacheTools 的摘要说明
/// </summary>
namespace MyTy
{
    public class CacheTools
    {
        public CacheTools() { }
        #region 开发平台缓存
        private static string WidKey = "wid_";
        public static void WidInsert(string intWid, string userid, object data)
        {
            WidInsertDep(intWid);
            CacheDependency dep = new CacheDependency(null, new string[] { WidKey + intWid.ToString() });
            HttpRuntime.Cache.Insert(intWid.ToString() + "*" + userid.ToString(), data, dep,
                                        Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
        }
        public static object WidGet(string intWid, string userid)
        {
            return HttpRuntime.Cache.Get(intWid.ToString() + "*" + userid.ToString());
        }

        public static void WidInsertDep(string intWid)
        {
            if (HttpRuntime.Cache.Get(WidKey + intWid.ToString()) == null)
            {
                HttpRuntime.Cache.Insert(WidKey + intWid.ToString(), Guid.NewGuid().ToString());
            }
        }

        public static void WidUpdateDep(string intWid)
        {
            HttpRuntime.Cache.Insert(WidKey + intWid.ToString(), Guid.NewGuid().ToString());
        }
        #endregion

        #region dbConn
        private static string DbConnKey = "dbConn_";
        public static void DbConnInsert(string tzid,string userid, object data)
        {
            HttpRuntime.Cache.Insert(DbConnKey+tzid+"*"+userid, data);
        }

        public static object DbConnGet(string tzid, string userid)
        {
            return HttpRuntime.Cache.Get(DbConnKey + tzid + "*" + userid);
        }
        #endregion

        #region bllConn
        private static string BllConnKey = "bllConn_";
        public static void BllConnInsert(string tzid, object data)
        {
            HttpRuntime.Cache.Insert(BllConnKey+tzid, data);
        }

        public static object BllConnGet(string tzid)
        {
            return HttpRuntime.Cache.Get(BllConnKey+tzid);
        }
        #endregion

        #region masterConn
        private static string MasterConnKey = "masterConn";
        public static void MasterConnInsert(object data)
        {
            HttpRuntime.Cache.Insert(MasterConnKey, data);
        }

        public static object MasterConnGet()
        {
            return HttpRuntime.Cache.Get(MasterConnKey);
        }
        #endregion

        #region mbConn
        private static string MbConnKey = "mbConn";
        public static void MbConnInsert(object data)
        {
            HttpRuntime.Cache.Insert(MbConnKey, data);
        }

        public static object MbConnGet()
        {
            return HttpRuntime.Cache.Get(MbConnKey);
        }
        #endregion

        #region
        private static string JsVer = "jsVer";
        public static void JsVerInsert(object data)
        {
            HttpRuntime.Cache.Insert(JsVer, data);
        }
        public static object jsVerGet()
        {
            return HttpRuntime.Cache.Get(JsVer);
        }
        #endregion
    }
}