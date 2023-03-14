var cbs = document.querySelectorAll('.otherthencity');
for (var i = 0; i < cbs.length; i++) {
    cbs[i].addEventListener('change', function () {
        if (this.checked) {
            $("input[type=checkbox][value='" + this.value + "']").prop('checked', true);
            addElement(this, this.value);
            myfilter(fpg = 1, fid = 0);
        }
        else {
            $("input[type=checkbox][value='" + this.value + "']").prop('checked', false);
            removeElement(this.value);
            myfilter(fpg = 1, fid = 0);
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
    let cross = '&times;'


    crossButton.addEventListener('click', function () {
        let elementToBeRemoved = document.getElementById(value);

        $("input[type=checkbox][value='" + value + "']").prop('checked', false);

        elementToBeRemoved.remove();
        myfilter(fpg = 1, fid = 0);
    })

    crossButton.innerHTML = cross;


    createdTag.appendChild(crossButton);
    filtersSection.appendChild(createdTag);
    myfilter(fpg = 1, fid = 0);
}

function ClearAllElement() {

    var filtersSection = document.querySelector("#filterlist");
    var filtersSectioncity = document.querySelector("#filterlistcity");
    filtersSectioncity.innerHTML = "";
    filtersSection.innerHTML = "";

    $(".citycheck").prop('checked', false);
    $(".otherthencity").prop('checked', false);
    myfilter(fpg = 1, fid = 0);
}


function removeElement(value) {

    let filtersSection = document.querySelector("#filterlist");
    let elementToBeRemoved = document.getElementById(value);
    filtersSection.removeChild(elementToBeRemoved);
    myfilter(fpg = 1, fid = 0);
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
    let cross = '&times;'


    crossButton.addEventListener('click', function () {
        let elementToBeRemoved = document.getElementById(value);

        $("input[type=checkbox][value='" + value + "']").prop('checked', false);

        elementToBeRemoved.remove();
        myfilter(fpg = 1, fid = 0);
    })

    crossButton.innerHTML = cross;


    createdTag.appendChild(crossButton);
    filtersSectioncity.appendChild(createdTag);
    myfilter(fpg = 1, fid = 0);
}



function ClearAllElementcity() {

    var filtersSectioncity = document.querySelector("#filterlistcity");
    filtersSectioncity.innerHTML = "";

    $(".citycheck").prop('checked', false);
    myfilter(fpg = 1, fid = 0);
}


function removeElementcity(value) {

    let filtersSectioncity = document.querySelector("#filterlistcity");
    let elementToBeRemoved = document.getElementById(value);
    filtersSectioncity.removeChild(elementToBeRemoved);
    myfilter(fpg = 1, fid = 0);
}