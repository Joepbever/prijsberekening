$(document).ready(function () {
    //Controleer op IOS
    var IOS = false;

    if (navigator.userAgent.match(/(iPod|iPhone|iPad)/)) {
        IOS = true
    }

    if (IOS) {
        $(".IfOIS").addClass("ios");
    }

    $('.checkboxSizes input').change(function () {

        $('.checkboxSizes input').prop('checked', false);
        $(this).prop('checked', true);
        $("#hidArtikelVariantID").val($(this).val());
    });

    $('.txtQtyPlusProduct').on("click touch", function (e) {
        e.preventDefault();

        var currentVal = parseInt($(this).parent(".Qty").find(".txtQuantity").val());
        $(".txtQuantity").val(currentVal + 1);

    });

    $('.txtQuantity').on('keypress', function (key) {
        if (key.charCode < 48 || key.charCode > 57) {
            return false;
        }
    });

    $('.txtQuantity').blur(function () {
        if (!this.value) {
            $(this).val(1);
        }
    });

    $(".txtQtyMinProduct").on("click touch", function (e) {
        e.preventDefault();

        var currentVal = parseInt($(this).parent(".Qty").find(".txtQuantity").val());

        if (currentVal == 1) {
            $(".txtQuantity").val(1);
        } else {
            $(".txtQuantity").val(currentVal - 1);
        }
    });

    if (navigator.userAgent.match(/(iPod|iPhone|iPad)/)) {
        var a = 3;
        $(".txtQuantity").css("top", -a);
    }

    if (navigator.platform.indexOf('Mac') > -1) {
        var a = 0;
        $(".txtQuantity").css("top", -a);
    }

    $('.aProductLinkProduct').click(function () {
        var odata = {
            iArtikelID: $("#hidArtikelID").val(),
            iArtikelVariantID: $("#hidArtikelVariantID").val(),
            iAantal: $(".txtQuantity").val(),
            iTaalID: $("#hidTaalID").val()
        }
        console.log(odata);
        AjaxRequest("POST", "/Data/shopping-cart.aspx/iFacRgl", odata, "", "json", "iFacRglCallback");
    });

});

function iFacRglCallback() {
    if (sMsg.d.code === 1) {
        console.log(sMsg.d);
        $('.spanCountProducts').html(sMsg.d.aantal);
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

//rgba(255," + (r_bg - 25) + "," + r_bg + ",1)