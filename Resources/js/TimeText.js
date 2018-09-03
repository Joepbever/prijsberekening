$(document).ready(function () {

    var selectedText;

    $("input[type='radio']").on('click', function () {
        selectedText = $(this).val();
    });

    $("#btnContinue").on("click", function (e) {

        e.preventDefault();
        $("#DivExclusion").show();
        document.getElementById("lblConfirmTime").textContent = selectedText + ".";

    });
})