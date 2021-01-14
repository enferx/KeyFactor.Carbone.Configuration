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
        loadProperties();
    } else {
        $("#Input_UnitId").val("");
    }

    function loadProperties() {
        //var properties = @properties;
        var l = abp.localization.getResource('Configuration');

        var dataTable = $('#PropertiesTable').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                serverSide: false,
                paging: true,
                searching: false,
                scrollX: true,
                data: properties,
                columnDefs: [
                    {
                        title: "Actions",
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        visible: abp.auth.isGranted('Configuration.Products.Edit'),
                                        action: function (data) {
                                            window.location.href = '/Products/EditProductProperty?Id=' + data.record.id
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        visible: abp.auth.isGranted('Configuration.Products.Delete'),
                                        confirmMessage: function (data) {
                                            return l('AreYouSureToDelete', data.record.name);
                                        },
                                        action: function (data) {
                                            keyFactor.carbone.configuration
                                                .products.product
                                                .delete(data.record.id)
                                                .then(function () {
                                                    abp.notify.info(l('SuccessfullyDeleted'));
                                                    dataTable.ajax.reload();
                                                });
                                        }
                                    }
                                ]
                        }
                    },
                    {
                        title: l('Name'),
                        data: "Name"
                    },
                    {
                        title: l('Type'),
                        data: "DataType",
                        render: function (data) {
                            return l('Enum:DataType:' + data);
                        }
                    }
                ]
            }));
    }
});

