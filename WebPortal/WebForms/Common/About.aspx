<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebPortal.WebForms.Common.About" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Common.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:Label ID="AboutLabel" SkinID="skn_xLargeText" CssClass="about_label" runat="server" Text="Гипотетическая авиакомпания SkyShark для выполнения курсовой работы по дисциплине 'Базы данных'"></asp:Label>
</asp:Content>
