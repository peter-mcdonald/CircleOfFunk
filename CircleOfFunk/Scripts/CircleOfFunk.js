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

var lastPage = "";

function initialiseSite() {

    var options = {
        containerWidth: 870,
        containerHeight: 180,
        activateOn: 'click',
        theme: 'dark',
        firstSlide: 1,
        rounded: true,
        onTriggerSlide: function () {
            this.find('figcaption').fadeOut();
        },
        onSlideAnimComplete: function () {
            var closeli = this.closest("li");
            showPage(closeli.data("page"));
            this.find('figcaption').fadeIn('fast');
            that = this;
            setTimeout(function() {
                that.find('figcaption').fadeOut('slow');
            }, 1500);
        },
        easing: 'easeInOutQuart'
    };

    $('#accordianmenu').liteAccordion(options);
    TwitterFollow(document, 'script', 'twitter-wjs');

    window.fbAsyncInit = function () {
        FB.init({
            appId: '936539446373269',
            xfbml: true,
            version: 'v2.0'
        });
    };
    
    showPage("news");
}

function showPage(currentPage) {

    // Need to get the plugin to do this at some stage
    if (lastPage === "news") {
        $('.vticker').removeData();
    }
    
    if (lastPage === "social") {
        $("#content").removeAttr("style");
    }

    lastPage = currentPage;
    
    switch (currentPage) {
        case "news":
            HomePage();
            break;
            
        case "slappedup":
            SlappedUpPage();
            break;
            
        case "discography":
            DiscographyPage();
            break;

        case "social":
            SocialPage();
            break;

        case "biography":
            BiographyPage();
            break;
            
        case "soundcloud":
            AudioPage();
            break;
            
        case "contact":
            contactPage();
            break;
    }
}

function HomePage() {

    setPageTitle("Latest News");
    setContentClasses("border vticker float-left");
    startSpinner();

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
        stopSpinner();
        $("#content").append(data);
        $('.vticker').easyTicker(tickerOpts);
    });
}

function SlappedUpPage() {
    setPageTitle("Slapped Up Soul");
    startSpinner();
    setContentClasses("border scrollable float-left");

    $.post("/SlappedUpSoul/GetSlappedUpSoul", function (data) {
        stopSpinner();
        $("#content").append(data);
    });
}

function DiscographyPage() {
    setPageTitle("Discography");
    setContentClasses("border float-left scrollable");
    startSpinner();
    $.post("/discography/GetDiscography", function (data) {
        stopSpinner();
        appendData(data);
    });
}

function SocialPage() {
    setPageTitle("Social Networks");
    $("#content").css("height", "595px");
    startSpinner();
    
    setContentClasses("border float-left");

    $.post("/Social/GetSocialView", function (data) {
        stopSpinner();
        appendData(data);
        twttr.widgets.load();
        prepareFamax();
    });
}

function BiographyPage() {
    setPageTitle("Biography");
    setContentClasses("border float-left scrollable");
    startSpinner("biowrapper");

    $.post("/biography/GetBiography", function (data) {
        stopSpinner();
        appendData(data);
    });
}

function AudioPage() {
    setPageTitle("Soundclound");
    startSpinner();
    setContentClasses("border scrollable float-left");
    $.ajax({
        url: "http://api.soundcloud.com/users/285835/tracks.json?client_id=2d7cc150f9e5813c0d93236b3312654c",
        async: true,
        cache: true,
        success: function(data) {
            processTracks(data);
            stopSpinner();
        },
        dataType: 'json',
        beforeSend: setHeader
    });
}

function processTracks(tracks) {
    var frame = '<iframe class="center iframe" src="https://w.soundcloud.com/player/?url=#URL#&amp;auto_play=false&amp;hide_related=false&amp;show_comments=true&amp;show_user=true&amp;show_reposts=false&amp;visual=true"></iframe>';

    for (var i = 0; i < tracks.length; i++) {
        if (tracks[i].embeddable_by === "all") {
            $('#content').append(frame.replace('#URL', tracks[i].uri));
        }
    }
}

function setHeader(xhr) {
    if (xhr && xhr.overrideMimeType) {
        xhr.overrideMimeType("application/j-son;charset=UTF-8");
    }
}

function contactPage() {
    setPageTitle("Contact Us");
    startSpinner();
    setContentClasses("border float-left");

    $.post("/Contact/GetContactView", function(data) {
        stopSpinner();
        appendData(data);
        setCaptcha();
        // set up validator
        $.validator.unobtrusive.parse($("#contactForm"));
    });
}

function setPageTitle(title) {
    $("#pageTitle").text(title);
}

function setCaptcha() {
    Recaptcha.create("6LfLEfsSAAAAANc0vEvzRFg-OSOVKQ92fz6dJoy2",
        "recaptcha",
        {
            theme: "blackglass",
            callback: Recaptcha.focus_response_field
        }
    );
}

function SetContactData() {
    $("#contactdata").hide();
    setCaptchaMessage("");
    //startSpinner();
    spinner.spin(document.getElementById("content"));
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
    stopSpinner();
    if (data === "") {
        ClearForm();
        ThankYouMessage();
    } else {
        setCaptchaMessage(data);
    }
    $("#contactdata").fadeIn('fast');
}

function ClearForm() {
    $("#contactForm").find('input:text, input:password, input:file, select, textarea').val('');
    $("#contactForm").find('input:radio, input:checkbox').removeAttr('checked').removeAttr('selected');
    $("#Phone").val('');
    $('#Email').val('');
    Recaptcha.reload();
}

function setCaptchaMessage(message) {
    $('#captchaMessage').text(message);
}

function setContentClasses(classes) {
    $("#content").attr("class", classes);
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

function appendData(data) {
    $("#content").append(data);
}

function startSpinner() {
    $("#content").empty();
    spinner.spin(document.getElementById("content"));
}

function stopSpinner() {
    spinner.stop();
}