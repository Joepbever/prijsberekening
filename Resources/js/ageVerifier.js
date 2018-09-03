var minAge = 18;
var birthDay = "";
var birthMonth = "";
var birthYear = "";
var fullYear = "";
var birthYearEdited = false;
var today = new Date();
var theirDate;
var cookie;

$(document).ready(function () {
    cookie = document.cookie;

    console.log(cookie);

    if (cookie == "islegal=true") {
        GotoPage(true);
    }
    else if (cookie == "islegal=false") {
        GotoPage(false);
    }

    $("#btnConfirm").hide();

    $("#MainContent_txtDay").on('input', function () {
        if ($(this).val() > 99) {
            $(this).val(birthDay);
        }
        else {
            birthDay = $(this).val();
            CheckDate();
        }
    });

    $("#MainContent_txtMonth").on('input', function () {
        if ($(this).val() > 99) {
            $(this).val(birthMonth);
        }
        else {
            birthMonth = $(this).val();
            CheckDate();
        }
    });

    $("#MainContent_txtYear").on('input', function () {
        if ($(this).val() > 99) {
            $(this).val(birthYear);
        }
        else {
            birthYear = $(this).val();
            birthYearEdited = true;
            CheckDate();
        }
    });
});

function CheckDate() {
    if (birthDay != "" && birthMonth != "" && birthYear != "") {
        if (birthYearEdited) {
            if (birthYear > (today.getFullYear() - 2001)) {
                fullYear = parseInt("19" + birthYear);
            }
            else if (birthYear < (today.getFullYear() - 2000)) {
                if (birthYear < 10 && birthYear.toString().charAt(0) != "0") {
                    fullYear = parseInt("200" + birthYear);
                }
                else {
                    fullYear = parseInt("20" + birthYear);
                }
            }
            birthYearEdited = false;
        }
        theirDate = new Date(fullYear, birthMonth - 1, birthDay)

        if (theirDate.getMonth().valueOf() != birthMonth - 1 || birthMonth > 12 || birthDay > 31) {
            ChangeColor(false);
            $("#btnConfirm").hide();
        }
        else {
            ChangeColor(true);
            $("#btnConfirm").show();
        }
    }
    else {
        $("#btnConfirm").hide();
    }
}

function ChangeColor(correct) {
    if (correct) {
        // Terug naar originele kleur
        $("#lblAge").text("Correct ingevuld");
        $("#txtDay").css({ "background-color": "00ff00" });
        $("#txtMonth").css({ "background-color": "00ff00" });
        $("#txtYear").css({ "background-color": "00ff00" });
    }
    else {
        // Kleuren naar rood voor incorrect
        $("#lblAge").text("Incorrect ingevuld");
        $("#txtDay").css({ "background-color": "ff0000" });
        $("#txtMonth").css({ "background-color": "ff0000" });
        $("#txtYear").css({ "background-color": "ff0000" });
    }
}

function VerifyAge() {
    today.setFullYear(today.getFullYear() - minAge);

    if (today.getTime() - theirDate.getTime() > 0) {
        document.cookie = "islegal=true";
        GotoPage(true);
    }
    else {
        document.cookie = "islegal=false";
        GotoPage(false);
    }
}

function GotoPage(islegal) {
    if (islegal) {
        // Moet doorgaan naar goede pagina
        console.log("legal");
    }
    else {
        // Moet een pagina laten zien dat de gebruiker niet meer verder mag
        console.log("illegal")
    }
}