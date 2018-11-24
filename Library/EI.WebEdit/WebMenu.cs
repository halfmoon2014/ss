using MyTy;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
namespace EI.Web
{
    /// <summary>
    /// 菜单页关联类
    /// </summary>
    public class WebMenu
    {

        public WebMenu()
        {
            string ip = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // using proxy
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
            else// not using proxy or can't get the Client IP
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
            Log(ip, "menu");
        }

        public string GetCont(string path)
        {
            string userid = MySession.SessionHandle.Get("userid");
            string tzid = MySession.SessionHandle.Get("tzid");
            string menuPage = MySession.SessionHandle.Get("menupage");
            FM.Business.Login lg = new FM.Business.Login();
            DataRow userDR = lg.GetUser(userid).Tables[0].Rows[0];
            string userName = userDR["name"].ToString();
            string usr= userDR["usr"].ToString();
            string loadingCss = ExtUtil.GetHtml(path, "\\webpage\\loading\\loading");
            if (RequestExtensions.IsMobileBrowser(HttpContext.Current.Request))
                return CreateMobileMenuTree(path,userid, userName, menuPage, tzid, usr) + loadingCss;
            else
            {
                string menu = ExtUtil.GetHtml(path, "\\webpage\\menuExt\\menu");
                /*等待框*/
                FM.Business.Help hp = new FM.Business.Help();
                return string.Format(menu, GetHeadCont(path, userName, "north"), CreateMenuTree(userid), hp.GetWait(), menuPage, tzid, userName, usr) + loadingCss;
            }

        }
        public string CreateMobileMenuTree(string path,string userid,string userName,string menuPage,string tzid,string usr)
        {

            string mobile = ExtUtil.GetHtml(path, "\\webpage\\menuExt\\mobile");
            string mobileList = ExtUtil.GetHtml(path, "\\webpage\\menuExt\\mobileList");
            string mobileDiv = ExtUtil.GetHtml(path, "\\webpage\\menuExt\\mobileDiv");
            string mobileBottom = ExtUtil.GetHtml(path, "\\webpage\\menuExt\\mobileBottom");

            FM.Business.Menu menu = new FM.Business.Menu();
            DataSet ds = menu.GetUserMenu(userid);
            DataTable dtMenu = ds.Tables[0];
            DataTable dtUserMenu = ds.Tables[1];
            DataTable dtTZ = menu.GetTzInfo();

            var queryTopMenu =
                from queryMenu in dtMenu.Select("jb=1").AsEnumerable()
                from queryUserMenu in dtUserMenu.AsEnumerable()
                where queryMenu.Field<int>("ID") == queryUserMenu.Field<int>("menuid")
                orderby queryMenu["xh"]
                select new
                {
                    id = queryMenu.Field<int>("id"),
                    text = queryMenu.Field<string>("text"),
                    mj = queryMenu.Field<int>("mj")
                };
            StringBuilder mobileListS = new StringBuilder();
            foreach (var obj in queryTopMenu)
            {   
                StringBuilder substring = new StringBuilder();
                if (obj.mj == 1)//判断本级是否是末级了
                {
                    //顶级菜单本身就是末级
                }
                else
                {
                    DataTable dtNode = GetMenu(obj.id.ToString(), dtMenu, dtUserMenu);
                    foreach (DataRow drNode in dtNode.Rows)
                        substring.Append("<li><a href=\"#\" url=\"" + drNode["id"].ToString().Trim() + "\" class=\"nav\" >" + drNode["text"].ToString() + "</a></li>");
                }
                mobileListS.Append(string.Format(mobileList, obj.text, substring.ToString()));
            }

            return string.Format(mobileDiv, 
                string.Format(mobile, 
                "当前系统:"+ dtTZ.Rows[0]["tzmc"].ToString()+"-"+userName, 
                mobileListS.ToString()+ GetHeadCont(path, userName, "northMobile"),
                "navbar-fixed-top", 
                "bs-example-navbar-collapse-1",
                menuPage,tzid,userName,usr),
                string.Format(mobileBottom, ""));
        }
        public string CreateMenuTree(string userid)
        {
            StringBuilder tmpStr = new StringBuilder();

            FM.Business.Menu menu = new FM.Business.Menu();
            DataSet ds = menu.GetUserMenu(userid);
            DataTable dtMenu = ds.Tables[0];
            DataTable dtUserMenu = ds.Tables[1];

            var queryTopMenu =
                from queryMenu in dtMenu.Select("jb=1").AsEnumerable()
                from queryUserMenu in dtUserMenu.AsEnumerable()
                where queryMenu.Field<int>("ID") == queryUserMenu.Field<int>("menuid")
                orderby queryMenu["xh"]
                select new
                {
                    id = queryMenu.Field<int>("id"),
                    text = queryMenu.Field<string>("text"),
                    mj = queryMenu.Field<int>("mj")
                };

            foreach (var obj in queryTopMenu)
            {
                tmpStr.Append("<li data-options=\"state: 'closed'\">");
                tmpStr.Append("<span>" + obj.text + "</span>");
                if (obj.mj == 1)//判断本级是否是末级了
                {
                    //顶级菜单本身就是末级
                }
                else
                {
                    DataTable dtNode = GetMenu(obj.id.ToString(), dtMenu, dtUserMenu);
                    tmpStr.Append("<ul >");
                    tmpStr.Append(CreateMenuSubTree(dtNode, dtMenu, dtUserMenu));
                    tmpStr.Append("</ul>");
                }
                tmpStr.Append("</li>");
            }
            return "<ul class=\"easyui-tree\" id=\"lmenu\">" + tmpStr + "</ul>";
        }

        public string CreateMenuSubTree(DataTable dtNode, DataTable dtMenu, DataTable dtUserMenu)
        {
            StringBuilder substring = new StringBuilder();
            foreach (DataRow drNode in dtNode.Rows)
            {
                if (Convert.ToInt32(drNode["xjmj"]) == 1)//如果菜单分组包含菜单项,那么就不要再递归下了.
                    substring.Append("<li><a href=\"#\" url=\"" + drNode["id"].ToString().Trim() + "\" class=\"nav\" >" + drNode["text"].ToString() + "</a></li>");
                else
                {
                    FM.Business.Menu menu = new FM.Business.Menu();
                    DataTable dtNodeNext = GetMenu(drNode["id"].ToString(), dtMenu, dtUserMenu);
                    string next = CreateMenuSubTree(dtNodeNext, dtMenu, dtUserMenu);
                    substring.Append("<li data-options=\"state: 'closed'\"><span>" + drNode["text"].ToString() + "</span>" + (string.IsNullOrEmpty(next) ? "<ul><li><span>开发中</span></li></ul>" : next) + "</li>");
                }
            }
            return substring.ToString();
        }

        public DataTable GetMenu(string menuID, DataTable dtMenu, DataTable dtUserMenu)
        {
            DataTable UserMenu = new DataTable();
            UserMenu.Columns.Add("id", typeof(int));
            UserMenu.Columns.Add("text", typeof(string));
            UserMenu.Columns.Add("mj", typeof(int));
            UserMenu.Columns.Add("xjmj", typeof(int));

            var mjQuery = from t in dtMenu.Select("mj=1").AsEnumerable()
                          group t by new { t1 = t.Field<int>("ssid") } into m
                          select new
                          {
                              ssid = m.Key.t1,
                              mj = m.Max(n => n.Field<int>("mj"))
                          };

            DataTable dtXJMJMenu = new DataTable();
            dtXJMJMenu.Columns.Add("ssid", typeof(int));
            dtXJMJMenu.Columns.Add("mj", typeof(int));
            if (mjQuery.ToList().Count > 0)
            {
                mjQuery.ToList().ForEach(q =>
                {
                    Console.WriteLine(q.ssid + "," + q.mj);
                    dtXJMJMenu.Rows.Add(q.ssid, q.mj);
                });
            }

            var queryTopMenu =
                from queryMenu in dtMenu.Select(" ssid=" + menuID).AsEnumerable()
                join queryUserMenu in dtUserMenu.AsEnumerable() on queryMenu.Field<int>("ID") equals queryUserMenu.Field<int>("menuid")
                join queryXJMJ in dtXJMJMenu.AsEnumerable() on queryMenu.Field<int>("ID") equals queryXJMJ.Field<int?>("ssid") into dtMJ
                from queryXJMJ in dtMJ.DefaultIfEmpty()
                orderby queryMenu["xh"]
                select new
                {
                    id = queryMenu.Field<int>("id"),
                    text = queryMenu.Field<string>("text"),
                    mj = queryMenu.Field<int>("mj"),
                    xjmj = queryXJMJ != null ? queryXJMJ.Field<int?>("mj") : 0
                };

            foreach (var obj in queryTopMenu)
            {
                UserMenu.Rows.Add(obj.id, obj.text, obj.mj, obj.xjmj);
            }

            return UserMenu;

        }
        /// <summary>
        /// 得到菜单北区域
        /// </summary>
        /// <returns></returns>
        public string GetHeadCont(string path, string userName,string fileName)
        {
            FM.Business.Menu menu = new FM.Business.Menu();
            DataTable dt = menu.GetTzInfo();
            string north = ExtUtil.GetHtml(path, "\\webpage\\menuExt\\" + fileName);
            return string.Format(north, dt.Rows[0]["tzmc"].ToString(), userName);
        }
        ///// <summary>
        ///// 根据记录循环得到其下所有MJ=1的菜单
        ///// </summary>
        ///// <param name="dr"></param>
        ///// <returns></returns>

        //public string GetMenuLink(DataRow dr)
        //{
        //    string ML = "";
        //    if (Convert.ToInt32(dr["mj"]) == 1)
        //    {
        //        //ML += "<li><div><a   href=\"#\" url=\"" + dr["cmd"].ToString().Trim() + "\" class=\"nav\" >" + "<span class=\"icon icon-sys\" ></span>" + dr["text"].ToString() + "</a></div></li>";
        //    }
        //    else
        //    {
        //        sjxg.Class1 Sjxg = new sjxg.Class1();
        //        DataTable dtNode = Sjxg.GetMenu(dr["id"].ToString());
        //        foreach (DataRow drNode in dtNode.Rows)
        //        {
        //            ML += GetMenuLink(drNode);
        //        }
        //    }
        //    return ML;
        //}
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="ssid"></param>
        /// <returns></returns>
        public string GetContentMenu()
        {
            FM.Business.Menu mu = new FM.Business.Menu();
            DataTable menuDT = mu.GetContentMenu(HttpContext.Current.Request.QueryString["url"].ToString());
            string helpString = "<span class=\"glyphicon glyphicon-book\" aria-hidden=\"true\"></span>";
            StringBuilder rowString = new StringBuilder();
            if (menuDT.Rows.Count > 0)
            {
                var mjQuery = from t in menuDT.AsEnumerable()
                              group t by new { t1 = t.Field<string>("ls") } into m
                              select new
                              {
                                  ls = m.Key.t1
                              };
                int listCount = mjQuery.ToList().Count;
                string col_md_num = "";
                if (listCount == 1 || listCount == 2 || listCount == 3 || listCount == 4 || listCount == 6)
                {
                    col_md_num = "col-xs-12 col-md-" + (12 / listCount).ToString();
                }
                else if (listCount == 5)
                {
                    col_md_num = "col-xs-12 col-md-2";
                }
                else
                {
                    col_md_num = "col-xs-12 col-md-1";
                }
                mjQuery.ToList().ForEach(q =>
                {
                    string htmlUL = "<div class=\"{1}\"><ul class=\"list-group\">{0}</ul></div>";
                    StringBuilder htmlLS = new StringBuilder();
                    foreach (DataRow dr in menuDT.Select("ls='" + q.ls + "'"))//
                    {
                        htmlLS.Append(string.Format("<li class=\"list-group-item menulist\" alone={2} cmd=\"{3}\" menuID=\"{4}\" >{0} <a  href=\"#\">{1}</a></li>",
                            helpString, dr["text"].ToString().Trim(), dr["alone"].ToString().Trim(), dr["cmd"].ToString().Trim(), dr["id"].ToString().Trim()));
                    }
                    rowString.Append(string.Format(htmlUL, htmlLS, col_md_num));
                });

            }
            return string.Format("<div class=\"row\">{0}</div>", rowString);
        }
        /// <summary>
        /// 记录菜单打开的时间
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="action"></param>
        public void Log(string ip, string action)
        {
            FM.Business.Menu mu = new FM.Business.Menu();
            mu.Log(ip, action);
        }
        /// <summary>
        /// 菜单帮助内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetHelp(string id)
        {
            FM.Business.Menu mu = new FM.Business.Menu();
            return mu.GetHelp(id);
        }

    }
}
