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

    // Dodanie przewijania z uwzględnieniem wysokości navbaru
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();

            const targetId = this.getAttribute('href').substring(1);
            const target = document.getElementById(targetId);
            const navbarHeight = 75;

            let scrollHeight = 0;
            if (targetId !== '') {
                scrollHeight = target.offsetTop - navbarHeight;
            }

            const scrollTo = scrollHeight;
            window.scrollTo({
                top: scrollTo,
                behavior: 'smooth'
            });
        });
    });
});
