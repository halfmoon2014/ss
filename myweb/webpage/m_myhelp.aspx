<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_myhelp.aspx.cs" Inherits="webpage_m_myhelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<ctrl:DefaultHeader    ID="sysHead" runat="server"   />
<script language="javascript" type="text/javascript">
    $(function () {
        $("#ok").bind("click", function () { mysave(); });
        $("#esc").bind("click", function () { window.close(); });
    });
    function mysave() {
        var reg = new RegExp("\r\n", "g");
        var help = mySysDate(document.getElementById("TextArea1").value);
        var myid = document.getElementById("myid").value;
        $('#ok').linkbutton('disable');

        $.ajax({ type: 'post',
            url: '../webuser/ws.asmx/helpup',
            data: { value1: help, value2: myid },
            error: function (e) {

                $.messager.alert('提示信息', '连接失败', 'info', function () {
                    $('#ok').linkbutton('enable');
                });
            },
            success: function (data) {
                var r = myAjaxData(data);
                if (r.r == 'true') {
                    $.messager.alert('提示信息', '保存成功', 'info', function () {
                        $('#ok').linkbutton('enable');
                    });

                } else {
                    $.messager.alert('提示信息', '保存失败', 'info', function () {
                        $('#ok').linkbutton('enable');
                    });
                }
            }
        })

    }
</script>
<head runat="server">
    <title></title>
</head>
<body>
    <input type="hidden" id="myid" runat="server" />
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td align="right">
                    <table>
                        <tr>
                            <td width="64" valign="middle">
                                <a href="javascript:void(0)" class="easyui-linkbutton" id="ok">保存</a>
                            </td>
                            <td width="64" valign="middle">
                                <a href="javascript:void(0)" class="easyui-linkbutton" id="esc">退出</a>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td>
                                <textarea id="TextArea1" rows="300" cols="20" runat="server" style="width: 100%;
                                    height: 550px"></textarea>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
