var elem,
    newValue;

//$(document).load(function (e) {

//    if ($(".IfLarger").length) {
//        if ($(".display").height() > $(window).height()) {
//            $(".IfLarger").addClass("static");
//        }
//    }

//});

$(document).ready(function () {

    if (navigator.userAgent.match(/(iPod|iPhone|iPad)/)) {
        var a = 3;
        $(".txtQuantity").css("top", -a);
    }

    if (navigator.platform.indexOf('Mac') > -1) {
        var a = 0;
        $(".txtQuantity").css("top", -a);
    }


    //var odata = {
    //}
    //AjaxRequest("POST", "/Winkelwagen.aspx/RefreshWinkelwagen", odata, "", "json", "RefreshWinkelwagenCallback");
});

function isDiscountValid() {
    var fv = $('.divGlobalForm').data('formValidation');
    var bOk = true;

    fv.revalidateField('ctl00$ContentPlaceHolder1$txtKortingscode');

    if (fv.isValidField('ctl00$ContentPlaceHolder1$txtKortingscode') === false) {
        bOk = false;
    }

    if (bOk) {
        __doPostBack('ctl00$ContentPlaceHolder1$btnKortingscode', '')
    }
}

$(document).ready(function () {

    $(".toggle").on("click touch", function (e) {

        var parent = $(this).parents(".drop")

        if (parent.hasClass("rel")) {
            var height = 0;
            parent.find(".item").each(function (e) {
                height = height + 200;
            });
        } else {
            var height = 200;
        }

        if (parent.hasClass("active")) {
            parent.removeClass("active");
            parent.find(".content").css("height", 0);
        } else {
            //$(".content").css("height", 0);
            //$(".drop").removeClass("active");
            parent.addClass("active")
            parent.find(".content").css("height", height);
        }

    });

    //$(window).on("click touch", function (evt) {
    //    if (evt.target.id == "toggle")
    //        return;

    //    if ($(evt.target).closest('.toggle').length)
    //        return;

    //    if (evt.target.id == "drop")
    //        return;

    //    if ($(evt.target).closest('.drop').length)
    //        return;

    //    $(".drop").removeClass("active");
    //    $(".content").css("height", 0);
    //});


    $('.aProductLink').click(function () {
        var odata = {
            iArtikelID: $(this).parent(".divItem").data("artikel-id"),
            iArtikelVariantID: $(this).parent(".divItem").data("variant-id"),
            iAantal: 1
        }
        AjaxRequest("POST", "/Data/shopping-cart.aspx/iFacRgl", odata, "", "json", "iFacRglCallback");
    });
});


function iFacRglCallback() {
    if (sMsg.d.code === 1) {
        //$('.spanCountProducts').html(sMsg.d.aantal);

        //AjaxRequest("POST", "/Data/shopping-cart.aspx", "", "", "html", "sShoppingBagCallback");
        location.reload();
        return true;
    } else {
        return false;
    }
}


function sShoppingBagCallback() {
    $('.divCartHolder').html($('.selector', $(sMsg)));
    $('#divWinkelmandTotal').html($('.selector-price', $(sMsg)).text());
    $('.lblDiscount').html($('.selector-discount', $(sMsg)).text());
    $('.lblTotalExcl').html($('.selector-subtotal', $(sMsg)).text());

    $('.liCart').addClass('show');
}

var newValue, target;
jQuery(document).ready(function () {

    $("#txtQuantity").keydown(function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    $('#txtQuantity').keyup(function () {
        if ($(this).val() == '')
            $(this).val(0);
    });

    $('.aRemoveItem').click(function () {
        target = $(this).parent().parent();
        var odata = {
            iFacRglID: $(this).data('id')
        }
        AjaxRequest("POST", "/Winkelwagen.aspx/dFacRgl", odata, "", "json", "dFacRglCallback");
    });


    $('.txtQtyPlus').on("click touch", function (e) {
        e.preventDefault();

        target = $(this).parent(".Qty").find(".txtQuantity");

        var currentVal = parseInt(target.val());
        target.val(currentVal + 1);
        newValue = currentVal + 1;
        var odata = {
            iFacRglID: $(this).data('id'),
            iAantal: newValue
        }
        AjaxRequest("POST", "/Winkelwagen.aspx/uFacRgl", odata, "", "json", "uFacRglCallback");
    });

    $(".txtQtyMin").on("click touch", function (e) {
        e.preventDefault();
        target = $(this).parent(".Qty").find(".txtQuantity");
        var currentVal = parseInt(target.val());
        if (currentVal === 1) {
            return false;
        }
        if (currentVal === 0) {
            target.val(0);
        } else {
            newValue = currentVal - 1;
            target.val(newValue);
            var odata = {
                iFacRglID: $(this).data('id'),
                iAantal: newValue
            }
            console.log("VAL: " + newValue);
            console.log("iAantal: " + odata.iAantal);
            AjaxRequest("POST", "/Winkelwagen.aspx/uFacRgl", odata, "", "json", "uFacRglCallback");
        }
    });
    $(".txtQuantity").on("blur", function (e) {
        e.preventDefault();
        target = $(this).parent(".Qty").find(".txtQuantity");
        var currentVal = parseInt(target.val());
        if (currentVal <= 0) {
            var odata = {
                iFacRglID: $(this).data('id')
            }
            AjaxRequest("POST", "/Winkelwagen.aspx/dFacRgl", odata, "", "json", "dFacRglCallback");
            return false;
        } else {
            target.val(currentVal);
            newValue = currentVal;

            var odata = {
                iFacRglID: $(this).data('id'),
                iAantal: newValue
            }
            console.log(odata);
            AjaxRequest("POST", "/Winkelwagen.aspx/uFacRgl", odata, "", "json", "uFacRglCallback");
        }
    });
});

function dFacRglCallback() {
    console.log(sMsg);
    $('.lblTotalExcl').html(sMsg.d.totaalExcl);
    $('#divWinkelmandTotal').html(sMsg.d.totaalExcl);


    //$('.lblVat').html(msg.d.btw);
    $('.lblTotalIncl').html(sMsg.d.totaalIncl);
    $('.lblDiscount').html(sMsg.d.korting);
    $('.spanCountProducts').html(sMsg.d.aantal);
    $(target.closest('.shoppingItem')).fadeOut('fast');
    if (sMsg.d.aantal === "0") {
        $(".spanInfo").show();
        $(".btnCheckout").hide();
    }
}

function uFacRglCallback() {
    $('.lblTotalExcl').html(sMsg.d.totaalExcl);
    //$('.lblVat').html(msg.d.btw);
    $('.lblTotalIncl').html(sMsg.d.totaalIncl);
    $('.lblDiscount').html(sMsg.d.korting);
    $('.spanCountProducts').html(sMsg.d.aantal);
    console.log(sMsg.d.bedragregel);
    console.log($(target).parent().parent().parent().find(".total"));
    //console.log($(target).parent().parent(".right").find(".total"));
    $(target).parent().parent().parent().find(".total").html(sMsg.d.bedragregel);
    $("#divWinkelmandTotal").html(sMsg.d.totaalIncl);
    //$(elem).parent().find(".spanQty").text(sMsg.d.aantal);
}
function RefreshWinkelwagenCallback() {
    $('.spanCountProducts').html(sMsg.d.aantal);
    $("#divWinkelmandTotal").html(sMsg.d.totaalIncl);
}
