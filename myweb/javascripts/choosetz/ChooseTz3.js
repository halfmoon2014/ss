require.config({
    paths: {
        "jquery": "../jquery/1.12.4/jquery.min",
        "utils": "../utilsA",
        "myweb": "../myjs/mywebA"
    }
})

require(["jquery", "utils", "myweb"], function ($, utils, myweb) {

    $('.tzlist').delegate('a', 'click', function (e) {
        var tzid, menu;
        if ($(e.target).is("a")) {
            tzid = $(e.target).attr("t");
            menu = $(e.target).attr("m");
        } else {
            tzid = $(e.target).parent().attr("t");
            menu = $(e.target).parent().attr("m");
        }
        gotz(tzid, menu);
        return false;
    });

    var gotz = function (tzid, menu) {
        myweb.showLoading();
        $.ajax({
            type: 'post',
            url: 'webuser/WebService.asmx/ChooseTz',
            data: { tzid: tzid, updata: $("#updata").is(':checked') },
            error: function () {
                myweb.hideLoading();
            },
            success: function (data) {
                r = utils.myAjaxData(data);
                if (r.r == "true") {
                    //document.forms[0].action = "webpage/" + menu + ".aspx"
                    //document.forms[0].submit();                    
                    window.location.href = "webpage/" + menu + ".aspx";
                }
            }
        })
    }

   

    //console.log("当js加载成功后会执行的函数");

}, function () {
    //console.log("当js加载失败后会执行的函数");
});

