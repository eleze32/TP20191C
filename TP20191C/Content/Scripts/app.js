$(window).resize(function () {
    pieAbajo();
});

$(document).ready(function () {

    pieAbajo();
});

function pieAbajo() {
    if ($("body").height() < $(window).height()) {
        $("footer").css({
            "position": "absolute",
            "bottom": "0px"
        });
    } else {
        $("footer").css({
            "position": "unset",
            "bottom": "unset"
        });
    }
}