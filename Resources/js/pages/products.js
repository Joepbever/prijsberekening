$(document).ready(function () {

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
    console.log(sMsg);
    if (sMsg.d.code === 1) {
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
}

//$(document).ready(function () {
//    $('#list').click(function (event) { event.preventDefault(); $('#products .item').addClass('list-group-item');});
//    $('#grid').click(function (event) { event.preventDefault(); $('#products .item').removeClass('list-group-item'); $('#products .item').addClass('grid-group-item'); });
//});
