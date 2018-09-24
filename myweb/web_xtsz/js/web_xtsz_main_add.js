require.config({
    paths: {
        "jquery": "../../javascripts/jquery/1.12.4/jquery.min",
        "utils": "../../javascripts/utilsA",
        "myweb": "../../javascripts/myjs/mywebA"
    }
})
require(["jquery", "utils", "myweb"], function ($, utils, myweb) {
    $("#ok").bind("click", function () { ok_click(); });
    var ok_click = function () {
        $('#ok').attr('disabled', 'disabled');
        var mc = document.getElementById("mc").value;
        var lx = document.getElementById("lx").value;
        if (mc.length == 0) {
            utils.sAlert('名称不能为空!', true, function () {
                $('#ok').removeAttr("disabled")
            });
        } else {
            var userid = document.getElementById("userid").value;
            var wid = document.getElementById("wid").value;
            var zt = document.getElementById("zt").value;
            $.ajax({
                type: 'post',
                url: "../webuser/ws.asmx/websj_cl",
                data: { userid: userid, mc: mc, lx: lx, wid: wid, zt: zt },
                error: function (e) {
                    utils.sAlert('连接失败!', true, function () {
                        $('#ok').removeAttr("disabled")
                    });
                },
                success: function (data) {
                    var r = utils.myAjaxData(data);
                    if (r.r == 'true') {
                        utils.sAlert('保存成功!', true, function () {
                            $('#ok').removeAttr("disabled"); myweb.closeWindow("ok");
                        });

                    } else {
                        utils.sAlert('连接失败!', true, function () {
                            $('#ok').removeAttr("disabled")
                        });
                    }
                }
            })
        }
    }
    //console.log("当js加载成功后会执行的函数");

}, function () {
    //console.log("当js加载失败后会执行的函数");
});