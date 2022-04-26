<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="BookingEntry.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking.BookingEntry" %>

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
                case "OPEN_MAP":
                    apcMap.Show();
                    break;
                case "SUBMIT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "SUBMITCONFIRM":
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

                case "DRIVER_PICKUP":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "DRIVER_PICKUP_CONFIRM":
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
                case "DRIVER_REJECT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "DRIVER_REJECT_CONFIRM":
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
                case "DRIVER_FINISH":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "DRIVER_FINISH_CONFIRM":
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

            }
        }
        function OnCarTypeChanged(s, e)
        {
            var grid = luCarType.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'CarLicense;Kilometer', OnGetSelectedFieldValues);
        }
        function OnGetSelectedFieldValues(selectedValues)
        {
            txtLicensePlate.SetValue(selectedValues[0]);
            txtLastKM.SetValue(selectedValues[1]);
        }
    </script>
    <script type="text/javascript" src="https://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=7.0&amp;s=1"></script>
    <script>
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
    </script>
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
    <dx:ASPxPopupControl ID="apcMap" runat="server" Width="400px" Height="400px" Modal="true"
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
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" BackColor="WhiteSmoke">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Name="LayoutGroupBookingEntry" GroupBoxDecoration="Box" Caption="Detail Booking - For Customer Only" Width="100%" ColCount="3">
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
                                <dx:ASPxLabel runat="server" ID="lbl1" ClientInstanceName="lbl1" Text="*Customers only need to fill in 'Detail Booking' section." ForeColor="SteelBlue" Font-Names="Calibri" Font-Size="Small" Font-Italic="true"></dx:ASPxLabel>
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
                                <dx:ASPxTextBox runat="server" ID="txtDepartment" ClientInstanceName="txtDepartment"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem><dx:LayoutItem ShowCaption="True" Caption="Phone" Width="20%">
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
                    <dx:LayoutItem ShowCaption="True" Caption="Booking Type" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox
                                    runat="server"
                                    ID="cbBookType"
                                    ClientInstanceName="cbBookType"
                                    NullText="-- Select --"
                                    AutoPostBack="false"
                                    KeyFieldName="BookTypeCode" ValueField="BookTypeDesc" TextField="BookTypeDesc"
                                    DisplayFormatString="{1}"
                                    TextFormatString="{1}"
                                    SelectionMode="Single"
                                    OnDataBinding="cbBookType_DataBinding">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Code" FieldName="BookTypeCode" />
                                        <dx:ListBoxColumn Caption="Type" FieldName="BookTypeDesc" />
                                    </Columns>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Booking type is required." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
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
                    <dx:EmptyLayoutItem Width="40%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Number of seat" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox
                                    runat="server"
                                    ID="cbNumberSeat"
                                    ClientInstanceName="cbNumberSeat"
                                    AutoPostBack="false"
                                    NullText="-- Select --"
                                    KeyFieldName="NumberOfSeat"
                                    ValueField="NumberOfSeat"
                                    TextField="NumberOfSeat"
                                    DisplayFormatString="{0}"
                                    TextFormatString="{0}"
                                    SelectionMode="Single"
                                    OnDataBinding="cbNumberSeat_DataBinding" ValueType="System.Int32">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Number Of Seat" FieldName="NumberOfSeat"/>
                                    </Columns>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Number of seat is required." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
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
                                <dx:ASPxMemo runat="server" ID="mmTripDetail" ClientInstanceName="mmTripDetail" NullText="Trip Details Address..." Height="50px" HelpText="Please add your trip details if your destination is more than two places."></dx:ASPxMemo>
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
                                                    OnCustomColumnDisplayText="gvPersonDetail_CustomColumnDisplayText">
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
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" VisibleIndex="1" Width="2%" Visible="false">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colDtlKey" Caption="DtlKey" FieldName="DtlKey" Width="20%" Visible="false">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colName" Caption="Name" FieldName="Name" Width="20%">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colGender" Caption="M / F" FieldName="Gender" Width="10%">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    <dx:ListEditItem Text="MALE" Value="MALE" />
                                                                    <dx:ListEditItem Text="FEMALE" Value="FEMALE" />
                                                                </Items>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colPersonStatus" Caption="Status" FieldName="Status" ReadOnly="false" ShowInCustomizationForm="true" Width="10%">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                            <PropertiesComboBox ClientInstanceName="colPersonStatus"
                                                                TextField="StatusDesc" ValueField="StatusDesc" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList"
                                                                TextFormatString="{0}" DataSourceID="sdsPersonStatus">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="Status" FieldName="StatusDesc"></dx:ListBoxColumn>
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colRemark1" Caption="Remark 1" FieldName="Remark1" Width="20%" PropertiesTextEdit-NullText="...">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colRemark2" Caption="Remark 2" FieldName="Remark2" Width="20%" PropertiesTextEdit-NullText="...">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colRemark3" Caption="Remark 3" FieldName="Remark3" Width="20%" PropertiesTextEdit-NullText="...">
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
            <dx:LayoutGroup Name="LayoutGroupAdminEntry" GroupBoxDecoration="Box" Caption="Estimated Booking - For Administrator Only" Width="50%" ColCount="3">
                <GroupBoxStyle>
                    <Caption ForeColor="SteelBlue" Font-Size="Larger" Font-Bold="true" Font-Names="Calibri" BackColor="WhiteSmoke"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Driver" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxComboBox
                                    runat="server"
                                    ID="cbDriver"
                                    ClientInstanceName="cbDriver"
                                    NullText="-- Select --"
                                    AutoPostBack="false"
                                    KeyFieldName="USER_NAME" ValueField="USER_NAME" TextField="USER_NAME"
                                    DisplayFormatString="{0}"
                                    TextFormatString="{0}"
                                    SelectionMode="Single"
                                    OnDataBinding="cbDriver_DataBinding">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Driver" FieldName="USER_NAME" />
                                    </Columns>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Est. Pickup Time" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deEstPickupTime" ClientInstanceName="deEstPickupTime" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss">
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
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
                                    DisplayFormatString="{1}"
                                    TextFormatString="{1}"
                                    SelectionMode="Single"
                                    OnDataBinding="luCarType_DataBinding">
                                    <ClientSideEvents ValueChanged="OnCarTypeChanged" />
                                    <GridViewProperties>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True" />
                                    </GridViewProperties>
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="Type" FieldName="CarType" ShowInCustomizationForm="True" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Name" FieldName="CarName" ShowInCustomizationForm="True" VisibleIndex="1">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="License" FieldName="CarLicense" ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Number of seat" FieldName="NumberOfSeat" ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Caption="Last KM" FieldName="Kilometer" ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <GridViewStyles AdaptiveDetailButtonWidth="22">
                                    </GridViewStyles>
                                    <%--<ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave">
                                        <RequiredField ErrorText="* Value can't be empty." IsRequired="True" />
                                    </ValidationSettings>--%>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Est. Arrival Time" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deEstArrivalTime" ClientInstanceName="deEstArrivalTime" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss">
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                </dx:ASPxDateEdit>
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
                                <dx:ASPxTextBox runat="server" ID="txtLastKM" ClientInstanceName="txtLastKM" ReadOnly="true"></dx:ASPxTextBox>
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
                                <dx:ASPxButton runat="server" ID="btnAdminOnHold" ClientInstanceName="btnAdminOnHold" Text="Hold" ForeColor="DarkSlateBlue" AutoPostBack="false" UseSubmitBehavior="false" Width="25%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('ADMIN_PENDING_CONFIRM;' + 'ADMIN_PENDING_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnAdminReject" ClientInstanceName="btnAdminReject" Text="Reject" ForeColor="Red" AutoPostBack="false" UseSubmitBehavior="false" Width="25%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('ADMIN_REJECT_CONFIRM;' + 'ADMIN_REJECT_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="true" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnAdminApprove" ClientInstanceName="btnAdminApprove" Text="Approve" ForeColor="DarkSlateBlue" AutoPostBack="false" UseSubmitBehavior="false" Width="25%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('ADMIN_APPROVE_CONFIRM;' + 'ADMIN_APPROVE_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupActualEntry" GroupBoxDecoration="Box" Caption="Actual Booking - For Driver Only" Width="50%" ColCount="1">
                <GroupBoxStyle>
                    <Caption ForeColor="SteelBlue" Font-Size="Larger" Font-Bold="true" Font-Names="Calibri" BackColor="WhiteSmoke"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Driver" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtActDriverName" ClientInstanceName="txtActDriverName" ReadOnly="true"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Act. Pickup" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deActPickupTime" ClientInstanceName="deActPickupTime" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" ReadOnly="true">
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Act. Arrival" Width="50%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deActArrivalTime" ClientInstanceName="deActArrivalTime" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" ReadOnly="true">
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Driver Remark" Width="100%" RowSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmActRemark" ClientInstanceName="mmActRemark" Height="50px" NullText="..." ClientEnabled="true"></dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnDriverPickUp" ClientInstanceName="btnDriverPickUp" Text="Pickup" ForeColor="DarkSlateBlue" Width="25%" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" ClientVisible="true">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('DRIVER_PICKUP_CONFIRM;' + 'DRIVER_PICKUP_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnDriverReject" ClientInstanceName="btnDriverReject" Text="Reject" ForeColor="Red" Width="25%" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" ClientVisible="true">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('DRIVER_REJECT_CONFIRM;' + 'DRIVER_REJECT_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxButton runat="server" ID="btnDriverFinish" ClientInstanceName="btnDriverFinish" Text="Finish" ForeColor="DarkSlateBlue" Width="25%" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" ClientVisible="true">
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('DRIVER_FINISH_CONFIRM;' + 'DRIVER_FINISH_CONFIRM'); }" />
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
            <dx:LayoutItem ShowCaption="False" Width="10%" Visible="false">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnTestMap" ClientInstanceName="btnTestMap" Text="Open Map test" AutoPostBack="false" UseSubmitBehavior="false" Width="100%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('OPEN_MAP;' + 'OPEN_MAP'); }" />
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
            <dx:LayoutItem ShowCaption="False" Width="10%" Visible="false">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnReject" ClientInstanceName="btnReject" Text="Reject" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('REJECTCONFIRM;' + 'REJECTCONFIRM'); }" />
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
    <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" ID="sdsPersonStatus" runat="server" SelectCommand="SELECT StatusDesc FROM [dbo].[PersonStatus] ORDER BY StatusDesc"></asp:SqlDataSource>
</asp:Content>
