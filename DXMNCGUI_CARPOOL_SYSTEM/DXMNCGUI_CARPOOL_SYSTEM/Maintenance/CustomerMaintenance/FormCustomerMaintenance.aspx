<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FormCustomerMaintenance.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Maintenance.CustomerMaintenance.FormCustomerMaintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/Application.js"></script>
    <script type="text/javascript">
        function OnClientNameSelectedChanged(s, e)
        {
            gvMain.GetEditor("SmileID").SetValue(s.GetSelectedItem().GetColumnText('Client'));
            gvMain.GetEditor("Address1").SetValue(s.GetSelectedItem().GetColumnText('Address1'));
            gvMain.GetEditor("RT").SetValue(s.GetSelectedItem().GetColumnText('RT'));
            gvMain.GetEditor("RW").SetValue(s.GetSelectedItem().GetColumnText('RW'));
            gvMain.GetEditor("Kelurahan").SetValue(s.GetSelectedItem().GetColumnText('Kelurahan'));
            gvMain.GetEditor("Kecamatan").SetValue(s.GetSelectedItem().GetColumnText('Kecamatan'));
            gvMain.GetEditor("Kota").SetValue(s.GetSelectedItem().GetColumnText('Kota'));
            gvMain.GetEditor("KodePos").SetValue(s.GetSelectedItem().GetColumnText('Area_code'));
            gvMain.GetEditor("ContactPerson").SetValue(s.GetSelectedItem().GetColumnText('Contact'));
            gvMain.GetEditor("MobilePhone").SetValue(s.GetSelectedItem().GetColumnText('Phone'));
        }
    </script>
    <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField" />
    <dx:ASPxPopupControl ID="apcconfirm" ClientInstanceName="apcconfirm" runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" HeaderText="Alert Confirmation !" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
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
    <dx:ASPxPopupControl ID="apcalert" ClientInstanceName="apcalert" runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter" AutoUpdatePosition="true" ShowCloseButton="true" PopupVerticalAlign="WindowCenter" HeaderText="Alert Error!" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False">
    </dx:ASPxPopupControl>
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Theme="PlasticBlue">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup Name="FormCaption" GroupBoxDecoration="HeadingLine" Caption="Customer Maintenance">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="#ffffff" Font-Names="Calibri"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False" Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView 
                                    runat="server" 
                                    ID="gvMain" 
                                    ClientInstanceName="gvMain" 
                                    KeyFieldName="ClientID" 
                                    AutoGenerateColumns="False"
                                    Theme="Office2010Blue" 
                                    EnableTheming="true"
                                    Width="100%" Font-Size="9" Font-Names="Calibri" 
                                    OnCellEditorInitialize="gvMain_CellEditorInitialize"
                                    OnRowInserting="gvMain_RowInserting" 
                                    OnRowUpdating="gvMain_RowUpdating" 
                                    OnRowDeleting="gvMain_RowDeleting">
                                    <SettingsAdaptivity AdaptivityMode="HideDataCellsWindowLimit" AllowOnlyOneAdaptiveDetailExpanded="True" HideDataCellsAtWindowInnerWidth="700"></SettingsAdaptivity>
                                    <ClientSideEvents/>
                                    <Settings ShowFilterRow="false" ShowGroupPanel="True" ShowFilterRowMenu="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true"/>
                                    <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="True" AllowSelectByRowClick="True" FilterRowMode="OnClick" EnableRowHotTrack="true" EnableCustomizationWindow="true" AllowEllipsisInText="true"/>
                                    <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="true"/>
                                    <SettingsSearchPanel Visible="true" />
                                    <SettingsFilterControl ViewMode="VisualAndText" AllowHierarchicalColumns="true" ShowAllDataSourceColumns="true" MaxHierarchyDepth="1" />
                                    <SettingsPager PageSize="20"></SettingsPager>
                                    <SettingsResizing ColumnResizeMode="Control" Visualization="Live" />
                                    <SettingsText ConfirmDelete="Are you really want to Delete?" PopupEditFormCaption="Client View"/>
                                    <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                    <SettingsCommandButton>
                                        <NewButton ButtonType="Button" Text="Add New"></NewButton>
                                        <EditButton ButtonType="Image" Image-Url="../../Content/Images/ViewIcon-16x16.png" Styles-Style-Width="25px" Styles-Style-Border-BorderStyle="Ridge" Styles-Style-Border-BorderWidth="1"></EditButton>
                                        <DeleteButton ButtonType="Image" Image-Url="../../Content/Images/DeleteIcon-16x16.png" Styles-Style-Width="25px" Styles-Style-Border-BorderStyle="Ridge" Styles-Style-Border-BorderWidth="1"></DeleteButton>
                                        <UpdateButton ButtonType="Button" Text="Save"  Styles-Style-Width="75px"></UpdateButton>
                                        <CancelButton ButtonType="Button" Styles-Style-Width="75px"></CancelButton>
                                    </SettingsCommandButton>
                                    <Columns>
                                        <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="true" ShowEditButton="true" ShowInCustomizationForm="true" ShowNewButtonInHeader="True" VisibleIndex="0" Visible="true" Width="100px"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Name="colSmileID" Caption="Smile ID" FieldName="SmileID" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colClientID" Caption="Client ID" FieldName="ClientID" PropertiesTextEdit-NullText="..." Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Name="colName" Caption="Name" FieldName="Name">
                                            <PropertiesComboBox DataSourceID="dsClient" ClientInstanceName="colName" TextField="Name" ValueField="Name" IncrementalFilteringMode="Contains" TextFormatString="{1}" DropDownStyle="DropDown">
                                                <ClientSideEvents ValueChanged="OnClientNameSelectedChanged"/>
                                                <Columns>
                                                    <dx:ListBoxColumn Caption="Smile ID" FieldName="Client"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="Customer Name" FieldName="Name"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="Address" FieldName="Address1"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="RT" FieldName="RT" Width="0"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="RW" FieldName="RW" Width="0"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="Kelurahan" FieldName="Kelurahan" Width="0"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="Kecamatan" FieldName="Kecamatan" Width="0"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="Kota" FieldName="Kota" Width="0"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="Zip Code" FieldName="Area_code" Width="0"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="Contact Person" FieldName="Contact" Width="0"></dx:ListBoxColumn>
                                                    <dx:ListBoxColumn Caption="Phone" FieldName="Phone" Width="0"></dx:ListBoxColumn>
                                                </Columns>
                                                <ItemStyle Font-Size="Smaller" />
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataMemoColumn Name="colAddress1" Caption="Address" FieldName="Address1">
                                            <PropertiesMemoEdit Height="60" ClientInstanceName="colAddress1"></PropertiesMemoEdit>
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataMemoColumn Name="colAddress2" Caption="Address 2" FieldName="Address2" Visible="false">
                                            <PropertiesMemoEdit Height="60"></PropertiesMemoEdit>
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataTextColumn Name="colRT" Caption="RT" FieldName="RT" Width="60px"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colRW" Caption="RW" FieldName="RW" Width="60px"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colKelurahan" Caption="Kelurahan" FieldName="Kelurahan"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colKecamatan" Caption="Kecamatan" FieldName="Kecamatan"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCity" Caption="City" FieldName="Kota"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colZIPCode" Caption="ZIP Code" FieldName="KodePos" Width="80px"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colContactPerson" Caption="Contact Person" FieldName="ContactPerson"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colMobilePhone" Caption="Mobile Phone" FieldName="MobilePhone"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colEmail" Caption="Email" FieldName="Email" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCreatedBy" FieldName="CreatedBy" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colCreatedDateTime" FieldName="CreatedDateTime" Visible="false"></dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colLastModifiedBy" FieldName="LastModifiedBy" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colLastModifiedTime" FieldName="LastModifiedTime" Visible="false"></dx:GridViewDataDateColumn>
                                        <dx:GridViewDataCheckColumn Name="colIsActive" FieldName="IsActive" Width="80px">
                                            <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" ValueGrayed="F" AllowGrayed="false" ValueType="System.String"></PropertiesCheckEdit>
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                    <EditFormLayoutProperties AlignItemCaptionsInAllGroups="true">
                                        <Items>
                                            <dx:GridViewLayoutGroup Caption="Customer Details" GroupBoxDecoration="HeadingLine" ColCount="2">
                                                <Items>
                                                    <dx:GridViewColumnLayoutItem ColumnName="SmileID" Caption="Smile ID"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="IsActive" Caption="Is Active ?"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="Name" Caption="Client Name" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="ClientID" Caption="Client ID" Width="50%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="Address1" Caption="Address" Width="100%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="RT" Caption="RT" Width="50%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="RW" Caption="RW" Width="50%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="Kelurahan" Caption="Kelurahan" Width="50%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="Kecamatan" Caption="Kecamatan" Width="50%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="Kota" Caption="City" Width="50%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="KodePos" Caption="Zip Code" Width="50%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="ContactPerson" Caption="Contact Person" Width="50%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="MobilePhone" Caption="Phone" Width="50%"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="Email" Caption="Email" Width="50%" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                    <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="Undefined" ShowCaption="True" Caption="" Width="50%">
                                                        <Template>
                                                            <dx:ASPxButton runat="server" ID="btnGenerate" ClientInstanceName="btnGenerate" Text="Generate Client" Theme="MetropolisBlue"></dx:ASPxButton>
                                                        </Template>
                                                    </dx:GridViewColumnLayoutItem>
                                                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                                                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                                                    <dx:EmptyLayoutItem Width="100%"></dx:EmptyLayoutItem>
                                                    <dx:EditModeCommandLayoutItem Width="100%" HorizontalAlign="Right"></dx:EditModeCommandLayoutItem>
                                                </Items>
                                            </dx:GridViewLayoutGroup>
                                        </Items>
                                        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="700"/>
                                    </EditFormLayoutProperties>
                                    <SettingsPopup>
                                        <EditForm Width="600" PopupAnimationType="Fade" Modal="true" HorizontalAlign="Center" VerticalAlign="Middle">
                                            <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="700" />
                                        </EditForm>
                                    </SettingsPopup>   
                                    <Styles>
                                        <Header Font-Bold="true"></Header>
                                    </Styles>                                                        
                                </dx:ASPxGridView>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <asp:SqlDataSource ID="dsMasterClient" runat="server" ConnectionString="<%$ ConnectionStrings:SqlLocalConnectionString %>"
        SelectCommand="SELECT * FROM MasterClient ORDER BY NAME" 
            InsertCommand="INSERT INTO MasterClient VALUES (@Name)"
                UpdateCommand="UPDATE MasterClient SET Name=@Name WHERE SmileID=@SmileID" 
                    DeleteCommand="DELETE MasterClient WHERE (ClientID=@ClientID)">
        <InsertParameters>
            <asp:Parameter Name="Name" Type="Char"/>
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="SmileID" Type="Char"/>
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="SmileID"/>
        </DeleteParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsClient" runat="server" ConnectionString="<%$ ConnectionStrings:SqlLocalConnectionString %>"
        SelectCommand="SELECT * FROM [dbo].[SYS_CLIENT] ORDER BY NAME">
    </asp:SqlDataSource>
</asp:Content>
