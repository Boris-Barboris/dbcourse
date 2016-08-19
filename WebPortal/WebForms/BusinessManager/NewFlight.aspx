<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="NewFlight.aspx.cs" Inherits="WebPortal.WebForms.BusinessManager.NewFlight" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Manager.css"/>
</asp:Content>

<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:FormView runat="server" ID="newFlightForm" DefaultMode="Insert" ItemType="WebPortal.DataBase.FlightDetails" InsertMethod="newFlightForm_InsertItem" CssClass="formViewFlight" HorizontalAlign="Left">
        <InsertItemTemplate>
            <fieldset runat="server">
                <ol>
                    <asp:DynamicEntity ID="newFlightControl" runat="server" Mode="Insert" ValidationGroup="newFlightVG" />
                </ol>
                <asp:Button runat="server" Text="Добавить" CommandName="Insert" SkinID="skn_addBtn" ValidationGroup="newFlightVG" />
                <asp:Button runat="server" Text="Отмена" CausesValidation="false" SkinID="skn_addBtn" CommandName="Cancel" />
            </fieldset>
        </InsertItemTemplate>        
    </asp:FormView>     
    <asp:ValidationSummary runat="server" CssClass="greyColor validationSummaryMiddle" ShowModelStateErrors="true" ValidationGroup="newFlightVG"/>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
