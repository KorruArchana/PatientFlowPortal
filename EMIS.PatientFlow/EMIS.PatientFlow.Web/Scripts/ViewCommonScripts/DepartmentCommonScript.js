document.write('<script src="/Scripts/UserScripts/SearchFunction.js" type="text/javascript"></script>');
document.write('<script src="/Scripts/UserScripts/ModalPopup.js" type="text/javascript"></script>');

$(document).ready(function () {
    var indexDepartmentUrl = $("#IndexDepartmentUrl").val();
    var addDepartmentUrl = $("#AddDepartmentUrl").val();
    var editDepartmentUrl = $("#EditDepartmentUrl").val();

    $("#addbtn").click(function () {
        window.location.href = addDepartmentUrl;
    });

    $('#department-table, #department-portal-table').on("click", "tr:not('.department-search-row')", function (e) {
        var $cell = $(e.target).closest('td');
        if ($cell.index() > 0) {
            showLoading();
            var departmentId = $(this).children().eq(1).attr("data-id");
            editDepartmentUrl = editDepartmentUrl.replace("__departmentId__", departmentId);
            window.location.href = editDepartmentUrl;
        }
    });

    $(document).on('click', "input[name='DepartmentActionCheckbox']", function () {
        $.each($("input[name='DepartmentActionCheckbox']:checked"), function () {
            if ($(this).parent().hasClass("is-checked")) {
                $(this).parent().removeClass("is-checked");
            }
        });
        $("input[name='DepartmentActionCheckbox']").not(this).prop('checked', false);

        CheckboxFunctionalityForIE($(this), this.checked);
    });

    $('ul.DepartmentAction li').click(function () {
        var linkCount = parseInt($("input[name='DepartmentActionCheckbox']:checked").closest('td').attr('data-linkCount'));
        var linkedMessageCount = parseInt($("input[name='DepartmentActionCheckbox']:checked").closest('td').attr('data-linkedMessageCount'));
        showLoading();
        var departmentId, departmentName;
        if ($("input[name='DepartmentActionCheckbox']:checked").length <= 0) {
            hideLoading();
            showDialog({
                liElement: "Delete",
                title: "Delete Department?",
                text: "Select a Department to delete.",
                negative: {
                    title: 'Okay'
                }
            });
        }
        else if (linkCount > 0) {
            hideLoading();
            showDialog({
                liElement: "Delete",
                title: "Department Staff?",
                text: "There are staff associated with this department. You must move them to a new department before you can delete it.",
                negative: {
                    title: 'Got it'
                }
            });
        }
        else if (linkedMessageCount > 0) {
            hideLoading();
            showDialog({
                liElement: "Delete",
                title: "Department Messages?",
                text: "There are arrival messages associated with this department. You must unlink these messages from this department before you can delete it.",
                negative: {
                    title: 'Got it'
                }
            });
        }
        else {
            $.each($("input[name='DepartmentActionCheckbox']:checked"), function () {
                departmentId = $(this).closest('tr').children().eq(1).attr('data-id');
                departmentName = $(this).closest('tr').children().eq(1).attr('data-name');
            });
            switch ($(this).find('a').text()) {
                case "Delete":
                    hideLoading();
                    showDialog({
                        liElement: "Delete",
                        title: "Delete Department?",
                        text: "Do you want to delete '" + departmentName + "'?",
                        negative: {
                            title: 'Cancel'
                        },
                        positive: {
                            title: 'Delete',
                            onClick: function (e) {
                                showLoading();
                                $.ajax({
                                    type: "Get",
                                    url: "/Department/DeleteDepartment",
                                    data: "departmentId=" + departmentId,
                                    success: function (data) {
                                        hideLoading();
                                        window.location.href = indexDepartmentUrl;
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
  
    $('.mdl-mini-footer .mdl-card__actions').append($(".dataTables_paginate,.dataTables_length,.dataTables_info"));

    $('#filterbtn').on('click', function (e) {
        e.preventDefault();
        ClearFilter();
        $('.department-search-row').toggleClass('hidden');
        if (!($('.department-search-row').hasClass('hidden'))) {
            $('.department-search-row').show();
        }
    });

    $("#department-portal-table thead tr").on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

    $('#department-portal-table_paginate').on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

});