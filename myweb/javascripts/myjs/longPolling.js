$(function () {
    longPolling(0, showMsg, randomKey());
});
var showMsg = function (msg) {
    $.messager.show({
        title: "info",
        msg: msg,
        timeout: 0,
        showType: 'fade'
    });
}
function longPolling(timeout,callFuc,g) {
    $.ajax({
        type: 'post',
        url: 'longPollingData.aspx?n=' + $("#username").attr("b") + "&t=" + timeout+"&g="+g,
        timeout: timeout,
        data: { "timed": new Date().getTime() },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (textStatus == "timeout") { // 请求超时
                //console.log("timeout")
                longPolling(timeout,callFuc,g); // 递归调用
                // 其他错误，如网络错误等                
            } else {
                //console.log("error")
                longPolling(timeout,callFuc,g);
            }
        },
        success: function (data) {     
            callFuc(data);
            longPolling(timeout,callFuc,g);
        }
    });
    //    $.ajax({
    //        url: "${pageContext.request.contextPath}/communication/user/ajax.mvc",
    //        data: { "timed": new Date().getTime() },
    //        dataType: "text",
    //        timeout: 5000,
    //        error: function (XMLHttpRequest, textStatus, errorThrown) {
    //            $("#state").append("[state: " + textStatus + ", error: " + errorThrown + " ]<br/>");
    //            if (textStatus == "timeout") { // 请求超时
    //                longPolling(); // 递归调用

    //                // 其他错误，如网络错误等
    //            } else {
    //                longPolling();
    //            }
    //        },
    //        success: function (data, textStatus) {
    //            $("#state").append("[state: " + textStatus + ", data: { " + data + "} ]<br/>");

    //            if (textStatus == "success") { // 请求成功
    //                longPolling();
    //            }
    //        }
    //    });
}