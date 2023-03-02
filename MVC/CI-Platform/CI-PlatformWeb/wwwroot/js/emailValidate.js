function emailValidate(email) {
    var e = document.getElementById(email).value;
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (regex.test(e)) {
        console.log("dfghj");
        var btn = document.getElementsByTagName('input[type = submit]');
        btn.disabled = true;
    }
    else {
        var btn = document.getElementsByTagName('input[type = submit]');
        btn.disabled = false;
        console.log("mk");
    }
}