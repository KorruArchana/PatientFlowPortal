document.write('<script src="/Scripts/UserScripts/SearchFunction.js" type="text/javascript"></script>');
document.write('<script src="/Scripts/UserScripts/ModalPopup.js" type="text/javascript"></script>');

function Divert(status, QuestionnaireId, OrganisationId, title) {
    $.ajax({
        url: '../Questionnaire/SetPublish',
        data: "status=" + status +
            "&questionnaireId=" + QuestionnaireId + "&organisationId=" + OrganisationId,
        type: "GET",
        success: function (data) {
            if (data.Success === true) {

                if (status === true) {
                    createSnackbar(title + " Questionnaire published");
                    LogEvent('Questionnaires', 'Published', title + " Questionnaire published");
                }
                else {
                    createSnackbar(title + " Questionnaire unpublished");
                    LogEvent('Questionnaires', 'Unpublished', title + " Questionnaire unpublished");
                }
            }
            else {
                createSnackbar("Cannot publish " + title);
                LogEvent('Questionnaires', 'Cannot publish', "Cannot publish " + title);
            }
        },
        error: function (data) {
            createSnackbar("Cannot publish " + title);
        }
    });
}

$(document).ready(function () {
    var addQuestionnaireUrl = $("#AddQuestionnaireUrl").val();
    var editQuestionnaireUrl = $("#EditQuestionnaireUrl").val();
    var indexQuestionnaireUrl = $("#IndexQuestionnaireUrl").val();

    $("#addbtn").click(function () {
        localStorage.setItem("isAddQuesOnEdit", "false"); // Check whether this is needed
        window.location.href = addQuestionnaireUrl;
    });

    $('#questionnaire-portal-table, #portal-table').on("click", "tr:not('.search-row, .questionnaire-search-row')", function (e) {
        var $cell = $(e.target).closest('td');
        if ($cell.index() > 0 && $cell.index() !== 4) {
            var questionnaireId = $(this).children().eq(1).attr("data-id");
            window.location.href = editQuestionnaireUrl.replace("__id__", questionnaireId);
        }
    });

    $(document).on('click', "input[name='QuestionnaireActionCheckbox']", function () {
        $.each($("input[name='QuestionnaireActionCheckbox']:checked"), function () {
            if ($(this).parent().hasClass("is-checked")) {
                $(this).parent().removeClass("is-checked");
            }
        });
        $("input[name='QuestionnaireActionCheckbox']").not(this).prop('checked', false);

        CheckboxFunctionalityForIE($(this), this.checked);
    });

    $('ul.QuestionnaireAction li').click(function () {
        showLoading();
        var questionnaireId, questionnaireName, organisationId;
        if ($("input[name='QuestionnaireActionCheckbox']:checked").length <= 0) {
            hideLoading();
            showDialog({
                liElement: "Delete",
                title: "Delete questionnaire?",
                text: "Select a questionnaire to delete.",
                negative: {
                    title: 'Okay'
                }
            });
        }
        else {
            $.each($("input[name='QuestionnaireActionCheckbox']:checked"), function () {
                questionnaireId = $(this).closest('td').find('input').prop('id');
                questionnaireName = $(this).closest('tr').children().eq(1).text();
                organisationId = $(this).closest('tr').children().eq(3).attr('data-orgId');
            });

            switch ($(this).find('a').text()) {
                case "Delete":
                    hideLoading();
                    showDialog({
                        liElement: "Delete",
                        title: "Delete questionnaire?",
                        text: "Do you want to delete '" + questionnaireName + "'?",
                        negative: {
                            title: 'Cancel'
                        },
                        positive: {
                            title: 'Delete',
                            onClick: function (e) {
                                showLoading();
                                $.ajax({
                                    type: "Get",
                                    url: "/Questionnaire/DeleteQuestionnaire",
                                    data: "questionnaireId=" + questionnaireId + "&organisationId=" + organisationId,
                                    success: function (data) {
                                        hideLoading();
                                        window.location.href = indexQuestionnaireUrl;
                                    },
                                    error: function (data) {
                                        hideLoading();
                                    }
                                });
                            }
                        }
                    });
                    break;

                default:
                    break;
            }
        }
    });

    $('.mdl-mini-footer-page .mdl-card__actions').append($(".dataTables_paginate,.dataTables_length,.dataTables_info"));

    $('#filterbtn').on('click', function (e) {
        ClearFilter();
        e.preventDefault();
        $('.search-row, .questionnaire-search-row').toggleClass('hidden');
        if (!($('.search-row, .questionnaire-search-row').hasClass('hidden'))) {
            $('.search-row, .questionnaire-search-row').show();
        }
    });

    $("#portal-table thead tr, #questionnaire-portal-table thead tr").on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

    $('#portal-table_paginate, #questionnaire-portal-table_paginate').on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

});