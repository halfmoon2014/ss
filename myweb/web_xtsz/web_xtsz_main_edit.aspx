<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_xtsz_main_edit.aspx.cs"
    Inherits="web_xtsz_web_xtsz_main_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <ctrl:DefaultHeader  id="sysHead" runat="server"   />
</head>
<body id="editbody" runat="server" class="easyui-layout">

</body>
<input type="hidden" id="wid" runat="server" />
</html>
<script>

    $(function () {        
        tabCloseEven();
    });

    function myALink(obj) {
       // alert(obj.innerHTML);
        var wid=document.getElementById("wid").value;
        var url = $(obj).attr("url");
        var subtitle=$.trim(obj.innerHTML);
        if (url.indexOf("?") > 0) {
            url = url + "&wid=" + wid ;
        } else {
            url = url + "?wid=" + wid;
        }
        addTab(subtitle, url);
    }
    /*标签*/
    function addTab(subtitle, url) {
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

    function createFrame(url, subtitle) {
        var s = '<iframe  scrolling="auto" frameborder="0"    src="' + url + '" style="width:100%;height:100%;"></iframe>';
        return s;
    }
    function tabClose() {
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
    function tabCloseEven() {
        $('#mm').menu({
            onClick: function (item) {
                closeTab(item.id);
            }
        });

        return false;
    }
    function closeTab(action,ts) {
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
</script>
