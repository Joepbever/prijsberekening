$(document).ready(function () {
    $('.ddlCountry').on("change", function () {
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
    }).trigger("change");

    $('.ddlCountry2').on("change", function () {
        if ($(this).find(':selected').data('id') === 'B') {
            $('.divHouseNr2').addClass("hidden");
            $('.divAdd2').addClass("hidden");
            $('.divExtraField2').removeClass("hidden");
            $('.divZipCode2').addClass('col-xs-12');
            $('.divZipCode2').removeClass('col-xs-6');
        } else {
            $('.divHouseNr2').removeClass("hidden");
            $('.divAdd2').removeClass("hidden");
            $('.divExtraField2').addClass("hidden");
            $('.divZipCode2').removeClass('col-xs-12');
            $('.divZipCode2').addClass('col-xs-6');
        }
    }).trigger("change");

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

    $(".txtHouseNr2").blur(function () {
        if ($(".ddlCountry2").find(':selected').data('code') === 'NL') {
            var postcode = $(".txtZipCode2").val();
            var nr = $(".txtHouseNr2").val();
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
                    AjaxRequest("POST", "/Data/AdresCompleet.aspx/sAdres", odata, "#lblAdresInfo", "json", "sAdresCallback2");
                } 
            }
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

function sAdresCallback2() {

    if (sMsg.d.code === "1") {
        $(".txtAddress2").val(sMsg.d.straatnaam);
        $(".txtResidence2").val(sMsg.d.plaatsnaam);
        $("#hdfProvincie2").val(sMsg.d.provincienaam);
        var decimal = /^[-+]?[0-9]+\,[0-9]+$/;

        lati = sMsg.d.lat
        if (!lati.match(decimal)) {
            lati = 0
        }
        $("#hdfLatitudeX2").val(lati);

        longi = sMsg.d.lon
        if (!longi.match(decimal)) {
            longi = 0
        }

        $("#hdfLongitudeY2").val(longi);

        return true;
    } else {
        //var $form = $(".divGlobalForm"),
        //    fv = $form.data('formValidation');

        //var field = "ctl00$ContentPlaceHolder1$txtZipCode"
        //fv.updateMessage(field, 'blank', sMsg.d.msg).updateStatus(field, 'INVALID', 'blank');
        return false;
    }
}

function isUpdateValid() {
    var fv = $('.divGlobalForm').data('formValidation');
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
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtZipCode2');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtHouseNr2');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtAddress2');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtResidence2');

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
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtResidence2') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtZipCode2') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtHouseNr2') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtAddress2') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtResidence2') === false) {
        bOk = false;
    }

    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnUpdate', '')
    } else {
        return false;
    }
}



//$(document).ready(function () {
//    $('.landAC1').change(function () {
//        if ($(this).find(':selected').data('id') === 'B') {
//            $('.lblHuisnr1').hide();
//            $('.txtNr1').hide();
//            $('.txtToev1').hide();
//            $('.optional1').show();
//            $('.txtPostcode1').addClass('col-md-12');
//            $('.txtPostcode1').removeClass('col-xs-4');
//        } else {
//            $('.lblHuisnr1').show();
//            $('.txtNr1').show();
//            $('.txtToev1').show();
//            $('.optional1').hide();
//            $('.txtPostcode1').removeClass('col-md-12');
//            $('.txtPostcode1').addClass('col-xs-4');
//        }
//    });

//    $('.landAC2').change(function () {
//        if ($(this).find(':selected').data('id') === 'B') {
//            $('.lblHuisnr2').hide();
//            $('.txtNr2').hide();
//            $('.txtToev2').hide();
//            $('.optional2').show();
//            $('.txtPostcode2').addClass('col-md-12');
//            $('.txtPostcode2').removeClass('col-xs-4');
//        } else {
//            $('.lblHuisnr2').show();
//            $('.txtNr2').show();
//            $('.txtToev2').show();
//            $('.optional2').hide();
//            $('.txtPostcode2').removeClass('col-md-12');
//            $('.txtPostcode2').addClass('col-xs-4');
//        }
//    });
//});


//function isAccountValid() {
//    var fv = $('.divGlobalForm').data('formValidation');
//    var bOk = true;

//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtVoornaam');
//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtAchternaam');
//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtEmail');
//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtTel');
//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPostcode');
//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtNr');
//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtStraatnaam');
//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPlaatsnaam');
//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtProvince');

//    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtVoornaam') === false) {
//        bOk = false;
//    }
//    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtAchternaam') === false) {
//        bOk = false;
//    }
//    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtEmail') === false) {
//        bOk = false;
//    }
//    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtTel') === false) {
//        bOk = false;
//    }
//    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtPostcode') === false) {
//        bOk = false;
//    }
//    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtNr') === false) {
//        bOk = false;
//    }
//    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtStraatnaam') === false) {
//        bOk = false;
//    }
//    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtPlaatsnaam') === false) {
//        bOk = false;
//    }
//    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtProvince') === false) {
//        bOk = false;
//    }
//    if (bOk) {
//        __doPostBack('ctl00$ContentPlaceHolder1$btnUpdate', '')
//    }
//}