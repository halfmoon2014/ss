(function () {
    //解决弹出2个窗口后tab失效的问题
    var close = window.swal.close;
    window.swal.close = function () {
        close();
        window.onkeydown = null;
    };
})();
//sweetalert封装
var sConfirm = function (title, callBack, closeOnConfirm, closeOnCancel, confirmButtonText, cancelButtonText) {   
    if (undefined==closeOnConfirm) { closeOnConfirm = true; }
    if (undefined==closeOnCancel) { closeOnCancel = true; }
    if (undefined==confirmButtonText ) { confirmButtonText = "确定"; }
    if (undefined==cancelButtonText ) { cancelButtonText = "取消"; }
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
        if (undefined!=callBack) {
            callBack(isConfirm);
        }
    });
}


var sAlert = function (title,closeOnConfirm,fn) {
    if (undefined==closeOnConfirm) { closeOnConfirm = true; }
    swal({
        title: title,
        text: "",
        closeOnConfirm:closeOnConfirm,
        type: "info",
    }, fn);
}
