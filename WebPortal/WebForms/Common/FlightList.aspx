<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="FlightList.aspx.cs" Inherits="WebPortal.WebForms.Common.FlightList" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Common.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <div class="centerText filterDiv">
        <asp:Label runat="server" Text="Пункт отправления:"></asp:Label>
        <asp:TextBox ID="depBox" runat="server" AutoPostBack="True" OnTextChanged="depBox_TextChanged"></asp:TextBox>
        <asp:Label runat="server" Text="Пункт прибытия:"></asp:Label>
        <asp:TextBox ID="destBox" runat="server" AutoPostBack="True"></asp:TextBox>
        <asp:CheckBox ID="filterCheckBox" runat="server" OnCheckedChanged="filterCheckBox_CheckedChanged" Text="Фильтр" AutoPostBack="True" />
        <asp:CheckBox ID="showOldFlights" runat="server" Visible="False" OnCheckedChanged="filterCheckBox_CheckedChanged" Text="Показывать прошедшие рейсы" AutoPostBack="True" />
    </div>
    <asp:GridView ID="flightGridView" runat="server" AllowPaging="True" CssClass="userGridView" AllowSorting="True" PageSize="15" AutoGenerateColumns="False" ItemType="WebPortal.DataBase.FlightDetails" SelectMethod="flightGridView_GetData" DataKeyNames="FlightNo" OnRowCommand="flightGridView_RowCommand">
        <Columns>
            <asp:DynamicField DataField="FlightNo" ReadOnly="true" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="Origin" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="Destination" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="DepTime" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="ArrTime" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="AircraftType" ValidationGroup="gridViewVG" Visible="false"/>
            <asp:DynamicField DataField="SeatsEco" ValidationGroup="gridViewVG" Visible="false" HeaderStyle-Width="60"/>
            <asp:DynamicField DataField="EcoFree" ValidationGroup="gridViewVG" HeaderStyle-Width="60"/>
            <asp:DynamicField DataField="SeatsBn" ValidationGroup="gridViewVG" Visible="false" HeaderStyle-Width="60"/>
            <asp:DynamicField DataField="BnFree" ValidationGroup="gridViewVG" HeaderStyle-Width="60"/>
            <asp:DynamicField DataField="FareEco" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="FareBn" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="FareCollected" ValidationGroup="gridViewVG" Visible="false"/>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="ReserveButton" runat="server" CausesValidation="false" CommandName="Reserve"
                        Text="Бронировать" SkinID="skn_reserveBtn" CommandArgument='<%# Eval("FlightNo")%>' Enabled='<%# (Convert.ToInt16(Eval("EcoFree")) > 0) ||(Convert.ToInt16(Eval("BnFree")) > 0) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" Visible="false">
                <ItemTemplate>
                    <asp:Button ID="manageReservationsBtn" runat="server" CausesValidation="false" CommandName="manageReservations"
                        Text="Билеты" SkinID="skn_reserveBtn" CommandArgument='<%# Eval("FlightNo")%>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ValidationSummary runat="server" CssClass="greyColor validationSummaryMiddle" ShowModelStateErrors="True" ValidationGroup="gridViewVG"/>
    <asp:HyperLink ID="addFlightLink" NavigateUrl="~/WebForms/BusinessManager/NewFlight.aspx" SkinId="skn_greyHyperLink" Text="Добавить рейс" runat="server" style="margin-left:20px" Visible="False"/>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
