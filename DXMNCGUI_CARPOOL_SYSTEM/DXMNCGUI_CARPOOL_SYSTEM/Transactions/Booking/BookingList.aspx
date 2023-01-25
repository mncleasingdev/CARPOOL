<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="BookingList.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Transactions.Booking.BookingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var DocNo;
        function cplMain_EndCallback(s, e)
        {
            switch (cplMain.cpCallbackParam)
            {
                case "NEED APPROVAL":
                    break;
                case "REFRESH":
                    gvMain.Refresh();
                    break;
                case "ONSCHEDULE_LIST":
                    apcOnSchedule.Show();
                    break;
                case "APPROVAL_LIST":
                    apcApproval.Show();
                    break;
                case "APPROVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "APPROVE_CONFIRM":
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
                case "REJECT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "REJECT_CONFIRM":
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
        function gvMain_EndCallback(s, e)
        {
            switch (gvMain.cpCallbackParam)
            {
                case "INDEX":
                    gvMain.SetFocusedRowIndex(gvMain.cpVisibleIndex);
                    break;
            }

            gvMain.cplblmessageError = "";
            gvMain.cpCallbackParam = "";
            scheduleGridUpdate(s);
        }
        function GetDocNo(values) {
            DocNo = values;
        }
        window.onload = function () {
            if (gvMain.GetFocusedRowIndex() > -1) {
                gvMain.GetRowValues(gvMain.GetFocusedRowIndex(), 'DocNo', GetDocNo);
            }
        }
        function FocusedRowChanged(s) {
            if (gvMain.GetFocusedRowIndex() > -1) {
            }
        }
        function OnGetRowValues(Value) {
            alert(Value);
        }
        var timeout;
        function scheduleGridUpdate(grid) {
            window.clearTimeout(timeout);
            timeout = window.setTimeout(
                function ()
                { gvMain.PerformCallback('REFRESH; REFRESH'); },
                60000
            );
        }
        function gvMain_Init(s, e) {
            scheduleGridUpdate(s);
        }
        function gvMain_BeginCallback(s, e) {
            window.clearTimeout(timeout);
        }
        var AppHeaderID;
        function GetHeaderID(values) {
            AppHeaderID = values;
        }
        window.onload = function () {
            if (gvMain.GetFocusedRowIndex() > -1) {
                gvMain.GetRowValues(gvMain.GetFocusedRowIndex(), 'DocNo', GetHeaderID);
            }
        }
        function FocusedRowChanged(s)
        {
            if (gvMain.GetFocusedRowIndex() > -1)
            {

            }
        }
        function gvApprovalList_CustomButtonClick(s, e)
        {
            switch (e.buttonID)
            {
                //case "btnApprove":
                //    gvApprovalList.GetRowValues(e.visibleIndex, "DocKey;", cplMain.PerformCallback("APPROVE_CONFIRM;APPROVE_CONFIRM"));
                //    break;
                //case "btnReject":
                //    gvApprovalList.GetRowValues(e.visibleIndex, "DocKey;", cplMain.PerformCallback("REJECT_CONFIRM;REJECT_CONFIRM"));
                //    break;
                case "btnShow":
                    cplMain.PerformCallback("SHOW;SHOW");
                    break;
            }
        }

        function gvOnSchedule_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case "btnShowSchedule":
                    cplMain.PerformCallback("ONSCHEDULE;ONSCHEDULE");
                    break;
            }
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Alert Confirmation !" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" Theme="Office2010Blue">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxFormLayout ID="ASPxFormLayout6" runat="server">
                    <Items>
                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None" ColCount="2">
                            <Items>
                                <dx:LayoutItem Caption="" ShowCaption="False" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                            <dx:ASPxLabel ID="lblmessage" ClientInstanceName="lblmessage" runat="server" Text="" Width="100%">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                            <dx:ASPxButton ID="btnSaveConfirm" runat="server" Text="OK" AutoPostBack="False" UseSubmitBehavior="false" Width="100" Theme="Office2010Black">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide();cplMain.PerformCallback(cplMain.cplblActionButton + ';'+cplMain.cplblActionButton); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" ColSpan="1">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                            <dx:ASPxButton ID="btnCancelConfirm" runat="server" Text="Cancel" AutoPostBack="False" UseSubmitBehavior="false" Width="100" Theme="Office2010Black">
                                                <ClientSideEvents Click="function(s, e) { apcconfirm.Hide(); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True" Theme="Office2010Blue"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Error!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="apcApproval" ClientInstanceName="apcApproval" runat="server" CloseAction="CloseButton" Modal="True" Theme="Office2010Blue" Width="1200px"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
        <SettingsAdaptivity Mode="OnWindowInnerWidth" MaxHeight="100%" MaxWidth="100%"/>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxFormLayout runat="server">
                    <Items>
                        <dx:LayoutItem ShowCaption="False" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView 
                                        ID="gvApprovalList"
                                        ClientInstanceName="gvApprovalList"
                                        runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="False"
                                        EnableCallBacks="true"
                                        EnablePagingCallbackAnimation="true"
                                        EnableTheming="True" 
                                        OnInit="gvApprovalList_Init" OnDataBinding="gvApprovalList_DataBinding" OnFocusedRowChanged="gvApprovalList_FocusedRowChanged" OnCustomCallback="gvApprovalList_CustomCallback"
                                        Theme="Office2010Blue" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DocKey">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                        <ClientSideEvents CustomButtonClick="function(s, e) { gvApprovalList_CustomButtonClick(s, e); }"/>
                                        <Settings ShowFilterRow="false" ShowGroupPanel="False" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                        <SettingsLoadingPanel Mode="Disabled" />
                                        <SettingsPager PageSize="10"></SettingsPager>
                                        <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                        <Columns>
                                            <dx:GridViewBandColumn Caption="Header">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center"/>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colDocNo" Caption="Document No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" Width="10%">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn Name="colDocDate" Caption="Document Date" FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="true" VisibleIndex="2" Width="10%">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn Name="colEmployeeName" Caption="Employee Name" FieldName="EmployeeName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3" Width="10%">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colEmployeeCompanyName" Caption="Company Name" FieldName="EmployeeCompanyName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" Width="10%">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewBandColumn Caption="Address">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center"/>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Name="colFrom" Caption="From" FieldName="RequestPickAddress" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="5" Width="10%">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTo" Caption="To" FieldName="RequestDestAddress" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="6" Width="10%">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewBandColumn Caption="Action">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center"/>
                                                <Columns>
                                                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="7" Caption=" " Width="6%">
                                                        <HeaderStyle Font-Bold="true" />
                                                        <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnShow" Text="Show"></dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                   <%-- <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="8" Caption=" " Width="6%">
                                                        <HeaderStyle Font-Bold="true" />
                                                        <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnReject" Text="Reject"></dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>--%>
                                                </Columns>
                                            </dx:GridViewBandColumn>
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
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

       <dx:ASPxPopupControl ID="apcOnSchedule" ClientInstanceName="apcOnSchedule" runat="server" CloseAction="CloseButton" Modal="True" Theme="Office2010Blue" Width="1200px"
        PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
        <SettingsAdaptivity Mode="OnWindowInnerWidth" MaxHeight="100%" MaxWidth="100%"/>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxFormLayout runat="server">
                    <Items>
                        <dx:LayoutItem ShowCaption="False" Width="100%">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxGridView 
                                        ID="gvOnSchedule"
                                        ClientInstanceName="gvOnSchedule"
                                        runat="server"
                                        Width="100%"
                                        AutoGenerateColumns="False"
                                        EnableCallBacks="true"
                                        EnablePagingCallbackAnimation="true"
                                        EnableTheming="True" 
                                        OnInit="gvOnSchedule_Init" OnDataBinding="gvOnSchedule_DataBinding" OnFocusedRowChanged="gvOnSchedule_FocusedRowChanged" OnCustomCallback="gvOnSchedule_CustomCallback"
                                        Theme="Office2010Blue" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DocKey">
                                        <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                        <ClientSideEvents CustomButtonClick="function(s, e) { gvOnSchedule_CustomButtonClick(s, e); }"/>
                                        <Settings ShowFilterRow="false" ShowGroupPanel="False" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFooter="true" />
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                        <SettingsLoadingPanel Mode="Disabled" />
                                        <SettingsPager PageSize="10"></SettingsPager>
                                        <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                        <Columns>
                                            <dx:GridViewBandColumn Caption="Header">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center"/>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Name="colDocKey" Caption="DocKey" FieldName="DocKey" ReadOnly="True" ShowInCustomizationForm="true" Visible="false" VisibleIndex="0">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colDocNo" Caption="Document No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" Width="10%">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn Name="colDocDate" Caption="Document Date" FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="true" VisibleIndex="2" Width="10%">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn Name="colEmployeeName" Caption="Employee Name" FieldName="EmployeeName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3" Width="10%">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Name="colEmployeeCompanyName" Caption="Company Name" FieldName="EmployeeCompanyName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" Width="10%">
                                                        <HeaderStyle Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewBandColumn Caption="Address">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center"/>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Name="colFrom" Caption="From" FieldName="RequestPickAddress" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="5" Width="10%">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colTo" Caption="To" FieldName="RequestDestAddress" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="6" Width="10%">
                                                <HeaderStyle Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:GridViewBandColumn>
                                            <dx:GridViewBandColumn Caption="Action">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center"/>
                                                <Columns>
                                                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="7" Caption=" " Width="6%">
                                                        <HeaderStyle Font-Bold="true" />
                                                        <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnShowSchedule" Text="Show"></dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                   <%-- <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="8" Caption=" " Width="6%">
                                                        <HeaderStyle Font-Bold="true" />
                                                        <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnReject" Text="Reject"></dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>--%>
                                                </Columns>
                                            </dx:GridViewBandColumn>
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
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Booking List" ColCount="5">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="#ffffff" Font-Names="Calibri"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxButton ID="btnNew" ClientInstanceName="btnNew" runat="server" Text="New" OnClick="btnNew_Click" ToolTip="Click here to create a new Booking" Width="100%" Height="30px" Theme="Office2010Blue">
                                    <Border BorderStyle="Outset" BorderWidth="2" BorderColor="#d0dded"/>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxButton ID="btnView" ClientInstanceName="btnView" runat="server" Text="View" OnClick="btnView_Click" ToolTip="Click here to view Booking" Width="100%" Height="30px" Theme="Office2010Blue">
                                    <Border BorderStyle="Outset" BorderWidth="2" BorderColor="#d0dded"/>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                <dx:ASPxButton ID="btnRefresh" ClientInstanceName="btnRefresh" runat="server" Text="Refresh" Image-Url="~/Content/Images/RefreshIcon-16x16.png" ToolTip="Click here to reload Booking list" Width="100%" Height="30px" Theme="Office2010Blue" AutoPostBack="false">
                                    <Border BorderStyle="Outset" BorderWidth="2" BorderColor="#d0dded"/>
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('REFRESH;' + 'REFRESH'); }"/>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                <dx:ASPxButton ID="btnApprovalList" ClientInstanceName="btnApprovalList" runat="server" Text="Approval List" Image-Url="~/Content/Images/approveico.png" Image-Height="17px" Width="100%" Theme="Office2010Blue" AutoPostBack="false" ClientVisible="true">
                                    <Border BorderStyle="Outset" BorderWidth="2" BorderColor="#d0dded"/>
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('APPROVAL_LIST;' + 'APPROVAL_LIST'); }"/>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                     <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                <dx:ASPxButton ID="btnOnSchedule" ClientInstanceName="btnOnSchedule" runat="server" Text="On Schedule" Width="100%" Theme="Office2010Blue" AutoPostBack="false" ClientVisible="false">
                                    <Border BorderStyle="Outset" BorderWidth="2" BorderColor="#d0dded"/>
                                    <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('ONSCHEDULE_LIST;' + 'ONSCHEDULE_LIST'); }"/>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Width="10%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                <dx:ASPxButton ID="btnEdit" ClientInstanceName="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" ToolTip="Click here to edit Booking" Width="100%" Height="30px" Theme="Office2010Blue" ClientVisible="false">
                                    <Border BorderStyle="Outset" BorderWidth="2" BorderColor="#d0dded"/>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
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
                                    OnInit="gvMain_Init" OnDataBinding="gvMain_DataBinding" OnFocusedRowChanged="gvMain_FocusedRowChanged" OnCustomCallback="gvMain_CustomCallback"
                                    Theme="Office2010Blue" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DocKey">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700">
                                    </SettingsAdaptivity>
                                    <ClientSideEvents
                                        EndCallback="gvMain_EndCallback"
                                        RowDblClick="function(s, e) {gvMain.PerformCallback('DOUBLECLICK;' +e.visibleIndex);}"
                                        FocusedRowChanged="FocusedRowChanged" Init="gvMain_Init" BeginCallback="gvMain_BeginCallback"/>
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
                                        <dx:GridViewDataTextColumn Name="colDocNo" Caption="Document No." FieldName="DocNo" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="1" Width="10%">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colDocDate" Caption="Document Date" FieldName="DocDate" ReadOnly="True" ShowInCustomizationForm="true" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" Visible="true" VisibleIndex="2" Width="10%">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colEmployeeName" Caption="Employee Name" FieldName="EmployeeName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="3" Width="10%">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colEmployeeCompanyName" Caption="Company Name" FieldName="EmployeeCompanyName" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="4" Width="10%">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colDepartment" Caption="Department" FieldName="Department" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="5" Width="10%">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colStatus" Caption="Status" FieldName="Status" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="6" Width="10%">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colNextApprover" Caption="Next Approver" FieldName="NextApprover" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="6" Width="10%">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCreBy" Caption="Created By" FieldName="CreatedBy" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="6" Width="10%">
                                            <HeaderStyle Font-Bold="true" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCreDate" Caption="Created Date" FieldName="CreatedDateTime" ReadOnly="True" ShowInCustomizationForm="true" Visible="true" VisibleIndex="6" Width="10%">
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
    <dx:ASPxCallback ID="cplMain" runat="server" ClientInstanceName="cplMain" OnCallback="cplMain_Callback">
        <ClientSideEvents EndCallback="cplMain_EndCallback" />
    </dx:ASPxCallback>
</asp:Content>
