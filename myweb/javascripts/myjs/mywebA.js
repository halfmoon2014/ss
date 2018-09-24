define(['jquery', 'utils'], function ($, utils) {
    /*
    *前台传递到后台,URL的参数 编码格式,与之对应通用反编码
    *用来编码URL中的参数
    */
    var mySysDate = function (str) {
        return encodeURIComponent(str);
    };
    var unMySysDate = function (str) {
        return decodeURIComponent(str);
    };

    /*
    *用于使用平台脚本打开窗口的情况
    *兼容chrome 因为没有模态窗口,chrome 使用open 打开新窗口,所以在关闭的时候注意执行父窗口的函数
    */
    var closeWindow = function (returnvalue) {
        if (utils.browser.versions.webKit) {
            //用来关闭chrome窗口时标识关闭的动作是否使用浏览器自带的关闭按钮
            //任何关闭的动作都会响应onunload事件
            if ("张茂洪" == "张茂洪") {
                parent.document.getElementById("platDialog").close();
                (parent.window.platDialogCallback != undefined) ? parent.window.platDialogCallback(returnvalue) : "";
                parent.document.getElementById("platIframe").src = "about:blank";
            } else {
                window.onunloadtag = true;
                (window.opener && window.opener.callback != undefined) ? window.opener.callback(returnvalue) : "";
                window.close();
            }
        } else {
            window.returnValue = returnvalue
        }
        window.close()
    };
   
    return {   
        mySysDate: mySysDate,
        unMySysDate: unMySysDate,
        closeWindow: closeWindow
    };


    /*
    *url地址
    */
    var mySysUrl = function (str) {
        return encodeURI(str);
    };
    /*
    *打开模态窗口
    */
    var openModal = function (url, varargin, varoptions, callback) {
        if (checkSession() == false) {
            reLoad();
        } else {
            /*如果使用SESSION判断,那么会因为构造HTML标签回调函数的参数会丢失*/
            var width = screen.availWidth - 50;
            var height = screen.availHeight - 150;
            //支持模态窗口浏览器中,代表传递的参数
            varargin = (varargin == undefined ? "" : varargin)
            //新窗口特性
            varoptions = (varoptions == undefined ? "" : varoptions)

            if (browser.versions.webKit) {
                //将回调函数放入当前窗体的属性中,用于子窗口调用
                //缺点是要控制一次只能打开一个子窗口
                if (callback != undefined) { window.callback = callback; }
                //处理模态窗口特性与open窗口特性不同
                if (varoptions == "") {
                    varoptions = "width=" + width + "px,height=" + height + "px";
                } else {
                    varoptions = varoptions.replace(/dialogWidth/g, "width").replace(/dialogHeight/g, "height").replace(/;/g, ",");
                }
                window.open(url, "", varoptions);
            } else {
                if (varoptions == "") {
                    varoptions = "dialogWidth=" + width + "px;dialogHeight=" + height + "px";
                }
                var r = window.showModalDialog(url, varargin, varoptions);
                if (callback) {
                    callback(r);
                } else {//如果没传入回调函数,IE状态下返回结果
                    return r;
                }
            }
        }

    };

    /*
    *用于使用平台脚本打开窗口的情况
    */
    window.onunload = function () {
        if (browser.versions.webKit) {
            if (window.onunloadtag != true) {
                //判断window.opener是否存在,因为这是个通用JS,所以有些窗口不是通过平台脚本打开的
                (window.opener && window.opener.callback != undefined) ? window.opener.callback(null) : "";
            }
        }
    };

    var openWin = function (url,obj) {
        window.open(url);
    }

    /*
    *
    */
    var sqlFormat = function (str) {
        return $.trim(str).replace(/'/g, "''");
    };
    /*
    *返回是一个JSON r.r,r.msg
    *同步
    *xact_abort事务回滚,取值范围[on,off,空],空的时候不加事务回滚
    */
    var myAjax = function (sqlCommand, xact_abort) {
        if (xact_abort == undefined) {
            //没有传递第二个参数
            xact_abort = "";
        } else if ($.trim(xact_abort) != "off" && $.trim(xact_abort) != "on") {
            //传递的参数不符合
            xact_abort = "";
        }
        var r = -1;
        var wid = -1;
        if (this.wid != undefined) {
            //如果窗口存在id为wid的控件
            wid = this.wid.value;
        }

        $.ajax({
            type: 'post',
            url: '../webuser/ws.asmx/execSqlCommand',
            async: false,
            data: { sqlCommand: sqlCommand, xact_abort: xact_abort, wid: wid },
            error: function (e) { },
            success: function (data) {
                r = myAjaxData(data);
                r.msg = decodeURIComponent(r.msg);
            }
        });

        return r;
    };


    var reLoad = function (callFun) {
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
            var rs = " <div id=\"div_SysSession\" style=\"position: relative; width:100%;height:100%; overflow: auto;display: none;\">  "

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
            rs += "                                                 <input type=\"password\" id=\"session_psw\" class=\"easyui-validatebox\" data-options=\"required:true,missingMessage:'密码不能为空'\" ";
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

    };
    var sessionOk = function (callFun) {

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
                data: { value1: mySysDate(usr), value2: mySysDate(pw), a: mySysDate(a), b: mySysDate(b) },
                error: function (e) { err = e; },
                success: function (data) {
                    r = myAjaxData(data);
                }
            });
            if (err == "") {
                if (r.r == "ture") {
                    $.messager.alert('提示信息', '登陆成功!', 'info', function () {
                        $('#ok').linkbutton('enable');
                        $('#esc').linkbutton('enable');
                        $("#session_psw").val("");
                        $('#div_SysSession').hide(); console.log("aa");
                        if (typeof (callFun) == "function") {
                            callFun();
                        }
                    })
                } else {
                    $.messager.alert('提示信息', '登陆失败,请检查密码是否正确!', 'info', function () {
                        $('#ok').linkbutton('enable');
                        $('#esc').linkbutton('enable');
                        $("#session_psw").focus();
                    });
                }
            } else {
                $.messager.alert('提示信息', '登陆失败,请联系管理员!', 'info', function () {
                    $('#ok').linkbutton('enable');
                    $('#esc').linkbutton('enable');
                    $("#session_psw").focus();
                });

            }
        } else {
            $.messager.alert('提示信息', '密码不能为空!', 'info', function () {
                $("#session_psw").focus();
            });
        }

    };
    var sessionEsc = function () {
        if ($("#session_psw").length > 0) {
            $("#session_psw").val("");
        }
    };

    //session检查
    var checkSession = function () {
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
    };
    //obj jquery对象

    var getJson = function (sobj) {
        var mydata = "";
        $.each($(sobj).children(), function (i, n) {
            if ($(n).attr("id") != null) {
                if ($(n).attr("value") != null) {
                    mydata += $(n).attr("id") + ":'" + $(n).attr("value") + "',";
                }
            }
        });
        if (mydata != "") {
            mydata = "{" + mydata.substring(0, mydata.length - 1) + "}"
            return eval("(" + mydata + ")");
        } else {
            return null;
        }

    };
    var mp = function (v1) {
        var r;
        var h1 = { c: 1, n: 'dd' };
        var h2 = { c: 2, n: 'ee' };
        var rows = [h1, h2];

        var tb = { name: 'tbname', rows: rows };


        v1 = { id: 1, s: '2', t1: tb };

        $.ajax({
            type: 'post',
            url: '../webuser/y.asmx/code',
            async: false,
            dataType: "json",
            data: tb,
            error: function (e) { r = -1; },
            success: function (data) {
                r = myAjaxData(data);
                r.msg = decodeURIComponent(r.msg);
            }
        });
        return r;
    };

});