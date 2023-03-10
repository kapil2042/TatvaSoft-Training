function searchbar() {
    var searchInput = document.getElementById('searchInput');
    var searchResults = document.getElementById('searchResults');
    var query = searchInput.value;
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var results = JSON.parse(this.responseText);
            searchResults.innerHTML = "";
            // Process and display search results
            $.each(results, function (index, item) {
                searchResults.innerHTML += `<h5 class="card-title">${item.title}</h5>`;
            });
        }
    };
    xhr.open('GET', 'Volunteer/Home/Search?q=' + query, true);
    xhr.send();
}

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
                cityResults1.innerHTML += `<li><a class="dropdown-item" id="${item.name}" onclick="addccst(this.id)">${item.name}</a></li>`;
                cityResults2.innerHTML += `<li><a class="dropdown-item" id="${item.name}" onclick="addccst(this.id)">${item.name}</a></li>`;
            });
        }
    };
    xhr.open('GET', 'Volunteer/Home/GetCityByCountry?country=' + country, true);
    xhr.send();
}