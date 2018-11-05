define(["jquery", "utils"], function ($, utils) {
    var start = function () {
        $(document).ready(function () {
            $('.list-group-item').delegate('a', 'click', function (e) {
                if ($(e.target).is("a")) {
                    var cmd = $(e.target).parent().attr("cmd");
                    parent.addTab($(e.target).html(), cmd, $(e.target).parent())
                }
            });
            $('.list-group-item').delegate('span', 'click', function (e) {
                if ($(e.target).is("span")) {
                    var menuID = $(e.target).parent().attr("menuID");
                    parent.myhelp(menuID)
                }
            });
            utils.hideLoading();
        });
    }
    return {
        start:start
    }
});

