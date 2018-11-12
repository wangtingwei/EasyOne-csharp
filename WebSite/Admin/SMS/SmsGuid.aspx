<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Sms.SmsGuid" Title="无标题页" Codebehind="SmsGuid.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
    发送手机短信
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <div class="guideexpand" onclick="Switch(this)">
        发送手机短信</div>
    <div class="guide">
        <ul>
            <li><a href="SmsMessageToUser.aspx" target="main_right">发送给会员</a></li>
            <li><a href="SmsMessageToContacter.aspx" target="main_right">发送给联系人</a></li>
            <pe:ExtendedLiteral HtmlEncode="false" ID="Literal1" runat="server"></pe:ExtendedLiteral>
            <li><a href="SmsMessageToOther.aspx" target="main_right">发送给其他人</a></li>
            <li><a href="SmsMessageLog.aspx?Action=send" target="main_right">查看短信发送结果</a></li>
            <li><a href="SmsMessageLog.aspx?Action=receive" target="main_right">查看接收的短信</a></li>
            <%--<li><a href="UserManage.aspx" target="main_right">短信通充值</a></li>--%>
        </ul>
    </div>
</asp:Content>
