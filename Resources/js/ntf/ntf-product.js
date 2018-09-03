$(document).ready(function () {

    //Variabels
    $slider1 = $('.slider_one');
    $slider2 = $('.slider_two');
    $slider3 = $('.slider_three');
    $arrowN = $('.arrowNext');
    $arrowB = $('.arrowBack');


    //Slider 1
    $slider1.slick({
        asNavFor: '.slider_two, .slider_three',
        arrows: false,
        center: true,
        slidesToShow: 1,
        infinite: true,
        focusOnSelect: false,
        accessibility: false,
    });

    //Custom arrows slider 1
    $arrowB.click(function () {
        $slider1.slick('slickPrev');
    })

    $arrowN.click(function () {
        $slider1.slick('slickNext');
    })


    //Slider 2
    $slider2.slick({
        asNavFor: '.slider_one, .slider_three',
        arrows: false,
        slidesToShow: 1,
        infinite: true,
        accessibility: false,
    });

    //Navigate slider 1 to slide of slider 2
    $slider2.on('click', function (event) {
        event.preventDefault();
        var goToSingleSlide = $(this).slick('slickCurrentSlide') + 1;

        $slider1.slick('slickGoTo', goToSingleSlide);
    });

    //Slider 3
    $slider3.slick({
        asNavFor: ".slider_two, .slider_one",
        arrows: false,
        slidesToShow: 1,
        infinite: true,
        accessibility: false,
    })

    //Navigate slider 1 to slide of slider 2
    $slider3.on('click', function (event) {
        event.preventDefault();
        var goToSingleSlide = $(this).slick('slickCurrentSlide') + 2;
        $slider1.slick('slickGoTo', goToSingleSlide);
    });

    //Bottom slider
    $sliderB = $('.bottom_slider');

    $sliderB.slick({
        speed: 5000,
        autoplay: true,
        autoplaySpeed: 0,
        cssEase: 'linear',
        slidesToShow: 1,
        slidesToScroll: 1,
        variableWidth: true,
        arrows: false,
        accessibility: false,
        pauseOnHover: false
    });
})