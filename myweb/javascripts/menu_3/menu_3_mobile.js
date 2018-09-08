$(function () {
    $('#mobileMenu li .nav').click(function () {
        var tabTitle = $(this).text();
        var url = $(this).attr("url");
        $("#collapse").click();
        addMainTab(tabTitle, url);
    })
});

function addMainTab(tabTitle, url) {
    if ($("#content_menu3_mydiv_mobile").attr("url") != url) {
        showLoading();
        checkSessionAsy(function (result) {
            if (result) {
                createMainFrame(url);
            } else {
                hideLoading();
                var o = new Object();
                o.functionName = "createMainFrame";
                var argList = new Array();
                argList[0] = url;
                o.args = argList;
                reLoad(o);
            }
        });
    }
}
function createMainFrame(url) {
    $.ajax({
        type: 'post',
        url: 'content_menu3.aspx?url=' + url + "&m=data",
        data: {},
        async: true,
        error: function (e) {

        },
        success: function (data) {
            $("#content_menu3_mydiv_mobile").html(data);
            $("#content_menu3_mydiv_mobile").attr("url", url)
            initContent_menu3_mydiv_mobile();
            hideLoading();
        }
    });
}
function initContent_menu3_mydiv_mobile() {

    $('.list-group-item').delegate('a', 'click', function (e) {
        if ($(e.target).is("a")) {
            window.open($(e.target).parent().attr("cmd"));
        }
    });
    $('.list-group-item').delegate('span', 'click', function (e) {
        if ($(e.target).is("span")) {
            var menuID = $(e.target).parent().attr("menuID");
            myhelp(menuID)
        }
    });
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
    window.open(url, name, 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=0,titlebar=no');
    //window.showModalDialog(url, "", "location:No;status:No;help:No;dialogWidth:800px;dialogHeight:600px;scroll:no;"); return false;
}

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