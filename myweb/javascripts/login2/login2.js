$(function () {
    if (getCookie("lur") != null) {
        $("#usr").attr("value", getCookie("lur"))
        $("#psw").focus();
    } else {
        $("#usr").focus();
    }
    $("#ok").bind("click", function () { okClick(); });
    $("#psw").bind("keydown", function (event) { onKey(event); });
});

function okClick() {
    var lur = $("#usr").attr("value");
    var lps = $("#psw").attr("value");
    if (lur != "" && lps != "") {
        $("#ok").attr("disabled", true);
        $.ajax({ type: 'post',
            url: 'webuser/WebService.asmx/Login',
            data: { ur: lur, ps: lps },           
            error: function () {
                $("#ok").removeAttr("disabled");
                $("#msg").html("连接错误,请检查!");
            },
            success: function (data) {
                r = myAjaxData(data);
                if (r.r == "true") {
                    if ($("#rememberme").get(0).checked) {
                        setCookie("lur", lur)
                    }
                    window.location.href = "ChooseTz";
                } else {
                    $("#ok").removeAttr("disabled");
                    $("#msg").html("登陆失败,请检查用户名与密码是否正确!");
                    $("#psw").focus();
                }
            }
        })
    } else if (lur == "") {        
        $("#msg").html("请输入用户名");
        $("#usr").focus();

    } else if (lps == "") {        
        $("#msg").html("请输入密码!");
        $("#psw").focus();
        
    }
}
//回车执行登陆操作
function onKey(e) {
    var keyn;
    if (browser.versions.trident) {
        keyn = e.keyCode;
    } else if (e.which) {
        keyn = e.which;
    }
    if (keyn == 13) {
        okClick();
    }
}


