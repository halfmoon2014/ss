/*
 * 进程守护
*/
//session检查
function checkSession() {
    var r = "";
    var err = "";
    $.ajax({
        type: 'post',
        url: '../webuser/ws.asmx/CheckSession',
        async: false,
        data: {},
        error: function (e) { err = e; },
        success: function (data) {
            r = myAjaxData(data);
        }
    });
    if (err == "") {
        if (r.r == "") {
            return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
}

function checkSessionAsy(fn) {  
    $.ajax({
        type: 'post',
        url: '../webuser/ws.asmx/CheckSession',
        async: true,
        data: {},
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var o = {}; o.errcode = 100;
            o.errmsg = 'XMLHttpRequest.status: ' + XMLHttpRequest.status + "$" + 'XMLHttpRequest.readyState: ' + XMLHttpRequest.readyState + "$" + 'textStatus: ' + textStatus + "$" + 'errorThrown: ' + errorThrown;
            fn(o);
        },
        success: function (data) {
            var o = {}; o.errcode = 0;
            if (myAjaxData(data).r == "") {
                o.data = true;
            } else {
                o.data = false;
            }
            fn(o);
        }
    }); 
}

//重新登陆
function reLoad(callFun) {
    if (typeof (callFun) == "function") {
        if (callFun.name == "") {
            //匿名函数
        } else {
            //有名函数
        }
    } else if (typeof (callFun) == "object") {
        //一个对象,构造一个字符串函数体
        var fNmae = callFun.functionName;
        var fArg = "";
        for (var i = 0; i < callFun.args.length; i++) {
            fArg += "'" + callFun.args[i] + "',";
        }
        if (fArg.length > 0) {
            fArg = fArg.substring(0, fArg.length - 1);
        }
        callFun = "function(){" + fNmae + "(" + fArg + ")}";
    } else {
        //其它
        callFun = "";
    }
    if (browser.versions.trident) {
        reLoad2(callFun);
    } else {
        reLoadSwal($("#username").val(), $("#username").attr("a"), $("#username").attr("b"), callFun)
    }
}

function reLoadSwal(username, a, b, callFun) {

    swal({
        text: username + '你好,请输入你的密码',
        content: {
            element: "input",
            attributes: {
                placeholder: "Type your password",
                type: "password",
            },
        },
        button: {
            text: "确定!",
            closeModal: false,
        },
        closeOnClickOutside: false,
        closeOnEsc: false,
    })
     .then(function(value2) {
         if (!value2) {             
             throw null;
         } else {
             return fetch("../webuser/ws.asmx/reloadJson", {
                 method: "POST",
                 credentials: 'include',
                 headers: {
                     "Content-Type": "application/x-www-form-urlencoded"
                 },
                 body: "value1=" + username + "&value2=" + value2 + "&a=" + a + "&b=" + b
             });
         }
     })
     .then(function(results) {
         return results.json();
     })
     .then(function(json){
         if (json.r) {
             sAlert("登陆成功", "success", function () {
                 if (typeof (callFun) == "function") {
                     callFun();
                 }
             });
         } else {
             sAlert('登陆失败,请检查密码是否正确!', function () { reLoadSwal(username, a, b, callFun) });
         }
     })
     .catch(function(err){
         if (err) {
             sAlert('The AJAX request failed!');
         } else {
             swal.stopLoading();
             swal.close();
         }
     });

}

function reLoad2(callFun) {
    if (typeof (callFun) == "function") {
        if (callFun.name == "") {
            //匿名函数
        } else {
            //有名函数
        }
    } else if (typeof (callFun) == "object") {
        //一个对象,构造一个字符串函数体
        var fNmae = callFun.functionName;
        var fArg = "";
        for (var i = 0; i < callFun.args.length; i++) {
            fArg += "'" + callFun.args[i] + "',";
        }
        if (fArg.length > 0) {
            fArg = fArg.substring(0, fArg.length - 1);
        }
        callFun = "function(){" + fNmae + "(" + fArg + ")}";
    } else {
        //其它
        callFun = "";
    }
    var username = "";
    if ($("#username").length > 0) {
        username = $("#username").val();
    }

    if ($("#div_SysSession").length == 0) {
        var rs = " <div id=\"div_SysSession\" style=\" width:100%;height:100%; overflow: auto;display: none;\">  "

        rs += "  <div id=\"SysSessionMask\" style=\"top: 0; left: 0; position: absolute; z-index: 1000;\"  class=\"SysFindSortMaskclass\"></div>"

        rs += "   <div  id='div_SessionWindow' inline=\"true\" title=\"登陆\" class=\"easyui-window\" data-options=\"collapsible:false,minimizable:false,maximizable:false,onClose:function(){$('#div_SysSession').hide();} \" style=\"width: 400px; height: 200px;\">"
        rs += "     <table style=\" width:100%;height:100% \" cellspacing=\"0\" cellpadding=\"0\" border=\"0\">   "
        rs += "       <tr><td>"
        rs += "                                     <table align=\"center\"> ";
        rs += "                                         <tr align=\"center\"> ";
        rs += "                                             <td style='width: 14px'> ";
        rs += "                                                 &nbsp; ";
        rs += "                                             </td> ";
        rs += "                                             <td align=\"right\"> ";
        rs += "                                                 用户名 ";
        rs += "                                             </td> ";
        rs += "                                             <td align=\"left\"> ";
        rs += "                                                 <input type=\"text\" value='" + username + "' id=\"session_usr\" class=\"easyui-validatebox\"  ";
        rs += "                                                     style='width: 140px; height: 22px;' disabled  /> ";
        rs += "                                             </td> ";
        rs += "                                             <td> ";
        rs += "                                                 &nbsp; ";
        rs += "                                             </td> ";
        rs += "                                         </tr> ";
        rs += "                                         <tr align=\"center\"> ";
        rs += "                                             <td style='width: 14px'> ";
        rs += "                                                 &nbsp; ";
        rs += "                                             </td> ";
        rs += "                                             <td align=\"right\"> ";
        rs += "                                                 密码 ";
        rs += "                                             </td> ";
        rs += "                                             <td align=\"left\"> ";
        rs += "                                                 <input type=\"password\" id=\"session_psw\" class=\"easyui-validatebox\" data-options=\"required:true,missingMessage:'密码能为空'\" ";
        rs += "                                                     style='width: 140px; height: 22px;' /> ";
        rs += "                                             </td> ";
        rs += "                                             <td> ";
        rs += "                                                 &nbsp; ";
        rs += "                                             </td> ";
        rs += "                                         </tr> ";
        rs += "                                     </table> ";
        rs += "                                     <table align=\"center\"> ";
        rs += "                                         <tr align=\"center\"> ";
        rs += "                                             <td style='width: 45px'> ";
        rs += "                                                 &nbsp; ";
        rs += "                                             </td> ";
        rs += "                                             <td align=\"center\"> ";
        rs += "                                                 <a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" onclick=\"sessionOk(" + callFun + ")\" id='ok'  > ";
        rs += "                                                     确定</a> ";
        rs += "                                             </td> ";
        rs += "                                             <td align=\"center\"> ";
        rs += "                                                 <a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" onclick=\"sessionEsc()\" id='esc' >清除</a> ";
        rs += "                                             </td> ";
        rs += "                                         </tr> ";
        rs += "                                     </table> ";

        rs += "        </td></tr>"
        rs += "    </table> "

        rs += "   </div> "
        rs += " </div>"
        var bodyId = document.body.id;
        $("#" + bodyId).append(rs); $.parser.parse($('#div_SessionWindow').parent());
    }
    $("#div_SysSession").show();
    $('#div_SessionWindow').window('open');
    $("#session_psw").focus();

}

//确定登陆
function sessionOk(callFun) {

    var pw = "";
    if ($("#session_psw").length > 0) {
        pw = $("#session_psw").val();
    }
    var usr = "";
    if ($("#session_usr").length > 0) {
        usr = $("#session_usr").val();
    }
    var a = ""; var b = "";
    if ($("#username").length > 0) {
        a = $("#username").attr("a");
        b = $("#username").attr("b");
    }

    if (pw != "") {
        $('#ok').linkbutton('disable');
        $('#esc').linkbutton('disable');
        var err = "";
        $.ajax({
            type: 'post',
            url: '../webuser/ws.asmx/reload',
            async: false,
            data: { value1: (usr), value2: (pw), a: (a), b: (b) },
            error: function (e) { err = e; },
            success: function (data) {
                r = myAjaxData(data);
            }
        });
        if (err == "") {
            if (r.r == "ture") {
                sAlert("登陆成功", "success", function () {
                    $('#ok').linkbutton('enable');
                    $('#esc').linkbutton('enable');
                    $("#session_psw").val("");
                    $('#div_SysSession').hide();
                    if (typeof (callFun) == "function") {
                        callFun();
                    }
                });
            } else {
                sAlert('登陆失败,请检查密码是否正确!', function () {
                    $('#ok').linkbutton('enable');
                    $('#esc').linkbutton('enable');
                    $("#session_psw").focus();
                });
            }
        } else {
            sAlert('登陆失败,请联系管理员!', function () {
                $('#ok').linkbutton('enable');
                $('#esc').linkbutton('enable');
                $("#session_psw").focus();
            });
        }
    } else {
        sAlert('密码不能为空!', function () {
            $("#session_psw").focus();
        });
    }

}

function sessionEsc() {
    if ($("#session_psw").length > 0) {
        $("#session_psw").val("");
    }
}