<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_z.aspx.cs"
    Inherits="web_xtsz_web_xtsz_main_edit_z" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Service.Util" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />
    <!-- Libraries -->
    <link href="../css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <link href="css/sticky-footer-navbar.css" rel="stylesheet" />

    <script src="../javascripts/bootstrap/ie-emulation-modes-warning.js"></script>
    <!--[if lt IE 9]>
        <script src="../javascripts/bootstrap/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="../javascripts/bootstrap/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- End of Libraries -->

    <link href="../css/sweetalert/sweetalert.css" rel="stylesheet" />
    <link href="css/web_xtsz_main_edit_z.css" rel="stylesheet" />
</head>
<body id="bodyzdwh">
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#zdnavbar" aria-expanded="false" aria-controls="navbar">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#"></a>
            </div>
            <div id="zdnavbar" class="navbar-collapse collapse">  
                <form class="navbar-form navbar-left">
                    <div class="btn-group" role="toolbar" id="btnGroup" runat="server" aria-label="操作按钮">
                      <button type="button" class="btn btn-default" id="ok" accesskey="s" aria-label="保存" >保存(S)</button>
                      <button type="button" class="btn btn-default" id="showtitp"  aria-label="提示" >提示</button>
                      <button type="button" class="btn btn-default" id="fz"  aria-label="复制" >复制</button>
                      <button type="button" class="btn btn-default" id="fb" runat="server" aria-label="发布">发布</button>                      
                    </div>
                </form>  
            </div><!--/.nav-collapse -->
        </div>
    </nav>
    <div class="container-fluid">
        <div class="form-group" id="formts" style="display: none">
            <label for="ts">提示</label>
            <textarea style="width: 100%;" id="textarea1" rows="1">
            button类型的宽度取html宽度;css0样式指定[标题名称];css样式指定[htmlid]             
            session字典 处理顺序
            1.URL
            2.默认值
                A.带select时,先将@key用URL夫换
                B.固定项@tzid @username @userid            
            3.替换主表字段中的@key
            
            在进行构造表头时取值
            1.主表字段
                A.主表有记录
                B.其它取默认值
            2.默认值
            默认值=session字段存在于session字典中
                    =默认字段存在于session字典中
                    =select 语句(@key可被session字典替换)
                    =某个值 
            [引用][表达式]用于ajax请求数据的时候,也只作用于此
        </textarea>
        </div>
        

        <%             
            int wid = int.Parse(Request.QueryString["wid"].ToString().Trim());
            string lx = Request.QueryString["lx"].ToString().Trim();
            Business business = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
            DataTable dt = business.GetTbLayOut(wid, lx).Tables[0];
        %>
        <div class="table-responsive">
        <table id="cxtj" class="  table table-bordered" style="table-layout: fixed">
            <tr>
                <td field="td_westwidth" style="width: 100px">Div布局-左宽
                </td>
                <td>
                    <input type="text" field="westwidth" value="<%= HtmlCha(dt.Rows.Count==0?"0":dt.Rows[0]["westwidth"].ToString()) %> " />
                </td>
                <td field="td_eastwidth" style="width: 100px">Div布局-右宽
                </td>
                <td>
                    <input type="text" field="eastwidth" value="<%= HtmlCha(dt.Rows.Count==0?"0":dt.Rows[0]["eastwidth"].ToString()) %> " />
                </td>
                <td field="td_northheight" style="width: 100px">Div布局-上高
                </td>
                <td>
                    <input type="text" field="northheight" value="<%= HtmlCha(dt.Rows.Count==0?"0":dt.Rows[0]["northheight"].ToString())%> " />
                </td>
                <td field="td_southheight" style="width: 100px">Div布局-下高
                </td>
                <td>
                    <input type="text" field="southheight" value="<%= HtmlCha(dt.Rows.Count==0?"0":dt.Rows[0]["southheight"].ToString())%> " />
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
        </div>
        <div class="table-responsive">
            <table id="zdwhtb" class="  table table-bordered">
                <tr class="tbth" rownum="1">
                    <td field="id" style="display: none">id
                    </td>
                    <td field="ord">排布
                    </td>
                    <td field="mc">标题名称
                    </td>
                    <td field="qwidth">标题宽度
                    </td>
                    <td field="htmlid">htmlid
                    </td>
                    <td field="width">html宽度
                    </td>
                    <td field="visible">可见
                    </td>
                    <td field="readonly">只读
                    </td>
                    <td field="type">类型
                    </td>
                    <td field="event">事件
                    </td>
                    <td field="yy">引用
                    </td>
                    <td field="bds">表达式
                    </td>
                    <td field="mrz">默认值
                    </td>
                    <td field="zb">后台字段
                    </td>
                    <td field="session">session
                    </td>
                    <td field="css0">css0
                    </td>
                    <td field="css">css
                    </td>
                    <td field="bz">备注
                    </td>
                    <td field="nwebid">下级webid
                    </td>
                    <td field="naspx">下级aspx
                    </td>
                    <td field="dwidth">复-宽度
                    </td>
                    <td field="dheight">复-高度
                    </td>
                    <td field="mark" style="display: none">mark
                    </td>
                </tr>
                <%                
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                %>
                <tr class="tbbody" rownum="<%=i%>">
                    <td style="display: none">
                        <input type="text" field="id"  value="<%= HtmlCha(dt.Rows[i]["id"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text" field="ord"  value="<%= HtmlCha(dt.Rows[i]["ord"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text" field="mc"  value="<%= HtmlCha(dt.Rows[i]["mc"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text" field="qwidth"  value="<%= HtmlCha(dt.Rows[i]["qwidth"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text" field="htmlid"  value="<%= HtmlCha(dt.Rows[i]["htmlid"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text" field="width"  value="<%= dt.Rows[i]["width"].ToString()%>" />
                    </td>
                    <td>
                        <input field="visible" type="checkbox"  <%=(dt.Rows[i]["visible"].ToString()=="0"?"":"checked") %> />
                    </td>
                    <td>
                        <input field="readonly" type="checkbox"  <%=(dt.Rows[i]["readonly"].ToString()=="0"?"":"checked") %> />
                    </td>
                    <td>
                        <select  field="type" mrz="">
                            <option value="text" <%=(dt.Rows[i]["type"].ToString()=="text"   ?"selected":"") %>>text</option>
                            <option value="select" <%=(dt.Rows[i]["type"].ToString()=="select"?"selected":"") %>>select</option>
                            <option value="button" <%=(dt.Rows[i]["type"].ToString()=="button"?"selected":"") %>>button</option>
                            <option value="checkbox" <%=(dt.Rows[i]["type"].ToString()=="checkbox"?"selected":"") %>>checkbox</option>
                            <option value="textarea" <%=(dt.Rows[i]["type"].ToString()=="textarea"?"selected":"") %>>textarea</option>
                            <option value="td" <%=(dt.Rows[i]["type"].ToString()=="td"?"selected":"") %>>td</option>
                            <option value="a" <%=(dt.Rows[i]["type"].ToString()=="a"?"selected":"") %>>a</option>
                            <option value="date" <%=(dt.Rows[i]["type"].ToString()=="date"?"selected":"") %>>date</option>
                            <option value="tree" <%=(dt.Rows[i]["type"].ToString()=="tree"?"selected":"") %>>tree</option>
                            <option value="" <% =dt.Rows[i]["type"].ToString()==string.Empty ?"selected":"" %>></option>
                        </select>
                    </td>
                    <td>
                        <input type="text"  field="event" value="<%= HtmlCha(dt.Rows[i]["event"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="yy" value="<%= HtmlCha(dt.Rows[i]["yy"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="bds" value="<%= HtmlCha(dt.Rows[i]["bds"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="mrz" value="<%= HtmlCha(dt.Rows[i]["mrz"].ToString())%>" />
                    </td>
                    <td>
                        <input field="zb"  type="checkbox" <%=(dt.Rows[i]["zb"].ToString()=="0"?"":"checked") %> />
                    </td>
                    <td>
                        <input type="text"  field="session" value="<%= HtmlCha(dt.Rows[i]["session"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="css0" value="<%= HtmlCha(dt.Rows[i]["css0"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="css" value="<%= HtmlCha(dt.Rows[i]["css"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="bz" value="<%= HtmlCha(dt.Rows[i]["bz"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="nwebid" value="<%= dt.Rows[i]["nwebid"].ToString()%>" />
                    </td>
                    <td>
                        <input type="text"  field="naspx" value="<%= HtmlCha(dt.Rows[i]["naspx"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="dwidth" value="<%= dt.Rows[i]["dwidth"].ToString()%>" />
                    </td>
                    <td>
                        <input type="text"  field="dheight" value="<%= HtmlCha(dt.Rows[i]["dheight"].ToString())%>" />
                    </td>
                    <td style="display: none">
                        <input field="mark" type="text" />
                    </td>
                </tr>
                <%
                        }
                    }
                    else
                    {
                %>
                <tr class="tbbody" rownum="0">
                    <td style="display: none">
                        <input type="text" field="id" value="" />
                    </td>
                    <td>
                        <input type="text"  field="ord" value="" />
                    </td>
                    <td>
                        <input type="text"  field="mc" value="" />
                    </td>
                    <td>
                        <input type="text"  field="qwidth" value="" />
                    </td>
                    <td>
                        <input type="text"  field="htmlid" value="" />
                    </td>
                    <td>
                        <input type="text"  field="width" value="" />
                    </td>
                    <td>
                        <input field="visible"  type="checkbox" />
                    </td>
                    <td>
                        <input field="readonly"  type="checkbox" />
                    </td>
                    <td>
                        <select field="type" mrz="" >
                            <option value="text">text</option>
                            <option value="select">select</option>
                            <option value="button">button</option>
                            <option value="checkbox">checkbox</option>
                            <option value="textarea">textarea</option>
                            <option value="td">td</option>
                            <option value="a">a</option>
                            <option value="date">date</option>
                            <option value="tree">tree</option>
                            <option value="" selected></option>
                        </select>
                    </td>
                    <td>
                        <input type="text"  field="event" value="" />
                    </td>
                    <td>
                        <input type="text"  field="yy" value="" />
                    </td>
                    <td>
                        <input type="text"  field="bds" value="" />
                    </td>
                    <td>
                        <input type="text"  field="mrz" value="" />
                    </td>
                    <td>
                        <input type="checkbox"  field="zb" />
                    </td>
                    <td>
                        <input type="text"  field="session" value="" />
                    </td>
                    <td>
                        <input type="text"  field="css0" value="" />
                    </td>
                    <td>
                        <input type="text"  field="css" value="" />
                    </td>
                    <td>
                        <input type="text"  field="bz" value="" />
                    </td>
                    <td>
                        <input type="text"  field="nwebid" value="" />
                    </td>
                    <td>
                        <input type="text"  field="naspx" value="" />
                    </td>
                    <td>
                        <input type="text"  field="dwidth" value="" />
                    </td>
                    <td>
                        <input type="text"  field="dheight" value="" />
                    </td>
                    <td style="display: none">
                        <input field="mark" type="text" />
                    </td>
                </tr>
                <%
                    }
                %>
            </table>
        </div>
    </div>
    <input type="hidden" id="wid" runat="server" />
    <input type="hidden" id="lx" runat="server" />
</body>
<script src="../javascripts/bootstrap/ie10-viewport-bug-workaround.js"></script>
<script src="../javascripts/bootstrap/3.3.7/bootstrap.min.js"></script>
<script src="../javascripts/sweetalert/sweetalert.min.js"></script>
<script data-main="js/web_xtsz_main_edit_z" src="../javascripts/require.js"></script>
</html>
