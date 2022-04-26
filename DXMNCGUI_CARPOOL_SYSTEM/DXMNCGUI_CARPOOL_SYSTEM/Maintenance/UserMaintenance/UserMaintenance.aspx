<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="UserMaintenance.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Maintenance.UserMaintenance.UserMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/Application.js"></script>
    <script type="text/javascript">
    </script>
        <dx:ASPxHiddenField runat="server" ID="HiddenField" ClientInstanceName="HiddenField"/>
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
        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" HeaderText="Update password" Width="307px" ClientInstanceName="popup" Theme="Office2010Blue">
				<ContentCollection>
					<dx:PopupControlContentControl ID="Popupcontrolcontentcontrol2" runat="server">
                        <dx:ASPxFormLayout ID="ASPxFormLayout7" runat="server" Font-Size="9" Font-Names="Calibri">
                            <Items>
                                <dx:LayoutItem ShowCaption="True" Caption="New Password">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="npsw" runat="server" Password="True" ClientInstanceName="npsw">
										        <ClientSideEvents Validation="function(s, e) {e.isValid = (s.GetText().length&gt;5)}" />
										        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="The password lengt should be more that 6 symbols">
										        </ValidationSettings>
									        </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="True" Caption="Confirm New Password">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="cnpsw" runat="server" Password="True" ClientInstanceName="cnpsw">
										        <ClientSideEvents Validation="function(s, e) {e.isValid = (s.GetText() == npsw.GetText());}" />
										        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="The password is incorrect">
										        </ValidationSettings>
									        </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:EmptyLayoutItem Width="50%"></dx:EmptyLayoutItem>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="confirmButton" runat="server" Text="Update" Theme="Office2010Blue" AutoPostBack="False" OnClick="confirmButton_Click">
						                    </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
					</dx:PopupControlContentControl>
				</ContentCollection>
			</dx:ASPxPopupControl>
        <dx:ASPxFormLayout runat="server" ID="FormLayout1" ClientInstanceName="FormLayout1" Theme="PlasticBlue">
            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
            <Items>
                <dx:LayoutGroup Name="FormCaption" GroupBoxDecoration="HeadingLine" Caption="User Maintenance">
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
                                        KeyFieldName="EMAIL" 
                                        AutoGenerateColumns="False"
                                        Theme="Office2010Blue" 
                                        EnableTheming="true"
                                        Width="100%" Font-Size="9" Font-Names="Calibri" 
                                        OnCellEditorInitialize="gvMain_CellEditorInitialize"
                                        OnRowInserting="gvMain_RowInserting" 
                                        OnRowUpdating="gvMain_RowUpdating" 
                                        OnRowDeleting="gvMain_RowDeleting" OnRowValidating="gvMain_RowValidating">
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
                                            <dx:GridViewCommandColumn Name="ClmnCommand" ShowApplyFilterButton="true" ShowClearFilterButton="true" ShowDeleteButton="false" ShowEditButton="true" ShowInCustomizationForm="true" ShowNewButtonInHeader="True" VisibleIndex="0" Visible="true" Width="100px"></dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Name="colUserName" Caption="User Name" FieldName="USER_NAME" Visible="true"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colEmail" Caption="Email" FieldName="EMAIL" Visible="true"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colPassword" Caption="Password" FieldName="USER_PASSWORD" Visible="false">
                                                <PropertiesTextEdit Password="true"></PropertiesTextEdit>
                                                <EditItemTemplate>
							                        <dx:ASPxTextBox ID="pswtextbox" runat="server" Text='<%# Bind("USER_PASSWORD") %>'
								                        Visible='<%# gvMain.IsNewRowEditing %>' Password="True" Width="100%" Theme="Office2010Blue">
								                        <ClientSideEvents Validation="function(s,e){e.isValid = s.GetText()>5;}" />
							                        </dx:ASPxTextBox>
							                        <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="popup.ShowAtElement(this); return false;" Visible='<%#!gvMain.IsNewRowEditing%>'>Edit password</asp:LinkButton>
						                        </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colCompanyName" Caption="Company Name" FieldName="CompanyName" Visible="false"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Name="colDepartment" Caption="Department" FieldName="Department" Visible="true"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataCheckColumn Name="colIsActive" Caption="Active?" FieldName="IS_ACTIVE_FLAG" Width="80px">
                                                <PropertiesCheckEdit ValueChecked="1" ValueUnchecked="0" ValueGrayed="0" AllowGrayed="false" ValueType="System.Int32"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn Name="colIsAdmin" Caption="Admin?" FieldName="IsAdmin" Width="80px">
                                                <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" AllowGrayed="false" ValueType="System.String"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn Name="colIsCoordinator" Caption="Dispatcher?" FieldName="IsCoordinator" Width="80px">
                                                <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" AllowGrayed="false" ValueType="System.String"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn Name="colIsCustomer" Caption="Customer?" FieldName="IsCustomer" Width="80px">
                                                <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" AllowGrayed="false" ValueType="System.String"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataCheckColumn Name="colIsDriver" Caption="Driver?" FieldName="IsDriver" Width="80px">
                                                <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" AllowGrayed="false" ValueType="System.String"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataTextColumn Name="colHandphone" Caption="Hp" FieldName="Hp" Visible="true"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataCheckColumn Name="colNeedApproval" Caption="Approval?" FieldName="NeedApproval" Width="80px">
                                                <PropertiesCheckEdit ValueChecked="T" ValueUnchecked="F" AllowGrayed="false" ValueType="System.String"></PropertiesCheckEdit>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataComboBoxColumn Name="colApprover" Caption="Approver" FieldName="Approver">
                                                <PropertiesComboBox DataSourceID="dsMasterEmail" ClientInstanceName="colApprover" TextField="EMAIL" ValueField="EMAIL" IncrementalFilteringMode="Contains" TextFormatString="{2}" DropDownStyle="DropDown">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Company" FieldName="CompanyName"></dx:ListBoxColumn>
                                                        <dx:ListBoxColumn Caption="User Name" FieldName="USER_NAME"></dx:ListBoxColumn>
                                                        <dx:ListBoxColumn Caption="Email" FieldName="EMAIL"></dx:ListBoxColumn>
                                                    </Columns>
                                                    <ItemStyle Font-Size="Smaller" />
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                        </Columns>
                                        <EditFormLayoutProperties AlignItemCaptionsInAllGroups="true">
                                            <Items>
                                                <dx:GridViewLayoutGroup Caption="User Detail" GroupBoxDecoration="HeadingLine" ColCount="2">
                                                    <Items>

                                                        <dx:GridViewColumnLayoutItem ColumnName="USER_NAME" Caption="Name" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="IS_ACTIVE_FLAG" Caption="Is Active ?"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="USER_PASSWORD" Caption="Password" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="NeedApproval" Caption="Need Approval ?"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="EMAIL" Caption="Email" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="Approver" Caption="Approver"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="CompanyName" Caption="CompanyName" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="Department" Caption="Department" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="Hp" Caption="Phone" RequiredMarkDisplayMode="Required"></dx:GridViewColumnLayoutItem>
                                                        <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="IsAdmin" Caption="Administrator" Width="25%"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="IsCoordinator" Caption="Dispatcher" Width="25%"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="IsCustomer" Caption="Customer" Width="25%"></dx:GridViewColumnLayoutItem>
                                                        <dx:GridViewColumnLayoutItem ColumnName="IsDriver" Caption="Driver" Width="25%"></dx:GridViewColumnLayoutItem>
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
        <asp:SqlDataSource ID="dsMasterEmail" runat="server" ConnectionString="<%$ ConnectionStrings:SqlLocalConnectionString %>"
        SelectCommand="SELECT USER_NAME, EMAIL, CompanyName FROM [dbo].[MasterUser] WHERE isCustomer='T' and IS_ACTIVE_FLAG='1' ORDER BY USER_NAME">
    </asp:SqlDataSource>
</asp:Content>
