<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_lsy_xmmcmx.aspx.cs" Inherits="web_lsy_xmmcmx" %>

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
        .zmc1
        {
            width: 100px;
        }
        .zmc2
        {
            width: 100px;
        }
        .zmc3
        {
            width: 100px;
        }
        .zmc4
        {
            width: 100px;
        }
        .zmc5
        {
            width: 100px;
        }
        .zmc6
        {
            width: 100px;
        }
        .zmc7
        {
            width: 100px;
        }
    </style>
    <title>项目名称明细</title>
</head>
<!-- 使用JQUERY-EUI 只在一个窗口打开!-->
<body>
    <form id="myform" runat="server">
    <table>
        <tr>
            <td>
                项目名称
            </td>
            <td id="xmmcselect" runat="server">
            </td>
            <td>
                代号
            </td>
            <td>
                <input type="text" id="dh" />
            </td>
            <td colspan="2">
                &nbsp;<input type="hidden" id="mxid" />
            </td>
        </tr>
        <tr>
            <td>
                名称1
            </td>
            <td>
                <input type="text" id="zmc1" />
            </td>
            <td>
                名称2
            </td>
            <td>
                <input type="text" id="zmc2" />
            </td>
            <td>
                名称3
            </td>
            <td>
                <input type="text" id="zmc3" />
            </td>
        </tr>
        <tr>
            <td>
                名称4
            </td>
            <td>
                <input type="text" id="zmc4" />
            </td>
            <td>
                名称5
            </td>
            <td>
                <input type="text" id="zmc5" />
            </td>
            <td>
                名称6
            </td>
            <td>
                <input type="text" id="zmc6" />
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
                项目名称明细
            </td>
        </tr>
    </table>
    <div id="content" runat="server">
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
    function xg_click() {
        $('#xg').linkbutton('disable');
        var zmc1 = document.getElementById("zmc1").value.replace(/'/g, "''");
        var zmc2 = document.getElementById("zmc2").value.replace(/'/g, "''");
        var zmc3 = document.getElementById("zmc3").value.replace(/'/g, "''");
        var zmc4 = document.getElementById("zmc4").value.replace(/'/g, "''");
        var zmc5 = document.getElementById("zmc5").value.replace(/'/g, "''");
        var zmc6 = document.getElementById("zmc6").value.replace(/'/g, "''");
        var mxid = document.getElementById("mxid").value.replace(/'/g, "''");
        var dh = document.getElementById("dh").value.replace(/'/g, "''");

        var myid = $.trim(document.getElementById("mykey").value).replace(/'/g, "''");
        if (myid <= 0) {
            $.messager.alert('提示信息', '项目名称不能为空', 'info', function () {
                $('#ok').linkbutton('enable');
            });
        } else if (dh.length <= 0) {
            $.messager.alert('提示信息', '代号不能为空', 'info', function () {
                $('#ok').linkbutton('enable');
            });

        } else if (mxid == 0 || mxid == "") {
            $.messager.alert('提示信息', '修改有误!', 'info', function () {
                $('#xg').linkbutton('enable');
            });

        } else {
            str = " update xm_t_xmmcmx set dh='" + dh + "', id='" + myid + "',zmc1='" + zmc1 + "',zmc2='" + zmc2 + "',zmc3='" + zmc3 + "',zmc4='" + zmc4 + "',zmc5='" + zmc5 + "',zmc6='" + zmc6 + "' where    mxid=" + mxid;

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
</script>
<script language="javascript" type="text/javascript">
    function ok_click() {
        $('#ok').linkbutton('disable');

        var zmc1 = document.getElementById("zmc1").value.replace(/'/g, "''");
        var zmc2 = document.getElementById("zmc2").value.replace(/'/g, "''");
        var zmc3 = document.getElementById("zmc3").value.replace(/'/g, "''");
        var zmc4 = document.getElementById("zmc4").value.replace(/'/g, "''");
        var zmc5 = document.getElementById("zmc5").value.replace(/'/g, "''");
        var zmc6 = document.getElementById("zmc6").value.replace(/'/g, "''");
        var dh = document.getElementById("dh").value.replace(/'/g, "''");

        var myid = $.trim(document.getElementById("mykey").value).replace(/'/g, "''");
        if (myid <= 0) {
            $.messager.alert('提示信息', '项目名称不能为空', 'info', function () {
                $('#ok').linkbutton('enable');
            });
        } else if (dh.length <= 0) {
            $.messager.alert('提示信息', '代号不能为空', 'info', function () {
                $('#ok').linkbutton('enable');
            });
        } else {
            var str = "";

            str = " insert xm_t_xmmcmx (dh,id,zmc1,zmc2,zmc3,zmc4,zmc5,zmc6) values ('" + dh + "','" + myid + "','" + zmc1 + "','" + zmc2 + "','" + zmc3 + "','" + zmc4 + "','" + zmc5 + "','" + zmc6 + "')";

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
</script>
<script language="javascript" type="text/javascript">
    function xg(i) {
        document.getElementById("mykey").value = document.getElementById("mykey_" + i).value;
        document.getElementById("mxid").value = document.getElementById("mxid_" + i).value;
        document.getElementById("zmc1").value = document.getElementById("zmc1_" + i).innerHTML;
        document.getElementById("zmc2").value = document.getElementById("zmc2_" + i).innerHTML;
        document.getElementById("zmc3").value = document.getElementById("zmc3_" + i).innerHTML;
        document.getElementById("zmc4").value = document.getElementById("zmc4_" + i).innerHTML;
        document.getElementById("zmc5").value = document.getElementById("zmc5_" + i).innerHTML;
        document.getElementById("zmc6").value = document.getElementById("zmc6_" + i).innerHTML;
        document.getElementById("dh").value = document.getElementById("dh_" + i).innerHTML;

    }
    function del(i) {
        $.messager.confirm("删除提示", "确定要删除?", function (b) {
            if (b == true) {
                var mxid = $.trim(document.getElementById("mxid_" + i).value).replace(/'/g, "''");
                var str = "delete from xm_t_xmmcmx where mxid=" + mxid;

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
