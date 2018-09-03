$.expr[":"].contains = $.expr.createPseudo(function (arg) {
    return function (elem) {
        return $(elem).text().toUpperCase().indexOf(arg.toUpperCase()) >= 0;
    };
});

var pageIndex = 1;
var pageCount;

var firstLoad = true;
window.onload = function () {
    setTimeout(function () {
        scrollTo(0, -1);
    }, 0);
}
$(window).load(function (e) {

    if ($(window).width() < 768) {

        if ($(window).width() < 651) {
            var ListHeight = 75;
        } else {
            var ListHeight = 102;
        }

        $(document).on('click touch', ".CBList", function (e) {
            if ($(e.target).is('h6')) {
                $(this).find("tr").each(function (e) {
                    ListHeight = (ListHeight + 72.5);
                    return ListHeight;
                });
                if ($(this).hasClass("active")) {
                    if ($(window).width() < 651) {
                        ListHeight = 75;
                    } else {
                        ListHeight = 102;
                    }
                    $(this).css("max-height", ListHeight);
                    $(this).removeClass("active");
                } else {
                    $(this).css("max-height", ListHeight);
                    $(this).addClass("active");
                }
            } else {
                return;
            }
        });
    }
});

$(document).ready(function (e) {

    //console.log('NOWWWW: ' + $(window).scrollTop());

    $('.search-brillen').select2({
        language: {
            noResults: function (params) {
                return "Geen resultaten gevonden";
            }
        }
    });
  
    $("#PriceMin, #PriceMax").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    $("#PriceMin, #PriceMax").blur(function () {
        if (!$(this).val()) {
            $(this).val('€ 0');
        }
    });

    function IfTouch() {
        return 'ontouchstart' in window || navigator.maxTouchPoints;
    };

    if (IfTouch()) {
        $(".divSlider").addClass("touch");
    }

    //$(window).scroll(function () {
    //    if ($(this).scrollTop() > 285) {
    //        $(".divFilters").addClass("fixed");
    //    } else {
    //        $(".divFilters").removeClass("fixed");
    //    }
    //});

    $("#txtSearch").keyup(function () {
        searchPersonen();
    });

    $("#txtSearch").keypress(function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            searchPersonen();
            return false;
        }
    });
   
    function searchPersonen() {
        var sTerm = $("#txtSearch").val();
        $("div.product").addClass("hidden");
        $("div.product:contains('" + sTerm + "')").removeClass("hidden");
    }

    //if ($(window).width() > 768) {
    //    //var HeightFilter = ($(".divFilters").height() + 90),
    //    //    HeightProducts = $(".divProducts").height();
    //    //console.log(HeightFil)
    //    //if (HeightFilter > HeightProducts) {
    //    //    $(".divProducts").css("height", HeightFilter);
    //    //} else {
    //    //    $(".divFilters").css("height", (HeightProducts + 45));
    //    //}
    //} else {
        
    //}


    $(".FilterToggle").click(function (e) {
        $(".divFilters").toggleClass("active");
        if ($(".divFilters").hasClass("active")) {
            $('html, body').animate({
                scrollTop: $(".divProducts").offset().top - 60
            }, 1000);
        }
    });
    $(".ToggleSearch").click(function (e) {
        $(".divSearch").addClass("active");
    });
    $(".CloseSearch").click(function (e) {
        $(".divSearch").removeClass("active");
    });

    $(document).on('click touchstart', "#categories input", function () { ////// CATEGORIE CLICK
        var parent = $(this).parent();
        if (parent.hasClass("active")) {
            parent.removeClass("active");
        } else {
            parent.addClass("active");
        }

        if ($(this).val() === 'all') {
            $("#cblSubCat_1 input").prop('checked', false); // bij all de eerste categorie unchecken
        }
        resetFilterTags(0);
    });
    
    $(document).on('click touchstart', ".active-filter", function () {
        resetFilterTags($(this).data('id'))
    });

    $("#SortOptions").change(function () {
        resetFilterTags(0)
    });

    if ($(window).width() > 768) {
        var maxHeight = 120;
        $(document).on('click touchstart', ".divShow", function () {
            var parent = $(this).parent();

            parent.find(".CBOptions tr").each(function (e) {
                maxHeight = (maxHeight + 15);
            });

            if (parent.hasClass("expanded")) {
                parent.removeClass("expanded");
                maxHeight = 120;
                parent.find(".CBOptions").css("max-height", maxHeight);
                $(this).text("Toon alle opties");
            } else {
                parent.addClass("expanded");
                parent.find(".CBOptions").css("max-height", maxHeight);
                $(this).text("Toon minder opties");
            }
        });
    }

    var prijsMin, prijsMax;
    if (sessionStorage.getItem('prijsMin') === null) {
        prijsMin = 0;
        prijsMax = 500;
    } else {
        prijsMin = sessionStorage.getItem('prijsMin');
        prijsMax = sessionStorage.getItem('prijsMax');
    }
    $("#slider").slider({
        range: true,
        min: 0,
        max: 500,
        values: [prijsMin, prijsMax],
        slide: function (event, ui) {
            $("#PriceMin").val("€" + ui.values[0]);
            $("#PriceMax").val("€" + ui.values[1]);
        },
        change: function (event, ui) {
            resetFilterTags(0);
        }
    });

    $(document).on('click touchstart', ".clear-filters", function () { // RESET CLICK
        var iCatIDs = '';
        var prijsMin = $("#slider").slider("values", 0);
        var prijsMax = $("#slider").slider("values", 1);
        var sort = $('#SortOptions').val();
        $(".loading").addClass('activeloader');
        AjaxRequest("POST", "/Data/Categorie.aspx?iCatIDs=" + iCatIDs + "&prijsMin=" + prijsMin + "&prijsMax=" + prijsMax + "&sort=" + sort + "&count=" + 0, "", "", "html", 'resetFilterCallBack');
        $('.active-filters').html("");
        saveSearchInCookie(iCatIDs, prijsMin, prijsMax, sort, 0, firstLoad) // Count staat op 0 omdat je alle filters hier verwijderd
    });

    if (sessionStorage.getItem('search') === 'true') {
        $(".loading").addClass('activeloader');

        //console.log('search by session ----------------------------------')
        //console.log('pageIndex: ' + sessionStorage.getItem('pageIndex'));
        //console.log('pageCount: ' + sessionStorage.getItem('pageCount'));
        //console.log('iCatIDs: ' + sessionStorage.getItem('iCatIDs'));
        //console.log('prijsMin: ' + sessionStorage.getItem('prijsMin'));
        //console.log('prijsMax: ' + sessionStorage.getItem('prijsMax'));
        //console.log('sort: ' + sessionStorage.getItem('sort'));
        //console.log('count: ' + sessionStorage.getItem('count'));
        //console.log('firstLoad: ' + sessionStorage.getItem('firstLoad'));
        //console.log('search by session ----------------------------------')

        var count = sessionStorage.getItem('count');
        var iCatIDs = sessionStorage.getItem('iCatIDs');
   
        pageIndex = sessionStorage.getItem('pageIndex');
        pageCount = sessionStorage.getItem('pageCount');


        var prijsMin = sessionStorage.getItem('prijsMin') //$("#slider").slider("values", 0);
        var prijsMax = sessionStorage.getItem('prijsMax') //$("#slider").slider("values", 1);

        $("#PriceMin").val("€" + prijsMin);
        $("#PriceMax").val("€" + prijsMax);

        var sort = sessionStorage.getItem('sort');
        AjaxRequest("POST", "/Data/Categorie.aspx?iCatIDs=" + iCatIDs + "&prijsMin=" + prijsMin + "&prijsMax=" + prijsMax + "&sort=" + sort + "&count=" + count + '&firstLoad=' + sessionStorage.getItem('firstLoad') + '&pageIndex=' + pageIndex, "", "", "html", 'resetFilterCallBackSearch');
       
    } else {
        //console.log('search without session')
        $("#PriceMin").val("€" + $("#slider").slider("values", 0));
        $("#PriceMax").val("€" + $("#slider").slider("values", 1));
        resetFilterTags(0);
        SaveScrollPosition();
    }
});

function openMobileMenu() {
    $(".CBList input").each(function (e) {
        if ($(this).is(':checked') === true) {
            $(this).parent().parent().parent().parent().parent().parent().addClass('active');
            $(this).parent().parent().parent().parent().parent().parent().addClass('full-height');
        }
    });
}


function resetFilterTags(iCatID) {
    $(".loading").addClass('activeloader');


    var HoofdCats = $('.CBOptions');
    var count = 0;
    for (i = 0; i < HoofdCats.length; i++) {
        if ($(HoofdCats[i]).find('input:checkbox:checked').length > 0) {
            count = count + 1;
        }
    }

    var iCatIDs = '';
    var sTags = '';
    $(".CBList input").each(function (e) {
        if ($(this).is(':checked') === true) { // als checkbox aangevinkt is
            if ($(this).val() != 'all') { // als het geen "Alle merken" is
                if (iCatID != $(this).val()) {
                    iCatIDs += $(this).val() + ','
                    //sTags += "<div class='active-filter' data-id='" + $(this).val() + "'>" + $(this).next('label').text() + "<span class='disable-filter'></span></div>"
                }
            }

        }
    });

    //$('.active-filters').html(sTags);
    if (iCatIDs.length > 0) {
        iCatIDs = iCatIDs.substring(0, iCatIDs.length - 1);
    }

    pageIndex = 1;
    sessionStorage.setItem('pageIndex', pageIndex);
    pageCount;
    sessionStorage.setItem('pageCount', pageCount);

   
    var prijsMin = $("#slider").slider("values", 0);
    var prijsMax = $("#slider").slider("values", 1);
    var sort = $('#SortOptions').val();
    AjaxRequest("POST", "/Data/Categorie.aspx?iCatIDs=" + iCatIDs + "&prijsMin=" + prijsMin + "&prijsMax=" + prijsMax + "&sort=" + sort + "&count=" + count + '&firstLoad=' + firstLoad, "", "", "html", 'resetFilterCallBack');
    if (firstLoad) {
        firstLoad = false;
    }
    saveSearchInCookie(iCatIDs, prijsMin, prijsMax, sort, count, firstLoad)
}


function resetFilterCallBackSearch() { //ALLEEN BIJ PAGE LOAD
    $("#categories").html(sDiv);
    $("#artikelen").html(sDivArtikelen);
    $(".JS-button").html(sDivButton);
    $(".loading").removeClass('activeloader');

    var sTags = '';
    $(".CBList input").each(function (e) {
        if ($(this).is(':checked') === true) { // als checkbox aangevinkt is
            sTags += "<div class='active-filter' data-id='" + $(this).val() + "'>" + $(this).next('label').text() + "<span class='disable-filter'></span></div>"
        }
    });
    $('.active-filters').html(sTags);
    openMobileMenu();
    //console.log('GO TO scrollPosition: ' + sessionStorage.getItem('scrollPosition'));
    //$('html, body').animate({
    //    scrollTop: 
    //}, 500);
    //setTimeout(function () { $(window).scrollTop(sessionStorage.getItem('scrollPosition')); }, 1000);

    if (sessionStorage.getItem('scrollPosition') != null) {
        console.log(sessionStorage.getItem('scrollPosition'));
        console.log($("#aLink" + sessionStorage.getItem('scrollPosition')));

        
        setTimeout(function () {
            var elementPosition = $("#aLink" + sessionStorage.getItem('scrollPosition')).offset().top;

            window.scrollTo(0, elementPosition);
    }, 1000);
        //$(window).scrollTop($("#aLink" + sessionStorage.getItem('scrollPosition')).offset().top); // - 70- 65
        // setTimeout(function () { }, 200);
    }
    
    setTimeout(function () { SaveScrollPosition(); }, 500);
}

function resetFilterCallBack() {
    $("#categories").html(sDiv);
    $("#artikelen").html(sDivArtikelen);
    $(".JS-button").html(sDivButton);
    $(".loading").removeClass('activeloader');

    var sTags = '';
    $(".CBList input").each(function (e) {
        if ($(this).is(':checked') === true) { // als checkbox aangevinkt is
            sTags += "<div class='active-filter' data-id='" + $(this).val() + "'>" + $(this).next('label').text() + "<span class='disable-filter'></span></div>"
        }
    });
    $('.active-filters').html(sTags);

    openMobileMenu();
}

function requestCategories() {
    $("#categories").html(sDiv);
    $("#artikelen").html(sDivArtikelen);
}

function saveSearchInCookie(iCatIDs, prijsMin, prijsMax, sort, count, firstLoad) {
    //scroll positie opslaan in object
    //var scrollPosition = Math.floor($(window).scrollTop());

    //sessionStorage.setItem('scrollPosition', scrollPosition);

    //variabelen opslaan in cookies object
    sessionStorage.setItem('iCatIDs', iCatIDs);
    sessionStorage.setItem('prijsMin', prijsMin);
    sessionStorage.setItem('prijsMax', prijsMax);
    sessionStorage.setItem('sort', sort);
    sessionStorage.setItem('count', count);
    sessionStorage.setItem('firstLoad', firstLoad);
    sessionStorage.setItem('search', 'true');


    //console.log('saveSearchInCookie ----------------------------------')
    //console.log('pageIndex: ' + sessionStorage.getItem('pageIndex'));
    //console.log('pageCount: ' + sessionStorage.getItem('pageCount'));
    //console.log('iCatIDs: ' + sessionStorage.getItem('iCatIDs'));
    //console.log('prijsMin: ' + sessionStorage.getItem('prijsMin'));
    //console.log('prijsMax: ' + sessionStorage.getItem('prijsMax'));
    //console.log('sort: ' + sessionStorage.getItem('sort'));
    //console.log('count: ' + sessionStorage.getItem('count'));
    //console.log('firstLoad: ' + sessionStorage.getItem('firstLoad'));
    //console.log('saveSearchInCookie ----------------------------------')
}

function SaveScrollPosition() {
    console.log('CHECK SCROLL');
    $(window).scroll(function () {
        var scrollPosition = Math.floor($(window).scrollTop());
        var total = Math.floor($(document).height() - $(window).height());
        if (scrollPosition >= total) {
            GetRecords();
        } else {
            if ((scrollPosition + 1) >= total) {
                GetRecords();
            }
        }
    });
}

//window.addEventListener('beforeunload', function(event) {
//    var scrollPosition = Math.floor($(window).scrollTop());
//    console.log(scrollPosition);
//    sessionStorage.setItem('scrollPosition', scrollPosition);
//});

function GetRecords() {
    pageIndex++;
    sessionStorage.setItem('pageIndex', pageIndex);
    //console.log('pageCount: ' + pageCount);
    console.log('GetRecords() pageIndex: ' + pageIndex);

    if (pageIndex == 2 || pageIndex <= pageCount) {
        $(".loading").addClass('activeloader');

        var iCatIDs = '';
        $(".CBList input").each(function (e) {
            if ($(this).is(':checked') === true) {
                iCatIDs += $(this).val() + ','
            }
        });
        var HoofdCats = $('.CBOptions');
        var count = 0;
        for (i = 0; i < HoofdCats.length; i++) {
            if ($(HoofdCats[i]).find('input:checkbox:checked').length > 0) {
                count = count + 1;
            }
        }


        if (iCatIDs.length > 0) {
            iCatIDs = iCatIDs.substring(0, iCatIDs.length - 1);
        }
        var prijsMin = $("#slider").slider("values", 0);
        var prijsMax = $("#slider").slider("values", 1);
        var sort = $('#SortOptions').val();
        $.ajax({
            type: "POST",
            url: "Data/Categorie.aspx/sArtikelen",
            data: '{pageIndex: ' + pageIndex + ', sCatIDs: "' + iCatIDs + '", iCount: ' + count + ', iPrijsMin: ' + prijsMin + ', iPrijsMax: ' + prijsMax + ', sort: ' + sort + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                console.log(response.d);
            },
            error: function (response) {
                console.log(response.d);
            }
        });
    }
}
function OnSuccess(response) {
    var xmlDoc = $.parseXML(response.d);
    var xml = $(xmlDoc);
    pageCount = parseInt(xml.find("PageCount").eq(0).find("PageCount").text());
    sessionStorage.setItem('pageCount', pageCount);
    var bOnline = xml.find("bIsOnline").eq(0).find("bIsOnline").text();
    var customers = xml.find("Artikelen");
    console.log(customers);
    customers.each(function () {
        var customer = $(this);
        var table = $("#artikelen .product").eq(0).clone(true);
        $(".name", table).html(customer.find("iArtikelID").text() + ' ' + customer.find("sArtikel").text());
        $("#imgThumb", table).attr('src', customer.find("sImage").text());
        if (customer.find("bNew").text() === 'nee') {
            $("#divNew", table).addClass('hidden')
        }
        console.log(customer.find("sURL").text().replace('~', ''));
        $(".JS-Link", table).attr('href', customer.find("sURL").text().replace('~', ''));
        if (bOnline) {
            $(".price", table).html('&euro;' + customer.find("iPrijsInkoop").text().replace('.', ','));
        } else {
            $(".price", table).html('');
        }
        //"#artikelen").appendTo(table);
        $(table).appendTo(".artikelenselector");
    });
    $(".loading").removeClass('activeloader');
}
