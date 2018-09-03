function isUpdatePasswordValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtPassword');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtNewPassword');
    fv.revalidateField('ctl00$ContentPlaceHolder1$txtNewPasswordConfirm');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtPassword') === false) {
        bOk = false;
    }
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