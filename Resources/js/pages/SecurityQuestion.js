function isSecurityQuestionValid() {
    var fv = $('.divGlobalForm').data('formValidation');
   
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtAnswer');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtAnswer') === false) {
        bOk = false;
    }

    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnSave', '')

    } else {
        return false;
    }
}