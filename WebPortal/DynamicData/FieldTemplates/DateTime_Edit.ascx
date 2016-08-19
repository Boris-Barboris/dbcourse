<%@ Control Language="C#" CodeBehind="DateTime_Edit.ascx.cs" Inherits="WebPortal.DateTime_EditField" %>

<juice:Datepicker runat="server" ID="t1" TargetControlID="TextBox1" DateFormat="dd/mm/yy"/>
<asp:TextBox ID="TextBox1" runat="server" CssClass="DDTextBox" Text='<%# FieldValueEditString %>' Columns="12"></asp:TextBox>
<div style="display:inline">
    χχ
    <asp:TextBox ID="Hours" runat="server" Width="15" Text="00" Columns="2"></asp:TextBox>
    μμ
    <asp:TextBox ID="Minutes" runat="server" Width="15" Text="00" Columns="2"></asp:TextBox>
</div>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" />
<asp:CustomValidator runat="server" ID="DateValidator" CssClass="DDControl DDValidator" ControlToValidate="TextBox1" Display="Static" EnableClientScript="false" Enabled="false" OnServerValidate="DateValidator_ServerValidate" />

<asp:CustomValidator runat="server" ID="HoursValidator" CssClass="DDControl DDValidator" ControlToValidate="Hours" Display="Static" EnableClientScript="false" Enabled="true" OnServerValidate="HoursValidator_ServerValidate" />
<asp:CustomValidator runat="server" ID="MinutesValidator" CssClass="DDControl DDValidator" ControlToValidate="Minutes" Display="Static" EnableClientScript="false" Enabled="true" OnServerValidate="MinutesValidator_ServerValidate" />

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" CssClass="DDControl DDValidator" ControlToValidate="Hours" Display="Static" Enabled="false" />
<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" CssClass="DDControl DDValidator" ControlToValidate="Minutes" Display="Static" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" CssClass="DDControl DDValidator" ControlToValidate="Hours" Display="Static" Enabled="false" ValidationExpression="^dd$"/>
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3" CssClass="DDControl DDValidator" ControlToValidate="Minutes" Display="Static" Enabled="false" ValidationExpression="^dd$"/>
