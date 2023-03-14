$(window).resize(function () {
    if($(window).width() < 871){
        $(".listajax").addClass("d-none");
        $(".gridajax").removeClass("d-none");
        $("#list").removeClass('grid-active');
        $("#grid").addClass('grid-active');
    }
});