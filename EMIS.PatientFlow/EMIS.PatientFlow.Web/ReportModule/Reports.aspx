<%@ Page  Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="EMIS.PatientFlow.Web.ReportModule.Reports" Async="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="/css/report-module.css" rel="stylesheet" />

    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/datepicker3.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.10.2.js"></script>
    <script src="/Scripts/dist/js/select2.full.min.js"></script>
    <script src="/Scripts/select2-ajax-wrapper.js"></script>
    <link href="~/Scripts/dist/css/select2.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datepicker.js"></script>
    <script src="Scripts/report-module.js"></script>

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
                
                  <select id="KioskId" ></select>
                <asp:HiddenField ID="hidKioskId" ClientIDMode="Static" runat="server" />
             
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
                      <asp:Button ID="BtnViewReports" runat="server" Text="View Reports" OnClientClick="ViewReports()" CssClass="green-button" OnClick="BtnViewReports_Click" />
                  </ContentTemplate>
                <Triggers><asp:AsyncPostBackTrigger ControlID="BtnViewReports" /> </Triggers>
              </asp:UpdatePanel>
            </div>
                
                </div>
        </div>
        
            <asp:UpdatePanel ID="UpdatePanelLogReport" runat="server" UpdateMode="Always">
                <ContentTemplate>
               <div class="report-filter">
            <div class="col-sm-12">
                <rsweb:ReportViewer ID="ReportViewerLogReport" runat="server"  Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1308px">
                    <LocalReport ReportPath="~\ReportModule\LogReport.rdlc">
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
