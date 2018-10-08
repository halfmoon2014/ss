define(['jquery', 'myweb', 'utils', 'swalProcess', 'easyui'], function ($, myweb, utils, swalProcess) {
    var start = function () {
        $(function () {

            $('#ctable').datagrid({
                title: '页面列表',
                fit: true,
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                collapsible: true,
                url: 'web_xtsz_main.ashx',
                queryParams: { 'type': 'GetCTable', 'id': -1, 'lx': '', 'js': '', 'sql': '', 'wid': '', 'myname': '' },
                columns: [[
                        {
                            field: 'wid', title: 'wid', width: 120
                        },
                        {
                            field: 'cl', title: '处理', width: 120,
                            formatter: function (value, rec, index) {
                                return "<a href=\"#\" class=\"ALink\"  \">" + value + "</a>";
                            }
                        },
                        {
                            field: 'name', title: '名称', width: 250
                        },
                        {
                            field: 'fb', title: '发布', width: 60,
                            formatter: function (value, rec, index) {
                                return "<a href=\"#\" >" + value + "</a>";
                            }
                        },
                        {
                            field: 'lx', title: '分类', width: 80
                        }
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
                }, '-', {
                    id: 'btndel',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        gridDel();
                    }
                }],
                onBeforeLoad: function (p) {
                    if (p.id == -1) return false;
                },
                onClickCell: function (rowIndex, field, value) {
                    var data = $('#ctable').datagrid('getRows')[rowIndex];
                    if (field == "fb") {
                        if (!isNaN(data.wid) && data.wid != "0" && data.wid != "") {
                            $.ajax({
                                type: 'post',
                                url: '../webuser/ws.asmx/web_fb',
                                data: { wid: data.wid },
                                error: function (e) {
                                    swalProcess.sAlert('连接失败!');
                                },
                                success: function (data) {
                                    var r = utils.myAjaxData(data);
                                    swalProcess.sAlert(r.r);
                                }
                            })

                        } else {
                            swalProcess.sAlert('无效!');
                        }
                    } else if (field == "cl") {
                        window.open("web_xtsz_main_edit.aspx?wid=" + data.wid + "&title=" + myweb.mySysDate(data.name))
                    }
                }
            });

            $('#ctable').datagrid('getPager').pagination({
                displayMsg: '当前显示从{from}到{to}共{total}记录',
                onBeforeRefresh: function () { },
                onSelectPage: function (pageNumber, pageSize) {
                    $('#ctable').datagrid('options').pageNumber = pageNumber;
                    $('#ctable').datagrid('options').pageSize = pageSize;
                    $('#ctable').datagrid('reload');
                }
            });

            /*单击树*/
            $('#leftmenu').tree({
                onClick: function (node) {
                    if (utils.isNum(node.id)) {//-1是人员信息!
                        //alert(node.attributes.type)
                        if (node.attributes != undefined) {
                            if (node.attributes.type != null && node.attributes.type == "user") {//如果是用户而不是部门树
                                getCtable(node.id, document.getElementById("lx").value, document.getElementById("js").value, document.getElementById("sql").value, document.getElementById("wid").value, document.getElementById("myname").value);  // alert node text property when clicked
                            }
                        }
                    }
                },
                url: 'web_xtsz_main.ashx?type=GetTree'
            });

            $.parser.onComplete = function () {
                waitOff();
            }

            $('#btn_cx').click(function () {
                var id = "";
                var node = $('#leftmenu').tree("getSelected");
                if (utils.isNum(node.id)) {//-1是人员信息!
                    //alert(node.attributes.type)
                    if (node.attributes != undefined) {
                        if (node.attributes.type != null && node.attributes.type == "user") {//如果是用户而不是部门树
                            getCtable(node.id, document.getElementById("lx").value, document.getElementById("js").value, document.getElementById("sql").value, document.getElementById("wid").value, document.getElementById("myname").value);  // alert node text property when clicked
                        }
                    }
                }
            });
            $.parser.parse($("#mainbody").parent());
        });
    }

    var gridAdd = function () {
        if ($('#leftmenu').tree("getSelected") != null && $('#leftmenu').tree("getSelected").attributes != undefined) {
            if ($('#leftmenu').tree("getSelected").attributes.type != null && $('#leftmenu').tree("getSelected").attributes.type == "user") {//如果是用户而不是部门树
                myweb.openModal("web_xtsz_main_add.aspx?zt=add&userid=" + $('#leftmenu').tree("getSelected").id, "", "dialogWidth=970;dialogHeight=400px;", function (t) {
                    if (t == "ok") {
                        $('#ctable').datagrid("reload");
                    }
                });

            } else {
                swalProcess.sAlert('请先选择人员!');
            }
        } else {
            swalProcess.sAlert('请先选择人员!');
        }
    }

    var gridEdit = function () {
        var mys = $('#ctable').datagrid("getSelected");
        if (mys != null) {

            if ($('#leftmenu').tree("getSelected") != null && $('#leftmenu').tree("getSelected").attributes != undefined) {
                if ($('#leftmenu').tree("getSelected").attributes.type != null && $('#leftmenu').tree("getSelected").attributes.type == "user") {//如果是用户而不是部门树
                    var userid = $('#leftmenu').tree("getSelected").id;
                    var mc = myweb.mySysDate(mys.name);
                    var lx = myweb.mySysDate(mys.lx);
                    myweb.openModal("web_xtsz_main_add.aspx?zt=edit&userid=" + userid + "&wid=" + mys.wid + "&lx=" + lx + "&mc=" + mc, "", "dialogWidth=970px;dialogHeight=400px;", function (r) {
                        if (r == "ok") {
                            $('#ctable').datagrid("reload");
                        }
                    });

                } else {
                    swalProcess.sAlert('请先选择人员!');
                }
            } else {
                swalProcess.sAlert('请先选择人员!');
            }

        } else {
            swalProcess.sAlert('请先选择一条记录!');
        }
    }

    var gridTest = function () {
        var mys = $('#ctable').datagrid("getSelected");
        if (mys != null) {
            window.open("../webpage/lss.aspx?wid=" + mys.wid + "&title=" + myweb.mySysDate(mys.name));
        } else {
            swalProcess.sAlert('请先选择一条记录!');
        }
    }

    var gridFz = function () {
        swalProcess.swal({
            text: '请复制要的wid',
            content: {
                element: "input",
                attributes: {
                    placeholder: "Type your wid",
                },
            },
            button: {
                text: "确定!",
                closeModal: false,
            }
        })
       .then(function (value2) {
           if (!value2) {
               throw null;
           } else {
               rFZ(value2);
           }
       })      
       .catch(function (err) {
           if (err) {
               swalProcess.sAlert('The AJAX request failed!');
           } else {
               swalProcess.swal.stopLoading();
               swalProcess.swal.close();
           }
       });

        //$.messager.prompt("请复制要的wid", "", function (r) {
        //    if (r) rFZ(r);
        //});
    }

    var rFZ = function (oldwid) {

        if (!isNaN(oldwid) && oldwid != "0" && oldwid != "") {
            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/websj_fz',
                data: { wid: oldwid },
                error: function (e) {
                    swalProcess.sAlert('连接失败!');
                },
                success: function (data) {
                    var r = utils.myAjaxData(data);
                    if (r.r == 'true') {
                        swalProcess.sAlert('复制成功!', 'success', function () { $('#ctable').datagrid("reload"); });
                    } else {
                        swalProcess.sAlert('复制失败!');
                    }
                }
            })

        } else {
            swalProcess.sAlert('无效!');
        }
    }

    var gridDel = function () {
        swalProcess.sAlert('不允许删除!');
        return false;//可用,但不用

        //var mys = $('#ctable').datagrid("getSelected");
        //if (mys != null) {
        //    $.messager.confirm('提示信息', '确定要删除吗?', function (r) {
        //        if (r) {
        //            $.ajax({
        //                type: 'post',
        //                url: '../webuser/ws.asmx/websj_del',
        //                data: { wid: mys.wid },
        //                error: function (e) {
        //                    $.messager.alert('提示信息', '连接失败!', 'info');
        //                },
        //                success: function (data) {
        //                    var r = myAjaxData(data);
        //                    if (r.r == 'true') {
        //                        $.messager.alert('提示信息', '删除成功!', 'info', function () { $('#ctable').datagrid("reload"); });
        //                    } else {
        //                        $.messager.alert('提示信息', '删除失败!', 'info');
        //                    }
        //                }
        //            })
        //        }
        //    });

        //} else {
        //    $.messager.alert('提示信息', '请先选择一条记录!', 'info');
        //}
    }

    var getCtable = function (id, lx, js, sql, wid, myname) {
        //url: 'web_xtsz_main.ashx',
        $('#ctable').datagrid('load', {
            'type': 'GetCTable', 'id': id, 'lx': myweb.mySysDate(lx), 'js': myweb.mySysDate(js), 'sql': myweb.mySysDate(sql), 'wid': myweb.mySysDate(wid), 'myname': myweb.mySysDate(myname)
        });
    }

    /*等待框*/
    var waitOff = function () {
        $("#Loading").fadeOut("normal", function () {
            $(this).remove();
        });
    }

    return {
        start: start
    }
});