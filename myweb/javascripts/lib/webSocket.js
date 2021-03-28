var webSocketLib = (function () {

    function init(key,call) {
        if ("WebSocket" in window) {
            console.log("您的浏览器支持 WebSocket!");
            var msg = {}; msg.errcode = 0; msg.data = "您的浏览器支持 WebSocket!";
            call("init", msg)
            // 打开一个 web socket
            ws = new WebSocket("ws://127.0.0.1:9999");

            ws.onopen = function () {
                // Web Socket 已连接上，使用 send() 方法发送数据
                console.log(ws.readyState);
                ws.send(key);
                console.log("数据发送中...");
                var msg = {}; msg.errcode = 0; msg.data = "数据发送中...";
                call("open",msg)
            };

            ws.onmessage = function (evt) {
                var received_msg = evt.data;
                //document.getElementById("txt").value = document.getElementById("txt").value + "\r\n" + received_msg;
                //console.log(received_msg)
                console.log("数据已接收...");
                var msg = {}; msg.errcode = 0; msg.data = received_msg;
                call("onmessage", msg)
            };

            ws.onclose = function () {
                // 关闭 websocket
                console.log("连接已关闭...");
                var msg = {}; msg.errcode = 0; msg.data = "连接已关闭...";
                call("onclose", msg);
            };
        }
        else {
            // 浏览器不支持 WebSocket
            console.log("您的浏览器不支持 WebSocket!");
            var msg = {}; msg.errcode = 100; msg.errmsg = "您的浏览器不支持 WebSocket!";
            call("init", msg)         
        }
    }


    return {
        init: init
    };
})();
