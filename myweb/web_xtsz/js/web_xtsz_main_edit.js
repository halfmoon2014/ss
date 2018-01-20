
var myALink = function (obj) {
    // alert(obj.innerHTML);
    var wid = document.getElementById("wid").value;
    var url = $(obj).attr("url");
    var subtitle = $.trim(obj.innerHTML);
    if (url.indexOf("?") > 0) {
        url = url + "&wid=" + wid;
    } else {
        url = url + "?wid=" + wid;
    }
    addTab(subtitle, url);
}
/*标签*/
var addTab = function (subtitle, url) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url, subtitle),
            closable: true,
            width: $('#mainPanle').width() - 10,
            height: $('#mainPanle').height() - 26,
            tools: [{
                iconCls: 'icon-mini-refresh',
                handler: function () {
                    closeTab('refresh');
                }
            }]


        });
    } else {
        $('#tabs').tabs('select', subtitle);
    }
    tabClose();
}

var createFrame = function (url, subtitle) {
    var s = '<iframe  scrolling="auto" frameborder="0"    src="' + url + '" style="width:100%;height:100%;"></iframe>';
    return s;
}
var tabClose = function () {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children("span").text();
        $('#tabs').tabs('close', subtitle);
    })

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
/*右键菜单*/
var tabCloseEven = function () {
    $('#mm').menu({
        onClick: function (item) {
            closeTab(item.id);
        }
    });

    return false;
}
var closeTab = function (action, ts) {
    var onlyOpenTitle = "主页";
    var alltabs = $('#tabs').tabs('tabs');
    var currentTab = $('#tabs').tabs('getSelected');
    var allTabtitle = [];
    $.each(alltabs, function (i, n) {
        allTabtitle.push($(n).panel('options').title);
    })

    switch (action) {
        case "refresh":
            if (ts != null && ts != undefined && ts == false) {
                var iframe = $(currentTab.panel('options').content);
                var src = iframe.attr('src');
                $('#tabs').tabs('update', {
                    tab: currentTab,
                    options: {
                        content: createFrame(src)
                    }
                })
            } else {
                $.messager.confirm("刷新提示", "确定要刷新吗?", function (b) {
                    if (b == true) {
                        var iframe = $(currentTab.panel('options').content);
                        var src = iframe.attr('src');
                        $('#tabs').tabs('update', {
                            tab: currentTab,
                            options: {
                                content: createFrame(src)
                            }
                        })
                    }
                });
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

/*等待框*/
var waitOff = function () {
    $("#Loading").fadeOut("normal", function () {
        $(this).remove();
    });
}

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

var pc;
$.parser.onComplete = function () {
    if (pc) clearTimeout(pc);
    pc = setTimeout(waitOff, 1000);
}
$(document).ready(function () {
    tabCloseEven();
    autoTextarea(document.getElementById("textarea1"))
    autoTextarea(document.getElementById("textarea2"))
    autoTextarea(document.getElementById("textarea3"))
    autoTextarea(document.getElementById("textarea4"))
});