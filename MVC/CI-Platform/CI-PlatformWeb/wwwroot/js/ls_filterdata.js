var a = document.getElementById("filterlist");
function addccst(ccst) {
    localStorage.removeItem("i18nextLng");
    localStorage.setItem('ccst' + ccst, ccst);
    allStorage();
}

function removeccst(ccst) {
    localStorage.removeItem('ccst' + ccst);
    allStorage();
}


function allStorage() {
    a.innerHTML = "";
    for (var i = 0; i < localStorage.length; i++) {
        var item = localStorage.getItem(localStorage.key(i));
        a.innerHTML += '<span class="fs-7 border px-2 me-2 mb-2 rounded-pill text-secondary d-inline-flex flex-nowrap align-items-center">' + item + '<a id="' + item + '" onclick="removeccst(this.id)"><img src="../images/cancel.png" alt="" class="ms-2"></a></span >';
    }
    if (localStorage.length!=0)
        a.innerHTML += '<span class="d-flex align-items-center h-100 mb-2"><a onclick="clearLocal()" class="text-secondary ms-2 fs-7">Clear all</a></span>';
}

function clearLocal() {
    localStorage.clear();
    allStorage();
}