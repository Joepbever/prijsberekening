//$(document).ready(function () {
//    $(".JSlinks").click(function () {
//        $(this).toggleClass("open");
//        $("body").toggleClass("search-open");
//        event.preventDefault();
//    });

//    $(".JScart").click(function () {
//        $(this).toggleClass("open");
//        $(".drop").toggleClass("open");
//        event.preventDefault();
//    });

//    $(".JSclose-cart").click(function () {
//        $(".drop").removeClass("open");
//        $(".JScart").removeClass("open");
//        event.preventDefault();
//    });

//    //$(".liCart").hover(
//    //    function () {
//    //        $(".drop").addClass("open");
//    //    }, function () {
//    //        $(".drop").removeClass("open");
//    //    }
//    //);

//    $(window).resize(function () {
//        if ($(window).width() < 1200) {

//        }
//        else {
//            $(function () {
//                // ADD SLIDEDOWN ANIMATION TO DROPDOWN //
//                $('.dropdown').on('show.bs.dropdown', function (e) {
//                    $(this).find('.dropdown-menu').first().stop(true, true).slideDown();
//                });

//                // ADD SLIDEUP ANIMATION TO DROPDOWN //
//                $('.dropdown').on('hide.bs.dropdown', function (e) {
//                    e.preventDefault();
//                    $(this).find('.dropdown-menu').first().stop(true, true).slideUp(400, function () {
//                        //On Complete, we reset all active dropdown classes and attributes
//                        //This fixes the visual bug associated with the open class being removed too fast
//                        $('.dropdown').removeClass('open');
//                        $('.dropdown').find('.dropdown-toggle').attr('aria-expanded', 'false');
//                    });
//                });
//            });
//        }
//    });

//    $(".js-close-nav").click(function () {
//        $("body").removeClass("search-open");
//    });

//    $(".overlay-body").click(function () {
//        $("#hamburger ").removeClass('open');
//        $("body").removeClass("search-open");
//        $("body").removeClass("nav-open");
//    });

//    var $hamburger = $('#hamburger');

//    $hamburger.on('click', function () {
//        $hamburger.toggleClass('open');
//        $("body").toggleClass('nav-open');
//    });
//});

//$(window).on("click touch", function (evt) {
//    if (evt.target.id == "toggle")
//        return;

//    if ($(evt.target).closest('.toggle').length)
//        return;

//    if (evt.target.id == "drop")
//        return;

//    if ($(evt.target).closest('.drop').length)
//        return;

//    $(".dropdown, .drop, .livesearch").removeClass("show");
//});


//$(".dropdown-toggle").on("touch click", function (e) {
//    if ($(window).width() < 1200) {
//        e.preventDefault();
//        var li = $(this).parent();
//        $(this).toggleClass("active");
//        li.find(".sub-items").toggleClass("active");
//    }
//});

//$(".dropdown .toggle").on('click touch', function () {
//    var toggle = $(this).parents(".dropdown");
//    var drop = toggle.find(".drop");

//    if (!toggle.hasClass("show")) {
//        $(".dropdown").removeClass("show");
//        toggle.addClass("show");
//    } else {
//        toggle.removeClass("show");
//    }

//    if (!drop.hasClass("show")) {
//        $(".drop").removeClass("show");
//        drop.addClass("show");
//    } else {
//        drop.removeClass("show");
//    }

//});


//$(document).on("touch click", "#liFirst.dropdown #aFirst", function (e) {
//    if ($(window).width() < 1200) {
//        var parent = $(this).closest("#liFirst.dropdown");
//        var dropdownItems = parent.find(".nav-products");
//        dropdownItems.toggleClass("open");
//    }
//});