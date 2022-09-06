$(function () {
    var l = jellog.localization.getResource("CmsKit");

    var urlShortingService = dataGap.cmsKit.admin.urlShorting.urlShortingAdmin;

    var createModal = new jellog.ModalManager(jellog.appPath + "CmsKit/UrlShorting/CreateModal");
    var editModal = new jellog.ModalManager(jellog.appPath + "CmsKit/UrlShorting/EditModal");

    var getFilter = function () {
        return {
            shortenedUrlFilter: $("#Filter").val()
        };
    };

    let dataTable = $("#UrlShortingTable").DataTable(jellog.libs.datatables.normalizeConfiguration({
        searching: false,
        processing: true,
        scrollX: true,
        serverSide: true,
        paging: true,
        ajax: jellog.libs.datatables.createAjax(urlShortingService.getList, getFilter),
        columnDefs: [
            {
                title: l("Actions"),
                targets: 0,
                width: "20%",
                rowAction: {
                    items: [
                        {
                            text: l('Edit'),
                            visible: jellog.auth.isGranted('CmsKit.UrlShorting.Update'),
                            action: function (data) {
                                editModal.open({
                                    id: data.record.id
                                });
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: jellog.auth.isGranted('CmsKit.UrlShorting.Delete'),
                            confirmMessage: function (data) {
                                return l("ForwardedUrlDeletionConfirmationMessage", data.record.name)
                            },
                            action: function (data) {
                                urlShortingService.delete(data.record.id)
                                .then(function () {
                                    dataTable.ajax.reload();
                                    jellog.notify.success(l('SuccessfullyDeleted'));
                                });
                            }
                        }
                    ]
                }
            },
            {
                width: "30%",
                title: l("Source"),
                data: "source"
            },
            {
                width: "50%",
                title: l("Target"),
                data: "target"
            }
        ]
    }));

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#RefreshFilterButton").on("click", "", function () {
        dataTable.ajax.reload();
    });

    $("#NewShortenedUrlButton").on("click", "", function () {
        createModal.open({});
    });

    $("#Filter").keypress(function (event) {
        let keyCode = (event.keyCode ? event.keyCode : event.which);
        if (keyCode == "13") {
            dataTable.ajax.reload();
        }
    })
});
