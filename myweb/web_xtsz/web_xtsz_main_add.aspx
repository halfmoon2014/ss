<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_add.aspx.cs" Inherits="web_xtsz_web_xtsz_main_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
    <ctrlHeader:DefaultHeader ID="sysHead" runat="server" Title="新增" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ul>
    <li>
    名称:<input type="text" style="width:400" id="mc" runat="server" />
    </li>
    <li>
    类型:<input type="text" style="width:200" id="lx" runat="server" />
    </li>
    </ul>
    <a href="javascript:void(0)" class="easyui-linkbutton" id="ok" >确定</a>
    </div>
    <input type="hidden" id="userid" runat="server" />
    <input type="hidden" id="zt" runat="server" />
    <input type="hidden" id="wid" runat="server" />
    </form>
</body>
</html>
<script language="javascript">
    $(function () {
        $("#ok").bind("click", function () { ok_click(); });
    });
    function ok_click() {
        $('#ok').linkbutton('disable');
        var mc = document.getElementById("mc").value;
        var lx = document.getElementById("lx").value;
        if (mc.length == 0) {
            $.messager.alert('提示信息', '名称不能为空', 'info', function () {
                $('#ok').linkbutton('enable');
            });                        
        } else {
            var userid = document.getElementById("userid").value;
            var wid = document.getElementById("wid").value;
            var zt = document.getElementById("zt").value;

            $.ajax({ type: 'post',
                url: "../webuser/ws.asmx/websj_cl",
                data: { userid: userid, mc: mc, lx: lx, wid: wid, zt: zt },
                error: function (e) {
                    $.messager.alert('提示信息', '连接失败!', 'info', function () {
                        $('#ok').linkbutton('enable');
                    });
                },
                success: function (data) {
                    var r = myAjaxData(data);
                    if (r.r == 'true') {
                        $.messager.alert('提示信息', '保存成功!', 'info', function () { window.returnValue = "ok"; $('#ok').linkbutton('enable'); window.close(); });

                    } else {
                        $.messager.alert('提示信息', '保存失败!', 'info', function () {
                            $('#ok').linkbutton('enable');
                        });
                    }
                }
            })
        }
        
    }    
</script>
