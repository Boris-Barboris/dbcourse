<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="BackUp.aspx.cs" Inherits="WebPortal.WebForms.NetworkAdministrator.BackUp" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="NetworkAdmin.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <div class="centerText filterDiv">
        <asp:Label runat="server" Text="Создать резервную копию БД SkySharkDB:" SkinID="skn_largeText"></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="Создать" OnClick="Button1_Click" />
    </div>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
    <asp:Button ID="generatePassengers" runat="server" Text="Сгенерировать траффик" OnClick="generatePassengers_Click" />
</asp:Content>
