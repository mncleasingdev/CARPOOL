<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ReportPenggunaanKendaraan.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Reporting.RptPenggunaanKendaraan.ReportPenggunaanKendaraan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Reporting" ColCount="5">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="#ffffff" Font-Names="Calibri"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView
                                    ID="gvMain"
                                    ClientInstanceName="gvMain"
                                    runat="server"
                                    Width="100%"
                                    AutoGenerateColumns="False"
                                    EnableCallBacks="true"
                                    EnablePagingCallbackAnimation="true"
                                    EnableTheming="True" 
                                    OnInit="gvMain_Init" OnDataBinding="gvMain_DataBinding" 
                                    Theme="Office2010Blue" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DocKey">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                    </SettingsAdaptivity>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <SettingsSearchPanel Visible="True" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsLoadingPanel Mode="Disabled" />
                                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" Landscape="true" PaperKind="A4" />
                                    <SettingsPager PageSize="10"></SettingsPager>
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                    <Toolbars>
                                        <dx:GridViewToolbar ItemAlign="Right" EnableAdaptivity="true" Position="Top">
                                            <Items>
                                                <dx:GridViewToolbarItem Command="ShowCustomizationWindow" DisplayMode="ImageWithText" />
                                                <dx:GridViewToolbarItem Command="ExportToXlsx" Text="Export to .xlsx" ToolTip="Click here to export grid data to excel" />
                                            </Items>
                                        </dx:GridViewToolbar>
                                    </Toolbars>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDocNo" Caption="Document No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1">
                                            <HeaderStyle Font-Bold="true" />
                                            <CellStyle Wrap="False"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colDocDate" Caption="Request Date" FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="true" VisibleIndex="2">
                                            <CellStyle Wrap="False"></CellStyle>
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colEmployeeName" Caption="Employee Name" FieldName="EmployeeName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3">
                                            <HeaderStyle Font-Bold="true" />
                                            <CellStyle Wrap="False"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                <%--        <dx:GridViewDataTextColumn Name="colEmployeeCompanyName" Caption="Company Name" FieldName="EmployeeCompanyName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4">
                                            <HeaderStyle Font-Bold="true" />
                                            <CellStyle Wrap="False"></CellStyle>
                                        </dx:GridViewDataTextColumn>--%>
                                        <dx:GridViewDataTextColumn Name="colDepartment" Caption="Department" FieldName="Department" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4">
                                            <HeaderStyle Font-Bold="true" />
                                            <CellStyle Wrap="False"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colPenumpang" Caption="Passengers" FieldName="Penumpang" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="5">
                                            <HeaderStyle Font-Bold="true" />
                                            <CellStyle Wrap="False"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colActualPickDateTime" Caption="Request Date" FieldName="ActualPickDateTime" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss" Visible="true" VisibleIndex="6">
                                            <HeaderStyle Font-Bold="true" />
                                            <CellStyle Wrap="False"></CellStyle>
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataDateColumn Name="colActualArriveDateTime" Caption="Finish" FieldName="ActualArriveDateTime" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss" Visible="true" VisibleIndex="7">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                       <%-- <dx:GridViewDataTextColumn Name="colMenit" Caption="Minutes Pick to Arrive" FieldName="Menit" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="9">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>--%>
                                        <dx:GridViewDataTextColumn Name="colLastKilometer" Caption="LastKilometer" FieldName="LastKilometer" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="8">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCurrentKilometer" Caption="CurrentKilometer" FieldName="CurrentKilometer" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="9">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <%--<dx:GridViewDataTextColumn Name="colAmountParkir" Caption="AmountParkir" FieldName="AmountParkir" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="12">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colAmountTOL" Caption="AmountTOL" FieldName="AmountTOL" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="13">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>--%>
                                        <dx:GridViewDataTextColumn Name="colAmountBBM" Caption="AmountBBM" FieldName="AmountBBM" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="10">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                 <%--       <dx:GridViewDataTextColumn Name="colDriverName" Caption="DriverName" FieldName="DriverName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="15">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>--%>
                                        <dx:GridViewDataTextColumn Name="colCarLicensePlate" Caption="CarLicensePlate" FieldName="CarLicensePlate" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="11">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colRequestPickLoc" Caption="RequestPickLoc" FieldName="RequestPickLoc" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="12">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colRequestDestLoc" Caption="RequestDestLoc" FieldName="RequestDestLoc" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="13">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colTripDetail" Caption="Detail" FieldName="TripDetails" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="14">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>                                        
                                    </Columns>
                                    <Styles>
                                        <AlternatingRow Enabled="True"></AlternatingRow>
                                    </Styles>
                                    <Styles AdaptiveDetailButtonWidth="22" Footer-Font-Bold="true"></Styles>
                                    <SettingsDetail ShowDetailRow="false" />
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
