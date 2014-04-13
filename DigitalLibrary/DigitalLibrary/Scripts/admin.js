$(function () {
    // Easy pie charts
    $('.chart').easyPieChart({ animate: 1000 });
});


function getBook(idd) {    
    var dat = parseInt(idd);
    $.ajax({
        url: "/Book/Details",
        type: 'GET',
        data: {id : dat},
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function getUser(idd) {
    var dat = parseInt(idd);
    $.ajax({
        url: "/User/Details",
        type: 'GET',
        data: { id: dat },
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function promoteUser(idd) {

    var dat = parseInt(idd);
    $.ajax({
        url: "/User/PromoteUser",
        type: 'GET',
        data: { id: dat },
        success: function (result) {
            $("#show").html(result);
        }
    });
}
function updateBook(idd) {
    var dat = parseInt(idd);
    $.ajax({
        url: "/Book/Edit",
        type: 'GET',
        data: { id: dat },
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function deleteBook(idd) {
    var dat = parseInt(idd);
    $.ajax({
        url: "/Book/Delete",
        type: 'GET',
        data: { id: dat },
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function updateAuthor(idd) {
    var dat = parseInt(idd);
    $.ajax({
        url: "/Author/Edit",
        type: 'GET',
        data: { id: dat },
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function deleteAuthor(idd) {
    var dat = parseInt(idd);
    $.ajax({
        url: "/Author/Delete",
        type: 'GET',
        data: { id: dat },
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function editGenre(idd) {
    var dat = parseInt(idd);
    $.ajax({
        url: "/Genre/Edit",
        type: 'GET',
        data: {id : dat},
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function deleteGenre(idd) {
    var dat = parseInt(idd);
    $.post("/Genre/Delete", { id: dat }, function (result) {
        $("#show").html(result);
    });
    return false;
}

function addGenre() {   
    $.ajax({
        url: "/Genre/Create",
        type: 'GET',
        data: null,
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;    
}


function getAuthor(idd) {
    var dat = parseInt(idd);
    $.ajax({
        url: "/Author/Details",
        type: 'GET',
        data: { id: dat },
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function editprofile() {
    $.ajax({
        url: "/Account/EditProfile",
        type: 'GET',
        data: null,
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}



