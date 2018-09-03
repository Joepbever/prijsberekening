$(document).ready(function (e) {
    // Script voor toggle bars
    var min = 90;
    $("li").css("max-height", min);

    $(".Toggle").click(function (e) {
        var parent = $(this).parent();
        var max = parent.find(".content").height() + (min + 30);
        var next = parent.next("li");

        if (parent.hasClass("active")) {
            parent.removeClass("active");
            $("li").removeClass("next");
            $("li").css("max-height", min)
        } else {
            $("li").removeClass("active");
            parent.addClass("active");
            $("li").css("max-height", min);
            parent.css("max-height", max + "px");
            $("li").removeClass("next");
            next.addClass("next");
        }
    });

    // Script voor cookie melding pop-up
    $('.btnAccept').click(function () {
        location.reload(true);
        $.cookie('PopUp', '1');
        $.cookie('Functional', '1');
        $.cookie('Analytics', '1');
    });

    if ($.cookie('PopUp')) {
        $('#PopupCookie').hide();
    } else {
        $('#PopupCookie').show();
    }

    if ($.cookie('Analytics')) {
        $("#cbPreference1").attr("checked", "checked");
        ga('create', 'UA-68889175-5', 'auto');
        ga('send', 'pageview');
    } else {
        $("#cbPreference1").removeAttr("checked");
        $.removeCookie('_ga', { domain: '.websentiment.nl', path: '/' });
        $.removeCookie('_gat', { domain: '.websentiment.nl', path: '/' });
        $.removeCookie('_gid', { domain: '.websentiment.nl', path: '/' });
    }

    // Script voor cookie cookie instellingen
    $('.btn-accept').click(function () {
        event.preventDefault();
        if ($("#cbPreference1").is(':checked')) {
            gaOptin();
            $.cookie('Analytics', '1');

        } else {
            gaOptout();
            $.removeCookie('Analytics', { path: '/' });
        }
    });

    var gaProperty = 'UA-68889175-5';
    var disableStr = 'ga-disable-' + gaProperty;
    if (document.cookie.indexOf(disableStr + '=true') > -1) {
        window[disableStr] = true;
    }

    // Opt-out function
    function gaOptout() {
        document.cookie = disableStr + '=true; expires=Thu, 31 Dec 2099 23:59:59 UTC; path=/';
        window[disableStr] = true;
        location.reload(true);
    }
    // Opt-in function
    function gaOptin() {
        document.cookie = disableStr + '=false; expires=Thu, 31 Dec 2099 23:59:59 UTC; path=/';
        window[disableStr] = false;
        location.reload(true);
    }
});

//Functie om cookie aan te maken
function setCookie(name, value = '') {
    document.cookie = name + '=' + value;
}

//Functie om cookie te checken
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

//Functie om cookie te verwijderen
function removeCookie(name) {
    document.cookie = name + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

