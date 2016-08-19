<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="RegisterClient.aspx.cs" Inherits="WebPortal.WebForms.Common.RegisterClient" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Common.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:Panel ID="registerClientPanel" runat="server" CssClass="absoluteCenter loginPanel" Width="550px" Height="250px">
        <asp:Label runat="server" CssClass="centerText" Width="100%" SkinId="skn_xLargeText" Text="Введите данные аккаунта"/>
        <div class="divLabelEditpair">
            <asp:Label runat="server" CssClass="labelBeforeEdit" SkinId="skn_mediumText" Text="Логин"/>            
            <asp:TextBox runat="server" CssClass="editBoxChangePw" ID="loginBox" Width="250px" ValidationGroup="registerValid" />
            <asp:RequiredFieldValidator ID="loginValidator" CssClass="greyColor validatorChengePw" runat="server" ErrorMessage="Укажите логин" ControlToValidate="loginBox" ValidationGroup="registerValid" InitialValue="" Text="*"/>
            <asp:RegularExpressionValidator ID="loginRegExp" runat="server" ErrorMessage="Логин должен состоять из цифр и латинских букв" CssClass="greyColor validatorChengePw" ValidationGroup="registerValid" ControlToValidate="loginBox" ValidationExpression="^[a-z,A-Z,0-9]*$"  InitialValue="" Text="*"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="loginUnique" CssClass="greyColor validatorChengePw" runat="server" ErrorMessage="Этот логин занят" ControlToValidate="loginBox" ValidationGroup="registerValid" InitialValue="" Text="*"></asp:CustomValidator>
        </div>
        <div class="divLabelEditpair">
            <asp:Label runat="server" CssClass="labelBeforeEdit" SkinId="skn_mediumText" Text="Пароль"/>            
            <asp:TextBox runat="server" CssClass="editBoxChangePw" ID="pwBox1" Width="250px" ValidationGroup="registerValid" TextMode="Password"/>
            <asp:RequiredFieldValidator ID="pwValidator1" CssClass="greyColor validatorChengePw" runat="server" ErrorMessage="Введите пароль" ControlToValidate="pwBox1" ValidationGroup="registerValid" InitialValue="" Text="*"/>
            <asp:RegularExpressionValidator ID="pwRegExp" runat="server" ErrorMessage="Пароль должен состоять из цифр и латинских букв" CssClass="greyColor validatorChengePw" ValidationGroup="registerValid" ControlToValidate="pwBox1" ValidationExpression="^[a-z,A-Z,0-9]*$" InitialValue="" Text="*"></asp:RegularExpressionValidator>
        </div>
        <div class="divLabelEditpair">
            <asp:Label runat="server" CssClass="labelBeforeEdit" SkinId="skn_mediumText" Text="Повторите пароль"/>            
            <asp:TextBox runat="server" CssClass="editBoxChangePw" ID="pwBox2" Width="250px" ValidationGroup="registerValid" TextMode="Password"/>
            <asp:RequiredFieldValidator ID="pwValidator2" CssClass="greyColor validatorChengePw" runat="server" ErrorMessage="Повторите пароль" ControlToValidate="pwBox1" ValidationGroup="registerValid" InitialValue="" Text="*"/>
            <asp:CompareValidator ID="pwSameValidator" runat="server" ErrorMessage="Пароли не совпадают" ControlToCompare="pwBox2" ControlToValidate="pwBox1" CssClass="greyColor validatorChengePw" ValidationGroup="registerValid" InitialValue="" Text="*"></asp:CompareValidator>
        </div>
        <div class="divLabelEditpair">
            <asp:Label runat="server" CssClass="labelBeforeEdit" SkinId="skn_mediumText" Text="E-Mail"/>            
            <asp:TextBox runat="server" CssClass="editBoxChangePw" ID="mailBox" Width="250px" ValidationGroup="registerValid"/>
            <asp:RequiredFieldValidator ID="mailValid" CssClass="greyColor validatorChengePw" runat="server" ErrorMessage="Введите e-mail" ControlToValidate="mailBox" ValidationGroup="registerValid" InitialValue="" Text="*"/>
            <asp:CustomValidator ID="mailUnique" CssClass="greyColor validatorChengePw" runat="server" ErrorMessage="Такой e-mail занят" ControlToValidate="mailBox" ValidationGroup="registerValid" InitialValue="" Text="*"></asp:CustomValidator>
        </div>
        <asp:Button runat="server" ID="registerBtn" CssClass="blocked middleButton" SkinId="skn_registerBtn" Text="Зарегистрироваться" ValidationGroup="registerValid" OnClick="registerBtn_Click"/>
        <asp:ValidationSummary ID="registerValidSummary" runat="server" CssClass="greyColor" ValidationGroup="registerValid"/>
    </asp:Panel>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
