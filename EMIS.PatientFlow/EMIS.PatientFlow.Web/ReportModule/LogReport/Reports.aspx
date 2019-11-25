<%@ Page  Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="EMIS.PatientFlow.Web.ReportModule.LogReport.Reports" Async="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src='<%= Page.ResolveUrl("~/Scripts/jquery-1.10.2.js")  %>'></script>
    <script src='<%= Page.ResolveUrl("~/Scripts/dist/js/select2.full.min.js")  %>'></script>
    <script src='<%= Page.ResolveUrl("~/Scripts/select2-ajax-wrapper.js")  %>'></script>  
    <script src='<%= Page.ResolveUrl("~/Scripts/bootstrap-datepicker.js")  %>'></script>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Content/datepicker3.css" rel="stylesheet" />
    <link href="../../Scripts/dist/css/select2.min.css" rel="stylesheet" />
   
    <style>
        .report-filter {
            margin: 15px 0;
            overflow: hidden;
            margin-bottom:0;
        }
        .report-filter .form-group {
            overflow: hidden;
        }
         .report-filter .form-group:last-child {
             margin-bottom:0;
         }
        .datepicker {
            top: 102px !important;
        }
        .select2-container .select2-selection--single {
            height: 34px;
        }
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 34px;
        }
        .green-button {
              background: #6d902A;
              border: none;
              border-radius: 5px;
              color: #FFF;
              font-size: 16px;
              margin-right: 20px;
              padding: 7px;
              width: 116px;
        }
        .validation-summary-errors {
          color: #e80c4d;
          font-weight: 100;
          font-size: .9em;
          margin-top: 18px;
          display: inline-block;
          margin-left: 16px;
        }
    </style>

    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r;
            i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date();
            a = s.createElement(o),
                m = s.getElementsByTagName(o)[0];
            a.async = 1;
            a.src = g;
            m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-101937322-1', 'auto');
        ga('create', 'UA-102052694-1', 'auto', 'devTracker');

        function ViewReports() {
            $("#hidOrganizationId").val($("#OrganisationId option:selected").val());
            $("#hidKioskGuid").val($("#KioskGuid option:selected").val());
            $("#hidtxtFromDate").val($("#txtFromDate").val());
            $("#hidtxtToDate").val($("#txtToDate").val());
        
            if ($("#OrganisationId").val() != null && $("#KioskGuid").val() != null && $("#txtFromDate").val() != '' && $("#txtToDate").val() != '') {
                SendGoogleEvents('event type', 'Reports', 'Log Report Viewed', 'Report Viewed');
            }
            return false;
        }


        function SendGoogleEvents(eventtype, name, action, detail) {
            if (eventtype != '') {
                if (eventtype.toLowerCase().indexOf('event') > -1) {
                    ga('devTracker.send', 'event', name, action, detail);
                    ga('send', 'event', name, action, detail);
                }
            }
        }

        $(document).ready(function () {

            $('.datepicker').datepicker({
                orientation: "top right",
                clearBtn: true,
                autoclose: true,
                format: "dd/mm/yyyy"
            });

            var orglistData = [];

            var optionsOrg = {
                selector: '#OrganisationId',
                responseArrayName: 'OrganisationList',
                searchKeyField: 'organisationName',
                textField: 'OrganisationName',
                data: orglistData,
                idField: 'Id',
                multiple: false,
                placeholder: 'Select Organisation'
            }

            $.ajax({
                url: '../../Organisation/GetOrganisationsForDropDown',
                type: "GET",
                dataType: "JSON",
                success: function (data) {
                    for(var org in data.OrganisationList)
                    {
                        var orgData = {
                            id: data.OrganisationList[org].Id,
                            text: data.OrganisationList[org].OrganisationName
                        }
                        orglistData.push(orgData);
                    }

                    select2AjaxSingle(optionsOrg);
                },
                error: function (data) {
                    console.log("Get organisations for drop down error");
                }
            })

            var optionsKiosk = {
                url: '../../Kiosk/GetKiosks',
                selector: '#KioskGuid',
                responseArrayName: 'KioskList',
                searchKeyField: 'kioskTitle',
                textField: 'KioskName',
                idField: 'KioskGuid',
                multiple: false,
                placeholder: 'Select Kiosk'
            };
            select2Ajax(optionsKiosk);
            function loadKiostList(organisationId) {
                $("#KioskGuid").prop("disabled", false);
                select2Ajax(optionsKiosk, { OrganisationId: organisationId });
            }
            function clearKiostList() {
                $("#KioskGuid").empty();
            }
            $('#OrganisationId').change(function () {
                clearKiostList();
                loadKiostList($(this).val());
            });
        });

    </script>

</head>
<body>
    <form id="FrmLogReport" runat="server">
        
        <asp:ScriptManager ID="ScriptManagerLogReport" runat="server" EnablePartialRendering="true" ></asp:ScriptManager>
         <div class="report-filter">
            <div class="form-group">
            <div class="col-sm-4">
                <select id="OrganisationId" ></select>
                <asp:HiddenField ID="hidOrganizationId" ClientIDMode="Static" runat="server" />
            </div>
            <div class="col-sm-4">
                <select id="KioskGuid" ></select>
                <asp:HiddenField ID="hidKioskGuid" ClientIDMode="Static" runat="server" />
            </div>
            </div>
            <div class="form-group">
            <div class="col-sm-4">
                <input type="text" id="txtFromDate" class="form-control datepicker" placeholder="From Date" />
                 <asp:HiddenField ID="hidtxtFromDate" ClientIDMode="Static" runat="server" />
             
            </div>
            <div class="col-sm-4">
                 <input type="text" id="txtToDate" class="form-control datepicker" placeholder="To Date" />
                 <asp:HiddenField ID="hidtxtToDate" ClientIDMode="Static" runat="server" />
           </div>
            <div class="col-sm-4">
              <asp:UpdatePanel ID="UpdatePanelFilterButton" runat="server" UpdateMode="Always">
                  <ContentTemplate>
                      <asp:Button ID="BtnViewReports" runat="server" Text="View Reports" OnClientClick="ViewReports()" CssClass="green-button" OnClick="BtnViewReports_Click" />
                  </ContentTemplate>
                <Triggers><asp:AsyncPostBackTrigger ControlID="BtnViewReports" /> </Triggers>
              </asp:UpdatePanel>
            </div>
                </div>
        </div>
            <asp:UpdatePanel ID="UpdatePanelLogReport" runat="server" UpdateMode="Always">
                <ContentTemplate>
              <asp:Label ID="lbError" runat="server" class="validation-summary-errors" Text="All fields are mandatory *"></asp:Label>
               <div class="report-filter">
            <div class="col-sm-12">
                <rsweb:ReportViewer ID="ReportViewerLogReport" 
                    runat="server"  
                    ProcessingMode="Local" 
                    Font-Names="Verdana" 
                    Font-Size="8pt" 
                    WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" Width="100%" Height="600px"
                    >
                    <LocalReport ReportPath="ReportModule\LogReport\LogReport.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource  />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
    </form>
  
</body>
</html>
