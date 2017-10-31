$(function () {    
    $("[mylink='1']").bind("click", function (e) { gotz($(e.target).attr("t"), $(e.target).attr("m")); })
});

function gotz(tzid,menu) {    
    document.getElementById("tzid").value = tzid;
    document.getElementById("menu").value = menu;

    $.ajax({ type: 'post',
        url: 'webuser/WebService.asmx/ChooseTz',
        data: { tzid: mySysDate(tzid) },
        error: function () {
        },
        success: function (data) {
            r = myAjaxData(data);
            if (r.r == "true") {                
                window.location.href = "webpage/" + menu + ".aspx";
            } 
        }
    })

    /*$("#__VIEWSTATE").attr("disabled", true);
    F.action = "webpage/"+menu+".aspx";
    F.submit();*/
}