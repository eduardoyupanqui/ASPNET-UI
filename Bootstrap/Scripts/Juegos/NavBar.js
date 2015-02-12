$(document).ready(function () {
    $(".nav li").removeClass("active");

    var url = window.location.pathname;
    var substr = url.split('/');
    var urlasp = substr[substr.length - 1];

    $(".nav li a").each(function () {
        if ($(this).text().toLowerCase() == urlasp.toLowerCase())
            $(this).parent().addClass('active');
    });
});