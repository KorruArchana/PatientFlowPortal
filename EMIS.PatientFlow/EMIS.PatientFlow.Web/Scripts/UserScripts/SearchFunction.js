function ClearFilter() {
    showLoading();
    $('.ui.dropdown.table').drop('clear');
    dataTable
        .search('')
        .columns().search('')
        .draw();

    hideLoading();
}

