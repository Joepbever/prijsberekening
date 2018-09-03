$(document).ready(function () {
    var TIME_PATTERN = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/;
    var BETWEEN = /^[1-8]$/;
    $('.divGlobalForm').formValidation({
        framework: 'bootstrap',
        icon: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            ctl00$ContentPlaceHolder1$txtAnswer: {
                trigger: 'blur',
                validators: {
                    stringLength: {
                        max: 21,
                        message: 'Antwoord mag niet meer dan 21 karakters lang zijn.'
                    },
                    notEmpty: {
                        message: 'Uw antwoord is niet ingevuld.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$ddlSecurity: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Select a valid security question.'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z-]+$/,
                        message: 'Sequrity question can only consist of letters.'
                    }
                }
            },
            ctl00$txtName: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Uw naam is niet ingevuld.'
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Naam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$txtEmail: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Uw e-mailadres is verplicht.'
                    },
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: 'Dit is geen geldig e-mailadres.'
                    }
                }
            },
            ctl00$txtEmailModal: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Uw e-mailadres is verplicht.'
                    },
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: 'Dit is geen geldig e-mailadres.'
                    }
                }
            },
            ctl00$txtEmailMaster: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Uw e-mailadres is verplicht.'
                    },
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: 'Dit is geen geldig e-mailadres.'
                    }
                }
            },
            ctl00$txtNameMaster: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Uw naam is niet ingevuld.'
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Naam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$TextBox2: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'dit veld is niet ingevuld.'
                    }
                }
            },
            ctl00$txtEmail1: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: "Er is geen e-mailadres ingevuld."
                    },
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: "Dit is geen geldig e-mailadres."
                    }
                }
            },
            ctl00$txtPhone: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'U heeft geen telefoonnummer ingevuld.'
                    },
                    regexp: {
                        regexp: /^[0-9]*$/,
                        message: "Een mobiele telefoonnummer mag alleen bestaan uit cijfers."
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtKortingscode: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: "Kortingscode is niet ingevuld."
                    }
                }
            },
            ctl00$txtLoginEmail: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Uw e-mailadres is verplicht.'
                    },
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: 'Dit is geen geldig e-mailadres.'
                    }
                }
            },
            ctl00$txtGn: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Uw e-mailadres is verplicht.'
                    },
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: 'Dit is geen geldig e-mailadres.'
                    }
                }
            },

            ctl00$txtLoginPassword: {
                trigger: 'blur',
                validators: {
                    stringLength: {
                        min: 5,
                        message: 'Wachtwoord moet 5 karakters lang zijn.'
                    },
                    notEmpty: {
                        message: 'Wachtwoord is niet ingevuld.'
                    }
                }
            },
            ctl00$txtDatum: {
                trigger: 'blur',
                validators: {
                    dateValidator: {
                        message: 'The date is not valid'
                    },
                    regexp: {
                        regexp: /^[0-9-]{10}$/,
                        message: "Datum dag is niet ingevuld"
                    },
                }
            },

            ctl00$rbGroup: {
                validators: {
                    notEmpty: {
                        message: 'RB Group is niet ingevuld'
                    }
                }
            },
            ctl00$ddList: {
                validators: {
                    notEmpty: {
                        message: 'Dd List is niet ingevuld'
                    }
                }
            },


            //ctl00$ContentPlaceHolder1$txtNewPassword: {
            //    trigger: 'blur',
            //    validators: {
            //        stringLength: {
            //            min: 5,
            //            message: 'Nieuwe wachtwoord moet 5 karakters lang zijn.'
            //        },
            //        notEmpty: {
            //            message: 'Nieuwe wachtwoord is niet ingevuld.'
            //        }
            //    }
            //},

            ctl00$ContentPlaceHolder1$txtNewPasswordConfirm: {
                //trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Nieuwe wachtwoord bevestigen is verplicht.'
                    },
                    identical: {
                        field: 'ctl00$ContentPlaceHolder1$txtNewPassword',
                        message: 'Nieuwe wachtwoord komt niet overeen.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtNewPassword: {
                //trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Nieuwe wachtwoord bevestigen is verplicht.'
                    },
                    identical: {
                        field: 'ctl00$ContentPlaceHolder1$txtNewPasswordRepeat',
                        message: 'Nieuwe wachtwoord komt niet overeen.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtNewPasswordRepeat: {
                //trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Oude wachtwoord verplicht.'
                    },
                    identical: {
                        field: 'ctl00$ContentPlaceHolder1$txtNewPassword',
                        message: 'Nieuwe wachtwoord komt niet overeen.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtOldPassword: {
                //trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Oude wachtwoord verplicht.'
                    },
                }
            },


            ctl00$ContentPlaceHolder1$txtLoginEmail: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Uw e-mailadres is verplicht.'
                    },
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: 'Dit is geen geldig e-mailadres.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtGn: {
                trigger: 'blur',
                validators: {
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: "Dit is geen geldig gebruikersnaam"
                    },
                    notEmpty: {
                        message: "Er is geen gebruikersnaam ingevuld"
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtLoginPassword: {
                trigger: 'blur',
                validators: {
                    stringLength: {
                        min: 5,
                        message: 'Wachtwoord moet 5 karakters lang zijn.'
                    },
                    notEmpty: {
                        message: 'Wachtwoord is niet ingevuld.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtCompany: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Bedrijfsnaam is niet ingevuld.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtFirm: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Firma is niet ingevuld.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtVAT: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'BTW nummer is niet ingevuld.'
                    }
                    //vat: {
                    //    country: 'NL',
                    //    message: 'BTW nummer is niet geldig'
                    //}
                }
            },
            ctl00$ContentPlaceHolder1$txtFirstName: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Voornaam is niet ingevuld.'
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Vorrnaam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtSurname: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Achternaam is niet ingevuld.'
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Achternaam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtFirstName2: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Voornaam is niet ingevuld.'
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Vorrnaam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtSurname2: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Achternaam is niet ingevuld.'
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Achternaam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtName: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Naam is niet ingevuld.'
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Naam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtPassword: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Wachtwoord is niet ingevuld.'
                    },
                    stringLength: {
                        min: 5,
                        message: 'Wachtwoord moet 5 karakters lang zijn.'
                    },
                }
            },
            ctl00$ContentPlaceHolder1$txtPasswordConfirm: {
                //trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Bevestiging wachtwoord is niet ingevuld.'
                    },
                    stringLength: {
                        min: 5,
                        message: 'Wachtwoord moet 5 karakters lang zijn.'
                    },
                    identical: {
                        field: 'ctl00$ContentPlaceHolder1$txtPassword',
                        message: 'De wachtwoord komt niet overeen.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtEmail: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: "Er is geen e-mailadres ingevuld."
                    },
                    emailAddress: {
                        message: " " //Dit doen anders weergeeft die automatische melding op de html5 attribute type="email"
                    },
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: "Dit is geen geldig e-mailadres."
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtPhone: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'U heeft geen telefoonnummer ingevuld.'
                    },
                    regexp: {
                        regexp: /^[0-9]*$/,
                        message: "Een mobiele telefoonnummer mag alleen bestaan uit cijfers."
                    },
                    stringLength: {
                        min: 10,
                        max: 12,
                        message: 'Telefoonummer moet tussen 10 & 12 cijfers zijn.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtMobile: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'U heeft geen mobiele telefoonnummer ingevuld.'
                    },
                    regexp: {
                        regexp: /^[0-9]*$/,
                        message: "Een mobiele telefoonnummer mag alleen bestaan uit cijfers."
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtZipCode: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Postcode is niet ingevuld.'
                    },
                    zipCode: {
                        country: 'NL',
                        message: 'De postcode is niet geldig'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtZipCode2: {
                trigger: 'blur',
                validators: {
                    //notEmpty: {
                    //    message: 'Postcode is niet ingevuld.'
                    //},
                    zipCode: {
                        country: 'NL',
                        message: 'De postcode is niet geldig'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtSubject: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Onderwerp is niet ingevuld.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtMessage: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Bericht is niet ingevuld.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtHouseNr: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Huisnummer is niet ingevuld.'
                    },
                    numeric: {
                        message: 'Huisnummer kan alleen bestaan uit cijfers.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtHouseNr2: {
                trigger: 'blur',
                validators: {
                    //notEmpty: {
                    //    message: 'Huisnummer is niet ingevuld.'
                    //},
                    numeric: {
                        message: 'Huisnummer kan alleen bestaan uit cijfers.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtAddress: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Straatnaam is niet ingevuld.'
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Straatnaam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtAddress2: {
                trigger: 'blur',
                validators: {
                    //notEmpty: {
                    //    message: 'Straatnaam is niet ingevuld.'
                    //},
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Straatnaam kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtResidence: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'Woonplaats is niet ingevuld. '
                    },
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Woonplaats kan alleen bestaan uit letters.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtResidence2: {
                trigger: 'blur',
                validators: {
                    //notEmpty: {
                    //    message: 'Woonplaats is niet ingevuld.'
                    //},
                    regexp: {
                        regexp: /^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/,
                        message: 'Woonplaats kan alleen bestaan uit letters.'
                    }
                }
            },
            'ctl00$ContentPlaceHolder1$cbTerms': {
                validators: {
                    choice: {
                        min: 1,
                        max: 1,
                        message: 'U dient akkoord te gaan met de algemene voorwaarden'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$uplFile: {
                validators: {
                    file: {
                        extension: 'png,jpeg,jpg,gif,bmp,doc,pdf',
                        type: 'image/png,image/jpeg,image/gif,image/x-ms-bmp,application/msword,application/pdf',
                        maxSize: 5242880,
                        message: 'U kunt alleen een png, jpeg, jpg, gif, bmp, doc of pdf bestand uploaden'
                    }
                }
            },




            ctl00$ContentPlaceHolder1$txtFax: {
                trigger: 'blur',
                validators: {
                    notEmpty: {
                        message: 'U heeft geen faxnummer ingevuld.'
                    },
                    regexp: {
                        regexp: /^[0-9]*$/,
                        message: "Een faxnummer mag alleen bestaan uit cijfers."
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtDate: {
                trigger: 'blur',
                validators: {
                    date: {
                        format: 'DD-MM-YYYY',
                        message: 'Datum format klopt niet, 00 00 0000',
                    },
                    notEmpty: {
                        message: 'Datum is niet ingevuld.'
                    }
                }
            },

            ctl00$txtTime: {
                verbose: false,
                validators: {
                    notEmpty: {
                        message: 'The start time is required'
                    },
                    regexp: {
                        regexp: TIME_PATTERN,
                        message: 'The start time must be between 09:00 and 17:59'
                    }
                }
            },

            ctl00$txtPersonen: {
                verbose: false,
                validators: {
                    notEmpty: {
                        message: 'The start time is required'
                    },
                    regexp: {
                        regexp: BETWEEN,
                        message: 'The start time must be between 09:00 and 17:59'
                    }
                }
            },
            ctl00$cbAkk: {
                validators: {
                    choice: {
                        min: 1,
                        max: 1,
                        message: 'U dient akkoord te gaan met de algemene voorwaarden'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$cbRemember: {
                validators: {
                    choice: {
                        min: 1,
                        max: 1,
                        message: 'U dient akkoord te gaan met de privacy verklaring.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtAdd: {
                trigger: 'blur',
                validators: {
                    //notEmpty: {
                    //    message: 'Uw heeft uw keuze nog niet gemaakt.'
                    //},
                    regexp: {
                        regexp: /^[a-z0-9]{0,8}$/,
                        message: 'U kunt niet meer dan 8 tekens gebruiken.'
                    }
                }
            },
            ctl00$ContentPlaceHolder1$txtAdd2: {
                trigger: 'blur',
                validators: {
                    //notEmpty: {
                    //    message: 'Uw heeft uw keuze nog niet gemaakt.'
                    //},
                    regexp: {
                        regexp: /^[a-z0-9]{0,8}$/,
                        message: 'U kunt niet meer dan 8 tekens gebruiken.'
                    }
                }
            },
        }
    }).on('err.field.fv', function (e, data) {
        data.fv.disableSubmitButtons(false);
    }).on('success.field.fv', function (e, data) {
        data.fv.disableSubmitButtons(false);
    });

    /* 
       ==========================================================================
       NTF - Contact
       ========================================================================== 
    */

    var count = 1;
    var numOfSteps = 5;
    var fv = $('.divGlobalForm').data('formValidation');

    //Contact_stepDiv
    $('.closeToggle').click(function () {
        if ($('.contact_steps').hasClass('active')) {
            $('body').css('overflowY', 'scroll');
            $('.contact_steps').removeClass('active');
            $('.contact_steps').addClass('close-contact-steps');
            $('#contact' + count).addClass('close_input_contact');
            $('#nextStep').show();
            $('#btnSubmit1').hide();
            $('.form-control').val('');
            count = 1;
        } else {
            $('body').css('overflowY', 'hidden');
            $('#contact1').removeClass('close_input_contact').addClass('active');
            $('.contact_steps').addClass('active');
            $('.contact_steps').removeClass('close-contact-steps');
        }
    });

    //Contact_steps
    $('#nextStep').click(function () {
        console.log("test");

        fv.revalidateField($('#contact' + count + " input").attr('name'));

        if (fv.isValidField($('#contact' + count + " input").attr('name'))) {
            $('#contact' + count).addClass('close_input_contact');
            count++;
            $('#contact' + count).removeClass('close_input_contact').addClass('active');
            if (count === numOfSteps) {
                $('#nextStep').hide();
                $('#btnSubmit1').show();
            }
        }
    });

});

function isContactValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    console.log(fv);
    var bOk = true;

    fv.revalidateField('ctl00$txtName');
    fv.revalidateField('ctl00$txtEmail');
    fv.revalidateField('ctl00$txtPhone');
    fv.revalidateField('ctl00$txtDatum');
    fv.revalidateField('ctl00$rbGroup');
    fv.revalidateField('ctl00$cbAkk');
    fv.revalidateField('ctl00$ddList');

    if (fv.isValidField('ctl00$txtName') === false) {
        bOk = false;
    } if (fv.isValidField('ctl00$txtEmail') === false) {
        bOk = false;
    } if (fv.isValidField('ctl00$txtPhone') === false) {
        bOk = false;
    } if (fv.isValidField('ctl00$cbAkk') === false) {
        bOk = false;
    } if (fv.isValidField('ctl00$txtDatum') === false) {
        bOk = false;
    } if (fv.isValidField('ctl00$rbGroup') === false) {
        bOk = false;
    } if (fv.isValidField('ctl00$ddList') === false) {
        bOk = false;
    }
    console.log(bOk);
    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnSubmit1', '')
    } else {
        return false;
    }
}

function isContactValid1() {
    var fv = $('.divGlobalForm').data('formValidation');
    console.log(fv);
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtName');
    fv.revalidateField('ctl00$txtPhone');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtEmail');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPhone');
    fv.revalidateField('ctl00$ContentPlaceHolder1$cbRemember');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtMessage');

    if (fv.isValidField('ctl00$txtPhone') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtName') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtEmail') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtPhone') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtMessage') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$cbRemember') === false) {
        bOk = false;
    }
    console.log(bOk);
    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnSubmit1', '')
    } else {
        return false;
    }
}

function isNieuwsbriefValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    console.log(fv);
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtEmail');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtEmail') === false) {
        bOk = false;
    }
    console.log(bOk);
    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnSubmit1', '')
    } else {
        return false;
    }
}

function isNieuwsbriefValid1() {
    var fv = $('.divGlobalForm').data('formValidation');
    console.log(fv);
    var bOk = true;

    fv.revalidateField('ctl00$txtEmail1');

    if (fv.isValidField('ctl00$txtEmail1') === false) {
        bOk = false;
    }
    console.log(bOk);
    if (bOk) {
        __doPostBack('ctl00$btnSubmit', '')
    } else {
        return false;
    }
}



function isPasswordValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtNewPassword');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtNewPasswordConfirm');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtNewPassword') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtNewPasswordConfirm') === false) {
        bOk = false;
    }

    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnUpdatePassword', '')
    } else {
        return false;
    }
}




function isRegisterValid() {
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
    fv.revalidateField('ctl00$ContentPlaceHolder1$cbTerms');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtAnswer');
    fv.revalidateField('ctl00$ContentPlaceHolder1$ddlSecurity');

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
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtAnswer') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$ddlSecurity') === false) {
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

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtEmail');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtLoginPassword');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtEmail') === false) {
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

function isChangePasswordValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtOldPassword');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtNewPassword');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtNewPasswordRepeat');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtOldPassword') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtNewPassword') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtNewPasswordRepeat') === false) {
        bOk = false;
    }

    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnSavePassword', '')
    } else {
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

function isSequrityquestionValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    console.log(fv);
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$ddlQuestions');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtAnswer');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPassword');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$ddlQuestions') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtAnswer') === false) {
        bOk = false;
    }
    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtPassword') === false) {
        bOk = false;
    }
    console.log(bOk);
    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnSave', '')
    } else {
        return false;
    }
}