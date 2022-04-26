<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FormItemMaintenance.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Maintenance.ItemMaintenance.FormItemMaintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <Items>
            <dx:LayoutGroup ShowCaption="True" GroupBoxDecoration="HeadingLine" Caption="Item Maintenance">
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
                                    KeyFieldName="ItemCode" 
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
                                    <SettingsText ConfirmDelete="Are you really want to Delete?" PopupEditFormCaption="Item Maintenance"/>
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
                                        <dx:GridViewDataTextColumn Name="colItemCode" Caption="Item Code" FieldName="ItemCode" Width="10%"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colItemDescription" Caption="Description" FieldName="ItemDescription" Width="50%"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Name="colCreatedBy" FieldName="CreatedBy" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colCreatedDateTime" FieldName="CreatedDateTime" Visible="false"></dx:GridViewDataDateColumn>
                                        <dx:GridViewDataTextColumn Name="colLastModifiedBy" FieldName="LastModifiedBy" Visible="false"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn Name="colLastModifiedDateTime" FieldName="LastModifiedDateTime" Visible="false"></dx:GridViewDataDateColumn>
                                        <dx:GridViewDataCheckColumn Name="colIsActive" FieldName="IsActive" Width="80px">
                                            <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" ValueGrayed="F" AllowGrayed="false" ValueType="System.String"></PropertiesCheckEdit>
                                        </dx:GridViewDataCheckColumn>
                                    </Columns>
                                    <EditFormLayoutProperties AlignItemCaptionsInAllGroups="true">
                                        <Items>
                                            <dx:GridViewLayoutGroup Caption="Item Details" GroupBoxDecoration="HeadingLine" ColCount="2">
                                                <Items>
                                                    <dx:GridViewColumnLayoutItem ColumnName="colItemCode" Caption="Item Code"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="colIsActive" Caption="Is Active ?"></dx:GridViewColumnLayoutItem>
                                                    <dx:GridViewColumnLayoutItem ColumnName="colItemDescription" Caption="Description" Width="100%" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
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
