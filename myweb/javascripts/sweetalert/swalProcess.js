//(function () {
//    //解决弹出2个窗口后tab失效的问题
//    var close = window.swal.close;
//    window.swal.close = function () {
//        close();
//        window.onkeydown = null;
//    };
//})();

//sweetalert封装
var sConfirmTitleAndCallBack = function (title, callBack) {    
    sConfirmAll(title, callBack, "确 定", "取 消", "warning")
}

var sConfirmOp = function (op) {
    var settings = $.extend({
        title: "",
        callBack: undefined,
        confirmButtonText: "确 定",
        cancelButtonText: '取 消',
        icon: 'warning'
    }, op);

    sConfirmAll(settings.title, settings.callBack, settings.confirmButtonText, settings.cancelButtonText, settings.icon)
}

var sConfirmAll = function (title, callBack, confirmButtonText, cancelButtonText, icon) {
    swal({
        title: title,
        text: "",
        icon: icon,
        buttons: {
            myCancel: {
                text: cancelButtonText,
                value: false
            },
            confirm: confirmButtonText
        },
        dangerMode: true,
        closeOnClickOutside: false,
        closeOnEsc: false,
    })
    .then((willDelete) => {
        if (undefined != callBack) {
            callBack(willDelete);
        }
    });
}

var sConfirm = function () {
    var a = arguments;
    if (a.length == 1) {//如果只有一个参数         
        sConfirmOp(a[0]);         
    } else if (a.length == 2) {
        sConfirmTitleAndCallBack(a[0], a[1]);
    } 
}

var sAlertTitle = function (title) {
    sAlertTitleAndCallBack(title, undefined);
}

var sAlertTitleAndCallBack = function (title, callBack) {
    sAlertAll(title, callBack, "warning", "确 定");
}

var sAlertTitleAndCallBackAndIcon = function (title, callBack, icon) {
    sAlertAll(title, callBack, icon, "确 定");
}

var sAlertOp = function (op) {
    var settings = $.extend({
        title: "",
        callBack: undefined,
        confirm: "确 定",        
        icon: 'warning'
    }, op);

    sAlertAll(settings.title, settings.callBack, settings.icon, settings.confirm);
}

var sAlertAll = function (title, callBack, icon, confirm) {
    //warning error success info    
    swal({
        title: title,
        text: "",
        icon: icon,
        buttons: {
            confirm: confirm
        },
        closeOnClickOutside: false,
        closeOnEsc: false,
    })
    .then((willDelete) => {
        if (undefined != callBack) {
            callBack();
        }
    });
}

var sAlert = function () {
    var a = arguments;
    if (a.length == 1) {//如果只有一个参数和,那么有可能是 sAlertTitle or sAlertOp
        if (typeof (a[0]) == "string") {
            sAlertTitle(a[0]);
        } else if (typeof (a[0]) == "object") {
            sAlertOp(a[0]);
        }
    } else if (a.length==2) {
        sAlertTitleAndCallBack(a[0], a[1]);
    } else if (a.length == 3) {
        sAlertTitleAndCallBackAndIcon(a[0], a[2], a[1]);
    }
}

var sAlertbak = function (title, closeOnConfirm, fn) {
    if (undefined == closeOnConfirm) { closeOnConfirm = true; }
    swal({
        title: title,
        text: "",
        closeOnConfirm: closeOnConfirm,
        type: "info",
    }, fn);
}
var sConfirmBAK = function (title, callBack, closeOnConfirm, closeOnCancel, confirmButtonText, cancelButtonText) {
    if (undefined == closeOnConfirm) { closeOnConfirm = true; }
    if (undefined == closeOnCancel) { closeOnCancel = true; }
    if (undefined == confirmButtonText) { confirmButtonText = "确定"; }
    if (undefined == cancelButtonText) { cancelButtonText = "取消"; }
    swal({
        title: title,
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: confirmButtonText,
        cancelButtonText: cancelButtonText,
        closeOnConfirm: closeOnConfirm,
        closeOnCancel: closeOnCancel
    },
    function (isConfirm) {
        if (undefined != callBack) {
            callBack(isConfirm);
        }
    });
}

//sweetalert封装 end 