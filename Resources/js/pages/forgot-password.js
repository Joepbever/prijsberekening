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