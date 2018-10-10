define(["jquery", "utils"], function ($, utils) {
    var start = function () {
        $(document).ready(function () {
            $("#ok").removeAttr("disabled");
            utils.hideLoading();
            if (utils.getCookie("lur") != null) {
                $("#usr").attr("value", utils.getCookie("lur"))
                $("#psw").focus();
            } else {
                $("#usr").focus();
            }
            if (utils.getCookie("rememberme") != null) {
                $("#rememberme").prop('checked', true)
            }

            $("#ok").click(function () {
                var lur = $("#usr").val();
                var lps = $("#psw").val();
                if (lur != "" && lps != "") {
                    $("#ok").attr("disabled", true);
                    utils.showLoading();
                    $.ajax({
                        type: 'post',
                        url: 'webuser/WebService.asmx/Login',
                        data: { ur: lur, ps: lps },
                        error: function () {
                            utils.hideLoading();
                            $("#ok").removeAttr("disabled");
                            $(".alert").show();
                            $("#msg").html("连接错误,请检查!");
                        },
                        success: function (data) {
                            r = utils.myAjaxData(data);
                            if (r.r == "true") {
                                if ($("#rememberme").get(0).checked) {
                                    utils.setCookie("lur", lur)
                                    utils.setCookie("rememberme", "true");
                                } else {
                                    utils.delCookie("lur")
                                    utils.delCookie("rememberme");
                                }
                                window.location.href = "ChooseTz";
                            } else {
                                utils.hideLoading();
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
            });
            $("#psw").bind("keydown", function (event) {
                var keyn;
                if (utils.browser.versions.trident) {
                    keyn = event.keyCode;
                } else if (event.which) {
                    keyn = event.which;
                }
                if (keyn == 13) {
                    okClick();
                }
            });                       
        });        
    }
    return {
        start:start
    }
});

