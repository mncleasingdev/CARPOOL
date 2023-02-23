<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FormVehicleMaintenance.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Maintenance.VehicleMaintenance.FormVehicleMaintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Vehicle Maintenance">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true" BackColor="#ffffff" Font-Names="Calibri"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxGridView 
                                    runat="server" 
                                    ID="gvMain" 
                                    ClientInstanceName="gvMain" 
                                    KeyFieldName="CarCode" 
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
                                    <SettingsText ConfirmDelete="Are you really want to Delete?" PopupEditFormCaption="Vehicle Maintenance"/>
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
                                        <dx:GridViewDataTextColumn Name="colCarCode" Caption="Car Code" FieldName="CarCode" Width="10%"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCarName" Caption="Name" FieldName="CarName" Width="50%"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colLicense" Caption="License Plate" FieldName="CarLicense"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn Name="colKilometer" Caption="Current KM." FieldName="Kilometer" PropertiesSpinEdit-DisplayFormatString="#,0.00"></dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataCheckColumn Name="colIsActive" FieldName="IsActive" Width="80px">
                                            <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" ValueGrayed="F" AllowGrayed="false" ValueType="System.String"></PropertiesCheckEdit>
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                    <EditFormLayoutProperties AlignItemCaptionsInAllGroups="true">
                                        <Items>
                                            <dx:GridViewLayoutGroup Caption="Vehicle Detail" GroupBoxDecoration="HeadingLine" ColCount="2">
                                                <Items>
                                                    <dx:GridViewColumnLayoutItem ColumnName="colIsActive" Caption="Is Active ?" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="colCarName" Caption="Name" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="colLicense" Caption="License Plat" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="colKilometer" Caption="Current Kilometer" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
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
</asp:Content>
