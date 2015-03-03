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
var that;
var storage;
var registration;
var historyJs;

var arr = {
    "news": "Home",
    "slappedup": "Slapped Up Soul",
    "discography": "Discography",
    "biography": "Biography",
    "social": "Social",
    "soundcloud": "Audio",
    "traxsource": "Traxsource",
    "contact": "Contact Us",
    "links": "Links"
};

pages = {
    change: function (newPage) {
        this.setAddress(arr[newPage], newPage);
    },
    home: function () {
        this.setAddress("Welcome", "news");
    },
    replacewithhome: function () {
        historyJs.replaceState(null, "Home", "?page=news");
    },
    setAddress: function (text, page) {
        historyJs.pushState(null, text, "?page=" + page);
    },
    exist: function(page) {
        return arr.hasOwnProperty(page);
    }
};

function initialiseSite() {

    storage = new Cache();
    registration = new Registration(storage);

    window.fbAsyncInit = function () {
        FB.init({
            appId: '936539446373269',
            xfbml: true,
            version: 'v2.0'
        });
    };
    
    TwitterFollow(document, 'script', 'twitter-wjs');
    facebook(document, 'script', 'facebook-jssdk');

    $('.registerBtn').click(function() {
        DoRegistration();
    });


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
            pages.change(closeli.data("page"));
            this.find('figcaption').fadeIn('fast');
            that = this;
            setTimeout(function() {
                that.find('figcaption').fadeOut('slow');
            }, 1500);
        },
        easing: 'easeInOutQuart'
    };

    $('#accordianmenu').liteAccordion(options);
    $('#accordianmenu').fadeIn('slow');
    initialiseHistory();
    setFirstPage();
}

function initialiseHistory() {
    historyJs = window.History;

    historyJs.Adapter.bind(window, 'statechange', function () {
        showPage(getPageFromUrl(historyJs.getState().url));
    });
}

function setFirstPage() {
    var state = historyJs.getState();

    if (state.url.indexOf("?") === -1) {
        pages.home();
    } else {
        var page = getPageFromUrl(historyJs.getState().url);

        if (pages.exist(page)) {
            showPage(page);
        } else {
            pages.replacewithhome();
        }
        
    }
}

function getPageFromUrl(url) {
    var parts = parseUri(url);
    return parts.queryKey.page;
}

function showPage(currentPage) {

    setLastPage(currentPage);
    
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

        case "traxsource":
            $('#accordianmenu').liteAccordion('show(4)');
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
            
        case "links":
            linksPage();
            break;

        default:
            pages.replacewithhome();
    }
}

function setLastPage(currentPage) {

    if (lastPage === "news") {
        $('.vticker').removeData();
    }

    if (lastPage === "social") {
        $("#content").removeAttr("style");
    }

    lastPage = currentPage;
}

function HomePage() {

    $('#accordianmenu').liteAccordion('show(0)');
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

    $.get("/home/GetNewsItems", function (data) {
        stopSpinner();
        $("#content").append(data);
        $('.vticker').easyTicker(tickerOpts);
        $("#newslist").fadeIn("fast")
    });
}

function SlappedUpPage() {
    $('#accordianmenu').liteAccordion('show(1)');
    setPageTitle("Slapped Up Soul");
    startSpinner();
    setContentClasses("border scrollable float-left");

    $.get("/SlappedUpSoul/GetSlappedUpSoul", function (data) {
        stopSpinner();
        $("#content").append(data);
    });
}

function DiscographyPage() {
    $('#accordianmenu').liteAccordion('show(2)');
    setPageTitle("Discography");
    setContentClasses("border float-left scrollable");
    startSpinner();
    $.get("/discography/GetDiscography", function (data) {
        stopSpinner();
        appendData(data);
    });
}

function SocialPage() {
    $('#accordianmenu').liteAccordion('show(3)');
    setPageTitle("Social Networks");
    $("#content").css("height", "595px");
    startSpinner();
    
    setContentClasses("border float-left");

    $.gett("/Social/GetSocialView", function (data) {
        stopSpinner();
        appendData(data);
        twttr.widgets.load();
        FB.XFBML.parse();
        prepareFamax();
    });
}

function BiographyPage() {
    $('#accordianmenu').liteAccordion('show(5)');
    setPageTitle("Biography");
    setContentClasses("border float-left scrollable");
    startSpinner();

    $.get("/biography/GetBiography", function (data) {
        stopSpinner();
        appendData(data);
    });
}

function AudioPage() {
    $('#accordianmenu').liteAccordion('show(6)');
    setPageTitle("Soundclound");
    startSpinner();
    setContentClasses("border scrollable float-left");

    var sc = new SoundCloud(storage);
    sc.getData("285835", "#content");
}

function contactPage() {
    $('#accordianmenu').liteAccordion('show(7)');
    setPageTitle("Contact Us");
    startSpinner();
    setContentClasses("border float-left");

    $.get("/Contact/GetContactView", function(data) {
        stopSpinner();
        appendData(data);
        setCaptcha();
        $.validator.unobtrusive.parse($("#contactForm"));
    });
}

function linksPage() {
    $('#accordianmenu').liteAccordion('show(8)');
    setPageTitle("Links");
    setContentClasses("border float-left scrollable");
    startSpinner();

    $.get("/Links/GetLinks", function (data) {
        stopSpinner();
        appendData(data);
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

function facebook(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
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

function DoRegistration() {
    registration.Perform();
}

function CheckRegistrationMessage(data) {
    registration.CheckMessage(data);
}

function ClearRegMessages() {
    registration.ClearMessages();
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