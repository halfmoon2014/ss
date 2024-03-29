﻿var longPollingStart = function () {
    var longpollingurl = $("#username").attr("longpollingurl");
    var usr = $("#username").attr("usr");
    var b = $("#username").attr("b");
    var title = encodeURI(document.title);
    if (longpollingurl.length > 0) {
        longPolling(longpollingurl, title, b, usr, 0, showMsg, randomKey());
    }
}

var showMsg = function (msg) {
    $.messager.show({
        title: "info",
        msg: msg,
        timeout: 0,
        showType: 'fade'
    });
    //if (action == "Query") {
    //    setTimeout(function () { location.reload() }, 10000)
    //}
}
var longPollingReload = function () {
    location.reload();
}

function longPolling(longpollingurl, title, b, usr, timeout, callFuc, g,timSpan) {

    $.ajax({
        type: 'post',
        url: longpollingurl + '/longPollingData.aspx?title=' + title + '&b=' + b + '&n=' + usr + "&t=" + timeout + "&g=" + g,
        timeout: timeout,
        data: { "timed": new Date().getTime() },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (textStatus == "timeout") { // 请求超时
                //longPolling(longpollingurl, title, b, usr, timeout, callFuc, g); // 递归调用
                // 其他错误，如网络错误等                
            } else {
                //longPolling(longpollingurl, title, b, usr, timeout, callFuc, g);
            }
            if (!timSpan) timSpan = 1000;
            else timSpan += 1000;
            setTimeout(function () { longPolling(longpollingurl, title, b, usr, timeout, callFuc, g, timSpan) }, timSpan)
            
        },
        success: function (data) {            
            try {
                var r = JSON.parse(data);
                if (r.Errcode == 0) {
                    callFuc(r.Data);
                } else if (r.Errcode == -1) {
                    eval(r.Data);
                } else {
                    alert(r.Errmsg);
                }
            } catch (err) {                
            }
            longPolling(longpollingurl, title, b, usr, timeout, callFuc, g)
        }
    });    
}