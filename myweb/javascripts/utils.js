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
function getBrowserVer() {
    var br = navigator.userAgent.toLowerCase();
    var Ver = (br.match(/.+(?:rv|it|ra|ie)[\/: ]([\d.]+)/) || [0, '0'])[1];
    return Ver;

}
/*
* 获取浏览器类型。
*/
function getUserBrowser() {
    var browserName = navigator.userAgent.toLowerCase();
    if (/msie/i.test(browserName) && !/opera/.test(browserName)) {

        return "IE";
    } else if (/firefox/i.test(browserName)) {

        return "Firefox";
    } else if (/chrome/i.test(browserName) && /webkit/i.test(browserName) && /mozilla/i.test(browserName)) {

        return "Chrome";
    } else if (/opera/i.test(browserName)) {

        return "Opera";
    } else if (/webkit/i.test(browserName) && !(/chrome/i.test(browserName) && /webkit/i.test(browserName) && /mozilla/i.test(browserName))) {

        return "Safari";
    } else {
        return "unKnow";
    }
}
/*
Javascript 操作select控件大全（新增、修改、删除、选中、清空、判断存在等） 
Posted on 2007-08-08 14:56 礼拜一 阅读(82638) 评论(40) 编辑 收藏  
1判断select选项中 是否存在Value="paraValue"的Item 
2向select选项中 加入一个Item 
3从select选项中 删除一个Item 
4删除select中选中的项 
5修改select选项中 value="paraValue"的text为"paraText" 
6设置select中text="paraText"的第一个Item为选中 
7设置select中value="paraValue"的Item为选中 
8得到select的当前选中项的value 
9得到select的当前选中项的text 
10得到select的当前选中项的Index 
11清空select的项 
js 代码*/
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
        $.messager.alert('提示信息', '成功删除', 'info');
    } else {
        $.messager.alert('提示信息', '成功删除', 'info');
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
        $.messager.alert('提示信息', '成功修改', 'info');
    } else {

        $.messager.alert('提示信息', '成功修改', 'info');
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

        $.messager.alert('提示信息', '成功选中', 'info');
    } else {

        $.messager.alert('提示信息', '该select中 不存在该项', 'info');
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
        mydata = data.documentElement.textContent;
    } else {
        mydata = data.text;
    }
    /*20140316 如果返回的是\那么js的eval会去掉*/
    mydata = mydata.replace(/\\/g, "/");
    return eval("(" + mydata + ")");
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