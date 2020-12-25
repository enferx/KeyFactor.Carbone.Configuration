$(function () {
    var l = abp.localization.getResource('Configuration');

    var dataTable = $('#UnitsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(keyFactor.carbone.configuration.units.unit.getList),
            columnDefs: [
                {
                    title: "Actions",
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('Configuration.Units.Edit'),
                                    action: function (data) {
                                        window.location.href = '/Units/EditUnit?Id=' + data.record.id
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('Configuration.Units.Delete'),
                                    confirmMessage: function (data) {
                                        return l('AreYouSureToDelete', data.record.name);
                                    },
                                    action: function (data) {
                                        keyFactor.carbone.configuration
                                            .units.unit
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
                    data: "name"
                }
            ]
        }));
});