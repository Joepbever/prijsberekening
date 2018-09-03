$(document).ready(function () {

    $(".Product").each(function (e) {
        var posx = $(this).attr("data-posx");
        var posy = $(this).attr("data-posy");
        $(this).css({ "left": posx + "%", "top": posy + "%" });
    });

});
