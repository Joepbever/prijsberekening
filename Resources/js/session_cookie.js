﻿/* Pop-up class = pop-up-session */
$('.pop-up-session').ready(function () {
    if (sessionStorage.getItem('dontLoad') === null) {
        setTimeout(function () {
            /* Show pop-up */
            $('.pop-up-session').css('background', 'red');
        }, 2000); // Time in milliseconds
        sessionStorage.setItem('dontLoad', 'true');
    }
});


/* Pop-up class = pop-up-cookie */
$('.pop-up-cookie').ready(function () {
    if (getCookie('dontLoad') === null) {
        setTimeout(function () {
            /* Show pop-up */
        }, 20000); // Time in milliseconds
        setCookie('dontLoad');
    }
});

function setCookie(name, value) {
    value = value || '';
    document.cookie = name + '=' + value;
}

function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else {
        begin += 2;
        var end = document.cookie.indexOf(";", begin);
        if (end == -1) {
            end = dc.length;
        }
    }
    return decodeURI(dc.substring(begin + prefix.length, end));
}

function removeCookie(name) {
    document.cookie = name + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}