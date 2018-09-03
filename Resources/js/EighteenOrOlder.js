$(document).ready(function () {
    var dateToday = new Date();
    var dateEntered = new Date('2036-04-03');

    if (dateEntered >= dateToday) {
        var todayPlusEighteen = new Date(dateToday.getFullYear() + 18, dateToday.getMonth(), dateToday.getDate());
        if (dateEntered > todayPlusEighteen) {
            console.log("Date today is bigger than 18")
        }
    }
});