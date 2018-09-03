$(document).ready(function () {
    $('.rblBetaalmethode input').change(function () {
        if ($(this).val() != 'ideal') {
            $(".divIdeal").hide();
        } else {
            $(".divIdeal").show();
        }
    });
});