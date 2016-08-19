<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="PassengerList.aspx.cs" Inherits="WebPortal.WebForms.BusinessManager.PassengerList" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Manager.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <div class="centerText filterDiv">
        <asp:Label runat="server" Text="Мин. выручка"></asp:Label>
        <asp:TextBox ID="minFareBox" runat="server" AutoPostBack="True" OnTextChanged="minFareBox_TextChanged" ValidationGroup="gridViewVG"></asp:TextBox>
        <asp:Label runat="server" Text="Макс. выручка"></asp:Label>
        <asp:TextBox ID="maxFareBox" runat="server" AutoPostBack="True" OnTextChanged="minFareBox_TextChanged" ValidationGroup="gridViewVG"></asp:TextBox>
        <asp:CheckBox ID="filterCheckBox" runat="server" OnCheckedChanged="filterCheckBox_CheckedChanged" Text="Фильтр" AutoPostBack="True" CausesValidation="True" />
        <asp:Label runat="server" style="margin-left:20px" Text="Размер скидки:"></asp:Label>
        <asp:TextBox ID="discountBox" runat="server" ValidationGroup="gridViewVG"></asp:TextBox>
        <asp:Button ID="discountBtn" runat="server" CausesValidation="true" Text="применить скидку к диапазону" SkinID="skn_discountBtn" OnClick="discountBtn_Click"/>   
    </div>
    <asp:GridView ID="passengerGridView" runat="server" AllowPaging="True" CssClass="userGridView" AllowSorting="True" PageSize="15" AutoGenerateColumns="True" ItemType="WebPortal.DataBase.Passenger" SelectMethod="passengerGridView_GetData" DataKeyNames="ID">
    </asp:GridView>
    <asp:ValidationSummary runat="server" CssClass="greyColor validationSummaryMiddle" ShowModelStateErrors="True" ValidationGroup="gridViewVG"/>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
