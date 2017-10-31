using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
/// <summary>
/// CacheTools 的摘要说明
/// </summary>
public class CacheTools
{
    public CacheTools()
    {
        
    }
    public static void Insert(string intWid,string userid,object data)
    {
        InsertDep(intWid);
        CacheDependency dep = new CacheDependency(null, new string[] { "key_" + intWid.ToString() });
        HttpRuntime.Cache.Insert(intWid.ToString() + "*" + userid.ToString(), data, dep,
                                    Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
    }
    public static object Get(string intWid, string userid)
    {
      return  HttpRuntime.Cache.Get(intWid.ToString() + "*" + userid.ToString());
    }

    public static void InsertDep(string intWid)
    {
        if (HttpRuntime.Cache.Get("key_" + intWid.ToString()) == null)
        {
            HttpRuntime.Cache.Insert("key_" + intWid.ToString(), Guid.NewGuid().ToString());
        }
    }

    public static void UpdateDep(string intWid)
    {
        
        HttpRuntime.Cache.Insert("key_" + intWid.ToString(), Guid.NewGuid().ToString());
         
    }

}