//function videoControl() {
//    var myVideo = document.getElementById('myVideo');
//    if (myVideo.paused) {
//        myVideo.play();
//    } else {
//        myVideo.pause();
//    }
//}

//$(document).ready(function () {
//    $(".overlay").click(function () {
//        $(".playbutton").fadeToggle("hide");
//        $(".text-video").fadeToggle("hide");
//        $(".widget-video .overlay").toggleClass("none");
//    });

//    $(".playbutton").click(function () {
//        $(".playbutton").fadeToggle("hide");
//        $(".text-video").fadeToggle("hide");
//        $(".widget-video .overlay").toggleClass("none");
//    });

//    if ($(window).width() > 1024) {
//        var videos = document.getElementById("myVideo"),
//            fraction = 0.8;
//        function checkScroll() {

//            for (var i = 0; i < videos.length; i++) {

//                var video = videos[i];

//                var x = video.offsetLeft, y = video.offsetTop, w = video.offsetWidth, h = video.offsetHeight, r = x + w, //right
//                    b = y + h, //bottom
//                    visibleX, visibleY, visible;

//                visibleX = Math.max(0, Math.min(w, window.pageXOffset + window.innerWidth - x, r - window.pageXOffset));
//                visibleY = Math.max(0, Math.min(h, window.pageYOffset + window.innerHeight - y, b - window.pageYOffset));

//                visible = visibleX * visibleY / (w * h);

//                if (visible > fraction) {
//                    video.play();
//                } else {
//                    video.pause();
//                }
//            }
//        }

//        window.addEventListener('scroll', checkScroll, false);
//        window.addEventListener('resize', checkScroll, false);
//    }
//});


