define(['jquery'], function ($) {

    // 取小数位
    var forDight = function (Dight, How) {
        Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
        return Dight;
    };

    // 获 取 浏 览器 版 本
    //var getBrowserVer = function () {
    //    var br = navigator.userAgent.toLowerCase();
    //    var Ver = (br.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [0, '0'])[1];
    //    return Ver;
    //};

    // 获取浏览器类型。
    //var getUserBrowser = function () {        
    //    var browserName = navigator.userAgent.toLowerCase();
    //    if (/msie/i.test(browserName) && !/opera/.test(browserName)) {

    //        return "IE";
    //    } else if (/firefox/i.test(browserName)) {

    //        return "Firefox";
    //    } else if (/chrome/i.test(browserName) && /webkit/i.test(browserName) && /mozilla/i.test(browserName)) {

    //        return "Chrome";
    //    } else if (/opera/i.test(browserName)) {

    //        return "Opera";
    //    } else if (/webkit/i.test(browserName) && !(/chrome/i.test(browserName) && /webkit/i.test(browserName) && /mozilla/i.test(browserName))) {

    //        return "Safari";
    //    } else {
    //        return "unKnow";
    //    }
    //};
    var browser = {
        versions: function () {
            var u = window.navigator.userAgent;
            return {
                trident: u.indexOf('Trident') > -1, //IE内核
                presto: u.indexOf('Presto') > -1, //opera内核
                webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
                gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
                mobile: !!u.match(/AppleWebKit.*Mobile.*/) || !!u.match(/AppleWebKit/), //是否为移动终端
                ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器
                iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者安卓QQ浏览器
                iPad: u.indexOf('iPad') > -1, //是否为iPad
                webApp: u.indexOf('Safari') == -1,//是否为web应用程序，没有头部与底部
                weixin: u.indexOf('MicroMessenger') == -1 //是否为微信浏览器
            };
        }()
    };

    //Javascript 操作select控件大全（新增、修改、删除、选中、清空、判断存在等） 
    //Posted on 2007-08-08 14:56 礼拜一 阅读(82638) 评论(40) 编辑 收藏  
    //1判断select选项中 是否存在Value="paraValue"的Item 
    //2向select选项中 加入一个Item 
    //3从select选项中 删除一个Item 
    //4删除select中选中的项 
    //5修改select选项中 value="paraValue"的text为"paraText" 
    //6设置select中text="paraText"的第一个Item为选中 
    //7设置select中value="paraValue"的Item为选中 
    //8得到select的当前选中项的value 
    //9得到select的当前选中项的text 
    //10得到select的当前选中项的Index 
    //11清空select的项 
    //js 代码
    // 1.判断select选项中 是否存在Value="paraValue"的Item        
    var jsSelectIsExitItem = function (objSelect, objItemValue) {
        var isExit = false;
        for (var i = 0; i < objSelect.options.length; i++) {
            if (objSelect.options[i].value == objItemValue) {
                isExit = true;
                break;
            }
        }
        return isExit;
    };

    // 2.向select选项中 加入一个Item        
    var jsAddItemToSelect = function (objSelect, objItemText, objItemValue) {
        //判断是否存在        
        if (jsSelectIsExitItem(objSelect, objItemValue)) {
            return -1;
            //alert("该Item的Value值已经存在");        
        } else {
            var varItem = new Option(objItemText, objItemValue);
            objSelect.options.add(varItem);
            return 1;
            //alert("成功加入");     
        }
    };

    // 3.从select选项中 删除一个Item        
    var jsRemoveItemFromSelect = function (objSelect, objItemValue) {
        //判断是否存在        
        if (jsSelectIsExitItem(objSelect, objItemValue)) {
            for (var i = 0; i < objSelect.options.length; i++) {
                if (objSelect.options[i].value == objItemValue) {
                    objSelect.options.remove(i);
                    break;
                }
            }
        }
    };

    // 4.删除select中选中的项    
    var jsRemoveSelectedItemFromSelect = function (objSelect) {
        var length = objSelect.options.length - 1;
        for (var i = length; i >= 0; i--) {
            if (objSelect[i].selected == true) {
                objSelect.options[i] = null;
            }
        }
    };

    // 5.修改select选项中 value="paraValue"的text为"paraText"        
    var jsUpdateItemToSelect = function (objSelect, objItemText, objItemValue) {
        //判断是否存在        
        if (jsSelectIsExitItem(objSelect, objItemValue)) {
            for (var i = 0; i < objSelect.options.length; i++) {
                if (objSelect.options[i].value == objItemValue) {
                    objSelect.options[i].text = objItemText;
                    break;
                }
            }
            return true;
        } else {
            return false;
        }
    };

    // 6.设置select中text="paraText"的第一个Item为选中        
    var jsSelectItemByValue = function (objSelect, objItemText) {
        //判断是否存在        
        var isExit = false;
        for (var i = 0; i < objSelect.options.length; i++) {
            if (objSelect.options[i].text == objItemText) {
                objSelect.options[i].selected = true;
                isExit = true;
                break;
            }
        }
        //Show出结果        
        if (isExit) {
            return true;
        } else {
            return false;
        }
    };


    // 7.设置select中value="paraValue"的Item为选中    
    //document.all.objSelect.value = objItemValue;    

    // 8.得到select的当前选中项的value    
    //var currSelectValue = document.all.objSelect.value;    

    // 9.得到select的当前选中项的text    
    //var currSelectText = document.all.objSelect.options[document.all.objSelect.selectedIndex].text;    

    // 10.得到select的当前选中项的Index    
    //var currSelectIndex = document.all.objSelect.selectedIndex;    

    // 11.清空select的项    
    //document.all.objSelect.options.length = 0;   

    var replacetsf = function (str) {
        str = str.replace(/</g, "<");
        str = str.replace(/>/g, ">");
        str = str.replace(/"/g, "\"");
        str = str.replace(/&/g, "&");
        str = str.replace(/ /g, " ");
        return str;
    };

    //判断是否为数字    
    var isNum = function (s) {
        if (s != null && s != "") {
            return !isNaN(s);
        }
        return false;
    };

    //cookie设置
    var setCookie = function (name, value)//两个参数，一个是cookie的名子，一个是值
    {
        var Days = 30; //此 cookie 将被保存 30 天
        var exp = new Date();    //new Date("December 31, 9998");
        exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
        document.cookie = name + "=" + value + ";expires=" + exp.toGMTString();
    };

    //获取cookies    
    var getCookie = function (name)//取cookies函数        
    {
        var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
        if (arr != null) return arr[2]; return null;

    };

    //删除cookie
    var delCookie = function (name) {
        var exp = new Date();
        exp.setTime(exp.getTime() - 1);
        var cval = getCookie(name);
        if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    };

    //取URL参数中的信息
    var request = function (paras) {
        var url = location.href;
        var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
        var paraObj = {}
        for (i = 0; j = paraString[i]; i++) {
            paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
        }
        var returnValue = paraObj[paras.toLowerCase()];
        if (typeof (returnValue) == "undefined") {
            return null;
        } else {
            return returnValue;
        }
    };

    //处理ajax返回值
    var myAjaxData = function (data) {
        var mydata;
        if (window.DOMParser) {
            mydata = data.documentElement.textContent;
        } else {
            mydata = data.text;
        }
        /*20140316 如果返回的是\那么js的eval会去掉*/
        mydata = mydata.replace(/\\/g, "/");
        return eval("(" + mydata + ")");
    };

    //Post方式提交表单
    var postNewWin = function (url, jsonObj) {
        var postUrl = url;
        var iframe = document.getElementById("postDataIframe");
        if (!iframe) {
            iframe = document.createElement("iframe");
            iframe.id = "postDataIframe";
            iframe.scr = "about:blank";
            iframe.frameborder = "0";
            iframe.style.width = "0px";
            iframe.style.height = "0px";

            var form = document.createElement("form");
            form.id = "postDataForm";
            form.method = "post";
            form.target = "_blank";
            document.body.appendChild(iframe);

            iframe.contentWindow.document.write("<body>" + form.outerHTML + "</body>");

        }
        var postStr = "";
        $.each(jsonObj, function (myName, myValue) {
            postStr += "  <input type=\"hidden\" name='" + myName + "' id=\"" + myName + "\"  value=\"" + myValue + "\" />";
        });
        iframe.contentWindow.document.getElementById("postDataForm").innerHTML = postStr;
        iframe.contentWindow.document.getElementById("postDataForm").action = postUrl;
        iframe.contentWindow.document.getElementById("postDataForm").submit();
    };

    //Post方式提交表单
    var postNewWin2 = function (action, data) {
        var form;
        if (document.getElementById("postDataForm") == null) {
            var form = $("<form/>").attr('action', action).attr('method', 'post').attr("id", "postDataForm");
            form.attr('target', '_blank');
        } else {
            form = $(document.getElementById("postDataForm"));
            form.attr('action', action);
        }
        var input = '';
        $.each(data, function (i, n) {
            input += '<input type="hidden" name="' + i + '" value="' + n + '" />';
        });
        form.html(input).appendTo("body").css('display', 'none').submit();
    };

    //随机数
    var randomKey = function () {
        var hex = new Array('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f');
        var t = '';
        for (var i = 0; i < 32; i++) {
            t += hex[Math.floor(Math.random() * 16)];
        }
        return t.toUpperCase();
    };

    var autoTextarea = function (elem, extra, maxHeight) {
        extra = extra || 0;
        var isFirefox = !!document.getBoxObjectFor || 'mozInnerScreenX' in window,
        isOpera = !!window.opera && !!window.opera.toString().indexOf('Opera'),
                addEvent = function (type, callback) {
                    elem.addEventListener ?
                            elem.addEventListener(type, callback, false) :
                            elem.attachEvent('on' + type, callback);
                },
                getStyle = elem.currentStyle ? function (name) {
                    var val = elem.currentStyle[name];

                    if (name === 'height' && val.search(/px/i) !== 1) {
                        var rect = elem.getBoundingClientRect();
                        return rect.bottom - rect.top -
                                parseFloat(getStyle('paddingTop')) -
                                parseFloat(getStyle('paddingBottom')) + 'px';
                    };

                    return val;
                } : function (name) {
                    return getComputedStyle(elem, null)[name];
                },
                minHeight = parseFloat(getStyle('height'));

        elem.style.resize = 'none';

        var change = function () {
            var scrollTop, height,
                    padding = 0,
                    style = elem.style;

            if (elem._length === elem.value.length) return;
            elem._length = elem.value.length;

            if (!isFirefox && !isOpera) {
                padding = parseInt(getStyle('paddingTop')) + parseInt(getStyle('paddingBottom'));
            };
            scrollTop = document.body.scrollTop || document.documentElement.scrollTop;

            elem.style.height = minHeight + 'px';
            if (elem.scrollHeight > minHeight) {
                if (maxHeight && elem.scrollHeight > maxHeight) {
                    height = maxHeight - padding;
                    style.overflowY = 'auto';
                } else {
                    height = elem.scrollHeight - padding;
                    style.overflowY = 'hidden';
                };
                style.height = height + extra + 'px';
                scrollTop += parseInt(style.height) - elem.currHeight;
                document.body.scrollTop = scrollTop;
                document.documentElement.scrollTop = scrollTop;
                elem.currHeight = parseInt(style.height);
            };
        };

        addEvent('propertychange', change);
        addEvent('input', change);
        addEvent('focus', change);
        change();
    };


    //sweetalert封装

    var sConfirmTitleAndCallBack = function (title, callBack) {
        sConfirmAll(title, callBack, "确 定", "取 消", "warning")
    }

    var sConfirmOp = function (op) {
        var settings = $.extend({
            title: "",
            callBack: undefined,
            confirmButtonText: "确 定",
            cancelButtonText: '取 消',
            icon: 'warning'
        }, op);

        sConfirmAll(settings.title, settings.callBack, settings.confirmButtonText, settings.cancelButtonText, settings.icon)
    }

    var sConfirmAll = function (title, callBack, confirmButtonText, cancelButtonText, icon) {      
        swal({
            title: title,
            text: "",
            icon: icon,
            buttons: {
                myCancel: {
                    text: cancelButtonText,
                    value: false
                },
                confirm: confirmButtonText
            },
            dangerMode: true,
            closeOnClickOutside: false,
            closeOnEsc: false,
        })
        .then(function (willDelete) {
            if (undefined != callBack) {
                callBack(willDelete);
            }
        });
    }

    var sConfirm = function () {
        var a = arguments;
        if (a.length == 1) {//如果只有一个参数         
            sConfirmOp(a[0]);
        } else if (a.length == 2) {
            sConfirmTitleAndCallBack(a[0], a[1]);
        }
    }

    var sAlertTitle = function (title) {
        sAlertTitleAndCallBack(title, undefined);
    }

    var sAlertTitleAndCallBack = function (title, callBack) {
        sAlertAll(title, callBack, "warning", "确 定");
    }

    var sAlertTitleAndCallBackAndIcon = function (title, callBack, icon) {
        sAlertAll(title, callBack, icon, "确 定");
    }

    var sAlertOp = function (op) {
        var settings = $.extend({
            title: "",
            callBack: undefined,
            confirm: "确 定",
            icon: 'warning'
        }, op);

        sAlertAll(settings.title, settings.callBack, settings.icon, settings.confirm);
    }

    var sAlertAll = function (title, callBack, icon, confirm) {
        //warning error success info    
        swal({
            title: title,
            text: "",
            icon: icon,
            buttons: {
                confirm: confirm
            },
            closeOnClickOutside: false,
            closeOnEsc: false,
        }).then(function (willDelete) {
            if (undefined != callBack) {
                callBack();
            }
        });       
    }

    var sAlert = function () {
        var a = arguments;
        if (a.length == 1) {//如果只有一个参数和,那么有可能是 sAlertTitle or sAlertOp
            if (typeof (a[0]) == "string") {
                sAlertTitle(a[0]);
            } else if (typeof (a[0]) == "object") {
                sAlertOp(a[0]);
            }
        } else if (a.length == 2) {
            sAlertTitleAndCallBack(a[0], a[1]);
        } else if (a.length == 3) {
            sAlertTitleAndCallBackAndIcon(a[0], a[2], a[1]);
        }
    }

    //显示遮罩
    var showLoading = function () {
        var spinner = document.createElement("div"); //首先创建一个div
        spinner.className = "sk-cube-grid";
        var rect1 = document.createElement("div"); //首先创建一个div    
        rect1.className = "sk-cube sk-cube1";
        var rect2 = document.createElement("div"); //首先创建一个div        
        rect2.className = "sk-cube sk-cube2";
        var rect3 = document.createElement("div"); //首先创建一个div
        rect3.className = "sk-cube sk-cube3";
        var rect4 = document.createElement("div"); //首先创建一个div
        rect4.className = "sk-cube sk-cube4";
        var rect5 = document.createElement("div"); //首先创建一个div
        rect5.className = "sk-cube sk-cube5";
        var rect6 = document.createElement("div"); //首先创建一个div
        rect6.className = "sk-cube sk-cube6";
        var rect7 = document.createElement("div"); //首先创建一个div
        rect7.className = "sk-cube sk-cube7";
        var rect8 = document.createElement("div"); //首先创建一个div
        rect8.className = "sk-cube sk-cube8";
        var rect9 = document.createElement("div"); //首先创建一个div
        rect9.className = "sk-cube sk-cube9";
        spinner.appendChild(rect1)
        spinner.appendChild(rect2)
        spinner.appendChild(rect3)
        spinner.appendChild(rect4)
        spinner.appendChild(rect5)
        spinner.appendChild(rect6)
        spinner.appendChild(rect7)
        spinner.appendChild(rect8)
        spinner.appendChild(rect9)
        var overlayHTML = document.createElement("div"); //首先创建一个div        
        overlayHTML.setAttribute("id", "overlay"); //定义该div的id
        overlayHTML.style.background = "#dedede";
        overlayHTML.style.width = "100%";
        overlayHTML.style.height = "100%";
        overlayHTML.style.position = "fixed";
        overlayHTML.style.top = "0";
        overlayHTML.style.left = "0";
        overlayHTML.style.zIndex = "99999";
        overlayHTML.style.opacity = "1";
        overlayHTML.style.filter = "Alpha(opacity=70)";
        overlayHTML.appendChild(spinner);
        document.body.appendChild(overlayHTML);
    }
    //取消遮罩
    var hideLoading = function () {
        var body = document.getElementsByTagName("body");
        var overlay = document.getElementById("overlay");
        $(overlay).fadeOut(2000, function () {
            body[0].removeChild(overlay);
        });
    }

    return {
        forDight: forDight,
        browser: browser,
        jsSelectIsExitItem: jsSelectIsExitItem,
        jsAddItemToSelect: jsAddItemToSelect,
        jsRemoveItemFromSelect: jsRemoveItemFromSelect,
        jsRemoveSelectedItemFromSelect: jsRemoveSelectedItemFromSelect,
        jsUpdateItemToSelect: jsUpdateItemToSelect,
        jsSelectItemByValue: jsSelectItemByValue,
        replacetsf: replacetsf,
        isNum: isNum,
        setCookie: setCookie,
        getCookie: getCookie,
        delCookie: delCookie,
        request: request,
        myAjaxData: myAjaxData,
        postNewWin: postNewWin,
        postNewWin2: postNewWin2,
        randomKey: randomKey,
        autoTextarea: autoTextarea,
        sConfirm: sConfirm,
        sAlert: sAlert,
        showLoading: showLoading,
        hideLoading: hideLoading
    };
});