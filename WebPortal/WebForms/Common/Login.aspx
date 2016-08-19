<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebPortal.WebForms.Common.Login" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Common.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:Panel ID="loginPanel" runat="server" CssClass="absoluteCenter loginPanel" Width="400px" Height="170px">
        <asp:Label runat="server" CssClass="centerText" Width="100%" SkinId="skn_xLargeText" Text="Войдите в систему"/>
        <div class="divLabelEditpair">
            <asp:Label runat="server" CssClass="labelBeforeEdit" SkinId="skn_LargeText" Text="Логин"/>            
            <asp:TextBox runat="server" CssClass="editBoxLogin" ID="loginBox" Width="250px"/>
            <asp:RequiredFieldValidator ID="loginRFValid" CssClass="validatorLogin" runat="server" ErrorMessage="Укажите логин" ControlToValidate="loginBox" ValidationGroup="loginValidGroup" />
        </div>
        <div class="divLabelEditpair">
            <asp:Label runat="server" CssClass="labelBeforeEdit" SkinId="skn_LargeText" Text="Пароль"/>            
            <asp:TextBox runat="server" CssClass="editBoxLogin" ID="passwordBox" Width="250px" TextMode="Password"/>
            <asp:RequiredFieldValidator ID="passwordRFValid" CssClass="validatorLogin" runat="server" ErrorMessage="Введите пароль" ControlToValidate="passwordBox" ValidationGroup="loginValidGroup" />
        </div>
        <asp:Button runat="server" ID="loginButton" CssClass="blocked middleButton" SkinId="skn_loginBtn" Text="Вход" OnClick="loginButton_Click" ValidationGroup="loginValidGroup" />
        <asp:Label ID="loginFailed" runat="server" SkinId="skn_mediumText" Text="Неверный логин или пароль" Visible="False"></asp:Label>
    </asp:Panel>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
