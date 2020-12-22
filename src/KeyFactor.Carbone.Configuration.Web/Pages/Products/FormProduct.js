$(document).ready(function () {

    if ($("#ValidFromDate").val() == "") {
        $("#ValidFromDate").val($("#ValidFromDateHidden").val());
    }
    if ($("#ValidToDate").val() == "") {
        $("#ValidToDate").val($("#ValidToDateHidden").val());
    }

    $('#mainForm').submit(function (event) {
        $("#ValidFromDateHidden").val($("#ValidFromDate").val());
        $("#ValidToDateHidden").val($("#ValidToDate").val());
        
    });
    
});

