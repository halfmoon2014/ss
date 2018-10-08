define(['jquery'], function ($) {
    var init = function () {
        //绑定事件
        $.each($("td", $(".tbbody")), function (i, n) {
            //每个n=td
            $.each($(n).children(), function (i1, n1) {
                //得到每个TD里面的每个元素,有可能是text 有可能是check or select 
                //不要传递行号! 因为新增的时候行号不会变
                $(n1).bind({
                    "change": function () { mysyschange($(this)); },
                    //"keypress": function (event) { mysyskeypress(event, $(this)); },
                    "keydown": function (event) { mysyskeypress(event, $(this)); }
                });
            });
        });
    }
    
    //如果有修改,打标识为1
    var mysyschange = function (obj, rownum) {
        var rownum = $(obj).parent().parent().attr("rownum");
        //td所在所中,,, 自定义属性field=mark的
        $($("[field='mark']", $(obj).parent().parent()))[0].value = "1";
    }
    
    //键盘事件
    //当表格可保存时,向下新增一行
    //下移一行
    var mysyskeypress = function (e, obj) {

        var rownum = Number($(obj).parent().parent().attr("rownum"));
        var keyn = e.keyCode;

        if (keyn == 40) {//向下箭头
            if (getRowNum() == rownum + 1) {//最后一行就是新增
                var cc = $(obj).parent().parent().clone(true);
                $(cc).attr("rownum", (rownum + 1));

                $.each($(cc).children(), function (i, n) {//td

                    //var mykey = $(n).attr("id").replace(qkey, hkey)
                    $.each($(n).children(), function (i1, n1) {
                        //td中的每个元素 
                        if ($(n1).attr("value") != undefined) {
                            if ($(n1).attr("type") == undefined || $(n1).attr("type") != "button") {
                                $(n1).attr("value", "");
                            }
                        }
                        if ($(n1).attr("type") == "checkbox") {
                            $(n1).removeAttr("checked");
                        }
                        if ($(n1).is("select")) {
                            //如果是下拉框就默认吧
                            $(n1).attr("value", $(n1).attr("mrz"));
                            //$(n1).removeAttr("selected");
                            /*$.each(cc.find("[selected]"), function (i, n) {
                            $(n).removeAttr("selected");
                            });*/
                        }
                    });

                });


                cc.appendTo($(obj).parent().parent().parent());
                $("[field='" + $(obj).attr("field") + "']", $(cc))[0].focus();


            } else {//非最后一行

                $("[field='" + $(obj).attr("field") + "']", $(obj).parent().parent().next("tr")).focus();
            }

        } else if (keyn == 38) {
            //向上键头
            if (rownum > 0) {//如果不是第一行

                $("[field='" + $(obj).attr("field") + "']", $("[rownum=" + ($(obj).parent().parent().attr("rownum") - 1) + "]")).focus();
            }
        }
    }

    //得到内容的总行数
    var getRowNum = function () {
        return $(".tbbody").length;
    }
    //取页面元素的值
    //frame iframe 对像
    var myVale = function (field, i, frame) {
        if (i == null || i == undefined) {
            //取查询条件上的值
            if (frame == null || frame == undefined) {
                return $("[field='" + field + "']", $("#cxtj"));
            } else {
                return $("[field='" + field + "']", $("#cxtj", frame));
            }
        } else {
            var selector = "[field='" + field + "']";
            if (frame == null || frame == undefined) {
                return $(selector, $(".tbbody[rownum='" + i + "']"));
            } else {
                return $(selector, $(".tbbody[rownum='" + i + "']", frame));
            }
        }
    }
    return {
        init: init,
        mysyschange: mysyschange,
        mysyskeypress: mysyskeypress,
        getRowNum: getRowNum,
        myVale: myVale
    }
});