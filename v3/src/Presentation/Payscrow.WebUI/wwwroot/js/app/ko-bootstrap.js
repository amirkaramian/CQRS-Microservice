ko.validation.init({
    decorateInputElement: true,
    errorElementClass: 'is-invalid',
    errorMessageClass: 'invalid-feedback',
    //errorElementClass: 'm-form--state',
    insertMessages: true,
    grouping: { deep: true, observable: true, live: true }
});

ko.validation.registerExtenders();

/* Set up block-ui */
$.blockUI.defaults.message = $("#custom-loader");
$.blockUI.defaults.css.border = 'none';
$.blockUI.defaults.css.background = 'none';
$.blockUI.defaults.baseZ = 1051;

$(document).ajaxSend(function (event, request, settings) {
    ko.postbox.publish("isLoading", true);

});

$(document).ajaxStop(function (event, request, settings) {
    ko.postbox.publish("isLoading", false);
});

$.ajaxSetup({
    statusCode: {
        401: function (jqxhr, textStatus, errorThrown) {
            window.location.reload();
        }
    }
})