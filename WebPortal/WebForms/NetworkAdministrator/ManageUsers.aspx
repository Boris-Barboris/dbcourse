<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="WebPortal.WebForms.NetworkAdministrator.ManageUsers" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="NetworkAdmin.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:GridView ID="usersGridView" runat="server" AllowPaging="True" CssClass="userGridView" AllowSorting="True" PageSize="15" AutoGenerateColumns="False" ItemType="WebPortal.DataBase.User" SelectMethod="usersGridView_GetData" UpdateMethod="usersGridView_UpdateItem" DeleteMethod="usersGridView_DeleteItem" DataKeyNames="Username">
        <Columns>
            <asp:CommandField ValidationGroup="gridViewVG" ShowEditButton="True" ShowDeleteButton="True" EditText="Изменить" DeleteText="Удалить" CancelText="Отмена" />
            <asp:DynamicField DataField="Username" HeaderText="Логин" ReadOnly="true" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="Password" HeaderText="Пароль" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="Role" HeaderText="Роль" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="passwordChanged" HeaderText="Изменён пароль" ValidationGroup="gridViewVG"/>
            <asp:DynamicField DataField="EMail" HeaderText="e-mail" ValidationGroup="gridViewVG"/>
        </Columns>
    </asp:GridView>
    <asp:ValidationSummary runat="server" CssClass="greyColor validationSummaryMiddle" ShowModelStateErrors="False" ValidationGroup="gridViewVG"/>
    <asp:FormView runat="server" ID="newUserForm" DefaultMode="Insert" ItemType="WebPortal.DataBase.User" InsertMethod="newUserForm_InsertItem" HorizontalAlign="Center">
        <InsertItemTemplate>
            <fieldset runat="server">
                <ol>
                    <asp:DynamicEntity ID="newUserControl" runat="server" Mode="Insert" ValidationGroup="newUserVG" />
                </ol>                
                <asp:Button runat="server" Text="Добавить" CommandName="Insert" SkinID="skn_addBtn" ValidationGroup="newUserVG" />
                <asp:Button runat="server" Text="Отмена" CausesValidation="false" SkinID="skn_addBtn" CommandName="Cancel" />
            </fieldset>
        </InsertItemTemplate>        
    </asp:FormView>     
    <asp:ValidationSummary runat="server" CssClass="greyColor validationSummaryMiddle" ShowModelStateErrors="true" ValidationGroup="newUserVG"/>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
