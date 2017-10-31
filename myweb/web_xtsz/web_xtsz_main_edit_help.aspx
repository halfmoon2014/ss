<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit_help.aspx.cs"
    Inherits="web_xtsz_web_xtsz_main_edit_help" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <ctrl:DefaultHeader  ID="sysHead" runat="server" />
</head>
<body runat="server">
    <form runat="server">
    <table style="width: 100%; height: 100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <a href="javascript:void(0)" class="easyui-linkbutton" id="ok">保存</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <ul>
                    <li>说明文档</li>
                    <li>
                        <textarea id="tbhelp" rows="30" cols="120" runat="server" ></textarea></li>
                </ul>
            </td>
        </tr>
    </table>
    <input type="hidden" id="wid" runat="server" />
    </form>
</body>
</html>
<script>

    $(function () {
        $("#ok").bind("click", function () { ok_click(); });
    });
    function ok_click() {        
        $('#ok').linkbutton('disable');
        var tbhelp = mySysDate(document.getElementById("tbhelp").value);
        var wid = mySysDate(document.getElementById("wid").value);

        $.ajax({ type: 'post',
            url: '../webuser/ws.asmx/sjy_uphelp',
            data: { wid: wid, help: tbhelp },
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

</script>
