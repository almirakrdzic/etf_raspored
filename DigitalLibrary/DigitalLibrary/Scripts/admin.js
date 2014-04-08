$(function () {
    // Easy pie charts
    $('.chart').easyPieChart({ animate: 1000 });
});

$(function () {
    $("#dashboard").on('click', function () {
        $.ajax({
            url: "/Home/AdminDashboard",
            type: 'GET',
            data: null,
            success: function (result) {
                $("#show").html(result);
            }
        });
        return false;
    });

});

$(function () {
    $("#books").on('click', function () {
        $.ajax({
            url: "/Book/Index",
            type: 'GET',
            data: null,
            success: function (result) {
                $("#show").html(result);
            }
        });
        return false;
    });

});

$(function () {
    $("#genres").on('click', function () {
        $.ajax({
            url: "/Genre/Index",
            type: 'GET',
            data: null,
            success: function (result) {
                $("#show").html(result);
            }
        });
        return false;
    });

});

$(function () {
    $("#authors").on('click', function () {
        $.ajax({
            url: "/Author/Index",
            type: 'GET',
            data: null,
            success: function (result) {
                $("#show").html(result);
            }
        });
        return false;
    });

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

function updateBook() {   
    $.ajax({
        url: "/Book/Create",
        type: 'GET',
        data: null,
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function updateAuthor() {
    $.ajax({
        url: "/Author/Create",
        type: 'GET',
        data: null,
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}

function deleteGenre(idd) {
    var dat = parseInt(idd);   
    $.post("/Genre/DeleteConfirmed", { id: dat }, function (result) {
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
            $("#newGenre").html(result);
        }
    });
    return false;    
}

$(function () {
    $('#createGenre').submit(function (evt) {
        //prevent the browsers default function
        evt.preventDefault();
        //grab the form and wrap it with jQuery
        var form = $(this);       
        $.post("/Genre/Create",form, function (result) {
            $("#show").html(result);
        });
        return false;       
    });
});

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



