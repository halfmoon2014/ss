$(function () {
    if (getCookie("lur") != null) {
        //document.getElementById('usr').value = getCookie("lur");
        $("#usr").attr("value", getCookie("lur"))
        //document.getElementById('psw').focus();
        $("#psw").focus();

    } else {
        //document.getElementById('usr').focus();
        $("#usr").focus();
    }
    /*($.extend($.fn.validatebox.defaults.rules, {
    NoEmpty: {
    validator: function (value, param) {
    return value.length > param[0];
    },
    message: '不能为空'
    }
    });*/
    
    $("#ok").bind("click", function () { okClick(); });
    $("#esc").bind("click", function () { esc(); });
    $("#psw").bind("keydown", function (event) { onKey(event); });

});

function esc() {
    //document.getElementById('psw').value = "";
    //document.getElementById('usr').value = "";
    $("#usr").attr("value", "")
    $("#psw").attr("value", "")
    document.getElementById('usr').focus();
}
function okClick() {
    var lur = $("#usr").attr("value");
    var lps = $("#psw").attr("value");
    if (lur != " " && lps != "") {
        $('#ok').linkbutton('disable');
        $('#esc').linkbutton('disable');

        $.ajax({ type: 'post',
            url: 'webuser/WebService.asmx/Login',
            data: { ur: mySysDate(lur), ps: mySysDate(lps) },
            error: function () {
                $.messager.alert('提示信息', '连接错误,请检查!', 'info', function () {
                    $('#ok').linkbutton('enable');
                    $('#esc').linkbutton('enable');
                });
            },
            success: function (data) {
                r = myAjaxData(data);
                if (r.r == "true") {
                    setCookie("lur", lur)
                    window.location.href = "ChooseTz.aspx";
                } else {
                    $.messager.alert('提示信息', '登陆失败,请检查用户名与密码是否正确!', 'info', function () {
                        $('#ok').linkbutton('enable');
                        $('#esc').linkbutton('enable');
                        $("#psw").focus();
                    });

                }
            }
        })
    } else if (lur == "") {
        // alert("请输入用户名")
        $.messager.alert('提示信息', '请输入用户名!', 'info', function () {
            $("#usr").focus();
        });
    } else {
        //  alert("请输入密码")
        $.messager.alert('提示信息', '请输入密码!', 'info', function () {
            $("#psw").focus();
        });
    }
}

function onKey(e) {

    var keyn;
    if (browser.versions.trident) {
        keyn = e.keyCode; 
    } else if (e.which) {
        keyn = e.which;
    }

    if (keyn == 13) {

        ok_click();
    }

}

/*
function setSessionValue(newValue) {
__doPostBack('SetSessionPostBack', newValue);
}*/


