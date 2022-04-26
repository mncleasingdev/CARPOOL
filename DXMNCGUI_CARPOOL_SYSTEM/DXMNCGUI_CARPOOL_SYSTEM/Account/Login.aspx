<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DXMNCGUI_CARPOOL_SYSTEM.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml"; style="width: 100%; height: 100%">
<head runat="server">
    <title>MNCL Carpool Management System</title>
    <link rel="icon" type="image/png" href="Content/Images/NumberingMenuIco-16x16.png" />
</head>
<body style="background-size:cover; 
                background-repeat:no-repeat; 
                    background-image:linear-gradient(#00468b, #40404b);
                        justify-content: center; 
                            align-items: center">
<form id="form1" runat="server">
    <dx:ASPxFormLayout runat="server" ColCount="3" EnableTheming="true">
        <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="800"/>
        <SettingsItemCaptions Location="Left"/>
        <Items>
            <dx:LayoutItem Caption="" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer>
                        <div>
                            <marquee direction="left" onmouseover="this.stop()" onmouseout="this.start()" style="font-family: Verdana; font-size:11px; color:whitesmoke;" loop="infinite">
                                <strong>w e l c o m e &nbsp t o &nbsp m n c &nbsp l e a s i n g &nbsp c a r - p o o l &nbsp m a n a g e m e n t &nbsp s y s t e m</strong>
                            </marquee>
                        </div>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:EmptyLayoutItem Width="100%" Height="100px"></dx:EmptyLayoutItem>
            <dx:EmptyLayoutItem Width="35%"></dx:EmptyLayoutItem>
            <dx:LayoutGroup Name="lgLogin" Width="30%" Height="250px" GroupBoxDecoration="None" BackColor="Transparent">
                <BackgroundImage ImageUrl="../Content/Images/whitebackgroud.jpg"></BackgroundImage>
                <Border BorderColor="WindowFrame" BorderStyle="Double"></Border>
                <Items>
                    <dx:EmptyLayoutItem Height="20px"></dx:EmptyLayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="" HorizontalAlign="Center">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxImage runat="server" ID="ImageLogo" ImageUrl="~/Content/Images/logo.png"  Width="130px" Height="40px"></dx:ASPxImage>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="True" Caption="" HorizontalAlign="Center">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxImage runat="server" ID="ImageLogoLogin" ImageUrl="~/Content/Images/carpoollogin.png" Width="220px" Height="180px"></dx:ASPxImage>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Center" CaptionStyle-Font-Names="Verdana" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxImage runat="server" ImageUrl="~/Content/Images/IDIcon-32x32.png" Width="20px" Height="20px"></dx:ASPxImage>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox runat="server" ID="txtEmail" Theme="DevEx" Width="150px" NullText="Email">
                                                <Border BorderColor="WhiteSmoke" BorderStyle="Inset" BorderWidth="2px"></Border>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    <CaptionStyle Font-Names="Verdana"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Center" CaptionStyle-Font-Names="Verdana" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxImage runat="server" ImageUrl="~/Content/Images/PassIcon-32x32.png" Width="20px" Height="20px"></dx:ASPxImage>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox runat="server" ID="txtPassword" Password="true" Theme="DevEx" Border-BorderStyle="Inset" Border-BorderColor="WhiteSmoke" Border-BorderWidth="2" Width="150px" NullText="Password">
                                                <Border BorderColor="WhiteSmoke" BorderStyle="Inset" BorderWidth="2px"></Border>
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    <CaptionStyle Font-Names="Verdana"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Center" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxImage runat="server" Width="20px" Height="20px"></dx:ASPxImage>
                                        </td>
                                        <td>
                                            <dx:ASPxButton runat="server" ID="btnLogin" Theme="DevEx" ForeColor="GrayText" Border-BorderStyle="Outset" Border-BorderColor="WhiteSmoke" Border-BorderWidth="1"  Width="150px" Text="Login" Font-Names="Verdana" OnClick="btnLogin_Click" UseSubmitBehavior="true">
<Border BorderColor="WhiteSmoke" BorderStyle="Outset" BorderWidth="1px"></Border>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <CaptionStyle Font-Names="Verdana"></CaptionStyle>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="false" Caption="" HorizontalAlign="Center" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxImage runat="server" Width="20px" Height="20px"></dx:ASPxImage>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel runat="server" ID="lblMessage" Theme="Glass" ForeColor="Red" Text="Login" Font-Names="Verdana" Font-Size="XX-Small" Font-Italic="true"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:EmptyLayoutItem Height="20px"></dx:EmptyLayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:EmptyLayoutItem Width="35%"></dx:EmptyLayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxTimer runat="server" ID="timer1" ClientInstanceName="timer1" OnTick="timer1_Tick"></dx:ASPxTimer>
</form>
</body>
</html>