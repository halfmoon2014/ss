define(["jquery", "utils", "myweb", "xtsz", "swalProcess"], function ($, utils, myweb, xtsz, swalProcess) {
    var getValSQL = function (s, i) {
        return $.trim(xtsz.getObj(s, i).val().replace(/'/g, "''"));
    }
    //保存
    var ok_click = function () {
        $('#ok').attr('disabled', 'disabled');
        var wid = document.getElementById("wid").value;
        var lx = document.getElementById("lx").value;
        var str = "";
        var css0, css, mrz, bds, ord, mobileord, width, widthm, qwidth, qwidthm, id, mc, visible, htmlid, westwidth, eastwidth, northheight, southheight, dwidth, dheight, readonly, type, bz, nwebid, naspx, event, session, zb, yy;
        var westwidthm, eastwidthm, northheightm, southheightm

        westwidth = $.trim(xtsz.getObj("westwidth").val());
        if (westwidth == "") { westwidth = "0"; }
        eastwidth = $.trim(xtsz.getObj("eastwidth").val());
        if (eastwidth == "") { eastwidth = "0"; }
        southheight = $.trim(xtsz.getObj("southheight").val());
        if (southheight == "") { southheight = "0"; }
        northheight = $.trim(xtsz.getObj("northheight").val());
        if (northheight == "") { northheight = "0"; }
        westwidthm = $.trim(xtsz.getObj("westwidthm").val());
        if (westwidthm == "") { westwidthm = "0"; }
        eastwidthm = $.trim(xtsz.getObj("eastwidthm").val());
        if (eastwidthm == "") { eastwidthm = "0"; }
        southheightm = $.trim(xtsz.getObj("southheightm").val());
        if (southheightm == "") { southheightm = "0"; }
        northheightm = $.trim(xtsz.getObj("northheightm").val());
        if (northheightm == "") { northheightm = "0"; }

        var data = {};
        data.row = new Array();
        for (var i = 0; i < xtsz.getRowNum() ; i++) {
            ord = $.trim(xtsz.getObj("ord", i).val());
            if (ord == "") { ord = "0"; }
            mobileord = $.trim(xtsz.getObj("mobileord", i).val());
            if (mobileord == "") { mobileord = "0"; }
            nwebid = $.trim(xtsz.getObj("nwebid", i).val());
            if (nwebid == "") { nwebid = "0"; }
            width = $.trim(xtsz.getObj("width", i).val());
            if (width == "") { width = "0"; }
            qwidth = $.trim(xtsz.getObj("qwidth", i).val());
            if (qwidth == "") { qwidth = "0"; }

            widthm = $.trim(xtsz.getObj("widthm", i).val());
            if (widthm == "") { widthm = "0"; }
            qwidthm = $.trim(xtsz.getObj("qwidthm", i).val());
            if (qwidthm == "") { qwidthm = "0"; }

            dwidth = $.trim(xtsz.getObj("dwidth", i).val());
            if (dwidth == "") { dwidth = "0"; }
            dheight = $.trim(xtsz.getObj("dheight", i).val());
            if (dheight == "") { dheight = "0"; }
            id = $.trim(xtsz.getObj("id", i).val());
            if (id == "") { id = "0"; }
            visible = $.trim(xtsz.getObj("visible", i).is(':checked') ? "1" : "0");
            readonly = $.trim(xtsz.getObj("readonly", i).is(':checked') ? "1" : "0");
            type = $.trim(xtsz.getObj("type", i).val());
            bz = getValSQL("bz", i);
            css = getValSQL("css", i);
            css0 = getValSQL("css0", i);
            mc = $.trim(xtsz.getObj("mc", i).val());
            event = getValSQL("event", i);
            session = getValSQL("session", i);
            if (session.toLowerCase() == "userid" || session.toLowerCase() == "tzid" || session.toLowerCase() == "username") {
                alert('session 不能是 [userid]或[tzid]或[username] ');
                $('#ok').removeAttr("disabled")
                return false;
            }
            zb = $.trim(xtsz.getObj("zb", i).is(':checked') ? "1" : "0");
            yy = getValSQL("yy", i)
            naspx = getValSQL("naspx", i);
            htmlid = $.trim(xtsz.getObj("htmlid", i).val());
            if (id == 0 && htmlid.length == 0 && qwidth=="0" && mc.length==0 ) { continue; }
            mrz = getValSQL("mrz", i);
            bds = getValSQL("bds", i);
            var dataRow = {};
            dataRow.id = id;
            dataRow.css0 = css0;
            dataRow.css = css;
            dataRow.mrz = mrz;
            dataRow.bds = bds;
            dataRow.webid = wid;
            dataRow.lx = lx;
            dataRow.mc = mc;
            dataRow.ord = ord;
            dataRow.mobileord = mobileord;
            dataRow.width = width;
            dataRow.qwidth = qwidth;
            dataRow.widthm = widthm;
            dataRow.qwidthm = qwidthm;

            dataRow.westwidth = westwidth;
            dataRow.eastwidth = eastwidth;
            dataRow.northheight = northheight;
            dataRow.southheight = southheight;
            dataRow.westwidthm = westwidthm;
            dataRow.eastwidthm = eastwidthm;
            dataRow.northheightm = northheightm;
            dataRow.southheightm = southheightm;
            dataRow.dwidth = dwidth;
            dataRow.dheight = dheight;
            dataRow.visible = visible;
            dataRow.readonly = readonly;
            dataRow.type = type;
            dataRow.bz = bz;
            dataRow.nwebid = nwebid;
            dataRow.event = event;
            dataRow.yy = yy;
            dataRow.zb = zb;
            dataRow.session = session;
            dataRow.naspx = naspx;
            dataRow.htmlid = htmlid;
            data.row.push(dataRow);
        }
        if (data.row.length == 0) {
            swalProcess.sAlert('没有可更新的记录!', function () {
                $('#ok').removeAttr("disabled")
            });
        } else {

            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/UpSYJLayout',
                data: { wid: wid, data: JSON.stringify(data) },
                error: function (e) {
                    swalProcess.sAlert('连接失败!', function () {
                        $('#ok').removeAttr("disabled")
                    });
                },
                success: function (data) {
                    var r = utils.myAjaxData(data);
                    if (r.r == 'true') {
                        swalProcess.sAlert('保存成功!', "success", function () {
                            $('#ok').removeAttr("disabled")
                            location.reload();
                        });
                    } else {
                        swalProcess.sAlert('保存失败!', function () {
                            $('#ok').removeAttr("disabled")
                        });
                    }
                }
            })
        }
    }
    //行得到焦点,变色
    var myselect = function (obj) {
        //alert(g);
        var g = $(obj.parentNode.parentNode).attr("rownum");
        $.each($(".tbbody"), function (i, r) {
            if (i != g) {
                $(r).removeClass("selectedhighlight");
            } else {
                $(r).addClass("selectedhighlight");
            }
        });
        //$("#zdwhtb tr").eq(i-1)
    }
    //fz
    var fz_click = function () {

        swal({
            title: "Copy",
            text: "输入wid",
            type: "input",
            showCancelButton: true,
            closeOnConfirm: false,
            inputPlaceholder: ""
        }, function (inputValue) {
            if (inputValue != "") {
                rFZ(inputValue)
            }
        });
    }
    var rFZ = function (oldwid) {
        var newwid = document.getElementById("wid").value;
        var lx = document.getElementById("lx").value;
        if (!isNaN(oldwid) && oldwid != "0" && oldwid != "") {
            $.ajax({
                type: 'post',
                url: '../webuser/Ws.asmx/WebsjFzZd',
                data: { wid: oldwid, newwid: newwid, bs: lx },
                error: function (e) {
                    swalProcess.sAlert('连接失败!');
                },
                success: function (data) {
                    var r = utils.myAjaxData(data)
                    if (r.r == 'true') {
                        swalProcess.sAlert('复制成功!', "success", function () { parent.closeTab("refresh", false); });
                    } else {
                        swalProcess.sAlert('复制失败!');
                    }
                }
            })
        } else {
            swalProcess.sAlert('复制wid无效!');
        }
    }

    //显示提示
    var showtitp_click = function () {
        var show = $('#formts').css('display');
        if (show == 'block') {
            $('#formts').hide();
        } else {
            $('#formts').show();
            //使用了bootstrap样式,需要增加12的高度            
            utils.autoTextarea(document.getElementById("helpTextarea"), 12);// 调用
        }
    }

    var btnmb_click = function (ev) {
        var e = ev || window.event;
        $(ev.target).toggleClass("btn-primary");
        if ($(ev.target).hasClass("btn-primary")) {
            document.getElementById("ismobile").value="1"
        } else {
            document.getElementById("ismobile").value = "0"
        }
        jsSubmit();
    }

    var hiddenMobile = function (showMobile) {
        return false;
        var mobileList = new Array();
        mobileList.push("qwidthm")
        mobileList.push("widthm")
        mobileList.push("mobileord")
        var bookList = new Array();
        bookList.push("qwidth")
        bookList.push("width")
        mobileList.push("ord")

        if (showMobile) {            
            var hiddenList = bookList;
            var showList = mobileList;
        } else {//隐藏手机
            var hiddenList = mobileList;
            var showList = bookList;
        }

        var head = document.getElementById("zdwhtb").rows;
        for (var d = 0; d < head[0].cells.length; d++) {
            if (hiddenList.indexOf($(head[0].cells[d]).attr("field")) != -1) {
                head[0].cells[d].style.display = "none";
                try {
                    for (var m = 1; m <= xtsz.getRowNum() ; m++) {
                        head[m].cells[d].style.display = "none";
                    }
                } catch (e) {
                    console.log(e.message);
                }
            }

            if (showList.indexOf($(head[0].cells[d]).attr("field")) != -1) {
                head[0].cells[d].style.display = "";        
                try {
                    for (var m = 1; m <= xtsz.getRowNum() ; m++) {
                        head[m].cells[d].style.display = "";
                    }
                } catch (e) {
                    console.log(e.message);
                }
            }

        }        
    }

    var jsSubmit=function() {
        var url = location.search; //获取url中"?"符后的字串   
        var p = "";
        if (url.indexOf("?") != -1) {
            var str = url.substr(1);
            strs = str.split("&");
            for (var i = 0; i < strs.length; i++) {
                if (strs[i].split("=")[0] == "ismobile") {
                    
                } else {
                    p+="&"+strs[i].split("=")[0]+"="+ unescape(strs[i].split("=")[1]);
                }
                
            }
        }        
        window.location.href = location.origin + location.pathname + "?ismobile=" + document.getElementById("ismobile").value + p;
    }
    var start = function () {
        $(document).ready(function () {
            if (document.getElementById("ismobile").value == "1") {
                $("#btnmb").toggleClass("btn-primary");
            }
            xtsz.init();
            $(":text", ".tbbody").focus(function (e) {
                myselect(e.currentTarget);
            });
            $("#showtitp").bind("click", function () { showtitp_click(); });
            $("#ok").bind("click", function () { ok_click(); });
            $("#fz").bind("click", function () { fz_click(); });
            $("#fb").bind("click", function () { fb_click(); });
            $("#btnmb").bind("click", function (ev) { btnmb_click(ev); });

            $("[field]", $(".tbbody")).each(function (i, n) {
                //var f = "field_" + $(n).attr("field");
                //$(n).width("100%");
            });

            if (document.getElementById("lx").value == "z") {
                //布局面板
                var head = document.getElementById("zdwhtb").rows;
                for (var d = 0; d < head[0].cells.length; d++) {
                    if (head[0].cells[d].innerHTML.indexOf("排布") >= 0 || head[0].cells[d].innerHTML.indexOf("下级webid") >= 0 || head[0].cells[d].innerHTML.indexOf(" 下级aspx") >= 0 || head[0].cells[d].innerHTML.indexOf("复-宽度") >= 0 || head[0].cells[d].innerHTML.indexOf("复-高度") >= 0 || head[0].cells[d].innerHTML.indexOf("htmlid") >= 0) { }
                    else {
                        head[0].cells[d].style.display = "none";
                        try {
                            for (var m = 1; m <= xtsz.getRowNum() ; m++) {
                                head[m].cells[d].style.display = "none";
                            }
                        } catch (e) { }
                    }
                }
                $("#cxtj").attr("style", "display:none");
            } else {
                //div布局
                var head = document.getElementById("zdwhtb").rows;
                for (var d = 0; d < head[0].cells.length; d++) {
                    if (head[0].cells[d].innerHTML.indexOf("复-高度") >= 0 || head[0].cells[d].innerHTML.indexOf("复-宽度") >= 0) {
                        head[0].cells[d].style.display = "none";
                        try {
                            for (var m = 1; m <= xtsz.getRowNum() ; m++) {
                                head[m].cells[d].style.display = "none";
                            }
                        } catch (e) { console.log(e.message) }
                    }
                }
            }
            utils.hideLoading();
        });
    }

    var fieldsHidden = function (arr,showKey) {
        for (var i = 0; i < arr.length; i++) {
            if (showKey != arr[i].key) {
                $("[field='" + arr[i].f+ "']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='td_" + arr[i].f + "']", $("#cxtj")).attr("style", "display:none");
                $("[field='" + arr[i].m + "']", $("#cxtj")).parent().attr("style", "display:none");
                $("[field='td_" + arr[i].m + "']", $("#cxtj")).attr("style", "display:none");
            }
        }
    }

    return {
        start:start
    }
    
});