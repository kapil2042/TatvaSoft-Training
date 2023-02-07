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
    var str1="";
    for (var i = 0; i < str.length; i++) {
        a = str.charCodeAt(i);
        if (inRange(a, 65, 122)) {
            if (!inRange(a, 91, 96)){
                str1 += String.fromCharCode(a);
            }
        }
    }
    document.getElementById(chkname).value =str1;
}
// function validPhone(phone) {
//     var txtPhone = phone.value;
//     if (typeof txtPhone != "string") {
//         var phoneno = /^\+?([0-9]{2})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$/;
//         var mobileno = /^((\\+91-?)|0)?[0-9]{10}$/;
//         if ((txtPhone.match(phoneno)) || (txtPhone.match(mobileno))) {
//             document.getElementById("demo").innerHTML = "Valid";
//         }
//         else {
//             document.getElementById("demo").innerHTML = "Not Valid";
//         }
//     }
//     else{
//         txtPhone = "";
//     }
// }