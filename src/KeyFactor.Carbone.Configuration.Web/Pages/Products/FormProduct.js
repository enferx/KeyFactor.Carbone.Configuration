$(document).ready(function () {
    $("#Input_UnitId_Select").rules("add", {
        required: true,
        messages: {
            required: "Required input"
        }
    });
    if ($("#Input_ValidFromDate").val() == "") {
        $("#Input_ValidFromDate").val($("#ValidFromDateHidden").val());
    }
    if ($("#Input_ValidToDate").val() == "") {
        $("#Input_ValidToDate").val($("#ValidToDateHidden").val());
    }

    $('#Input_UnitId_Select').select2({
        ajax: {
            url: '/Products/CreateProduct?handler=Search',
            dataType: 'json',
            processResults: function (data) {
                return {
                    results: data.items
                };
            }
        }
    });
    $('#Input_UnitId_Select').on('select2:select', function (e) {
        $("#Input_UnitId").val(e.params.data.id);
    });
    
    $('#mainForm').submit(function (event) {
        $("#ValidFromDateHidden").val($("#Input_ValidFromDate").val());
        $("#ValidToDateHidden").val($("#Input_ValidToDate").val());
        
    });


    // If is an update... set controls.
    if ($("#Id").val() != undefined) {
        keyFactor.carbone.configuration.units.unit.get($("#Input_UnitId").val())
            .then(function (data) {
                var selectedOption = new Option(data.name, data.id, false, false);
                $('#Input_UnitId_Select').append(selectedOption).trigger('change');
            });
    } else {
        $("#Input_UnitId").val("");
    }
});

