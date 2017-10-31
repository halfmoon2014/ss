<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_ls_cpdaxxcx.aspx.cs" Inherits="web_ls_web_ls_cpdaxx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <ctrl:DefaultHeader  ID="sysHead" runat="server" TITLE="选择" />
</head>
<body style="width: 200px">
    <form id="form1" runat="server">
    <!--
    <table>
        <tr>
            <td>
                <a href="javascript:void(0)" class="easyui-linkbutton" id="ok">确定</a>
            </td>
            <td>
               &nbsp;
            </td>

        </tr>
    </table>
    -->
    <br />

    <div style="width: 200px">
        <ul id="xx">
        </ul>
    </div>
    <input type="hidden" id="khlbid" value="0" runat="server" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        $("#ok").bind("click", function () { ok_click(); });
        $('#xx').tree({
            url: 'web_ls_main.ashx?lx=dl',
            onBeforeLoad: function (node, param) {
            },
            onLoadSuccess: function (node, data) {
                if (document.getElementById("khlbid").value != '0') {
                    ss(document.getElementById("khlbid").value);
                }
            }
        });
    });

    function r() {
        var r = new Array();
        if ($('#xx').tree("getSelected") != null) {//选择了一行
            r[0] = $.trim($('#xx').tree("getSelected").id)
            r[1] = $.trim($('#xx').tree("getSelected").text)
            document.getElementById("khlbid").value = r[0];
        }
        return r;
    }
    function ss(id) {
        if (id != null && id != "") {//选择了一行
            var node = $('#xx').tree('find', id);
            $('#xx').tree('select', node.target);
        }
        
    }
    function ok_click() {
        if ($('#xx').tree("getSelected") != null) {//选择了一行
            var id = $.trim($('#xx').tree("getSelected").id);
            if (id != "" || id != "0") {
                var r = new Array()
                r[0]=id;
                r[1] = $.trim($('#xx').tree("getSelected").text)
                document.getElementById("khlbid").value = r[0];
                window.returnValue = r;
                window.close();
            }
        } else {
            $.messager.alert('提示信息', '请先选择一个类别!', 'info');
        }
    }
  

   
</script>
