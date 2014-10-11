
var spinner = new Spinner({
    lines: 10, // The number of lines to draw
    length: 20, // The length of each line
    width: 7, // The line thickness
    radius: 30, // The radius of the inner circle
    color: '#ffffff', // #rbg or #rrggbb
    speed: 1, // Rounds per second
    trail: 60, // Afterglow percentage
    shadow: true // Whether to render a shadow
});

$(function () {
    var selectedPage = $('#slideId').val();
    setTimeout(showPage(selectedPage), 0);
});

function showPage(currentPage) {

    switch (currentPage) {
        case "1":
            HomePage();
            break;
            
        case "2":
            SlappedUpPage();
            break;
            
        case "3":
            DiscographyPage();
            break;

        case "4":
            SocialPage();
            break;

        case "6":
            BiographyPage();
            break;
            
        case "7":
            AudioPage();
            break;
    }
}

function SetUpAccordion(currentPage) {
    var options = {
        containerWidth: 870,
        containerHeight: 180,
        theme: 'dark',
        firstSlide: currentPage,
        rounded: true,
        onSlideAnimComplete: function () {
            $('#slideId').val(this.closest("li").attr("id"));
        },

        easing: 'easeInOutQuart'
    };

    $('#accordianmenu').liteAccordion(options);
}

function HomePage() {

    StartSpinner("news");

    var tickerOpts = {
        direction: 'up',
        easing: 'easeInOutBack',
        speed: 'slow',
        interval: 3000,
        height: 450,
        visible: 3,
        mousePause: true,
    };

    $.post("/home/GetNewsItems", function (data) {
        StopSpinner();
        $("#newsList").append(data);
        $('.vticker').easyTicker(tickerOpts);
    });
}

function SlappedUpPage() {
    StartSpinner("theLabel");

    $.post("/SlappedUpSoul/GetSlappedUpSoul", function (data) {
        StopSpinner();
        $("#theLabel").append(data);
    });
}


function DiscographyPage() {

    StartSpinner("discog");

    $.post("/discography/GetDiscography", function (data) {
        StopSpinner();
        $("#discog").append(data);
    });
}

function SocialPage() {
    window.fbAsyncInit = function () {
        FB.init({
            appId: '936539446373269',
            xfbml: true,
            version: 'v2.0'
        });
        prepareFamax();
    };

    TwitterFollow(document, 'script', 'twitter-wjs');
}

function TwitterFollow(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https';
    if (!d.getElementById(id)) {
        js = d.createElement(s);
        js.id = id;
        js.src = p + '://platform.twitter.com/widgets.js';
        fjs.parentNode.insertBefore(js, fjs);
    }
}

function BiographyPage() {
    StartSpinner("biowrapper");

    $.post("/biography/GetBiography", function (data) {
        StopSpinner();
        $("#biowrapper").append(data);
    });
}

function AudioPage() {

    StartSpinner("soundcloud");

    $.ajax({
        url: "http://api.soundcloud.com/users/285835/tracks.json?client_id=2d7cc150f9e5813c0d93236b3312654c",
        async: true,
        cache: true,
        success: function(data) {
            processTracks(data);
            StopSpinner();
        },
        dataType: 'json',
        beforeSend: setHeader
    });
}

function processTracks(tracks) {
    var frame = '<iframe class="center iframe" src="https://w.soundcloud.com/player/?url=#URL#&amp;auto_play=false&amp;hide_related=false&amp;show_comments=true&amp;show_user=true&amp;show_reposts=false&amp;visual=true"></iframe>';

    for (var i = 0; i < tracks.length; i++) {
        if (tracks[i].embeddable_by === "all") {
            $('#soundcloud').append(frame.replace('#URL', tracks[i].uri));
        }
    }
}

function setHeader(xhr) {
    if (xhr && xhr.overrideMimeType) {
        xhr.overrideMimeType("application/j-son;charset=UTF-8");
    }
}

function OpenLink(linkid) {
    var link = $('#' + linkid);
    var target = link.attr("target");

    if ($.trim(target).length === 0) {
        window.location = link.attr("href");
    }
}

function SetContactData() {
    $("#contactdata").hide();
    console.log("hide form");
    SetCaptchaMessage("");
    StartSpinner("contactus");
    return true;
}

function ThankYouMessage() {
    $('#sentMessage').slideDown('slow', function() {
        setTimeout(function() {
            $('#sentMessage').slideUp('slow');
        },5000);
    });
}


function CheckCaptchaMessage(data) {
    StopSpinner();
    if (data === "") {
        ClearForm();
        ThankYouMessage();
    } else {
        SetCaptchaMessage(data);
    }
    console.log("show form");
    $("#contactdata").fadeIn('fast');
}

function ClearForm() {
    $("#contactForm").find('input:text, input:password, input:file, select, textarea').val('');
    $("#contactForm").find('input:radio, input:checkbox').removeAttr('checked').removeAttr('selected');
    $("#Phone").val('');
    $('#Email').val('');
    Recaptcha.reload();
}

function SetCaptchaMessage(message) {
    $('#captchaMessage').text(message);
}

function StartSpinner(target) {
    spinner.spin(document.getElementById(target));
}

function StopSpinner() {
    spinner.stop();
}