<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="FinReport.aspx.cs" Inherits="WebPortal.WebForms.BusinessManager.FinReport" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Manager.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <div class="centerText filterDiv">
        <asp:Label runat="server" Text="Финансовый отчёт за последний год" SkinID="skn_mediumText"></asp:Label>
    </div>
    <asp:GridView ID="reportGridView" runat="server" CssClass="userGridView" SelectMethod="reportGridView_GetData" AutoGenerateColumns="True" ItemType="WebPortal.DataBase.TimeIntervalReport"></asp:GridView>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>

