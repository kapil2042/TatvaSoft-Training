function showPass(idpass) {
    document.getElementById(idpass).setAttribute("type", "text");
}
function hidePass(idpass) {
    document.getElementById(idpass).setAttribute("type", "password");
}
// function checkName(chkname){
//     var str = chkname.value;
//     for (var i = 0; i < str.length; i++) {
//         if(!((str.charCodeAt(i))>=65 && (str.charCodeAt(i))<=90) && ((str.charCodeAt(i))>=97 && (str.charCodeAt(i))<=122)){
//             console.log("yes");
//         }
//     }
// }
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