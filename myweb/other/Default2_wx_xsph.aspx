<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2_wx_xsph.aspx.cs" Inherits="Default2_wx_xsph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="height=device-height,width=device-width,initial-scale=1.0,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="format-detection" content="telephone=yes" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="../css/impromptu/jquery-impromptu.css" rel="stylesheet" type="text/css" />
    <script src="../javascripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../javascripts/weixin/jweixin-1.0.0.js" type="text/javascript"></script>
    <script src="../javascripts/impromptu/jquery-impromptu.js" type="text/javascript"></script>
    <script type="text/javascript">
        var appIdVal, timestampVal, nonceStrVal, signatureVal;
        window.onload = function () {
            //WeiXin JSSDK
            appIdVal = document.getElementById("appIdVal").value;
            timestampVal = document.getElementById("timestampVal").value;
            nonceStrVal = document.getElementById("nonceStrVal").value;
            signatureVal = document.getElementById("signatureVal").value;
            jsConfig();
        }
        /********************签名**********************/
        function jsConfig() {
            wx.config({
                debug: false,
                appId: appIdVal, // 必填，公众号的唯一标识
                timestamp: timestampVal, // 必填，生成签名的时间戳
                nonceStr: nonceStrVal, // 必填，生成签名的随机串
                signature: signatureVal, // 必填，签名，见附录1
                jsApiList: ['scanQRCode'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
            });
            wx.ready(function () {
                //alert("ready");
                scan();
            });
            wx.error(function (res) {
                alert(allPrpos(res));
                alert("JS注入失败！");
            });
        }

        /*
        * 用来遍历指定对象所有的属性名称和值
        * obj 需要遍历的对象
        * author: Jet Mah
        * website: http://www.javatang.com/archives/2006/09/13/442864.html 
        */
        function allPrpos(obj) {
            // 用来保存所有的属性名称和值
            var props = "";
            // 开始遍历
            for (var p in obj) {
                // 方法
                if (typeof (obj[p]) == "function") {
                    obj[p]();
                } else {
                    // p 为属性名称，obj[p]为对应属性的值
                    props += p + "=" + obj[p] + "\t";
                }
            }
            // 最后显示所有的属性
            return props;


        }

        function scan() {
            wx.scanQRCode({
                desc: 'scanQRCode desc',
                needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
                success: function (res) {
                    var result = res.resultStr; // 当needResult 为 1 时，扫码返回的结果 
                    var mes = 1;
                    var checkInfo = getInfo(result);                    
                    if (checkInfo.result == "Successed") {
                        $.prompt("<div style='font-size:15px;'>abc:" + checkInfo.abc1 + "</br>abc1:" + checkInfo.abc2 + "</div>",
                        {
                            title: "提示",
                            buttons: {'ok': 'iscancel' },
                            submit: function (e, v, m, f) {                                                      
                            },
                            close: function (event, value, message, formVals) {
                                //关闭的时候就会调用这个函数,
                                scan();
                            }
                        });
                    } else if (checkInfo.result == "Error") {
                        alert("two信息查询错误");
                        scan();
                    } else if (checkInfo.result == "netError") {
                        alert("网络错误");
                        scan();
                    }

                }
            });
        };
     

        //获取2维码对应信息
        function getInfo(result) {
            var obj = null;
            $.ajax({
                type: "POST",
                timeout: 1000,
                async: false,
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                url: "Default2_wx_xsph.ashx",
                data: { ctrl: "getInfo", info: result },
                success: function (msg) {
                    obj = eval("(" + msg + ")");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    obj = { result: 'netError' };
                }
            });
            return obj;
        }  

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <input type="hidden" runat="server" id="appIdVal" />
    <input type="hidden" runat="server" id="timestampVal" />
    <input type="hidden" runat="server" id="nonceStrVal" />
    <input type="hidden" runat="server" id="signatureVal" />
    
    </form>
</body>
</html>
