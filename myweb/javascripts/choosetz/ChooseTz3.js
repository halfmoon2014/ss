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
        //alert(tzid);
        //return false;
        gotz(tzid,menu);

    })


    //$("[mylink='1']").bind("click", function (e) {
    //    gotz($(e.target).parent().attr("t"), $(e.target).parent().attr("m"));
    //})

    var gotz = function (tzid, menu) {
        document.getElementById("tzid").value = tzid;
        document.getElementById("menu").value = menu;

        $.ajax({
            type: 'post',
            url: 'webuser/WebService.asmx/ChooseTz',
            data: { tzid: myweb.mySysDate(tzid) },
            error: function () {
            },
            success: function (data) {
                r = utils.myAjaxData(data);
                if (r.r == "true") {
                    window.location.href = "webpage/" + menu + ".aspx";
                }
            }
        })
    }
    //console.log("当js加载成功后会执行的函数");

}, function () {
    //console.log("当js加载失败后会执行的函数");
});

