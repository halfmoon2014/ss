require.config({
    paths: {
        "jquery": "../../javascripts/jquery/1.12.4/jquery.min",
        "utils": "../../javascripts/utilsA",
        "myweb": "../../javascripts/myjs/mywebA",
    }
})
require(["jquery", "utils", "myweb"], function ($, utils, myweb) {

    $("#ok").bind("click", function () { ok_click(); });
    $("#fb").bind("click", function () { fb_click(); });
    $("#showtitp").bind("click", function () { showtitp_click(); });  

    var salert = function (title, text, type, fn) {
        swal({
            title: title,
            text: text,
            type: type,
        }, fn);
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
                salert('提示信息', '连接失败!', 'info', function () {
                    $('#ok').removeAttr("disabled")
                });
            },
            success: function (data) {
                var r = utils.myAjaxData(data);
                if (r.r == 'true') {
                    salert('提示信息', '保存成功!', 'info', function () {
                        $('#ok').removeAttr("disabled")
                    });
                } else {
                    salert('提示信息', '保存失败!', 'info', function () {
                        $('#ok').removeAttr("disabled")
                    });
                }
            }
        })
    }
    //使用了bootstrap样式,需要增加12的高度
    utils.autoTextarea(document.getElementById("tbjs"), 12);// 调用    
    //console.log("当js加载成功后会执行的函数");

}, function () {
    //console.log("当js加载失败后会执行的函数");
});