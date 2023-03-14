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
                                            <input class="form-check-input citycheck" name="city" type="checkbox" value="${item.name}" id="Checkme + ${item.cityId}" onchange="myfilter(fpg = 1, fid = 0)"/>
                                            <label class="form-check-label" for="Checkme + ${item.cityId}">${item.name}</label>
                                        </div>
                                    </a>
                                </li>`;
                cityResults2.innerHTML += `<li>
                                    <a class="dropdown-item" href="#">
                                        <div class="form-check">
                                            <input class="form-check-input citycheck" name="city" type="checkbox" value="${item.name}" id="Checkme + ${item.cityId}" onchange="myfilter(fpg = 1, fid = 0)"/>
                                            <label class="form-check-label" for="Checkme + ${item.cityId}">${item.name}</label>
                                        </div>
                                    </a>
                                </li>`;
            });
            var cbs = document.querySelectorAll('.citycheck');
            ClearAllElementcity();
            document.querySelector("#filterlistcity").innerHTML = `<span class="fs-7 border px-2 me-2 mb-2 rounded-pill text-secondary"> ${$("#" + country + "").text()} </span>`;

            for (var i = 0; i < cbs.length; i++) {
                cbs[i].addEventListener('change', function () {
                    if (this.checked) {
                        $("input[type=checkbox][value='" + this.value + "']").prop('checked', true);
                        addElementcity(this, this.value);
                        myfilter(fpg = 1, fid = 0, c = $("#" + country + "").text());
                    }
                    else {
                        $("input[type=checkbox][value='" + this.value + "']").prop('checked', false);
                        removeElementcity(this.value);
                        myfilter(fpg = 1, fid = 0, c = $("#" + country + "").text());
                    }
                });
            }

        }
        myfilter(fpg = 1, fid = 0, c = $("#" + country + "").text());
    };
    xhr.open('GET', 'Volunteer/Home/GetCityByCountry?country=' + country, true);
    xhr.send();
}