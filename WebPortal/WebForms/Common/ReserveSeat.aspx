<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="ReserveSeat.aspx.cs" Inherits="WebPortal.WebForms.Common.ReserveSeat" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Common.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label runat="server" Text="Номер рейса" SkinID="skn_largeText" CssClass="blocked middleButton centerText" ID="reservLabel"></asp:Label>
    <asp:FormView runat="server" ID="newReservationForm" DefaultMode="Insert" ItemType="WebPortal.DataBase.Reservation" InsertMethod="newReservationForm_InsertItem" HorizontalAlign="Center" >
        <InsertItemTemplate>
            <fieldset runat="server">
                <ol>
                    <asp:DynamicEntity ID="newReservationControl" runat="server" Mode="Insert" ValidationGroup="newReservationVG" />
                </ol>                
                <asp:Button runat="server" Text="Добавить" CommandName="Insert" SkinID="skn_addBtn" ValidationGroup="newReservationVG" />
                <asp:Button runat="server" Text="Отмена" CausesValidation="false" SkinID="skn_addBtn" CommandName="Cancel" />
            </fieldset>
        </InsertItemTemplate>        
    </asp:FormView>
    <asp:Label runat="server" Text="Ваш паспорт не обнаружен в базе. Введите фамилию и имя." SkinID="skn_smallText" CssClass="blocked middleButton centerText" Visible ="false" ID="nameLabel"></asp:Label>
    <asp:TextBox ID="nameTextBox" runat="server" ValidationGroup="NameVG" CssClass="blocked middleButton" Visible="False"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="nameTextBox" ValidationGroup="NameVG"></asp:RequiredFieldValidator>
    <asp:ValidationSummary runat="server" CssClass="greyColor validationSummaryMiddle" ShowModelStateErrors="true" ValidationGroup="newReservationVG NameVG"/>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
