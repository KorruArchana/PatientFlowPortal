document.write('<script src="/Scripts/UserScripts/SearchFunction.js" type="text/javascript"></script>');
document.write('<script src="/Scripts/UserScripts/ModalPopup.js" type="text/javascript"></script>');

$(document).ready(function () {
    var addMessageUrl = $("#AddMessageUrl").val();
    var editMessageUrl = $("#EditMessageUrl").val();
    var indexMessageUrl = $("#IndexMessageUrl").val();

    $("#addbtn").click(function () {
        window.location.href = addMessageUrl;
    });

    $('#message-portal-table, #portal-table').on("click", "tr:not('.search-row, .message-search-row')", function (e) {
        var $cell = $(e.target).closest('td');
        if ($cell.index() > 0) {
            showLoading();
            var messageId = $(this).children().eq(1).attr("data-id");
            var messageType = $(this).children().eq(1).attr("data-AlertType");
            editMessageUrl = editMessageUrl.replace("__messageId__", messageId);
            editMessageUrl = editMessageUrl.replace("__messageType__", messageType);
            window.location.href = editMessageUrl;
        }
    });

    $(document).on('click', "input[name='messageActionCheckbox']", function () {
        $.each($("input[name='messageActionCheckbox']:checked"), function () {
            if ($(this).parent().hasClass("is-checked")) {
                $(this).parent().removeClass("is-checked");
            }
        });
        $("input[name='messageActionCheckbox']").not(this).prop('checked', false);

        CheckboxFunctionalityForIE($(this), this.checked);
    });  

    $('ul.MessageAction li').click(function () {
        showLoading();
        var message, messageType, OrgId, Id;
        if ($("input[name='messageActionCheckbox']:checked").length <= 0) {
            hideLoading();
            showDialog({
                liElement: "Delete",
                title: "Delete message?",
                text: "Select a message to delete.",
                negative: {
                    title: 'Okay'
                }
            });
        }
        else {
            $.each($("input[name='messageActionCheckbox']:checked"), function () {
                message = $(this).closest('tr').children().eq(1).text();
                messageType = $(this).closest('tr').children().eq(1).attr('data-AlertType');
                OrgId = $(this).closest('tr').children().eq(1).attr('data-orgId');
                Id = $(this).closest('tr').children().eq(1).attr('data-Id');
            });
            switch ($(this).find('a').text()) {
                case "Delete":
                    hideLoading();
                    showDialog({
                        liElement: "Delete",
                        title: "Delete message?",
                        text: "Do you want to delete this message '" + message + "'?",
                        negative: {
                            title: 'Cancel'
                        },
                        positive: {
                            title: 'Delete',
                            onClick: function (e) {
                                showLoading();
                                $.ajax({
                                    type: "Get",
                                    url: "../Messages/DeleteAlert",
                                    data: "id=" + Id + "&orgId=" + OrgId + "&MessageType=" + messageType,
                                    success: function (data) {
                                        hideLoading();
                                        window.location.href = indexMessageUrl;
                                    },
                                    error: function (xhr) {
                                        hideLoading();
                                        window.location.href = indexMessageUrl;
                                    }
                                });
                            }
                        }
                    });
            }
        }
    });
    $('.mdl-mini-footer .mdl-card__actions').append($(".dataTables_paginate,.dataTables_length,.dataTables_info"));

    $('#filterbtn').on('click', function (e) {
        e.preventDefault();
        ClearFilter();
        $('.search-row, .message-search-row').toggleClass('hidden');
        if (!($('.search-row, .message-search-row').hasClass('hidden'))) {
            $('.search-row, .message-search-row').show();
        }
    });

    $("#portal-table thead tr, #message-portal-table thead tr").on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

    $('#portal-table thead tr, #message-portal-table_paginate').on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

});