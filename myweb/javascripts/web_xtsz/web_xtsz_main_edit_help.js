define(['jquery', 'myweb', 'utils', 'swalProcess'], function ($, myweb, utils, swalProcess) {

    var start = function () {
        $(function () {
            $("#ok").bind("click", function () {                
                $('#ok').attr('disabled', 'disabled');
                var tbhelp = myweb.mySysDate(document.getElementById("tbhelp").value);
                var wid = myweb.mySysDate(document.getElementById("wid").value);

                $.ajax({
                    type: 'post',
                    url: '../webuser/ws.asmx/sjy_uphelp',
                    data: { wid: wid, help: tbhelp },
                    error: function (e) {                       
                        swalProcess.sAlert('连接失败!', function () {
                            $('#ok').removeAttr("disabled")
                        });
                    },
                    success: function (data) {
                        var r = utils.myAjaxData(data);
                        if (r.r == 'true') {                            
                            swalProcess.sAlert('保存成功!', function () {
                                $('#ok').removeAttr("disabled")
                            });
                        } else {                            
                            swalProcess.sAlert('保存失败!', function () {
                                $('#ok').removeAttr("disabled")
                            });
                        }
                    }
                })
            });
            utils.hideLoading();
        });
    }

    return {
        start: start
    }
});