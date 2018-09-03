$(document).ready(function () {

    //var odata = {}
    //AjaxRequest("POST", "/Winkelwagen.aspx/RefreshWinkelwagen", odata, "", "json", "RefreshWinkelwagenCallback");

    $('.aProductLink').click(function () {
        var odata = {
            iArtikelID: $(this).data('id'),
            iArtikelVariantID: $(this).parent().find('.JS-size input:checked').val(),
            iAantal: 1,
            iTaalID: $("#hidTaalID").val()
        }

        AjaxRequest("POST", "/Data/shopping-cart.aspx/iFacRgl", odata, "", "json", "iFacRglCallback");
    });

    //$('.checkboxSizes input').change(function () {
    //    $('.checkboxSizes input').prop('checked', false);
    //    $(this).prop('checked', true);
    //    $("#hidArtikelVariantID").val($(this).val());
    //});



    $('.checkboxSizes input').change(function () {
        var id = $(this).parent().parent().parent().parent().parent().attr('id');
        //console.log(id);
        $('#' + id + ' .checkboxSizes input').prop('checked', false);
        $(this).prop('checked', true);
        $("#hidArtikelVariantID").val($(this).val());
    });


    //var LastItem = -1;
    //$(".JsProducts").css("height", "0px");
    //$(".JsToggleCategory").click(function () {

    //    var parent = $(this).parent();
    //    var id = parent.data('id');
    //    var CurrentItem = id;

    //    console.log(CurrentItem);
    //    console.log(LastItem);

    //    if (LastItem != CurrentItem) {
    //        $(".JsProducts").css("height", "0px");

    //        var products = parent.find(".JsProducts");
    //        var height = $(".JsProductsContainer").height();

    //        setTimeout(function () {
    //            products.css("height", height);
    //            $(".JsCorrectHeight").css("height", height);
    //        }, 500);

    //    } else {
    //        $(".JsProducts").css("height", "0px");
    //        $(".JsCorrectHeight").css("height", "0px");
    //    }
    //    LastItem = id;
    //});

    //$(".product-bar").on('click', function (e) {
    //    e.stopPropagation();
    //});


    var $grid = $('.grid').isotope({
        itemSelector: '.element-item',
        layoutMode: 'fitRows',
    });

    var filterFns = {
        numberGreaterThan50: function () {
            var number = $(this).find('.number').text();
            return parseInt(number, 10) > 50;
        }
    };

    if ($('.is-checked').length > 0) {
        $grid.isotope({ filter: $('.is-checked').data('filter') });
    }

    $('.filters-button-group').on('click', 'li', function () {
        var filterValue = $(this).attr('data-filter');
        filterValue = filterFns[filterValue] || filterValue;
        $grid.isotope({ filter: filterValue });
    });

    $('.button-group').each(function (i, buttonGroup) {
        var $buttonGroup = $(buttonGroup);
        $buttonGroup.on('click', 'li', function () {
            $buttonGroup.find('.is-checked').removeClass('is-checked');
            $(this).addClass('is-checked');
        });
    });

    if ($(window).width() > 1250) {
        $("li.item").click(function () {
            $('html, body').animate({
                scrollTop: $("#grid-items").offset().top - 145
            }, 750);
        });
    }
    else {
        $("li.item").click(function () {
            $('html, body').animate({
                scrollTop: $("#grid-items").scrollTop() - 95
            }, 750);
        });
    }
    var iCatID = $('#hidCatID').val();
    if (iCatID != "") {
        var scrollLeft = $('#grid-items .is-checked').scrollLeft() - 150;
        $("#grid-items").scrollLeft(scrollLeft);
    }
});

function iFacRglCallback() {
    if (sMsg.d.code === 1) {
        $('.spanCountProducts').html(sMsg.d.aantal);
        $('.total').html(sMsg.d.aantal);

        AjaxRequest("POST", "/Data/shopping-cart.aspx?lang=" + $('#hidLanguage').val(), "", "", "html", "sShoppingBagCallback");

        return true;
    } else {
        return false;
    }
}

function sShoppingBagCallback() {
    $('.divCartHolder').html($('.selector', $(sMsg)));
    $('#divWinkelmandTotal').html($('.selector-price', $(sMsg)).text());
    $('.liCart').addClass('show');
    $('.drop').addClass('open');
}

function RefreshWinkelwagenCallback() {
    $('.spanCountProducts').html(sMsg.d.aantal);
    $("#divWinkelmandTotal").html(sMsg.d.totaalIncl);
}

//$(document).on('click', '.smoothscroll-product', function (event) {
//    event.preventDefault();

//    var target = this.hash,
//     $target = $(target);
//    product - bar
//    if ($(window).width() < 767) {
//        return false;
//    }
//    else {

//    }
//});