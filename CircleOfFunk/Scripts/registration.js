var Registration = function () {
};

Registration.prototype.Perform = function () {

    var options = {
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
    
    //$("#element-id").modal({
    //    onClose: function (dialog) {
    //        dialog.data.fadeOut('slow', function () {
    //            dialog.container.slideUp('slow', function () {
    //                dialog.overlay.fadeOut('slow', function () {
    //                    $.modal.close(); // must call this!
    //                });
    //            });
    //        });
    //    }
    //});

    $('#basic-modal-content').modal(options);




};