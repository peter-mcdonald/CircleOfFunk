var Registration = function (storage) {
    this.storage = storage;
    this.options = {
        onOpen: function (dialog) {
            dialog.overlay.fadeIn('slow', function () {
                dialog.container.slideDown('slow', function () {
                    dialog.data.fadeIn('slow');
                });
            });
        },
        onClose: function (dialog) {
            dialog.data.fadeOut('slow', function () {
                dialog.container.slideUp('slow', function () {
                    dialog.overlay.fadeOut('slow', function () {
                        $.modal.close();
                    });
                });
            });
        }
    };

};

Registration.prototype.Perform = function () {

    var url = "/Registration/register";

    if (this.storage.exists(url)) {
        this.ShowDialog(this.storage.get(url));
    }

    var that = this;

    $.get(url, function (data) {
        that.storage.set(url, data);
        that.ShowDialog(data);
    });

};


Registration.prototype.ShowDialog = function(view) {
    $.modal(view, this.options);

    Recaptcha.create("6LfLEfsSAAAAANc0vEvzRFg-OSOVKQ92fz6dJoy2",
        "regcaptcha",
        {
            theme: "blackglass",
            callback: Recaptcha.focus_response_field
        });

    $.validator.unobtrusive.parse($("#registerForm"));
};

Registration.prototype.CheckMessage = function(data) {
    if (data === "") {
        this.SayThankYou();
    } else {
        this.SetRegCaptchaMessage(data);
    }
};

Registration.prototype.ClearMessages = function() {
    $("#regCaptchaMessage").text('');
    $("#regThankYou").text('');
};

Registration.prototype.SayThankYou = function() {
    $("#regThankYou").text("Thanks for registering!");
};

Registration.prototype.SetRegCaptchaMessage = function(data) {
    $("#regCaptchaMessage").text(data);
};
