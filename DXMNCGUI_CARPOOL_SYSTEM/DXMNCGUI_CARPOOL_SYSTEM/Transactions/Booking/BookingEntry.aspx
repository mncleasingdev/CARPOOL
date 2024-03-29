﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="BookingEntry.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking.BookingEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var prevOnLoad = window.onload;
        window.onload = myOnLoad;
        function myOnLoad()
        {
            if (prevOnLoad != null)
                prevOnLoad();
            document.onkeydown = myOnKeyDown;
        }
        function myOnKeyDown()
        {
            if (event.keyCode == 27)
                gvDetail.UpdateEdit();
            if (event.keyCode == 13)
                gvDetail.UpdateEdit();
        }
        function cplMain_EndCallback(s, e)
        {
            switch (cplMain.cpCallbackParam)
            {
                //case "OPEN_MAP":
                //    apcMap.Show();
                //    break;
                case "SUBMIT":                   
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "SUBMITCONFIRM":
                    var today = new Date();
                    var starttime = formatDate(deReqPickupTime.GetDate());
                    var finishtime = formatDate(deReqArrivalTime.GetDate());
                    if (starttime < formatDate(today)) {
                        apcalert.SetContentHtml("Start time tidak bisa back date..");
                        apcalert.Show();
                        break;
                    }
                    if (finishtime < formatDate(today)) {
                        apcalert.SetContentHtml("Finish time tidak bisa back date..");
                        apcalert.Show();
                        break;
                    }
                    if (cplMain.cplblmessageError.length > 0)
                    {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;
                case "APPROVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "APPROVECONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }

                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        DecisionNote.SetVisible(true);
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;
                case "REJECT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "REJECTCONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }

                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        DecisionNote.SetVisible(true);
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }

                    break;
                case "CANCEL":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "CANCEL_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;

                case "ADMIN_PENDING":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "ADMIN_PENDING_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;
                case "ADMIN_REJECT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "ADMIN_REJECT_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;
                case "ADMIN_APPROVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "ADMIN_APPROVE_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
                    if (ASPxClientEdit.ValidateGroup("ValidationSave")) {
                        apcconfirm.Show();
                        lblmessage.SetText(cplMain.cplblmessage);
                    }
                    break;
                case "FINISH":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "FINISH_CONFIRM":
                    if (cplMain.cplblmessageError.length > 0) {
                        apcalert.SetContentHtml(cplMain.cplblmessageError);
                        apcalert.Show();
                        break;
                    }
            }
        }
        function formatDate(date) {
            var d = new Date(date),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2)
                month = '0' + month;
            if (day.length < 2)
                day = '0' + day;

            return [year, month, day].join('-');
        }
        function gvApproval_EndCallback(s, e) {
            if (s.cpCmd == "INSERT" || s.cpCmd == "UPDATE" || s.cpCmd == "DELETE") {
                s.cpCmd = "";
            }
        }
        function OnNameChangedApproval(s, e) {
            gvApproval.GetEditor("colNIK").SetValue(s.GetSelectedItem().GetColumnText('NIK'));
            gvApproval.GetEditor("colEmail").SetValue(s.GetSelectedItem().GetColumnText('Email'));
            gvApproval.GetEditor("colJabatan").SetValue(s.GetSelectedItem().GetColumnText('Jabatan'));
        }
        function OnCarTypeChanged(s, e)
        {
            var grid = luCarType.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'CarLicense;Kilometer;Remark', OnGetSelectedFieldValues);
        }
        function OnGetSelectedFieldValues(selectedValues)
        {
            txtLicensePlate.SetValue(selectedValues[0]);
            txtLastKM.SetValue(selectedValues[1]);
            mmAdminRemark.SetValue(selectedValues[2]);
        }
        function OnNameChanged(s, e) {
            gvPersonDetail.GetEditor("colDtlKey").SetValue(s.GetSelectedItem().GetColumnText('DtlKey'));
            gvPersonDetail.GetEditor("colName").SetValue(s.GetSelectedItem().GetColumnText('Name'))
            gvPersonDetail.GetEditor("colNIK").SetValue(s.GetSelectedItem().GetColumnText('NIK'));
            gvPersonDetail.GetEditor("colEmail").SetValue(s.GetSelectedItem().GetColumnText('Email'));
            gvPersonDetail.GetEditor("colJabatan").SetValue(s.GetSelectedItem().GetColumnText('Jabatan'));
        }
    </script>
    <%--<script type="text/javascript" src="https://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=7.0&amp;s=1"></script>--%>
   <%-- <script>
        var map = (function () {
            var bingMap = null;
            var mapElementID = "myMap";
            function getMapElement() { return document.getElementById(mapElementID); }
            function showMap() {
                if (!bingMap) {
                    createMap();
                }
            }
            function createMap()
            {
                if (typeof Microsoft.Maps.Map === "undefined") return;
                var mapOptions =
                    {
                        credentials: "AuH3CyaslDfy3L1cECbkVM5t5sbdA8NqInGCDqo9ZhsBwXrhzX6KVdlrdEhUNvbR",
                        mapTypeId: Microsoft.Maps.MapTypeId.road,
                        zoom: 18,
                        center: new Microsoft.Maps.Location(-6.184005, 106.831559),
                        enableSearchLogo: true,
                        showScalebar: false,
                        useInertia: false,
                        disableKeyboardInput: true,
                        showBreadcrumb: true
                    };
                
                bingMap = new Microsoft.Maps.Map(getMapElement(), mapOptions);
                var center = bingMap.getCenter();
                var pin = new Microsoft.Maps.Pushpin(center,
                    {
                        title: 'IT Dept.',
                        subTitle: 'PT. Guna Usaha Indonesia',
                        text: '',
                        color: 'red'
                    });
                bingMap.entities.push(pin);
            }
            return { showMap: showMap, createMap: createMap };
        })();
        function onPopupShown(s, e) {
            var windowInnerWidth = window.innerWidth;
            if (apcMap.GetWidth() > windowInnerWidth) {
                s.SetWidth(windowInnerWidth);
                s.UpdatePosition();
            }
            map.showMap();
        }
    </script>--%>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True" Theme="MetropolisBlue" EnableCallbackAnimation="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="we need your confirmation.." AllowDragging="False" PopupAnimationType="Fade" EnableViewState="False" CloseAction="CloseButton">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2">
                    <Items>
                        <dx:LayoutItem Caption="" ColSpan="2" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                    <dx:ASPxLabel ID="lblmessage" ClientInstanceName="lblmessage" runat="server" Text="">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                        <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                        <dx:LayoutItem ShowCaption="False" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="Layout_DecisionNote" runat="server">
                                    <dx:ASPxMemo runat="server" ID="DecisionNote" ClientInstanceName="DecisionNote" ClientVisible="false" Width="250px" Height="60px" Theme="Aqua">
                                    </dx:ASPxMemo>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                    <dx:ASPxButton ID="btnSaveConfirm" runat="server" Text="OK" AutoPostBack="False" UseSubmitBehavior="false" Width="100%">
                                        <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback(cplMain.cplblActionButton + ';'+cplMain.cplblActionButton); apcconfirm.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                    <dx:ASPxButton ID="btnCancelConfirm" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="100%">
                                        <ClientSideEvents Click="function(s, e) { apcconfirm.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True" Theme="Glass" EnableCallbackAnimation="true" Width="400px" Height="100px"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcFormComment" ClientInstanceName="apcFormComment" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Add Comment" AllowDragging="True" PopupAnimationType="Fade"
        EnableCallbackAnimation="true" CloseAction="CloseButton"
        EnableViewState="False"
        Width="400px"
        Height="200px"
        FooterStyle-Wrap="False" ShowFooter="false" FooterText="" AllowResize="false">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout" runat="server" ColCount="2" Width="100%">
                    <Items>
                        <dx:LayoutItem ShowCaption="True" Caption="Comment" Width="100%" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxMemo runat="server" ID="mmComment" ClientInstanceName="mmComment" Width="100%" Height="100px"></dx:ASPxMemo>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="True" Caption="" Width="100%" ColSpan="2" HorizontalAlign="Right">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                    <dx:ASPxButton ID="btnSaveComment" runat="server" Text="Save" AutoPostBack="False" UseSubmitBehavior="false" Width="50%">
                                        <ClientSideEvents Click="function(s, e) { cplMain.PerformCallback('SAVE_COMMENT'); apcFormComment.Hide(); }" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="btnCancelComment" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="50%">
                                        <ClientSideEvents Click="function(s, e) { apcFormComment.Hide(); }" />
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <%--<dx:ASPxPopupControl ID="apcMap" runat="server" Width="400px" Height="400px" Modal="true"
        ShowPinButton="True" ShowRefreshButton="True" ShowCollapseButton="True" ShowMaximizeButton="True"
        ClientInstanceName="apcMap" PopupElementID="popupArea" ShowOnPageLoad="false"
        PopupVerticalAlign="TopSides" PopupHorizontalAlign="LeftSides"
        AllowDragging="True" AllowResize="false" CloseAction="CloseButton"
        ScrollBars="None" HeaderText="Map" ShowFooter="true" FooterText="" PopupAnimationType="Fade">
        <ContentStyle Paddings-Padding="0">
        </ContentStyle>
        <ClientSideEvents Shown="onPopupShown" EndCallback="map.createMap"></ClientSideEvents>
        <ContentCollection>
            <dx:PopupControlContentControl>
                <div id='myMap' style="position: relative; width: 100%; height: 100%">
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>--%>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" BackColor="WhiteSmoke">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Name="LayoutGroupBookingEntry" GroupBoxDecoration="Box" Caption="Detail Booking" Width="100%" ColCount="3">
                <GroupBoxStyle>
                    <Caption ForeColor="SteelBlue" Font-Size="Larger" Font-Bold="true" Font-Names="Calibri" BackColor="WhiteSmoke"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Employee Name" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtEmployee" ClientInstanceName="txtEmployee"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Company" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtComapany" ClientInstanceName="txtComapany"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Status" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtStatus" ClientInstanceName="txtStatus"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" Width="40%" HorizontalAlign="Right">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Booking No" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDocNo" ClientInstanceName="txtDocNo"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Department" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox
                                    runat="server"
                                    ID="cbDepartment"
                                    ClientInstanceName="cbDepartment"
                                    NullText="-- Select --"
                                    AutoPostBack="false"
                                    KeyFieldName="Code" ValueField="DESCS" TextField="DESCS"
                                    DisplayFormatString="{1}"
                                    TextFormatString="{1}"
                                    SelectionMode="Single"
                                    OnDataBinding="cbDepartment_DataBinding">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Code" FieldName="CODE" />
                                        <dx:ListBoxColumn Caption="Descs" FieldName="DESCS" />
                                    </Columns>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Department is required." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Phone" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtHp" ClientInstanceName="txtHp"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="40%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Booking Date" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deDocDate" ClientInstanceName="deDocDate" EditFormat="DateTime" EditFormatString="dd/MM/yyyy">
                                    <TimeSectionProperties Visible="False">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Booking date is required." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>  
                    <dx:LayoutItem ShowCaption="True" Caption="Start Time" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deReqPickupTime" ClientInstanceName="deReqPickupTime" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss">
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Date and time is required." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Finish time" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deReqArrivalTime" ClientInstanceName="deReqArrivalTime" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss">
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Date and time is required" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Pickup" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtPickupLoc" ClientInstanceName="txtPickupLoc" NullText="Pickup location ..">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Location is required." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Destination" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDestinationLoc" ClientInstanceName="txtDestinationLoc" NullText="Destination location ..">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Location is required." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="40%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Address" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmPickupAddress" ClientInstanceName="mmPickupAddress" NullText="Pickup Address..." Height="50px">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Pick-up address is required." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Address" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmDestinationAddress" ClientInstanceName="mmDestinationAddress" NullText="Destination Address..." Height="50px">
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Destination address is required." IsRequired="True"/>
                                    </ValidationSettings>
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="40%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Trip Details" Width="40%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmTripDetail" ClientInstanceName="mmTripDetail" NullText="Trip Details Address..." Height="50px"></dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:TabbedLayoutGroup Height="135px" Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0">
                        <Items>
                            <dx:LayoutGroup Caption="Person Detail">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False" Caption="" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridView
                                                    ID="gvPersonDetail"
                                                    ClientInstanceName="gvPersonDetail"
                                                    runat="server"
                                                    Width="100%"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Office2010Blue" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DtlKey" OnInitNewRow="gvPersonDetail_InitNewRow"
                                                    OnDataBinding="gvPersonDetail_DataBinding"
                                                    OnRowInserting="gvPersonDetail_RowInserting"
                                                    OnRowUpdating="gvPersonDetail_RowUpdating"
                                                    OnRowDeleting="gvPersonDetail_RowDeleting" 
                                                    OnCustomColumnDisplayText="gvPersonDetail_CustomColumnDisplayText"
                                                    OnCellEditorInitialize="gvPersonDetail_CellEditorInitialize">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents />
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsText ConfirmDelete="Are you really want to Delete?" />
                                                    <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px"></NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" Width="2%" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colDtlKey" Caption="DtlKey" FieldName="DtlKey" VisibleIndex="1" Width="2%" Visible="true">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colNIK" Caption="NIK" FieldName="NIK" VisibleIndex="2" ReadOnly="True" UnboundType="String" Width="2%" Visible="True">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colName" FieldName="Name" VisibleIndex="3" ShowInCustomizationForm="True" Caption="Name" Width="10%">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399"/>
                                                            <PropertiesComboBox EnableCallbackMode="true" ClientInstanceName="colName" DropDownRows="10" IncrementalFilteringDelay="500" IncrementalFilteringMode="Contains" DisplayFormatString="{2}" TextFormatString="{2}" DropDownStyle="DropDownList" ValueField="Name" TextField="Name" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="function(s,e) {OnNameChanged(s);}" />
                                                                <ItemStyle Wrap="True"></ItemStyle>
                                                                <Columns>
                                                                    <dx:ListBoxColumn FieldName="DtlKey" Caption="DtlKey" Width="300px" />
                                                                    <dx:ListBoxColumn FieldName="NIK" Caption="NIK" Width="300px" />
                                                                    <dx:ListBoxColumn FieldName="Name" Caption="Name" Width="300px" />
                                                                    <dx:ListBoxColumn FieldName="Email" Caption="Email" Width="300px" />
                                                                    <dx:ListBoxColumn FieldName="Jabatan" Caption="Jabatan" Width="300px" />
                                                                </Columns>
                                                                <ValidationSettings ValidateOnLeave="true" ValidationGroup="ValidationSave" Display="Dynamic" ErrorDisplayMode="ImageWithText">
                                                                    <RequiredField ErrorText="* Value can't be empty." IsRequired="true" />
                                                                </ValidationSettings>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                         <dx:GridViewDataTextColumn Name="colJabatan" Caption="Jabatan" FieldName="Jabatan" VisibleIndex="4" ReadOnly="True" UnboundType="String" Width="2%" Visible="True">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colEmail" Caption="Email" FieldName="Email" VisibleIndex="5" ReadOnly="false" ShowInCustomizationForm="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                    </dx:TabbedLayoutGroup>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupAdminEntry" GroupBoxDecoration="Box" Caption="Booking Car Type" Width="50%" ColCount="3">
                <GroupBoxStyle>
                    <Caption ForeColor="SteelBlue" Font-Size="Larger" Font-Bold="true" Font-Names="Calibri" BackColor="WhiteSmoke"></Caption>
                </GroupBoxStyle>
                <Items>             
                    <dx:LayoutItem ShowCaption="True" Caption="Car Type" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup
                                    runat="server"
                                    ID="luCarType"
                                    ClientInstanceName="luCarType"
                                    NullText="-- Select --"
                                    AutoPostBack="false"
                                    KeyFieldName="CarCode" ValueField="CarName" TextField="CarName"
                                    DisplayFormatString="{0}"
                                    TextFormatString="{0}"
                                    SelectionMode="Single"
                                    OnDataBinding="luCarType_DataBinding">
                                    <ClientSideEvents ValueChanged="OnCarTypeChanged" />
                                    <GridViewProperties>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True" />
                                    </GridViewProperties>
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="Name" FieldName="CarName" ShowInCustomizationForm="True" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="License" FieldName="CarLicense" ShowInCustomizationForm="True" VisibleIndex="1">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Last KM" FieldName="Kilometer" ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Remark" FieldName="Remark" ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <GridViewStyles AdaptiveDetailButtonWidth="22">
                                    </GridViewStyles>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
  
                    <dx:LayoutItem ShowCaption="True" Caption="License Plate" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtLicensePlate" ClientInstanceName="txtLicensePlate" ReadOnly="true"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Last Kilometer" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtLastKM" ClientInstanceName="txtLastKM"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Remark" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmAdminRemark" ClientInstanceName="mmAdminRemark" Height="50px" NullText="..."></dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnAdminOnHold" ClientInstanceName="btnAdminOnHold" Text="Hold" ForeColor="DarkSlateBlue" AutoPostBack="false" UseSubmitBehavior="false" Width="25%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" ClientVisible="false">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('ADMIN_PENDING_CONFIRM;' + 'ADMIN_PENDING_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnAdminReject" ClientInstanceName="btnAdminReject" Text="Reject" ForeColor="Red" AutoPostBack="false" UseSubmitBehavior="false" Width="25%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" ClientVisible="false">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('ADMIN_REJECT_CONFIRM;' + 'ADMIN_REJECT_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnAdminApprove" ClientInstanceName="btnAdminApprove" Text="Approve" ForeColor="DarkSlateBlue" AutoPostBack="false" UseSubmitBehavior="false" Width="25%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" ClientVisible="false">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('ADMIN_APPROVE_CONFIRM;' + 'ADMIN_APPROVE_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupApproval" ShowCaption="True" Caption="Approval" GroupBoxDecoration="Box" ColCount="1" Width="100%">
                <GroupBoxStyle>
                    <Caption ForeColor="SteelBlue" Font-Size="Larger" Font-Bold="true" Font-Names="Calibri" BackColor="WhiteSmoke"></Caption>
                </GroupBoxStyle>
                      <Items>
                            <dx:LayoutItem ShowCaption="False" Width="100%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxGridView
                                            runat="server"
                                            ID="gvApproval"
                                            ClientInstanceName="gvApproval"
                                            Width="100%"
                                            KeyFieldName="DtlKey"
                                            AutoGenerateColumns="False"
                                            EnableCallBacks="true"
                                            EnablePagingCallbackAnimation="true"
                                            EnableTheming="True"
                                            Theme="Office2010Blue" Font-Size="Small" Font-Names="Calibri"
                                            OnDataBinding="gvApproval_DataBinding"
                                            OnRowInserting="gvApproval_RowInserting"
                                            OnRowUpdating="gvApproval_RowUpdating"
                                            OnRowDeleting="gvApproval_RowDeleting"
                                            OnCustomCallback="gvApproval_CustomCallback"
                                            OnAutoFilterCellEditorInitialize="gvApproval_AutoFilterCellEditorInitialize" OnCellEditorInitialize="gvApproval_CellEditorInitialize"
                                            OnCustomColumnDisplayText="gvApproval_CustomColumnDisplayText">
                                            <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                            <ClientSideEvents EndCallback="gvApproval_EndCallback" />
                                            <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                            <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                            <SettingsSearchPanel Visible="false" />
                                            <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                            <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                            <SettingsText ConfirmDelete="Are you really want to Delete?" />
                                            <SettingsEditing Mode="Inline" NewItemRowPosition="Bottom"></SettingsEditing>
                                            <SettingsCommandButton>
                                                <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px">
                                                </NewButton>
                                                <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                            </SettingsCommandButton>
                                            <Columns>
                                                <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" VisibleIndex="0" Width="2%">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                    <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                        AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                        AllowSort="False" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="1" Width="10%">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn Name="colDtlAppKey" Caption="DtlKey" FieldName="DtlKey" ReadOnly="True" Visible="false" VisibleIndex="2">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="3">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colSeq" Caption="Seq" FieldName="Seq" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="4">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Name="colNIK" FieldName="NIK" ShowInCustomizationForm="True" Caption="NIK" ReadOnly="true" Width="10%">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataComboBoxColumn Name="colNama" FieldName="Nama" ShowInCustomizationForm="True" Caption="Nama" Width="10%">
                                                   <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                    <PropertiesComboBox EnableCallbackMode="true" ClientInstanceName="colNama" DropDownRows="10" IncrementalFilteringDelay="500" IncrementalFilteringMode="Contains" DisplayFormatString="{1}" TextFormatString="{1}" DropDownStyle="DropDownList" ValueField="Nama" TextField="Nama" Width="100%">
                                                        <ClientSideEvents SelectedIndexChanged="function(s,e) {OnNameChangedApproval(s);}" />
                                                        <ItemStyle Wrap="True"></ItemStyle>
                                                        <Columns>
                                                            <dx:ListBoxColumn FieldName="NIK" Caption="NIK" Width="300px" />
                                                            <dx:ListBoxColumn FieldName="Nama" Caption="Nama" Width="300px" />
                                                            <dx:ListBoxColumn FieldName="Email" Caption="Email" Width="300px" />
                                                            <dx:ListBoxColumn FieldName="Jabatan" Caption="Jabatan" Width="300px" />
                                                        </Columns>
                                                        <ValidationSettings ValidateOnLeave="true" ValidationGroup="ValidationSave" Display="Dynamic" ErrorDisplayMode="ImageWithText">
                                                            <RequiredField ErrorText="* Value can't be empty." IsRequired="true" />
                                                        </ValidationSettings>
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataTextColumn Name="colJabatan" FieldName="Jabatan" Caption="Jabatan" Width="10%">
                                                   <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataCheckColumn Name="colIsDecision" FieldName="IsDecision" ShowInCustomizationForm="True" Caption="Decision?" ReadOnly="true" Width="5%">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                    <PropertiesCheckEdit ValueChecked="T" ValueGrayed="N" ClientInstanceName="colIsApprove" ValueType="System.Char" ValueUnchecked="F"></PropertiesCheckEdit>
                                                </dx:GridViewDataCheckColumn>
                                                <dx:GridViewDataMemoColumn Name="colTypeApproval" FieldName="TypeApproval" Caption="Type Approval" ReadOnly="true" Width="10%">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataMemoColumn>
                                                <dx:GridViewDataTextColumn Name="colDecisionState" FieldName="DecisionState" Caption="State" ReadOnly="true" Width="10%">
                                                   <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataMemoColumn Name="colDecisionNote" FieldName="DecisionNote" Caption="Decision Note" ReadOnly="true" Width="10%">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataMemoColumn>
                                                <dx:GridViewDataDateColumn Name="colDecisionDate" FieldName="DecisionDate" Caption="Decision Date" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy hh:mm:ss tt" ReadOnly="true" Width="10%">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataTextColumn Name="colEmail" FieldName="Email" Caption="Email" ReadOnly="true" Width="10%">
                                                    <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Name="txtNotesApproval" ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
            </dx:LayoutGroup>

            <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
            <dx:LayoutItem ShowCaption="False" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnApprove" ClientInstanceName="btnApprove" Text="Approve" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" ClientVisible="false">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('APPROVECONFIRM;' + 'APPROVECONFIRM'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>           
             <dx:LayoutItem ShowCaption="False" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnReject" ClientInstanceName="btnReject" Text="Reject" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" ClientVisible="false">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('REJECTCONFIRM;' + 'REJECTCONFIRM'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>           

            <dx:LayoutItem ShowCaption="False" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnSubmit" ClientInstanceName="btnSubmit" Text="Submit" ForeColor="WhiteSmoke" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SUBMITCONFIRM;' + 'SUBMITCONFIRM'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="False" Width="10%" Visible="false">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnCancel" ClientInstanceName="btnCancel" Text="Cancel" AutoPostBack="false" UseSubmitBehavior="false" Width="100%" ForeColor="Red" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('CANCEL_CONFIRM;' + 'CANCEL_CONFIRM'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
             <dx:LayoutItem ShowCaption="False" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnFinish" ClientInstanceName="btnFinish" Text="Finish" ClientVisible="false" ForeColor="DarkSlateBlue" AutoPostBack="false" UseSubmitBehavior="false" Width="100%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                             <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('FINISH;' + 'FINISH'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="False" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnBack" ClientInstanceName="btnBack" Text="Back" ForeColor="DarkSlateBlue" AutoPostBack="false" UseSubmitBehavior="false" Width="100%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" OnClick="btnBack_Click"></dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
    <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:SqlLocalConnectionString %>" ID="sdsPersonStatus" runat="server" SelectCommand="SELECT StatusDesc FROM [dbo].[PersonStatus] ORDER BY StatusDesc"></asp:SqlDataSource>
</asp:Content>
