//jquery easyui 日期格式化
define([], function () {
    var formatDate=function(date) {
        var month = date.getMonth() + 1;
        if ("" != date) {
            if (date.getMonth() + 1 < 10) {
                month = '0' + (date.getMonth() + 1);
            }
            var day = date.getDate();
            if (date.getDate() < 10) {
                day = '0' + date.getDate();
            }
            return date.getFullYear() + '-' + month + '-' + day; //将2011-01-01   这种格式转换为2011-01-01 
        } else {
            return "";
        }
    }
    return{
        formatDate: formatDate
    }
});