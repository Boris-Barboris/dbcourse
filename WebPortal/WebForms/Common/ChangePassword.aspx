<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterForm.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WebPortal.WebForms.Common.ChangePassword" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="Common.css"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" runat="server">
    <asp:Panel ID="changePwPanel" runat="server" CssClass="absoluteCenter loginPanel" Width="550px" Height="150px">
        <asp:Label runat="server" CssClass="centerText" Width="100%" SkinId="skn_xLargeText" Text="Введите необходимые сведения"/>
        <div class="divLabelEditpair">
            <asp:Label runat="server" CssClass="labelBeforeEdit" SkinId="skn_LargeText" Text="Новый пароль"/>            
            <asp:TextBox runat="server" CssClass="editBoxChangePw" ID="newPwBox1" Width="250px" TextMode="Password"/>
            <asp:RequiredFieldValidator ID="pwBox1RFV" CssClass="validatorChengePw" runat="server" ErrorMessage="Укажите новый пароль" ControlToValidate="newPwBox1" ValidationGroup="changePwVG" />
        </div>
        <div class="divLabelEditpair">
            <asp:Label runat="server" CssClass="labelBeforeEdit" SkinId="skn_LargeText" Text="Повторите пароль"/>            
            <asp:TextBox runat="server" CssClass="editBoxChangePw" ID="newPwBox2" Width="250px" TextMode="Password"/>
            <asp:RequiredFieldValidator ID="pwBox2RFV" CssClass="validatorChengePw" runat="server" ErrorMessage="Повторите новый пароль" ControlToValidate="newPwBox2" ValidationGroup="changePwVG" />
        </div>
        <asp:CompareValidator ID="newPwEqual" runat="server" CssClass="loginPanel greyColor" ErrorMessage="Пароли не совпадают" ControlToValidate="newPwBox2" ControlToCompare="newPwBox1" Type="String" ValidationGroup="changePwVG"/>
        <asp:Button runat="server" ID="changePwBtn" CssClass="blocked middleButton" SkinId="skn_chengePwBtn" Text="Сменить пароль" ValidationGroup="changePwVG" OnClick="changePwBtn_Click"/>
        <asp:Label ID="passwordChanged" runat="server" SkinId="skn_mediumText" Text="Пароль изменён" Visible="False"></asp:Label>
    </asp:Panel>
    <asp:Label ID="errorLabel" runat="server" Text="Ошибка" Visible="False" Font-Size="Medium" ForeColor="Red" CssClass="errorLabel"></asp:Label>
</asp:Content>
