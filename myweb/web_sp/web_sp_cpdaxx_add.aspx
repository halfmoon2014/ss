<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_sp_cpdaxx_add.aspx.cs"
    Inherits="web_ls_cpdaxx_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <ctrl:DefaultHeader  id="sysHead" runat="server" title="新增" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 400px; height: 500px">
        名称:<input type="text" id="mc" runat="server" />
        <a href="javascript:void(0)" onclick="ok_click()" class="easyui-linkbutton" id="ok">
            确定</a>
    </div>
    <input type="hidden" id="myid" runat="server" />
    <input type="hidden" id="lx" runat="server" />
    <input type="hidden" id="zt" runat="server" />
    </form>
</body>
</html>
<script language="javascript">
    $(function () {
        //$("#ok").bind("click", function () { ok_click(); });
    });


    function ok_click() {
        $('#ok').linkbutton('disable');
        var mc = $.trim(document.getElementById("mc").value).replace(/'/g, "''");
        if (mc.length == 0) {
            $.messager.alert('提示信息', '名称不能为空', 'info', function () {
                $('#ok').linkbutton('enable');
            });
        } else {
            var id = $.trim(document.getElementById("myid").value);
            var lx = $.trim(document.getElementById("lx").value);
            var zt = $.trim(document.getElementById("zt").value);
            var str = "";
            if (zt == "add") {
                if (id == "-1") {//如果什么都有进来新增的
                    str = " insert v_sp_xxdmb (mc,lx,ssid) values ('" + mc + "','" + lx + "',0)";
                }
                else {
                    str = " insert v_sp_xxdmb (mc,lx,ssid) values ('" + mc + "','" + lx + "','" + id + "')";
                }
            } else if (zt == "edit") {
                str = " update v_sp_xxdmb set mc='" + mc + "' where lx='" + lx + "' and  id=" + id;
            }

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
                            window.close();
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
