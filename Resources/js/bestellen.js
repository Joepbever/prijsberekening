$(document).ready(function () {
    $('.bedrag input').change(function () {
        $('.bedrag input').prop('checked', false);
        $(this).prop('checked', true);
        $("#hidArtikelID").val($(this).val());
    });
    $('.betaling input').change(function () {
        $('.betaling input').prop('checked', false);
        $(this).prop('checked', true);
        if ($(this).val() === 'rbideal') {
            $('.bankInp').show();
        } else {
            $('.bankInp').hide();
        }
        $("#hidBetaalmethode").val($(this).val());
    });

    $('.ideal').click(function () {
        $('.bankInp').show();
    })

    //$('#ctl01').formValidation({
    //    framework: 'bootstrap',
    //    icon: {
    //        valid: 'glyphicon glyphicon-ok',
    //        invalid: 'glyphicon glyphicon-remove',
    //        validating: 'glyphicon glyphicon-refresh'
    //    },
    //    fields: {
    //        ctl00$ContentPlaceHolder1$txtVoornaam: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'Uw voornaam is verplicht'
    //                }
    //            },
    //            regexp: {
    //                regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
    //                message: 'Uw voornaam kan enkel bestaan uit letters.'
    //            }
    //        },
    //        ctl00$ContentPlaceHolder1$txtAchternaam: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'Uw achternaam is verplicht'
    //                }
    //            },
    //            regexp: {
    //                regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
    //                message: 'Uw voornaam kan enkel bestaan uit letters.'
    //            }
    //        },
    //        ctl00$ContentPlaceHolder1$txtStraatnaam: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'Uw adres is verplicht'
    //                }
    //            },
    //        },
    //        ctl00$ContentPlaceHolder1$txtNr: {
    //            validators: {
    //                notEmpty: {
    //                    message: '*'
    //                },
    //                numeric: {
    //                    message: '*',
    //                }
    //            },
    //        },
    //        ctl00$ContentPlaceHolder1$txtPostcode: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'Uw postcode is verplicht'
    //                }
    //                //,
    //                //zipCode: {
    //                //    country: 'NL',
    //                //    message: 'Dit is geen geldige postcode'
    //                //}
    //            }
    //        },
    //        ctl00$ContentPlaceHolder1$txtPlaatsnaam: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'Uw adres is verplicht'
    //                }
    //            },
    //        },
    //        //ctl00$ContentPlaceHolder1$txtMobiel: {
    //        //    validators: {
    //        //        notEmpty: {
    //        //            message: 'Uw telefoonnummer is verplicht'
    //        //        },
    //        //        numeric: {
    //        //            message: 'Uw telefoonnummer kan alleen bestaan uit cijfers'
    //        //        }
    //        //    },
    //        //},
    //        ctl00$ContentPlaceHolder1$txtEmail: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'Uw e-mail adres is verplicht'
    //                },
    //                regexp: {
    //                    regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
    //                    message: 'Dit is geen geldig e-mail adres'
    //                }
    //            }
    //        },
    //        ctl00$ContentPlaceHolder1$cbxPrivacy: {
    //            validators: {
    //                notEmpty: {
    //                    message: 'U dient akkoord te gaan met de privacy statement'
    //                }
    //            }
    //        }

    //    }
    //}).on('err.field.fv', function (e, data) {
    //    data.fv.disableSubmitButtons(false);
    //}).on('success.field.fv', function (e, data) {
    //    data.fv.disableSubmitButtons(false);
    //});

});

function isBestellenValid() {
    __doPostBack('ctl00$ContentPlaceHolder1$btnAfronden', '')

}
//function isStreetValid() {
//    var fv = $('#ctl01').data('formValidation');
//    var bOk = true;

//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtStraatnaam');
//    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPlaatsnaam');
//}
