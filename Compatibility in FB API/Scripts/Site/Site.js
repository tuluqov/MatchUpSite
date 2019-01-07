var halfText = $('.spoiler').innerHeight() / 4,
    textHeight = $('.spoiler').innerHeight();

$('.spoiler').css('height', $('.spoiler').innerHeight() / 4);

$('.show-hide').click(function () {
    if ($('.spoiler').innerHeight() == halfText) {
        $('.spoiler').animate({ height: textHeight }, 300);
        $(this).text('Hide');
    } else {
        $('.spoiler').animate({ height: halfText }, 300);
        $(this).text('More');
    }
});