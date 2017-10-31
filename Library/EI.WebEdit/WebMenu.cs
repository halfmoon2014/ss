using System;
using System.Data;
using System.Text;
namespace EI.Web
{
    /// <summary>
    /// 菜单页关联类
    /// </summary>
    public class WebMenu
    {       
    
        public string GetCont(string userid, string tzid, string menuPage)
        {
           
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
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        //public String CreateMenu()
        //{
        //    StringBuilder menuStr = new StringBuilder();
        //    menuStr.Append(" <div data-options=\"region:'west',split:true,title:'目录'\" style=\"width:160px;padding:0px;\">");
        //    StringBuilder tmpStr = new StringBuilder();
        //    tmpStr.Append("<div class=\"easyui-accordion\" id=\"lmenu\" data-options=\"fit:true,border:false\" sytle=\"width:100%\">");
        //    FM.Business.Menu menu = new FM.Business.Menu();
        //    DataTable dt = menu.GetTopLM();
        //    foreach (DataRow dr in dt.Rows)//1
        //    {
        //        tmpStr.Append("<div title=\"" + dr["text"].ToString() + "\" style=\"padding:10px;overflow:auto;\">");
        //        if (Convert.ToInt32(dr["mj"]) == 1)//判断本级是否是末级了
        //        {
        //            tmpStr.Append("</div>");
        //        }
        //        else
        //        {
        //            DataTable dtNode = menu.GetMenu(dr["id"].ToString());
        //            StringBuilder tmp = new StringBuilder();
        //            foreach (DataRow drNode in dtNode.Rows)//2
        //            {
        //                tmp.Append("<li><div><a   href=\"#\" url=\"" + drNode["id"].ToString().Trim() + "\" class=\"nav\" >" + "<span class=\"icon icon-sys\" ></span>" + drNode["text"].ToString() + "</a></div></li>");
        //            }
        //            tmpStr.Append("<ul style='padding-left: 0px;'>" + tmp + "</ul></div>");
        //        }

        //    }
        //    tmpStr.Append("</div>");
        //    menuStr.Append(tmpStr + "</div>");
        //    return menuStr.ToString();
        //}
        public String CreateMenuTree(string userid)
        {
            StringBuilder menuStr = new StringBuilder();
            menuStr.Append(" <div data-options=\"region:'west',split:true,title:'目录'\" style=\"width:160px;padding:0px;\">");
            StringBuilder tmpStr = new StringBuilder();
            FM.Business.Menu menu = new FM.Business.Menu();
            DataTable dt = menu.GetTopLM(userid);
            foreach (DataRow dr in dt.Rows)//1
            {
                tmpStr.Append("<li>");
                tmpStr.Append("<span>" + dr["text"].ToString() + "</span>");
                if (Convert.ToInt32(dr["mj"]) == 1)//判断本级是否是末级了
                {
                    //顶级菜单本身就是末级
                }
                else
                {
                    DataTable dtNode = menu.GetMenu(userid,dr["id"].ToString());
                    tmpStr.Append("<ul >");
                    tmpStr.Append(CreateMenuSubTree(userid,dtNode));
                    tmpStr.Append("</ul>");
                }
                tmpStr.Append("</li>");
            }
            menuStr.Append("<ul class=\"easyui-tree\" id=\"lmenu\">" + tmpStr + "</ul>" + "</div>");
            return menuStr.ToString();
        }

        public String CreateMenuSubTree(string userid,DataTable dtNode)
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
                    DataTable dtNodeNext = menu.GetMenu(userid,drNode["id"].ToString());
                    substring.Append("<ul ><li><span>" + drNode["text"].ToString() + "</span></li>" + CreateMenuSubTree(userid,dtNodeNext) + "</ul>");
                }
            }
            return substring.ToString();
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
        public string GetContentMenu(string ssid)
        {
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
