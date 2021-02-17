define(["jquery", "utils", "myweb", "swalProcess", "bootstrap"], function ($, utils, myweb, swalProcess) {
    var start = function () {
        $(document).ready(function () {
            $("#ok").bind("click", function () {
                $('#ok').attr('disabled', 'disabled');
                var mc = document.getElementById("mc").value;
                var lx = document.getElementById("lx").value;
                if (mc.length == 0) {
                    swalProcess.sAlert('名称不能为空!', function () {
                        $('#ok').removeAttr("disabled")
                    });
                } else {
                    var userid = document.getElementById("userid").value;
                    var wid = document.getElementById("wid").value;
                    var zt = document.getElementById("zt").value;
                    $.ajax({
                        type: 'post',
                        url: "../webuser/Ws.asmx/WebSjCl",
                        data: { userid: userid, mc: mc, lx: lx, wid: wid, zt: zt },
                        error: function (e) {
                            swalProcess.sAlert('连接失败!', function () {
                                $('#ok').removeAttr("disabled")
                            });
                        },
                        success: function (data) {
                            var r = utils.myAjaxData(data);
                            if (r.r == 'true') {
                                swalProcess.sAlert('保存成功!', function () {
                                    $('#ok').removeAttr("disabled"); myweb.closeWindow("ok");
                                });

                            } else {
                                swalProcess.sAlert('连接失败!', function () {
                                    $('#ok').removeAttr("disabled")
                                });
                            }
                        }
                    })
                }
            });
            $("#close").bind("click", function () {
                myweb.closeWindow();
            });
            utils.hideLoading();
        });
    };
    return {
        start: start
    }
});