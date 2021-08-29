function getErrorMsgs(formId) {

    $(document).ready(function () {
        var form = $(formId)
            , formData = $.data(form[0])
            , settings = formData.validator.settings
            , oldErrorPlacement = settings.errorPlacement

        settings.errorPlacement = function (label, element) {
            oldErrorPlacement(label, element);
            label.addClass('alert alert-danger');
        };
    });
}