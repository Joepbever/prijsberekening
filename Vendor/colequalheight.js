$(document).ready(function () {

    if ($(window).width() > 1024) {
        function resizeCols() {
            var autoheightParentSel = '.autoHeightContainer';
            var autoHeightChildSel = '.autoHeightChild';

            $(autoheightParentSel).each(function (i, item) {
                // remove old class
                $(item).find(autoHeightChildSel).css('height', '');
                var maxHeight = 0;
                $(item).find(autoHeightChildSel).each(function (ii, child) {
                    maxHeight = Math.max(maxHeight, $(child).outerHeight(true))
                });
                $(item).find(autoHeightChildSel).css('height', maxHeight + 'px');
            });
        }

        if ($('.autoHeightContainer').length > 0 && $('.autoHeightChild').length > 0) {
            // different refereshing techniques
            resizeCols();
            setTimeout(resizeCols, 0);
            setTimeout(resizeCols, 100);
            $(window).on('resize', resizeCols);
            $(window).on('load', resizeCols);
            // again after optional animations
            $(window).on('resize', function () { setTimeout(resizeCols, 150); setTimeout(resizeCols, 500) });
            $(window).on('load', function () { setTimeout(resizeCols, 150); setTimeout(resizeCols, 500) });
        }
    }
});
