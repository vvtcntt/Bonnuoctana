// JavaScript Document
 $("document").ready(function($){
    var nav = $('#SideBar');

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
			$("#SideBar").css("display", "block");
            nav.addClass("f-nav");
        } else {
			$("#SideBar").css("display", "none");
            nav.removeClass("f-nav");
        }
    });
});