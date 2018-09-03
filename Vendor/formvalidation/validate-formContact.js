$(document).ready(function () {
    $('.divGlobalForm').formValidation({
        framework: 'bootstrap',
        icon: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            ctl00$ContentPlaceHolder1$txtName: {
                validators: {
                    notEmpty: {
                        message: 'Uw voornaam is niet ingevuld.'
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Uw voornaam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtCompany: {
                validators: {
                    notEmpty: {
                        message: 'De bedrijfsnaam is niet ingevuld.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtEmail: {
                validators: {
                    notEmpty: {
                        message: "Er is geen e-mailadres ingevuld"
                    },
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: "Dit is geen geldig e-mailadres"
                    }
                }
            },
            job: {
                validators: {
                    notEmpty: {
                        message: 'The job position is required'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtTel: {
                validators: {
                    notEmpty: {
                        message: 'U heeft geen telefoonnummer ingevuld.'
                    },
                    regexp: {
                        regexp: /^[0-9]*$/,
                        message: "Een telefoonnummer mag alleen bestaan uit cijfers"
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtMessage: {
                validators: {
                    notEmpty: {
                        message: 'U heeft geen bericht ingevuld.'
                    }
                }
            }
        }
    }).on('err.field.fv', function (e, data) {
        data.fv.disableSubmitButtons(false);
    }).on('success.field.fv', function (e, data) {
        data.fv.disableSubmitButtons(false);
    });
});

function isContactValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtName');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtCompany');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtEmail');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtTel');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtMessage');
    fv.revalidateField('job');


    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtName') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtCompany') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtEmail') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtTel') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtMessage') === false) {
        bOk = false;
    }
    if (fv.isValidField('job') === false) {
        bOk = false;
    }
    console.log(bOk);
    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnSubmit', '')
    } else {
        return false;
    }
};