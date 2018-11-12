<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Welcome" Codebehind="Welcome.ascx.cs" %>
<div>
<asp:Literal ID="LitAdminName" runat="server"></asp:Literal>您好，  <br />
今天是 <asp:Literal ID="LitDateTime" runat="server"></asp:Literal> 您尚有：<br /> 
待审内容：<asp:Literal ID="LitContent" runat="server"></asp:Literal>篇  待签收内容：<asp:Literal ID="LitSignin" runat="server"></asp:Literal>个<br />
待审评论：<asp:Literal ID="LitComment" runat="server"></asp:Literal>条  未读短消息：<asp:Literal ID="LitMessage" runat="server"></asp:Literal>条<br />
</div>