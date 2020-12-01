$(function () {
    var l = abp.localization.getResource('Configuration');

    var dataTable = $('#ProductsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(keyFactor.carbone.configuration.products.product.getList),
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
                                        window.location.href = '/Products/EditProduct?Id=' + data.record.id
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
                    title: l('Number'),
                    data: "number"
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Type'),
                    data: "productStructure",
                    render: function (data) {
                        return l('Enum:ProductStructure:' + data);
                    }
                },
                {
                    title: l('FieldServiceProductType'),
                    data: "fieldServiceProductType",
                    render: function (data) {
                        return l('Enum:FieldServiceProductType:' + data);
                    }
                }
            ]
        }));
});