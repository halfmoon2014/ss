$(function () {
    var longpollingurl = $("#username").attr("longpollingurl");
    var usr = $("#username").attr("usr");
    var b = $("#username").attr("b");
    var title = encodeURI(document.title);
    if (longpollingurl.length > 0) {
        longPolling(longpollingurl, title, b, usr, 0, showMsg, randomKey());
    }
});
var showMsg = function (msg, action) {
    $.messager.show({
        title: "info",
        msg: msg,
        timeout: 0,
        showType: 'fade'
    });
    if (action == "Query") {
        setTimeout(function () { location.reload() }, 10000)
    }
}
function longPolling(longpollingurl, title, b, usr, timeout, callFuc, g) {

    $.ajax({
        type: 'post',
        url: longpollingurl + '/longPollingData.aspx?title=' + title + '&b=' + b + '&n=' + usr + "&t=" + timeout + "&g=" + g,
        timeout: timeout,
        data: { "timed": new Date().getTime() },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (textStatus == "timeout") { // 请求超时
                longPolling(longpollingurl, title, b, usr, timeout, callFuc, g); // 递归调用
                // 其他错误，如网络错误等                
            } else {
                longPolling(longpollingurl, title, b, usr, timeout, callFuc, g);
            }
        },
        success: function (data) {
            var r = JSON.parse(data);
            if (r.Errcode == 0) {
                callFuc(r.Data);
                longPolling(longpollingurl, title, b, usr, timeout, callFuc, g);
            } else if (r.Errcode == -1) {
                eval(r.Data);
                longPolling(longpollingurl, title, b, usr, timeout, callFuc, g);
            } else {
                alert(r.Errmsg);
                longPolling(longpollingurl, title, b, usr, timeout, callFuc, g);
            }
        }
    });    
}