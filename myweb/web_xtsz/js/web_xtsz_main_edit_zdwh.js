﻿require.config({
    paths: {
        "jquery": "../../javascripts/jquery/1.12.4/jquery.min",
        "utils": "../../javascripts/utilsA",
        "myweb": "../../javascripts/myjs/mywebA",
        "xtsz": "../../javascripts/xtsz/xtsz"
    }
})
require(["jquery", "utils", "myweb", "xtsz"], function ($, utils, myweb, xtsz) {

   
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
    //取页面上的值
    var sr = function (s, i) {
        return $.trim(xtsz.myVale(s, i).val().replace(/'/g, "''"));
    }
    //保存
    var ok_click = function () {
        $('#ok').attr('disabled', 'disabled');
        var wid = document.getElementById("wid").value;
        var str = "";
        var ywname, zwname, ord, width, id, visible, readonly, type, sx, bz, showzero, event, btnvalue, showmrrq, hj, hbltname, px, prtname
        var data = {};
        data.row = new Array();
        for (var i = 0; i < xtsz.getRowNum() ; i++) {
            ywname = sr("ywname", i);
            zwname = sr("zwname", i);
            ord = sr("ord", i); if (ord == "") { ord = "0"; }
            width = sr("width", i); if (width == "") { width = "0"; }
            id = sr("id", i); if (id == "") { id = "0"; }
            visible = sr("visible", i);
            if (visible == "") { visible = "0"; }
            readonly = sr("readonly", i);
            if (readonly == "") { readonly = "0"; }
            type = sr("type", i); if (type == "") { type = "text"; }
            sx = sr("sx", i); if (sx == "") { sx = "0"; }
            bz = sr("bz", i);
            showzero = sr("showzero", i); if (showzero == "") { showzero = "0"; }
            event = sr("event", i);
            btnvalue = sr("btnvalue", i);
            showmrrq = sr("showmrrq", i); if (showmrrq == "") { showmrrq = "0"; }
            hj = sr("hj", i); if (hj == "") { hj = "0"; }
            hbltname = sr("hbltname", i);
            px = sr("px", i); if (px == "") { px = "0"; }
            prtname = sr("prtname", i);
            var dataRow = {};
            if (sr("mark", i) == "") {
                dataRow.mark = 0;
            } else {
                dataRow.mark = sr("mark", i);
            }
            dataRow.ywname = ywname;
            dataRow.zwname = zwname;
            dataRow.ord = ord;
            dataRow.width = width;
            dataRow.visible = visible;
            dataRow.readonly = readonly;
            dataRow.type = type;
            dataRow.sx = sx;
            dataRow.bz = bz;
            dataRow.showzero = showzero;
            dataRow.event = event;
            dataRow.btnvalue = btnvalue;
            dataRow.showmrrq = showmrrq;
            dataRow.hj = hj;
            dataRow.hbltname = hbltname;
            dataRow.px = px;
            dataRow.prtname = prtname;
            dataRow.wid = wid;
            dataRow.id = id;
            data.row.push(dataRow);

        }

        if (data.row.length == 0) {
            utils.sAlert('没有可更新的记录!', true, function () {
                $('#ok').removeAttr("disabled")
            });
        } else {
            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/UpSYJZdwh',
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
                            $('#ok').removeAttr("disabled");
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
        if (!isNaN(oldwid) && oldwid != "0" && oldwid != "") {
            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/websj_fz_zd',
                data: { wid: oldwid, newwid: newwid, bs: 'zd' },
                error: function (e) {
                    utils.sAlert('连接失败!');
                },
                success: function (data) {
                    var r = utils.myAjaxData(data);
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

    $("#ok").bind("click", function () { ok_click(); });
    $("#fz").bind("click", function () { fz_click(); });
    $("#fb").bind("click", function () { fb_click(); });
   
  
    $(":text", ".tbbody").focus(function (e) {
        myselect(e.currentTarget);
    });
    $("[field]").each(function (i, n) {
        var f = "field_" + $(n).attr("field");
        $(n).addClass(f);
    });
    $("[field='ord']").bind('contextmenu', function (e) {
        var r = $(e.currentTarget).parent().parent().attr("rownum");
        var v = $(e.currentTarget).val();
        for (var i = r; i < xtsz.getRowNum() ; i++) {
            $("[field='ord']", $("[rownum=" + i + "]", $("#zdwhtb"))).attr("value", Number(v) + (Number(i) - Number(r)))
            $("[field='mark']", $("[rownum=" + i + "]", $("#zdwhtb"))).attr("value", 1)
        }
        return false;
    });
    xtsz.init();
    //console.log("当js加载成功后会执行的函数");

}, function () {
    //console.log("当js加载失败后会执行的函数");
});