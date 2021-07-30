$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
});

let form = $("#mainForm");
let validator = form.validate();

function submitForm() {
    let isValid = form.valid();
    if (!isValid) {
        $("#validationSummary").show();
    } else {
        $("#validationSummary").hide();
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
                    let error = {};
                    error['Input.'+x.memberNames[0]] = x.message;
                    validator.showErrors(error);
                });
                error(data.errors);
            } else {
                success();
            }
        }
    });
};

function success() {
};

function error(errors) {
    let l = abp.localization.getResource('Configuration');
    let sub_ul = $('<ul/>');
    $.each(errors, function (index, object) {
        const fieldName = $($("label[for='Input_" + object.memberNames[0] + "']")[0]).text()
        let sub_li = $('<li>').text(fieldName + ': ' + object.message);
        sub_ul.append(sub_li);
    });
    $('#validationErrors').empty().append('<h6>' + l('Validation:SeeErrors') +'</h6>').append(sub_ul);
    $('#validationSummary').show();
}