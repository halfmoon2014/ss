define(["jquery", "utils"], function ($, utils) {
    var start = function () {
        $(document).ready(function () {
            utils.hideLoading();
            $('.tzlist').delegate('a', 'click', function (e) {
                var tzid, menu;
                if ($(e.target).is("a")) {
                    tzid = $(e.target).attr("t");
                    menu = $(e.target).attr("m");
                } else {
                    tzid = $(e.target).parent().attr("t");
                    menu = $(e.target).parent().attr("m");
                }
                utils.showLoading();
                $.ajax({
                    type: 'post',
                    url: 'webuser/WebService.asmx/ChooseTz',
                    data: { tzid: tzid, updata: $("#updata").is(':checked') },
                    error: function () {
                        utils.hideLoading();
                    },
                    success: function (data) {
                        r = utils.myAjaxData(data);
                        if (r.r == "true") {
                            //document.forms[0].action = "webpage/" + menu + ".aspx"
                            //document.forms[0].submit();                    
                            window.location.href = "webpage/" + menu + ".aspx";
                        }
                    }
                })                
            });
        });
    }
    return {
        start:start
    }
});

