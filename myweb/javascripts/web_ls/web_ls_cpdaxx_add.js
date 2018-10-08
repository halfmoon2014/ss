define(["jquery",  "myweb", "swalProcess"], function ($,  myweb, swalProcess) {
    var start = function () {
        $("#ok").bind("click", function () {
            $('#ok').attr('disabled', 'disabled');
            var mc = $.trim(document.getElementById("mc").value).replace(/'/g, "''");
            if (mc.length == 0) {
                swalProcess.sAlert("名称不能为空!", function () {
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
                    swalProcess.sAlert("没有可更新的记录!", function () {
                        $('#ok').removeAttr("disabled")
                    });
                } else {
                    var r = myweb.myAjax(str);
                    if (r == -1) {
                        swalProcess.sAlert("连接失败!", function () {
                            $('#ok').removeAttr("disabled")
                        });

                    } else {
                        if (r.r == 'true') {
                            swalProcess.sAlert('保存成功!', 'success', function () {
                                $('#ok').removeAttr("disabled")
                                myweb.closeWindow("ok")
                            });
                        } else {
                            swalProcess.sAlert(r.msg, function () {
                                $('#ok').removeAttr("disabled")
                            });
                        }
                    }
                }
            }
        });
        $("#close").bind("click", function () {
            myweb.closeWindow();
        });
    }
    return {
        start: start
    }
});