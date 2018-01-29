require.config({
    paths: {
        "jquery": "../../javascripts/jquery/1.12.4/jquery.min",
        "utils": "../../javascripts/utilsA",
        "myweb": "../../javascripts/myjs/mywebA",
        "xtsz": "../../javascripts/xtsz/xtsz"
    }
})
require(["jquery", "utils", "myweb", "xtsz"], function ($, utils, myweb, xtsz) {

   
    var sr = function (s, i) {
        return $.trim(xtsz.myVale(s, i).val().replace(/'/g, "''"));
    }
    //保存
    var ok_click = function () {
        $('#ok').attr('disabled', 'disabled');
        var wid = document.getElementById("wid").value;
        var lx = document.getElementById("lx").value;
        var str = "";
        var css0, css, mrz, bds, ord,mobileord, width, qwidth, id, mc, visible, htmlid, westwidth, eastwidth, northheight, southheight, dwidth, dheight, readonly, type, bz, nwebid, naspx, event, session, zb, yy;
        westwidth = $.trim(xtsz.myVale("westwidth").val());
        if (westwidth == "") { westwidth = "0"; }
        eastwidth = $.trim(xtsz.myVale("eastwidth").val());
        if (eastwidth == "") { eastwidth = "0"; }
        southheight = $.trim(xtsz.myVale("southheight").val());
        if (southheight == "") { southheight = "0"; }
        northheight = $.trim(xtsz.myVale("northheight").val());
        if (northheight == "") { northheight = "0"; }
        var data = {};
        data.row = new Array();
        for (var i = 0; i < xtsz.getRowNum() ; i++) {
            ord = $.trim(xtsz.myVale("ord", i).val());
            if (ord == "") { ord = "0"; }
            mobileord = $.trim(xtsz.myVale("mobileord", i).val());
            if (mobileord == "") { mobileord = "0"; }
            nwebid = $.trim(xtsz.myVale("nwebid", i).val());
            if (nwebid == "") { nwebid = "0"; }
            width = $.trim(xtsz.myVale("width", i).val());
            if (width == "") { width = "0"; }
            qwidth = $.trim(xtsz.myVale("qwidth", i).val());
            if (qwidth == "") { qwidth = "0"; }
            dwidth = $.trim(xtsz.myVale("dwidth", i).val());
            if (dwidth == "") { dwidth = "0"; }
            dheight = $.trim(xtsz.myVale("dheight", i).val());
            if (dheight == "") { dheight = "0"; }
            id = $.trim(xtsz.myVale("id", i).val());
            if (id == "") { id = "0"; }
            visible = $.trim(xtsz.myVale("visible", i).is(':checked') ? "1" : "0");
            readonly = $.trim(xtsz.myVale("readonly", i).is(':checked') ? "1" : "0");
            type = $.trim(xtsz.myVale("type", i).val());
            bz = sr("bz", i);
            css = sr("css", i);
            css0 = sr("css0", i);
            mc = $.trim(xtsz.myVale("mc", i).val());
            event = sr("event", i);
            session = sr("session", i);
            if (session.toLowerCase() == "userid" || session.toLowerCase() == "tzid" || session.toLowerCase() == "username") {
                alert('session 不能是 [userid]或[tzid]或[username] ');
                $('#ok').removeAttr("disabled")
                return false;
            }
            zb = $.trim(xtsz.myVale("zb", i).is(':checked') ? "1" : "0");
            yy = sr("yy", i)
            naspx = sr("naspx", i);
            htmlid = $.trim(xtsz.myVale("htmlid", i).val());
            if (id == 0 && htmlid.length == 0) { continue; }
            mrz = sr("mrz", i);
            bds = sr("bds", i);
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
            dataRow.westwidth = westwidth;
            dataRow.eastwidth = eastwidth;
            dataRow.northheight = northheight;
            dataRow.southheight = southheight;
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
            utils.sAlert('没有可更新的记录!', true, function () {
                $('#ok').removeAttr("disabled")
            });
        } else {
           
            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/UpSYJLayout',
                data: { wid: wid, data: JSON.stringify(data) },
                error: function (e) {
                    utils.sAlert('连接失败!', true, function () {
                        $('#ok').removeAttr("disabled")
                    });
                },
                success: function (data) {
                    var r = utils.myAjaxData(data);
                    if (r.r == 'true') {
                        utils.sAlert('保存成功!', true, function () {
                            $('#ok').removeAttr("disabled")
                            location.reload();
                        });
                    } else {
                        utils.sAlert('保存失败!', true, function () {
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
                url: '../webuser/ws.asmx/websj_fz_zd',
                data: { wid: oldwid, newwid: newwid, bs: lx },
                error: function (e) {
                    utils.sAlert('连接失败!');
                },
                success: function (data) {
                    var r = utils.myAjaxData(data)
                    if (r.r == 'true') {
                        utils.sAlert('复制成功!', true, function () { parent.closeTab("refresh", false); });
                    } else {
                        utils.sAlert('复制失败!');
                    }
                }
            })
        } else {
            utils.sAlert('复制wid无效!');
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
    xtsz.init();
  
    $(":text", ".tbbody").focus(function (e) {
        myselect(e.currentTarget);
    });
    $("#showtitp").bind("click", function () { showtitp_click(); });
    $("#ok").bind("click", function () { ok_click(); });
    $("#fz").bind("click", function () { fz_click(); });
    $("#fb").bind("click", function () { fb_click(); });
    $("[field]").each(function (i, n) {
        var f = "field_" + $(n).attr("field");
        $(n).addClass(f);
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
                } catch (e) {console.log(e.message) }
            }
        }
        if (document.getElementById("lx").value == "c") {//中间面板不需要设定高宽
            $("[field='southheight']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='northheight']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='eastwidth']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='westwidth']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='td_southheight']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_northheight']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_eastwidth']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_westwidth']", $("#cxtj")).attr("style", "display:none");
        } else if (document.getElementById("lx").value == "t") {
            $("[field='southheight']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='eastwidth']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='westwidth']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='td_southheight']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_eastwidth']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_westwidth']", $("#cxtj")).attr("style", "display:none");
        } else if (document.getElementById("lx").value == "b") {

            $("[field='northheight']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='eastwidth']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='westwidth']", $("#cxtj")).parent().attr("style", "display:none");

            $("[field='td_northheight']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_eastwidth']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_westwidth']", $("#cxtj")).attr("style", "display:none");
        } else if (document.getElementById("lx").value == "l") {
            $("[field='southheight']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='northheight']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='eastwidth']", $("#cxtj")).parent().attr("style", "display:none");

            $("[field='td_southheight']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_northheight']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_eastwidth']", $("#cxtj")).attr("style", "display:none");
        } else if (document.getElementById("lx").value == "r") {
            $("[field='southheight']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='northheight']", $("#cxtj")).parent().attr("style", "display:none");

            $("[field='westwidth']", $("#cxtj")).parent().attr("style", "display:none");
            $("[field='td_southheight']", $("#cxtj")).attr("style", "display:none");
            $("[field='td_northheight']", $("#cxtj")).attr("style", "display:none");

            $("[field='td_westwidth']", $("#cxtj")).attr("style", "display:none");
        }
    }
    //console.log("当js加载成功后会执行的函数");

}, function () {
    //console.log("当js加载失败后会执行的函数");
});