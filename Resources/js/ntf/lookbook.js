$(document).ready(function () {
    var $ell = $('.left');
    var $el = $('.right');
    $(window).on('scroll', function () {

        if ($(window).width() > 768) {
            var scroll = $(window).scrollTop();

            /*Left*/
            $($ell[0]).css('left', (scroll * +.08 - 100) + 'px').css('top', (scroll * -.03 + 350) + 'px');
            /*Right*/
            $($el[0]).css('right', (scroll * + .08 - 200) + 'px').css('top', (scroll * -.03 + 350) + 'px');

            for (var i = 1; i < $el.length + 1; i++) {
                /*Left*/
                $($ell[i]).css('left', (scroll * +.08 - (100 + (70 * i)) + 'px')).css('top', (scroll * -.03 + 350) + 'px');

                /*Right*/
                $($el[i]).css('right', (scroll * +.08 - (200 + (60 * i)) + 'px')).css('top', (scroll * -.03 + 350) + 'px');
            }
        }
    }).trigger('scroll');

    $(window).resize(function () {
        if ($(window).width() < 768) {
            $ell.removeAttr('style');
            $el.removeAttr('style');
        }
    });
});