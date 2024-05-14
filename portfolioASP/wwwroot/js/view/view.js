$(function () {
    updateNavbarClass();

    $(window).on('scroll', function () {
        updateNavbarClass();
    });

    $(window).on('load', function () {
        updateNavbarClass();
    });

    function updateNavbarClass() {
        var scrollTop = $(this).scrollTop();
        var navbar = $('.navbar');

        if (scrollTop > 150) {
            navbar.addClass('navbar-solid');
        } else {
            navbar.removeClass('navbar-solid');
        }
    }
});
