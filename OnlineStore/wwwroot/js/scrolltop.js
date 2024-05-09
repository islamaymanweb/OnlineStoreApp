﻿//back to top
if ($("#back-to-top").length) {
    let scrollTrigger = 100, //px
        backToTop = function () {
            var scrollTop = $(window).scrollTop();
            if (scrollTop > scrollTrigger) {
                $('#back-to-top').addClass('show');
            } else {
                $('#back-to-top').removeClass('show');
            }
        };
    backToTop();
    $(window).on('scroll', function () {
        backToTop();
    });
    $("#back-to-top").on('click', function (e) {
        e.preventDefault();
        $('html, body').animate({
            scrollTo: -100,
        }, 900);
    });
}
