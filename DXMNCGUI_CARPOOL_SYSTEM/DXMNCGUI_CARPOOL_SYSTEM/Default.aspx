<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Default.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM._Default" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%" Height="100%" BackColor="White">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
        <Items>
            <dx:LayoutGroup GroupBoxDecoration="None" Caption="" GroupBoxStyle-Caption-BackColor="Transparent">
                <GroupBoxStyle>
                    <Caption ForeColor="SlateGray" Font-Size="Larger" Font-Bold="true"></Caption>
                </GroupBoxStyle>
                <Items>
                    <dx:LayoutItem Width="50%" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxImageSlider 
                                    runat="server" 
                                    ID="slider1" 
                                    ClientInstanceName="slider1" Theme="iOS" Width="100%" Height="350px"
                                    ImageSourceFolder="~\Content\Slider">
                                    <SettingsImageArea ImageSizeMode="FillAndCrop" AnimationType="Fade" NavigationButtonVisibility="None"/>
                                    <SettingsNavigationBar Mode="Dots" ThumbnailsNavigationButtonPosition="Inside"/>
                                    <SettingsSlideShow AutoPlay="true" Interval="5000" PlayPauseButtonVisibility="Faded" />
                                </dx:ASPxImageSlider>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutGroup GroupBoxDecoration="HeadingLine" Caption="PT. MNC GUNA USAHA INDONESIA" Width="50%" Height="350px" ColCount="1">
                        <GroupBoxStyle>
                            <Caption ForeColor="#3e4f8d" Font-Size="Larger" Font-Bold="true" BackColor="#ffffff">
                            </Caption>
                        </GroupBoxStyle>
                        <Items>
                            <dx:LayoutItem Width="100%" ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxLabel runat="server" EncodeHtml="False" Text="Pada 4 Desember 2014, <b>PT MNC Kapital Indonesia</b> mengambilalih kepemilikan <b>PT. Indo Finance Perkasa.</b> Seiring dengan akuisisi tersebut, Perseroan berganti nama menjadi PT MNC Guna Usaha Indonesia <b>MNC Leasing.</b> Sesuai dengan anggaran dasar dan ijin yang dimiliki, Perseroan dapat melakukan kegiatan usaha sebagai berikut:
                                            <br/>
                                            <br/>
                                            <b>Pembiayaan Aset Produktif</b>
                                            <br/>
                                            Fasilitas pembiayaan tersedia untuk individu korporasi ataupun institusi yang dapat digunakan untuk investasi barang modal dan modal kerja. MNC Leasing menawarkan pembiayaan dalam bentuk Financial Lease, Pembiayaan Cicilan, Jual-Sewa Kembali untuk para nasabah di berbagai bidang, mulai dari alat berat sampai peralatan medis.
                                            <br/><br/>
                                            <b>Anjak Piutang</b>
                                            <br/>
                                            Menyediakan solusi pembiayaan untuk kebutuhan modal kerja jangka pendek dan bridging loan dengan piutang usaha sebagai jaminannya.
                                            <br/><br/>
                                            <b>Sewa Operasi</b>
                                            <br/>
                                            Menyediakan penyewaan mobil untuk korporasi atau nasabah institusi dalam bentuk sewa operasi ataupun sewa untuk dimiliki.
                                            <br/><br/>
                                            <b>Pembiayaan Syariah</b>
                                            <br/>
                                            Menyediakan produk pembiayaan dengan kontrak Syariah, berfokus pada pembiayaan untuk Ibadah Haji dan pinjaman untuk biaya pendidikan."></dx:ASPxLabel>
                                        <%--<br />--%>
                                        <%--<br />--%>
                                        <%--<dx:ASPxButton runat="server" Text="BOOKING NOW !" Theme="MetropolisBlue"></dx:ASPxButton>--%>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup GroupBoxDecoration="HeadingLine" Caption="" Width="50%" ColCount="1">
                        <GroupBoxStyle>
                            <Caption ForeColor="#3e4f8d" Font-Size="Larger" Font-Bold="true" BackColor="#ffffff">
                            </Caption>
                        </GroupBoxStyle>
                        <Items>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxImage runat="server" Width="80%" ImageUrl="~/Content/Images/image3.png"></dx:ASPxImage>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup GroupBoxDecoration="HeadingLine" Caption="" Width="50%" ColCount="1">
                        <GroupBoxStyle>
                            <Caption ForeColor="#3e4f8d" Font-Size="Larger" Font-Bold="true" BackColor="#ffffff">
                            </Caption>
                        </GroupBoxStyle>
                        <Items>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxImage runat="server" ImageUrl="~/Content/Images/image1.png"></dx:ASPxImage>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup GroupBoxDecoration="HeadingLine" Caption="" Width="50%" ColCount="1">
                        <GroupBoxStyle>
                            <Caption ForeColor="#3e4f8d" Font-Size="Larger" Font-Bold="true" BackColor="#ffffff">
                            </Caption>
                        </GroupBoxStyle>
                        <Items>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxImage runat="server" ImageUrl="~/Content/Images/image2.png"></dx:ASPxImage>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup GroupBoxDecoration="HeadingLine" Caption="" Width="50%" ColCount="1">
                        <GroupBoxStyle>
                            <Caption ForeColor="#3e4f8d" Font-Size="Larger" Font-Bold="true" BackColor="#ffffff">
                            </Caption>
                        </GroupBoxStyle>
                        <Items>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <%--<dx:ASPxImage runat="server" ImageUrl="~/Content/Images/image2.png"></dx:ASPxImage>--%>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>