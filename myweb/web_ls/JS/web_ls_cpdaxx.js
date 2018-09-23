require.config({
    shim: {
        //easyui-lang-zh_CN.js也依赖jquery
        'zhCN': ['jquery'],
        'easyui': ['jquery']
    },
    paths: {
        "jquery": "../../javascripts/jquery/1.12.4/jquery.min",
        "easyui": "../../javascripts/jey/jquery.easyui.min",
        "zhCN": "../../javascripts/jey/easyui-lang-zh_CN",
        "utils": "../../javascripts/utilsA",
        "myweb": "../../javascripts/myjs/mywebA"        
    }
})

require(["jquery", "utils", "myweb", 'zhCN', 'easyui'], function ($, utils, myweb, zhCN, easyui) {

    $("#ok").bind("click", function () { ok_click(); });
    $("#edit").bind("click", function () { edit_click(); });
    $("#del").bind("click", function () { del_click(); });
    $('#xx').tree({
        url: 'web_ls_main.ashx?lx=dl',
        onBeforeLoad: function (node, param) {}
    });

    var ok_click = function () {
        if ($('#xx').tree("getSelected") != null) {//选择了一行
            var id = $.trim($('#xx').tree("getSelected").id);
            var t = openModal("web_ls_cpdaxx_add.aspx?lx=dl&zt=add&id=" + id, "", "dialogWidth=850px;dialogHeight=600px", function (r) {
                if (r == "ok") {
                    $('#xx').tree("reload");
                }
            });

        } else {
            utils.sAlert("请先选择一个大类!");
        }
    }
    var edit_click = function () {
        if ($('#xx').tree("getSelected") != null) {
            var id = $.trim($('#xx').tree("getSelected").id);
            var mc = $.trim($('#xx').tree("getSelected").text)
            //alert(mc);
            //alert($('#xx').tree("getSelected").text)
            //alert(mySysDate(mc))           
            if (id == "-1") {//没有大类自动生成               
                utils.sAlert("不能修改这个大类!");
            } else {
                //alert("web_ls_cpdaxx_add.aspx?lx=dl&zt=edit&mc=" + mc + "&id=" + id);
                var t = openModal("web_ls_cpdaxx_add.aspx?lx=dl&zt=edit&mc=" + mySysDate(mc) + "&id=" + id, "", "dialogWidth=850px;dialogHeight=400px", function (r) {
                    if (r == "ok") { $('#xx').tree("reload"); }
                });

            }
        } else {
            utils.sAlert("请先选择一个大类!");
        }

    }
    var del_click = function () {

        if ($('#xx').tree("getSelected") != null) {
            var id = $.trim($('#xx').tree("getSelected").id);
            if ($('#xx').tree("getSelected").id == "-1") {//没有大类自动生成                
                utils.sAlert("不能删除这个大类!");
            } else if ($('#xx').tree("getSelected").attributes.xjbs == "1") {//有下级              
                utils.sAlert("此大类有下级类别,不能删除!");
            } else {
                $('#del').attr('disabled', 'disabled');
                //xjbs 下级标识
                var str  = "select top 1 khlb from V_ls_cpda where khlb=" + id
                //alert(str);
                var r = myAjax(str);
                if (r == -1) {
                    utils.sAlert("连接失败!", function () {
                        $('#del').removeAttr("disabled")
                    });
                } else {
                    if (r.r == 'true' && r.msg == "null") {
                        //允许删除                        
                        str = " delete from v_ls_xxdmb where id=" + id;
                        if (id == "") {
                            utils.sAlert("没有可更新的记录!", function () {
                                $('#del').removeAttr("disabled")
                            });                            
                        } else {
                            r = myAjax(str);
                            if (r == -1) {                               
                                utils.sAlert("连接失败!", function () {
                                    $('#del').removeAttr("disabled")
                                });

                            } else {
                                if (r.r == 'true') {
                                    utils.sAlert('删除成功!', 'success', function () {
                                        $('#del').removeAttr("disabled")
                                        $('#xx').tree("reload");
                                    });

                                } else {
                                    utils.sAlert(r.msg, function () {
                                        $('#del').removeAttr("disabled")
                                    });
                                }
                            }
                        }
                    } else {
                        utils.sAlert("已经被产品档案引用,不能删除", function () {
                            $('#del').removeAttr("disabled")
                        });
                    }
                }
            }
        } else {
            utils.sAlert('提示信息', '请先选择一个大类!');
        }
    }
});