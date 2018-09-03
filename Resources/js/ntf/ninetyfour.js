
$(document).ready(function () {
    var odata = {}
    AjaxRequest("POST", "/Winkelwagen.aspx/RefreshWinkelwagen", odata, "", "json", "RefreshWinkelwagenCallback");


/* 
   ==========================================================================
   NavToggle
   ========================================================================== 
*/
    $header = $("header");
    $navToggle = $(".NavToggle");
    $hamburger = $('#hamburger');

    $navToggle.click(function () {
        $hamburger.toggleClass('open');
        $header.toggleClass('active');
        $("body").toggleClass('nav-open');
    });

/* 
   ==========================================================================
   Scroll
   ========================================================================== 
*/
    var $image = $('#ScrollImg > img');
    $(window).on('scroll', function () {
        var scroll = $(window).scrollTop() * -.2 + 150//offset
        $image.css('top', scroll + 'px');
    }).trigger('scroll');

    $(window).scroll(function () {
        if ($(this).scrollTop() > 1) {
            $header.addClass("scrolled");
        } else {
            $header.removeClass("scrolled");
        }
    });

/* 
   ==========================================================================
   BagToggle
   ========================================================================== 
*/

    $bagToggle = $(".BagToggle");
    $drop = $(".drop");
    $overlap = $(".overlap");

    $bagToggle.click(function () {
        $drop.toggleClass('active');
        $overlap.toggleClass('hidden');
    })

    $overlap.click(function () {
        $drop.toggleClass('active');
        $overlap.toggleClass('hidden');
    })

/* 
   ==========================================================================
   Menu
   ========================================================================== 
*/
    $(".MainNav a").each(function (e) {

        var characters = $(this).text().split("");

        $this = $(this);
        $this.empty();
        $.each(characters, function (i, el) {
            $this.append("<span>" + el + "</span");
        });
    });

/* 
   ==========================================================================
   Animation on Scroll
   ========================================================================== 
*/
    AOS.init();
});

$.ajaxSetup({ cache: false });
var sMsg, sDiv, sDivArtikelen, sDivButton, getMessageID, getFaqID;
function AjaxRequest(reqtyp, sUrl, odata, msgId, datatype, sFunctie) {
    if (datatype === "html") {
        $.ajax({
            type: reqtyp,
            url: sUrl, //Pagina bestandsnaam en functienaam
            contentType: "application/json; charset=utf-8",
            dataType: datatype,
            success: function (msg) {
                sDiv = $('#categories', $(msg));
                sDivArtikelen = $('#artikelen', $(msg));
                sDivButton = $('#button', $(msg));
                sMsg = msg;
                window[sFunctie]();
            },
            error: function (msg) {
                $(msgId).html(msg);
                return false;
            }
        });
    } else {
        $.ajax({
            type: reqtyp,
            url: sUrl, //Pagina bestandsnaam en functienaam
            data: JSON.stringify(odata),
            contentType: "application/json; charset=utf-8",
            dataType: datatype,
            success: function (msg) {
                sMsg = msg;
                console.log(sFunctie);
                window[sFunctie]();
            },
            error: function (msg) {
                $(msgId).html(msg);
                return false;
            }
        });
    }
}
