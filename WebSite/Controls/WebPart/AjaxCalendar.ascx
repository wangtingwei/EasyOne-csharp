<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.WebPart.AjaxCalendar" Codebehind="AjaxCalendar.ascx.cs" %>
<asp:UpdatePanel ID="UpCalendar" runat="server">
    <ContentTemplate>
        <asp:Calendar ID="CalAjax" Width="100%" Height="100%" SelectionMode="DayWeekMonth" runat="server">
        </asp:Calendar>
    </ContentTemplate>
</asp:UpdatePanel>
