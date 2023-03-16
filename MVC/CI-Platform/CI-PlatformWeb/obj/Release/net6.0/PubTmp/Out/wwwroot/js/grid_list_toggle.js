$("#grid").click(function () {
    $(".listajax").addClass("d-none");
    $(".gridajax").removeClass("d-none");
    $("#list").removeClass('grid-active');
    $("#grid").addClass('grid-active');
});
$("#list").click(function () {
    $(".listajax").removeClass("d-none");
    $(".gridajax").addClass("d-none");
    $("#list").addClass('grid-active');
    $("#grid").removeClass('grid-active');
});