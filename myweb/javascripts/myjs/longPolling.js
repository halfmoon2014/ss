function longPolling(callFuc) {

    $.ajax({ type: 'post',
        url: 'longPollingData.aspx',
        timeout: 5000,
        data: { "timed": new Date().getTime() },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            if (textStatus == "timeout") { // 请求超时
                console.log("timeout")
                longPolling(callFuc); // 递归调用
                // 其他错误，如网络错误等                
            } else {
                console.log("error")
                longPolling(callFuc);
            }
        },
        success: function (data) {
            console.log("success")
            r = myAjaxData(data);
            r.msg = decodeURIComponent(r.msg);
            if (r.r == "true") {
                callFuc(r);
            }
            longPolling(callFuc);
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