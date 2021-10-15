$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
});

let form = $("#mainForm");
let validator = form.validate({
    //ignore: [],
    rules: {
        //Rules
    },
    messages: {
        //messages
    },
    errorPlacement: function (label, element) {
        const select2 = $(element).parent().siblings('span').find('.select2-selection');
        if (select2 && select2.length == 1) {
            select2.addClass('error');
        }
        label.insertAfter(element);
    },
    wrapper: 'span'
});


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
                    _.forEach(x.memberNames, function (y) {
                        error['Input.' + y] = x.message;
                        validator.showErrors(error);
                    });
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
    //let sub_ul = $('<ul/>');
    //$.each(errors, function (index, object) {
    //    const fieldName = $($("label[for='Input_" + object.memberNames[0] + "']")[0]).text()
    //    let sub_li = $('<li>').text(fieldName + ': ' + object.message);
    //    sub_ul.append(sub_li);
    //});//.append(sub_ul);
    $('#validationErrors').empty().append('<h6>' + l('Validation:SeeErrors') + '</h6>');
    $('#validationSummary').show();
}