$(document).ready(function () {
    $('.ddlCountry').change(function () {
        if ($(this).find(':selected').data('id') === 'B') {
            $('.divHouseNr').addClass("hidden");
            $('.divAdd').addClass("hidden");
            $('.divExtraField').removeClass("hidden");
            $('.divZipCode').addClass('col-xs-12');
            $('.divZipCode').removeClass('col-xs-6');
        } else {
            $('.divHouseNr').removeClass("hidden");
            $('.divAdd').removeClass("hidden");
            $('.divExtraField').addClass("hidden");
            $('.divZipCode').removeClass('col-xs-12');
            $('.divZipCode').addClass('col-xs-6');
        }
    });

    $(".txtHouseNr").blur(function () {
        if ($(".ddlCountry").find(':selected').data('code') === 'NL') {
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
                }
            } 
        }
    });

    $('#cbRemember').on("click touch", function () {

        if ($(this).hasClass("ones")) {
            if ($(this).is(':checked')) {
                $(this).prop('checked', true);
            } else {
                $(this).prop('checked', false);
            }
        } else {
            $(this).addClass("ones");
            $(this).prop('checked', true);
        }

    });

});

function sAdresCallback() {

    if (sMsg.d.code === "1") {
        $(".txtAddress").val(sMsg.d.straatnaam);
        $(".txtResidence").val(sMsg.d.plaatsnaam);
        $("#hdfProvincie").val(sMsg.d.provincienaam);
        var decimal = /^[-+]?[0-9]+\,[0-9]+$/;

        lati = sMsg.d.lat
        if (!lati.match(decimal)) {
            lati = 0
        }
        $("#hdfLatitudeX").val(lati);

        longi = sMsg.d.lon
        if (!longi.match(decimal)) {
            longi = 0
        }

        $("#hdfLongitudeY").val(longi);

        return true;
    } else {
        //var $form = $(".divGlobalForm"),
        //    fv = $form.data('formValidation');

        //var field = "ctl00$ContentPlaceHolder1$txtZipCode"
        //fv.updateMessage(field, 'blank', sMsg.d.msg).updateStatus(field, 'INVALID', 'blank');
        return false;
    }
}

function isRegisterValid() {
    var fv = $('.register').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtCompany');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtVAT');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtFirstName');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtSurname');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPassword');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPasswordConfirm');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtEmail');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPhone');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtZipCode');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtHouseNr');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtAddress');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtResidence');
    fv.revalidateField('ctl00$ContentPlaceHolder1$cbTerms');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtCompany') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtVAT') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtFirstName') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtSurname') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtPassword') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtPasswordConfirm') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtEmail') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtPhone') === false) {
        bOk = false;
    }

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtZipCode') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtHouseNr') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtAddress') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtResidence') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$cbTerms') === false) {
        bOk = false;
    }
    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnRegister', '')
    } else {
        return false;
    }
}

function isLoginValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtGn');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtLoginPassword');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtGn') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtLoginPassword') === false) {
        bOk = false;
    }

    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnLogin', '')
    } else {
        return false;
    }
}

function isLoginPopValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$txtGn');
    fv.revalidateField('ctl00$txtLoginPassword');

    if (fv.isValidField('ctl00$txtGn') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$txtLoginPassword') === false) {
        bOk = false;
    }

    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnLogin', '')
    } else {
        return false;
    }
}

function isEmailValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtEmail');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtEmail') === false) {
        bOk = false;
    }

    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnRetrievePassword', '')
    } else {
        return false;
    }
}