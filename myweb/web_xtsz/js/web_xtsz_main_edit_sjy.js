﻿require.config({
    paths: {
        "jquery": "../../javascripts/jquery/1.12.4/jquery.min",
        "utils": "../../javascripts/utilsA",
        sweetalert: "../../javascripts/sweetalert/sweetalert.min",
        swalProcessA: "../../javascripts/sweetalert/swalProcessA" 
    },
    shim: {
        'swalProcessA': ['sweetalert']
    }
})
require(["jquery", "utils", "swalProcessA"], function ($, utils, swalProcessA) {

    $("#ok").bind("click", function () { ok_click(); });
    $("#fb").bind("click", function () { fb_click(); });
    //使用了bootstrap样式,需要增加12的高度
    utils.autoTextarea(document.getElementById("fwsql"), 12); 
    utils.autoTextarea(document.getElementById("tbsql"), 12); 
    utils.autoTextarea(document.getElementById("tbsql2"), 12); 
    utils.autoTextarea(document.getElementById("mxhsql"), 12);  
    utils.autoTextarea(document.getElementById("mxsql"), 12); 
    
  
    var ok_click = function () {
        $('#ok').attr('disabled', 'disabled');
        var sql = document.getElementById("tbsql").value;
        var fwsql = document.getElementById("fwsql").value;
        var name = document.getElementById("name").value;
        //var zwname = document.getElementById("zwname").value;
        var mxgl = document.getElementById("mxgl").value;
        var mxsql = document.getElementById("mxsql").value;
        var mxhgl = document.getElementById("mxhgl").value;
        var mxhord = document.getElementById("mxhord").value;
        var mxhsql = document.getElementById("mxhsql").value;
        var sql_2 = document.getElementById("tbsql2").value;

        if (name.length == 0) {
            swalProcessA.sAlert('中文名称一定要输入!',  function () {
                $('#ok').removeAttr("disabled")
            });

        } else {
            var wid = document.getElementById("wid").value;
            var mrcx = (document.getElementById("mrcx").checked ? "1" : "0");
            var myadd = (document.getElementById("myadd").checked ? "1" : "0");
            var orderby = document.getElementById("orderby").value;
            var pagesize = document.getElementById("pagesize").value;
            var mxly = document.getElementById("mxly").value;
            if (pagesize == "") { pagesize = "0"; }

            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/sjy_up',
                data: { wid: wid, value1: name, value3: sql, value4: fwsql, mrcx: mrcx, myadd: myadd, orderby: orderby, pagesize: pagesize, mxgl: mxgl, mxsql: mxsql, mxhgl: mxhgl, mxhord: mxhord, mxhsql: mxhsql, mxly: mxly, sql_2: sql_2 },
                error: function (e) {
                    swalProcessA.sAlert('连接失败!',  function () {
                        $('#ok').removeAttr("disabled")
                    });
                },
                success: function (data) {
                    var r = utils.myAjaxData(data);
                    if (r.r == 'true') {
                        swalProcessA.sAlert('保存成功!', "success", function () {
                            $('#ok').removeAttr("disabled")
                        });
                    } else {
                        swalProcessA.sAlert('保存失败!',  function () {
                            $('#ok').removeAttr("disabled")
                        });
                    }
                }
            })
        }

    }

    //放大
    var fd = function (obj) {
        return false;
        if (typeof ($(obj).attr("oldw")) == "undefined") {
            //没有属性,增加一个
            $(obj).attr("oldw", $(obj).css("width"));
        }
        if (typeof ($(obj).attr("oldh")) == "undefined") {
            //没有属性,增加一个
            $(obj).attr("oldh", $(obj).css("height"));
        }
        if ($(obj).css("width") == $(obj).attr("oldw")) {
            $(obj).css("height", "600");
            $(obj).css("width", "90%");
        } else {
            $(obj).css("height", $(obj).attr("oldh"));
            $(obj).css("width", $(obj).attr("oldw"));
        }
    }
    //console.log("当js加载成功后会执行的函数");

}, function () {
    //console.log("当js加载失败后会执行的函数");
});