<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ChangeTheme" Codebehind="ChangeTheme.ascx.cs" %>
<div>
    <asp:RadioButtonList ID="RadlTheme" runat="server" DataSourceID="OdsTheme" DataTextField="Name"
        DataValueField="Name" AutoPostBack="True" OnDataBound="RadlTheme_DataBound" OnSelectedIndexChanged="RadlTheme_SelectedIndexChanged">
    </asp:RadioButtonList>
</div>
<br />
<asp:ObjectDataSource ID="OdsTheme" runat="server" SelectMethod="ThemesList" TypeName="EasyOne.Web.ThemeManager">
</asp:ObjectDataSource>
