document.write('<script src="/Scripts/UserScripts/SearchFunction.js" type="text/javascript"></script>');
document.write('<script src="/Scripts/UserScripts/ModalPopup.js" type="text/javascript"></script>');

$(document).ready(function () {
    var addKioskUrl = $("#AddKioskUrl").val();
    var editKioskUrl = $("#EditKioskUrl").val();
    var indexKioskUrl = $("#IndexKioskUrl").val();

    $("#addbtn").click(function () {
        showLoading();
        window.location.href = addKioskUrl;
    });

    $('#kiosk-portal-table, #portal-table').on("click", "tr:not('.search-row, .kiosk-search-row')", function (e) {
        var $cell = $(e.target).closest('td');
        if ($cell.index() > 0) {
            showLoading();
            var kioskId = $(this).children().eq(1).attr("data-id");
            editKioskUrl = editKioskUrl.replace("__id__", kioskId);
            window.location.href = editKioskUrl;
        }
    });

    $(document).on('click', "input[name='kioskActionCheckbox']", function () {
        $.each($("input[name='kioskActionCheckbox']:checked"), function () {
            if ($(this).parent().hasClass("is-checked")) {
                $(this).parent().removeClass("is-checked");
            }
        });
        $("input[name='kioskActionCheckbox']").not(this).prop('checked', false);

        CheckboxFunctionalityForIE($(this), this.checked);
    });

    $('ul.KioskAction li').click(function () {
        showLoading();
        var KioskId;
        var KioskName;
        var connectionGuid;
        if ($("input[name='kioskActionCheckbox']:checked").length <= 0) {
            hideLoading();
            showDialog({
                liElement: "Delete",
                title: "Delete/Restart kiosk?",
                text: "Select a kiosk to perform operation.",
                negative: {
                    title: 'Okay'
                }
            });
        }
        else {
            $.each($("input[name='kioskActionCheckbox']:checked"), function () {
                KioskId = $(this).closest('tr').children().eq(1).attr('data-id');
                KioskName = $(this).closest('tr').children().eq(1).text();
                connectionGuid = $(this).closest('tr').children().eq(7).attr('data-cguid');
            });

            switch ($(this).find('a').text()) {
                case "Kiosk Keys":
                    $.ajax({
                        type: "Get",
                        url: "../Kiosk/GetKioskSyncServiceKeys",
                        data: "KioskId=" + KioskId,
                        dataType: "html",
                        success: function (data) {
                            hideLoading();
                            var kioskKeys = JSON.parse(data);
                            showDialog({
                                liElement: "KioskKey",
                                title: "Kiosk Keys",
                                text: "<table cellspacing='30' class='KioskKeyTable'>\
                                          <tr class='border_bottom'>\
                                          <td style='text-align:left' class='body1 highEmphasisBlack'>Organisation</td>\
                                          <td class='body1 highEmphasisBlack'>"+ kioskKeys.OrganisationName + "</td>\
                                          <td></td>\
                                          </tr>\
                                          <tr class='border_bottom'>\
                                          <td style='text-align:left' class='body1 highEmphasisBlack'>Kiosk</td>\
                                          <td class='body1 highEmphasisBlack'>"+ kioskKeys.KioskName + "</td>\
                                          <td></td>\
                                          </tr>\
                                           <tr class='border_bottom'>\
                                           <td style='text-align:left' class='body1 highEmphasisBlack'>Kiosk Key</td>\
                                           <td class='kioskGuid body1 highEmphasisBlack'>"+ kioskKeys.KioskGuid + "</td>\
                                            <td><a class='anchorbutton KioskKeyCopy'>copy</a></td>\
                                            </tr>\
                                            <tr class='border_bottom'>\
                                           <td style='text-align:left' class='body1 highEmphasisBlack'>Sync Key</td>\
                                           <td class='SyncProductKey body1 highEmphasisBlack'>"+ kioskKeys.SyncGuid + "</td>\
                                            <td><a class='anchorbutton syncKeyCopy'>copy</a></td>\
                                            </tr>\
                                             <tr class='border_bottom'>\
                                          <td style='text-align:left' class='body1 highEmphasisBlack'>Sync Service Status</td>\
                                          <td class='body1 highEmphasisBlack'>"+ kioskKeys.SyncServiceStatus + "</td>\
                                          <td></td>\
                                          </tr>\
                                       </table>",
                                negative: {
                                    title: 'Close'
                                }
                            });
                        },
                        error: function (xhr) {
                            hideLoading();
                        }
                    });
                    break;

                case "Delete":
                    hideLoading();
                    showDialog({
                        liElement: "Delete",
                        title: "Delete kiosk?",
                        text: "Do you want to delete kiosk '" + KioskName + "'?",
                        negative: {
                            title: 'Cancel'
                        },
                        positive: {
                            title: 'Delete',
                            onClick: function (e) {
                                showLoading();
                                $.ajax({
                                    type: "Get",
                                    url: "../Kiosk/DeleteKiosk",
                                    data: "KioskId=" + KioskId + "&kioskName=" + KioskName,
                                    success: function (data) {
                                        hideLoading();
                                        window.location.href = indexKioskUrl;
                                    },
                                    error: function (data) {
                                        hideLoading();
                                    }
                                });
                            }
                        }
                    });
                    break;

                case "Restart kiosk":
                    showDialog({
                        liElement: "Delete",
                        title: "Restart kiosk?",
                        text: "Do you want to Restart kiosk '" + KioskName + "'?",
                        negative: {
                            title: 'Cancel',
                            onClick: function (e) {
                                hideLoading();
                            }
                        },
                        positive: {
                            title: 'Restart',
                            onClick: function (e) {
                                showLoading();
                                $.ajax({
                                    type: "Get",
                                    url: "../Kiosk/UpdateKioskStatus",
                                    data: "KioskId=" + KioskId + "&connectionId=" + connectionGuid + "&Title=test",
                                    success: function (data) {
                                        hideLoading();
                                        window.location.href = indexKioskUrl;
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
                    hideLoading();
                    break;

            }
        }
    });

    $('.mdl-mini-footer .mdl-card__actions').append($(".dataTables_paginate,.dataTables_length,.dataTables_info"));

    $('#filterbtn').on('click', function (e) {
        e.preventDefault();
        ClearFilter();
        $('.search-row, .kiosk-search-row').toggleClass('hidden');
        if (!($('.search-row, .kiosk-search-row').hasClass('hidden'))) {
            $('.search-row, .kiosk-search-row').show();
        }
    });

    $("#portal-table thead tr, #kiosk-portal-table thead tr").on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

    $('#portal-table thead tr, #kiosk-portal-table_paginate').on("click", function () {
        componentHandler.upgradeAllRegistered();
    });

});