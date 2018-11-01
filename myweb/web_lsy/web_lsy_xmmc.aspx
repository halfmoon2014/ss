<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_lsy_xmmc.aspx.cs" Inherits="web_lsy_xmmc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../javascripts/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../javascripts/jey/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="../css/jey/icon.css" rel="stylesheet" type="text/css" />
    <link href="../css/jey/mycss.css" rel="stylesheet" type="text/css" />
    <link href="../css/jey/pepper-grinder/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../javascripts/myjs/myweb.js" type="text/javascript"></script>
    <style>
        .xmmc
        {
            width: 100px;
        }
    </style>
    <title>项目名称</title>
</head>
<!-- 使用JQUERY-EUI 只在一个窗口打开!-->
<body>
    <form id="myform" runat="server">
    <table>
        <tr>
            <td>
                <input id="xmmc" type="text" />
                <input id="myid" type="hidden" />
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
                项目名称
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
        var xmmc = $.trim(document.getElementById("xmmc").value).replace(/'/g, "''");
        var id = $.trim(document.getElementById("myid").value);
        var str = "";
        if (xmmc.length == 0) {
            $.messager.alert('提示信息', '项目名称不能为空', 'info', function () {
                $('#xg').linkbutton('enable');
            });

        } else if (id == "0" || id == "") {
            $.messager.alert('提示信息', '修改有误!', 'info', function () {
                $('#xg').linkbutton('enable');
            });

        } else {
            str = " update xm_t_xmmczb set xmmc='" + xmmc + "' where    id=" + id;

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
        var xmmc = $.trim(document.getElementById("xmmc").value).replace(/'/g, "''");
        if (xmmc.length == 0) {
            $.messager.alert('提示信息', '项目名称不能为空', 'info', function () {
                $('#ok').linkbutton('enable');
            });

        } else {
            var str = "";

            str = " insert xm_t_xmmczb (xmmc) values ('" + xmmc + "')";

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
        document.getElementById("myid").value = document.getElementById("mykey_" + i).value;
        document.getElementById("xmmc").value = document.getElementById("xmmc_" + i).innerHTML;
    }
    function del(i) {
        $.messager.confirm("删除提示", "确定要删除?", function (b) {
            if (b == true) {
                var id = $.trim(document.getElementById("mykey_" + i).value);
                var str = "delete from xm_t_xmmczb where id=" + id;

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
