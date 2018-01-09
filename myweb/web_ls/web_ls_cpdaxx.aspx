<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_ls_cpdaxx.aspx.cs" Inherits="web_ls_web_ls_cpdaxx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <ctrl:DefaultHeader   id="sysHead" runat="server" title="产品参数" />
</head>
<body style="width: 200px">
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <a href="javascript:void(0)" class="easyui-linkbutton" id="ok">新增</a>
            </td>
            <td>
                <a href="javascript:void(0)" class="easyui-linkbutton" id="edit">修改</a>
            </td>
            <td>
                <a href="javascript:void(0)" class="easyui-linkbutton" id="del">删除</a>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <div style="width: 200px">
        <ul id="xx">
        </ul>
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    $(function () {
        $("#ok").bind("click", function () { ok_click(); });
        $("#edit").bind("click", function () { edit_click(); });
        $("#del").bind("click", function () { del_click(); });
        $('#xx').tree({
            url: 'web_ls_main.ashx?lx=dl',
            onBeforeLoad: function (node, param) {

            }
        });
    });

    function ok_click() {
        if ($('#xx').tree("getSelected") != null) {//选择了一行
            var id = $.trim($('#xx').tree("getSelected").id);
            var t = openModal("web_ls_cpdaxx_add.aspx?lx=dl&zt=add&id=" + id);
            if (t == "ok") {
                $('#xx').tree("reload");
            }
        } else {
            parent.$.messager.alert('提示信息', '请先选择一个大类!', 'info');
        }
    }
    function edit_click() {
        if ($('#xx').tree("getSelected") != null) {
            var id = $.trim($('#xx').tree("getSelected").id);
            var mc = $.trim($('#xx').tree("getSelected").text)
            //alert(mc);
            //alert($('#xx').tree("getSelected").text)
            //alert(mySysDate(mc))           
            if (id == "-1") {//没有大类自动生成
                parent.$.messager.alert('提示信息', '不能修改这个大类!', 'info', function () {
                });

            } else {
                //alert("web_ls_cpdaxx_add.aspx?lx=dl&zt=edit&mc=" + mc + "&id=" + id);
                var t = openModal("web_ls_cpdaxx_add.aspx?lx=dl&zt=edit&mc=" + mySysDate(mc) + "&id=" + id);
                if (t == "ok") { $('#xx').tree("reload"); }
            }
        } else {
            parent.$.messager.alert('提示信息', '请先选择一个大类!', 'info');
        }

    }
    function del_click() {

        if ($('#xx').tree("getSelected") != null) {
            var id = $.trim($('#xx').tree("getSelected").id);
            if ($('#xx').tree("getSelected").id == "-1") {//没有大类自动生成
                parent.$.messager.alert('提示信息', '不能删除这个大类!', 'info', function () {                    ;
                });

            } else if ($('#xx').tree("getSelected").attributes.xjbs == "1") {//有下级
                parent.$.messager.alert('提示信息', '此大类有下级类别,不能删除!', 'info', function (r) {                    
                });

            } else {
                //xjbs 下级标识
                var str = "";
                str = "select top 1 khlb from V_ls_cpda where khlb=" + id
                //alert(str);
                var r = myAjax(str);
                if (r == -1) {
                    $('#ok').linkbutton('enable');
                    parent.$.messager.alert('提示信息', '连接失败!', 'info', function (r) {                        
                    });

                } else {
                    if (r.r == 'true' && r.msg == "null") {  
                          //允许删除                        
                        str = " delete from v_ls_xxdmb where id=" + id;
                        if (id == "") {
                            parent.$.messager.alert('提示信息', '没有可更新的记录!', 'info', function (r) {
                                $('#ok').linkbutton('enable');
                            });

                        } else {
                            r = myAjax(str);
                            if (r == -1) {
                                parent.$.messager.alert('提示信息', '连接失败!', 'info', function () {
                                    $('#ok').linkbutton('enable');
                                });
                            } else {
                                if (r.r == 'true') {
                                    parent.$.messager.alert('提示信息', '删除成功!', 'info', function () {
                                        $('#xx').tree("reload");
                                    });
                                } else {
                                    parent.$.messager.alert('提示信息', r.msg, 'info', function (r) {
                                        $('#ok').linkbutton('enable');
                                    });
                                }
                            }
                        }
                    } else {
                        parent.$.messager.alert('提示信息', "已经被产品档案引用,不能删除", 'info', function (r) {
                            $('#ok').linkbutton('enable');                            
                        });
                    }
                }
            }
        } else {
            parent.$.messager.alert('提示信息', '请先选择一个大类!', 'info');
        }

    }
</script>
