<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SettlementEntry.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Transactions.Settlement.SettlementEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/Application.js"></script>
    <script type="text/javascript">
        var IsItemCodeChanged = true;
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
                case "SUBMIT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "SUBMITCONFIRM":
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
                case "APPROVE":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
                case "CANCEL":
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
                case "REJECT":
                    apcalert.SetContentHtml(cplMain.cpAlertMessage);
                    apcalert.Show();
                    break;
            }
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
        function gvSettlementDetail_EndCallback(s, e)
        {
            if (s.cpCmd == "INSERT" || s.cpCmd == "UPDATE" || s.cpCmd == "DELETE")
            {
                seTotal.SetText("0");
                seTotal.SetText(s.cpTotal);
                seTotal.GetInputElement().readOnly = true;
                s.cpCmd = "";
            }
        }
        function OnBookNoChange(s, e)
        {
            var grid = luBookNo.GetGridView();
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'DocDate;DocType;NumberOfSeat;EmployeeCompanyName;Department;EmployeeName;DriverName;CarType;CarLicensePlate;ActualPickDateTime;ActualArriveDateTime;RequestPickLoc;RequestDestLoc;RequestPickAddress;RequestDestAddress;TripDetails', OnGetSelectedFieldValues);
        }
        function OnGetSelectedFieldValues(selectedValues)
        {
            deBookingDate.SetValue(selectedValues[0]);
            txtBookingType.SetValue(selectedValues[1]);
            txtNumberOfSeat.SetValue(selectedValues[2]);
            txtCompany.SetValue(selectedValues[3]);
            txtDepartement.SetValue(selectedValues[4]);
            txtBookingBy.SetValue(selectedValues[5]);
            txtDriver.SetValue(selectedValues[6]);
            txtCarType.SetValue(selectedValues[7]);
            txtLicense.SetValue(selectedValues[8]);
            deBookingPickupDate.SetValue(selectedValues[9]);
            deBookingArrivalDate.SetValue(selectedValues[10]);
            txtBookingPickupLoc.SetValue(selectedValues[11]);
            txtBookingDestinationLoc.SetValue(selectedValues[12]);
            mmBookingPickupAddress.SetValue(selectedValues[13]);
            mmBookingDestinationAddress.SetValue(selectedValues[14]);
            mmBookingTripDetail.SetValue(selectedValues[15]);
            //txtApprover.SetValue(selectedValues[16]);
            //String(selectedValues[17]) == "T" ? chkNeedApproval.SetValue(true) : chkNeedApproval.SetValue(false);        
        }
        function OnItemCodeChanged(s, e)
        {
            gvSettlementDetail.GetEditor("colItemDesc").SetValue(s.GetSelectedItem().GetColumnText('ItemDescription'));
        }
        function calculationGrid()
        {
            var vQty = parseFloat(0.0);
            if (gvSettlementDetail.GetEditor("Qty").GetValue() != null &&
                gvSettlementDetail.GetEditor("Qty").GetValue().toString() != "" &&
                gvSettlementDetail.GetEditor("Qty").GetValue().toString().length != 0) {
                vQty = parseFloat(gvSettlementDetail.GetEditor("Qty").GetValue().toString());
            }
            var vUnitPrice = parseFloat(0.0);
            if (gvSettlementDetail.GetEditor("UnitPrice").GetValue() != null &&
                gvSettlementDetail.GetEditor("UnitPrice").GetValue().toString() != "" &&
                gvSettlementDetail.GetEditor("UnitPrice").GetValue().toString().length != 0) {
                vUnitPrice = parseFloat(gvSettlementDetail.GetEditor("UnitPrice").GetValue().toString());
            }
            gvSettlementDetail.GetEditor("SubTotal").SetValue(vQty * vUnitPrice);
        }
    </script>
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
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" BackColor="WhiteSmoke">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800" />
        <Items>
            <dx:LayoutGroup Name="LayoutGroupSettlementEntry" GroupBoxDecoration="Box" Caption="Settlement Entry" Width="100%" ColCount="3">
                <GroupBoxStyle>
                    <Caption ForeColor="SteelBlue" Font-Size="Larger" Font-Bold="true" Font-Names="Calibri" BackColor="WhiteSmoke"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Settlement No." Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDocNo" ClientInstanceName="txtDocNo"></dx:ASPxTextBox>
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
                    <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Settlement Date" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deDocDate" ClientInstanceName="deDocDate" EditFormat="DateTime" EditFormatString="dd/MM/yyyy">
                                    <TimeSectionProperties Visible="False">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Name="LayoutGroupBookingDetails" GroupBoxDecoration="Box" Caption="Booking Details" Width="100%" ColCount="3">
                <GroupBoxStyle>
                    <Caption ForeColor="SteelBlue" Font-Size="Larger" Font-Bold="true" Font-Names="Calibri" BackColor="WhiteSmoke"></Caption>
                </GroupBoxStyle>
                <Items>
     <%--               <dx:LayoutItem Name="lyttxtBookNo" ShowCaption="True" Caption="Booking No." Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtBookNo" ClientInstanceName="txtBookNo"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>--%>
                    <dx:LayoutItem Name="lytluBookNo" ShowCaption="True" Caption="Booking No." Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridLookup 
                                    runat="server" 
                                    ID="luBookNo" 
                                    ClientInstanceName="luBookNo"
                                    AutoPostBack="false"
                                    KeyFieldName="DocNo"
                                    DisplayFormatString="{0}"
                                    TextFormatString="{0}"
                                    SelectionMode="Single"
                                    OnDataBinding="luBookNo_DataBinding">
                                    <ClientSideEvents ValueChanged="OnBookNoChange"/>
                                    <GridViewProperties>
                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" EnableRowHotTrack="true" />
                                        <Settings ShowFilterBar="Hidden" ShowFilterRow="false" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" ShowStatusBar="Visible" />
                                        <SettingsSearchPanel ShowApplyButton="True" ShowClearButton="True" Visible="True" />
                                    </GridViewProperties>
                                    <Columns>
                                        <dx:GridViewDataColumn Caption="Booking No." Name="colDocNo" FieldName="DocNo" ShowInCustomizationForm="True" VisibleIndex="0">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataDateColumn Caption="Booking Date" Name="colDocDate" FieldName="DocDate" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" ShowInCustomizationForm="True" VisibleIndex="1">
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataColumn Name="colDocType" FieldName="DocType" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Name="colNumberOfSeat" FieldName="NumberOfSeat" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Name="colEmployeeCompanyName" FieldName="EmployeeCompanyName" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Name="colDepartement" FieldName="Department" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Name="colEmployeeName" FieldName="EmployeeName" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Name="colDriverName" FieldName="DriverName" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Name="colCarType" FieldName="CarType" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Name="colCarLicensePlate" FieldName="CarLicensePlate" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataDateColumn Name="colActualPickDateTime" FieldName="ActualPickDateTime" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataDateColumn Name="colActualArriveDateTime" FieldName="ActualArriveDateTime" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy">
                                        </dx:GridViewDataDateColumn>
                                        <dx:GridViewDataColumn Name="colRequestPickLoc" FieldName="RequestPickLoc" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Name="colRequestDestLoc" FieldName="RequestDestLoc" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataMemoColumn Name="colRequestPickAddress" FieldName="RequestPickAddress" Visible="false">
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colRequestDestAddress" FieldName="RequestDestAddress" Visible="false">
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colTripDetails" FieldName="TripDetails" Visible="false">
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataColumn Name="colApprover" FieldName="Approver" Visible="false">
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn Name="colNeedApproval" FieldName="NeedApproval" Visible="false">
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <GridViewStyles AdaptiveDetailButtonWidth="22"></GridViewStyles>
                                    <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidationSave" ErrorDisplayMode="ImageWithTooltip">
                                        <RequiredField ErrorText="Booking No. is required." IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxGridLookup>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Company" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtCompany" ClientInstanceName="txtCompany"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>               
                    <dx:LayoutItem ShowCaption="True" Caption="Driver" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDriver" ClientInstanceName="txtDriver"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
               <%--     <dx:LayoutItem ShowCaption="True" Caption="Need Approval" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxCheckBox runat="server" ID="chkNeedApproval" ClientInstanceName="chkNeedApproval" ValueChecked="true" ValueUnchecked="false" Theme="MetropolisBlue"></dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>--%>
                    <dx:EmptyLayoutItem Width="40%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Booking Date" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deBookingDate" ClientInstanceName="deBookingDate" EditFormat="DateTime" EditFormatString="dd/MM/yyyy">
                                    <TimeSectionProperties Visible="False">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Department" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtDepartement" ClientInstanceName="txtDepartement"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Car Type" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtCarType" ClientInstanceName="txtCarType"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
<%--                    <dx:LayoutItem ShowCaption="True" Caption="Approver" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtApprover" ClientInstanceName="txtApprover"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>--%>
                    <dx:EmptyLayoutItem Width="40%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Book Type" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtBookingType" ClientInstanceName="txtBookingType"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Booking By" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtBookingBy" ClientInstanceName="txtBookingBy"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="License" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtLicense" ClientInstanceName="txtLicense"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Number Of Seat" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtNumberOfSeat" ClientInstanceName="txtNumberOfSeat"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Pickup" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deBookingPickupDate" ClientInstanceName="deBookingPickupDate" EditFormat="DateTime" EditFormatString="dd/MM/yyyy [ HH:mm:ss ]">
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Arrival" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxDateEdit runat="server" ID="deBookingArrivalDate" ClientInstanceName="deBookingArrivalDate" EditFormat="DateTime" EditFormatString="dd/MM/yyyy [ HH:mm:ss ]">
                                    <TimeSectionProperties Visible="True">
                                        <TimeEditProperties EditFormatString="HH:mm:ss">
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>                 
                    <dx:LayoutItem ShowCaption="True" Caption="From" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtBookingPickupLoc" ClientInstanceName="txtBookingPickupLoc"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="To" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox runat="server" ID="txtBookingDestinationLoc" ClientInstanceName="txtBookingDestinationLoc"></dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Address" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmBookingPickupAddress" ClientInstanceName="mmBookingPickupAddress" Font-Size="8" NullText="Pickup Address..." Height="50px">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Address" Width="20%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmBookingDestinationAddress" ClientInstanceName="mmBookingDestinationAddress" Font-Size="8" NullText="Destination Address..." Height="50px">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:EmptyLayoutItem Width="20%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="Trip Details" Width="40%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxMemo runat="server" ID="mmBookingTripDetail" ClientInstanceName="mmBookingTripDetail" Font-Size="8" NullText="Trip Details Address..." Height="50px"></dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:TabbedLayoutGroup Height="135px" Name="tbLayoutGroup" ClientInstanceName="tbLayoutGroup" Width="100%" ActiveTabIndex="0" Border-BorderStyle="Solid" Border-BorderWidth="0">
                        <Items>
                            <dx:LayoutGroup Caption="Settlement Detail">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False" Caption="" Width="100%">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridView
                                                    ID="gvSettlementDetail"
                                                    ClientInstanceName="gvSettlementDetail"
                                                    runat="server"
                                                    Width="100%"
                                                    AutoGenerateColumns="False"
                                                    EnableCallBacks="true"
                                                    EnablePagingCallbackAnimation="true"
                                                    EnableTheming="True"
                                                    Theme="Office2010Blue" Font-Size="Small" Font-Names="Calibri" KeyFieldName="DtlKey"
                                                    OnInitNewRow="gvSettlementDetail_InitNewRow"
                                                    OnDataBinding="gvSettlementDetail_DataBinding"
                                                    OnRowInserting="gvSettlementDetail_RowInserting"
                                                    OnRowUpdating="gvSettlementDetail_RowUpdating"
                                                    OnRowDeleting="gvSettlementDetail_RowDeleting" 
                                                    OnCustomColumnDisplayText="gvSettlementDetail_CustomColumnDisplayText"
                                                    OnAutoFilterCellEditorInitialize="gvSettlementDetail_AutoFilterCellEditorInitialize" 
                                                    OnCellEditorInitialize="gvSettlementDetail_CellEditorInitialize" 
                                                    OnCustomCallback="gvSettlementDetail_CustomCallback" 
                                                    OnCustomButtonCallback="gvSettlementDetail_CustomButtonCallback">
                                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                                    <ClientSideEvents EndCallback="gvSettlementDetail_EndCallback"/>
                                                    <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowFilterRowMenu="false" ShowFilterBar="Auto" ShowHeaderFilterButton="false" />
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" />
                                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true" />
                                                    <SettingsSearchPanel Visible="false" />
                                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                                    <SettingsPager PageSize="5" AllButton-Visible="true" Summary-Visible="true" Visible="true"></SettingsPager>
                                                    <SettingsText CommandEdit="Edit" CommandDelete="Delete" CommandCancel="Cancel" CommandUpdate="Save" ConfirmDelete="Are you really want to Delete?" />
                                                    <SettingsEditing Mode="EditForm" UseFormLayout="true" NewItemRowPosition="Bottom"></SettingsEditing>
                                                    <SettingsCommandButton>
                                                        <NewButton ButtonType="Button" Text="Add New" Styles-Style-Width="75px"></NewButton>
                                                        <EditButton Text="Edit" ButtonType="Button" Styles-Style-Width="75px"></EditButton>
                                                        <UpdateButton Text="Save" ButtonType="Button" Styles-Style-Width="75px"></UpdateButton>
                                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                                        <DeleteButton ButtonType="Button" Styles-Style-Width="75px"></DeleteButton>
                                                    </SettingsCommandButton>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="5%">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colNo" Caption="No" ReadOnly="True" UnboundType="String" VisibleIndex="1" Width="2%" Visible="false">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                            <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False"
                                                                AllowDragDrop="False" AllowGroup="False" AllowHeaderFilter="False"
                                                                AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn  Name="ClmnCommand2"  ShowNewButton="false" ShowEditButton="false" ButtonRenderMode="Image" Caption=" " VisibleIndex="2" Width="1%" Visible="false">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="ctmbtnView">
                                                                    <Image ToolTip="View detail" Url="../../Content/Images/ViewIcon-16x16.png"/>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Name="colDtlKey" Caption="DtlKey" FieldName="DtlKey" VisibleIndex="3" Visible="false">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Name="colItemCode" Caption="Item Code" FieldName="ItemCode" Width="10%" VisibleIndex="4">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                            <PropertiesComboBox ClientInstanceName="colPersonStatus" DataSourceID="sdsItem"
                                                                TextField="ItemCode" ValueField="ItemCode" IncrementalFilteringMode="StartsWith"
                                                                DropDownStyle="DropDownList" DropDownWidth="250px"
                                                                TextFormatString="{0}">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="Item Code" FieldName="ItemCode"></dx:ListBoxColumn>
                                                                    <dx:ListBoxColumn Caption="Description" FieldName="ItemDescription"></dx:ListBoxColumn>
                                                                </Columns>
                                                                <ClientSideEvents SelectedIndexChanged="OnItemCodeChanged"/>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Name="colItemDesc" Caption="Description" FieldName="ItemDesc" Width="30%" ReadOnly="true" VisibleIndex="5">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colRemark1" Caption="Remark 1" FieldName="Remark1" Visible="false" PropertiesTextEdit-NullText="..." VisibleIndex="6">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="colRemark2" Caption="Remark 2" FieldName="Remark2" Visible="false" PropertiesTextEdit-NullText="..." VisibleIndex="7">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataMemoColumn Name="colNote" Caption="Note" FieldName="Note" PropertiesMemoEdit-Height="75px" Visible="false" VisibleIndex="8">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataMemoColumn>
                                                        <dx:GridViewDataBinaryImageColumn  Name="colImage" FieldName="Image" ShowInCustomizationForm="True" UnboundType="Object" Settings-AllowAutoFilter="False" Visible="false" VisibleIndex="9">
                                                            <PropertiesBinaryImage ImageHeight="170px" ImageWidth="140px">
                                                                <EditingSettings Enabled="true" UploadSettings-UploadValidationSettings-MaxFileSize="4194304" UploadSettings-UploadMode="Advanced">
                                                                    <UploadSettings UploadMode="Advanced">
                                                                        <UploadValidationSettings MaxFileSize="4194304" MaxFileSizeErrorText="{0} file(s) have been removed from the selection because they exceed the limit of files to be uploaded at once (which is set to {1})."></UploadValidationSettings>
                                                                    </UploadSettings>
                                                                </EditingSettings>
                                                            </PropertiesBinaryImage>
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                            <Settings AllowAutoFilter="False"></Settings>
                                                        </dx:GridViewDataBinaryImageColumn>
                                                        <dx:GridViewDataSpinEditColumn Name="colQty" Caption="Qty" FieldName="Qty" ReadOnly="false"
                                                            PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}"
                                                            PropertiesSpinEdit-DisplayFormatInEditMode="true"
                                                            PropertiesSpinEdit-AllowMouseWheel="false"
                                                            PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false"
                                                            PropertiesSpinEdit-MinValue="0"
                                                            PropertiesSpinEdit-MaxValue="999999999999999"
                                                            PropertiesSpinEdit-ClientSideEvents-ValueChanged="calculationGrid" VisibleIndex="10">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn
                                                            Name="colUnitPrice"
                                                            Caption="Unit Price"
                                                            FieldName="UnitPrice"
                                                            ReadOnly="false"
                                                            PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}"
                                                            PropertiesSpinEdit-DisplayFormatInEditMode="true"
                                                            PropertiesSpinEdit-AllowMouseWheel="false"
                                                            PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false"
                                                            PropertiesSpinEdit-MinValue="0"
                                                            PropertiesSpinEdit-MaxValue="999999999999999"
                                                            PropertiesSpinEdit-ClientSideEvents-ValueChanged="calculationGrid" VisibleIndex="11">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewDataSpinEditColumn
                                                            Name="colSubTotal"
                                                            Caption="Sub Total"
                                                            FieldName="SubTotal"
                                                            ReadOnly="true"
                                                            PropertiesSpinEdit-DisplayFormatString="{0:#,0.##}"
                                                            PropertiesSpinEdit-DisplayFormatInEditMode="true"
                                                            PropertiesSpinEdit-AllowMouseWheel="false"
                                                            PropertiesSpinEdit-SpinButtons-ShowIncrementButtons="false"
                                                            PropertiesSpinEdit-MinValue="0"
                                                            PropertiesSpinEdit-MaxValue="999999999999999" VisibleIndex="12">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399"/>
                                                        </dx:GridViewDataSpinEditColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Button" Caption=" " ShowInCustomizationForm="True" Width="5%" VisibleIndex="13" Visible="false">
                                                            <HeaderStyle Font-Bold="true" ForeColor="#003399" />
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnImage" Text="Show Image">
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <EditFormLayoutProperties>
                                                        <Items>
                                                            <dx:GridViewLayoutGroup GroupBoxDecoration="None" Width="100%">
                                                                <Items>
                                                                    <dx:GridViewColumnLayoutItem Caption="Item Code" ColumnName="colItemCode" RequiredMarkDisplayMode="Required" Width="20%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem Caption="Description" ColumnName="colItemDesc" Width="40%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem Caption="Qty" ColumnName="colQty" RequiredMarkDisplayMode="Required" Width="20%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:EmptyLayoutItem Width="40%"></dx:EmptyLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem Caption="Remark 1" ColumnName="colRemark1" Width="40%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem Caption="Unit Price" ColumnName="colUnitPrice" RequiredMarkDisplayMode="Required" Width="20%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:EmptyLayoutItem Width="40%"></dx:EmptyLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem Caption="Remark 2" ColumnName="colRemark2" Width="40%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem Caption="Sub Total" ColumnName="colSubTotal" Width="20%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:EmptyLayoutItem Width="40%"></dx:EmptyLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem Caption="Note" ColumnName="colNote" Width="40%"></dx:GridViewColumnLayoutItem>
                                                                    <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                                                                    <dx:GridViewColumnLayoutItem Caption="Image" ColumnName="colImage" Width="40%" Visible="false"></dx:GridViewColumnLayoutItem>
                                                                    <dx:EmptyLayoutItem Width="60%"></dx:EmptyLayoutItem>
                                                                    <dx:EditModeCommandLayoutItem HorizontalAlign="Right" Width="100%"></dx:EditModeCommandLayoutItem>
                                                                </Items>
                                                                <SettingsItemCaptions Location="Top"/>
                                                            </dx:GridViewLayoutGroup>
                                                        </Items>
                                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" />
                                                    </EditFormLayoutProperties>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                    </dx:TabbedLayoutGroup>
                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                    <dx:LayoutItem Caption="Total" Width="20%" CaptionStyle-Font-Bold="true">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxSpinEdit runat="server" ID="seTotal" ClientInstanceName="seTotal" AutoPostBack="false" MinValue="0" MaxValue="999999999999999" DisplayFormatString="#,0.00" SpinButtons-ShowIncrementButtons="false" HorizontalAlign="Right" AllowMouseWheel="false" Font-Bold="true" ForeColor="#666666">
                                    <ReadOnlyStyle ForeColor="DarkSlateBlue"></ReadOnlyStyle>
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Width="80%"></dx:EmptyLayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
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
            <dx:EmptyLayoutItem Width="70%"></dx:EmptyLayoutItem>
           <dx:LayoutItem ShowCaption="False" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnApprove" ClientInstanceName="btnApprove" Text="Approve" ForeColor="DarkSlateBlue" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('APPROVECONFIRM;' + 'APPROVECONFIRM'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>    
            <dx:LayoutItem ShowCaption="False" Width="10%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnSubmit" ClientInstanceName="btnSubmit" Text="Submit" ForeColor="DarkSlateBlue" AutoPostBack="false" UseSubmitBehavior="false" ValidationGroup="ValidationSave" Width="100%" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('SUBMITCONFIRM;' + 'SUBMITCONFIRM'); }" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="False" Width="10%" Visible="true">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <dx:ASPxButton runat="server" ID="btnReject" ClientInstanceName="btnReject" Text="Reject" AutoPostBack="false" UseSubmitBehavior="false" Width="100%" ForeColor="Red" Theme="Office2010Blue" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Border-BorderStyle="Outset" ClientVisible="false">
                            <ClientSideEvents Click="function(s,e) { cplMain.PerformCallback('REJECT_CONFIRM;' + 'REJECT_CONFIRM'); }" />
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
    <asp:SqlDataSource ID="sdsItem" runat="server" ConnectionString="<%$ ConnectionStrings:SqlLocalConnectionString %>"
        SelectCommand="SELECT * FROM dbo.Item WHERE IsActive = 'T' ORDER BY ItemDescription" SelectCommandType="Text">
    </asp:SqlDataSource>
</asp:Content>
