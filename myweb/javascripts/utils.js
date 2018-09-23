/*
*取小数位
*/
function ForDight(Dight, How) {
    Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
    return Dight;
}
/*
*获取浏览器版本号
*/
//function getBrowserVer() {
//    var br = navigator.userAgent.toLowerCase();
//    var Ver = (br.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [0, '0'])[1];
//    return Ver;
//}
/*
* 获取浏览器类型。
*/
//function getUserBrowser() {
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
//}
var browser = {
    versions: function () {
        var u = window.navigator.userAgent;
        return {
            trident: u.indexOf('Trident') > -1, //IE内核
            presto: u.indexOf('Presto') > -1, //opera内核
            webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
            mobilebak: !!u.match(/AppleWebKit.*Mobile.*/) || !!u.match(/AppleWebKit/), //是否为移动终端
            mobile:/Android|webOS|iPhone|iPod|BlackBerry/i.test(u),
            ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器
            iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者安卓QQ浏览器
            iPad: u.indexOf('iPad') > -1, //是否为iPad
            webApp: u.indexOf('Safari') == -1,//是否为web应用程序，没有头部与底部
            weixin: u.indexOf('MicroMessenger') == -1, //是否为微信浏览器
            ver: (navigator.userAgent.toLowerCase().match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [0, '0'])[1]
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
function jsSelectIsExitItem(objSelect, objItemValue) {
    var isExit = false;
    for (var i = 0; i < objSelect.options.length; i++) {
        if (objSelect.options[i].value == objItemValue) {
            isExit = true;
            break;
        }
    }
    return isExit;
}

// 2.向select选项中 加入一个Item        
function jsAddItemToSelect(objSelect, objItemText, objItemValue) {
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
}

// 3.从select选项中 删除一个Item        
function jsRemoveItemFromSelect(objSelect, objItemValue) {
    //判断是否存在        
    if (jsSelectIsExitItem(objSelect, objItemValue)) {
        for (var i = 0; i < objSelect.options.length; i++) {
            if (objSelect.options[i].value == objItemValue) {
                objSelect.options.remove(i);
                break;
            }
        }
    } 
}


// 4.删除select中选中的项    
function jsRemoveSelectedItemFromSelect(objSelect) {
    var length = objSelect.options.length - 1;
    for (var i = length; i >= 0; i--) {
        if (objSelect[i].selected == true) {
            objSelect.options[i] = null;
        }
    }
}

// 5.修改select选项中 value="paraValue"的text为"paraText"        
function jsUpdateItemToSelect(objSelect, objItemText, objItemValue) {
    //判断是否存在        
    if (jsSelectIsExitItem(objSelect, objItemValue)) {
        for (var i = 0; i < objSelect.options.length; i++) {
            if (objSelect.options[i].value == objItemValue) {
                objSelect.options[i].text = objItemText;
                break;
            }
        }
    } 
}

// 6.设置select中text="paraText"的第一个Item为选中        
function jsSelectItemByValue(objSelect, objItemText) {
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
}

/*   
// 7.设置select中value="paraValue"的Item为选中    
document.all.objSelect.value = objItemValue;    
       
// 8.得到select的当前选中项的value    
var currSelectValue = document.all.objSelect.value;    
       
// 9.得到select的当前选中项的text    
var currSelectText = document.all.objSelect.options[document.all.objSelect.selectedIndex].text;    
       
// 10.得到select的当前选中项的Index    
var currSelectIndex = document.all.objSelect.selectedIndex;    
       
// 11.清空select的项    
document.all.objSelect.options.length = 0;   
*/

function replacetsf(str) {
    str = str.replace(/</g, "<");
    str = str.replace(/>/g, ">");
    str = str.replace(/"/g, "\"");
    str = str.replace(/&/g, "&");
    str = str.replace(/ /g, " ");
    return str;
}

/*
*判断是否为数字
*/
function IsNum(s) {
    if (s != null && s != "") {
        return !isNaN(s);
    }
    return false;
}

/*
*cookie设置
*/
function setCookie(name, value)//两个参数，一个是cookie的名子，一个是值
{
    var Days = 30; //此 cookie 将被保存 30 天
    var exp = new Date();    //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + value + ";expires=" + exp.toGMTString();
}
/*
*获取cookies
*/
function getCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return arr[2]; return null;

}
/*
*删除cookie
*/
function delCookie(name)
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}

/*
*取URL参数中的信息
*/
function request(paras) {
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
}
/*
*处理ajax返回值
*/
function myAjaxData(data) {
    var mydata;
    if (window.DOMParser) {
        //非IE
        if (data.documentElement) {
            //如果返回的信息中没有这个元素
            mydata = data.documentElement.textContent;
        } else {
            mydata = "";
            try {
                console.log(data)
            } catch(e){ }
        }
    } else {
        if (data.text) {
            mydata = data.text;
        } else {
            mydata = "";
            try {
                console.log(data)
            } catch(e){ }
        }
    }
    var obj = {};
    if (mydata.length > 0) {
        /*20140316 如果返回的是\那么js的eval会去掉*/
        //mydata = mydata.replace(/\\/g, "/");        
        //return eval("(" + mydata + ")");        
        try{
            obj = JSON.parse(mydata);
        } catch (e) {
            obj.r = "false";
            obj.msg = "JSON反序列化返回结果失败";
        }        
    } else {
        obj.r = "false";
        obj.msg = "返回的数据是空";        
    }
    return obj;
}
/*
*Post方式提交表单
*/
function postNewWin(url, jsonObj) {
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
}
/*
*Post方式提交表单
*/
function postNewWin(action, data) {
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
}
/*
*随机数
*/
function randomKey() {
    var hex = new Array('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f');
    var t = '';
    for (var i = 0; i < 32; i++) {
        t += hex[Math.floor(Math.random() * 16)];
    }

    return t.toUpperCase();
}


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
    $(overlay).fadeOut(1000, function () {
        //主标签页更新的时候使用了iframe做数据源,导致tab加载完成事件无法触发,所以在onUpdate 和 onSelect同时写了取消遮罩
        if (document.getElementById("overlay")) {
            body[0].removeChild(overlay);
        }
    });
}