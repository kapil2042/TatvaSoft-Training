let files = [];
let dragArea = document.querySelector('.drag-area');
let input = document.querySelector('.drag-area input');
let container = document.querySelector('.img-container');


input.addEventListener('change', () => {
    let file = input.files;

    if (file.length == 0) return;

    for (let i = 0; i < file.length; i++) {
        if (file[i].type.split("/")[0] != 'image') continue;
        if (!files.some(e => e.name == file[i].name)) files.push(file[i])
    }

    showImages();
});


function showImages() {
    container.innerHTML = files.reduce((prev, curr, index) => {
        return `${prev}
		    <div class="image">
			    <span onclick="delImage(${index})">&times;</span>
			    <img src="${URL.createObjectURL(curr)}" />
			</div>`
    }, '');
    console.log(files);
}


function delImage(index) {
    files.splice(index, 1);
    showImages();
}


dragArea.addEventListener('dragover', e => {
    e.preventDefault()
    dragArea.classList.add('dragover')
})


dragArea.addEventListener('dragleave', e => {
    e.preventDefault()
    dragArea.classList.remove('dragover')
});


dragArea.addEventListener('drop', e => {
    e.preventDefault()
    dragArea.classList.remove('dragover');

    let file = e.dataTransfer.files;
    for (let i = 0; i < file.length; i++) {

        if (file[i].type.split("/")[0] != 'image') continue;

        if (!files.some(e => e.name == file[i].name)) files.push(file[i])
    }
    showImages();
});
