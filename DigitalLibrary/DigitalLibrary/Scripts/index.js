
$.ajax({
    url: "/Home/Login",
    type: 'GET',
    data: null,
    success: function (result) {
        $("#indexshow").html(result);
    }
});

function register() {
    $.ajax({
        url: "/User/Create",
        type: 'GET',
        data: null,
        success: function (result) {
            $("#indexshow").html(result);
        }
    });
    return false;
}

function login() {
    $.ajax({
        url: "/Home/Login",
        type: 'GET',
        data: null,
        success: function (result) {
            $("#indexshow").html(result);
        }
    });
    return false;
}