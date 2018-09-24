$(function () {
    $('#mobileMenu li .nav').click(function () {
        var tabTitle = $(this).text();
        var url = $(this).attr("url");
        $("#collapse").click();
        addMainTab(tabTitle, url);
    })
    hideLoading();
});

function addMainTab(tabTitle, url) {
    if ($("#content_menu3_mydiv_mobile").attr("url") != url) {
        showLoading();
        checkSessionAsy(function (result) {
            if (result) {
                createMainFrame(url);
            } else {
                hideLoading();                
                reLoad(function () { createMainFrame(url) });
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
            showLoading();
            var url = $(e.target).parent().attr("cmd");
            url = (url.indexOf("?") >= 0 ? url + "&" : url + "?") + "title=" + encodeURIComponent($(e.target).html());
            //一定要使用同步的,不然IOS打开的窗口会提示是否打开
            if (checkSession()) {                
                window.open(url);
                hideLoading();
            } else {
                hideLoading();
                reLoad(function () {
                    window.open(url);                    
                });
            }            
        }
    });
    $('.list-group-item').delegate('span', 'click', function (e) {
        if ($(e.target).is("span")) {
            showLoading();
            if (checkSession()) {
                myhelp($(e.target).parent().attr("menuID"));
                hideLoading();
            } else {
                reLoad(function () {
                    myhelp($(e.target).parent().attr("menuID"));
                    hideLoading();
                });
            }                       
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