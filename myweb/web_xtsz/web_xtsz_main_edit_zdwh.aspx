<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_zdwh.aspx.cs" Inherits="web_xtsz_web_xtsz_main_edit_zdwh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="MyTy" %>
<%@ Import Namespace="Service.Util" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" />
    <!-- Libraries -->
    <link href="../css/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <link href="../css/bootstrap/userplatform/sticky-footer-navbar.css" rel="stylesheet" />
    <!-- End of Libraries -->
    <link href="../css/sweetalert/sweetalert.css" rel="stylesheet" />
    <link href="css/web_xtsz_main_edit_zdwh.css" rel="stylesheet" />
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
                      <button type="button" class="btn btn-default" id="fz"  aria-label="复制" >复制</button>
                      <button type="button" class="btn btn-default" id="fb" runat="server" aria-label="发布">发布</button>                      
                    </div>
                </form>  
            </div><!--/.nav-collapse -->
        </div>
    </nav>

    <div class="container-fluid">
        <ul>
            <li>筛选,打印名称 功能还没有完成;</li>
            <li>合计=11会显示合计二字</li>
            <li>bz现在只用于作为select数据源</li>
            <li>显示默认日期只判断日期年份是否等于1900</li>
        </ul>
        <div class="table-responsive">
            <table id="zdwhtb" class="table table-bordered">
                <tr class="tbth" rownum="1">
                    <td field="id" style="display: none">id
                    </td>
                    <td field="ywname">字段
                    </td>
                    <td field="zwname">名称
                    </td>
                    <td field="ord">顺序
                    </td>
                    <td field="width">宽度
                    </td>
                    <td field="visible">显示
                    </td>
                    <td field="readonly">只读
                    </td>
                    <td field="type">类型
                    </td>
                    <td field="sx">筛选
                    </td>
                    <td field="bz">备注
                    </td>
                    <td field="showzero">显示数字0
                    </td>
                    <td field="event">事件
                    </td>
                    <td field="btnvalue">按钮名称
                    </td>
                    <td field="showmrrq">显示默认日期
                    </td>
                    <td field="hj">合计
                    </td>
                    <td field="hbltname">合并列头名称
                    </td>
                    <td field="px">排序
                    </td>
                    <td field="format">format
                    </td>
                    <td field="prtname">打印名称
                    </td>
                    <td field="mark" style="display: none">mark
                    </td>
                </tr>
                <%                
                int wid = int.Parse(Request.QueryString["wid"].ToString().Trim());
                Business business = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
                DataTable dt = business.GetTbzd(wid).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                %>
                <tr class="tbbody" rownum="<%=i%>">
                    <td style="display: none">
                        <input type="text"  field="id" value="<%= dt.Rows[i]["id"].ToString()%>" />
                    </td>
                    <td>
                        <input type="text"  field="ywname" value="<%=HtmlCha(dt.Rows[i]["ywname"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="zwname" value="<%= HtmlCha(dt.Rows[i]["zwname"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="ord" value="<%= HtmlCha(dt.Rows[i]["ord"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="width" value="<%= HtmlCha(dt.Rows[i]["width"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="visible" value="<%= HtmlCha(dt.Rows[i]["visible"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="readonly" value="<%= HtmlCha(dt.Rows[i]["readonly"].ToString())%>" />
                    </td>
                    <td>
                        <select  field="type" mrz="">
                            <option value="text" <%=(dt.Rows[i]["type"].ToString()=="text"  ?"selected":"") %>>text</option>
                            <option value="select" <%=(dt.Rows[i]["type"].ToString()=="select"?"selected":"") %>>select</option>
                            <option value="button" <%=(dt.Rows[i]["type"].ToString()=="button"?"selected":"") %>>button</option>
                            <option value="checkbox" <%=(dt.Rows[i]["type"].ToString()=="checkbox"?"selected":"") %>>checkbox</option>
                            <option value="textarea" <%=(dt.Rows[i]["type"].ToString()=="textarea"?"selected":"") %>>textarea</option>
                            <option value="td" <%=(dt.Rows[i]["type"].ToString()=="td"?"selected":"") %>>td</option>
                            <option value="a" <%=(dt.Rows[i]["type"].ToString()=="a"?"selected":"") %>>a</option>
                            <option value="img" <%=(dt.Rows[i]["type"].ToString()=="img"?"selected":"") %>>img</option>
                            <option value="mx" <% =dt.Rows[i]["type"].ToString()=="mx" ?"selected":"" %>>mx</option>
                            <option value="" <% =dt.Rows[i]["type"].ToString()==string.Empty ?"selected":"" %>></option>
                        </select>
                    </td>
                    <td>
                        <input type="text"  field="sx" value="<%= HtmlCha(dt.Rows[i]["sx"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="bz" value="<%= HtmlCha(dt.Rows[i]["bz"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="showzero" value="<%= HtmlCha(dt.Rows[i]["showzero"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="event" value="<%= HtmlCha(dt.Rows[i]["event"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="btnvalue" value="<%= HtmlCha(dt.Rows[i]["btnvalue"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="showmrrq" value="<%= HtmlCha(dt.Rows[i]["showmrrq"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="hj" value="<%= HtmlCha(dt.Rows[i]["hj"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="hbltname" value="<%= HtmlCha(dt.Rows[i]["hbltname"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="px" value="<%= HtmlCha(dt.Rows[i]["px"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="format" value="<%= HtmlCha(dt.Rows[i]["format"].ToString())%>" />
                    </td>
                    <td>
                        <input type="text"  field="prtname" value="<%= HtmlCha(dt.Rows[i]["prtname"].ToString())%>" />
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
                        <input type="text"  field="ywname" value="" />
                    </td>
                    <td>
                        <input type="text"  field="zwname" value="" />
                    </td>
                    <td>
                        <input type="text"  field="ord" value="" />
                    </td>
                    <td>
                        <input type="text"  field="width" value="" />
                    </td>
                    <td>
                        <input type="text"  field="visible" value="" />
                    </td>
                    <td>
                        <input type="text"  field="readonly" value="" />
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
                            <option value="img">img</option>
                            <option value="mx">mx</option>
                            <option value="" selected></option>
                        </select>
                    </td>
                    <td>
                        <input type="text"  field="sx" value="" />
                    </td>
                    <td>
                        <input type="text"  field="bz" value="" />
                    </td>
                    <td>
                        <input type="text"  field="showzero" value="" />
                    </td>
                    <td>
                        <input type="text"  field="event" value="" />
                    </td>
                    <td>
                        <input type="text"  field="btnvalue" value="" />
                    </td>
                    <td>
                        <input type="text"  field="showmrrq" value="" />
                    </td>
                    <td>
                        <input type="text"  field="hj" value="" />
                    </td>
                    <td>
                        <input type="text"  field="hbltname" value="" />
                    </td>
                    <td>
                        <input type="text"  field="px" value="" />
                    </td>
                    <td>
                        <input type="text"  field="format" value="" />
                    </td>
                    <td>
                        <input type="text"  field="prtname" value="" />
                    </td>
                    <td style="display: none">
                        <input field="mark" type="text" />
                    </td>
                </tr>
                <%}%>
            </table>
        </div>

    </div>
    <!-- /container -->
    <input type="hidden" id="wid" runat="server" />
</body>
<script src="../javascripts/bootstrap/ie10-viewport-bug-workaround.js"></script>
<script src="../javascripts/bootstrap/3.3.7/bootstrap.min.js"></script>
<script src="../javascripts/sweetalert/sweetalert.min.js"></script>
<script data-main="js/web_xtsz_main_edit_zdwh" src="../javascripts/require.js"></script>
</html>

