using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DTO;
using MyTy;

namespace FM.Controls
{
    /// <summary>
    /// Repeater分页控件
    /// 变量分类
    /// A。从客户端取值的
    /// B。从客户端取值，但如果没值，按系统分配
    /// C。处理过程要用到的
    /// </summary>
    [ToolboxData("<{0}:FMPager runat=\"server\" />")]
    public class FMPager : WebControl, INamingContainer
    {
        //导航条 翻页
        protected static readonly string navigateClassFmt = "<div><table class=\"navigateClass\" ><tr><td><div class=\"pageClass\">{0}</div></td></tr></table></div>";
        protected static readonly string navigatePageTotalFmt = "<div class=\"pagetotal\">总记录：<strong>{0}</strong>&nbsp;&nbsp;&nbsp;页码：<span>{1}</span>/{2}<input type=\"hidden\" runat=\"server\" id=\"pageSize\" value=\"{3}\" /></div>";
        protected static readonly string navigateAjaxlinkFmt = "<li><a href=\"javascript:;\" onclick=\"{0}('{1}');\" class=\"{3}\">{2}</a></li>";
        protected static readonly string navigateDisableLinkFmt = "<li><a href=\"#\">{0}</a></li>";
        protected static readonly string navigateGotoFmt = "<div prtNoprint=true  prtdisappear=true class=\"fr\">转到第&nbsp;<input type=\"text\" style=\"width: 40px;\" value=\"{1}\" onblur=\"{0}(this.value);\" />&nbsp;页</div>";
        public FMPagerArguments pagerArguments = new FMPagerArguments();

        /// <summary>
        /// 页头数据源
        /// </summary>
        public DataTable headlineData = new DataTable();

        /// <summary>
        /// 页内容数据源
        /// </summary>
        public DataTable detailsData = new DataTable();

        /// <summary>
        /// 页内容数总据源
        /// </summary>
        public DataTable totalDetailsData = new DataTable();

        /// <summary>
        /// 到表字段属性
        /// </summary>
        public DataTable columnDataType = new DataTable();

        /// <summary>
        /// 主数据源的SQL代码
        /// </summary>
        public string detailsSql = "";

        /// <summary>
        /// 明细数据源
        /// </summary>
        public DataTable cmDetailsData = new DataTable();

        /// <summary>
        /// 页头尺码
        /// </summary>
        public DataTable cmHeadlineData = new DataTable();

        /// <summary>
        /// 主表与尺码的关联
        /// </summary>
        public string masterCmRelation = "";

        /// <summary>
        /// 主表与明细的关联
        /// </summary>
        public string masterSlaveKey = "";
        /// <summary>
        /// 明细与尺码
        /// </summary>
        public string detailCmRelation = "";

        /// <summary>
        /// 表格样式
        /// </summary>
        public int tableCSSBorderLeft = 1;
        public int tableCSSBorderRight = 1;
        public int tableCSSPaddingRight = 10;
        /// <summary>
        /// 尺码最大数量
        /// </summary>
        public static int sizeCount = 999;
        /// <summary>
        /// 记录尺码的顺序,从0开始,0,1,2,3,
        /// </summary>
        string[] cmord = new string[sizeCount];
        /// <summary>
        /// 记录尺码每个顺序对应的 |尺码组ID1/尺码ID1|尺码组ID2/尺码ID2|
        /// </summary>
        string[] cmid = new string[sizeCount];

        //此页表名
        //public string tbname = "";
        //后台数据每列列宽之合
        //public int tbWidth = 0;

        /// <summary>
        /// 打印的时候,如果下拉框的值,不需要重复读取数据库
        /// </summary>
        public Dictionary<string, DataTable> selectOption = new Dictionary<string, DataTable>();

        /// <summary>
        /// 记录查询所有列
        /// </summary>
        public string detailsColumns = "";

        /// <summary>
        /// 是否允许向下移动
        /// </summary>
        public bool addNewRowPermission = false;

        public System.Collections.Specialized.NameValueCollection requestFormParm;
        
        public override void DataBind()
        {
            base.DataBind();
            Control repeater = FindRepeater(Page);
            HtmlContainerControl MyRepeater = (HtmlContainerControl)FindRepeater(Page);

            MyRepeater.InnerHtml = Html().Data.Html;
            ChildControlsCreated = false;

        }

        public Result<PageHtml> Html()
        {
            PageHtml pageHtml = new PageHtml();
            //string str = "<table ><tr><td>"+"<div  class='fmheadclass'   >" + this.GetHeadHtml(this.DataHead, this.WebId.ToString().Trim()) + "</div></td></tr><tr><td class='hc_tdclass'><div  class='fmcontentclass' " + fmcontentclass + "   >" + this.GetContentHtml(this.DataSource, this.DataHead, this.WebId.ToString().Trim()) + "</div></td></tr><tr><td>" + hj + "</td></tr></table>";
            //int divw = (this.tbWidth + this.vcols * 5 + this.scolwidth);
            //string str = "<ol style='list-style-type:none'><li><div  class='fmheadclass' style='width:" + divw.ToString().Trim() + "px'  >" + this.GetHeadHtml(this.DataHead, this.WebId.ToString().Trim()) + "</div></li>" + "<li><div  class='fmcontentclass' style='width:" + (divw + 5).ToString().Trim() + "px' " + fmcontentclass + "   >" + this.GetContentHtml(this.DataSource, this.DataHead, this.WebId.ToString().Trim()) + "</div></li>" + "<li><div class='fmheadclass' style='width:" + divw.ToString().Trim() + "px' >" + hj + "</div></li></ol>";
            string headHtml = this.GetHeadHtml(this.headlineData, pagerArguments.wid.ToString().Trim(), this.cmHeadlineData, this.masterCmRelation);
            string contentHtml = this.GetContentHtml(this.detailsData, this.headlineData, pagerArguments.wid.ToString().Trim(), this.masterCmRelation, this.masterSlaveKey, this.cmDetailsData, this.detailCmRelation, this.cmHeadlineData);
            int tableWidth=0;
            string hjHtml = this.GetHjHtml(this.headlineData, this.totalDetailsData, this.masterSlaveKey, this.masterCmRelation, this.detailCmRelation, this.cmDetailsData, pagerArguments.wid.ToString().Trim(), ref tableWidth);
            string contentCSS = "";
            if (pagerArguments.clientHeight > 0 && contentHtml.Length > 0)
            {
                Business.ProcPager inf = new Business.ProcPager();
                DataSet widConfig = inf.GetTableRecord("v_wid_layout", "webid=" + pagerArguments.wid);//得到wid 对应信息                
                contentCSS = string.Format("style='max-height:{0}px;overflow-y:auto;width:" + (tableWidth+20) + "px;'", (pagerArguments.clientHeight - 160 - int.Parse(widConfig.Tables[0].Rows[0]["northheight"].ToString()) - int.Parse(widConfig.Tables[0].Rows[0]["southheight"].ToString())).ToString());
            }
            pageHtml.Html = string.Format("<div>{0}</div><div {3} >{1}</div><div>{2}</div>",headHtml,contentHtml,hjHtml, contentCSS);
            //        string Html = "<div class=\"easyui-layout\" data-options=\"fit:true\">"+
            //"<div region=\"north\" border=\"false\" style=\"background:#B3DFDA;\">" + this.GetHeadHtml(this.headlineData, pagerArguments.wid.ToString().Trim(), this.cmHeadlineData, this.masterCmRelation) + "</div>" +
            //"<div region=\"south\" border=\"false\" style=\"background:#A9FACD;\">" + this.GetHjHtml(this.headlineData, this.totalDetailsData, this.masterSlaveKey, this.masterCmRelation, this.detailCmRelation, this.cmDetailsData, pagerArguments.wid.ToString().Trim()) + "</div>" +
            //"<div region=\"center\" >" + this.GetContentHtml(this.detailsData, this.headlineData, pagerArguments.wid.ToString().Trim(), this.masterCmRelation, this.masterSlaveKey, this.cmDetailsData, this.detailCmRelation, this.cmHeadlineData) + "</div>" +
            //"</div>";
            pageHtml.ColumnCount = this.headlineData.Select("visible=1").Length;
            return ResultUtil<PageHtml>.success(pageHtml);

        }

        public virtual void GetDate() { }

        /// <summary>
        /// 得到内容页
        /// tr.rownum td.field td.innerctrl
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dt_h"></param>
        /// <param name="webid"></param>
        /// <returns></returns>
        public string GetContentHtml(DataTable dt, DataTable dt_h, string webid, string mxhord, string mxgl, DataTable DataMx, string mxhgl, DataTable DataHeadCm)
        {

            StringBuilder strBuild = new StringBuilder();
            MyTy.Utils us = new MyTy.Utils();
            if (dt == null || dt.Rows.Count == 0)
            {
                #region 数据源为空

                string emptyContent = GetEmptyContent(dt_h, webid);
                return "<Table id='table_Content_" + webid + "' runat='server' class='style_Content'    >" + emptyContent + "</table>";
                #endregion
            }
            else
            {
                #region 数据源非空
                int cols = dt.Columns.Count;
                // myclass myc = new myclass();                  

                foreach (DataRow dr in dt.Rows)// 每行
                {
                    strBuild.Append("<tr rownum=\"" + dt.Rows.IndexOf(dr) + "\">");
                    #region 隐藏的标记列,用于标记此行数据是否被变动过
                    string flag_lid = " id='table_td_" + webid + "_" + dt.Rows.IndexOf(dr) + "_flag' ";
                    string flag = " id='table_" + webid + "_" + dt.Rows.IndexOf(dr) + "_flag' ";
                    if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                    {
                    }
                    else
                    {
                        strBuild.Append("<td style='display:none' field='flag' innerctrl='flag' " + flag_lid + " ><input type='hidden' " + flag + "value='0'/></td>");
                    }
                    #endregion
                    #region 分解主表与明细关系

                    string mx_zb = "";
                    for (int d = 0; d < mxgl.Split(',').Length; d++)
                    {
                        if (mxgl.Split(',')[d] != "")
                        {
                            mx_zb += " " + mxgl.Split(',')[d] + "='" + dr[mxgl.Split(',')[d].ToString()] + "' and";
                        }
                    }
                    if (mx_zb != "")
                    {
                        mx_zb = mx_zb.Substring(0, mx_zb.Length - 3);
                    }
                    #endregion
                    #region 主体控件构造
                    foreach (DataRow dr_h in dt_h.Rows)
                    {

                        if (dr_h["type"].ToString().Trim() == "mx")
                        {
                            #region 尺码
                            for (int i = 0; i < sizeCount; i++)
                            {
                                if (this.cmord[i] != null && this.cmord[i] != "")
                                {
                                    string lid = ""; string lputid = "";
                                    string drcn = "";
                                    string where = mx_zb + " and '" + this.cmid[i] + "' like  '%|" + dr[mxhord].ToString().Trim() + "/'+" + mxhgl + " +'|%' ";
                                    #region
                                    foreach (DataRow dr1 in DataMx.Select(where))
                                    {
                                        drcn = MyTy.Utils.HtmlCha(dr1["sl"].ToString().Trim());
                                        //begin 根据数据原始值和设置,是否显示0或默认日期处理                                        
                                        //处理默认属性,如0值不显示,日期空值不显示等
                                        if ((dr1.Table.Columns["sl"].DataType.ToString().ToUpper().IndexOf("INT") >= 0 || dr1.Table.Columns["sl"].DataType.ToString().ToUpper().IndexOf("DECIMAL") >= 0 || dr1.Table.Columns["sl"].DataType.ToString().ToUpper().IndexOf("FLOAT") >= 0) && dr_h["showzero"].ToString().Trim() == "0")
                                        {//判断是否是数值 ,且设置不显示0
                                            if (drcn != "" && Convert.ToDecimal(drcn) == 0)
                                            {
                                                drcn = "";
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(dr_h["format"].ToString().Trim()) && !string.IsNullOrEmpty(drcn))
                                                {
                                                    drcn = string.Format(dr_h["format"].ToString().Trim(), Convert.ToDecimal(drcn));
                                                }
                                            }
                                        }


                                    }
                                    #endregion

                                    GetWebContentControlId(webid, dt.Rows.IndexOf(dr), dr_h["ywname"].ToString().Trim() + "_" + i, ref lid, ref lputid);

                                    if (dr_h["visible"].ToString().Trim() == "0")
                                    {
                                        #region 如果是隐藏字段
                                        if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                        {
                                        }
                                        else
                                        {
                                            strBuild.Append("<td gfield='" + dr_h["ywname"].ToString().Trim() + "' field='" + dr_h["ywname"].ToString().Trim() + "_" + i + "' innerctrl='hidden' style='display:none' " + lid + " ><input type='hidden' " + lputid + " value=\"" + drcn + "\"/></td>");
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region
                                        string itype = "";
                                        string linput = "";
                                        string myevent = "";
                                        GetWebContentControlEvent(dr_h["event"].ToString().Trim(), dr_h["ywname"].ToString().Trim() + "_" + i, drcn, dr_h["type"].ToString().Trim(), dt.Rows.IndexOf(dr), ref myevent);

                                        //控制控件只读1.字段属性只读
                                        string myreadonly = (dr_h["readonly"].ToString().Trim() == "1" ? " readonly='readonly' " : " ");

                                        //td内字控件的宽度
                                        string stywidth = " style='width:" + Convert.ToInt32(dr_h["width"]) + "px'";
                                        itype = " type=\"text\" "
                                            + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_text_d\" " : " class=\"style_Content_td_text_e\" ");

                                        if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                        {//如果是打印                                                
                                            linput = drcn;
                                        }
                                        else
                                        {
                                            linput = "<input " + itype
                                               + myreadonly + myevent + stywidth
                                               + lputid + "value=\"" + drcn + "\" />";

                                        }
                                        strBuild.Append("<td gfield='" + dr_h["ywname"].ToString().Trim() + "' field='" + dr_h["ywname"].ToString().Trim() + "_" + i + "' innerctrl='" + dr_h["type"].ToString().Trim() + "' " +
                                             "onkeydown='fmOnKey(event,id," + (this.addNewRowPermission && !pagerArguments.prtFlag && !pagerArguments.excelFlag ? "1" : "0") + ")' " + stywidth + lid + "> " + linput + "</td>");
                                        #endregion
                                    }
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            #region
                            string cn = dr_h["ywname"].ToString().Trim();//列名                                 
                            #region

                            #region 得到TD ID 与TD下字控件ID
                            string lputid = "";
                            string lid = "";
                            GetWebContentControlId(webid, dt.Rows.IndexOf(dr), cn, ref lid, ref lputid);
                            #endregion

                            string drcn = MyTy.Utils.HtmlCha(dr[cn].ToString().Trim());//数据原始值

                            #region  根据数据原始值和设置,是否显示0或默认日期处理
                            DataRow[] tydr = this.columnDataType.Select("column_name='" + cn + "'");
                            //处理默认属性,如0值不显示,日期空值不显示等
                            if ((tydr[0]["data_type"].ToString().ToUpper().IndexOf("INT") >= 0 || tydr[0]["data_type"].ToString().ToUpper().IndexOf("DECIMAL") >= 0 || tydr[0]["data_type"].ToString().ToUpper().IndexOf("FLOAT") >= 0) && dr_h["showzero"].ToString().Trim() == "0")
                            {//判断是否是数值 ,且设置不显示0
                             //数字型字段,但是空值,就要先判断是否是数字
                                if (us.IsNumber(drcn))
                                {
                                    if (Convert.ToDecimal(drcn) == 0)
                                    {
                                        drcn = "";
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(dr_h["format"].ToString().Trim()) && !string.IsNullOrEmpty(drcn))
                                        {
                                            drcn = string.Format(dr_h["format"].ToString().Trim(), Convert.ToDecimal(drcn));
                                        }
                                    }
                                }

                            }
                            if ((tydr[0]["data_type"].ToString() == "datetime" || tydr[0]["data_type"].ToString() == "date") && dr_h["showmrrq"].ToString().Trim() == "0")
                            {//判断是否是日期 ,且设置不显示"1900-01-01"
                                if (drcn.Substring(0, 4) == "1900") { drcn = ""; }
                            }


                            #endregion


                            if (dr_h["visible"].ToString().Trim() == "0")
                            {
                                #region 如果是隐藏字段
                                if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                {//打印的时候隐藏列不显示
                                }
                                else
                                {
                                    strBuild.Append("<td field='" + cn + "' innerctrl='hidden' style='display:none' " + lid + " ><input type='hidden' " + lputid + " value=\"" + drcn + "\"/></td>");
                                }
                                #endregion
                            }
                            else
                            {
                                #region
                                //元素控件的类型
                                string itype = "";

                                //td之间的innerHtml
                                string linput = "";

                                #region 事件,可将定义的value 转为真实的值,,row 转为行号
                                string myevent = "";
                                GetWebContentControlEvent(dr_h["event"].ToString().Trim(), cn, drcn, dr_h["type"].ToString().Trim(), dt.Rows.IndexOf(dr), ref myevent);
                                #endregion

                                //控制控件只读1.字段属性只读
                                string myreadonly = (dr_h["readonly"].ToString().Trim() == "1" ? " readonly='readonly' " : " ");

                                //控制控件失效1.字段属性只读,2.打印时候
                                //有些控件只有失效,没有只读
                                string mydisable = (dr_h["readonly"].ToString().Trim() == "1" || pagerArguments.prtFlag || pagerArguments.excelFlag ? " disabled='disabled' " : " ");

                                //td内字控件的宽度
                                string stywidth = " style='width:" + Convert.ToInt32(dr_h["width"]) + "px'";

                                switch (dr_h["type"].ToString().Trim())
                                {
                                    case "checkbox":
                                        #region
                                        itype = " type=\"checkbox\" " + (drcn == "1" ? "checked=\"true\" " : "")
                                            + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_checkbox_d\" " : " class=\"style_Content_td_checkbox_e\" ");

                                        linput = "<input " + itype + mydisable + myevent + stywidth + lputid + " />";
                                        break;
                                    #endregion
                                    case "select":
                                        #region
                                        itype = dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_select_d\" " : " class=\"style_Content_td_select_e\" ";

                                        //得到下拉框内容,如果是打印状态,只输出一个值
                                        string OPTION = GetOption(drcn, dr_h["bz"].ToString().Trim(), pagerArguments.prtFlag || pagerArguments.excelFlag);

                                        if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                        {//如果是打印
                                            linput = OPTION;
                                        }
                                        else
                                        {
                                            linput = "<select " + itype + mydisable + myevent + stywidth
                                                + lputid + ">"
                                                + OPTION + "</SELECT>";
                                        }
                                        break;
                                    #endregion
                                    case "button":
                                        #region
                                        itype = " type=\"button\" "
                                            + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_button_d\" " : " class=\"style_Content_td_buttont_e\" ");
                                        if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                        {//如果是打印
                                            linput = dr_h["btnvalue"].ToString().Trim();
                                        }
                                        else
                                        {
                                            linput = "<input " + itype
                                               + mydisable + myevent + stywidth
                                               + lputid + "value=\"" + dr_h["btnvalue"].ToString().Trim() + "\" />";
                                        }
                                        break;
                                    #endregion
                                    case "td":
                                        #region
                                        if (dr_h["bz"].ToString() != string.Empty)
                                        {
                                            //如果备注有值,当成下拉框处理,取出其中的值
                                            //查询的时候会用到 khid  select khid,khmc from khb
                                            //这个目前是可以规避掉
                                            string OPTIONtd = GetOption(drcn, dr_h["bz"].ToString().Trim(), true);
                                            linput = OPTIONtd;
                                        }
                                        else
                                        {
                                            linput = drcn;
                                        }
                                        break;
                                    #endregion
                                    case "a":
                                        #region
                                        if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                        {
                                            //如果是打印
                                            //linput = "<span " + stywidth + ">" + drcn + "</span>";
                                            linput = drcn;
                                        }
                                        else
                                        {
                                            linput = "<a href=\"#\" class=\"style_Content_td_a_d\"  " + lputid + myevent + "> " + drcn + "</a>";
                                        }
                                        break;
                                    #endregion
                                    case "textarea":
                                        #region
                                        if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                        {//如果是打印
                                            linput = drcn;
                                            break;
                                        }
                                        else
                                        {
                                            if (drcn != string.Empty)
                                            {
                                                itype = "textarea "
                                                    + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_textarea_d\" " : " class=\"style_Content_td_textarea_e\" ");

                                                linput = "<" + itype
                                                   + myreadonly + myevent + stywidth
                                                   + lputid + ">" + drcn + "</textarea>";
                                                break;
                                            }
                                            else
                                            {
                                                //如果是空值,使用textarea会不美观
                                                //使用text控件
                                                //不换为td 这样可以输入值!
                                                itype = " type=\"text\" "
                                                    + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_text_d\" " : " class=\"style_Content_td_text_e\" ");

                                                linput = "<input " + itype
                                                   + myreadonly + myevent + stywidth
                                                   + lputid + "value=\"" + drcn + "\" />";
                                                break;
                                            }
                                        }
                                    #endregion
                                    default:
                                        #region
                                        //默认为文本
                                        itype = " type=\"text\" "
                                            + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_text_d\" " : " class=\"style_Content_td_text_e\" ");
                                        if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                        {//如果是打印                                                
                                            linput = drcn;
                                        }
                                        else
                                        {
                                            linput = "<input " + itype
                                               + myreadonly + myevent + stywidth
                                               + lputid + "value=\"" + drcn + "\" />";
                                        }
                                        break;
                                        #endregion
                                };
                                strBuild.Append("<td field='" + cn + "' innerctrl='" + dr_h["type"].ToString().Trim() + "' " +
                                     "onkeydown='fmOnKey(event,id," + (this.addNewRowPermission && !pagerArguments.prtFlag && !pagerArguments.excelFlag ? "1" : "0") + ")' " + stywidth + lid + "> " + linput + "</td>");

                                #endregion
                            }
                            #endregion
                            #endregion
                        }
                    }
                    #endregion
                    #region 最后一个补充控件
                    if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                    {
                        strBuild.Append("</tr>");
                    }
                    else
                    {
                        //如果不是打印输入最后一个,自动升长!
                        strBuild.Append("<td field='nbsp' innerctrl='nbsp' id='table_td_" + webid + "_" + dt.Rows.IndexOf(dr) + "_nbsp' >&nbsp;</td></tr>");
                    }
                    #endregion

                }
                //return "<Table id='table_Content_" + webid + "' runat='server' class='style_Content' style='width: " + (this.tbWidth + this.vcols * 5 + this.scolwidth).ToString().Trim() + "px'> " + str + "</table>";
                //string kdwidth = (this.PrtFlag == "sysprt" ? "style='width: " + (this.tbWidth + this.vcols * 5).ToString().Trim() + "px' " : "");//如果是打印请固定表格的宽度
                string kdwidth = "";
                return "<Table m='post' id=\"table_Content_" + webid + "\" runat=\"server\" class=\"style_Content\" " + kdwidth + " > " + strBuild.ToString() + "</table>";
                #endregion

            }
        }

        /// <summary>
        /// 获取下拉框数据源
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sour"></param>        
        /// <returns></returns>
        public string GetOption(string value, string sour, bool printFlag)
        {
            //返回值
            string strSql = "";
            //打印时,需要返回的值
            string strSqlPrint = "";
            //下拉框的值
            string myValue = "";
            //下拉框的内容
            string myText = "";
            DataTable dt = new DataTable();
            if (sour.ToLower().Contains("dm") && sour.ToLower().Contains("mc") && sour.ToLower().Contains("select") && sour.ToLower().Contains("from"))
            {
                #region
                if (selectOption != null && selectOption.ContainsKey(sour))
                {
                    //在下接数据源中寻找是否已经临时保存了这个下拉框的数据源
                    //如果类变量已经保存,就不需要从SQL中读取,提高效率
                    foreach (var item in selectOption)
                    {
                        if (item.Key == sour) { dt = item.Value; }
                    }
                }
                else
                {
                    //如果类变量不存在这个值,通过SQL查询得到
                    //dbConnet.dbstring db = new dbConnet.dbstring();
                    if (sour.IndexOf("@userid") > 0)
                    {
                        sour = sour.Replace("@userid", MySession.SessionHandle.Get("userid").ToString().Trim());
                    }
                    if (sour.IndexOf("@tzid") > 0)
                    {
                        sour = sour.Replace("@tzid", MySession.SessionHandle.Get("tzid").ToString().Trim());
                    }
                    FM.Business.Help execObj = new FM.Business.Help();
                    dt = execObj.ExecuteDataset(sour).Tables[0];
                }

                foreach (DataRow dr in dt.Rows)
                {
                    myValue = dr["dm"].ToString().Trim();
                    myText = dr["mc"].ToString().Trim();
                    //如果值等于传入进来值,那么这个就是被选中的值
                    strSql += "<OPTION value='" + myValue + "' " + (value == myValue ? "selected='true'" : "") + ">" + myText + "</OPTION>";
                    if (strSqlPrint == "")
                    {
                        //打印的时候返回下拉框的值
                        strSqlPrint = (value == myValue ? myText : "");
                    }
                }
                //将下拉数据源临时保存,下一次可使用
                if (selectOption == null || !selectOption.ContainsKey(sour)) { selectOption.Add(sour, dt); }
                #endregion
            }
            else if (sour.Contains("|") && sour.Contains("/"))
            {
                #region
                //如果使用名|值格式
                string[] sArray = sour.Split('|');
                for (int i = 0; i < sArray.Length - 1; i++)
                {
                    string[] sArrayl = sArray[i].Split('/');
                    myValue = sArrayl[0];
                    myText = sArrayl[1];
                    strSql += "<OPTION value='" + myValue + "' " + (value == myValue ? "selected='true'" : "") + ">" + myText + "</OPTION>";
                    if (strSqlPrint == "")
                    {
                        //打印的时候返回下拉框的值
                        strSqlPrint = (value == myValue ? myText : "");
                    }
                }
                #endregion
            }

            if (printFlag)
            {
                return strSqlPrint;
            }
            else
            {
                return strSql;
            }
        }

        /// <summary>
        /// 获取内容为空时,数据源应返回的内容
        /// </summary>
        /// <param name="dt_h"></param>
        /// <param name="webid"></param>
        /// <returns></returns>
        public string GetEmptyContent(DataTable dt_h, string webid)
        {
            string flag_lid = " id='table_td_" + webid + "_0_flag' ";
            string flag = " id='table_" + webid + "_0_flag' ";
            string str = "<tr rownum=\"0\">";
            str += "<td field='flag' innerctrl='flag'  style='display:none' " + flag_lid + " ><input type='hidden' " + flag + "value='0'/></td>";

            //把得到查询字段的值分开
            string[] sArray = Regex.Split(this.detailsColumns, ",", RegexOptions.IgnoreCase);

            int cols = sArray.Length;
            foreach (DataRow dr_h in dt_h.Rows)
            {
                if (dr_h["type"].ToString().Trim() == "mx")
                {
                    #region 尺码
                    for (int i = 0; i < 50; i++)
                    {
                        if (this.cmord[i] != null && this.cmord[i] != "")
                        {
                            string lid = ""; string lputid = "";
                            GetWebContentControlId(webid, 0, dr_h["ywname"].ToString().Trim() + "_" + i, ref lid, ref lputid);
                            string myevent = "";
                            GetWebContentControlEvent(dr_h["event"].ToString().Trim(), dr_h["ywname"].ToString().Trim() + "_" + i, "", dr_h["type"].ToString().Trim(), 0, ref myevent);
                            string myreadonly = (dr_h["readonly"].ToString().Trim() == "1" ? " readonly='readonly' " : " ");
                            string itype = " type=\"text\" "
                                + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_text_d\" " : " class=\"style_Content_td_text_e\" ");

                            string stywidth = " style='width: " + Convert.ToInt32(dr_h["width"]) + "px'";
                            string linput = "";
                            if (dr_h["visible"].ToString().Trim() == "0")
                            {
                                str += "<td gfield='" + dr_h["ywname"].ToString().Trim() + "' field='" + dr_h["ywname"].ToString().Trim() + "_" + i + "' style='display:none' innerctrl='hidden'  " + lid + "><input type='hidden' " + lputid + " /></td>";
                            }
                            else
                            {
                                if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                {
                                    linput = "";
                                }
                                else
                                {
                                    linput = "<input " + itype + myreadonly + myevent + stywidth + lputid + "/>";
                                }
                                str += "<td gfield='" + dr_h["ywname"].ToString().Trim() + "' field='" + dr_h["ywname"].ToString().Trim() + "_" + i + "' innerctrl='" + dr_h["type"].ToString().Trim() + "'  "
                + "onkeydown=\"fmOnKey(event,id," + (this.addNewRowPermission ? "1" : "0") + ")\" " + stywidth + lid + "> " + linput + "</td>";
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region 正常字段
                    for (int j = 0; j < cols; j++)
                    {
                        string cn = sArray[j].Trim();//列名
                        if (dr_h["ywname"].ToString().Trim() == cn)
                        {
                            //begin得到TD ID 与 TD内元素ID
                            string lputid = "";
                            string lid = "";
                            GetWebContentControlId(webid, 0, cn, ref lid, ref lputid);
                            //end得到TD ID 与 TD内元素ID

                            if (dr_h["visible"].ToString().Trim() == "0")
                            {
                                str += "<td field='" + cn + "'  innerctrl='hidden' style='display:none' " + lid + " ><input type='hidden' " + lputid + " /></td>";
                            }
                            else
                            {
                                #region

                                string itype = "";
                                string linput = "";
                                string myevent = "";
                                GetWebContentControlEvent(dr_h["event"].ToString().Trim(), cn, "", dr_h["type"].ToString().Trim(), 0, ref myevent);
                                string myreadonly = (dr_h["readonly"].ToString().Trim() == "1" ? " readonly='readonly' " : " ");
                                string mydisable = (dr_h["readonly"].ToString().Trim() == "1" || pagerArguments.prtFlag || pagerArguments.excelFlag ? " disabled='disabled' " : " ");
                                string stywidth = " style='width: " + Convert.ToInt32(dr_h["width"]) + "px'";
                                if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                                {
                                    linput = "";
                                }
                                else
                                {
                                    #region
                                    switch (dr_h["type"].ToString().Trim())
                                    {
                                        case "checkbox":
                                            itype = " type=\"checkbox\" "
                                                + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_checkbox_d\" " : " class=\"style_Content_td_checkbox_e\" ");
                                            //
                                            linput = "<input " + itype
                                           + myreadonly + myevent + stywidth
                                           + lputid + " />";
                                            break;
                                        case "select":
                                            itype = dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_select_d\" " : " class=\"style_Content_td_select_e\" ";
                                            string OPTION = GetOption("", dr_h["bz"].ToString().Trim(), pagerArguments.prtFlag || pagerArguments.excelFlag);
                                            linput = "<select " + itype + mydisable + myevent + stywidth
                                                + lputid + ">"
                                                + OPTION + "</SELECT>";
                                            break;

                                        case "button":
                                            itype = " type=\"button\" "
                                                + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_button_d\" " : " class=\"style_Content_td_buttont_e\" ");

                                            linput = "<input " + itype
                                           + mydisable + myevent + stywidth
                                           + lputid + "value=\"" + dr_h["btnvalue"].ToString().Trim() + "\" />";
                                            break;
                                        case "td":
                                            linput = "";
                                            break;
                                        case "a":
                                            linput = "<a href=\"#\" class=\"style_Content_td_a_d\"  " + lputid + myevent + "></a>";
                                            break;
                                        default:
                                            itype = " type=\"text\" "
                                                + (dr_h["readonly"].ToString().Trim() == "1" ? " class=\"style_Content_td_text_d\" " : " class=\"style_Content_td_text_e\" ");

                                            linput = "<input " + itype
                                           + myreadonly + myevent + stywidth
                                           + lputid + "/>";
                                            break;
                                    };
                                    #endregion
                                }
                                str += "<td field='" + cn + "' innerctrl='" + dr_h["type"].ToString().Trim() + "'  "
                                    + "onkeydown=\"fmOnKey(event,id," + (this.addNewRowPermission ? "1" : "0") + ")\" " + stywidth + lid + "> " + linput + "</td>";
                                #endregion
                            }
                        }
                    }
                    #endregion
                }
            }
            if (pagerArguments.prtFlag || pagerArguments.excelFlag)
            {
                return str + "</tr>";
            }
            else
            {
                return str + "<td field='nbsp' innerctrl='nbsp' id=\"table_td_" + webid + "_0_nbsp\"  >&nbsp;</td></tr>";
            }
        }

        /// <summary>
        /// 得到合计
        /// </summary>
        /// <param name="headlineData"></param>
        /// <param name="webid"></param>
        /// <returns></returns>
        public string GetHjHtml(DataTable headlineData, DataTable totalDetailsData, string masterSlaveKey, string masterCmRelation, string detailCmRelation, DataTable cmDetailsData, string webid,ref int tableWidth)
        {
            string str = "";
            tableWidth = 0;
            foreach (DataRow dr in headlineData.Rows)
            {
                string lid = "";
                string lputid = "";
                string lid_cm = "";
                string lputid_cm = "";
                string drname = dr["ywname"].ToString().Trim();
                string drzwname = MyTy.Utils.HtmlCha(dr["zwname"].ToString().Trim());
                GetWebHjControlId(webid, drname, ref lid, ref lputid, ref lid_cm, ref lputid_cm);
                DataTable cm = new DataTable();
                if (totalDetailsData.Rows.Count > 0 && cmDetailsData.Rows.Count > 0)
                {
                    char split = ',';
                    cm = MyTy.Utils.Join(cmDetailsData, totalDetailsData, masterSlaveKey, split, masterSlaveKey, split);
                }

                if (dr["visible"].ToString().Trim() == "0")
                {
                    if (pagerArguments.prtFlag || pagerArguments.excelFlag) { }
                    else
                    {
                        if (dr["type"].ToString().Trim() == "mx")
                        {
                            for (int i = 0; i < sizeCount; i++)
                            {
                                if (this.cmord[i] != null && this.cmord[i] != "")
                                {
                                    str += "<td style='display:none' id=\"" + lid_cm + "_" + i + "\"  ><input type='hidden'  id=\"" + lputid_cm + "_" + i + "\" value=\"" + drzwname + "\"/></td>";
                                }
                            }
                        }
                        else
                        {
                            str += "<td style='display:none'" + lid + "><input type='hidden'" + lputid + "value=\"" + drzwname + "\"/></td>";
                        }
                    }
                }
                else
                {
                    //twidth += Convert.ToInt32(dr["width"]);
                    //string rmc = Convert.ToInt32(dr["sx"]) == 1 ? " onmousedown='javascript:h_rightclk(event)' " : "";

                    string linput = "";
                    if (dr["type"].ToString().Trim() == "mx")
                    {
                        #region  尺码设计
                        string cmstr = "";
                        if (dr["hj"].ToString().Trim() == "1" && cm.Rows.Count > 0)
                        {
                            for (int i = 0; i < sizeCount; i++)
                            {
                                if (this.cmord[i] != null && this.cmord[i] != "")
                                {
                                    linput = cm.Compute("sum(sl)", "'" + this.cmid[i] + "' like '%|'+" + masterCmRelation + "+'/'+" + detailCmRelation + "+'|%'").ToString();

                                    if (dr["showzero"].ToString().Trim() == "0")
                                    {//判断是否是数值 ,且设置不显示0
                                        if (linput != "" && Convert.ToDecimal(linput) == 0) { linput = ""; }
                                    }
                                    if (!string.IsNullOrEmpty(dr["format"].ToString().Trim()) && !string.IsNullOrEmpty(linput))
                                    {//如果设置了format
                                        linput = string.Format(dr["format"].ToString().Trim(), Convert.ToDecimal(linput));
                                    }

                                    cmstr += "<td  style='width:" + Convert.ToInt32(dr["width"]) + "px' id=\"" + lid_cm + "_" + i + "\" > " + linput + "</td>";
                                    tableWidth += Convert.ToInt32(dr["width"]) + tableCSSBorderLeft + tableCSSBorderRight + tableCSSPaddingRight;
                                }
                            }
                        }
                        else
                        {//没有满足计算汇总的数据条件等
                            for (int i = 0; i < sizeCount; i++)
                            {
                                if (this.cmord[i] != null && this.cmord[i] != "")
                                {
                                    cmstr += "<td  style='width:" + Convert.ToInt32(dr["width"]) + "px' id=\"" + lid_cm + "_" + i + "\" > " + linput + "</td>";
                                    tableWidth += Convert.ToInt32(dr["width"]) + tableCSSBorderLeft + tableCSSBorderRight + tableCSSPaddingRight;
                                }
                            }
                        }

                        str += cmstr;
                        #endregion
                    }
                    else
                    {
                        #region 普通字段
                        if (dr["hj"].ToString().Trim() == "1" && totalDetailsData.Rows.Count > 0)
                        {
                            linput = totalDetailsData.Compute("sum(" + drname + ")", "").ToString();
                            if (dr["showzero"].ToString().Trim() == "0")
                            {//判断是否是数值 ,且设置不显示0
                                if (Convert.ToDecimal(linput) == 0) { linput = ""; }
                            }
                            if (!string.IsNullOrEmpty(dr["format"].ToString().Trim()) && !string.IsNullOrEmpty(linput))
                            {////如果设置了format
                                linput = string.Format(dr["format"].ToString().Trim(), Convert.ToDecimal(linput));
                            }
                        }
                        else if (dr["hj"].ToString().Trim() == "11")
                        {//合计备注字段
                            linput = "合计";
                        }
                        str += "<td  style='width:" + Convert.ToInt32(dr["width"]) + "px'" + lid + " > " + linput + "</td>";
                        tableWidth += Convert.ToInt32(dr["width"])+ tableCSSBorderLeft+tableCSSBorderRight+tableCSSPaddingRight;
                        #endregion
                    }

                }
            }
            //string kdwidth = (this.PrtFlag == "sysprt" ? "style='width: " + (this.tbWidth + this.vcols * 5).ToString().Trim() + "px' " : "");//如果是打印请固定表格的宽度
            string kdwidth = "";
            if (pagerArguments.prtFlag || pagerArguments.excelFlag)
            {
                return "<Table id='table_Hj_" + webid + "' runat='server'" + kdwidth + " class= " + "'style_head_prt'" + " > <tr>" + str + "</tr></table>";
            }
            else
            {
                return "<Table id='table_Hj_" + webid + "' runat='server'" + kdwidth + " class= " + "'style_hj'" + " > <tr>" + str + "<td  id='table_hj_td_" + webid + "_" + "nbsp' " + " >&nbsp;</td></tr></table>";
            }

        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventbz">事件源</param>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="type">类型</param>
        /// <param name="row">行号</param>
        /// <param name="myevent">返回值</param>
        protected void GetWebContentControlEvent(string eventbz, string field, string value, string type, int row, ref string myevent)
        {
            string sysmyevent = "";

            if (eventbz != string.Empty)
            {
                char[] separator = { '|' };
                string[] arr = eventbz.Split(separator);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].IndexOf("onchange=") >= 0)
                    {
                        string t = arr[i].Replace("field", "'" + field + "'").Replace("value", "'" + value + "'").Replace("row", "'" + row + "'") + " ";
                        t = t.Replace("onchange=\"", "").Replace("\"", "");
                        sysmyevent = " onchange=javascript:sysFmmyChange(id,'" + field + "');" + t + " ";
                    }
                    else
                    {
                        myevent += arr[i].Replace("field", "'" + field + "'").Replace("value", "'" + value + "'").Replace("row", "'" + row + "'") + " ";
                    }

                }
            }
            else
            {
                sysmyevent = " onchange=javascript:sysFmmyChange(id,'" + field + "');";
            }
            myevent += sysmyevent;

        }

        /// <summary>
        /// 取内容区td,与td里面的id
        /// </summary>
        /// <param name="webid"></param>
        /// <param name="index"></param>
        /// <param name="cn"></param>
        /// <param name="lid"></param>
        /// <param name="lputid"></param>
        protected void GetWebContentControlId(string webid, int index, string cn, ref string lid, ref string lputid)
        {

            lid = " id='table_td_" + webid + "_" + index + "_" + cn + "' ";
            lputid = " id='table_" + webid + "_" + index + "_" + cn + "' ";

        }

        //<table>
        //<tr><td rowspan="2">aaa111</td><td colspan="2">bbbccc</td><td rowspan="2">ddd444</td></tr>
        //<tr><td>222</td><td>333</td></tr>
        //</table>
        public string GetHeadHtml(DataTable dt_h, string webid, DataTable DataHeadCm, string mxhord)
        {
            //表头,或者合并表格上面一行
            string str = "";

            //如果合并表格,存放下面那一行
            string b_str = "";

            //标识是否为存在合并表头
            bool doubleHeadTitle = dt_h.Select("hbltname<>''").Length > 0 ? true : false;

            string hbltname = "";//存合并表头名
            int dbgs = 0;//存个数            
            int dbwidth = 0;

            foreach (DataRow dr in dt_h.Rows)
            {
                string lid = "";
                string lputid = "";
                string cmid = ""; string lputcmid = "";
                string drname = dr["ywname"].ToString().Trim();
                string drzwname = MyTy.Utils.HtmlCha(dr["zwname"].ToString().Trim());
                GetWebHeadControlId(webid, drname, ref lid, ref lputid, ref cmid, ref lputcmid);

                if (dr["visible"].ToString().Trim() == "0")
                {
                    if (pagerArguments.prtFlag || pagerArguments.excelFlag) { }
                    else
                    {
                        #region 隐藏列
                        if (!doubleHeadTitle)
                        {//如果不存在合并表头!
                            if (dr["type"].ToString().Trim() == "mx")
                            {//明细表头
                                str += this.getDataHeadCm(doubleHeadTitle, "hidden", DataHeadCm, mxhord, (dr["prtname"].ToString() == string.Empty ? drzwname : dr["prtname"].ToString().Trim()), dr["width"].ToString(), cmid, lputcmid, "");
                            }
                            else
                            {
                                str += "<td style='display:none'" + lid + "><input type='hidden'" + lputid + "value=\"" + drzwname + "\"/></td>";
                            }
                        }
                        else
                        {
                            #region
                            if (dr["hbltname"].ToString().Trim() == string.Empty)
                            {
                                str += "<td rowspan='2' style='display:none'" + lid + "><input type='hidden'" + lputid + "value=\"" + drzwname + "\"/></td>";
                                hbltname = "";
                                dbgs = 0;
                            }
                            else
                            {

                                if (dr["hbltname"].ToString().Trim() != hbltname)
                                {
                                    str += "<td colspan='2' style='display:none'" + ">" + dr["hbltname"].ToString().Trim() + "</td>";
                                    dbgs = 1;
                                }
                                else
                                {
                                    dbgs += 1;
                                    //查到最右边的colspan='?',然后加?+1
                                    HeadHbadd(ref str, dbgs, 0);
                                }

                                b_str += "<td  style='display:none'" + lid + "><input type='hidden'" + lputid + "value=\"" + drzwname + "\"/></td>";
                                hbltname = dr["hbltname"].ToString().Trim();
                            }
                            #endregion
                        }
                        #endregion
                    }
                }
                else
                {
                    #region
                    string filterEvent = Convert.ToInt32(dr["sx"]) == 1 ? " onmousedown='javascript:h_rightclk(event)' " : "";
                    string linput = ""; string pxcss = "";
                    if (pagerArguments.prtFlag || pagerArguments.excelFlag)
                    {
                        linput = drzwname;
                    }
                    else
                    {
                        //linput = "<input type='text' " + sty + " readonly='readonly' style='width:" + Convert.ToInt32(dr["width"]) + "px'" + rmc + lputid + lwid + "value=\"" + drzwname + "\"/>";
                        if (Convert.ToInt32(dr["px"]) == 1)
                        {

                            if (drname == pagerArguments.orderBy)
                            {
                                pxcss = "ion-arrow-up-b";
                            }
                            else if (drname + " desc" == pagerArguments.orderBy)
                            {
                                pxcss = "ion-arrow-down-b";
                            }
                            else
                            {
                                pxcss = "ion-stats-bars";
                            }
                            linput = "<a href=\"#\" onclick=\"javascript:mySysPx( this,'" + drname + "')\"  class='" + pxcss + "' >" + drzwname + "</a>";
                        }
                        else
                        {
                            linput = drzwname;
                        }
                    }

                    if (!doubleHeadTitle)
                    {//如果不存在合并表头!
                        if (dr["type"].ToString().Trim() == "mx")
                        {//明细表头
                            str += this.getDataHeadCm(doubleHeadTitle, "nohidden", DataHeadCm, mxhord, (dr["prtname"].ToString() == string.Empty ? drzwname : dr["prtname"].ToString().Trim()), dr["width"].ToString(), cmid, lputcmid, filterEvent);
                        }
                        else
                        {

                            str += "<td HeadName='" + (dr["prtname"].ToString() == string.Empty ? drzwname : dr["prtname"].ToString().Trim()) + "'style='width:" + Convert.ToInt32(dr["width"]) + "px'" + lid + filterEvent + " > " + linput + "</td>";
                        }
                    }
                    else
                    {
                        #region
                        if (dr["hbltname"].ToString().Trim() == string.Empty)
                        {
                            if (dr["type"].ToString().Trim() == "mx")
                            {
                                str += this.getDataHeadCm(doubleHeadTitle, "nohidden", DataHeadCm, mxhord, (dr["prtname"].ToString() == string.Empty ? drzwname : dr["prtname"].ToString().Trim()), dr["width"].ToString(), cmid, lputcmid, filterEvent);
                            }
                            else
                            {
                                str += "<td rowspan='2' HeadName='" + (dr["prtname"].ToString() == string.Empty ? drzwname : dr["prtname"].ToString().Trim()) + "' style='width:" + Convert.ToInt32(dr["width"]) + "px'" + lid + filterEvent + " > " + linput + "</td>";
                            }
                            hbltname = "";
                            dbgs = 0;
                            dbwidth = 0;
                        }
                        else
                        {
                            if (dr["hbltname"].ToString().Trim() != hbltname)
                            {
                                //注意样式不要改,,,,因为在后面增加合并列的时候用到!
                                //只能向后加!
                                //padding-right一定要结合实际的样式!,这点很致命
                                str += "<td colspan='2'  style='width:" + Convert.ToInt32(dr["width"]) + "px;padding-right:"+tableCSSPaddingRight+"px;text-align:center;' " + ">" + dr["hbltname"].ToString().Trim() + "</td>";
                                dbgs = 1;
                                dbwidth = Convert.ToInt32(dr["width"]);
                            }
                            else
                            {
                                dbgs += 1;
                                dbwidth += Convert.ToInt32(dr["width"]);
                                //查到最右边的colspan='?',然后加?+1
                                HeadHbadd(ref str, dbgs, dbwidth);
                            }

                            b_str += "<td  HeadName='" + (dr["prtname"].ToString() == string.Empty ? drzwname : dr["prtname"].ToString().Trim()) + "'  style='width:" + Convert.ToInt32(dr["width"]) + "px'" + lid + filterEvent + " > " + linput + "</td>";

                            hbltname = dr["hbltname"].ToString().Trim();
                        }
                        #endregion
                    }
                    #endregion
                }

            }

            #region
            //string kdwidth = (this.PrtFlag == "sysprt" ? "style='width: " + (this.tbWidth + this.vcols * 5).ToString().Trim() + "px' " : "");//如果是打印请固定表格的宽度
            string kdwidth = "";
            if (pagerArguments.prtFlag || pagerArguments.excelFlag)
            {
                return "<Table id='table_Header_" + webid + "' runat='server'" + kdwidth + " class=" + "'style_head_prt'" + " > <tr>" + str + "</tr>" + (doubleHeadTitle ? "<tr>" + b_str + "</tr>" : "") + "</table>";
            }
            else
            {
                if (doubleHeadTitle)
                {//有双表头
                    return "<Table id='table_Header_" + webid + "' runat='server'" + kdwidth + " class=" + "'style_head'" + " > <tr>" + str + "<td rowspan='2' id='table_header_td_" + webid + "_nbsp'" + " >&nbsp;</td></tr><tr>" + b_str + "</tr></table>";
                }
                else
                {
                    return "<Table id='table_Header_" + webid + "' runat='server'" + kdwidth + " class=" + "'style_head'" + " > <tr>" + str + "<td  id='table_header_td_" + webid + "_nbsp'" + " >&nbsp;</td></tr></table>";
                }
            }
            #endregion

        }

        /// <summary>
        /// 拼接表头上的尺码
        /// </summary>
        /// <param name="DataHeadCm"></param>
        /// <param name="mxhord"></param>
        /// <param name="HeadName"></param>
        /// <param name="width"></param>
        /// <param name="cmid"></param>
        /// <param name="rmc"></param>
        /// <param name="linput"></param>
        /// <returns></returns>
        protected string getDataHeadCm(bool doubleHeadTitle, string hidden, DataTable DataHeadCm, string mxhord, string HeadName, string width, string cmid, string lputcmid, string rmc)
        {


            List<string> cmGroupList = new List<string>();//尺码组
            int minl = 0;//最小尺码顺序号
            int maxl = 0;//最大尺码顺序号
            GetMaxCmlen(DataHeadCm, ref cmGroupList, ref minl, ref maxl);
            string[] cmHtml = new string[maxl - minl + 1];//列显示的数据 
            string[] cmHtmlInfo = new string[maxl - minl + 1];//列隐藏起来的尺码相关的数据
            #region
            //int ti = 0;//可以理解为有几个尺码组
            //string fz = "";
            //foreach (DataRow dr in DataHeadCm.Rows)
            //{
            //    //第一行进入
            //    if (fz == "") { fz = dr[mxhord].ToString(); ti = 1; }
            //    //非第一组进入
            //    if (fz != dr[mxhord].ToString()) { fz = dr[mxhord].ToString(); ti = ti + 1; }

            //    if ((tc[int.Parse(dr["ord"].ToString())] == "" || tc[int.Parse(dr["ord"].ToString())] == null) && ti != 1)
            //    {//如果第一个尺码短,第二个长
            //        for (int j = 0; j < ti - 1; j++)
            //        {
            //            tc[int.Parse(dr["ord"].ToString())] += "</br>";
            //        }
            //    }

            //    tc[int.Parse(dr["ord"].ToString())] += dr["cmmc"].ToString().Trim() + "</br>";
            //    //(DataHeadCm.Rows.IndexOf(dr) + 1 == DataHeadCm.Rows.Count ? "" : "</br>")
            //    cmzbid[int.Parse(dr["ord"].ToString())] += dr["cmzbid"].ToString().Trim() + "/" + dr["cmid"].ToString().Trim() + "|";
            //}
            #endregion

            string hxHtml = "</br>";//换行标识,EXCEL换行和HTML换行不一样
            if (pagerArguments.excelFlag)
            {
                hxHtml = "<br style='mso-data-placement:same-cell;'/> ";
            }
            for (int i = minl; i <= maxl; i++)
            {//循环每个列
                foreach (string cmGroup in cmGroupList)
                {//循环每个尺码组
                    DataRow[] dr = DataHeadCm.Select("ord=" + i + " and " + mxhord + "=" + cmGroup);
                    if (dr.Length > 0)
                    {
                        cmHtml[i - minl] += dr[0]["cmmc"].ToString().Trim();
                        cmHtmlInfo[i - minl] += dr[0]["cmzbid"].ToString().Trim() + "/" + dr[0]["cmid"].ToString().Trim() + "|";
                    }

                    if (cmGroupList.IndexOf(cmGroup) != cmGroupList.Count - 1)
                    {
                        cmHtml[i - minl] += hxHtml;
                    }
                }
            }

            string html = "";
            int ord = 0;//html中的顺序
            string rowspan = "";//如果是存在合并表头,那么需要把尺码的行样式调整
            if (doubleHeadTitle)
            {
                rowspan = " rowspan='2' ";
            }
            string heightCss = "";//如果是导EXCEL 如果有尺码那么需要把高度加上,不然导出的EXCEL高度不够
            if (!doubleHeadTitle && pagerArguments.excelFlag)
            {
                heightCss = " height:60px;";
            }
            for (int i = 0; i < maxl - minl + 1; i++)
            {
                if (cmHtml[i] != null && cmHtml[i] != "" && cmHtml[i].Replace(hxHtml, "") != "")
                {

                    this.cmord[ord] = ord.ToString();
                    this.cmid[ord] = "|" + cmHtmlInfo[i];
                    if (hidden == "hidden")
                    {
                        html += "<td " + rowspan + " cmstring=\"|" + cmHtmlInfo[i] + "\" ord=\"" + ord + "\" id=\"" + cmid + "_" + ord + "\"  style='display:none'  ><input type='hidden' id=\"" + lputcmid + "_" + ord + "\"  value=\"" + cmHtml[i] + "\"/></td>";
                    }
                    else
                    {
                        html += "<td " + rowspan + "cmstring=\"|" + cmHtmlInfo[i] + "\" ord=\"" + ord + "\" HeadName=\"" + HeadName + "\" id=\"" + cmid + "_" + ord + "\"  style='width:" + width + "px;" + heightCss + "' >" + cmHtml[i] + "</td>";
                    }
                    ord += 1;
                }
            }
            return html;

        }
       
        /// <summary>
        /// mxhord存储cmzbid
        //cmzbid cmzb    cmmc cmid  ord
        //2	    女装	155	   1	1
        //2	    女装	160	   2	2
        //2	    女装	165	   3	3
        //2	    女装	170	   4	4
        //2	    女装	175	   5	5
        //2	    女装	180	   6	6
        //2	    女装	185	   7	7
        //1	    男装	160    2	1
        //1	    男装	165	   3	2
        //1	    男装	170	   4	3
        //1	    男装	175	   5	4
        //1	    男装	180	   6	5
        //1	    男装	185	   7	6
        //1	    男装	190	   8	7
        //1	    男装	195	   9	8
        /// </summary>
        /// <param name="DataHeadCm"></param>
        /// <param name="cm_g">用来保存有几种不可的尺码组</param>
        /// <param name="minl"></param>
        /// <param name="maxl"></param>
        public void GetMaxCmlen(DataTable DataHeadCm, ref List<string> cm_g, ref int minl, ref int maxl)
        {

            foreach (DataRow dr in DataHeadCm.Rows)
            {
                if (!cm_g.Contains(dr[masterCmRelation].ToString()))
                {
                    cm_g.Add(dr[masterCmRelation].ToString());
                }
                maxl = Math.Max(maxl, int.Parse(dr["ord"].ToString()));
                minl = Math.Min(minl, int.Parse(dr["ord"].ToString()));
            }
        }

        /// <summary>
        /// 计算字符串中子串出现的次数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="substring">子串</param>
        /// <returns>出现的次数</returns>
        static int SubstringCount(string str, string substring)
        {
            if (str.Contains(substring))
            {
                string strReplaced = str.Replace(substring, "");
                return (str.Length - strReplaced.Length) / substring.Length;
            }

            return 0;
        }

        /// <summary>
        /// 查到最右边的colspan='?',然后加?+1
        /// </summary>
        /// <param name="str"></param>
        protected void HeadHbadd(ref string str, int dbgs, int dbwidth)
        {
            string tmp = "";
            if (dbgs > 2)
            {//增加次数
                int i = str.LastIndexOf("colspan=");
                int i2 = str.IndexOf("'", i + 9);

                tmp = str.Substring(0, i + 9) + Convert.ToString(int.Parse(str.Substring(i + 9, i2 - i - 9)) + 1).Trim() + str.Substring(i2).Trim();
                str = tmp;
            }
            //增加宽度
            int i_ = str.LastIndexOf("style='width:");
            int i2_ = str.IndexOf("px", i_ + 13);

            tmp = str.Substring(0, i_ + 13) + (dbwidth+dbgs*tableCSSBorderRight) + str.Substring(i2_).Trim();
            str = tmp;

            //padding样式!
            int i__ = str.LastIndexOf("padding-right:");
            int i2__ = str.IndexOf("px", i__ + 14);
            //和样式.style_head td 有关,如果,如果修改到样式,这里需要调整
            tmp = str.Substring(0, i__ + 14) + (dbgs * (tableCSSBorderRight+tableCSSPaddingRight)) + str.Substring(i2__).Trim();
            str = tmp;


        }

        /// <summary>
        /// 返回tb 和tb控件里面的ID名称
        /// </summary>
        /// <param name="webid"></param>
        /// <param name="idname"></param>
        /// <param name="lid"></param>
        /// <param name="lputid"></param>        
        protected void GetWebHeadControlId(string webid, string idname, ref string lid, ref string lputid, ref string cmid, ref string lputcmid)
        {
            lid = " id='table_header_td_" + webid + "_" + idname + "' ";
            lputid = " id='table_header_" + webid + "_" + idname + "' ";
            cmid = "table_header_td_" + webid + "_" + idname;
            lputcmid = "table_header_" + webid + "_" + idname;

        }

        /// <summary>
        /// 得到合计TD的ID 与TD内元素ID
        /// </summary>
        /// <param name="webid"></param>
        /// <param name="idname"></param>
        /// <param name="lid"></param>
        /// <param name="lputid"></param>
        protected void GetWebHjControlId(string webid, string idname, ref string lid, ref string lputid, ref string lid_cm, ref string lputid_cm)
        {
            lid = " id='table_hj_td_" + webid + "_" + idname + "' ";
            lputid = " id='table_hj_" + webid + "_" + idname + "' ";
            lid_cm = "table_hj_td_" + webid + "_" + idname + "";
            lputid_cm = "table_hj_" + webid + "_" + idname + "";

        }

        /// <summary>
        /// 把数据内容传递到客户端
        /// </summary>
        protected override void Render(HtmlTextWriter output)
        {
            if (Site != null && Site.DesignMode)
            {
                CreateChildControls();
            }
            if (!pagerArguments.prtFlag)
            {//如果打印那么不输出页码
                output.Write(OutputNavigate());
            }
            base.Render(output);
        }

        /// <summary>
        /// 输出导航
        /// </summary>
        protected virtual string OutputNavigate()
        {
            //总页码数
            int pageCount = pagerArguments.recordCount / pagerArguments.pageSize;
            if (pagerArguments.recordCount % pagerArguments.pageSize != 0)
            {
                pageCount += 1;
            }

            if (pagerArguments.currentPageIndex > pageCount)
            {
                pagerArguments.currentPageIndex = pageCount;
            }

            StringBuilder sbNavigate = new StringBuilder();
            //输出总页数、当前页
            sbNavigate.AppendFormat(navigatePageTotalFmt, pagerArguments.recordCount, pagerArguments.currentPageIndex, pageCount, pagerArguments.pageSize);



            //获取第一页、上一页
            sbNavigate.Append("<ul prtNoprint=true>");
            if (pagerArguments.currentPageIndex > 1 && pageCount > 1)
            {
                sbNavigate.AppendFormat(navigateAjaxlinkFmt, pagerArguments.pagerJs, 1, "<<", "");
                sbNavigate.AppendFormat(navigateAjaxlinkFmt, pagerArguments.pagerJs, pagerArguments.currentPageIndex - 1, "<", "");
            }
            else
            {
                sbNavigate.Append(string.Format(navigateDisableLinkFmt, "<<"));
                sbNavigate.Append(string.Format(navigateDisableLinkFmt, "<"));
            }

            //获取数字页
            int navigateCount = 6;   //每6页进行导航
            int navigateTotal = pageCount / navigateCount;  //总计能生成多少个数字导航
            int pageInNavigate = ((pagerArguments.currentPageIndex - 1) / navigateCount) + 1; //当前在第几个数字导航中

            //计算数字导航开始页序及结束页序
            int startIndex = (pageInNavigate - 1) * navigateCount + 1;     //数字导航开始页序
            int endIndex = startIndex + navigateCount - 1;   //数字导航结束页序
            if (endIndex > pageCount)
            {
                endIndex = pageCount;
            }

            string currentPageClass = "";
            for (int i = startIndex; i <= endIndex; i++)
            {
                currentPageClass = "";
                if (i == pagerArguments.currentPageIndex)
                {
                    currentPageClass = "pageactive";
                }

                sbNavigate.AppendFormat(navigateAjaxlinkFmt, pagerArguments.pagerJs, i, i, currentPageClass);
            }

            //获取下一页、最后页
            if (pagerArguments.currentPageIndex != pageCount && pageCount > 1)
            {
                sbNavigate.AppendFormat(navigateAjaxlinkFmt, pagerArguments.pagerJs, pagerArguments.currentPageIndex + 1, ">", "");
                sbNavigate.AppendFormat(navigateAjaxlinkFmt, pagerArguments.pagerJs, pageCount, ">>", "");
            }
            else
            {
                sbNavigate.AppendFormat(navigateDisableLinkFmt, ">");
                sbNavigate.AppendFormat(navigateDisableLinkFmt, ">>");
            }

            sbNavigate.Append("</ul>");


            //输出跳转到
            sbNavigate.AppendFormat(navigateGotoFmt, pagerArguments.pagerJs, pagerArguments.currentPageIndex);


            return string.Format(navigateClassFmt, sbNavigate.ToString());
        }

        /// <summary>
        /// 查找输出Repeater
        /// </summary>
        private Control FindRepeater(Control ctrl)
        {
            Control retCtrl = ctrl.FindControl(RepeaterId);
            if (retCtrl != null)
            {
                return retCtrl;
            }

            foreach (Control childCtrl in Page.Controls)
            {
                retCtrl = childCtrl.FindControl(RepeaterId);
                if (retCtrl != null)
                {
                    return retCtrl;
                }

                FindRepeater(retCtrl);
            }

            return null;
        }

        [Description("分页控件ID")]
        public string RepeaterId
        {
            get
            {
                return repeaterId;
            }
            set
            {
                repeaterId = value;
            }
        }
        private string repeaterId = "";
    }

}
