$(function () {
    //选项卡右击菜单
    tabMenuInit();
    //选项卡事件绑定
    tabTitleEven();

    /*标签数据更新后,激活*/
    $('#tabs').tabs({
        onUpdate: function (title) {
            $('#tabs').tabs('select', title);
            hideLoading();
        },
        onAdd: function (title) {
            //选项卡事件绑定
            tabTitleEven();
        },
        onSelect: function (title, index) {
            hideLoading();
        }
    });

    //菜单目录事件
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

    //这个一定要有,不然布局会失效
    $("#menubody").layout({
        onCollapse: function (a) {
            console.log("onCollapse");
        },
        onExpand: function (a) {
            console.log("onExpand");
        }
    });

});

//main选项卡
function addMainTab(subtitle, url) {
    showLoading();
    checkSessionAsy(function (result) {
        if (result) {
            goAddMainTab(subtitle, url)
        } else {
            hideLoading();            
            reLoad(function () {
                goAddMainTab(subtitle, url)
            })
        }
    });
}

function goAddMainTab(subtitle, url) {
    if ($('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('select', subtitle);
        hideLoading();
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
    return '<iframe  scrolling="auto" frameborder="0"  allowtransparency=true  src="content_menu3.aspx?url=' + url + '" style="width:100%;height:100%;"></iframe>';
}

/* end main选项卡*/
/*选项卡*/
function addTab(subtitle, url, obj) {
    var alone = $(obj).attr("alone");
    checkSessionAsy(function (result) {
        if (result) {
            goAddTab(subtitle, url, alone);
        } else {            
            reLoad(function () {
                goAddTab(subtitle, url, alone);
            })
        }
    });
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
        openWin(url);
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
function passwordModify() {
    var r = openModal('../webpage/lss.aspx?wid=143', '', 'dialogWidth=300px;dialogHeight=400px');
}

/*
*用户注消
*清空SESSION
*/
function window_onunload(type) {
    $.ajax({
        type: 'post',
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
    if (browser.versions.trident) {
        if (browser.versions.ver == "6.0") {
            window.opener = null; window.close();
        }
        else {
            openWin('', '_top'); window.top.close();
        }
    }
    else if (browser.versions.gecko) {
        window.location.href = 'about:blank ';
    }
    else {
        window.location.href = "about:blank";
        window.close();
    }
}

function myhelp(id) {
    var url = "m_myhelp.aspx?id=" + id;
    var name = '帮助文档';                            //网页名称 
    var iWidth = 800;                          //弹出窗口的宽度; 
    var iHeight = 600;                         //弹出窗口的高度; 
    //获得窗口的垂直位置 
    var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
    //获得窗口的水平位置 
    var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
    //chrome没有模态,先改为打开非模态
    openWin(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=0,titlebar=no');
    //window.showModalDialog(url, "", "location:No;status:No;help:No;dialogWidth:800px;dialogHeight:600px;scroll:no;"); return false;
}