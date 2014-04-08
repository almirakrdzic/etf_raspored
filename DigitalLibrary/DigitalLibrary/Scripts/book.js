$(function () {
    $("a").on('click', function () {
        var dat = $(this).id;
        $.ajax({
            url: "/Book/Details",
            type: 'GET',
            data: dat,
            success: function (result) {
                $("#show").html(result);
            }
        });
        return false;
    });
});

function getBook() {
    var dat = item.id;
    $.ajax({
        url: "/Book/Details",
        type: 'GET',
        data: dat,
        success: function (result) {
            $("#show").html(result);
        }
    });
    return false;
}