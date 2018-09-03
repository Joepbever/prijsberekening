$(document).ready(function () {
    $(window).resize(function () {
        if ($(window).width() > 786) {
            if ($(".smooth-scroll").length > 0) {
                $('.smooth-scroll a[href*=#]:not([href=\\#]), a[href*=\\#]:not([href=\\#]).smooth-scroll').click(function () {
                    var target = $(this.hash);
                    target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                    if (target.length) {
                       if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {

                            $("html, body").animate({
                                scrollTop: target.offset().top - 102
                            }, 1000);

                            $("html, body").on("scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove", function () {
                                $("html, body").stop();
                            });

                            return false;
                        } else {
                            event.preventDefault();
                            setTimeout(function () {
                                $('body, html').animate({ scrollTop: target.offset().top - 102 }, 'slow');
                            }, 100);

                            $("html, body").on("mousewheel wheel DOMMouseScroll mousedown keyup touchmove", function () {
                                $("html, body").stop();
                            });
                        }
                    }
                });
            }
        } else {
            if ($(".smooth-scroll").length > 0) {
                $('.smooth-scroll a[href*=#]:not([href=\\#]), a[href*=\\#]:not([href=\\#]).smooth-scroll').click(function () {
                    var target = $(this.hash);
                    target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                    if (target.length) {
                        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {

                            $("html, body").animate({
                                scrollTop: target.offset().top
                            }, 1000);

                            $("html, body").on("scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove", function () {
                                $("html, body").stop();
                            });

                            return false;
                        } else {
                            event.preventDefault();
                            setTimeout(function () {
                                $('body, html').animate({ scrollTop: target.offset().top - 120 }, 'slow');
                            }, 100);

                            $("html, body").on("scroll mousedown wheel DOMMouseScroll mousewheel keyup touchmove", function () {
                                $("html, body").stop();
                            });
                        }
                    }
                });
            }
        }
    }).trigger("resize");
});

$('.smooth-scroll').click(function () {
    //$('#bs-example-navbar-collapse-1').removeClass('in');
    $('.dropdown').removeClass('open');
})

$(document).ready(function () {
    $('.smooth-scroll').each(function () {
        var goLink = $(this).attr('href');

        if (goLink != undefined || goLink != null) {
            goLink = goLink.toLowerCase().split(' ').join('-');

            $(this).attr("href", goLink);
        }
    });

    $('.smooth-scroll').click(function () {
        //$(".hiddenNav").delay(1000).fadeOut(500);
        //$(".mainLogo").delay(1000).fadeIn(500);
        //$(".mainBurger").delay(1000).fadeIn(500);
        //$(".hiddenLogo").delay(1000).fadeOut(500);
        //$(".hiddenBurger").delay(1000).fadeOut(500);


        //var targetGa = $(this).attr('href');

        //if (targetGa != undefined || targetGa != null) {
        //    targetGa = targetGa.toLowerCase().replace("#", "");

        //    ga('send', 'pageview', targetGa);
        //}

        $(".smooth-scroll").removeClass("active");

        $(this).addClass("active");

        //console.log(targetGa);
    });

    //$('.scroll').each(function () {
    //    var currentLink = $(this).attr('href');
    //    $(this).attr("href", "/" + currentLink);
    //});

    var url = window.location.toString().toLowerCase();
    //console.log("url = " + url);

    $('.smooth-scroll').each(function () {
        var myHref = $(this).attr('href');

        if (myHref != undefined || myHref != null) {
            myHref = myHref.toLowerCase().replace("#", "").replace("/", "");

            if (myHref != "") {
                if (url.indexOf(myHref) >= 0) {
                    $(this).addClass('active');
                }
            }
        }

        //console.log(myHref);
    });


    var sections = $('section');
    var nav = $('.navbar-nav');
    var navHeight = nav.outerHeight() + 70;

    $(window).on('scroll', function () {
        var curPos = $(this).scrollTop();

        sections.each(function () {
            var top = $(this).offset().top - navHeight,
                bottom = top + $(this).outerHeight();

            if (curPos >= top && curPos <= bottom) {
                nav.find('a').removeClass('active');
                sections.removeClass('active');

                //console.log($(this).attr('id'));
                nav.find('a[href="#' + $(this).attr('id') + '"]').addClass('active');
            }
        });
    });

    $(window).on("hashchange", offsetAnchor);
    window.setTimeout(offsetAnchor, 1);
});

function offsetAnchor() {
    if (location.hash.length !== 0) {
        window.scrollTo(window.scrollX, window.scrollY - 60);
    }
}