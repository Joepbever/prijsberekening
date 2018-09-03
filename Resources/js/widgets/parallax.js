//var timeTaken;
//var start = +new Date();

//for (i = 0; i < 10000000; i++)
//{
//    // ...
//}

//timeTaken = (+new Date()) - start;

//if (timeTaken > 500) {
//    alert("slecht");
//} else {
//    alert("goed");
//}

var visie = $('.parallax-image-visie');
var creatie = $('.parallax-image-creatie');
var regie = $('.parallax-image-regie');
var productie = $('.parallax-image-productie');
var stats = $('.parallax-image-stats');
var more = $('.parallax-image-more');
var cases = $('.parallax-image-cases');
var slot = $('.parallax-image-slot');
var more1 = $('.parallax-image-more1');

$(document).ready(function () {

    //Controleer op IE
    var IE = false;
    var ua = window.navigator.userAgent;
    var old_ie = ua.indexOf('MSIE ');
    var new_ie = ua.indexOf('Trident/');

    if ((old_ie > -1) || (new_ie > -1)) {
        IE = true;
    }

    //Controleer op IOS
    var IOS = false;

    if(navigator.userAgent.match(/(iPod|iPhone|iPad)/)){
        IOS = true
    }

    //Controleer op Android
    var ua = navigator.userAgent.toLowerCase();
    var isAndroid = ua.indexOf("android") > -1; //&& ua.indexOf("mobile");

    if ($(window).width() < 992 || IE || IOS || isAndroid) {

        var timer;

        $(window).scroll(function () {
            parallax();
        });

        //if ($(window).width() > 992 && !ms_ie) {
        //    var getImageSrc = $('.parallax-image-visie').attr('src');
        //    $('.parallax-container-visie').css('background-image', 'url(' + getImageSrc + ')');
        //    $('.parallax-image-visie').hide();

        //    var getImageSrc = $('.parallax-image-creatie').attr('src');
        //    $('.parallax-container-creatie').css('background-image', 'url(' + getImageSrc + ')');
        //    $('.parallax-image-creatie').hide();

        //    var getImageSrc = $('.parallax-image-regie').attr('src');
        //    $('.parallax-container-regie').css('background-image', 'url(' + getImageSrc + ')');
        //    $('.parallax-image-regie').hide();

        //    var getImageSrc = $('.parallax-image-productie').attr('src');
        //    $('.parallax-container-productie').css('background-image', 'url(' + getImageSrc + ')');
        //    $('.parallax-image-productie').hide();

        //    var getImageSrc = $('.parallax-image-stats').attr('src');
        //    $('.parallax-container-stats').css('background-image', 'url(' + getImageSrc + ')');
        //    $('.parallax-image-stats').hide();

        //    var getImageSrc = $('.parallax-image-more').attr('src');
        //    $('.parallax-container-more').css('background-image', 'url(' + getImageSrc + ')');
        //    $('.parallax-image-more').hide();

        //    var getImageSrc = $('.parallax-image-cases').attr('src');
        //    $('.parallax-container-cases').css('background-image', 'url(' + getImageSrc + ')');
        //    $('.parallax-image-cases').hide();

        //    var getImageSrc = $('.parallax-image-slot').attr('src');
        //    $('.parallax-container-slot').css('background-image', 'url(' + getImageSrc + ')');
        //    $('.parallax-image-slot').hide();

        //    var getImageSrc = $('.parallax-image-more1').attr('src');
        //    $('.parallax-container-more1').css('background-image', 'url(' + getImageSrc + ')');
        //    $('.parallax-image-more1').hide();

        //}

    }else{
        var getImageSrc = visie.attr('src');
        $('.parallax-container-visie').css('background-image', 'url(' + getImageSrc + ')');
        visie.hide();

        var getImageSrc = creatie.attr('src');
        $('.parallax-container-creatie').css('background-image', 'url(' + getImageSrc + ')');
        creatie.hide();

        var getImageSrc = regie.attr('src');
        $('.parallax-container-regie').css('background-image', 'url(' + getImageSrc + ')');
        regie.hide();

        var getImageSrc = productie.attr('src');
        $('.parallax-container-productie').css('background-image', 'url(' + getImageSrc + ')');
        productie.hide();

        var getImageSrc = stats.attr('src');
        $('.parallax-container-stats').css('background-image', 'url(' + getImageSrc + ')');
        stats.hide();

        var getImageSrc = more.attr('src');
        $('.parallax-container-more').css('background-image', 'url(' + getImageSrc + ')');
        more.hide();

        var getImageSrc = cases.attr('src');
        $('.parallax-container-cases').css('background-image', 'url(' + getImageSrc + ')');
        cases.hide();

        var getImageSrc = slot.attr('src');
        $('.parallax-container-slot').css('background-image', 'url(' + getImageSrc + ')');
        slot.hide();

        var getImageSrc = more1.attr('src');
        $('.parallax-container-more1').css('background-image', 'url(' + getImageSrc + ')');
        more1.hide();

    }

});

function parallax() {
    var scrolled = $(window).scrollTop();
    visie.css('top', (-250) + (scrolled * 0.11) + 'px');
    creatie.css('top', (-375) + (scrolled * 0.11) + 'px');
    regie.css('top', (-475) + (scrolled * 0.11) + 'px');
    productie.css('top', (-625) + (scrolled * 0.11) + 'px');
    stats.css('top', (-925) + (scrolled * 0.11) + 'px');
    more.css('top', (-500) + (scrolled * 0.11) + 'px');
    cases.css('top', (-175) + (scrolled * 0.11) + 'px');
    slot.css('top', (-1250) + (scrolled * 0.11) + 'px');
    more1.css('top', (-50) + (scrolled * 0.11) + 'px');
}