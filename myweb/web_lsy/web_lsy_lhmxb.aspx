<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_lsy_lhmxb.aspx.cs" Inherits="web_lsy_lhmxb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../javascripts/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../javascripts/jey/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../css/jey/icon.css" rel="stylesheet" type="text/css" />
    <link href="../css/jey/mycss.css" rel="stylesheet" type="text/css" />
    <link href="../css/jey/pepper-grinder/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../javascripts/myjs/myweb.js" type="text/javascript"></script>
    <link href="../css/mycss/myweb.css" rel="stylesheet" type="text/css" />
    <style>
        .zh
        {
            width: 100px;
        }
        .fh
        {
            width: 100px;
        }
        .xm
        {
            width: 100px;
        }
        .a1
        {
            width: 100px;
        }
        .a2
        {
            width: 100px;
        }
        .a3
        {
            width: 100px;
        }
        .a4
        {
            width: 100px;
        }
        .a5
        {
            width: 100px;
        }
        .a6
        {
            width: 100px;
        }
        .dh
        {
            width: 100px;
        }
    </style>
    <title>楼号明细表</title>
</head>
<!-- 使用JQUERY-EUI 只在一个窗口打开!-->
<body>
    <form id="myform" runat="server">
    <table>
        <tr>
            <td style="width: 80px;">
                代号
            </td>
            <td id="xmselect" runat="server" style="width: 80px;">
            </td>
            <td colspan="4">
                &nbsp;<input type="hidden" id="mykey" /><input type="hidden" runat="server" id="xmhidden" />
            </td>
        </tr>
        <tr>
            <td style="width: 80px;">
                幢号
            </td>
            <td style="width: 80px;">
                <input type="text" id="zh" />
            </td>
            <td style="width: 80px;">
                房号
            </td>
            <td style="width: 80px;">
                <input type="text" id="fh" />
            </td>
            <td style="width: 80px;">
                姓名
            </td>
            <td style="width: 80px;">
                <input type="text" id="xm" />
            </td>
        </tr>
        <tr>
            <td id="tda1" runat="server" style="width: 80px;">
                a1
            </td>
            <td style="width: 80px;">
                <input type="text" id="a1" />
            </td>
            <td id="tda2" runat="server" style="width: 80px;">
                a2
            </td>
            <td style="width: 80px;">
                <input type="text" id="a2" />
            </td>
            <td id="tda3" runat="server" style="width: 80px;">
                a3
            </td>
            <td style="width: 80px;">
                <input type="text" id="a3" />
            </td>
        </tr>
        <tr>
            <td id="tda4" runat="server" style="width: 80px;">
                a4
            </td>
            <td style="width: 80px;">
                <input type="text" id="a4" />
            </td>
            <td id="tda5" runat="server" style="width: 80px;">
                a5
            </td>
            <td style="width: 80px;">
                <input type="text" id="a5" />
            </td>
            <td id="tda6" runat="server" style="width: 80px;">
                a6
            </td>
            <td style="width: 80px;">
                <input type="text" id="a6" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
            <td>
                <a href="javascript:void(0)" onclick="xg_click()" class="easyui-linkbutton" id="xg">
                    修改</a>
            </td>
            <td>
                <a href="javascript:void(0)" onclick="ok_click()" class="easyui-linkbutton" id="ok">
                    新增</a>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <div style="height: 10px">
    </div>
    <table>
        <tr>
            <td>
                楼号明细表
            </td>
        </tr>
    </table>
    <div id="content" runat="server">
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
    function xmchang() {
        document.getElementById("xmhidden").value = document.getElementById("mxid").value;
        myform.submit();
    }
    function xg_click() {
        $('#xg').linkbutton('disable');

        var a1 = document.getElementById("a1").value.replace(/'/g, "''");
        var a2 = document.getElementById("a2").value.replace(/'/g, "''");
        var a3 = document.getElementById("a3").value.replace(/'/g, "''");
        var a4 = document.getElementById("a4").value.replace(/'/g, "''");
        var a5 = document.getElementById("a5").value.replace(/'/g, "''");
        var a6 = document.getElementById("a6").value.replace(/'/g, "''");

        var zh = document.getElementById("zh").value.replace(/'/g, "''");
        var fh = document.getElementById("fh").value.replace(/'/g, "''");
        var xm = document.getElementById("xm").value.replace(/'/g, "''");
        var myid = document.getElementById("mykey").value.replace(/'/g, "''");
        var mxid = document.getElementById("mxid").value.replace(/'/g, "''");

        if (myid == 0 || myid == "") {
            $.messager.alert('提示信息', '修改有误!', 'info', function () {
                $('#xg').linkbutton('enable');
            });

        } else if (mxid == -1) {
            $.messager.alert('提示信息', '项目有误!', 'info', function () {
                $('#ok').linkbutton('enable');
            });

        } else {
            str = " update xm_t_lhmxb set mxid='" + mxid + "',zh='" + zh + "',fh='" + fh + "',xm='" + xm + "',a1='" + a1 + "',a2='" + a2 + "',a3='" + a3 + "',a4='" + a4 + "',a5='" + a5 + "',a6='" + a6 + "' where    id=" + myid;
            if (str == "") {
                $.messager.alert('提示信息', '没有可更新的记录!', 'info', function () {
                    $('#xg').linkbutton('enable');
                });

            } else {
                //alert(str);
                //return false;
                var r = myAjax(str);
                if (r == -1) {

                    $.messager.alert('提示信息', '连接失败!', 'info', function () {
                        $('#xg').linkbutton('enable');
                    });
                } else {
                    if (r.r == 'true') {
                        $.messager.alert('提示信息', '处理成功!', 'info', function () {
                            $('#xg').linkbutton('enable');
                            window.returnValue = "ok";
                            myform.submit();
                        });
                    } else {
                        $.messager.alert('提示信息', r.msg, 'info', function () {
                            $('#xg').linkbutton('enable');
                        });

                    }
                }
            }
        }

    }

    function ok_click() {
        $('#ok').linkbutton('disable');

        var a1 = document.getElementById("a1").value.replace(/'/g, "''");
        if (a1.length == 0) { a1 = "0"; }
        var a2 = document.getElementById("a2").value.replace(/'/g, "''");
        if (a2.length == 0) { a2 = "0"; }
        var a3 = document.getElementById("a3").value.replace(/'/g, "''");
        if (a3.length == 0) { a3 = "0"; }
        var a4 = document.getElementById("a4").value.replace(/'/g, "''");
        if (a4.length == 0) { a4 = "0"; }
        var a5 = document.getElementById("a5").value.replace(/'/g, "''");
        if (a5.length == 0) { a5 = "0"; }
        var a6 = document.getElementById("a6").value.replace(/'/g, "''");
        if (a6.length == 0) { a6 = "0"; }

        var zh = document.getElementById("zh").value.replace(/'/g, "''");
        var fh = document.getElementById("fh").value.replace(/'/g, "''");
        var xm = document.getElementById("xm").value.replace(/'/g, "''");

        var mxid = document.getElementById("mxid").value.replace(/'/g, "''");

        if (mxid == -1) {
            $.messager.alert('提示信息', '项目有误!', 'info', function () {
                $('#ok').linkbutton('enable');
            });

        } else {
            var str = "";
            str = " insert xm_t_lhmxb (mxid,zh,fh,xm,a1,a2,a3,a4,a5,a6) values ('" + mxid + "','" + zh + "','" + fh + "','" + xm + "','" + a1 + "','" + a2 + "','" + a3 + "','" + a4 + "','" + a5 + "','" + a6 + "')";
            if (str == "") {
                $.messager.alert('提示信息', '没有可更新的记录!', 'info', function () {
                    $('#ok').linkbutton('enable');
                });

            } else {
                //alert(str);
                //return false;
                var r = myAjax(str);
                if (r == -1) {
                    $.messager.alert('提示信息', '连接失败!', 'info', function () {
                        $('#ok').linkbutton('enable');
                    });
                } else {
                    if (r.r == 'true') {
                        $.messager.alert('提示信息', '处理成功!', 'info', function () {
                            $('#ok').linkbutton('enable');
                            window.returnValue = "ok";
                            myform.submit();
                        });
                    } else {
                        $.messager.alert('提示信息', r.msg, 'info', function () {
                            $('#ok').linkbutton('enable');
                        });

                    }
                }
            }
        }

    }

    function xg(i) {
        document.getElementById("mykey").value = document.getElementById("mykey_" + i).value;
        document.getElementById("mxid").value = document.getElementById("mxid_" + i).value;
        document.getElementById("xmhidden").value = document.getElementById("mxid_" + i).value;
        document.getElementById("zh").value = document.getElementById("zh_" + i).innerHTML;
        document.getElementById("fh").value = document.getElementById("fh_" + i).innerHTML;
        document.getElementById("xm").value = document.getElementById("xm_" + i).innerHTML;
        document.getElementById("a1").value = document.getElementById("a1_" + i).innerHTML;
        document.getElementById("a2").value = document.getElementById("a2_" + i).innerHTML;
        document.getElementById("a3").value = document.getElementById("a3_" + i).innerHTML;
        document.getElementById("a4").value = document.getElementById("a4_" + i).innerHTML;
        document.getElementById("a5").value = document.getElementById("a5_" + i).innerHTML;
        document.getElementById("a6").value = document.getElementById("a6_" + i).innerHTML;
    }
    function del(i) {
        $.messager.confirm("删除提示", "确定要删除?", function (b) {
            if (b == true) {
                var mykey = $.trim(document.getElementById("mykey_" + i).value).replace(/'/g, "''");
                var str = "delete from xm_t_lhmxb where id=" + mykey;

                //alert(str);
                //return false;
                var r = myAjax(str);
                if (r == -1) {
                    $.messager.alert('提示信息', '连接失败!', 'info');
                } else {
                    if (r.r == 'true') {
                        $.messager.alert('提示信息', '处理成功!', 'info', function () {
                            myform.submit();
                        });
                    } else {
                        $.messager.alert('提示信息', r.msg, 'info');

                    }
                }

            }
        });
    }
    
</script>
</html>
