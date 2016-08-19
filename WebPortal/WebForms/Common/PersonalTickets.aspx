<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="PersonalTickets.aspx.cs" Inherits="WebPortal.WebForms.Common.PersonalTickets" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Common.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:GridView ID="reservationGridView" runat="server" AllowPaging="True" CssClass="userGridView" AllowSorting="True" PageSize="15" AutoGenerateColumns="True" ItemType="WebPortal.DataBase.Reservation" SelectMethod="reservationList_GetData" DataKeyNames="FlightNo" OnRowCommand="reservationList_RowCommand">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="deleteButton" runat="server" CausesValidation="false" CommandName="Cancel"
                        Text="Отменить" SkinID="skn_reserveBtn" CommandArgument='<%# Eval("TicketNo")%>' Visible='<%# (byte)Eval("Status") == 0 %>' PostBackUrl="~/WebForms/Executive/ReservationList.aspx"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
