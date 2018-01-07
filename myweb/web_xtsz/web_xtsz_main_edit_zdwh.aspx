<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_zdwh.aspx.cs" Inherits="web_xtsz_web_xtsz_main_edit_zdwh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="MyTy" %>
<%@ Import Namespace="Service.Util" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <ctrl:DefaultHeader ID="sysHead" runat="server" />
    <script src="../javascripts/utils.js" type="text/javascript"></script>
    <script src="../javascripts/xtsz/xtsz.js" type="text/javascript"></script>
    
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
        .ord{ width:40px; }
        .width{width:40px;}
        .visible{width:40px;}
        .readonly{width:40px;}
        .sx{width:40px;}
        .showzero{width:40px;}
        .showmrrq{width:40px;}
        .hj{width:40px;}
        .px{width:40px;}
    </style>
</head>
<body id="bodyzdwh" class="easyui-layout">
    <div region="north" style="height:30px;" border="false">
        <table id="Table1" fit="true">
            <tr  >
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="ok">保存</a>
                </td>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="fz">复制</a>
                </td>
                <td id="fb" runat="server">
                </td>
            </tr>
        </table>
        <div style="color:Red"  >
            <ul>
            <li>筛选,打印名称 功能还没有完成;</li>
            <li>合计=11会显示合计二字</li>      
            <li>bz现在只用于作为select数据源</li>   
            <li>显示默认日期只判断日期年份是否等于1900</li>   
            </ul>
        </div>
    </div>
    <div region="center" border="false">        
        <table id="zdwhtb">
            <tr class="tbth" rownum="1">
                <td field="id" style="display: none">
                    id
                </td>
                <td field="ywname">
                    字段
                </td>
                <td field="zwname">
                    名称
                </td>
                <td field="ord" >
                    顺序
                </td>
                <td field="width" >
                    宽度
                </td>
                <td field="visible">
                    显示
                </td>
                <td field="readonly" >
                    只读
                </td>
                <td field="type">
                    类型
                </td>
                <td field="sx" >
                    筛选
                </td>
                <td field="bz">
                    备注
                </td>
                <td field="showzero" >
                    显示数字0
                </td>
                <td field="event">
                    事件
                </td>
                <td field="btnvalue">
                    按钮名称
                </td>
                <td field="showmrrq" >
                    显示默认日期
                </td>
                <td field="hj" >
                    合计
                </td>
                <td field="hbltname">
                    合并列头名称
                </td>
                <td field="px" >
                    排序
                </td>
                <td field="prtname">
                    打印名称
                </td>
                <td field="mark" style="display: none">
                    mark
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
                    <input type="text" onfocus="myselect(this)" field="id" value="<%= dt.Rows[i]["id"].ToString()%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="ywname" value="<%=HtmlCha(dt.Rows[i]["ywname"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="zwname" value="<%= HtmlCha(dt.Rows[i]["zwname"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="ord"  value="<%= HtmlCha(dt.Rows[i]["ord"].ToString())%>"  />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)"  field="width" value="<%= HtmlCha(dt.Rows[i]["width"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="visible" value="<%= HtmlCha(dt.Rows[i]["visible"].ToString())%>"  />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="readonly" value="<%= HtmlCha(dt.Rows[i]["readonly"].ToString())%>" />
                </td>
                <td>
                    <select onfocus="myselect(this)" field="type" mrz="">
                        <option value="text" <%=(dt.Rows[i]["type"].ToString()=="text"  ?"selected":"") %> >text</option>
                        <option value="select" <%=(dt.Rows[i]["type"].ToString()=="select"?"selected":"") %> >select</option>
                        <option value="button" <%=(dt.Rows[i]["type"].ToString()=="button"?"selected":"") %> >button</option>
                        <option value="checkbox" <%=(dt.Rows[i]["type"].ToString()=="checkbox"?"selected":"") %> >checkbox</option>
                        <option value="textarea" <%=(dt.Rows[i]["type"].ToString()=="textarea"?"selected":"") %> >textarea</option>
                        <option value="td" <%=(dt.Rows[i]["type"].ToString()=="td"?"selected":"") %> >td</option>
                        <option value="a" <%=(dt.Rows[i]["type"].ToString()=="a"?"selected":"") %> >a</option>
                        <option value="mx" <% =dt.Rows[i]["type"].ToString()=="mx" ?"selected":"" %> >mx</option>
                        <option value="" <% =dt.Rows[i]["type"].ToString()==string.Empty ?"selected":"" %> ></option>
                    </select>
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="sx" value="<%= HtmlCha(dt.Rows[i]["sx"].ToString())%>"  />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="bz" value="<%= HtmlCha(dt.Rows[i]["bz"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="showzero" value="<%= HtmlCha(dt.Rows[i]["showzero"].ToString())%>"
                        style="width: 40px" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="event" value="<%= HtmlCha(dt.Rows[i]["event"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)"  field="btnvalue" value="<%= HtmlCha(dt.Rows[i]["btnvalue"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="showmrrq" value="<%= HtmlCha(dt.Rows[i]["showmrrq"].ToString())%>"
                        style="width: 40px" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="hj" value="<%= HtmlCha(dt.Rows[i]["hj"].ToString())%>"  />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="hbltname" value="<%= HtmlCha(dt.Rows[i]["hbltname"].ToString())%>" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="px" value="<%= HtmlCha(dt.Rows[i]["px"].ToString())%>"  />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="prtname" value="<%= HtmlCha(dt.Rows[i]["prtname"].ToString())%>" />
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
                    <input type="text" onfocus="myselect(this)" field="ywname" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="zwname" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="ord" value="" />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="width" value=""  />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="visible" value=""  />
                </td>
                <td>
                    <input type="text" onfocus="myselect(this)" field="readonly" value=""  />
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
                        <option value="mx">mx</option>
                        <option value="" selected></option>
                    </select>
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="sx" value=""  />
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="bz" value="" />
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="showzero" value=""  />
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="event" value="" />
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="btnvalue" value="" />
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="showmrrq" value=""  />
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="hj" value=""  />
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="hbltname" value="" />
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="px" value=""  />
                </td>
                <td>
                    <input type="text"  onfocus="myselect(this)" field="prtname" value="" />
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
<input type="hidden" id="wid" runat="server" />
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        $("#ok").bind("click", function () { ok_click(); });
        $("#fz").bind("click", function () { fz_click(); });
        $("#fb").bind("click", function () { fb_click(); });
        $("[field]").each(function (i, n) {
            var f = $(n).attr("field");
            $(n).addClass(f);
        });
        $("[field='ord']").bind('contextmenu', function (e) {
            var r = $(e.currentTarget).parent().parent().attr("rownum");
            var v = $(e.currentTarget).val();
            for (var i = r; i < getRowNum(); i++) {
                $("[field='ord']", $("[rownum=" + i + "]", $("#zdwhtb"))).attr("value", Number(v) + (Number(i) - Number(r)))
                $("[field='mark']", $("[rownum=" + i + "]", $("#zdwhtb"))).attr("value", 1)
            }
            return false;
        });

    });
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
    //取页面上的值
    function sr(s, i) {
        return $.trim(myVale(s, i).attr("value").replace(/'/g, "''"));
    }
    //保存
    function ok_click() {
        $('#ok').linkbutton('disable');
        var wid = document.getElementById("wid").value;
        var str = "";
        var ywname, zwname, ord, width, id, visible, readonly, type, sx, bz, showzero, event, btnvalue, showmrrq, hj, hbltname, px, prtname
        var data = {};
        data.row = new Array();
        for (var i = 0; i < getRowNum(); i++) {
            ywname = sr("ywname", i);
            zwname = sr("zwname", i);
            ord = sr("ord", i); if (ord == "") { ord = "0"; }
            width = sr("width", i); if (width == "") { width = "0"; }
<<<<<<< HEAD
            id = sr("id", i); if (id == "") { id = "0"; }
=======
            id = sr("id", i);if(id==""){id="0";}
>>>>>>> 4470c356466c0fbac2a67180f6672715fb9be616
            visible = sr("visible", i);
            if (visible == "") { visible = "0"; }
            readonly = sr("readonly", i);
            if (readonly == "") { readonly = "0"; }
            type = sr("type", i); if (type == "") { type = "text";}
            sx = sr("sx", i);if (sx == "") { sx = "0"; }
            bz = sr("bz", i);
            showzero = sr("showzero", i);if (showzero == "") { showzero = "0"; }
            event = sr("event", i);
            btnvalue = sr("btnvalue", i);
            showmrrq = sr("showmrrq", i); if (showmrrq == "") { showmrrq = "0"; }
            hj = sr("hj", i); if (hj == "") { hj = "0"; }
            hbltname = sr("hbltname", i);
            px = sr("px", i);if (px == "") { px = "0"; }
            prtname = sr("prtname", i);
            var dataRow = {};
            if (sr("mark", i) == "") {
                dataRow.mark = 0;
            } else {
                dataRow.mark = sr("mark", i);
            }
            dataRow.ywname = ywname;
            dataRow.zwname=zwname;
            dataRow.ord=ord;
            dataRow.width = width;
            dataRow.visible = visible;
            dataRow.readonly = readonly;
            dataRow.type = type;
            dataRow.sx = sx;
            dataRow.bz = bz;
            dataRow.showzero = showzero;
            dataRow.event = event;
            dataRow.btnvalue = btnvalue;
            dataRow.showmrrq = showmrrq;
            dataRow.hj = hj;
            dataRow.hbltname = hbltname;
            dataRow.px = px;
            dataRow.prtname = prtname;
            dataRow.wid = wid;
            dataRow.id = id;
            data.row.push(dataRow);
            //if (sr("mark", i) == "1") {
            //    if (id.length == 0 || id == "0") {
            //        str += " insert mb.dbo.v_tbzd (ywname, zwname, ord, width, webid, visible, readonly, type,  sx, bz, showzero, event, btnvalue, showmrrq, hj, hbltname, px, prtname) "
            //        str += "select '" + ywname + "', '" + zwname + "', '" + ord + "', '" + width + "', a.id, '" + visible + "', '" + readonly + "', '" + type + "','" + sx + "', '" + bz + "',"
            //        str+="'" + showzero + "','" + event + "','" + btnvalue + "','" + showmrrq + "','" + hj + "','" + hbltname + "','" + px + "','" + prtname + "' from mb.dbo.v_wid a where a.id='" + wid + "';";

            //    } else {
            //        if (ywname == "" && zwname == "") {
            //            str += " delete mb.dbo.v_tbzd  where id='" + id + "'; ";
            //        } else {
            //            str += " update mb.dbo.v_tbzd set ywname='" + ywname + "', zwname='" + zwname + "', ord='" + ord + "', width='" + width + "', visible='" + visible + "', readonly='" + readonly + "', type='" + type + "',  sx='" + sx + "', bz='" + bz + "', showzero='" + showzero + "', event='" + event + "', btnvalue='" + btnvalue + "', showmrrq='" + showmrrq + "', hj='" + hj + "', hbltname='" + hbltname + "', px='" + px + "', prtname='" + prtname + "'   where id='" + id + "'; ";
            //        }
            //    }
            //}

        }

        if (data.row.length == 0) {
            $.messager.alert('提示信息', '没有可更新的记录!', 'info', function () {
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
                url: '../webuser/ws.asmx/UpSYJZdwh',
                data: { wid: wid, data: JSON.stringify(data) },
                error: function (e) {
                    $.messager.alert('提示信息', '连接失败!', 'info', function () {
                        $('#ok').linkbutton('enable');
                    });
                },
                success: function (data) {
                    var r = myAjaxData(data);
                    if (r.r == 'true') {
                        $.messager.alert('提示信息', '保存成功!', 'info', function () {
                            $('#ok').linkbutton('enable');
                        });
                    } else {
                        $.messager.alert('提示信息', '保存失败!', 'info', function () {
                            $('#ok').linkbutton('enable');
                        });
                    }
                }
            })

        }
    }
    //fz
    function fz_click() {
        var oldwid
        $.messager.prompt("请复制要的wid", "", function (r) {
            if (r) {
                rFZ(r);
            }
        });
    }
    function rFZ(oldwid) {
        var newwid = document.getElementById("wid").value;
        if (!isNaN(oldwid) && oldwid != "0" && oldwid != "") {
            $.ajax({ type: 'post',
                url: '../webuser/ws.asmx/websj_fz_zd',
                data: { wid: oldwid, newwid: newwid, bs: 'zd' },
                error: function (e) {
                    $.messager.alert('提示信息', '连接失败!', 'info');
                },
                success: function (data) {
                    var r = myAjaxData(data);
                    if (r.r == 'true') {
                        $.messager.alert('提示信息', '复制成功!', 'info', function () { parent.closeTab("refresh", false); });
                    } else {
                        $.messager.alert('提示信息', '复制失败!', 'info');
                    }
                }
            })

        } else {
            $.messager.alert('提示信息', '复制wid无效!', 'info');
        }
    }

</script>
