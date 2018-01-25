require.config({
    paths: {
        "jquery": "../jquery/1.12.4/jquery.min",
        "utils": "../utilsA",
        "myweb": "../myjs/mywebA"
    }
})

require(["jquery", "utils", "myweb"], function ($, utils, myweb) {

    $("#ok").bind("click", function () { mysave(); });
    $("#esc").bind("click", function () { window.close(); });
    //使用了bootstrap样式,需要增加12的高度
    utils.autoTextarea(document.getElementById("helpText"), 12);
    var mysave = function () {
        var reg = new RegExp("\r\n", "g");
        var help = myweb.mySysDate(document.getElementById("helpText").value);
        var myid = document.getElementById("myid").value;        
        $('#ok').attr('disabled', 'disabled');
        $.ajax({
            type: 'post',
            url: '../webuser/ws.asmx/HelpUp',
            data: { value1: help, value2: myid },
            error: function (e) {
                utils.sAlert('连接失败', true, function () {
                    $('#ok').removeAttr("disabled")
                });
            },
            success: function (data) {
                var r = utils.myAjaxData(data);
                if (r.r == 'true') {
                    utils.sAlert('保存成功!', true, function () {
                        $('#ok').removeAttr("disabled")
                    });

                } else {
                    utils.sAlert('保存失败!', true, function () {
                        $('#ok').removeAttr("disabled")
                    });
                }
            }
        })

    }
    //console.log("当js加载成功后会执行的函数");

}, function () {
    //console.log("当js加载失败后会执行的函数");
});
