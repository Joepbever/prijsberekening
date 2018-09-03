$(document).ready(function () {
    // MM/DD/YYYY HH:MM:SS
    var deadline = '02/16/2018 18:00:00';
    initializeClock(deadline);
});

function getTimeRemaining(endtime) {
    var t = Date.parse(endtime) - Date.parse(new Date());
    var seconds = Math.floor((t / 1000) % 60);
    var minutes = Math.floor((t / 1000 / 60) % 60);
    var hours = Math.floor((t / (1000 * 60 * 60)) % 24);
    var days = Math.floor(t / (1000 * 60 * 60 * 24));

    return {
    'total': t,
    'days': days,
    'hours': hours,
    'minutes': minutes,
    'seconds': seconds
    };
}

function initializeClock(endtime) {
    var daysSpan = $("#lblDays");
    var daysSpan2 = $("#lblDays2");
    var hoursSpan = $("#lblHours");
    var hoursSpan2 = $("#lblHours2");
    var minutesSpan = $("#lblMinutes");
    var minutesSpan2 = $("#lblMinutes2");
    var secondsSpan = $("#lblSeconds");
    var secondsSpan2 = $("#lblSeconds2");

    function updateClock() {
        var t = getTimeRemaining(endtime);

        var d = t.days.toString();
        daysSpan.text(d[0]);
        daysSpan2.text(d[1]);

        var h = ('0' + t.hours).slice(-2).toString()
        hoursSpan.text(h[0]);
        hoursSpan2.text(h[1]);

        var m = ('0' + t.minutes).slice(-2).toString()
        minutesSpan.text(m[0]);
        minutesSpan2.text(m[1]);

        var s = ('0' + t.seconds).slice(-2).toString();
        secondsSpan.text(s[0]);
        secondsSpan2.text(s[1]);

        if (t.total <= 0) {
            clearInterval(timeinterval);
        }
    }

    updateClock();
    var timeinterval = setInterval(updateClock, 1000);
}

