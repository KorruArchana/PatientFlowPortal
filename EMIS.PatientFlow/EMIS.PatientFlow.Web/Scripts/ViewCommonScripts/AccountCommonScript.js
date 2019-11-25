document.write('<script src="/Scripts/UserScripts/SearchFunction.js" type="text/javascript"></script>');
document.write('<script src="/Scripts/UserScripts/ModalPopup.js" type="text/javascript"></script>');
$(document).ready(function () {
    var indexAccountUrl = $("#IndexAccountUrl").val();
    var addAccountUrl = $("#AddAccountUrl").val();
    var editAccountUrl = $("#EditAccountUrl").val();
    var resetPswdUrl = $("#ResetPswdUrl").val();

    $("#addbtn").click(function () {
        window.location.href = addAccountUrl;
    });
    
    $('#user-portal-table,#user-table').on("click", "tr:not('.user-search-row')", function (e) {
        var $cell = $(e.target).closest('td');
        if ($cell.index() > 0) {
            var name = $(this).children().eq(0).attr("data-name");
            editAccountUrl = editAccountUrl.replace("__userName__", name);
            window.location.href = editAccountUrl;
        }
    });

    $(document).on('click', "input[name='UserActionCheckbox']", function () {
        $.each($("input[name='UserActionCheckbox']:checked"), function () {
            if ($(this).parent().hasClass("is-checked")) {
                $(this).parent().removeClass("is-checked");
            }
        });
        $("input[name='UserActionCheckbox']").not(this).prop('checked', false);

        CheckboxFunctionalityForIE($(this), this.checked);
    });

    $('ul.UserAction li').click(function () {
        var currentUserName = $('#CurrentUserName').val();
        var linkText = $(this).find('a').text();
        showLoading();
        var userName;
        if ($("input[name='UserActionCheckbox']:checked").length <= 0) {
            if (linkText==="Remove user") {
                hideLoading();
                showDialog({
                    liElement: "Delete",
                    title: "Remove user?",
                    text: "Select a user to remove.",
                    negative: {
                        title: 'Okay'
                    }
                });
            }
            else if (linkText === "Reset password") {
                hideLoading();
                showDialog({
                    liElement: "Delete",
                    title: "Reset password?",
                    text: "Select a user to reset password.",
                    negative: {
                        title: 'Okay'
                    }
                });
            }
           
        }
        else {
            $.each($("input[name='UserActionCheckbox']:checked"), function () {
                userName = $(this).closest('td').attr('data-name');
            });
            switch ($(this).find('a').text()) {
                case "Remove user":
                    hideLoading();
                    if (currentUserName === userName) {
                        showDialog({
                            liElement: "Delete",
                            text: "you can't delete your own user account",
                            negative: {
                                title: 'GOT IT',
                                onClick: function (e) {
                                    hideLoading();
                                }
                            }
                        });
                    }
                    else {
                        showDialog({
                            liElement: "Delete",
                            title: "Remove User?",
                            text: "Do you want to remove user '" + userName + "'?",
                            negative: {
                                title: 'Cancel'
                            },
                            positive: {
                                title: 'Remove User',
                                onClick: function (e) {
                                    showLoading();
                                    $.ajax({
                                        type: "Get",
                                        url: "/Account/DeleteUser",
                                        data: "userName=" + userName,
                                        success: function (data) {
                                            hideLoading();
                                            window.location.href = indexAccountUrl;
                                        },
                                        error: function (data) {
                                            hideLoading();
                                        }
                                    });
                                }
                            }
                        });
                    }
                    break;
                case "Reset password":
                    resetPswdUrl = resetPswdUrl.replace("__userName__", userName);
                    hideLoading();
                    window.location.href = resetPswdUrl;
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
        $('.user-search-row').toggleClass('hidden');
        if (!($('.user-search-row').hasClass('hidden'))) {
            $('.user-search-row').show();
        }
    });

    $("#user-portal-table thead tr").on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

    $('#user-portal-table_paginate').on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

});