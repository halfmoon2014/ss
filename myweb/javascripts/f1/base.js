$(function () {
    //文本框Style
    $(".txt").mouseover(function () {
        $(this).addClass("txt_o");
    }).mouseout(function () {
        $(this).removeClass("txt_o");
    }).focus(function () {
        $(this).addClass("txt_s");
    }).blur(function () {
        $(this).removeClass("txt_s");
    });    
    
});

