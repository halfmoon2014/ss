define(["jquery", "utils", "myweb", "swalProcess"], function ($, utils, myweb, swalProcess) {
    var start = function () {
        $(function () {
            $("#ok").bind("click", function () { ok_click(); });
            $("#fb").bind("click", function () { fb_click(); });
            $("#showtitp").bind("click", function () { showtitp_click(); });
            //使用了bootstrap样式,需要增加12的高度
            utils.autoTextarea(document.getElementById("tbjs"), 12);// 调用    
        });
    }
    //显示提示
    var showtitp_click = function () {
        var show = $('#formts').css('display');
        if (show == 'block') {
            $('#formts').hide();
        } else {
            $('#formts').show();
            //使用了bootstrap样式,需要增加12的高度            
            utils.autoTextarea(document.getElementById("ts"), 12);// 调用
        }
    }
    var ok_click = function () {        
        $('#ok').attr('disabled', 'disabled');
        var js = myweb.mySysDate(document.getElementById("tbjs").value);
        var wid = document.getElementById("wid").value;
        $.ajax({
            type: 'post',
            url: '../webuser/ws.asmx/sjy_upjs',
            data: { wid: wid, js: js },
            error: function (e) {
                swalProcess.sAlert( '连接失败!',  function () {
                    $('#ok').removeAttr("disabled")
                });
            },
            success: function (data) {
                var r = utils.myAjaxData(data);
                if (r.r == 'true') {
                    swalProcess.sAlert('保存成功!', "success", function () {
                        $('#ok').removeAttr("disabled")
                    });
                } else {
                    swalProcess.sAlert('保存失败!',  function () {
                        $('#ok').removeAttr("disabled")
                    });
                }
            }
        })
    }
    return {
        start:start
    }
});