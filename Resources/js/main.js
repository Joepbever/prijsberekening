$(document).ready(function () {
    $("#slider").slick({
        infinite: true,
        slidesToShow: 1,
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


});