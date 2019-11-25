document.write('<script src="/Scripts/UserScripts/SearchFunction.js" type="text/javascript"></script>');
document.write('<script src="/Scripts/UserScripts/ModalPopup.js" type="text/javascript"></script>');

var checkboxevent;
function Divert(status, SessionHolderId, OrganisationId, Title, Firstname, Surname) {
    var divert = '';
    var name = Title + '. ' + Firstname + ' ' + Surname;
    $.ajax({
        url: '../Staff/SetDivert',
        data: "status=" + status +
            "&sessionHolderId=" + SessionHolderId + "&organisationId=" + OrganisationId,
        type: "GET",
        datatype: "JSON",
        success: function (data) {
            divert = data === true ? "off" : "on";
            if (divert = data === true) {
                LogEvent('Doctor Divert', 'Off', 'Divert is OFF for(' + name + ')');
                createSnackbar('Divert off');
            }
            else {
                LogEvent('Doctor Divert', 'On', 'Divert is ON for(' + name + ')');
                createSnackbar('Divert on');
            }
        },
        error: function (data) {
            $('#cover-spin').hide();
            alertify.error("Error! Cannot set divert");
        }
    });
}

$(document).ready(function () {
    var addMemberUrl = $("#AddMemberUrl").val();
    var editMemberUrl = $("#EditMemberUrl").val();
    var indexMemberUrl = $("#IndexMemberUrl").val();
    var getMessagesUrl = $("#GetMessagesUrl").val();

    $("#addbtn").click(function () {
        window.location.href = addMemberUrl;
    });

    $('#member-portal-table, #portal-table').on("click", "tr:not('.search-row, .member-search-row')", function (e) {
        var $cell = $(e.target).closest('td');
        if ($cell.index() > 0 && $cell.index() !== 6 && $cell.index() !== 2) {
            var Id = $(this).children().eq(1).attr("data-id");
            window.location.href = editMemberUrl.replace("__id__", Id);
        }
    });

    $(document).on('click', "input[name='memberActionCheckbox']", function () {
        $.each($("input[name='memberActionCheckbox']:checked"), function () {
            if ($(this).parent().hasClass("is-checked")) {
                $(this).parent().removeClass("is-checked");
            }
        });
        $("input[name='memberActionCheckbox']").not(this).prop('checked', false);

        CheckboxFunctionalityForIE($(this), this.checked);
    });

    $('ul.MemberAction li').click(function () {
        showLoading();
        var memberId;
        var memberName;
        var OrganisationId;
        if ($("input[name='memberActionCheckbox']:checked").length <= 0) {
            hideLoading();
            showDialog({
                liElement: "Delete",
                title: "Remove/View member?",
                text: "Select a member to perform operation.",
                negative: {
                    title: 'Okay'
                }
            });
        }
        else {
            $.each($("input[name='memberActionCheckbox']:checked"), function () {
                memberId = $(this).closest('tr').children().eq(1).attr('data-id');
                memberName = $(this).closest('tr').children().eq(1).attr('data-FullName');
                OrganisationId = $(this).closest('tr').children().eq(4).attr('data-orgId');
            });

            switch ($(this).find('a').text()) {
                case "Remove staff":                    
                    hideLoading();
                    showDialog({
                        liElement: "Delete",
                        title: "Remove staff member?",
                        text: "Do you want to remove '" + memberName + "'?",
                        negative: {
                            title: 'Cancel'
                        },
                        positive: {
                            title: 'Remove',
                            onClick: function (e) {
                                showLoading();
                                $.ajax({
                                    type: "Get",
                                    url: "../Staff/DeleteMember",
                                    data: "memberId=" + memberId + "&memberName=" + memberName + "&OrganisationId=" + OrganisationId,
                                    success: function (data) {
                                        hideLoading();
                                        window.location.href = indexMemberUrl;
                                    },
                                    error: function (data) {
                                        hideLoading();
                                        window.location.href = indexMemberUrl;
                                    }
                                });
                            }
                        }
                    });
                    break;

                case "View Messages":
                    showLoading();
                    getMessagesUrl = getMessagesUrl.replace("__id__", memberId);
                    getMessagesUrl = getMessagesUrl.replace("__name__", memberName);
                    window.location.href = getMessagesUrl;
                    hideLoading();
                    break;

                default:
                    break;
            }
        }
    });

    $(document).on('change', '.mdl-switch__input', function (e) {
        checkboxevent = $(this);
        var target = $(e.target);
        if (target.is(":checked")) {
            $(this).parent().next().text("Divert is ON");
        }
        else {
            $(this).parent().next().text("Divert is OFF");
        }
    });

    $('.mdl-mini-footer-page .mdl-card__actions').append($(".dataTables_paginate,.dataTables_length,.dataTables_info"));

    $('#filterbtn').on('click', function (e) {
        ClearFilter();
        e.preventDefault();
        $('.search-row, .member-search-row').toggleClass('hidden');
        if (!($('.search-row, .member-search-row').hasClass('hidden'))) {
            $('.search-row, .member-search-row').show();
        }
    });

    $("#portal-table thead tr, #member-portal-table thead tr").on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

    $('#portal-table_paginate, #member-portal-table_paginate').on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

});