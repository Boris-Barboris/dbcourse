<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WebPortal.WebForms.Common.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Common.css"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:Label ID="errorLabel" runat="server" Text="Текст ошибки" Font-Size="X-Large" ForeColor="Red" CssClass="errorLabelMiddle"></asp:Label>
</asp:Content>
