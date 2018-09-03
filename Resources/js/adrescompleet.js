$(document).ready(function () {
    $(".txtHouseNr").blur(function () {
        if ($(".ddlCountry").find(':selected').data('code') === 'NL') {
                $('#').focus();

            $('.loading').show();
            var postcode = $(".txtZipCode").val();
            var nr = $(".txtHouseNr").val();
            if (postcode.length >= 6) {
                if (nr.length > 0) {
                    var wijkcode = postcode.substring(0, 4);
                    var straatcode;

                    if (postcode.length === 6) {
                        straatcode = postcode.substring(4, 6);
                    }
                    if (postcode.length === 7) {
                        straatcode = postcode.substring(5, 7);
                    }
                    var odata = {
                        sWijkCode: wijkcode,
                        sStraatCode: straatcode,
                        sNummer: nr
                    }
                    AjaxRequest("POST", "/Data/AdresCompleet.aspx/sAdres", odata, "#lblAdresInfo", "json", "sAdresCallback");
                } else {
                    $('.loading').hide();
                }
            } else {
                $('.loading').hide();
            }
        }
    });
});