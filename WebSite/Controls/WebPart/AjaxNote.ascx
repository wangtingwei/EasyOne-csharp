<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.WebPart.AjaxNote" Codebehind="AjaxNote.ascx.cs" %>
<asp:UpdatePanel ID="UpTxtEditor" runat="server" UpdateMode="Always">
<ContentTemplate>
<asp:TextBox ID="TxtEditor" runat="server" AutoPostBack="true" TextMode="MultiLine" Width="100%" Height="100" OnTextChanged="TxtEditor_TextChanged"></asp:TextBox>
</ContentTemplate>
</asp:UpdatePanel>