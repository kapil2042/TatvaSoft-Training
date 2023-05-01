var fpg = 1;
var fid = 0;
function citybycountry(country) {
    var cityResults1 = document.getElementById('citieslist1');
    var cityResults2 = document.getElementById('citieslist2');
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var results = JSON.parse(this.responseText);
            cityResults1.innerHTML = "";
            cityResults2.innerHTML = "";
            // Process and display search results
            $.each(results, function (index, item) {
                cityResults1.innerHTML += `<li>
                                    <a class="dropdown-item" href="#">
                                        <div class="form-check">
                                            <label class="form-check-label">
                                                <input class="form-check-input citycheck" name="city" type="checkbox" value="${item.name}" id="Checkme + ${item.cityId}"/>
                                                ${item.name}
                                            </label>
                                        </div>
                                    </a>
                                </li>`;
                cityResults2.innerHTML += `<li>
                                    <a class="dropdown-item" href="#">
                                        <div class="form-check">
                                            <label class="form-check-label">
                                                <input class="form-check-input citycheck" name="city" type="checkbox" value="${item.name}" id="Checkme + ${item.cityId}"/>
                                                ${item.name}
                                            </label>
                                        </div>
                                    </a>
                                </li>`;
            });
            var cbs = document.querySelectorAll('.citycheck');
            ClearAllElementcity();
            document.querySelector("#filterlistcity").innerHTML = `<span class="fs-7 border px-2 me-2 mb-2 rounded-pill text-secondary"> ${$("#" + country + "").val()} </span>`;
            document.getElementById("clearbtn").classList.remove("d-none");
            for (var i = 0; i < cbs.length; i++) {
                cbs[i].addEventListener('change', function () {
                    if (this.checked) {
                        $("input[type=checkbox][value='" + this.value + "']").prop('checked', true);
                        addElementcity(this, this.value);
                        myfilter(fpg = fpg, fid = fid);
                    }
                    else {
                        $("input[type=checkbox][value='" + this.value + "']").prop('checked', false);
                        removeElementcity(this.value);
                        myfilter(fpg = fpg, fid = fid);
                    }
                });
            }
            myfilter(fpg = fpg, fid = fid);
        }
    };
    xhr.open('GET', '/Volunteer/Home/GetCityByCountry?country=' + country, true);
    xhr.send();
}


var cbs = document.querySelectorAll('.otherthencity');
for (var i = 0; i < cbs.length; i++) {
    cbs[i].addEventListener('change', function () {
        if (this.checked) {
            $("input[type=checkbox][value='" + this.value + "']").prop('checked', true);
            addElement(this, this.value);
            myfilter(fpg = fpg, fid = fid);
        }
        else {
            $("input[type=checkbox][value='" + this.value + "']").prop('checked', false);
            removeElement(this.value);
            myfilter(fpg = fpg, fid = fid);
        }
    });
}


function addElement(current, value) {
    let filtersSection = document.querySelector("#filterlist");

    let createdTag = document.createElement('span');
    createdTag.classList.add('filter-list');
    createdTag.classList.add('fs-7');
    createdTag.classList.add('border');
    createdTag.classList.add('px-2');
    createdTag.classList.add('me-2');
    createdTag.classList.add('mb-2');
    createdTag.classList.add('rounded-pill');
    createdTag.classList.add('text-secondary');

    createdTag.innerHTML = value;

    createdTag.setAttribute('id', value);
    let crossButton = document.createElement('a');
    crossButton.classList.add("filter-close-button");
    crossButton.style.textDecoration = 'none';
    crossButton.classList.add('btn-light');
    crossButton.classList.add('ms-1');
    crossButton.classList.add('fs-7');
    let cross = '&times;'


    crossButton.addEventListener('click', function () {
        let elementToBeRemoved = document.getElementById(value);

        $("input[type=checkbox][value='" + value + "']").prop('checked', false);

        elementToBeRemoved.remove();

        if (document.getElementById("filterlist").textContent.trim() === '') {
            document.getElementById("clearbtn").classList.add("d-none");
        }
        myfilter(fpg = fpg, fid = fid);
    })

    crossButton.innerHTML = cross;


    createdTag.appendChild(crossButton);
    filtersSection.appendChild(createdTag);
    document.getElementById("clearbtn").classList.remove("d-none");
}

function ClearAllElement() {

    var filtersSection = document.querySelector("#filterlist");
    var filtersSectioncity = document.querySelector("#filterlistcity");
    filtersSectioncity.innerHTML = "";
    filtersSection.innerHTML = "";

    $(".citycheck").prop('checked', false);
    $(".otherthencity").prop('checked', false);
    $(".country").prop('checked', false);
    document.getElementById('citieslist1').innerHTML = "";
    document.getElementById('citieslist2').innerHTML = "";
    document.getElementById("clearbtn").classList.add("d-none");
    myfilter(fpg = fpg, fid = fid);
}


function removeElement(value) {
    let filtersSection = document.querySelector("#filterlist");
    let elementToBeRemoved = document.getElementById(value);
    filtersSection.removeChild(elementToBeRemoved);
}





function addElementcity(current, value) {
    let filtersSectioncity = document.querySelector("#filterlistcity");
    let createdTag = document.createElement('span');
    createdTag.classList.add('filter-list');
    createdTag.classList.add('fs-7');
    createdTag.classList.add('border');
    createdTag.classList.add('px-2');
    createdTag.classList.add('me-2');
    createdTag.classList.add('mb-2');
    createdTag.classList.add('rounded-pill');
    createdTag.classList.add('text-secondary');

    createdTag.innerHTML = value;

    createdTag.setAttribute('id', value);
    let crossButton = document.createElement('a');
    crossButton.classList.add("filter-close-button");
    crossButton.style.textDecoration = 'none';
    crossButton.classList.add('btn-light');
    crossButton.classList.add('ms-1');
    crossButton.classList.add('fs-7');
    let cross = '&times;'


    crossButton.addEventListener('click', function () {
        let elementToBeRemoved = document.getElementById(value);

        $("input[type=checkbox][value='" + value + "']").prop('checked', false);

        elementToBeRemoved.remove();
        myfilter(fpg = fpg, fid = fid);
    })

    crossButton.innerHTML = cross;


    createdTag.appendChild(crossButton);
    filtersSectioncity.appendChild(createdTag);
}



function ClearAllElementcity() {

    var filtersSectioncity = document.querySelector("#filterlistcity");
    filtersSectioncity.innerHTML = "";

    $(".citycheck").prop('checked', false);
    myfilter(fpg = fpg, fid = fid);
}


function removeElementcity(value) {

    let filtersSectioncity = document.querySelector("#filterlistcity");
    let elementToBeRemoved = document.getElementById(value);
    filtersSectioncity.removeChild(elementToBeRemoved);
}

function favunfavmission(mid) {
    $.ajax({
        type: 'POST',
        url: "/Volunteer/Mission/Favourite_Mission",
        data: { 'missoinid': mid },
        success: function (res) {
            myfilter(fpg = fpg, fid = fid);
        },
        error: function (data) {
            toastr.error("Please Login First", "Error Message", { timeOut: 5000, "positionClass": "toast-top-right", "closeButton": true, "progressBar": true });
        }
    });
}