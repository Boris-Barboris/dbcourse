<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="SearchReservations.aspx.cs" Inherits="WebPortal.WebForms.Executive.SearchReservations" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Executive.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="centerText filterDiv">
        <asp:Label runat="server" Text="Номер паспорта:"></asp:Label>
        <asp:TextBox ID="passIDBox" runat="server"></asp:TextBox>
        <asp:Button ID="searchButton" runat="server" CausesValidation="false" Text="Поиск" SkinID="skn_reserveBtn" OnClick="searchButton_Click"/>
    </div>
    <asp:GridView ID="reservationGridView" runat="server" AllowPaging="True" CssClass="userGridView" AllowSorting="True" PageSize="15" AutoGenerateColumns="True" ItemType="WebPortal.DataBase.Reservation" SelectMethod="reservationList_GetData" DataKeyNames="FlightNo" OnRowCommand="reservationList_RowCommand">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="confirmButton" runat="server" CausesValidation="false" CommandName="Confirm"
                        Text="Оплатить" SkinID="skn_reserveBtn" CommandArgument='<%# Eval("TicketNo")%>' Visible='<%# (byte)Eval("Status") == 0 %>'/>
                    <asp:Button ID="deleteButton" runat="server" CausesValidation="false" CommandName="Cancel"
                        Text="Отменить" SkinID="skn_reserveBtn" CommandArgument='<%# Eval("TicketNo")%>' Visible='<%# (byte)Eval("Status") == 0 %>'/>
                    <asp:Button ID="refundButton" runat="server" CausesValidation="false" CommandName="Refund"
                        Text="Возврат" SkinID="skn_reserveBtn" CommandArgument='<%# Eval("TicketNo")%>' Visible='<%# (byte)Eval("Status") == 1 %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="showCancButton" runat="server" CausesValidation="false" CommandName="ShowCancellation"
                        Text="возврат" SkinID="skn_reserveBtn" CommandArgument='<%# Eval("TicketNo")%>' Visible='<%# (byte)Eval("Status") == 2 %>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:DetailsView ID="listView" runat="server" Width="400px" CssClass="centerText userGridView" ItemType="DataBase.Cancellation" Visible="false" DefaultMode="ReadOnly" BorderStyle="None"></asp:DetailsView>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
