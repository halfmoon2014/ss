define(["jquery", "utils",  "swalProcess"], function ($, utils, swalProcess) {
    var start = function () {
        $("#ok").bind("click", function () {
            var reg = new RegExp("\r\n", "g");
            var help = document.getElementById("helpText").value;
            var myid = document.getElementById("myid").value;
            $('#ok').attr('disabled', 'disabled');
            $.ajax({
                type: 'post',
                url: '../webuser/ws.asmx/HelpUp',
                data: { value1: help, value2: myid },
                error: function (e) {
                    swalProcess.sAlert('连接失败',  function () {
                        $('#ok').removeAttr("disabled")
                    });
                },
                success: function (data) {
                    var r = utils.myAjaxData(data);
                    if (r.r == 'true') {
                        swalProcess.sAlert('保存成功!',  function () {
                            $('#ok').removeAttr("disabled")
                        });

                    } else {
                        swalProcess.sAlert('保存失败!',  function () {
                            $('#ok').removeAttr("disabled")
                        });
                    }
                }
            })
        });
        $("#esc").bind("click", function () { window.close(); });
        //使用了bootstrap样式,需要增加12的高度
        utils.autoTextarea(document.getElementById("helpText"), 12);       
    }
    return {
        start: start
    }
});
