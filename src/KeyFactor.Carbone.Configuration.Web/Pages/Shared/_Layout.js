$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
});

function submitForm() {
    let form = $("#mainForm");
    let isValid = form.valid();
    if (!isValid) {
        return;
    }
    abp.ui.block({
        busy: true
    });
    $.ajax({
        type: "POST",
        url: '',
        data: $('#mainForm').serialize(),
        success: function (data) {
            abp.ui.unblock();
            if (!data.success) {
                _.forEach(data.errors, function (x) {
                    let validator = $("#mainForm").validate();
                    let error = {};
                    error[x.key] = x.details[0];
                    validator.showErrors(error);
                });
            } else {
                $("#mainForm").find("label.error").remove();
                $("#mainForm").find(".error").removeClass("error");
                success();
            }
        }
    });
};

function success() {
};