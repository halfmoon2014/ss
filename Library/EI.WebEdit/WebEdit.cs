using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using Service.Util;
using EI.Widget;
using DTO;
namespace EI.Web
{
    public class WebEdit
    {
        private string tzid;
        private string userid;
        private string username;
        private bool IsMobileBrowser;
        Business business;
        public WebEdit(string tzid, string userid, string username)
        {
            this.tzid = tzid;
            this.userid = userid;
            this.username = username;
            business = new Business(tzid, userid);
        }

        /// <summary>
        /// 根据intWid得到对应的body内标签
        /// </summary>
        /// <param name="intWid">key</param>
        /// <param name="requestParameter">数据</param>
        /// <returns>LayOut对象</returns>
        public Modal.Html WebLayOut(int intWid, HtmlParameter requestParameter,bool IsMobileBrowser)
        {
            this.IsMobileBrowser = IsMobileBrowser;
            Modal.Html layout = new Modal.Html();
            /*等待框*/
            FM.Business.Help help = new FM.Business.Help();
            //判断当前wid是否包含下级                    
            DataTable frameLayout = business.GetLayout(intWid, "z").Tables[0];
            if (frameLayout.Rows.Count == 0)
            {
                //简单布局                 
                //如果=1,前台加载分页处理
                layout.IsEasyLayout = 1;
                layout.HtmlMark = EasyLayout(intWid, requestParameter) + help.GetWait();
            }
            else
            {
                //组合布局
                layout.IsEasyLayout = 0;
                layout.HtmlMark = MultipleLayout(frameLayout, requestParameter) + help.GetWait();
            }
            return layout;

        }

        /// <summary>
        /// 页面设计
        /// </summary>
        /// <returns></returns>
        public string GetCont()
        {
            StringBuilder west = new StringBuilder();
            StringBuilder ltree = new StringBuilder();
            StringBuilder center = new StringBuilder();
            StringBuilder ctable = new StringBuilder();
            west.Append(" <div data-options=\"region:'west',split:true,title:'人员选择'\" style=\"width:150px;padding:0px;\">");
            // class=\"easyui-tree\" data-options=\"url:'web_xtsz_main.ashx?type=GetTree'\"
            ltree.Append("<ul id=\"leftmenu\"   ></ul> ");//class=\"easyui-tree\" data-options=\"url:web_xtsz_main.aspx/GetTree\"
            center.Append("<div data-options=\"region:'center',title:''\"  style=\"padding:0px;\">");
            
            ctable.Append(" <div  class=\"easyui-layout\" data-options=\"fit:true\" > <div data-options=\"region:'north',split:true\" style=\"height:30px;padding:0px;\">");
            ctable.Append("分类:<select id=\"lx\" >" + GetCTableLx() + " </select>wid:<input type='text' style='width:40px' id='wid' />名称:<input type='text' style='width:80px' id='myname' />js:<input type='text' id='js' />sql:<input type='text' id='sql' /> <input type=\"button\" value=\"查询\" onclick=\"getCx()\" />");
            ctable.Append("</div>");
            ctable.Append("<div data-options=\"region:'center',title:''\"  style=\"padding:0px;\">");
            ctable.Append(" <table id=\"ctable\"></table>");
            ctable.Append("</div>");
            ctable.Append("</div>");

            /*等待框*/
            FM.Business.Help help = new FM.Business.Help();
            string platDialog = "<dialog id=\"platDialog\" style=\"border: 3px;padding:16px;\"><iframe style=\"width: 800px; height: 600px\" id=\"platIframe\" frameborder=\"0\" ></iframe></dialog>";
            return west.ToString() + ltree.ToString() + "</div>" + center.ToString() + ctable.ToString() + "</div>" + help.GetWait()+ platDialog;
        }

        /// <summary>
        /// 页面设计得到右边WEB信息中的分类
        /// </summary>
        /// <returns></returns>
        public string GetCTableLx()
        {
            StringBuilder r = new StringBuilder();
            r.Append("<option value=\"\">全部</option>");
            foreach (DataRow dr in business.GetCTableLx().Tables[0].Rows)
            {
                r.Append("<option value=\"" + dr["lx"].ToString() + "\">" + dr["lx"].ToString() + "</option>");
            }
            return r.ToString();
        }

        /// <summary>
        /// 页面设计edit
        /// </summary>
        /// <returns></returns>
        public string GetContEdit(string title, string path)
        {
            StringBuilder west = new StringBuilder(" <div data-options=\"region:'west',split:true,title:'" + title + "'\" style=\"width:150px;padding:0px;\">");
            StringBuilder myhelp = new StringBuilder("<a href=\"#\" url=\"web_xtsz_main_edit_help.aspx\"   onclick=\"myALink(this)\" >  说明文档</a>");
            StringBuilder sjy = new StringBuilder("<a href=\"#\" url=\"web_xtsz_main_edit_sjy.aspx\"   onclick=\"myALink(this)\" >  数据源</a>");
            StringBuilder zdwh = new StringBuilder("<a href=\"#\" url=\"web_xtsz_main_edit_zdwh.aspx\"     onclick=\"myALink(this)\" >  字段维护</a>");
            StringBuilder js = new StringBuilder("<a href=\"#\" url=\"web_xtsz_main_edit_js.aspx\"     onclick=\"myALink(this)\" >  javascript</a>");

            StringBuilder t1 = new StringBuilder("<a href=\"#\"  url=\"web_xtsz_main_edit_z.aspx?lx=t\"  onclick=\"myALink(this)\" >  上DIV-t</a>");

            StringBuilder b1 = new StringBuilder("<a href=\"#\"  url=\"web_xtsz_main_edit_z.aspx?lx=b\"  onclick=\"myALink(this)\" >  下DIV-b</a>");

            StringBuilder c1 = new StringBuilder("<a href=\"#\"  url=\"web_xtsz_main_edit_z.aspx?lx=c\"  onclick=\"myALink(this)\" >  中DIV-c</a>");

            StringBuilder l1 = new StringBuilder("<a href=\"#\"  url=\"web_xtsz_main_edit_z.aspx?lx=l\"  onclick=\"myALink(this)\" >  左DIV-l</a>");

            StringBuilder r1 = new StringBuilder("<a href=\"#\"  url=\"web_xtsz_main_edit_z.aspx?lx=r\"  onclick=\"myALink(this)\" >  右DIV-r</a>");

            StringBuilder z = new StringBuilder("<a href=\"#\"  url=\"web_xtsz_main_edit_z.aspx?lx=z\"  onclick=\"myALink(this)\" >  布局面板-z</a>");

            StringBuilder ltree = new StringBuilder("<div class=\"easyui-accordion\" id=\"lmenu\" data-options=\"fit:true,border:false\" sytle=\"width:100%\">");
            ltree.Append("<div title=\"设计\" style=\"padding:10px;overflow:auto;\">");
            ltree.Append("<ul style='padding-left: 0px;'><li><div>" + myhelp + "</div></li></ul>");
            ltree.Append("<ul style='padding-left: 0px;'><li><div>" + sjy + "</div></li></ul>");
            ltree.Append("<ul style='padding-left: 0px;'><li><div>" + zdwh + "</div></li></ul>");
            ltree.Append("<ul style='padding-left: 0px;'><li><div>" + js + "</div></li></ul>");
            ltree.Append("<ul style='padding-left: 0px;'><li><div>" + t1 + "</div></li></ul><ul style='padding-left: 0px;'><li><div>" + b1 + "</div></li></ul><ul style='padding-left: 0px;'><li><div>" + l1 + "</div></li></ul><ul style='padding-left: 0px;'><li><div>" + r1 + "</div></li></ul><ul style='padding-left: 0px;'><li><div>" + c1 + "</div></li></ul>");
            ltree.Append("<ul style='padding-left: 0px;'><li><div>" + z + "</div></li></ul>");
            ltree.Append("</div>");

            //ltree.Append("<div title=\"DIV布局\" style=\"padding:10px;overflow:auto;\">");
            //ltree.Append("<ul style='padding-left: 0px;'><li><div>" + t1 + "</div></li><li><div>" + b1 + "</div></li><li><div>" + l1 + "</div></li><li><div>" + r1 + "</div></li><li><div>" + c1 + "</div></li></ul>");
            //ltree.Append("</div>");

            //ltree.Append("<div title=\"布局面板\" style=\"padding:10px;overflow:auto;\">");
            //ltree.Append("<ul style='padding-left: 0px;'><li><div>" + z + "</div></li></ul>");
            //ltree.Append("</div>");

            ltree.Append("</div>");

            //string ltree = "<table><tr><td>" + sjy + "</td></tr><tr><td>" + h1 + "</td></tr><tr><td>" + h2 + "</td></tr><tr><td>" + h3 + "</td></tr><tr><td>" + zdwh + "</td></tr><tr><td>"+b1+"</td></tr><tr><td>"+b2+"</td></tr><tr><td>"+b3+"</td></tr><tr><td>"+l1+"</td></tr><tr><td>"+l2+"</td></tr><tr><td>"+l3+"</td></tr><tr><td>"+r1+"</td></tr><tr><td>"+r2+"</td></tr><tr><td>"+r3+"</td></tr></table> ";
            StringBuilder center = new StringBuilder("<div id=\"mainPanle\" data-options=\"region:'center',title:''\">");
            center.Append("<div id=\"tabs\" class=\"easyui-tabs\"  fit=\"true\" border=\"false\" >");
            center.Append("<div title=\"主页\" style=\"padding:20px;overflow:auto;\" id=\"home\">");

            string strReadFilePath = @path + "\\webinfo.ext";
            StreamReader m_streamReader = new StreamReader(strReadFilePath, Encoding.Default);

            //使用StreamReader类来读取文件  
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            // 从数据流中读取每一行，直到文件的最后一行，并在richTextBox1中显示出内容  
            StringBuilder txt = new StringBuilder();
            string strLine = m_streamReader.ReadLine();
            while (strLine != null)
            {
                txt.Append(strLine + "\n");
                strLine = m_streamReader.ReadLine();
            }
            //关闭此StreamReader对象  
            m_streamReader.Close();
            center.Append(txt);
            /*center += "<h1>帮助说明:</h1>";
            center += "<ul>";

            center += "<li>";
            center += "如果此页面当布局板,那么只需要填写[布局面板]里相应信息 ";
            center += "<ul>";
            center += "<li>";
            center += "l(left)r(right)t(top)b(bottom)c(center) ";
            center += "</li>";
            center += "<li>";
            center += "其中面板按方位填写,如果只有一个,一定需要填写在中间面板上 ";
            center += "</li>";
            center += "<li>";
            center += "排布可以填写t,l,c,r,b 2个字母的随意组合,如:tl,tc,tr,tb,tt;lt,ll,lc,lr,lb.... ";
            center += "</li>";
            center += "</ul>";
            center += "</li>";

            center += "<li>";
            center += "如果此页面当数据成现(单一页面),那么只一定不要填写[布局面板]里相应信息, ";
            center += "<ul>";
            center += "<li>";
            center += "上下左右中,分别填写后会生成对应的元素,如果位置里面设置了长度(上[简-上高]下[简-下高],左[简-左宽],右[简-右宽]),页按布置生成  ";
            center += "</li>";
            center += "<li>";
            center += "上下左右中,每个方位一个DIV,每个DIV中根据[排布]的前一个字母区分不同的TABLE  ";
            center += "</li>";
            center += "</ul>";
            center += "</li>";


            center += "</ul>";
             */
            center.Append("</div>");
            center.Append("</div>");
            /*等待框*/

            FM.Business.Help help = new FM.Business.Help();
            /*右键菜单*/
            StringBuilder contextmenu = new StringBuilder("<div id=\"mm\" class=\"easyui-menu\" style=\"width:150px;\">");
            contextmenu.Append("<div id=\"refresh\">刷新</div>");
            contextmenu.Append("<div class=\"menu-sep\"></div>");
            contextmenu.Append("<div id=\"close\">关闭</div>");
            contextmenu.Append("<div id=\"closeall\">全部关闭</div>");
            contextmenu.Append("<div id=\"closeother\">除此之外全部关闭</div>");
            contextmenu.Append("<div class=\"menu-sep\"></div>");
            contextmenu.Append("<div id=\"closeright\">当前页右侧全部关闭</div>");
            contextmenu.Append("<div id=\"closeleft\">当前页左侧全部关闭</div>");
            contextmenu.Append("<div class=\"menu-sep\"></div>");
            contextmenu.Append("<div id=\"exit\">退出</div>");
            contextmenu.Append("</div>");
            return west.ToString() + ltree.ToString() + "</div>" + center.ToString() + "</div>" + help.GetWait() + contextmenu.ToString();
        }

        /// <summary>
        /// 页面设计得到左边树
        /// </summary>
        /// <returns></returns>
        public string GetTree()
        {

            StringBuilder str = new StringBuilder("[");
            DataSet ds = business.GetLeftTree();
            DataTable dtuser = ds.Tables[0];//人员
            DataTable dtjg = ds.Tables[1];//部门
            DataTable dtno1 = ds.Tables[2];//第一级菜单
            foreach (DataRow dr in dtno1.Rows)
            {
                str.Append(GetNextTree(int.Parse(dr["id"].ToString()), dtjg, dtuser, dr));
            }
            return str.ToString().Substring(0, str.Length - 1) + "]";
        }
        public string GetNextTree(int id, DataTable dtjg, DataTable dturser, DataRow dr)
        {
            string myrs = "{\"id\":" + dr["id"].ToString() + ",\"text\":" + "\"" + dr["dptname"].ToString() + "\",\"iconCls\":\"icon-sum\" ";
            DataRow[] mydr = dtjg.Select("ssid=" + id);
            StringBuilder mychil = new StringBuilder();
            if (mydr.Length > 0)
            {
                foreach (DataRow dr1 in mydr)
                {
                    mychil.Append(GetNextTree(int.Parse(dr1["id"].ToString()), dtjg, dturser, dr1));
                }
            }
            else
            {//如果一级菜单没有下级                 
            }
            //处理人员
            string utmp = GetTreeUser(id, dturser) + mychil.ToString();

            return myrs + (utmp == string.Empty ? "" : ",\"children\":[ " + utmp.Substring(0, utmp.Length - 1) + "] ") + "},";
        }
        public string GetTreeUser(int id, DataTable dturser)
        {
            StringBuilder utmp = new StringBuilder();
            DataRow[] mydr = dturser.Select("departmentid=" + id);
            foreach (DataRow dr1 in mydr)
            {
                utmp.Append("{\"id\":" + dr1["id"].ToString() + ",\"attributes\":{\"type\":\"user\"}, \"text\":" + "\"" + dr1["name"].ToString() + "\"},");
            }
            return utmp.ToString();
        }

        /// <summary>
        /// 页面设计得到右边WEB信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCTable(int id, string lx, string js, string sql, string wid, string myname, int page, int rows)
        {
            DataSet ds = business.GetCTable(id, lx, js, sql, wid, myname, page, rows);
            DataTable dtwid = ds.Tables[0];

            DataTable dtzd = null;// ds.Tables[1];
            //myty.GetPagedTable(dtwid, page, rows),
            return GetTJson(dtwid, dtzd, page, rows);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtsour">全部数量</param>
        /// <param name="dtzd">字段表头</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <returns></returns>
        public string GetTJson(DataTable dtsour, DataTable dtzd, int page, int rows)
        {
            MyTy.Utils us = new MyTy.Utils();
            //实际页显示的内容
            DataTable fytb = us.GetPagedTable(dtsour, page, rows);

            string s = "";
            string[] sarry = new string[fytb.Rows.Count];
            for (int i = 0; i < fytb.Columns.Count; i++)
            {
                foreach (DataRow dr1 in fytb.Rows)
                {
                    string v = "";
                    v = dr1[fytb.Columns[i].Caption.ToString()].ToString();
                    sarry[fytb.Rows.IndexOf(dr1)] += "\"" + fytb.Columns[i].Caption.ToString() + "\":" + "\"" +
                        v + "\",";
                }
            }
            for (int i = 0; i < sarry.Length; i++)
            {
                sarry[i] = "{" + sarry[i].Substring(0, sarry[i].Length - 1) + "},";
                s += sarry[i];
            }
            if (s == string.Empty)
            {
                return "{\"total\":0,\"rows\":[]}";
            }
            else
            {
                return "{\"total\":" + dtsour.Rows.Count + ",  \"rows\":[" + s.Substring(0, s.Length - 1) + "]}";
            }
        }
        /// <summary>
        /// 简单布局,返回HTML字符
        /// </summary>
        /// <param name="intWid"></param>
        /// <param name="requestParameter">请求中的所有参数</param>
        /// <returns></returns>
        public string EasyLayout(int intWid, HtmlParameter requestParameter)
        {
            //上下左右中,五个
            Dictionary<string, string> pz = new Dictionary<string, string> { { "t", "north" }, { "l", "west" }, { "c", "center" }, { "r", "east" }, { "b", "south" } };
            string easyHtmlMark = "";
            #region 取得div上的数据源            

            NameValueCollection queryString = requestParameter.QueryString;
            //获取查询区sql语句            
            DataSet fwSqlDataSet = business.GetWebFwSql(intWid);
            string divSql = "";
            if (fwSqlDataSet.Tables[0].Rows.Count != 0)
            {
                divSql = fwSqlDataSet.Tables[0].Rows[0]["fwsql"].ToString();
            }

            //处理所有平台上的session字段
            //如果url传参就使用url参数值,否则取默认值(默认值替换只允许替换url里的)
            Dictionary<string, string> sessionKey = new Dictionary<string, string>();
            sessionKey.Add("@userid", this.userid);
            sessionKey.Add("@tzid", this.tzid);
            FM.Business.Login lg = new FM.Business.Login();
            sessionKey.Add("@username", this.username);

            System.Data.DataSet divSessionDataSet = business.GetLayout(intWid, "allwz");
            foreach (DataRow dr in divSessionDataSet.Tables[0].Rows)
            {
                //报表配置的session
                string session = dr["session"].ToString();

                if ( !string.IsNullOrEmpty(session))
                {//如果存在session
                    if (!sessionKey.ContainsKey(session))
                    {//session唯一                        
                        if (string.IsNullOrEmpty(queryString[session.Replace("@", "")]))
                        {//如果sql没有包含参数
                            sessionKey.Add(session, GetMrz(dr["mrz"].ToString(), queryString));
                        }
                        else
                        {
                            sessionKey.Add(session, queryString[session.Replace("@", "")]);
                        }

                    }
                }
            }

            foreach (string key in sessionKey.Keys)
            {
                //如果有给值                    
                divSql = divSql.Replace(key, sessionKey[key]);
            }

            DataSet divDataSet = null;
            if (!string.IsNullOrEmpty(divSql))
            {
                FM.Business.Help hp = new FM.Business.Help();
                divDataSet = hp.ExecuteDataset(divSql);
            }
            #endregion

            #region 循环五个方位,得到对应方位上的html标签
            foreach (string key in pz.Keys)
            {
                HtmlContent htmlContent = DivInEasyLayOut(intWid, divSessionDataSet, key, requestParameter, sessionKey, divDataSet);
                string htmlMark = htmlContent.Htmlmark;

                if (!string.IsNullOrEmpty(htmlMark))
                {
                    string tmpCssStyle = "data-options=\"fit:true\"";
                    if (key == "l" || key == "r")
                    {//左可以得到宽度
                        //tmpCssStyle = (divDic["width"] != string.Empty && divDic["width"] != "0" ? "style=\"width:" + divDic["width"] + "px\"" : tmpCssStyle);
                        tmpCssStyle = (htmlContent.Width==0 ? tmpCssStyle:"style=\"width:" + htmlContent.Width.ToString() + "px\"" );
                    }
                    else if (key == "t" || key == "b")
                    {//上,下可以得到高度
                        tmpCssStyle = (htmlContent .Height==0? tmpCssStyle:"style=\"height:" + htmlContent.Height.ToString() + "px\"" );
                    }

                    htmlMark = "<div id=\"" + pz[key] + "\" data-options=\"region:'" + pz[key] + "',split:false,border:false \" " + tmpCssStyle + "  >"
                        + htmlMark + (key == "c" ? GetEditPermission(intWid) : "") + "</div>";
                }
                easyHtmlMark += htmlMark;
            }
            #endregion
            return easyHtmlMark;
        }

        public string _EasyLayout(int intWid, Dictionary<string, NameValueCollection> requestParameter)
        {
            //上下左右中,五个
            Dictionary<string, string> pz = new Dictionary<string, string> { { "t", "north" }, { "l", "west" }, { "c", "center" }, { "r", "east" }, { "b", "south" } };
            DataSet divSessionDataSet = business.GetLayout(intWid, "allwz");
            Dictionary<string, ClientDiv> clientDivDic = new Dictionary<string, ClientDiv>();

            #region 五个方位的布局
            foreach (string key in pz.Keys)
            {
                clientDivDic.Add(key, _DivInEasyLayOut(intWid, divSessionDataSet, key));
            }
            #endregion

            
            #region 取得div上的数据源            


            //获取查询区sql语句,这里面可能会用到替换变量
            DataSet fwSqlDataSet = business.GetWebFwSql(intWid);
            string divSql = "";
            if (fwSqlDataSet.Tables[0].Rows.Count != 0)
            {
                divSql = fwSqlDataSet.Tables[0].Rows[0]["fwsql"].ToString();
            }

            //处理所有平台上的session字段
            //如果url传参就使用url参数值,否则取默认值(默认值替换只允许替换url里的)            
            Dictionary<string, string> sessionKey = new Dictionary<string, string>();
            NameValueCollection queryString = requestParameter["QueryString"];

            foreach (DataRow dr in divSessionDataSet.Tables[0].Rows)
            {
                if (dr["session"].ToString() != string.Empty)
                {
                    //如果存在session
                    if (!sessionKey.ContainsKey(dr["session"].ToString()))
                    {
                        //session唯一                        
                        if (queryString[dr["session"].ToString().Replace("@", "")] == null)
                        {
                            //如果sql没有包含参数
                            sessionKey.Add(dr["session"].ToString(), GetMrz(dr["mrz"].ToString(), queryString));
                        }
                        else
                        {
                            sessionKey.Add(dr["session"].ToString(), queryString[dr["session"].ToString().Replace("@", "")]);
                        }

                    }
                }
            }
            sessionKey.Add("@userid", this.userid);
            sessionKey.Add("@tzid", this.tzid);
            FM.Business.Login lg = new FM.Business.Login();
            sessionKey.Add("@username", this.username);
            foreach (string key in sessionKey.Keys)
            {
                //如果有给值                    
                divSql = divSql.Replace(key, sessionKey[key]);
            }

            DataSet divDataSet = null;
            if (!string.IsNullOrEmpty(divSql))
            {
                FM.Business.Help hp = new FM.Business.Help();
                divDataSet = hp.ExecuteDataset(divSql);
            }
            #endregion

            #region 将布局和数据融合
            foreach (string key in clientDivDic.Keys)
            {
                ClientDiv clientDiv = clientDivDic[key];

            }
            #endregion

            string easyHtmlMark = "";

            //#region 循环五个方位,得到对应方位上的html标签
            //foreach (string key in pz.Keys)
            //{
            //    Dictionary<string, string> divDic = DivInEasyLayOut(intWid, divSessionDataSet, key, requestParameter);

            //    string htmlMark = divDic["htmlmark"];

            //    if (htmlMark != string.Empty)
            //    {
            //        string tmpCssStyle = "data-options=\"fit:true\"";
            //        if (key == "l" || key == "r")
            //        {//左可以得到宽度
            //            tmpCssStyle = (divDic["width"] != string.Empty && divDic["width"] != "0" ? "style=\"width:" + divDic["width"] + "px\"" : tmpCssStyle);
            //        }
            //        else if (key == "t" || key == "b")
            //        {//上,下可以得到高度
            //            tmpCssStyle = (divDic["height"] != string.Empty && divDic["height"] != "0" ? "style=\"height:" + divDic["height"] + "px\"" : tmpCssStyle);
            //        }

            //        htmlMark = "<div id=\"" + pz[key] + "\" data-options=\"region:'" + pz[key] + "',split:false,border:false \" " + tmpCssStyle + "  >"
            //            + htmlMark + (key == "c" ? GetEditPermission(intWid) : "") + "</div>";
            //    }
            //    easyHtmlMark += htmlMark;
            //}
            //#endregion
            return easyHtmlMark;
        }

        /// <summary>
        /// 获取简单布局div
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="divSessionDataSet"></param>
        /// <param name="type"></param>
        /// <param name="requestParameter"></param>
        /// <param name="sessionKey"></param>
        /// <param name="divDataSet"></param>
        /// <returns></returns>
        public HtmlContent DivInEasyLayOut(int wid, DataSet divSessionDataSet, string type, HtmlParameter requestParameter, Dictionary<string, string> sessionKey, DataSet divDataSet)
        {            
            HtmlContent htmlContent = new HtmlContent();
            //得到的这个数据源一定要排序好!后面会使用到排序算法
            //排序按lx,ord

            //按ord 排序datatable            
            DataRow[] rows = divSessionDataSet.Tables[0].Select("lx='" + type + "'", "ord");
            DataTable divInLayoutDataTable = divSessionDataSet.Tables[0].Clone();
            divInLayoutDataTable.Clear();

            foreach (DataRow row in rows)
            {
                divInLayoutDataTable.ImportRow(row);
            }

            if (divInLayoutDataTable.Rows.Count != 0)
            {
                //方位上一定要有数据,不然就是空!
                #region 获取方位上面的高or宽
                if (string.Compare(type, "l") == 0)
                {
                    htmlContent.Width = int.Parse(divInLayoutDataTable.Rows[0]["westwidth"].ToString().Trim());
                }
                else if (string.Compare(type, "r") == 0)
                {
                    htmlContent.Width = int.Parse(divInLayoutDataTable.Rows[0]["eastwidth"].ToString().Trim());
                }
                else if (string.Compare(type, "t") == 0)
                {
                    htmlContent.Height = int.Parse(divInLayoutDataTable.Rows[0]["northheight"].ToString().Trim());
                }
                else if (string.Compare(type, "b") == 0)
                {
                    htmlContent.Height = int.Parse(divInLayoutDataTable.Rows[0]["southheight"].ToString().Trim());
                }
                #endregion

                #region 简单布局中
                if (divInLayoutDataTable.Rows[0]["nwebid"].ToString() != "0" || divInLayoutDataTable.Rows[0]["naspx"].ToString() != string.Empty)
                {
                    #region 如果方位上是一个页面或者是个wid, 并且一定是第一行
                    string url = "";
                    if (divInLayoutDataTable.Rows[0]["naspx"].ToString() != string.Empty)
                    {
                        url = divInLayoutDataTable.Rows[0]["naspx"].ToString().Trim();
                    }
                    else
                    {
                        url = "lss.aspx?wid=" + int.Parse(divInLayoutDataTable.Rows[0]["nwebid"].ToString().Trim());
                    }
                    string ifid = divInLayoutDataTable.Rows[0]["htmlid"].ToString().Trim();
                    if (url.IndexOf("?") < 0)
                    {
                        url = url + "?";
                    }
                    foreach (string key in requestParameter.QueryString.Keys)
                    {
                        //如果下级是wid,那么wid这个参数就要修改
                        if (divInLayoutDataTable.Rows[0]["naspx"].ToString() != string.Empty || (int.Parse(divInLayoutDataTable.Rows[0]["nwebid"].ToString().Trim()) != 0 && key.ToLower() != "wid"))
                        {
                            url += "&" + key + "=" + requestParameter.QueryString[key] + "&";
                        }
                    }
                    url = url.Substring(0, url.Length - 1);
                    htmlContent.Htmlmark = "<iframe id=\"" + ifid + "\" scrolling=\"auto\" frameborder=\"0\"  src=\"" + url + "\"  style=\"width:100%;height:100%;\" " + "></iframe>";
                    #endregion

                }
                else if (divInLayoutDataTable.Rows[0]["type"].ToString() == "tree")
                {
                    #region 处理树
                    string htmlid = divInLayoutDataTable.Rows[0]["htmlid"].ToString();
                    string width = "";
                    string bz = divInLayoutDataTable.Rows[0]["bz"].ToString().Trim();
                    string visible = "";
                    if (divInLayoutDataTable.Rows[0]["visible"].ToString().Trim() != "1")
                    {
                        visible = " display: none; ";
                    }
                    else
                    {
                        width = (divInLayoutDataTable.Rows[0]["width"].ToString().Trim() == string.Empty ? " " : " width:" + divInLayoutDataTable.Rows[0]["width"].ToString().Trim() + "px; ");
                    }
                    htmlContent.Htmlmark = "<div style=\"" + visible + width + "\"><a  href=\"#\" style=\"text-decoration: none;\" onclick=\"reloadTree('" + htmlid + "')\">[刷新]</a>&nbsp;<a  href=\"#\" style=\"text-decoration: none;\" onclick=\"collapseAllTree('" + htmlid + "')\">[折叠]</a>&nbsp;<a  href=\"#\" style=\"text-decoration: none;\" onclick=\"expandAllTree('" + htmlid + "')\">[展开]</a></div>" +
                        "<div style=\"" + visible + width + "\"><ul id=\"" + htmlid + "\"></ul></div>";
                    htmlContent.Htmlmark += "<script type=\"text/javascript\" src=\"../javascripts/myjs/myeasyuitree.js\"></script>";
                    htmlContent.Htmlmark += "<script language=\"javascript\" type=\"text/javascript\"> $(function(){loadTree('" + htmlid + "',\"" + bz + "\");});</script>";
                    if (type == "l" && divInLayoutDataTable.Rows[0]["westwidth"].ToString().Trim() != "0") { htmlContent.Width = int.Parse(divInLayoutDataTable.Rows[0]["westwidth"].ToString().Trim()); }//取左边那个的宽度
                    if (type == "l" && divInLayoutDataTable.Rows[0]["eastwidth"].ToString().Trim() != "0") { htmlContent.Width = int.Parse(divInLayoutDataTable.Rows[0]["eastwidth"].ToString().Trim()); }//取左边那个的宽度
                    if (type == "t" && divInLayoutDataTable.Rows[0]["northheight"].ToString().Trim() != "0") { htmlContent.Height = int.Parse(divInLayoutDataTable.Rows[0]["northheight"].ToString().Trim()); }//取上边那个的高度
                    if (type == "b" && divInLayoutDataTable.Rows[0]["southheight"].ToString().Trim() != "0") { htmlContent.Height = int.Parse(divInLayoutDataTable.Rows[0]["southheight"].ToString().Trim()); }//取下边那个的高度
                    #endregion
                }
                else
                {
                    #region

                    if (divInLayoutDataTable.Rows.Count > 0)
                    {
                        htmlContent.Htmlmark += CreatDiv(divInLayoutDataTable,  sessionKey, divDataSet);
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                if (string.Compare(type, "c") == 0)
                {
                    //如果中间的是表格,而不是布局的话初始占位div
                    //否则就是单记录形式
                    htmlContent.Htmlmark = GetCenterHtml();
                }
            }
            return htmlContent;
        }


        public ClientDiv _DivInEasyLayOut(int wid, DataSet divSessionDataSet, string type)
        {
            ClientDiv clientDiv = new ClientDiv();
            //得到的这个数据源一定要排序好!后面会使用到排序算法
            //排序按lx,ord

            //按ord 排序datatable            
            DataRow[] rows = divSessionDataSet.Tables[0].Select("lx='" + type + "'", "ord");
            DataTable divInLayoutDataTable = divSessionDataSet.Tables[0].Clone();
            divInLayoutDataTable.Clear();

            foreach (DataRow row in rows)
            {
                divInLayoutDataTable.ImportRow(row);
            }


            if (divInLayoutDataTable.Rows.Count != 0)
            {
                //方位上一定要有数据,不然就是空!
                #region 获取方位上面的高or宽
                if (string.Compare(type, "l") == 0)
                {
                    clientDiv.Width = int.Parse(divInLayoutDataTable.Rows[0]["westwidth"].ToString().Trim());
                }
                else if (string.Compare(type, "r") == 0)
                {
                    clientDiv.Width = int.Parse(divInLayoutDataTable.Rows[0]["eastwidth"].ToString().Trim());
                }
                else if (string.Compare(type, "t") == 0)
                {
                    clientDiv.Height = int.Parse(divInLayoutDataTable.Rows[0]["northheight"].ToString().Trim());
                }
                else if (string.Compare(type, "b") == 0)
                {
                    clientDiv.Height = int.Parse(divInLayoutDataTable.Rows[0]["southheight"].ToString().Trim());
                }
                #endregion

                #region 简单布局中
                if (divInLayoutDataTable.Rows[0]["nwebid"].ToString() != "0" || divInLayoutDataTable.Rows[0]["naspx"].ToString() != string.Empty)
                {
                    #region 如果方位上是一个页面或者是个wid, 并且一定是第一行
                    string url = "";
                    if (divInLayoutDataTable.Rows[0]["naspx"].ToString() != string.Empty)
                    {
                        url = divInLayoutDataTable.Rows[0]["naspx"].ToString().Trim();
                    }
                    else
                    {
                        url = "lss.aspx?wid=" + int.Parse(divInLayoutDataTable.Rows[0]["nwebid"].ToString().Trim());
                    }

                    ClientPage clientPage = new ClientPage();
                    clientDiv.DivType = DivType.page;
                    clientPage.Id = divInLayoutDataTable.Rows[0]["htmlid"].ToString().Trim();
                    clientPage.Url = url;
                    clientDiv.ClientData = clientPage;
                    #endregion

                }
                else if (divInLayoutDataTable.Rows[0]["type"].ToString() == "tree")
                {
                    #region 处理树
                    ClientTree clientTree = new ClientTree();
                    clientTree.Id = divInLayoutDataTable.Rows[0]["htmlid"].ToString();
                    clientTree.Data = divInLayoutDataTable.Rows[0]["bz"].ToString().Trim();

                    if (divInLayoutDataTable.Rows[0]["visible"].ToString().Trim() != "1")
                    {
                        clientTree.Visible = true;
                    }
                    else
                    {
                        clientTree.Visible = false;
                        clientTree.Width = int.Parse(divInLayoutDataTable.Rows[0]["width"].ToString().Trim());
                    }

                    clientDiv.ClientData = clientTree;
                    clientDiv.DivType = DivType.tree;
                    #endregion
                }
                else
                {
                    clientDiv.ClientData = _CreatDiv(divInLayoutDataTable);
                    clientDiv.DivType = DivType.table;
                }

                #endregion
            }
            else
            {
                if (string.Compare(type, "c") == 0)
                {
                    //如果中间的是表格,而不是布局的话初始占位div
                    //否则就是单记录形式                    
                    clientDiv.DivType = DivType.content;
                }
            }
            return clientDiv;
        }
        /// <summary>
        /// //创建DIV
        /// </summary>
        /// <param name="lxTag"></param>
        /// <param name="divInLayoutDataTable"></param>
        /// <param name="queryString"></param>
        /// <param name="divDataSet"></param>
        /// <param name="divWidth"></param>
        /// <param name="divHeight"></param>
        /// <returns></returns>
        public string CreatDiv(DataTable divInLayoutDataTable,  Dictionary<string, string> sessionKey, DataSet divDataSet)
        {

            string tablebTag = "";
            StringBuilder tableHtml = new StringBuilder();
            foreach (DataRow dr in divInLayoutDataTable.Rows)
            {
                if (tablebTag != dr["ord"].ToString().Trim().Substring(0, 1))
                {
                    tablebTag = dr["ord"].ToString().Trim().Substring(0, 1);
                    tableHtml.Append(CreateTable(tablebTag, divInLayoutDataTable,  sessionKey, divDataSet));
                }

            }
            return "<div class=\"MyDivBlockClass\">" + tableHtml.ToString() + "</div>";
        }
        public List<ClientTable> _CreatDiv(DataTable divInLayoutDataTable)
        {
            string tablebTag = "";
            List<ClientTable> clientTableList = new List<ClientTable>();
            foreach (System.Data.DataRow dr in divInLayoutDataTable.Rows)
            {
                if (tablebTag != dr["ord"].ToString().Trim().Substring(0, 1))
                {
                    tablebTag = dr["ord"].ToString().Trim().Substring(0, 1);
                    clientTableList.Add(_CreateTable(tablebTag, divInLayoutDataTable));
                }

            }
            return clientTableList;
        }
        /// <summary>
        /// 创建TABLE
        /// </summary>
        /// <param name="tablebTag"></param>
        /// <param name="lxTag"></param>
        /// <param name="divInLayoutDataTable"></param>
        /// <param name="queryString"></param>
        /// <param name="divDataSet"></param>
        /// <param name="divWidth"></param>
        /// <param name="divHeight"></param>
        /// <returns></returns>
        public string CreateTable(string tablebTag, DataTable divInLayoutDataTable,  Dictionary<string, string> sessionKey, DataSet divDataSet)
        {
            string mrz; string mrzSql;
            List<string> tdList = new List<string>();

            foreach (System.Data.DataRow dr in divInLayoutDataTable.Select("substring(ord,1,1)='" + tablebTag + "'"))
            {

                #region 默认值
                mrzSql = dr["mrz"].ToString();
                if (sessionKey.Keys.Contains(dr["session"].ToString()))
                {
                    mrz = sessionKey[dr["session"].ToString()];
                }
                else if (sessionKey.Keys.Contains(dr["mrz"].ToString()))
                {
                    mrz = sessionKey[dr["mrz"].ToString()];
                }
                else
                {
                    if (mrzSql.ToUpper().IndexOf("SELECT") >= 0)
                    {
                        foreach (string key in sessionKey.Keys)
                        {//用url参数替换默认值里的替换变量
                            mrzSql.Replace("@" + key, sessionKey[key]);
                        }
                        mrz = business.execSqlCommand(mrzSql, "off", new Dictionary<string, string> { { "wid", "-1" }, { "callFucntion", "CreateTable" } })["resultText"];
                    }
                    else
                    {
                        mrz = mrzSql;
                    }
                }
                #endregion
                #region 控件值
                string defaultValue = "";
                if (dr["zb"].ToString().Trim() == "1")
                {//主表字段
                    if (divDataSet == null)
                    {
                        defaultValue = mrz;
                    }
                    else if (divDataSet.Tables[0].Rows.Count == 0)
                    {
                        defaultValue = mrz;
                    }
                    else
                    {
                        defaultValue = divDataSet.Tables[0].Rows[0][dr["htmlid"].ToString().Trim()].ToString().Trim();
                    }
                }
                else
                {
                    defaultValue = mrz;
                }
                #endregion
                if (dr["type"].ToString().Trim() == "text")
                {
                    #region
                    string htmlid = (dr["htmlid"].ToString().Trim() == string.Empty ? "" : " id=\"" + dr["htmlid"].ToString().Trim() + "\" ");
                    string visible = "";
                    string width = "";string inputWidth = "";
                    string qwidth = "";

                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        width = (dr["width"].ToString().Trim() == string.Empty ? "" : " width:" + dr["width"].ToString().Trim() + "px; ");
                        //4px是控件的border
                        inputWidth = (dr["width"].ToString().Trim() == string.Empty ? "" : " width:" + (int.Parse(dr["width"].ToString().Trim())-4) + "px; ");
                        qwidth = (dr["qwidth"].ToString().Trim() == string.Empty ? "" : " width:" + dr["qwidth"].ToString().Trim() + "px; ");
                    }
                    else
                    {
                        visible = " display: none; ";
                    }

                    string sreadonly = (dr["readonly"].ToString().Trim() == "1" ? " readonly=\"readonly\" " : "");
                    string sevent = dr["event"].ToString().Trim();
                    string yy = "";//引用+表达示
                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {
                        string bds = dr["bds"].ToString().Trim();
                        yy = "yy=\"" + dr["yy"].ToString().Trim() + "\" bds=\"" + bds + "\"";
                    }
                    string tdHtml = "<td style=\"" + qwidth + visible + "text-align:right;" + dr["css0"].ToString().Trim() + " \" ><label >" + dr["mc"].ToString().Trim() + "</label></td>";
                    tdHtml += "<td style=\"" + visible+ width + "\" innerctrl=\"text\" ><input style=\""+ inputWidth+ dr["css"].ToString().Trim() + "\" " + yy + " type=\"text\"" + htmlid + " " + sevent + sreadonly + " value=\"" + defaultValue + "\" />";
                    tdHtml += "</td>";
                    tdList.Add(tdHtml);
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "select")
                {
                    #region
                    string htmlid = (dr["htmlid"].ToString().Trim() == string.Empty ? "" : " id=\"" + dr["htmlid"].ToString().Trim() + "\" ");

                    string visible = "";
                    string width = "";
                    string qwidth = "";

                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        width = (dr["width"].ToString().Trim() == string.Empty ? "" : " width:" + dr["width"].ToString().Trim() + "px; ");
                        qwidth = (dr["qwidth"].ToString().Trim() == string.Empty ? "" : " width:" + dr["qwidth"].ToString().Trim() + "px; ");
                    }
                    else
                    {
                        visible = " display: none; ";
                    }

                    string sreadonly = (dr["readonly"].ToString().Trim() == "1" ? " readonly=\"readonly\" " : "");
                    string sevent = dr["event"].ToString().Trim();
                    StringBuilder OPTION = new StringBuilder();
                    Dictionary<string, string> option = GetDivSelectOption(dr["bz"].ToString().Trim(), sessionKey);
                    foreach (string key in option.Keys)
                    {
                        OPTION.Append("<OPTION value='" + key + "' " + (defaultValue == key ? "selected='true'" : "") + ">" + option[key] + "</OPTION>");
                    }
                    string yy = "";//引用+表达示
                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {
                        string bds = dr["bds"].ToString().Trim();
                        yy = "yy=\"" + dr["yy"].ToString().Trim() + "\" bds=\"" + bds + "\"";
                    }
                    string tdHtml = "<td style=\"" + qwidth + visible + "text-align:right;" + dr["css0"].ToString().Trim() + " \"   ><label >" + dr["mc"].ToString().Trim() + "</label></td>";
                    tdHtml += "<td style=\"" + visible + "\" innerctrl=\"select\" ><select   style=\"" + width + ";" + dr["css"].ToString().Trim() + "\" " + yy + htmlid + " " + sevent + sreadonly + "  >" + OPTION + "</select>";
                    tdHtml += "</td>";
                    tdList.Add(tdHtml);
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "button")
                {
                    #region
                    string sevent = dr["event"].ToString().Trim();
                    string visible = "";
                    string width = "";                  

                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        width = (dr["width"].ToString().Trim() == string.Empty || dr["width"].ToString().Trim() == "0" ? "" : " width:" + dr["width"].ToString().Trim() + "px; ");                       
                    }
                    else
                    {
                        visible = " display: none; ";
                    }
                    string htmlid = (dr["htmlid"].ToString().Trim() == string.Empty ? "" : " id=\"" + dr["htmlid"].ToString().Trim() + "\" ");

                    string sreadonly = (dr["readonly"].ToString().Trim() == "1" ? " disabled=\"disabled\" " : "");
                    string tdHtml = "<td style=\"" + visible + "\" innerctrl=\"button\" ><input type=\"button\"  style=\"" + width + ";" + dr["css"].ToString().Trim() + "\"" + htmlid + sreadonly + " value=\"" + dr["mc"].ToString().Trim() + "\" " + sevent + " />";
                    tdHtml += "</td>";
                    tdList.Add(tdHtml);
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "checkbox")
                {
                    #region
                    string sevent = dr["event"].ToString().Trim();
                    string htmlid = (dr["htmlid"].ToString().Trim() == string.Empty ? " " : " id=\"" + dr["htmlid"].ToString().Trim() + "\" ");

                    string visible = "";
                    string width = "";
                    string qwidth = "";
                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        width = (dr["width"].ToString().Trim() == string.Empty ? " " : " width:" + dr["width"].ToString().Trim() + "px; ");
                        qwidth = (dr["qwidth"].ToString().Trim() == string.Empty ? " " : " width:" + dr["qwidth"].ToString().Trim() + "px; ");
                    }
                    else
                    {
                        visible = " display: none; ";
                    }

                    string sreadonly = (dr["readonly"].ToString().Trim() == "1" ? " disabled=\"disabled\" " : " ");
                    if (defaultValue == "1") { defaultValue = "checked"; } else { defaultValue = " "; }
                    string yy = "";//引用+表达示
                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {
                        string bds = dr["bds"].ToString().Trim();
                        yy = " yy=\"" + dr["yy"].ToString().Trim() + "\" bds=\"" + bds + "\" ";
                    }
                    string tdHtml = "<td style=\"" + qwidth + visible + "text-align:right;" + dr["css0"].ToString().Trim() + " \"  ><label >" + dr["mc"].ToString().Trim() + "</label></td>";
                    tdHtml += "<td style=\"" + visible + "\" innerctrl=\"checkbox\" ><input style=\"" + width + ";" + dr["css"].ToString().Trim() + "\"  " + yy + " type=\"checkbox\" " + htmlid + sevent + sreadonly + defaultValue + " />";
                    tdHtml += "</td>";
                    tdList.Add(tdHtml);
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "textarea")
                {
                    #region
                    string htmlid = (dr["htmlid"].ToString().Trim() == string.Empty ? " " : " id=\"" + dr["htmlid"].ToString().Trim() + "\" ");

                    string visible = "";
                    string width = "";
                    string qwidth = "";
                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        width = (dr["width"].ToString().Trim() == string.Empty ? " " : " width:" + dr["width"].ToString().Trim() + "px; ");
                        qwidth = (dr["qwidth"].ToString().Trim() == string.Empty ? " " : " width:" + dr["qwidth"].ToString().Trim() + "px; ");
                    }
                    else
                    {
                        visible = " display: none; ";
                    }
                    string sevent = dr["event"].ToString().Trim();
                    string sreadonly = (dr["readonly"].ToString().Trim() == "1" ? " readonly=\"readonly\" " : " ");

                    string yy = "";//引用+表达示
                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {
                        string bds = dr["bds"].ToString().Trim();
                        yy = "yy=\"" + dr["yy"].ToString().Trim() + "\" bds=\"" + bds + "\"";
                    }
                    string tdHtml = "<td style=\"" + qwidth + visible + "text-align:right;" + dr["css0"].ToString().Trim() + " \"   ><label >" + dr["mc"].ToString().Trim() + "</label></td>";
                    tdHtml += "<td style=\"" + visible + "\" innerctrl=\"textarea\" >" + "<textarea " + yy + " style=\"" + width + ";" + dr["css"].ToString().Trim() + "\" " + htmlid + sreadonly + sevent + ">" + defaultValue + "</textarea>";
                    tdHtml += "</td>";
                    tdList.Add(tdHtml);
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "td")
                {
                    #region
                    string qwidth = (dr["qwidth"].ToString().Trim() == string.Empty ? "" : " style=\"width:" + dr["qwidth"].ToString().Trim() + "px;text-align:right;" + dr["css0"].ToString().Trim() + "\" ");
                    string tdHtml = "<td " + qwidth + " >" + dr["mc"].ToString().Trim();
                    tdHtml += "</td>";
                    tdList.Add(tdHtml);
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "a")
                {
                    #region
                    string visible = "";
                    string width = "";
                    string qwidth = "";
                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        width = (dr["width"].ToString().Trim() == string.Empty ? "" : " width:" + dr["width"].ToString().Trim() + "px; ");
                        qwidth = (dr["qwidth"].ToString().Trim() == string.Empty ? "" : " width:" + dr["qwidth"].ToString().Trim() + "px; ");
                    }
                    else
                    {
                        visible = " display: none; ";
                    }
                    string sevent = dr["event"].ToString().Trim();
                    string sreadonly = (dr["readonly"].ToString().Trim() == "1" ? " disabled=\"disabled\" " : "");
                    string tdHtml = "<td style=\"" + qwidth + visible + "text-align:right;" + dr["css0"].ToString().Trim() + " \"   ><label >" + dr["mc"].ToString().Trim() + "</label></td>";
                    tdHtml += "<td style=\"" + visible + "\" innerctrl=\"a\" >" + "<a href=\"#\" " + sevent + sreadonly + " style=\"" + width + ";" + dr["css"].ToString().Trim() + "\" >" + dr["mc"].ToString().Trim() + "</a>";
                    tdHtml += "</td>";
                    tdList.Add(tdHtml);
                    #endregion
                }

                else if (dr["type"].ToString().Trim() == "date")
                {
                    #region
                    string htmlid = (dr["htmlid"].ToString().Trim() == string.Empty ? "" : " id=\"" + dr["htmlid"].ToString().Trim() + "\" ");
                    string visible = "";
                    string width = "";
                    string qwidth = "";

                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        width = (dr["width"].ToString().Trim() == string.Empty ? "" : " width:" + dr["width"].ToString().Trim() + "px; ");
                        qwidth = (dr["qwidth"].ToString().Trim() == string.Empty ? "" : " width:" + dr["qwidth"].ToString().Trim() + "px; ");
                    }
                    else
                    {
                        visible = " display: none; ";
                    }

                    string sreadonly = (dr["readonly"].ToString().Trim() == "1" ? " readonly=\"readonly\" " : "");
                    string sevent = dr["event"].ToString().Trim();
                    string yy = "";//引用+表达示
                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {
                        string bds = dr["bds"].ToString().Trim();
                        yy = "yy=\"" + dr["yy"].ToString().Trim() + "\" bds=\"" + bds + "\"";
                    }

                    string tdHtml = "<td style=\"" + qwidth + visible + "text-align:right;" + dr["css0"].ToString().Trim() + " \"  ><label >" + dr["mc"].ToString().Trim() + "</label></td>";
                    tdHtml += "<td style=\"" + visible + "\" innerctrl=\"date\" ><input class=\"easyui-datebox\" " + htmlid + "  data-options=\"formatter:formatDate\" value=\"" + defaultValue + "\"  " + yy + " style=\"" + width + ";" + dr["css"].ToString().Trim() + "\" />  ";
                    //h += "<td style=\"" + visible + "\" ><input style=\"" + width + "\" " + yy + " type=\"text\"" + htmlid + " " + sevent + sreadonly + " value=\"" + v + "\" />";
                    tdHtml += "</td>";
                    tdList.Add(tdHtml);
                    #endregion
                }

            }
            if (IsMobileBrowser)
            {
                StringBuilder sb = new StringBuilder();
                foreach(string k in tdList)
                {
                    sb.Append(string.Format("<table style=\"table-layout:fixed\" parameters='upload' class=\"c_head_tb\"><tr>{0}</tr></table>", k));
                }
                return sb.ToString();
            }
            else
            {
                return string.Format("<table style=\"table-layout:fixed\" parameters='upload' class=\"c_head_tb\"><tr>{0}<td>&nbsp;</td></tr></table>", string.Join("", tdList.ToArray()));
            }
        }

        public ClientTable _CreateTable(string tablebTag, DataTable divInLayoutDataTable)
        {
            ClientTable clientTable = new ClientTable();
            foreach (System.Data.DataRow dr in divInLayoutDataTable.Select("substring(ord,1,1)='" + tablebTag + "'"))
            {
                ClientWidget clientWidget = new ClientWidget();
                ClientWidgetDefaultValue clientWidgetDefaultValue = new ClientWidgetDefaultValue();
                clientWidget.ClientWidgetDefaultValue = clientWidgetDefaultValue;
                clientWidgetDefaultValue.Session = dr["session"].ToString();
                clientWidgetDefaultValue.Mrz = dr["mrz"].ToString();
                clientWidgetDefaultValue.Iszb = int.Parse(dr["zb"].ToString().Trim());

                if (dr["type"].ToString().Trim() == "text")
                {
                    #region 文本
                    clientWidget.ClientControlType = ClientControlType.text;
                    ClientLable clientLable = new ClientLable();
                    clientLable.Css = dr["css0"].ToString().Trim();
                    clientLable.Value = dr["mc"].ToString().Trim();
                    ClientText clientText = new ClientText();
                    clientText.Lable = clientLable;
                    clientText.Css = dr["css"].ToString().Trim();
                    clientText.Event = dr["event"].ToString().Trim();
                    clientText.Readonly = (dr["readonly"].ToString().Trim() == "1" ? true : false);
                    //clientText.Value = defaultValue;
                    clientText.Id = dr["htmlid"].ToString().Trim();

                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        clientLable.Width = int.Parse(dr["qwidth"].ToString().Trim());
                        clientLable.Visible = true;
                        clientText.Visible = true;
                        clientText.Width = int.Parse(dr["width"].ToString().Trim());
                    }
                    else
                    {
                        clientLable.Visible = false;
                        clientText.Visible = false;
                    }


                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {
                        clientText.Bds = dr["bds"].ToString().Trim();
                        clientText.Yy = dr["yy"].ToString().Trim();
                    }

                    clientWidget.Widget = clientText;
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "select")
                {
                    #region
                    clientWidget.ClientControlType = ClientControlType.select;
                    ClientLable clientLable = new ClientLable();
                    clientLable.Css = dr["css0"].ToString().Trim();
                    clientLable.Value = dr["mc"].ToString().Trim();

                    ClientSelect clientSelect = new ClientSelect();
                    clientSelect.ClientLable = clientLable;
                    clientSelect.Css = dr["css"].ToString().Trim();
                    clientSelect.Event = dr["event"].ToString().Trim();
                    clientSelect.Readonly = (dr["readonly"].ToString().Trim() == "1" ? true : false);
                    clientSelect.Id = dr["htmlid"].ToString().Trim();

                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        clientLable.Width = int.Parse(dr["qwidth"].ToString().Trim());
                        clientLable.Visible = true;
                        clientSelect.Visible = true;
                    }
                    else
                    {
                        clientLable.Visible = false;
                        clientSelect.Visible = false;

                    }

                    clientSelect.Option = dr["bz"].ToString().Trim();
                    //clientSelect.Value = defaultValue;

                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {
                        clientSelect.Bds = dr["bds"].ToString().Trim();
                        clientSelect.Yy = dr["yy"].ToString().Trim();
                    }

                    clientWidget.Widget = clientSelect;
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "button")
                {
                    #region
                    clientWidget.ClientControlType = ClientControlType.button;
                    ClientButton clientButton = new ClientButton();
                    clientButton.Css = dr["css"].ToString().Trim();
                    clientButton.Id = dr["htmlid"].ToString().Trim();
                    clientButton.Readonly = (dr["readonly"].ToString().Trim() == "1" ? true : false);
                    clientButton.Value = dr["mc"].ToString().Trim();
                    clientButton.Event = dr["event"].ToString().Trim();

                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        clientButton.Visible = true;
                        clientButton.Width = int.Parse(dr["width"].ToString().Trim());
                    }
                    else
                    {
                        clientButton.Visible = false;

                    }


                    clientWidget.Widget = clientButton;
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "checkbox")
                {
                    #region
                    clientWidget.ClientControlType = ClientControlType.checkbox;
                    ClientLable clientLable = new ClientLable();
                    clientLable.Css = dr["css0"].ToString().Trim();
                    clientLable.Value = dr["mc"].ToString().Trim();
                    ClientCheckbox clientCheckbox = new ClientCheckbox();
                    clientCheckbox.ClientLable = clientLable;
                    clientCheckbox.Css = dr["css"].ToString().Trim();
                    clientCheckbox.Id = dr["htmlid"].ToString().Trim();
                    clientCheckbox.Event = dr["event"].ToString().Trim();
                    clientCheckbox.Readonly = (dr["readonly"].ToString().Trim() == "1" ? true : false);
                    //clientCheckbox.Value = defaultValue;

                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        clientCheckbox.Width = int.Parse(dr["width"].ToString().Trim());
                        clientLable.Width = int.Parse(dr["qwidth"].ToString().Trim());
                        clientLable.Visible = true;
                        clientCheckbox.Visible = true;
                    }
                    else
                    {
                        clientLable.Visible = false;
                        clientCheckbox.Visible = false;

                    }


                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {
                        clientCheckbox.Bds = dr["bds"].ToString().Trim();
                        clientCheckbox.Yy = dr["yy"].ToString().Trim();
                    }
                    clientWidget.Widget = clientCheckbox;
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "textarea")
                {
                    #region
                    clientWidget.ClientControlType = ClientControlType.textarea;
                    ClientLable clientLable = new ClientLable();
                    clientLable.Css = dr["css0"].ToString().Trim();
                    clientLable.Value = dr["mc"].ToString().Trim();
                    ClientText clientText = new ClientText();
                    clientText.Lable = clientLable;
                    clientText.Css = dr["css"].ToString().Trim();
                    clientText.Event = dr["event"].ToString().Trim();
                    clientText.Readonly = (dr["readonly"].ToString().Trim() == "1" ? true : false);
                    //clientText.Value = defaultValue;
                    clientText.Id = dr["htmlid"].ToString().Trim();

                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        clientLable.Width = int.Parse(dr["qwidth"].ToString().Trim());
                        clientLable.Visible = true;
                        clientText.Visible = true;
                        clientText.Width = int.Parse(dr["width"].ToString().Trim());
                    }
                    else
                    {
                        clientLable.Visible = false;
                        clientText.Visible = false;
                    }


                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {
                        clientText.Bds = dr["bds"].ToString().Trim();
                        clientText.Yy = dr["yy"].ToString().Trim();
                    }

                    clientWidget.Widget = clientText;

                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "td")
                {
                    #region
                    clientWidget.ClientControlType = ClientControlType.td;
                    ClientTd clientTd = new ClientTd();
                    clientTd.Width = int.Parse(dr["qwidth"].ToString().Trim());
                    clientTd.Css = dr["css0"].ToString().Trim();
                    clientTd.Value = dr["mc"].ToString().Trim();
                    clientWidget.Widget = clientTd;
                    #endregion
                }
                else if (dr["type"].ToString().Trim() == "a")
                {
                    #region
                    clientWidget.ClientControlType = ClientControlType.href;
                    ClientLable clientLable = new ClientLable();
                    clientLable.Css = dr["css0"].ToString().Trim();
                    clientLable.Value = dr["mc"].ToString().Trim();
                    ClientHref clientHref = new ClientHref();
                    clientHref.Lable = clientLable;
                    clientHref.Event = dr["event"].ToString().Trim();
                    clientHref.Readonly = (dr["readonly"].ToString().Trim() == "1" ? true : false);
                    clientHref.Css = dr["css"].ToString().Trim();
                    clientHref.Value = dr["mc"].ToString().Trim();

                    if (dr["visible"].ToString().Trim() == "1")
                    {

                        clientLable.Width = int.Parse(dr["qwidth"].ToString().Trim());
                        clientLable.Visible = true;
                        clientHref.Visible = true;
                        clientHref.Width = int.Parse(dr["width"].ToString().Trim());
                    }
                    else
                    {
                        clientLable.Visible = false;
                        clientHref.Visible = false;

                    }
                    clientWidget.Widget = clientHref;
                    #endregion
                }

                else if (dr["type"].ToString().Trim() == "date")
                {
                    #region
                    clientWidget.ClientControlType = ClientControlType.data;
                    ClientLable clientLable = new ClientLable();
                    clientLable.Css = dr["css0"].ToString().Trim();
                    clientLable.Value = dr["mc"].ToString().Trim();
                    ClientDate clientDate = new ClientDate();
                    clientDate.Lable = clientLable;
                    clientDate.Id = dr["htmlid"].ToString().Trim();
                    //clientDate.Value = defaultValue;
                    clientDate.Css = dr["css"].ToString().Trim();


                    if (dr["visible"].ToString().Trim() == "1")
                    {
                        clientLable.Width = int.Parse(dr["qwidth"].ToString().Trim());
                        clientLable.Visible = true;
                        clientDate.Visible = true;
                        clientDate.Width = int.Parse(dr["width"].ToString().Trim());
                    }
                    else
                    {
                        clientLable.Visible = false;
                        clientDate.Visible = false;

                    }


                    if (dr["yy"].ToString().Trim() != string.Empty)
                    {

                        clientDate.Bds = dr["bds"].ToString().Trim();
                        clientDate.Yy = dr["yy"].ToString().Trim();
                    }
                    clientWidget.Widget = clientDate;
                    #endregion
                }
                clientTable.WidgetList.Add(clientWidget);

            }
            return clientTable;
        }

        /// <summary>
        /// 获取下拉框数据源
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="dataSour">下拉数据源</param>
        /// <param name="urlSession">session字典</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDivSelectOption(string dataSour, Dictionary<string, string> urlSession)
        {
            ////返回值
            //string selectHtmlMark = "";
            //if (dataSour.ToLower().Contains("dm") && dataSour.ToLower().Contains("mc") && dataSour.ToLower().Contains("select"))
            //{
            //    //如果类变量不存在这个值,通过SQL查询得到                
            //    foreach (string key in urlSession.Keys)
            //    {
            //        dataSour = dataSour.Replace(key, urlSession[key]);
            //    }

            //    System.Data.DataTable dt = new System.Data.DataTable();

            //    FM.Business.Help hp = new FM.Business.Help();
            //    dt = hp.ExecuteDataset(dataSour).Tables[0];

            //    foreach (System.Data.DataRow dr in dt.Rows)
            //    {
            //        //如果值等于传入进来值,那么这个就是被选中的值
            //        selectHtmlMark += "<OPTION value='" + dr["dm"].ToString().Trim() + "' " + (defaultValue == dr["dm"].ToString().Trim() ? "selected='true'" : "") + ">" + dr["mc"].ToString().Trim() + "</OPTION>";
            //    }
            //}
            //else if (dataSour.Contains("|") && dataSour.Contains("/"))
            //{
            //    //如果使用名|值格式
            //    string[] selectArray = dataSour.Split('|');
            //    for (int i = 0; i < selectArray.Length - 1; i++)
            //    {
            //        string[] selectOption = selectArray[i].Split('/');
            //        selectHtmlMark += "<OPTION value='" + selectOption[0] + "' " + (defaultValue == selectOption[0] ? "selected='true'" : "") + ">" + selectOption[1] + "</OPTION>";
            //    }
            //}
            //return selectHtmlMark;
            //返回值
            Dictionary<string, string> selectHtmlMark = new Dictionary<string, string>();
            if (dataSour.ToLower().Contains("dm") && dataSour.ToLower().Contains("mc") && dataSour.ToLower().Contains("select"))
            {
                //如果类变量不存在这个值,通过SQL查询得到                
                foreach (string key in urlSession.Keys)
                {
                    dataSour = dataSour.Replace(key, urlSession[key]);
                }

                System.Data.DataTable dt = new System.Data.DataTable();

                FM.Business.Help hp = new FM.Business.Help();
                dt = hp.ExecuteDataset(dataSour).Tables[0];

                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    selectHtmlMark.Add(dr["dm"].ToString().Trim(), dr["mc"].ToString().Trim());
                }
            }
            else if (dataSour.Contains("|") && dataSour.Contains("/"))
            {
                //如果使用名|值格式
                string[] selectArray = dataSour.Split('|');
                for (int i = 0; i < selectArray.Length - 1; i++)
                {
                    string[] selectOption = selectArray[i].Split('/');
                    selectHtmlMark.Add(selectOption[0], selectOption[1]);
                }
            }
            return selectHtmlMark;

        }

        /// <summary>
        /// 简单布局的中间方位,直接输出内容控件窗口,等待后面JS动态加载
        /// </summary>        
        /// <returns></returns>
        public string GetCenterHtml()
        {
            return "<div id=\"divPager\" ></div>";

        }
        /// <summary>
        /// 取得默认值
        /// </summary>
        /// <param name="m"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public string GetMrz(string m, NameValueCollection queryString)
        {
            if (m.ToUpper().IndexOf("SELECT") >= 0)
            {
                foreach (string key in queryString.Keys)
                {//用url参数替换默认值里的替换变量
                    m.Replace("@" + key, queryString[key]);
                }
                m.Replace("@userid", this.userid);
                m.Replace("@tzid", this.tzid);
                m.Replace("@username", this.username);
                return business.execSqlCommand(m, "off", new Dictionary<string, string> { { "wid", "-1" }, { "callFucntion", "GetMrz" } })["resultText"];
            }
            else if (m == "@username")
            {
                return this.username;
            }
            else if (m == "@tzid")
            {
                return this.tzid;
            }
            else if (m == "@userid")
            {
                return this.userid;
            }
            else
            {
                return m;
            }
        }

        /// <summary>
        /// 取得编辑权
        /// </summary>
        /// <returns></returns>
        public string GetEditPermission(int intWid)
        {
            FM.Business.Login lg = new FM.Business.Login();
            DataSet ds = lg.GetUser(this.userid);
            if (ds.Tables[0].Rows[0]["platform_edit_permission"].ToString().Trim() == "1")
            {
                return "<div style=\" text-align:right;  margin-left: 20px; margin-right: 20px;\" ><span onclick=\"var url='../web_xtsz/web_xtsz_main_edit.aspx?wid=" + intWid.ToString() + "&title=\'+mySysDate(document.title); window.open(url);\">Edit Console</span></div>";
                 
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// 组合布局
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string MultipleLayout(DataTable frameLayout, HtmlParameter requestParameter)
        {

            Dictionary<string, string> pz = new Dictionary<string, string> { { "t", "north" }, { "l", "west" }, { "c", "center" }, { "r", "east" }, { "b", "south" } };
            string multipleHtmlMark = "";//BODY下总值
            /*
             * 处理组合布局框架中的,URL传值没成功2013-4-24
             * 将URL参数构造传入下级页面中
             */
            System.Collections.Specialized.NameValueCollection queryString = requestParameter.QueryString;
            string urlQueryString = "";
            foreach (string key in queryString.Keys)
            {
                if (key != "wid")
                {
                    urlQueryString += key + "=" + queryString[key] + "&";
                }
            }
            urlQueryString = urlQueryString.Substring(0, urlQueryString.Length - 1);

            foreach (string key in pz.Keys)
            {
                Dictionary<string, string> divDic = new Dictionary<string, string>();
                divDic = IframeInMultipleLayOut(key, pz[key], frameLayout, urlQueryString);
                string htmlMark = divDic["htmlmark"];
                if (key == "c" && htmlMark == string.Empty)
                {
                    htmlMark = "<div id=\"center\" data-options=\"region:'center',border:false\" >缺少中间布局</div>";
                }
                if (htmlMark != string.Empty)
                {
                    string tmpCssStyle = "data-options=\"fit:true\"";
                    if (key == "t" || key == "b")
                    {
                        tmpCssStyle = (divDic["height"] != string.Empty && divDic["height"] != "0" ? "style=\"heigth:" + divDic["height"] + "px;width:100%\"" : tmpCssStyle);

                    }
                    else if (key == "l" || key == "r")
                    {
                        tmpCssStyle = (divDic["width"] != string.Empty && divDic["width"] != "0" ? "style=\"width:" + divDic["width"] + "px;height:100%\"" : tmpCssStyle);

                    }
                    //pzz[i] = "<div id=\"" + Dm(pz[i]) + "\" data-options=\"region:'" + Dm(pz[i]) + "',split:true,border:false \"" + (rsa[1] == "1" ? lsytl : "data-options=\"fit:true\"") + "  ><div class=\"-easyui-layout\" id=\"s" + Dm(pz[i]) + "\" runat=\"server\" " + (rsa[1] == "1" ? lsytl : "data-options=\"fit:true\"") + ">" + pzz[i] + "</div></div>";
                    htmlMark = "<div id=\"" + pz[key] + "\" data-options=\"region:'" + pz[key] + "',split:true,border:false \"" + tmpCssStyle
                        + "  ><div class=\"easyui-layout\" id=\"s" + pz[key] + "\" runat=\"server\" " + "data-options=\"fit:true\"" + ">" + htmlMark + "</div></div>";
                }
                multipleHtmlMark += htmlMark;
            }

            return multipleHtmlMark;

        }

        /// <summary>
        /// 获取每个布局中 上下左右中DIV
        /// </summary>
        /// <param name="pz">具体哪个布局例如:l</param>
        /// <param name="divid">布局对应的方位如:west</param>
        /// <param name="frameLayout"></param>
        /// <param name="urlkey">上一级URL参数</param>
        /// <returns></returns>
        public Dictionary<string, string> IframeInMultipleLayOut(string pz, string divid, DataTable frameLayout, string urlkey)
        {
            string htmlMark = ""; int divHeight = 0; int divWidth = 0;
            //是否存在中间DIV
            bool isNotExistsCenter = true;
            //布局面板,每个方位可再细分五个DIV
            foreach (DataRow dr in frameLayout.Select("ord like '" + pz + "%'"))
            {
                #region
                Dictionary<string, string> mxPz = new Dictionary<string, string> { { "t", "north" }, { "l", "west" }, { "c", "center" }, { "r", "east" }, { "b", "south" } };
                foreach (string mxKey in mxPz.Keys)
                {
                    #region
                    if (dr["ord"].ToString().IndexOf(pz + mxKey) >= 0)
                    {
                        string dwidth = dr["dwidth"].ToString();
                        string dheight = dr["dheight"].ToString();
                        string tmpwidth = "data-options=\"fit:true\"";
                        if (dwidth != "0" || dheight != "0")
                        {
                            tmpwidth = "style=\"" + (dwidth != "0" ? "width:" + dwidth + "px;" : "") + (dheight != "0" ? "height:" + dheight + "px" : "") + "\"";
                            if (pz == "t" || pz == "b")
                            {//只需要算高度
                                if (dr["ord"].ToString().Substring(1, 1) == "t" || dr["ord"].ToString().Substring(1, 1) == "c" || dr["ord"].ToString().Substring(1, 1) == "b")
                                {
                                    divHeight += int.Parse(dheight);
                                }

                            }
                            else if (pz == "l" || pz == "r")
                            {//只需要算宽度
                                if (dr["ord"].ToString().Substring(1, 1) == "l" || dr["ord"].ToString().Substring(1, 1) == "c" || dr["ord"].ToString().Substring(1, 1) == "r")
                                {
                                    divWidth += int.Parse(dwidth);
                                }
                            }

                        }
                        string url = "";
                        if (dr["naspx"].ToString() != string.Empty)
                        {
                            url = dr["naspx"].ToString().Trim() + "?" + urlkey;
                        }
                        else
                        {
                            url = "lss.aspx?wid=" + int.Parse(dr["nwebid"].ToString().Trim()) + "&" + urlkey;
                        }
                        htmlMark += "<div id=\"s" + divid + "_" + mxPz[mxKey] + "\" data-options=\"region:'" + mxPz[mxKey] + "',split:true,border:false \" " + tmpwidth + " >" +
                            "<iframe id=\"" + dr["htmlid"].ToString().Trim() + "\" scrolling=\"auto\" frameborder=\"0\"  src=\"" + url + "\"  style=\"width:100%;height:100%;\" " + tmpwidth + "></iframe></div>";
                        if (mxKey == "c") { isNotExistsCenter = false; }
                    }
                    #endregion
                }
                #endregion
            }
            //如果整个布局五个div都没有,那就空着
            //如果只是少了center那么提醒
            if (isNotExistsCenter && htmlMark != string.Empty)
            {
                htmlMark += "<div id=\"center\" data-options=\"region:'center',border:false\" >缺少中间布局</div>";
            }
            return new Dictionary<string, string> { { "htmlmark", htmlMark }, { "width", divWidth.ToString() }, { "height", divHeight.ToString() } };
        }


    }
}
