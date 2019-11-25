document.write('<script src="/Scripts/UserScripts/UserDefinedFunctions.js" type="text/javascript"></script>');

var dataTable;
$(document).ready(function () {
    // DataTable
    showLoading();
    var columnindex = 0;
    var i = 0;

    DataTableIntialization($(window).width());

    $('.search-row td').each(function (index, value) {
        var row = "";
        var td = $(this);
        var usedText = [];
        var itemlist = [];
        var title = $(this).text();
        if (title !== '' && title !== 'Divert' && title !== 'Publish' && title !== "Alert Text") {
            row += "<select class='ui multiple search dropdown table selectionDropdown column_search'\
                    multiple='multiple' data-column='" + index + "'\
                     id='"+ title.trim().replace(' ', '_') + "' >";
            row += '<option value="">Search</option>';
            var str = '#' + title.trim().replace(' ', '_');
            $.each(dataTable.column(index).data().unique().sort(), function (idx, v) {
                columnindex = i;
                i = i + 1;
                var newString = [];

                if (v.indexOf(",") > -1) {
                    //row += '<option value="' + i + '">' + v + '</option>';
                    newString = v.split(/[ ,.]+/);
                    columnindex = i + 1;
                    $.each(newString, function (index, tabletext) {
                        var tabletext1 = camalize(tabletext);
                        if (usedText.indexOf(tabletext1) === -1) {
                            itemlist.push(tabletext1);
                            //row += '<option value="' + columnindex + '">' + tabletext + '</option>';
                        }
                        columnindex++;
                        usedText.push(tabletext1);
                    });
                    i = columnindex;
                }
                else {
                    //  row += '<option value="' + i + '">' + v + '</option>';
                    itemlist.push(v);
                }
                td.html(row);
                $(str).drop();

                $('.ui.dropdown.table').drop({ fullTextSearch: true });
            });

            itemlist = itemlist.sort(naturalCompare);
            $.each(itemlist, function (index, tabletext) {
                row += '<option value="' + index + '">' + tabletext + '</option>';
            });

            td.html(row);
            $(str).drop();

            $('.ui.dropdown.table').drop({ fullTextSearch: true });

            row += '</select>';

        }

        else if (title === "Alert Text") {
            $(this).html('<input type="text" data-column="' + i + '" placeholder="Search" class="column_search" style="height: 56px;"/>');
        }

        else if (title === "Publish") {
            row += "<select class='ui multiple search dropdown table selectionDropdown publishDropdown column_search'\
                    data-column='" + index + "'\
                    id='"+ title.trim().replace(' ', '_') + "' multiple='multiple' >";
            row += '<option value="">Search</option><option value="1">Published</option><option value="2">Unpublished</option>';
            row += '</select>';
            var publish = '#' + title.trim().replace(' ', '_');
            td.html(row);
            $(publish).drop();
            $('.ui.dropdown.table').drop({ fullTextSearch: true });
        }

        else { $(this).html(""); }

    });

    $('#portal-table thead').on('keyup', ".column_search", function () {
        dataTable
            .column($(this).parent().index())
            .search(this.value)
            .draw();
    });

    $('.ui.dropdown.table.publishDropdown').on("change", function () {
        var textSingleValue = $(this).find('option:selected').text();
        if (textSingleValue === "Published") {
            dataTable
                .column($(this).parent().index())
                .search("true")
                .draw();

        }
        else if (textSingleValue === "Unpublished") {
            dataTable
                .column($(this).parent().index())
                .search("false")
                .draw();
        }

        else if (textSingleValue === "PublishedUnpublished" || textSingleValue === "UnpublishedPublished" || textSingleValue === "") {
            var data = [];
            dataTable
                .column($(this).parent().index())
                .search(data, null, true, false)
                .draw();

        }
    });

    $('.ui.dropdown.table').on("change", function () {
        if (!$(this).hasClass("publishDropdown")) {
            var textValue = [];
            var textSingleValue = $(this).find('option:selected').text();
            $(this).find('option:selected').each(function () {
                textValue.push($(this).text());
            });
            var data = $.map(textValue, function (index, value) {
                return index ? '^.*' + $.fn.dataTable.util.escapeRegex(index) + '.*$' : null;
            });
            if (data.length === 0) {
                data = [""];
            }
            var val = data.join('|');
            if (textValue.length > 1) {
                dataTable.columns($(this).parent().index()).every(function () {
                    var column = this;
                    column
                        .search(val ? val : '', true, false)
                        .draw();
                });
            }
            else {
                dataTable
                    .column($(this).parent().index())
                    .search(textSingleValue)
                    .draw();
            }
        }
    });


    //$(window).resize(function () {
    //    if ($.fn.dataTable.isDataTable('#portal-table')) {
    //        $('.dataTables_paginate,.dataTables_length,.dataTables_info').remove();
    //        $('#portal-table').DataTable().destroy();
    //        DataTableIntialization($(window).width());
    //    }
    //});

    function DataTableIntialization(width) {
        dataTable = $("#portal-table").DataTable({
            "dom": "rt<'bottom' lip><'clear'>",
            "pagingType": "simple",
            "bFilter": true,
            "bPaginate": true,
            "processing": true,
            initComplete: function () {
                componentHandler.upgradeAllRegistered();
                $('.mdl-tooltip').toggle(function () {
                    $(".mdl-tooltip").css({ 'margin-top': "-20px" });
                }, function () {
                    $(".mdl-tooltip").css({ 'margin-top': "-20px" });
                });
            },
            "columnDefs": [{
                "targets": '_all',
                "className": "mdl-data-table__cell--non-numeric"
            },
            {
                "targets": [0, 'no-sort'],
                "orderable": false,
                bSortable: false
                }],
            "fnDrawCallback": function () {
                componentHandler.upgradeAllRegistered();
                $('.mdl-tooltip').toggle(function () {
                    $(".mdl-tooltip").css({ 'margin-top': "-20px" });
                }, function () {
                    $(".mdl-tooltip").css({ 'margin-top': "-20px" });
                });
            },
            language: {
                processing: '<div class="dataTables_wrapper" > Loading... </div>',
                paginate: {
                    next: ' <i class="material-icons PrevNextButtonColor">chevron_right</i>',
                    previous: '<i class="material-icons PrevNextButtonColor">chevron_left</i>'
                },
                sLengthMenu: width < 380 ? "Rows : _MENU_" : "Rows per Page : _MENU_"
            },
            "order": [[1, "asc"]],
            orderCellsTop: true

        });
    }

    //$.fn.dataTable.ext.search.push(
    //function (settings, data, dataIndex) {
    //         var searchvalue = $('#Organisation').val();
    //         var index = $('#Organisation').attr('data-column')
    //         if (index != undefined) {
    //             var orgvalue = data[index].toString();
    //             if (orgvalue.toLowerCase().startsWith(searchvalue)) {
    //                 return true;
    //             }
    //             return false;
    //         }
    //         return true;
    //     }
    //);
    hideLoading();
}); 