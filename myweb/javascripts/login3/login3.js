require.config({
    paths: {
        "jquery": "../jquery/1.12.4/jquery.min",
        "utils": "../utilsA",
        "myweb": "../myjs/mywebA"
    }
})

require(["jquery", "utils","myweb"], function ($, utils,myweb) {

    if (utils.getCookie("lur") != null) {
        $("#usr").attr("value", utils.getCookie("lur"))
        $("#psw").focus();
    } else {
        $("#usr").focus();
    }
    $("#ok").click(function () {
        okClick();
    });
    $("#psw").bind("keydown", function (event) {
        var keyn;
        if (utils.getUserBrowser() == "IE") {
            keyn = event.keyCode;
        } else if (event.which) {
            keyn = event.which;
        }
        if (keyn == 13) {
            okClick();
        }
    });

    var okClick = function () {
        var lur = $("#usr").val();
        var lps = $("#psw").val();
        if (lur != "" && lps != "") {
            $("#ok").attr("disabled", true);
            myweb.showLoading();
            $.ajax({
                type: 'post',
                url: 'webuser/WebService.asmx/Login',
                data: { ur: lur, ps: lps },
                error: function () {
                    myweb.hideLoading();
                    $("#ok").removeAttr("disabled");
                    $(".alert").show();
                    $("#msg").html("连接错误,请检查!");
                },
                success: function (data) {
                    r = utils.myAjaxData(data);
                    if (r.r == "true") {
                        if ($("#rememberme").get(0).checked) {
                            utils.setCookie("lur", lur)
                        }
                        window.location.href = "ChooseTz.aspx";
                    } else {
                        myweb.hideLoading();
                        $("#ok").removeAttr("disabled");
                        $(".alert").show();
                        $("#msg").html("登陆失败,请检查用户名与密码是否正确!");
                        $("#psw").focus();
                    }
                }
            })
        } else if (lur == "") {
            $(".alert").show();
            $("#msg").html("请输入用户名");
            $("#usr").focus();            

        } else if (lps == "") {
            $(".alert").show();
            $("#msg").html("请输入密码!");
            $("#psw").focus();            
        }
    }
    //console.log("当js加载成功后会执行的函数");

}, function () {
    //console.log("当js加载失败后会执行的函数");
});

