<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncServiceReport.aspx.cs" Inherits="EMIS.PatientFlow.Web.ReportModule.SyncServiceReport.SyncServiceReport" Async="true" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge"/>

<title></title>
   
    <script src='<%= Page.ResolveUrl("~/Scripts/jquery-1.10.2.js")  %>'></script>
    <script src='<%= Page.ResolveUrl("~/Scripts/dist/js/select2.full.min.js")  %>'></script>
    <script src='<%= Page.ResolveUrl("~/Scripts/select2-ajax-wrapper.js")  %>'></script>  
    <script src='<%= Page.ResolveUrl("~/Scripts/bootstrap-datepicker.js")  %>'></script>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Content/datepicker3.css" rel="stylesheet" />
     <link href="../../Scripts/dist/css/select2.min.css" rel="stylesheet" />
     <style>
         /*loading animation starts*/
         .spinner {
             width: 74px;
             position: fixed;
             left: 0;
             top: 0;
             text-align: center;
             padding: 7px;
             padding-bottom: 3px;
             height: 100%;
             width: 100%;
             z-index: 1000;
             background: rgba(255, 255, 255, 0);
             transition: 1s;
         }

             .spinner.faded {
                 background: rgba(255, 255, 255, 0.3);
             }

             .spinner > div {
                 width: 117px;
                 position: fixed;
                 left: 30%;
                 top: 30%;
                 margin-left: -35px;
                 text-align: center;
                 padding: 43px 0;
                 padding-bottom: 3px;
                 z-index: 1001;
                 height: 100px;
                 background: rgba(0, 0, 0, 0.48);
                 border-radius: 10px;
             }

             .spinner.shown {
                 opacity: 1;
             }

             .spinner > div > div {
                 width: 14px;
                 height: 14px;
                 background-color: rgba(255, 255, 255, 1);
                 border-radius: 100%;
                 display: inline-block;
                 -webkit-animation: bouncedelay 1.4s infinite ease-in-out;
                 animation: bouncedelay 1.4s infinite ease-in-out;
                 /* Prevent first frame from flickering when animation starts */
                 -webkit-animation-fill-mode: both;
                 animation-fill-mode: both;
             }

             .spinner .bounce1 {
                 -webkit-animation-delay: -0.32s;
                 animation-delay: -0.32s;
             }

             .spinner .bounce2 {
                 -webkit-animation-delay: -0.16s;
                 animation-delay: -0.16s;
             }

         @-webkit-keyframes bouncedelay {
             0%, 80%, 100% {
                 -webkit-transform: scale(0.0);
             }

             40% {
                 -webkit-transform: scale(1.0);
             }
         }

         @keyframes bouncedelay {
             0%, 80%, 100% {
                 transform: scale(0.0);
                 -webkit-transform: scale(0.0);
             }

             40% {
                 transform: scale(1.0);
                 -webkit-transform: scale(1.0);
             }
         }
         /*loading animation ends */
         .report-filter {
             margin: 15px 0;
             overflow: hidden;
             margin-bottom: 0;
         }

             .report-filter .form-group {
                 overflow: hidden;
             }

                 .report-filter .form-group:last-child {
                     margin-bottom: 0;
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
   <script>
       function ViewReports() {
           $("#hidOrganizationId").val($("#OrganisationId option:selected").val());
           $("#hidtxtFromDate").val($("#txtFromDate").val());
           $("#hidtxtToDate").val($("#txtToDate").val());
           return false;
       }
       $(document).ready(function () {
           $('.datepicker').datepicker({

               orientation: "top right",
               clearBtn: true,
               autoclose: true,
               format: "dd/mm/yyyy"
           });
           var optionsOrg = {
               url: '../../Organisation/GetOrganisations',
               selector: '#OrganisationId',
               responseArrayName: 'OrganisationList',
               searchKeyField: 'organisationName',
               textField: 'OrganisationName',
               idField: 'Id',
               multiple: false,
               placeholder: 'Select Organisation'
           };
           select2Ajax(optionsOrg);
       });
   </script>
</head>
<body>
   
    <form id="SynServiceForm" runat="server">
        <asp:ScriptManager ID="ScriptManagerSyncServiceReport" runat="server" EnablePartialRendering="true" ></asp:ScriptManager>
          <div class="report-filter">
            <div class="form-group">
            <div class="col-sm-4">
                <select id="OrganisationId" ></select>
                <asp:HiddenField ID="hidOrganizationId" ClientIDMode="Static" runat="server" />
            </div>
            <div class="col-sm-4">
                
            </div>
            </div>
            <div class="form-group">
            <div class="col-sm-4">
                <input type="text" id="txtFromDate" class="form-control datepicker" placeholder="From Date" />
                 <asp:HiddenField ID="hidtxtFromDate" ClientIDMode="Static" runat="server" />
             
            </div>
            <div class="col-sm-4">
                 <input type="text" id="txtToDate" class="form-control datepicker" placeholder="To Date" / >
                 <asp:HiddenField ID="hidtxtToDate" ClientIDMode="Static" runat="server" />
           </div>
            <div class="col-sm-4">
              <asp:UpdatePanel ID="UpdatePanelFilterButton" runat="server" UpdateMode="Always">
                  <ContentTemplate>
                      <asp:Button ID="BtnViewReports" runat="server" Text="View Reports" OnClientClick="ViewReports()" CssClass="green-button" OnClick="BtnViewReports_Click"  />
                  </ContentTemplate>
                <Triggers><asp:AsyncPostBackTrigger ControlID="BtnViewReports" /> </Triggers>
              </asp:UpdatePanel>
            </div>
           </div>
        </div>
      <asp:UpdatePanel ID="UpdatePanelSyncServiceReport" runat="server" UpdateMode="Always">
       <ContentTemplate>
       <asp:Label ID="lbError" runat="server" class="validation-summary-errors" Text="All fields are mandatory *"></asp:Label>
       <div class="report-filter">
            <div class="col-sm-12">
            <rsweb:ReportViewer ID="ReportViewerSyncServiceReport" runat="server"
                    ProcessingMode="Local"   
                    Font-Names="Verdana" 
                    Font-Size="8pt" 
                    WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" 
                     Width="100%" Height="600px" >
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
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                 <div class="spinner" id="loadingDiv">
                    <div>
                        <div class="bounce1"></div>
                        <div class="bounce2"></div>
                        <div class="bounce3"></div>
                    </div>
                </div>
            </ProgressTemplate>
            
        </asp:UpdateProgress>
    </form>
</body>
</html>
