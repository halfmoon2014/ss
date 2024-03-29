﻿/*
*前台传递到后台,URL的参数 编码格式,与之对应通用反编码
*用来编码URL中的参数
*/
function mySysDate(str) {
    return encodeURIComponent(str);
}

function unMySysDate(str) {
    return decodeURIComponent(str);
}

/*
*url地址
*/
function mySysUrl(str) {
    return encodeURI(str);
}

/*
*打开模态窗口
*/
function openModal(url, argin, options, callback) {
    if (checkSession() == false) {        
        reLoad(function () { openModal(url, argin, options, callback);});
    } else {
        if (arguments.length == 2) {
            if (typeof arguments[1] == "function") {
                callback = arguments[1];
            }
        }
        if (arguments.length == 3) {
            if (typeof arguments[2] == "function") {
                callback = arguments[2];
                options = arguments[1];
            }
        }
        /*如果使用SESSION判断,那么会因为构造HTML标签回调函数的参数会丢失*/
        var width = document.body.clientWidth;
        var height = document.body.clientHeight;
        //支持模态窗口浏览器中,代表传递的参数
        argin = (argin == undefined ? "" : argin)
        //新窗口特性
        options = (options == undefined ? "" : options)
        // //苹果、谷歌内核
        if (browser.versions.webKit) {
            //将回调函数放入当前窗体的属性中,用于子窗口调用
            //缺点是要控制一次只能打开一个子窗口
            if (callback != undefined) {
                window.callback = callback;
            }
            //处理模态窗口特性与open窗口特性不同
            if (options == "") {
                options = "width=" + width + "px,height=" + height + "px";
            } else {
                options = options.replace(/dialogWidth/g, "width").replace(/dialogHeight/g, "height").replace(/;/g, ",");
                for (var i = 0; i < options.split(",").length; i++) {
                    if (options.split(",")[i] != "") {
                        if (options.split(",")[i].split("=")[0] == "width") {
                            width = height = options.split(",")[i].split("=")[1].replace("px","");
                        } else if (options.split(",")[i].split("=")[0] == "height") {
                            height = options.split(",")[i].split("=")[1].replace("px", "");
                        }
                    }
                }
            }
            //居中设置
            //获得窗口的垂直位置
            var iTop = (window.screen.availHeight - 30 - height) / 2;
            //获得窗口的水平位置
            var iLeft = (window.screen.availWidth - 10 - width) / 2;
            
            if (browser.versions.mobile) {
                window.open(url, "", options + ",top=" + iTop + ",left=" + iLeft);
            } else {
                document.getElementById("platIframe").src = url;
                document.getElementById("platIframe").style.width = (Number(width) - 38) + "px";//对话框有补白
                document.getElementById("platIframe").style.height = (Number(height) - 38) + "px";
                if (callback != undefined) {
                    window.platDialogCallback = callback;
                }
                document.getElementById("platDialog").showModal();                
            }
        } else {
            if (options == "") {
                options = "dialogWidth=" + width + "px;dialogHeight=" + height + "px";
            }
            var r = window.showModalDialog(url, argin, options);            
            if ("function"==typeof(callback)) {
                callback(r);
            } else {//如果没传入回调函数,IE状态下返回结果
                return r;
            }
        }
    }

}

/*
*用于使用平台脚本打开窗口的情况
*兼容chrome 因为没有模态窗口,chrome 使用open 打开新窗口,所以在关闭的时候注意执行父窗口的函数
*子窗口在调用
*/
function closeWindow(returnvalue) {
    if (browser.versions.webKit) {
        //用来关闭chrome窗口时标识关闭的动作是否使用浏览器自带的关闭按钮
        //任何关闭的动作都会响应onunload事件
        if (browser.versions.mobile) {
            window.onunloadtag = true;
            (window.opener && window.opener.callback != undefined) ? window.opener.callback(returnvalue) : "";
            window.close();            
        } else {
            parent.document.getElementById("platDialog").close();
            (parent.window.platDialogCallback != undefined) ? parent.window.platDialogCallback(returnvalue) : "";
            parent.document.getElementById("platIframe").src = "about:blank";
        }
    } else {
        window.returnValue = returnvalue;
        window.close();
    }
}

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
}

function openWin(url, obj) {
    window.open(url);
}

/*
*
*/
function sqlFormat(str) {
    return $.trim(str).replace(/'/g, "''");
}

/*
*返回是一个JSON r.r,r.msg
*同步
*xact_abort事务回滚,取值范围[on,off,空],空的时候不加事务回滚
*/
function myAjax(sqlCommand, xact_abort) {
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

    $.ajax({ type: 'post',
        url: '../webuser/Ws.asmx/ExecSqlCommand',
        async: false,
        data: { sqlCommand: sqlCommand, xact_abort: xact_abort, wid: wid },
        error: function (e) { },
        success: function (data) {
            r = myAjaxData(data);
            r.msg = decodeURIComponent(r.msg);
        }
    });

    return r;
}

function getAjax(par) {    
    $.ajax({
        type: 'post',
        url: '../webuser/Ws.asmx/ExecSqlCommand',
        async: false,
        data: { par: par },
        error: function (e) { },
        success: function (data) {
            r = myAjaxData(data);
            r.msg = decodeURIComponent(r.msg);
        }
    });

    return r;
}

//obj jquery对象
function getJson(sobj) {
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

}

function mp(v1) {
    var r;
    var h1 = { c: 1, n: 'dd' };
    var h2 = { c: 2, n: 'ee' };
    var rows = [h1, h2];

    var tb = { name: 'tbname', rows: rows };


    v1 = { id: 1, s: '2', t1: tb };

    $.ajax({ type: 'post',
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
}
