function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

var numb = '0123456789';
var getallen = '0123456789';
var lwr = 'abcdefghijklmnopqrstuvwxyz';
var upr = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
var bmk = '0123456789,';
var bmp = '0123456789.';
function isValid(the_key, val) {
    if (!the_key) {
        the_key = event.keyCode;
    } else if (the_key === 8) {
        return true;
    }
    var t = String.fromCharCode(the_key);

    if (the_key === "") return false;
    for (i = 0; i < val.length; i++) {
        if (val.indexOf(t.charAt(i), 0) === -1) return false;
    }
    return true;
}
function isHoofdletters(the_key) { return isValid(the_key, upr); }
function isBedragMetKomma(the_key) { return isValid(the_key, bmk); }
function isBedragMetPunt(the_key) { return isValid(the_key, bmp); }
function isGetal(the_key) { return isValid(the_key, getallen); }
function isLower(the_key) { return isValid(the_key, lwr); }
function isUpper(the_key) { return isValid(the_key, upr); }
function isAlpha(the_key) { return isValid(the_key, lwr + upr); }
function isAlphanum(the_key) { return isValid(the_key, lwr + upr + numb); }
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}