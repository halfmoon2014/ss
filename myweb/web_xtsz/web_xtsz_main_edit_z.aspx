<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_z.aspx.cs"
    Inherits="web_xtsz_web_xtsz_main_edit_z" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Service.Util" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <ctrl:DefaultHeader   id="sysHead" runat="server" />
    <link href="../css/sweetalert/sweetalert.css" rel="stylesheet" />
    <script src="../javascripts/xtsz/xtsz.js" type="text/javascript"></script>
    <script src="../javascripts/sweetalert/sweetalert.min.js"></script>
    <style type="text/css">
        tr.selectedhighlight 
        {            
            background-color: #654B24;
        }
        tr.selectedhighlight input 
        {
            color: #FF0000;
        }
        tr.selectedhighlight select
        {
            color: #FF0000;
        }
        tr.selectedhighlight checkb
        {
            color: #FF0000;
        } 
        .zdwhtb
        {
            border-left-width: 0px;
            border-collapse: collapse; /* 关键属性：合并表格内外边框(其实表格边框有2px，外面1px，里面还有1px哦) */
            border: solid #999; /* 设置边框属性；样式(solid=实线)、颜色(#999=灰) */
            border-width: 0; /* 设置边框状粗细：上 右 下 左 = 对应：1px 0 0 1px */
            table-layout: fixed;
        }
        .zdwhtb td
        {
            text-overflow: ellipsis;
            overflow: hidden;
            /*background: none repeat scroll 0 0 #E7E5DB;*/
            
            border-bottom: 2px solid #E1EBCE;
            border-top: 2px solid #E1EBCE;
            border-left: 2px solid #E1EBCE;            
            border-top: 2px solid #E1EBCE;
            
            color: #9CB085;
            font-weight: normal;
            height: 19px;
            line-height: 19px;
            padding-right: 2px;
        }
        .field_ord
        {
            width: 40px;
        }
        .field_mc
        {
            width: 80px;
        }
        .field_qwidth
        {
            width: 60px;
        }
        .field_width
        {
            width: 40px;
        }
        .field_htmlid
        {
            width: 60px;
        }
        .field_visible
        {
            width: 40px;
        }
        .field_readonly
        {
            width: 40px;
        }
        .field_type
        {
            width: 60px;
        }
        .field_event
        {
            width: 100px;
        }
        .field_yy
        {
            width: 60px;
        }
        .field_zb
        {
            width: 40px;
        }
        .field_session
        {
            width: 60px;
        }
        .field_css0
        {
            width: 60px;
        }        
        .field_css
        {
            width: 60px;
        }        
        .field_bz
        {
            width: 100px;
        }
        .field_nwebid
        {
            width: 40px;
        }
        .field_naspx
        {
            width: 80px;
        }
        .field_mrz
        {
            width: 60px;
        }
        .field_bds
        {
            width: 60px;
        }
    </style>
</head>
<body id="bodyzdwh" class="easyui-layout">
    <div region="north" style="height:200px;" border="false">
        <table id="Table1" fit="true" >
            <tr>
                <td >
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="ok" accesskey="s">保存(S)</a>
                </td>
                <td >
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="fz">复制</a>
                </td>                
                <td id="fb" runat="server">
                </td>
            </tr>            
        </table>
        <div  >  
        <textarea style="width:95%;"  rows=9>
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
    </div>
    <div region="center" border="false">
        <input type="hidden" id="wid" runat="server" />
        <input type="hidden" id="lx" runat="server" />
        <%             
            int wid = int.Parse(Request.QueryString["wid"].ToString().Trim());
            string lx = Request.QueryString["lx"].ToString().Trim();
            Business business = new Business(MySession.SessionHandle.Get("tzid"), MySession.SessionHandle.Get("userid"));
            DataTable dt = business.GetTbLayOut(wid, lx).Tables[0];
        %>
        <table id="cxtj" style="table-layout: fixed">
            <tr>
                <td field="td_westwidth" style="width: 100px">
                    Div布局-左宽
                </td>
                <td>
                    <input type="text" field="westwidth" value="<%= HtmlCha(dt.Rows.Count==0?"0":dt.Rows[0]["westwidth"].ToString()) %> " />
                </td>
                <td field="td_eastwidth" style="width: 100px">
                    Div布局-右宽
                </td>
                <td>
                    <input type="text" field="eastwidth" value="<%= HtmlCha(dt.Rows.Count==0?"0":dt.Rows[0]["eastwidth"].ToString()) %> " />
                </td>
                <td field="td_northheight" style="width: 100px">
                    Div布局-上高
                </td>
                <td>
                    <input type="text" field="northheight" value="<%= HtmlCha(dt.Rows.Count==0?"0":dt.Rows[0]["northheight"].ToString())%> " />
                </td>
                <td field="td_southheight" style="width: 100px">
                    Div布局-下高
                </td>
                <td>
                    <input type="text" field="southheight" value="<%= HtmlCha(dt.Rows.Count==0?"0":dt.Rows[0]["southheight"].ToString())%> " />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table id="zdwhtb" class="zdwhtb">
            <tr class="tbth" rownum="1">
                <td field="id" style="display: none">
                    id
                </td>
                <td field="ord">
                    排布
                </td>
                <td field="mc">
                    标题名称
                </td>
                <td field="qwidth">
                    标题宽度
                </td>
                <td field="htmlid">
                    htmlid
                </td>
                <td field="width">
                    html宽度
                </td>
                <td field="visible">
                    可见
                </td>
                <td field="readonly">
                    只读
                </td>
                <td field="type">
                    类型
                </td>
                <td field="event">
                    事件
                </td>
                <td field="yy">
                    引用
                </td>
                <td field="bds">
                    表达式
                </td>
                <td field="mrz">
                    默认值
                </td>
                <td field="zb">
                    后台字段
                </td>
                <td field="session">
                    session
                </td>
                <td field="css0">
                    css0
                </td>
                <td field="css">
                    css
                </td>
                <td field="bz">
                    备注
                </td>
                <td field="nwebid">
                    下级webid
                </td>
                <td field="naspx">
                    下级aspx
                </td>
                <td field="dwidth">
                    复-宽度
                </td>
                <td field="dheight">
                    复-高度
                </td>
                <td field="mark" style="display: none">
                    mark
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
                    <input type="text" field="id" onfocus="myselect(this)" value="<%= HtmlCha(dt.Rows[i]["id"].ToString())%>" />
                </td>
                <td>
                    <input type="text" field="ord" onfocus="myselect(this)" value="<%= HtmlCha(dt.Rows[i]["ord"].ToString())%>" />
                </td>
                <td>
                    <input type="text" field="mc" onfocus="myselect(this)"  value="<%= HtmlCha(dt.Rows[i]["mc"].ToString())%>" />
                </td>
                <td>
                    <input type="text" field="qwidth" onfocus="myselect(this)" value="<%= HtmlCha(dt.Rows[i]["qwidth"].ToString())%>" />
                </td>
                <td>
                    <input type="text" field="htmlid" onfocus="myselect(this)" value="<%= HtmlCha(dt.Rows[i]["htmlid"].ToString())%>" />
                </td>
                <td>
                    <input type="text" field="width" onfocus="myselect(this)" value="<%= dt.Rows[i]["width"].ToString()%>" />
                </td>
                <td>
                    <input field="visible" type="checkbox" onfocus="myselect(this)"  <%=(dt.Rows[i]["visible"].ToString()=="0"?"":"checked") %> />
                </td>
                <td>
                    <input field="readonly" type="checkbox"  onfocus="myselect(this)" <%=(dt.Rows[i]["readonly"].ToString()=="0"?"":"checked") %> />
                </td>
                <td>
                    <select onfocus="myselect(this)" field="type" mrz="">
                        <option value="text" <%=(dt.Rows[i]["type"].ToString()=="text"   ?"selected":"") %> >text</option>
                        <option value="select" <%=(dt.Rows[i]["type"].ToString()=="select"?"selected":"") %>>select</option>
                        <option value="button" <%=(dt.Rows[i]["type"].ToString()=="button"?"selected":"") %>>button</option>
                        <option value="checkbox" <%=(dt.Rows[i]["type"].ToString()=="checkbox"?"selected":"") %>>checkbox</option>
                        <option value="textarea" <%=(dt.Rows[i]["type"].ToString()=="textarea"?"selected":"") %>>textarea</option>
                        <option value="td" <%=(dt.Rows[i]["type"].ToString()=="td"?"selected":"") %>>td</option>
                        <option value="a" <%=(dt.Rows[i]["type"].ToString()=="a"?"selected":"") %>>a</option>
                        <option value="date" <%=(dt.Rows[i]["type"].ToString()=="date"?"selected":"") %>>date</option>
                        <option value="tree" <%=(dt.Rows[i]["type"].ToString()=="tree"?"selected":"") %>>tree</option>
                        <option value="" <% =dt.Rows[i]["type"].ToString()==string.Empty ?"selected":"" %>>
                        </option>
                    </select>
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="event" value="<%= HtmlCha(dt.Rows[i]["event"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)"  field="yy" value="<%= HtmlCha(dt.Rows[i]["yy"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)"  field="bds" value="<%= HtmlCha(dt.Rows[i]["bds"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="mrz" value="<%= HtmlCha(dt.Rows[i]["mrz"].ToString())%>" />
                </td>
                <td>
                    <input field="zb" onfocus="myselect(this)" type="checkbox" <%=(dt.Rows[i]["zb"].ToString()=="0"?"":"checked") %> />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="session" value="<%= HtmlCha(dt.Rows[i]["session"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="css0" value="<%= HtmlCha(dt.Rows[i]["css0"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="css" value="<%= HtmlCha(dt.Rows[i]["css"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="bz" value="<%= HtmlCha(dt.Rows[i]["bz"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="nwebid" value="<%= dt.Rows[i]["nwebid"].ToString()%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="naspx" value="<%= HtmlCha(dt.Rows[i]["naspx"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="dwidth" value="<%= dt.Rows[i]["dwidth"].ToString()%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="dheight" value="<%= HtmlCha(dt.Rows[i]["dheight"].ToString())%>" />
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
                    <input type="text" onfocus="myselect(this)" field="ord" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="mc" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="qwidth" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="htmlid" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="width" value="" />
                </td>
                <td>
                    <input field="visible" onfocus="myselect(this)" type="checkbox" />
                </td>
                <td>
                    <input field="readonly" onfocus="myselect(this)" type="checkbox" />
                </td>
                <td>
                    <select field="type" mrz="" onfocus="myselect(this)" >
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
                    <input type="text" onfocus="myselect(this)" field="event" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="yy" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="bds" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="mrz" value="" />
                </td>
                <td>
                    <input type="checkbox" onfocus="myselect(this)" field="zb"  />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="session" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="css0" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="css" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="bz" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="nwebid" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="naspx" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="dwidth" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="dheight" value="" />
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
    
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        $("#ok").bind("click", function () { ok_click(); });
        $("#fz").bind("click", function () { fz_click(); });
        $("#fb").bind("click", function () { fb_click(); });
        $("[field]").each(function (i, n) {
            var f = "field_"+$(n).attr("field");
            $(n).addClass(f);
        });
        if (document.getElementById("lx").value == "z") {
            //布局面板
            var head = document.getElementById("zdwhtb").rows;
            for (var d = 0; d < head[0].cells.length; d++) {
                if (head[0].cells[d].innerHTML.indexOf("排布") >= 0 || head[0].cells[d].innerHTML.indexOf("下级webid") >= 0 || head[0].cells[d].innerHTML.indexOf(" 下级aspx") >= 0 || head[0].cells[d].innerHTML.indexOf("复-宽度") >= 0 || head[0].cells[d].innerHTML.indexOf("复-高度") >= 0 || head[0].cells[d].innerHTML.indexOf("htmlid") >= 0) { }
                else {
                    head[0].cells[d].style.display = "none";
                    try {
                        for (var m = 1; m <= getRowNum(); m++) {
                            head[m].cells[d].style.display = "none";
                        }
                    } catch (e) { }
                }
            }
            $("#cxtj").attr("style", "display:none");            
        } else {
            //div布局
            var head = document.getElementById("zdwhtb").rows;
            for (var d = 0; d < head[0].cells.length; d++) {
                if (head[0].cells[d].innerHTML.indexOf("复-高度") >= 0 || head[0].cells[d].innerHTML.indexOf(" 复-宽度") >= 0) {
                    head[0].cells[d].style.display = "none";
                    try {
                        for (var m = 1; m <= getRowNum(); m++) {
                            head[m].cells[d].style.display = "none";
                        }
                    } catch (e) { }
                }
            }
            if (document.getElementById("lx").value == "c") {//中间面板不需要设定高宽
                $("[field='southheight']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='northheight']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='eastwidth']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='westwidth']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='td_southheight']", $("#cxtj")).attr("style", "display:none");
                $("[field='td_northheight']", $("#cxtj")).attr("style", "display:none");
                $("[field='td_eastwidth']", $("#cxtj")).attr("style", "display:none");
                $("[field='td_westwidth']", $("#cxtj")).attr("style", "display:none");
            } else if (document.getElementById("lx").value == "t") {
                $("[field='southheight']", $("#cxtj")).parent().attr("style", "display:none");                
                $("[field='eastwidth']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='westwidth']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='td_southheight']", $("#cxtj")).attr("style", "display:none");                
                $("[field='td_eastwidth']", $("#cxtj")).attr("style", "display:none");
                $("[field='td_westwidth']", $("#cxtj")).attr("style", "display:none");
            } else if (document.getElementById("lx").value == "b") {
                
                $("[field='northheight']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='eastwidth']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='westwidth']", $("#cxtj")).parent().attr("style", "display:none");
                
                $("[field='td_northheight']", $("#cxtj")).attr("style", "display:none");
                $("[field='td_eastwidth']", $("#cxtj")).attr("style", "display:none");
                $("[field='td_westwidth']", $("#cxtj")).attr("style", "display:none");
            } else if (document.getElementById("lx").value == "l") {
                $("[field='southheight']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='northheight']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='eastwidth']", $("#cxtj")).parent().attr("style", "display:none");
                
                $("[field='td_southheight']", $("#cxtj")).attr("style", "display:none");
                $("[field='td_northheight']", $("#cxtj")).attr("style", "display:none");
                $("[field='td_eastwidth']", $("#cxtj")).attr("style", "display:none");
            } else if (document.getElementById("lx").value == "r") {
                $("[field='southheight']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='northheight']", $("#cxtj")).parent().attr("style", "display:none");
                
                $("[field='westwidth']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='td_southheight']", $("#cxtj")).attr("style", "display:none");
                $("[field='td_northheight']", $("#cxtj")).attr("style", "display:none");
                
                $("[field='td_westwidth']", $("#cxtj")).attr("style", "display:none");
            }
        }
    });
    function salert(title, text, type, fn) {
        swal({
            title: title,
            text: text,
            type: type,
        }, fn);
    }
    function sr(s, i) {
        return $.trim(myVale(s, i).val().replace(/'/g, "''"));
    }
    //保存
    function ok_click() {
        $('#ok').linkbutton('disable');
        var wid = document.getElementById("wid").value;
        var lx = document.getElementById("lx").value;
        var str = "";
        var css0,css, mrz, bds, ord, width, qwidth, id, mc, visible, htmlid, westwidth, eastwidth, northheight, southheight, dwidth, dheight, readonly, type, bz, nwebid, naspx, event, session, zb, yy;
        westwidth = $.trim(myVale("westwidth").val());
        if (westwidth == "") { westwidth = "0"; }
        eastwidth = $.trim(myVale("eastwidth").val());
        if (eastwidth == "") { eastwidth = "0"; }
        southheight = $.trim(myVale("southheight").val());
        if (southheight == "") { southheight = "0"; }
        northheight = $.trim(myVale("northheight").val());
        if (northheight == "") { northheight = "0"; }
        var data = {};        
        data.row = new Array();
        for (var i = 0; i < getRowNum(); i++) {            
            ord = $.trim(myVale("ord", i).val());
            if (ord == "") { ord = "0"; }
            nwebid = $.trim(myVale("nwebid", i).val());
            if (nwebid == "") { nwebid = "0"; }
            width = $.trim(myVale("width", i).val());
            if (width == "") { width = "0"; }
            qwidth = $.trim(myVale("qwidth", i).val());
            if (qwidth == "") { qwidth = "0"; }
            dwidth = $.trim(myVale("dwidth", i).val());
            if (dwidth == "") { dwidth = "0"; }
            dheight = $.trim(myVale("dheight", i).val());
            if (dheight == "") { dheight = "0"; }
            id = $.trim(myVale("id", i).val());
			if(id==""){id="0";}
			visible = $.trim(myVale("visible", i).is(':checked') ? "1" : "0");
			readonly = $.trim(myVale("readonly", i).is(':checked') ? "1" : "0");
            type = $.trim(myVale("type", i).val());
            bz = sr("bz", i);
            css = sr("css", i);
            css0 = sr("css0", i);
            mc = $.trim(myVale("mc", i).val());
            event = sr("event", i);
            session = sr("session", i);
            if (session.toLowerCase() == "userid" || session.toLowerCase() == "tzid" || session.toLowerCase() == "username") {                
                alert('session 不能是 [userid]或[tzid]或[username] ');
                $('#ok').linkbutton('enable');
                return false;
            }
            zb = $.trim(myVale("zb", i).is(':checked') ? "1" : "0");
            yy = sr("yy", i)
            naspx = sr("naspx", i);
            htmlid = $.trim(myVale("htmlid", i).val());
            if (id == 0 && htmlid.length == 0) { continue;}
            mrz = sr("mrz", i);
            bds = sr("bds", i);
            var dataRow = {};
            dataRow.id = id;
            dataRow.css0 = css0;
            dataRow.css = css;
            dataRow.mrz = mrz;
            dataRow.bds = bds;
            dataRow.webid=wid;
            dataRow.lx=lx;
            dataRow.mc = mc;
            dataRow.ord = ord;
            dataRow.width = width;
            dataRow.qwidth = qwidth;
            dataRow.westwidth = westwidth;
            dataRow.eastwidth = eastwidth;
            dataRow.northheight = northheight;
            dataRow.southheight = southheight;
            dataRow.dwidth = dwidth;
            dataRow.dheight = dheight;
            dataRow.visible = visible;
            dataRow.readonly = readonly;
            dataRow.type = type;
            dataRow.bz = bz;
            dataRow.nwebid = nwebid;
            dataRow.event = event;
            dataRow.yy = yy;
            dataRow.zb = zb;
            dataRow.session = session;
            dataRow.naspx = naspx;
            dataRow.htmlid = htmlid;
            data.row.push(dataRow);
            //if (id.length == 0 || id == "0") {
            //    if (ord == "0" && qwidth == "0" && mc == "") { }
            //    else {
            //        str += " insert v_wid_layout ( css0,css, mrz,bds,webid,lx, mc,ord, width,qwidth, westwidth,eastwidth, northheight, southheight, dwidth, dheight, visible, readonly, type,   bz,nwebid, event, yy,zb,session,naspx,htmlid) ";
            //        str += "values('"+css0+"','"+css+"','" + mrz + "','" + bds + "','" + wid + "','" + lx + "', '" + mc + "',  '" + ord + "', '" + width + "','" + qwidth + "','" + westwidth + "','" + eastwidth + "', '" + northheight + "', '" + southheight + "', '" + dwidth + "', '" + dheight + "' ,'" + visible + "', '" + readonly + "', '" + type + "', '" + bz + "','" + nwebid + "','" + event + "','" + yy + "','" + zb + "','" + session + "','" + naspx + "','" + htmlid + "');";
            //    }
            //} else {
            //    if (ord == "0" && qwidth == "0" && mc == "") {
            //        str += " delete v_wid_layout  where id='" + id + "'; ";
            //    } else {
            //        str += " update v_wid_layout  set css0='"+css0+"', css='"+css+"',mrz='" + mrz + "',bds='" + bds + "',mc='" + mc + "', htmlid='" + htmlid + "',ord='" + ord + "', width='" + width + "',qwidth='" + qwidth + "',westwidth='" + westwidth + "',eastwidth='" + eastwidth + "', northheight='" + northheight + "',southheight='" + southheight + "' ,dwidth='" + dwidth + "', dheight='" + dheight + "', visible='" + visible + "', readonly='" + readonly + "', type='" + type + "',   bz='" + bz + "',nwebid='" + nwebid + "',  event='" + event + "', yy='" + yy + "',zb='" + zb + "',session='" + session + "',naspx='" + naspx + "'   where id='" + id + "'; ";
            //    }
            //}
        }
        if (data.row.length == 0) {
            salert('提示信息', '没有可更新的记录!', 'info', function () {
                $('#ok').linkbutton('enable');
            });
        } else {
            //alert(str);
            //return false;
            //var r = myAjax(str);
            //if (r == -1) {
            //    $.messager.alert('提示信息', '连接失败!', 'info', function () {
            //        $('#ok').linkbutton('enable');
            //    });
            //} else {
            //    if (r.r == 'true') {
            //        $.messager.alert('提示信息', '保存成功!', 'info', function () {
            //            $('#ok').linkbutton('enable');
            //            parent.closeTab("refresh", false);
            //        });
            //    } else {
            //        $.messager.alert('提示信息', r.msg, 'info', function () {
            //            $('#ok').linkbutton('enable');
            //        });
            //    }
            //}
            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/UpSYJLayout',
                data: { wid: wid, data: JSON.stringify(data) },
                error: function (e) {
                    salert('提示信息', '连接失败!', 'info', function () {
                        $('#ok').linkbutton('enable');
                    });
                },
                success: function (data) {
                    var r = myAjaxData(data);
                    if (r.r == 'true') {
                        salert('提示信息', '保存成功!', 'info', function () {
                            $('#ok').linkbutton('enable'); location.reload();
                        });
                    } else {
                        salert('提示信息', '保存失败!', 'info', function () {
                            $('#ok').linkbutton('enable');
                        });
                    }
                }
            })
        }
    }
    //行得到焦点,变色
    function myselect(obj) {
        //alert(g);
        var g = $(obj.parentNode.parentNode).attr("rownum");
        $.each($(".tbbody"), function (i, r) {
            if (i != g) {
                $(r).removeClass("selectedhighlight");
            } else {
                $(r).addClass("selectedhighlight");
            }
        });
        //$("#zdwhtb tr").eq(i-1)
    }
    //fz
    function fz_click() {
      
        swal({
            title: "Copy",
            text: "输入wid",
            type: "input",
            showCancelButton: true,
            closeOnConfirm: false,
            inputPlaceholder: ""
        }, function (inputValue) {
            if (inputValue != "") {
                rFZ(inputValue)
            }
        });
    }
    function rFZ(oldwid) {
        var newwid = document.getElementById("wid").value;
        var lx = document.getElementById("lx").value;
        if (!isNaN(oldwid) && oldwid != "0" && oldwid != "") {
            $.ajax({ type: 'post',
                url: '../webuser/ws.asmx/websj_fz_zd',
                data: { wid: oldwid, newwid: newwid, bs: lx },
                error: function (e) {
                    salert('提示信息', '连接失败!', 'info');
                },
                success: function (data) {
                    var r = myAjaxData(data)
                    if (r.r == 'true') {
                        salert('提示信息', '复制成功!', 'info', function () { parent.closeTab("refresh", false); });
                    } else {
                        salert('提示信息', '复制失败!', 'info');
                    }
                }
            })
        } else {
            salert('提示信息', '复制wid无效!', 'info');
        }
    }
</script>