$(document).ready(function () {
    $(".txtHouseNr").blur(function () {
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
    });
});

function sAdresCallback() {
    if (sMsg.d.code === "1") {
        $(".txtAddress").val(sMsg.d.straatnaam);
        $(".txtResidence").val(sMsg.d.plaatsnaam);
        //$("#hdfProvincie").val(sMsg.d.provincienaam);

        //var lati = sMsg.d.lat
        //if (!$.isNumeric(lati)) {
        //    lati = 0
        //}
        //$("#hdfLatitudeX").val(lati);

        //var longi = sMsg.d.lat
        //if (!$.isNumeric(longi)) {
        //    longi = 0
        //}
        //$("#hdfLongitudeY").val(longi);

        return true;
    } else {
        return false;
    }
}

function isPropositionValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtFirm');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtFirstName');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtSurname');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtEmail');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPhone');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtMobile');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtZipCode');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtHouseNr');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtAddress');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtResidence');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtSubject');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtMessage');
    fv.revalidateField('ctl00$ContentPlaceHolder1$uplFile');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtFirm') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtFirstName') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtSurname') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtEmail') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtPhone') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtMobile') === false) {
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
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtSubject') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtMessage') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$uplFile') === false) {
        bOk = false;
    }

    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnSubmit', '')
    } else {
        return false;
    }
}