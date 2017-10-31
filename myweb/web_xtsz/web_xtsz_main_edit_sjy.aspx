<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_sjy.aspx.cs"
    Inherits="web_xtsz_web_xtsz_main_edit_sjy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <ctrl:DefaultHeader  ID="sysHead" runat="server" />
</head>
<body runat="server">
    <form runat="server">
    <div  style=" width:100%;height:100%;">
    <table style="width: 100%; height: 30px">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <a href="javascript:void(0)" class="easyui-linkbutton" id="ok">保存</a>
                        </td>
                        <td id="fb" runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="width: 100%; height:650px; overflow:scroll">
    <table style="width: 100%; height: 100%">
        <tr>
            <td>
                <ul>
                    <li>页面信息:</li>
                    <li>名称                    
                        <input type="text" id="name" readonly="readonly" runat="server" />                    
                    <li><input type="checkbox" id="mrcx" runat="server" />默认查询;排序字段:<input type="text" id="orderby" runat="server" />
                        页大小:<input type="text" id="pagesize" runat="server" /> <input type="checkbox" id="myadd" runat="server" />允许新增
                    </li>
                    <li>方位sql</li>
                    <li>
                        <textarea  ondblclick="fd(this)" id="fwsql" rows="5" cols="120" runat="server"></textarea></li>
                    <li>sql语句</li>
                    <li>
                    <ul style=" list-style-type:none ;padding-left: 0px; "><li>
                        <textarea ondblclick="fd(this)" id="tbsql" rows="10" cols="120" runat="server"></textarea>
                        </li><li>
                        <textarea ondblclick="fd(this)" id="tbsql2" rows="10" cols="120" runat="server"></textarea>
                        </li>
                        </ul>
                        </li>
                    <li>明细关联:<input type="text" id="mxgl" runat="server" />明细来源:<input type="text" id="mxly"
                        runat="server" />可输入:主表,明细数据源,存储过程</li>
                    <li>明细数据源:</li>
                    <li>
                        <textarea  ondblclick="fd(this)" id="mxsql" rows="10" cols="120" runat="server"></textarea></li>
                    <li>明细数据源-表头关联:<input type="text" id="mxhgl" runat="server" />sql语句-表头关联:<input type="text"
                        id="mxhord" runat="server" /></li>
                    <li>明细头数据源:</li>
                    <li>
                        <textarea ondblclick="fd(this)" id="mxhsql" rows="2" cols="120" runat="server"></textarea></li>
                </ul>
            </td>
        </tr>
    </table>
    </div>
    </div>
    <input type="hidden" id="wid" runat="server" />
    </form>
</body>
</html>
<script>

    $(function () {
        $("#ok").bind("click", function () { ok_click(); });
        $("#fb").bind("click", function () { fb_click(); });
    });

    function ok_click() {

        $('#ok').linkbutton('disable');
        var sql = document.getElementById("tbsql").value;
        var fwsql = document.getElementById("fwsql").value;
        var name = document.getElementById("name").value;
        //var zwname = document.getElementById("zwname").value;
        var mxgl = document.getElementById("mxgl").value;
        var mxsql = document.getElementById("mxsql").value;
        var mxhgl = document.getElementById("mxhgl").value;
        var mxhord = document.getElementById("mxhord").value;
        var mxhsql = document.getElementById("mxhsql").value;
        var sql_2 = document.getElementById("tbsql2").value;
//        if (sql.length != 0 && name.length != 0) {
//            $.messager.alert('提示信息', '视图与sql语句中只能填一项!', 'info', function () {
//                $('#ok').linkbutton('enable');
//            });

//            /*} else if (sql.length == 0 && tbname.length == 0) {
//            $.messager.alert('提示信息', '视图与sql语句中必填一项!', 'info');
//            $('#ok').linkbutton('enable');
//            return false;*/
        //        } else 
        if (name.length == 0) {
            $.messager.alert('提示信息', '中文名称一定要输入!', 'info', function () {
                $('#ok').linkbutton('enable');
            });

        } else {
            var wid = document.getElementById("wid").value;

            var mrcx = (document.getElementById("mrcx").checked ? "1" : "0");
            var myadd = (document.getElementById("myadd").checked ? "1" : "0");
            var orderby = document.getElementById("orderby").value;
            var pagesize = document.getElementById("pagesize").value;
            var mxly = document.getElementById("mxly").value;
            if (pagesize == "") { pagesize = "0"; }

            $.ajax({ type: 'post',
                url: '../webuser/ws.asmx/sjy_up',
                data: { wid: wid, value1: name,  value3: sql, value4: fwsql, mrcx: mrcx, myadd: myadd, orderby: orderby, pagesize: pagesize, mxgl: mxgl, mxsql: mxsql, mxhgl: mxhgl, mxhord: mxhord, mxhsql: mxhsql, mxly: mxly,sql_2:sql_2 },
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
    //放大
    function fd(obj) {

        if (typeof ($(obj).attr("oldw")) == "undefined") {
            //没有属性,增加一个
            $(obj).attr("oldw", $(obj).css("width"));
        }
        if (typeof ($(obj).attr("oldh")) == "undefined") {
            //没有属性,增加一个
            $(obj).attr("oldh", $(obj).css("height"));
        }
        if ($(obj).css("width") == $(obj).attr("oldw")) {
            $(obj).css("height", "600");
            $(obj).css("width", "90%");
        } else {
            $(obj).css("height", $(obj).attr("oldh"));
            $(obj).css("width", $(obj).attr("oldw"));
        }
    }

</script>
