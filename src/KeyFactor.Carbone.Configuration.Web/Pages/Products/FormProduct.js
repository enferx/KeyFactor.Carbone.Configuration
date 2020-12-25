$(document).ready(function () {

    if ($("#Input_ValidFromDate").val() == "") {
        $("#Input_ValidFromDate").val($("#ValidFromDateHidden").val());
    }
    if ($("#Input_ValidToDate").val() == "") {
        $("#Input_ValidToDate").val($("#ValidToDateHidden").val());
    }

    $('#mainForm').submit(function (event) {
        $("#ValidFromDateHidden").val($("#Input_ValidFromDate").val());
        $("#ValidToDateHidden").val($("#Input_ValidToDate").val());
        
    });
    
});

