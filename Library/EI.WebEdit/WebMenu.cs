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
            {
                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
            }
            else// not using proxy or can't get the Client IP
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
            }
            Log(ip, "menu");
        }

        public string GetCont()
        {            
            string userid = MySession.SessionHandle.Get("userid");
            string tzid = MySession.SessionHandle.Get("tzid");
            string menuPage = MySession.SessionHandle.Get("menupage");
            FM.Business.Login lg = new FM.Business.Login();
            string userName = lg.GetUser(userid).Tables[0].Rows[0]["name"].ToString();

            #region 北开始
            StringBuilder north = new StringBuilder();
            north.Append("<div data-options=\"region:'north',border:false\" style=\"height:40px;padding:0px\" class=\"menu3_north\">");
            north.Append(GetHeadCont(userName));
            north.Append("</div>");
            #endregion 北结束

            #region 西开始
            //string west = createMenu();
            string west = CreateMenuTree(userid);
            #endregion 西结束

            #region 南开始
            string south = "<div data-options=\"region:'south',border:false\" style=\"height:30px;padding:0px;\"></div>";
            #endregion 南结束

            #region 中开始
            StringBuilder center = new StringBuilder();
            center.Append("<div id=\"mainPanle\" data-options=\"region:'center',title:''\">");
            center.Append("<div id=\"tabs\" class=\"easyui-tabs\"  fit=\"true\" border=\"false\" >");
            center.Append("<div title=\"主页\" style=\"padding:20px;overflow:hidden;\" id=\"home\">");
            center.Append("<h1>Welcome </h1>");
            center.Append("</div>");
            center.Append("</div>");
            center.Append("</div>");
            #endregion 中结束

            /*等待框*/
            FM.Business.Help hp = new FM.Business.Help();
            string wait = hp.GetWait();
            #region 右键菜单   
            StringBuilder contextMenu = new StringBuilder();
            contextMenu.Append("<div id=\"mm\" class=\"easyui-menu\" style=\"width:150px;\">");
            contextMenu.Append("<div id=\"refresh\">刷新</div>");
            contextMenu.Append("<div class=\"menu-sep\"></div>");
            contextMenu.Append("<div id=\"close\">关闭</div>");
            contextMenu.Append("<div id=\"closeall\">全部关闭</div>");
            contextMenu.Append("<div id=\"closeother\">除此之外全部关闭</div>");
            contextMenu.Append("<div class=\"menu-sep\"></div>");
            contextMenu.Append("<div id=\"closeright\">当前页右侧全部关闭</div>");
            contextMenu.Append("<div id=\"closeleft\">当前页左侧全部关闭</div>");
            contextMenu.Append("<div class=\"menu-sep\"></div>");
            contextMenu.Append("<div id=\"exit\">退出</div>");
            contextMenu.Append("</div>");
            #endregion

            string hidden = "<input type=\"hidden\"  id=\"username\" a=\"" + menuPage + "\" b=\"" + tzid + "\" value=\"" + userName + "\" />";
            return north + west + south + center + wait + contextMenu + hidden;

        }

        public string CreateMenuTree(string userid)
        {
            StringBuilder menuStr = new StringBuilder();
            menuStr.Append(" <div data-options=\"region:'west',split:true,title:'目录'\" style=\"width:160px;padding:0px;\">");
            StringBuilder tmpStr = new StringBuilder();
            FM.Business.Menu menu = new FM.Business.Menu();
            //DataTable dt = menu.GetTopLM(userid);
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
            
            DataTable dtTopMenu = new DataTable();
            dtTopMenu.Columns.Add("id", typeof(int));
            dtTopMenu.Columns.Add("text", typeof(string));
            dtTopMenu.Columns.Add("mj", typeof(int));

            foreach (var obj in queryTopMenu)
            {
                dtTopMenu.Rows.Add(obj.id, obj.text, obj.mj);
            }

            foreach (DataRow dr in dtTopMenu.Rows)
            {
                tmpStr.Append("<li>");
                tmpStr.Append("<span>" + dr["text"].ToString() + "</span>");
                if (Convert.ToInt32(dr["mj"]) == 1)//判断本级是否是末级了
                {
                    //顶级菜单本身就是末级
                }
                else
                {
                    //DataTable dtNode = menu.GetMenu(userid, dr["id"].ToString());
                    DataTable dtNode = GetMenu(dr["id"].ToString(), dtMenu, dtUserMenu);
                    tmpStr.Append("<ul >");
                    tmpStr.Append(CreateMenuSubTree(dtNode, dtMenu, dtUserMenu));
                    tmpStr.Append("</ul>");
                }
                tmpStr.Append("</li>");
            }
            menuStr.Append("<ul class=\"easyui-tree\" id=\"lmenu\">" + tmpStr + "</ul>" + "</div>");
            return menuStr.ToString();
        }

        public string CreateMenuSubTree(DataTable dtNode, DataTable dtMenu, DataTable dtUserMenu)
        {
            StringBuilder substring = new StringBuilder();
            foreach (DataRow drNode in dtNode.Rows)
            {
                if (Convert.ToInt32(drNode["xjmj"]) == 1)//如果菜单分组包含菜单项,那么就不要再递归下了.
                {
                    substring.Append("<li><a   href=\"#\" url=\"" + drNode["id"].ToString().Trim() + "\" class=\"nav\" >" + drNode["text"].ToString() + "</a></li>");
                }
                else
                {
                    FM.Business.Menu menu = new FM.Business.Menu();
                    //DataTable dtNodeNext = menu.GetMenu(userid, drNode["id"].ToString());
                    DataTable dtNodeNext = GetMenu(drNode["id"].ToString(), dtMenu, dtUserMenu);
                    substring.Append("<ul ><li><span>" + drNode["text"].ToString() + "</span></li>" + CreateMenuSubTree(dtNodeNext, dtMenu, dtUserMenu) + "</ul>");
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
        public string GetHeadCont(string userName)
        {
            //sjxg.Class1 sj = new sjxg.Class1();
            FM.Business.Menu menu = new FM.Business.Menu();
            DataTable dt = menu.GetTzInfo();
            string strrq = DateTime.Now.ToString("d");
            FM.Business.Login lg = new FM.Business.Login();
            string m_top_title = "[<a href='../ChooseTz.aspx' class='a_top'  style='text-decoration:none' target='_parent' >切换套账</a>] 当前系统:" + dt.Rows[0]["tzmc"].ToString() + userName + " &nbsp;日期:" + strrq + "&nbsp;&nbsp;[<a href=\"#\" class='a_top'  onclick=\"myChangmm()\" >修改密码</a>]&nbsp;&nbsp;[<a href=\"#\" class='a_top'  onclick=\"window_onunload(0)\" >用户注销</a>]&nbsp;&nbsp;[<a href=\"#\" class='a_top'  onclick=\"window_onunload(-1)\" >退出系统</a>]&nbsp;&nbsp;";

            StringBuilder tmp = new StringBuilder();
            tmp.Append("<table width=\"98%\" height=\"98%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" >");

            tmp.Append("<tr>");
            tmp.Append("<td align=\"left\">");
            tmp.Append("&nbsp;");
            tmp.Append("</td>");
            tmp.Append("<td>");
            tmp.Append("&nbsp;");
            tmp.Append("</td>");
            tmp.Append("<td style=\"width: 520px;  vertical-align: bottom; text-align: right;\" id=\"m_top_title\" runat=\"server\">");
            tmp.Append(m_top_title);
            tmp.Append("</td>");
            tmp.Append("</tr>");
            tmp.Append("</table>");

            return tmp.ToString();
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
            string ssid = HttpContext.Current.Request.QueryString["url"].ToString();
            FM.Business.Menu mu = new FM.Business.Menu();
            DataTable dt = mu.GetContentMenu(ssid);
            int ls = 0;
            string sls = "";
            string outs = ""; string alone = "";
            if (dt.Rows.Count > 0)
            {
                outs = "<table class='home_table' width='100%'><tr>";

                foreach (DataRow dr in dt.Rows)//1
                {
                    string url = "href=\"#\"";
                    string webid = dr["webid"].ToString().Trim();
                    alone = dr["alone"].ToString().Trim();
                    if (dr["cmd"].ToString().Trim() != string.Empty)
                    {
                        url += " onclick=parent.addTab(\"" + dr["text"].ToString().Trim() + "\",\"" + dr["cmd"].ToString().Trim() + "\",this)";
                    }

                    if (sls != dr["ls"].ToString().Trim())
                    {
                        ls += 1;
                        sls = dr["ls"].ToString().Trim();
                        if (ls != 1) { outs += "</table></td>"; }
                        outs += "<td style='vertical-align:top'>";
                        outs += "<table><tr><td><input type='image'  src='../images/menu2_myhelp.png' onclick='parent.myhelp(" + dr["id"].ToString().Trim() + ");return false;' /></td><td><a  alone=" + alone + " style='text-decoration:none'" + url + " >" + dr["text"].ToString().Trim() + "</a></td></tr>";
                        //<a href='#' onclick='myhelp(" + dr["id"].ToString().Trim() + ")' >
                    }
                    else
                    {
                        outs += "<tr><td><input type='image' src='../images/menu2_myhelp.png' onclick='parent.myhelp(" + dr["id"].ToString().Trim() + ");return false;'  /></td><td><a alone=" + alone + " style='text-decoration:none'" + url + " >" + dr["text"].ToString().Trim() + "</a></td></tr>";
                    }

                }
                outs += "</table></td></tr></table>";
            }
            return outs;
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
