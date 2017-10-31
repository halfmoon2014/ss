<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2_html5.aspx.cs" Inherits="Default2_html5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta name="viewport" charset="utf-8" content="width=device-width, initial-scale=1">
    <title></title>
    <style type="text/css">
    .top
    {
        background:red;
        height:40px;
        margin:0px;
    }
     .bottom
    {
        background:red;margin:0px;
    }
    .test
    {
        height:500px;
        overflow:auto;margin:0px;
    }
    .content
    {
        margin:0px;
    }
    </style>
    <script language="javascript" type="text/javascript" >
        var touchesclientY = 0;
        window.onload = function () {
            document.getElementById("content").addEventListener('touchstart', touch, false);
            document.getElementById("content").addEventListener('touchmove', touch, false);
            document.getElementById("content").addEventListener('touchend', touch, false);
        }
        function touch(event) {
            var event = event || window.event;

            var oInp = document.getElementById("top");
            var istop; //向上划动

            switch (event.type) {
                case "touchstart":
                    touchesclientY = event.touches[0].clientY;
                    oInp.innerHTML = "Touch started (" + event.touches[0].clientX + "," + event.touches[0].clientY + ")";
                    break;
                case "touchend":
                    oInp.innerHTML = "<br>Touch end (" + event.changedTouches[0].clientX + "," + event.changedTouches[0].clientY + ")";
                    break;
                case "touchmove":
                    if (event.touches[0].clientY > touchesclientY) {
                        istop = true;
                    } else {
                        istop = false;
                    }
                    if (!istop) {//向下划动
                        if (document.getElementById("test").scrollTop + document.getElementById("test").clientHeight >= document.getElementById("content").clientHeight) {
                            event.preventDefault();
                        }
                    }
                    oInp.innerHTML = "<br>Touch moved (" + event.touches[0].clientX + "," + event.touches[0].clientY + ")";
                    break;
            }
        }
    </script>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    
    <div class="top" id="top">top</div>
    <div class="test"  id="test" >
    <div class="content" id="content" >
    <video id="myVideo" style=" width:100%"  src="http://www.157.hk:89/samplevideo_hq.mp4" preload  controls="controls"  >         
    </video>
		a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />
        a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />a<br />b<br />

    </div>
    </div>
    <div class="bottom" id="bottom">bottom</div>
    </form>
</body>
</html>
