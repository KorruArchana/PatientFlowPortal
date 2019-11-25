function ViewReports() {

    $("#hidOrganizationId").val($("#OrganisationId option:selected").val());
    $("#hidKioskId").val($("#KioskId option:selected").val())
    $("#hidtxtFromDate").val($("#txtFromDate").val());
    $("#hidtxtToDate").val($("#txtToDate").val());
    return false;
}

$(document).ready(function () {

    $('.datepicker').datepicker({
        orientation: "top right",
        clearBtn: true,
        autoclose: true
    });
    var optionsOrg = {
        url: '/Organisation/GetOrganisations',
        selector: '#OrganisationId',
        responseArrayName: 'OrganisationList',
        searchKeyField: 'organisationName',
        textField: 'OrganisationName',
        idField: 'Id',
        multiple: false,
        placeholder: 'Select Organisation'
    }
    select2Ajax(optionsOrg);
    var optionsKiosk = {
        url: '/Kiosk/GetKiosks',
        selector: '#KioskId',
        responseArrayName: 'KioskList',
        searchKeyField: 'kioskTitle',
        textField: 'KioskName',
        idField: 'Id',
        multiple: false,
        placeholder: 'Select Kiosk'
    }
    select2Ajax(optionsKiosk);

    $('#OrganisationId').change(function () {
        clearKiostList();
        loadKiostList($(this).val());
    });
    function clearKiostList() {
        $("#KioskId").empty();
    }
    function loadKiostList(organisationId) {
        $("#KioskId").prop("disabled", false);
        select2Ajax(optionsKiosk, { OrganisationId: organisationId });
    }
});
