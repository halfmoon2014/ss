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
        public static void WidInsert(string intWid, string userid, object data)
        {
            WidInsertDep(intWid);
            CacheDependency dep = new CacheDependency(null, new string[] { "wid_" + intWid.ToString() });
            HttpRuntime.Cache.Insert(intWid.ToString() + "*" + userid.ToString(), data, dep,
                                        Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
        }
        public static object WidGet(string intWid, string userid)
        {
            return HttpRuntime.Cache.Get(intWid.ToString() + "*" + userid.ToString());
        }

        public static void WidInsertDep(string intWid)
        {
            if (HttpRuntime.Cache.Get("wid_" + intWid.ToString()) == null)
            {
                HttpRuntime.Cache.Insert("wid_" + intWid.ToString(), Guid.NewGuid().ToString());
            }
        }

        public static void WidUpdateDep(string intWid)
        {
            HttpRuntime.Cache.Insert("wid_" + intWid.ToString(), Guid.NewGuid().ToString());
        }

        public static void ConnInsert(string tzid, string userid, object data)
        {
            HttpRuntime.Cache.Insert("conn" + tzid + userid, data);
        }

        public static object ConnGet(string tzid, string userid)
        {
            return HttpRuntime.Cache.Get("conn" + tzid + userid);
        }

    }
}