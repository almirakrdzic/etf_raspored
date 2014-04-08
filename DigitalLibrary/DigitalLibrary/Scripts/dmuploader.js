function imagesSelected(myFiles) {
    for (var i = 0, f; f = myFiles[i]; i++) {
        var imageReader = new FileReader();
        imageReader.onload = (function (aFile) {
            return function (e) {
                var image = document.getElementById("img");
                image.setAttribute("src", e.target.result);               
            };
        })(f);
        imageReader.readAsDataURL(f);
    }
}

function dropIt(e) {
    var file = document.getElementById('file');
    file.files = e.dataTransfer.files;
    imagesSelected(e.dataTransfer.files);
    e.stopPropagation();
    e.preventDefault();
}


