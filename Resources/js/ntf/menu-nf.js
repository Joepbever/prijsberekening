$(document).ready(function () {

    /* ==========================================================================
       Options
       ========================================================================== */
    speed = 0; // Set animation speed through the letters here
    $wrapper = $(".menu a"); //Wrapper around letters
    spanClass = "sl";
    animationClass = "active";
    cubicClass = "wrap ";

    /* ==========================================================================
       Spaner
       ========================================================================== */
    (function ($) {
        $.fn.spanLetters = function () {

            // Loop through each element on which this function has been called
            this.each(function () {

                // Scope the variables
                var words, i, text;

                // Make an array with each letter of the string as a value
                words = $(this).text().split('');
                // Loop through the letters and wrap each one in a span
                for (i = 0; i in words; i++) {
                    words[i] = '<div class="' + cubicClass + spanClass + $(this).text() + '"><span class="span-letter">' + words[i] + '</span></div>'
                };
                // Join our array of span-wrapped letters back into a string
                text = words.join('');
                // Replace the original string with the new string
                $(this).html(text);
            })
        }
    }(jQuery));

    /* ==========================================================================
       Init Spaner
       ========================================================================== */

    $wrapper.spanLetters();


    /* ==========================================================================
       Animation Logic
       ========================================================================== */


    // Add the animation class to the letter, then remove it later
    function animateLetter(value) {

        if (turnedOn) {

            $(value).addClass(animationClass);
        }

    };

    // Remove the animation class from the letter
    function stopAnimateLetter(value) {

        $(value).removeClass(animationClass);

    };

    // Trigger function
    function startEverything(text) {

        // Loop through spans in specific container
        $("." + spanClass + text).each(function (index, value) {

            // Sequentially delay the firing of the animation through the letters
            timer = setTimeout(function () {

                animateLetter(value);

            }, speed * index);

        });

    }

    // Kill everything function
    function stopEverything(text) {
        turnedOn = false;
        // Delay the kill just a bit
        $("." + spanClass + text).each(function (index, value) {
            stopAnimateLetter(value);
        });
    }

    // Hover trigger logic
    $wrapper
        .mouseenter(function () { //PC
            turnedOn = true;
            clicked = false;

            startEverything($(this).text());

        })
        .mouseleave(function () {// PC

            stopEverything($(this).text());

        });

})
