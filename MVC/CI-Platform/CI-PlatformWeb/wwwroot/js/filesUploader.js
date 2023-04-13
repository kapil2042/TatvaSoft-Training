document.getElementById('file-input').addEventListener('change', function (e) {
    var fileList = e.target.files;
    var fileListView = document.getElementById('file-list');
    fileListView.innerHTML = '';

    for (var i = 0; i < fileList.length; i++) {
        var file = fileList[i];
        var fileItem = document.createElement('li');
        fileItem.className = 'file-item';

        var fileIcon = document.createElement('span');
        fileIcon.className = 'file-icon';
        fileIcon.innerHTML = `<i class="bi bi-file-earmark text-success me-2"></i> ${file.name}`;
        fileItem.appendChild(fileIcon);

        //var fileName = document.createElement('span');
        //fileName.className = 'file-name';
        //fileName.textContent = file.name;
        //fileItem.appendChild(fileName);

        //var deleteFileBtn = document.createElement('span');
        //deleteFileBtn.className = 'delete-file';
        //deleteFileBtn.innerHTML = '<i class="bi bi-trash3 text-danger"></i>';
        //fileItem.appendChild(deleteFileBtn);

        fileListView.appendChild(fileItem);

        //deleteFileBtn.addEventListener('click', function (event) {
        //    event.preventDefault();
        //    event.stopPropagation();
        //    var listItem = event.target.closest('.file-item');
        //    listItem.remove();
        //    $('#file-input')[0].files.splice(i, 1);
        //    console.log($('#file-input')[0].files);
        //});
    }
});
