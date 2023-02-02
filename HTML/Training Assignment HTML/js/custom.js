$(document).ready(function () {
  $("#showmenu").click(function () {
    $(".menutext").toggleClass("hide");
    $("aside").toggleClass("asidewidth",1000);
    $("section").toggleClass("sectionwidth");
    $(".mobile-menu").toggleClass("d-block");
  });
});

$(function () {
  $("#tabs").tabs();
});

$("#tb1").click(function () {
  $(this).addClass("text-danger border-danger border-bottom border-3");
  $("#tb2").removeClass("text-danger border-danger border-bottom border-3");
  $("#tb3").removeClass("text-danger border-danger border-bottom border-3");
});
$("#tb2").click(function () {
  $(this).addClass("text-danger border-danger border-bottom border-3");
  $("#tb1").removeClass("text-danger border-danger border-bottom border-3");
  $("#tb3").removeClass("text-danger border-danger border-bottom border-3");
});
$("#tb3").click(function () {
  $(this).addClass("text-danger border-danger border-bottom border-3");
  $("#tb2").removeClass("text-danger border-danger border-bottom border-3");
  $("#tb1").removeClass("text-danger border-danger border-bottom border-3");
});

let fileInput = document.getElementById("file-input");
let fileList = document.getElementById("file-list");

fileInput.addEventListener("change", () => {
  fileList.textContent = `${fileInput.files.length} Files Selected`;
});