$(document).ready(function () {
    $(window).on("resize", function () {
        setTemp12Height();
    }).trigger("resize");

    $(".divTemp12").bind("DOMSubtreeModified", function () {
        setTemp12Height();
    });
});

function setTemp12Height() {
    $(".divTemp12").height($(".divTemp12 .divInside").outerHeight());
}