require.config({
    paths: {
        "jquery": "../../javascripts/jquery/1.12.4/jquery.min",
        "utils": "../../javascripts/utilsA",
        "myweb": "../../javascripts/myjs/mywebA"
    }
})

require(["jquery", "utils", "myweb"], function ($, utils, myweb) {
    
    $("#ok").bind("click", function () { ok_click(); });
    $("#close").bind("click", function () { close_click(); });    

    var close_click=function(){
        myweb.myWindowClose()
    }

    var ok_click = function () {        
        $('#ok').attr('disabled', 'disabled');
        var mc = $.trim(document.getElementById("mc").value).replace(/'/g, "''");
        if (mc.length == 0) {
            utils.sAlert("名称不能为空!", function () {
                $('#ok').removeAttr("disabled")
            });
        } else {
            var id = $.trim(document.getElementById("myid").value);
            var lx = $.trim(document.getElementById("lx").value);
            var zt = $.trim(document.getElementById("zt").value);
            var str = "";
            if (zt == "add") {
                if (id == "-1") {//如果什么都有进来新增的
                    str = " insert v_ls_xxdmb (mc,lx,ssid) values ('" + mc + "','" + lx + "',0)";
                }
                else {
                    str = " insert v_ls_xxdmb (mc,lx,ssid) values ('" + mc + "','" + lx + "','" + id + "')";
                }
            } else if (zt == "edit") {
                str = " update v_ls_xxdmb set mc='" + mc + "' where lx='" + lx + "' and  id=" + id;
            }

            if (str == "") {
                utils.sAlert("没有可更新的记录!", function () {
                    $('#ok').removeAttr("disabled")
                });
            } else {
                var r = myAjax(str);
                if (r == -1) {
                    utils.sAlert("连接失败!", function () {
                        $('#ok').removeAttr("disabled")
                    });

                } else {
                    if (r.r == 'true') {
                        utils.sAlert('保存成功!', 'success', function () {
                            $('#ok').removeAttr("disabled")
                            myweb.myWindowClose("ok")
                        });
                    } else {
                        utils.sAlert(r.msg, function () {
                            $('#ok').removeAttr("disabled")
                        });
                    }
                }
            }
        }
    }

});