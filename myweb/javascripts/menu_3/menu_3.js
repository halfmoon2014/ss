$(function () {
    tabMenuInit();
    tabTitleEven();
    /*标签数据更新后,激活*/
    $('#tabs').tabs({
        onUpdate: function (title) {
            $('#tabs').tabs('select', title);
        },
        onAdd: function (title) {
            tabTitleEven();
        }
    });
    $('#lmenu li a').click(function () {
        var tabTitle = $(this).text();
        var url = $(this).attr("url");
        addMainTab(tabTitle, url);
        $('#lmenu li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }).hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });
    //    longPolling(function (e) {
    //        console.log(1); 
    //    });

});

function myhelp(id) {
    var url = "m_myhelp.aspx?id=" + id;
    //chrome没有模态,先改为打开非模态
    window.open(url, "帮助文档", "height=600,width=800", "");
    //window.showModalDialog(url, "", "location:No;status:No;help:No;dialogWidth:800px;dialogHeight:600px;scroll:no;"); return false;
}

//main选项卡
function addMainTab(subtitle, url) {
    if (checkSession() == false) {
        var o = new Object();
        o.functionName = "goAddMainTab";
        var argList = new Array();
        argList[0] = subtitle;
        argList[1] = url;
        o.args = argList;
        reLoad(o);
    } else {
        goAddMainTab(subtitle, url)
    }

}

function goAddMainTab(subtitle, url) {
    if ($('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('select', subtitle);
    } else {
        //var m = $(document.getElementById("home"));

        if (document.getElementById("menu")) {
            $('#tabs').tabs('update', {
                tab: $(document.getElementById("menu")),
                options: {
                    title: subtitle,
                    content: createMainFrame(url)
                }
            });
        } else {
            $('#tabs').tabs('add', {
                title: subtitle,
                id: "menu",
                content: createMainFrame(url)
            });
        }
    }
}

function createMainFrame(url) {
    //href: "content_menu3.ashx?url=" + url

    /*20130607改为AJAX调用,20130609停用,因为SESSION超时不好处理
    var myurl = url;
    var r = "";
    var error = "";
    $.ajax({ type: 'post',
    url: 'content_menu3.aspx?url=' + myurl,
    data: {},
    async: false,
    error: function (e) {

    error = e;
    },
    success: function (data) {
    r = data;
    }
    })
    if (error == "") {
    return r;
    } else {
    $.messager.alert('提示信息', '连接失败!', 'info', function () {
    return "";
    });
    }*/
    return '<iframe  scrolling="auto" frameborder="0"  allowtransparency=true  src="content_menu3.aspx?url=' + url + '" style="width:100%;height:100%;"></iframe>';
}

/* end main选项卡*/
/*选项卡*/
function addTab(subtitle, url, obj) {
    var alone = $(obj).attr("alone");
    //先检查session
    if (checkSession() == false) {
        var o = new Object();
        o.functionName = "goAddTab";
        var argList = new Array();
        argList[0] = subtitle;
        argList[1] = url;
        argList[2] = alone;
        o.args = argList;
        reLoad(o);
    } else {
        goAddTab(subtitle, url, alone);
    }
}

function goAddTab(subtitle, url, alone) {
    if (subtitle != undefined) {
        url = (url.indexOf("?") >= 0 ? url + "&" : url + "?") + "title=" + encodeURIComponent(subtitle);
    }
    if (alone == 0) {
        if ($('#tabs').tabs('exists', subtitle)) {
            $('#tabs').tabs('select', subtitle);
        } else {
            $('#tabs').tabs('add', {
                title: subtitle,
                content: createFrame(url),
                closable: true,
                width: $('#mainPanle').width() - 10,
                height: $('#mainPanle').height() - 26,
                tools: [{
                    iconCls: 'icon-mini-refresh',
                    handler: function () {
                        tabMenuEven('refresh');
                    }
                }]
            });
        }
        //tabTitleEven();
    } else {
        //弹出模块
        window.open(url);
    }
}

function createFrame(url) {
    return '<iframe  scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
}
//end 选项卡
function tabTitleEven() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children("span").text();
        var onlyOpenTitle = $(document.getElementById("home")).panel('options').title;
        if (onlyOpenTitle != subtitle) {
            $('#tabs').tabs('close', subtitle);
        }
    })
    /*绑定右键菜单*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children("span").text();
        $('#mm').data("currtab", subtitle);

        return false;
    });
}

/*
*右键菜单 只初始化
*/
function tabMenuInit() {
    $('#mm').menu({
        onClick: function (item) {
            tabMenuEven(item.id);
        }
    });
    return false;
}

function tabMenuEven(action) {
    //首页
    var onlyOpenTitle = $(document.getElementById("home")).panel('options').title;
    var alltabs = $('#tabs').tabs('tabs');
    var currentTab = $('#tabs').tabs('getSelected');
    var allTabtitle = [];
    $.each(alltabs, function (i, n) {
        allTabtitle.push($(n).panel('options').title);
    })

    switch (action) {
        case "refresh":
            var iframe = $(currentTab.panel('options').content);
            if (iframe.length > 0) {//如果标签页没有iframe那么不用刷新,有可能是第一次进入的展示页
                var currtab_title = currentTab.panel('options').title;
                var src = iframe.attr('src');
                $('#tabs').tabs('update', {
                    tab: currentTab,
                    options: {
                        content: createFrame(src)
                    }
                })
            }
            break;
        case "close":

            var currtab_title = currentTab.panel('options').title;
            if (currtab_title != onlyOpenTitle) {
                $('#tabs').tabs('close', currtab_title);
            }
            break;
        case "closeall":
            $.each(allTabtitle, function (i, n) {
                if (n != onlyOpenTitle) {
                    $('#tabs').tabs('close', n);
                }
            });
            break;
        case "closeother":
            var currtab_title = currentTab.panel('options').title;
            $.each(allTabtitle, function (i, n) {
                if (n != currtab_title && n != onlyOpenTitle) {
                    $('#tabs').tabs('close', n);
                }
            });
            break;
        case "closeright":
            var tabIndex = $('#tabs').tabs('getTabIndex', currentTab);

            if (tabIndex == alltabs.length - 1) {
                //右边没有页面
                return false;
            }
            $.each(allTabtitle, function (i, n) {
                if (i > tabIndex) {
                    if (n != onlyOpenTitle) {
                        $('#tabs').tabs('close', n);
                    }
                }
            });

            break;
        case "closeleft":
            var tabIndex = $('#tabs').tabs('getTabIndex', currentTab);
            if (tabIndex == 1) {
                //首页不允许关闭
                return false;
            }
            $.each(allTabtitle, function (i, n) {
                if (i < tabIndex) {
                    if (n != onlyOpenTitle) {
                        $('#tabs').tabs('close', n);
                    }
                }
            });

            break;
        case "exit":
            $('#closeMenu').menu('hide');
            break;
    }
}

//等待框
function waitOff() {
    $("#Loading").fadeOut("normal", function () {
        $(this).remove();
    });
}

var pc;
$.parser.onComplete = function () {
    if (pc) clearTimeout(pc);
    pc = setTimeout(waitOff, 1000);
}

/*
*修改密码
*/
function myChangmm() {
    if (checkSession() == false) {
        reLoad(function () {
            var r = openModal('../webpage/lss.aspx?wid=143', '', 'dialogWidth=300px;dialogHeight=400px');
        });
    } else {
        var r = openModal("../webpage/lss.aspx?wid=143", "", "dialogWidth=300px;dialogHeight=400px");
    }
}

/*
*用户注消
*清空SESSION
*/
function window_onunload(type) {
    $.ajax({ type: 'post',
        url: '../webuser/ws.asmx/SessionEnd',
        async: false,
        data: {},
        error: function (e) { r = -1; },
        success: function (data) {
            var r = myAjaxData(data);
            if (type == -1) {
                closeWebPage();
            } else if (type == 0) {
                window.location.href = r.r;
            }
        }
    });
}

/*
*关闭浏览器FF跳转空页
*/
function closeWebPage() {
    if (getUserBrowser() == "IE") {
        if (getBrowserVer() == "6.0") {
            window.opener = null; window.close();
        }
        else {
            window.open('', '_top'); window.top.close();
        }
    }
    else if (getUserBrowser() == "Firefox") {
        window.location.href = 'about:blank ';
        //window.history.go(-2);
    }
    else {
        window.opener = null;
        window.open('', '_self', '');
        window.close();
    }
}