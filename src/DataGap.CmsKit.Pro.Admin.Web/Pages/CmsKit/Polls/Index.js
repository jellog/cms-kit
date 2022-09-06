$(function () {
    var l = jellog.localization.getResource("CmsKit");

    var pollService = dataGap.cmsKit.admin.polls.pollAdmin;

    var createModal = new jellog.ModalManager({ viewUrl: jellog.appPath + "CmsKit/Polls/CreateModal", modalClass: 'newPollButton' });
    var editModal = new jellog.ModalManager({ viewUrl: jellog.appPath + "CmsKit/Polls/EditModal", modalClass: 'editPoll' });
    var resultModal = new jellog.ModalManager({ viewUrl: jellog.appPath + "CmsKit/Polls/ResultModal", modalClass: 'resultPoll' });


    var getFilter = function () {
        return {
            filter: $("#Filter").val()
        };
    };

    let dataTable = $("#PollTable").DataTable(jellog.libs.datatables.normalizeConfiguration({
        searching: false,
        processing: true,
        scrollX: true,
        serverSide: true,
        paging: true,
        ajax: jellog.libs.datatables.createAjax(pollService.getList, getFilter),
        columnDefs: [
            {
                title: l("Actions"),
                targets: 0,
                width: "20%",
                rowAction: {
                    items: [
                        {
                            text: l('Edit'),
                            visible: jellog.auth.isGranted('CmsKit.Poll.Update'),
                            action: function (data) {
                                editModal.open({
                                    id: data.record.id
                                });
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: jellog.auth.isGranted('CmsKit.Poll.Delete'),
                            confirmMessage: function (data) {
                                return l("PollDeletionConfirmationMessage", data.record.name)
                            },
                            action: function (data) {
                                pollService.delete(data.record.id)
                                    .then(function () {
                                        dataTable.ajax.reload();
                                        jellog.notify.success(l('SuccessfullyDeleted'));
                                    });
                            }
                        },
                        {
                            text: l('Result'),
                            visible: jellog.auth.isGranted('CmsKit.Poll'),
                            action: function (data) {
                                resultModal.open({
                                    id: data.record.id
                                });
                            }
                        },
                    ]
                }
            },
            {
                width: "60%",
                title: l("Question"),
                data: "question"
            },
            {
                width: "20%",
                title: l("VoteCount"),
                data: "voteCount"
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

    $('button[name=NewPollButton]').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $("#Filter").keypress(function (event) {
        let keyCode = (event.keyCode ? event.keyCode : event.which);
        if (keyCode == "13") {
            dataTable.ajax.reload();
        }
    })
});
