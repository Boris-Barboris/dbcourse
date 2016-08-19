<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="CustomMessage.aspx.cs" Inherits="WebPortal.WebForms.Common.CustomMessage" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Common.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:Label ID="customLabel" runat="server" CssClass="welcomePos centerText" SkinID="skn_welcomeLabel" Text="Добро пожаловать на сайт нашей авиакомпании!"/>
</asp:Content>
