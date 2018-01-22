
/*
*系统模块
*/
$(function () {    
    //loadMark 标识是否是第一次加载.首次不查的情况下,不用加载数据
    var loadMark = 1;
    if ($("#wid").attr("IsEasyLayout") == "1" && $("#divPager").length > 0) {
        //easy模块并且存在数据窗口
        waitOn(function () {
            loadInfo(loadMark);
        });
    } else {
        waitOff(loadMark);
    }

});
/*
*IE回车换TAB
*/
$("body").keydown(function () {
    if (getUserBrowser() == "IE") {
        if (window.event.keyCode == 13) {
            window.event.keyCode = 9;
        }
    }
});
/*
*单一模块 数据加载
*/
function loadInfo(loadMark) {
    if (loadMark == undefined) { loadMark = 0; }
    var headParams = getUploadParameters("json");
    var filterRow = "";
    if ($("#sysFindSortRow").length > 0) {
        filterRow = $("#sysFindSortRow").val();
    }
    var orderBy = "";
    if ($("#orderBy").length > 0) {
        orderBy = $("#orderBy").val();

    }
    var pageSize = "";
    if ($("#pageSize").length > 0) {
        pageSize = $("#pageSize").val();
    }
    pagerObj = new PagerObj({
        wid: $("#wid").val(),
        loadMark: loadMark,
        url: '../AjaxHandler/ProcPager.aspx',
        extendParams: eval("({" + headParams + (headParams == "" ? "" : ",") + "filterRow:\"" + filterRow + "\",orderBy:\"" + orderBy + "\",pageSize:\"" + pageSize + "\"})"),
        callbackFn: function () { loadInfoCallback(loadMark); }
    });
    pagerObj.Page(1);
}
/*
*数据加载完后自定义回调
*/
function loadInfoCallback(loadMark) {
    if (getContentTableId() != null) {
        var key = "table_Content_" + $("#wid").val();
        /*
        $("#" + key + " tr").mouseover(function () {
        $(this).addClass("over");
        }).click(function () {
        $("#" + key + " tr").removeClass("selected");
        $(this).addClass("selected");

        }).mouseout(function () {
        $(this).removeClass("over");
        })去掉鼠标事件*/
        $("#" + key + " tr").click(function () {
            $("#" + key + " tr").removeClass("selected");
            $(this).addClass("selected");

        });
    }
    if (typeof (beforeFadeout) == "function") {
        beforeFadeout(loadMark, function () {
            waitOff(loadMark);
        });
    } else {
        waitOff(loadMark);
    }
}

/*
*等待动画开始
*/
function waitOn(callback) {
    $("#Loading").fadeTo("fast", 0.5, function () {
        //开始后回调
        if (typeof (callback) == "function") {
            callback();
        }
    });
}

/*
*等待动画结束
*/
function waitOff(loadMark) {
    $("#Loading").fadeOut("normal", function () {
        if (typeof (afterFadeout) == "function") {
            //回调函数
            afterFadeout(loadMark);
        }
    });
}
/*
*获取查询条件 取表达式
*所有要查询条件都放在parameters='upload'的table 中,而且都有一个bds属性        
*needType:json,string
*iframeName 框架名 contentDocument
*/
function getUploadParameters(needType, iframeName) {
    var uploadParameters = "";
    var bdsObj;
    if (iframeName == null || iframeName == "" || iframeName == undefined) {
        bdsObj = $("[bds]", $("[parameters='upload']"));
    } else {
        bdsObj = $("[bds]", $("[parameters='upload']", window.frames[iframeName].contentDocument));
    }
    bdsObj.each(function (i, n) {
        //取控件值
        var v = getCtrlValue(n, iframeName);
        //将表达式中的@为正确的值
        var z = $(n).attr("bds").replace(/@/g, v);
        if (v == '@' || v == '') {
            //如果值为空或者为@,那么就不传入
            z = "";
        }
        //URL里面包含%,接收页面使用Request.QueryString接收的参数不正常
        //例如djh like '%100001%'
        if (needType == "json") {
            //构造JSON        
            uploadParameters += "\"@" + $(n).attr("yy") + "\":\"" + z + "\",";
        } else if (needType == "string") {
            uploadParameters += "@" + $(n).attr("yy") + "=" + z + "&";
        }

    });
    if (uploadParameters != "") {
        uploadParameters = uploadParameters.substring(0, uploadParameters.length - 1);
    }
    return uploadParameters;
}
/*
*取方位控件上的值
*/
function getCtrlValue(n, iframeName) {

    if ($(n).attr("type") == "checkbox") {
        //单选框
        return ($(n).is(':checked') ? 1 : 0)
    } else {
        var c_class = $(n).attr("class");
        if (c_class != undefined && c_class.indexOf("easyui-datebox") >= 0) {
            //日期控件
            if (iframeName == null || iframeName == "" || iframeName == undefined) {
                return $.trim($(n).datebox("getValue"));
            } else {
                //查询框架中的日期
                return $.trim(window.frames[iframeName].contentWindow.getEayuiDatebox($(n).attr("id")));
            }
        } else {
            //文本
            return $.trim($(n).val());
        }
    }

}
/*
*在框架模式下,日期控制要控制这个函数取
*/
function getEayuiDatebox(objName) {
    return $("#" + objName).datebox("getValue");
}
/*
*iframe切换
*/
function iframeSwitch(iframeName, wid) {
    var paramsObj = eval("({" + getUploadParameters("json", iframeName) + "})");
    var urlParms = "";
    $.each(paramsObj, function (myName, myValue) {
        urlParms += "&" + myName.substring(1, myName.length) + "=" + myValue;
    });
    $("#" + iframeName).attr("src", "lss.aspx?wid=" + wid + urlParms);
}
/*
*系统模块end
*/
