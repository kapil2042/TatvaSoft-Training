function showPass(idpass) {
    document.getElementById(idpass).setAttribute("type", "text");
}
function hidePass(idpass) {
    document.getElementById(idpass).setAttribute("type", "password");
}
function inRange(x, min, max) {
    return ((x - min) * (x - max) <= 0);
}
function checkName(chkname) {
    var str = document.getElementById(chkname).value;
    var str1 = "";
    for (var i = 0; i < str.length; i++) {
        a = str.charCodeAt(i);
        if (inRange(a, 65, 122)) {
            if (!inRange(a, 91, 96)) {
                str1 += String.fromCharCode(a);
            }
        }
        if (a == 32) {
            if (str.charCodeAt(i - 1) != 32 && i != 0)
                str1 += String.fromCharCode(a);
        }
        if (a == 46) {
            if (str.charCodeAt(i - 1) != 46 && str.charCodeAt(i - 1) != 32 && i != 0)
                str1 += String.fromCharCode(a);
        }
    }
    document.getElementById(chkname).value = str1;
}


document.querySelector(".phone").addEventListener("keypress", function (evt) {
    if (evt.which != 8 && evt.which != 0 && evt.which != 13 && evt.which == 109 && evt.which != 16 && evt.which < 48 || evt.which > 57) {
        evt.preventDefault();
    }
});