
//Usage: 
//Geef input tag een class: "formatted-date". Gebruik "data-min-date" en "data-max-date" attributes om de defaults van 1945- 01 - 01 en Date.now() te overwriten
//Gebruik "dateValidator" voor formvalidation.IO 

$(document).ready(function () {

    // Polyfills
    if (!String.prototype.splice) {
        String.prototype.splice = function (start, delCount, newSubStr) {
            return this.slice(0, start) + newSubStr + this.slice(start + Math.abs(delCount));
        };
    }

    var sel = '.formatted-date';
    var fmt = 'dd-mm-yyyy'; // constant

    $(sel).each(function (e) {
        var minDate = new Date($(this).attr('data-min-date') || '1945-01-01');
        var maxDate = $(this).attr('data-max-date') ? new Date($(this).attr('data-max-date')) : new Date();

        $(this).attr('placeholder', fmt);
        var oldInput = 'dd-mm-yyyy';
        var oldCursor = 0;
        $(this).val(oldInput);

        $(this).focus(function (e1) {
            console.log('focus');
            // TODO

            setCaretPosition($(this).get(0), 0);

            // Work around Chrome's little problem
            window.setTimeout(function () {
                setCaretPosition($(this).get(0), 0);
            }, 1);
        });

        $(this).on('click', handleDateFieldInput);

        $(this).on('input', handleDateFieldInput);

        function handleDateFieldInput() {
            var val = $(this).val();
            var res = processDate(val);

            $(this).val(res.val);

            $(this).removeAttr('data-error-msg');
            if (res.errMsg) {
                $(this).attr('data-error-msg', res.errMsg);
            }
            if (res.cursor !== null)
                setCaretPosition($(this).get(0), res.cursor);
        }

        function processDate(val) {
            try {
                // check if deleted a "-": then: remove the letter before that character
                var count = (val.match(/-/g) || []).length;
                if (count < 2) {
                    if (val.indexOf('-') > 3) {
                        val = val[0] + '-' + val.slice(2);
                    } else {
                        val = val.slice(0, 4) + '-' + val.slice(5);
                    }
                }

                var cursor = null;
                var split = val.split('-');
                split[0] = split[0] || '';
                split[1] = split[1] || '';
                split[2] = split[2] || '';

                // day
                split[0] = split[0].replace(/[a-z]/gi, '').trim();
                // check day cursor
                if (cursor === null && split[0].length < 2)
                    cursor = split[0].length;
                split[0] = (split[0][0] || 'd') + (split[0][1] || 'd');

                // month
                split[1] = split[1].replace(/[a-z]/gi, '').trim();
                // check month cursor
                if (cursor === null && split[1].length < 2)
                    cursor = split[1].length + 3;
                split[1] = (split[1][0] || 'm') + (split[1][1] || 'm');
                split.splice(3);

                // year
                split[2] = split[2].replace(/[a-z]/gi, '').trim();
                // check year cursor
                if (cursor === null && split[2].length < 4)
                    cursor = split[2].length + 6;
                split[2] = (split[2][0] || 'y') + (split[2][1] || 'y') + (split[2][2] || 'y') + (split[2][3] || 'y');

                var val = split.join('-');

                var defVal = val.replace(/[md]{2}/gi, '01').replace(/1m/gi, '12').replace(/[md]/gi, '1').replace(/-....$/gi, '-1999');
                var fmttedCheck = '01-01-1999'.splice(0, defVal.length, defVal);

                // ceckyear
                var wrongValue = false;
                var min = minDate.getFullYear().toString();
                var max = maxDate.getFullYear().toString();
                var c = '';
                for (var j = 0; j < split[2].length && wrongValue === false; j++) {
                    c += split[2][j];
                    if (isNaN(c) === false && (parseInt(c) < parseInt(min.slice(0, j + 1)) || parseInt(c) > parseInt(max.slice(0, j + 1)))) {
                        wrongValue = true;
                    }
                }

                var regex = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
                if (regex.test(fmttedCheck) === false || wrongValue)
                    return {
                        errMsg: 'De datum moet tussen ' + gimmeDate(minDate) + ' en ' + gimmeDate(maxDate) + ' zitten',
                        val: oldInput,
                        cursor: oldCursor,
                    };

                oldInput = val;
                oldCursor = cursor;
                return {
                    //errMsg: val.match(/[a-z]/i) ? 'Datum, niet alle gegevens zijn ingevuld' : undefined,
                    val: val,
                    cursor: cursor,
                };
            } catch (err) {
                // prevent broken input field
                return {
                    errMsg: 'Datum, iets is misgegaan',
                    val: oldInput,
                    cursor: oldCursor,
                };
            }
        }
    });

    function setCaretPosition(ctrl, pos) {
        // Modern browsers
        if (ctrl.setSelectionRange) {
            ctrl.focus();
            ctrl.setSelectionRange(pos, pos);

            // IE8 and below
        } else if (ctrl.createTextRange) {
            var range = ctrl.createTextRange();
            range.collapse(true);
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    }

    function gimmeDate(dt) {
        return dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate();
    }
});

FormValidation.Validator.dateValidator = {
    validate: function (validator, $field, options) {
        var value = $field.val();
        var errorMsg = $field.attr('data-error-msg');
        if (errorMsg) {
            return {
                valid: false,
                message: errorMsg
            }
        }

        return true;
    }
};

        