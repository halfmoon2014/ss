<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main.aspx.cs" Inherits="web_xtsz_web_xtsz_main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <ctrl:DefaultHeader  id="sysHead" runat="server" title="页面管理" />
</head>
<body id="mainbody" runat="server" class="easyui-layout">

</body>
</html>
<script type="text/javascript">
    $(function () {

        $('#leftmenu').tree({
            url: 'web_xtsz_main.ashx?type=GetTree'
        });

        $('#ctable').datagrid({
            title: '页面列表',
            //iconCls: 'icon-save',
            //width: 700,
            //width: function () { return document.body.clientWidth * 0.9 },
           // height: 550,
            fit: true,
            nowrap: true,
            autoRowHeight: false,
            striped: true,
            collapsible: true,
            url: 'web_xtsz_main.ashx',
            queryParams: { 'type': 'GetCTable', 'id': -1, 'lx': '','js':'','sql':'','wid':'','myname':'' },
            //idField: 'wid',
            columns: [[
					{ field: 'wid', title: 'wid', width: 120 },
                    { field: 'cl', title: '处理', width: 120,
                        formatter: function (value, rec, index) {
                            var s = "<a href=\"#\"  onclick=\"ALink(\'edit\'  ,\'" + rec.wid + "\',\'" + rec.name + "\')\">" + value + "</a>";
                            return s;
                        }
                    },
                    { field: 'name', title: '名称', width: 250 },
                    { field: 'fb', title: '发布', width: 60,
                        formatter: function (value, rec, index) {
                            var s = "<a href=\"#\"  onclick=\"FBLink(\'" + rec.wid + "\',\'" + rec.name + "\')\">" + value + "</a>";
                            return s;
                        }
                    },
                    { field: 'lx', title: '分类', width: 80 }
				]],
            loadMsg: '数据加载中...',
            pagination: true,
            pageList: [20, 30, 50],
            pageSize: 50,
            pageNumber: 1,
            singleSelect: true,
            toolbar: [{
                id: 'btnadd',
                text: '新增',
                iconCls: 'icon-add',
                handler: function () {
                    gridAdd();
                }
            }, {
                id: 'btncut',
                text: '修改',
                iconCls: 'icon-edit',
                handler: function () {
                    gridEdit();
                }
            },
            {
                id: 'btncut',
                text: '复制',
                iconCls: 'icon-edit',
                handler: function () {
                    gridFz();
                }
            },
            {
                id: 'btncut',
                text: '测试',
                iconCls: 'icon-edit',
                handler: function () {
                    gridTest();
                }
            }

            , '-', {
                id: 'btndel',
                text: '删除',
                iconCls: 'icon-remove',
                handler: function () {
                    gridDel();
                }
            }]
            //rownumbers: true
        });
        $('#ctable').datagrid('getPager').pagination({
            displayMsg: '当前显示从{from}到{to}共{total}记录',
            onBeforeRefresh: function () {
                
            },
            onSelectPage: function (pageNumber, pageSize) {
                $('#ctable').datagrid('options').pageNumber = pageNumber;
                $('#ctable').datagrid('options').pageSize = pageSize;
                $('#ctable').datagrid('reload');
            }
        });
    });

    //发布
    function FBLink(wid, name) {
        if (!isNaN(wid) && wid != "0" && wid != "") {
            $.ajax({ type: 'post',
                url: '../webuser/ws.asmx/web_fb',
                data: { wid: wid },
                error: function (e) {
                    $.messager.alert('提示信息', '连接失败!', 'info');
                },
                success: function (data) {
                    var r = myAjaxData(data);
                    $.messager.alert('提示信息', r.r, 'info');
                }
            })

        } else {
            $.messager.alert('提示信息', 'wid无效!', 'info');
        }
    }

    function gridAdd() {
        if ($('#leftmenu').tree("getSelected") != null && $('#leftmenu').tree("getSelected").attributes != undefined) {
            if ($('#leftmenu').tree("getSelected").attributes.type != null && $('#leftmenu').tree("getSelected").attributes.type == "user") {//如果是用户而不是部门树
                openModal("web_xtsz_main_add.aspx?zt=add&userid=" + $('#leftmenu').tree("getSelected").id, "", "dialogWidth=300px;dialogHeight=200px;", function (t) {
                    if (t == "ok") {
                        $('#ctable').datagrid("reload");
                    }
                });
                
            } else {
                $.messager.alert('提示信息', '请先选择人员!', 'info');
            }
        } else {
            $.messager.alert('提示信息', '请先选择人员!', 'info');
        }
    }
    function gridEdit() {
        var mys = $('#ctable').datagrid("getSelected");
        if (mys != null) {

            if ($('#leftmenu').tree("getSelected") != null && $('#leftmenu').tree("getSelected").attributes != undefined) {
                if ($('#leftmenu').tree("getSelected").attributes.type != null && $('#leftmenu').tree("getSelected").attributes.type == "user") {//如果是用户而不是部门树
                    var userid = $('#leftmenu').tree("getSelected").id;
                    var mc = mySysDate(mys.name);
                    var lx = mySysDate(mys.lx);
                    openModal("web_xtsz_main_add.aspx?zt=edit&userid=" + userid + "&wid=" + mys.wid + "&lx=" + lx + "&mc=" + mc, "", "dialogWidth=300px;dialogHeight=200px;", function (r) {
                        if (t == "ok") {
                            $('#ctable').datagrid("reload");
                        }
                    });
                   
                } else {
                    $.messager.alert('提示信息', '请先选择人员!', 'info');
                }
            } else {
                $.messager.alert('提示信息', '请先选择人员!', 'info');
            }

        } else {
            $.messager.alert('提示信息', '请先选择一条记录!', 'info');
        }
    }
    function gridTest() {
        var mys = $('#ctable').datagrid("getSelected");
        if (mys != null) {
            window.open("../webpage/lss.aspx?wid=" + mys.wid + "&title=" + mySysDate(mys.name));
        } else {
            $.messager.alert('提示信息', '请先选择一条记录!', 'info');
        }
    }

    function gridFz() {
        var oldwid
        $.messager.prompt("请复制要的wid", "", function (r) {
            
            if (r) {                
                rFZ(r);
            }
        });
    }
    function rFZ(oldwid) {
        
        if (!isNaN(oldwid) && oldwid != "0" && oldwid != "") {
            $.ajax({ type: 'post',
                url: '../webuser/ws.asmx/websj_fz',
                data: { wid: oldwid },
                error: function (e) {
                    $.messager.alert('提示信息', '连接失败!', 'info');
                },
                success: function (data) {
                    var r = myAjaxData(data);
                    if (r.r == 'true') {
                        $.messager.alert('提示信息', '复制成功!', 'info', function () { $('#ctable').datagrid("reload"); });
                    } else {
                        $.messager.alert('提示信息', '复制失败!', 'info');
                    }
                }
            })

        } else {
            $.messager.alert('提示信息', '复制wid无效!', 'info');
        }
    }
    function gridDel() {
        $.messager.alert('提示信息', '不允许删除!', 'info');
        return false;//可用,但不用

        var mys = $('#ctable').datagrid("getSelected");
        if (mys != null) {
            $.messager.confirm('提示信息', '确定要删除吗?', function (r) {
                if (r) {
                    $.ajax({ type: 'post',
                        url: '../webuser/ws.asmx/websj_del',
                        data: { wid: mys.wid },
                        error: function (e) {
                            $.messager.alert('提示信息', '连接失败!', 'info');
                        },
                        success: function (data) {
                            var r = myAjaxData(data);
                            if (r.r == 'true') {
                                $.messager.alert('提示信息', '删除成功!', 'info', function () { $('#ctable').datagrid("reload"); });
                            } else {
                                $.messager.alert('提示信息', '删除失败!', 'info');
                            }
                        }
                    })
                }
            });

        } else {
            $.messager.alert('提示信息', '请先选择一条记录!', 'info');
        }
    }

    function getCx() {
        var id = "";
        var node = $('#leftmenu').tree("getSelected");
        if (IsNum(node.id)) {//-1是人员信息!
            //alert(node.attributes.type)
            if (node.attributes != undefined) {
                if (node.attributes.type != null && node.attributes.type == "user") {//如果是用户而不是部门树
                    getCtable(node.id, document.getElementById("lx").value, document.getElementById("js").value, document.getElementById("sql").value, document.getElementById("wid").value, document.getElementById("myname").value);  // alert node text property when clicked
                }
            }
        }        
    }
    function getCtable(id,lx,js,sql,wid,myname) {
        //url: 'web_xtsz_main.ashx',
        $('#ctable').datagrid('load',{
            'type': 'GetCTable', 'id': id, 'lx': mySysDate(lx), 'js': mySysDate(js), 'sql': mySysDate(sql), 'wid': mySysDate(wid), 'myname': mySysDate(myname)
        });              
    }

    function ALink(type, id, value) {
        //alert(value);
        // alert(id);       
        var url = "web_xtsz_main_edit.aspx?wid=" + id + "&title=" + mySysDate(value);
        window.open(url);
    }

    /*单击树*/
    $('#leftmenu').tree({
        onClick: function (node) {
            if (IsNum(node.id)) {//-1是人员信息!
                //alert(node.attributes.type)
                if (node.attributes != undefined) {
                    if (node.attributes.type != null && node.attributes.type == "user") {//如果是用户而不是部门树
                        getCtable(node.id, document.getElementById("lx").value, document.getElementById("js").value, document.getElementById("sql").value, document.getElementById("wid").value, document.getElementById("myname").value);  // alert node text property when clicked
                    }
                }
            }
        }
    });
    /*等待框*/
    function waitOff() {
        $("#Loading").fadeOut("normal", function () {
            $(this).remove();
        });
    }

    var pc;
    $.parser.onComplete = function () {
        if (pc) clearTimeout(pc);
        pc = setTimeout(waitOff, 1000);
    }

</script>
